using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TextConverter.Models;
using static TextConverter.DownloadHelper;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public class WordConverter
    {
        public static readonly string[] separators = new string[] { "(", ")", "/", "-" };

        protected async Task ProcessWordsAsync(IList<WordItem> words, WordData data)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var word in words)
                {
                    string normalized = GetNormalized(word.GetItem(En));
                    if (string.IsNullOrEmpty(normalized))
                    {
                        continue;
                    }
                    data.InfoAction?.Invoke(normalized);
                    string trExisted = GetNormalized(word.GetItem(EnTr));

                    bool isMultiWord = IsMultiWord(normalized);
                    string tr = null;
                    string ru = null;
                    if (isMultiWord)
                    {
                        if (data.DownloadTranscription)
                        {
                            tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, data);
                        }
                        if (data.DownloadTranslation)
                        {
                            string html = await DownloadHtmlAsync(web, normalized, data.ErrorAction);
                            word.Rank = ParseRank(html);
                            ru = ParseRu(html, normalized);
                        }
                    }
                    else
                    {
                        string html = await DownloadHtmlAsync(web, normalized, data.ErrorAction);
                        word.Rank = ParseRank(html);
                        if (data.DownloadTranscription)
                        {
                            tr = ParseTranscription(html, normalized, data.Region);
                        }
                        if (data.DownloadTranslation)
                        {
                            ru = ParseRu(html, normalized);
                        }
                    }
                    if (!string.IsNullOrEmpty(tr) && trExisted != tr)
                    {
                        word.AddItem(EnTr, tr);
                    }
                }
            }
        }

        protected async Task<string> DownloadMultiWordsTranscriptionAsync(WebClient web, string normalized, WordData data)
        {
            var words = normalized.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            var brackets = new Brackets();
            foreach (var word in words)
            {
                await ProcessWordAsync(web, sb, GetNormalized(word), brackets, data);
            }
            sb.Insert(0, "[");
            sb.Append("]");
            return sb.ToString();
        }

        protected bool IsMultiWord(string normalized)
        {
            return normalized.Contains(" ") || normalized.Contains("-");
        }

        private async Task ProcessWordAsync(WebClient web, StringBuilder sb, string wrd, Brackets brackets, WordData data)
        {
            if (wrd.StartsWith("("))
            {
                brackets.IsBrackets = true;
            }
            if (brackets.IsBrackets)
            {
                if (wrd.EndsWith(")"))
                {
                    brackets.IsBrackets = false;
                }
                return;
            }
            if (sb.Length != 0)
            {
                sb.Append(" ");
            }
            if (separators.Contains(wrd))
            {
                sb.Append(wrd);
                return;
            }
            string tr = await DownloadTranscriptionAsync(web, wrd, ParseTranscriptionWithoutBrackets, data);
            if (!string.IsNullOrEmpty(tr))
            {
                sb.Append(tr);
            }
            else
            {
                if (wrd.Contains("-"))
                {
                    var words = wrd.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        await ProcessWordAsync(web, sb, GetNormalized(word), brackets, data);
                    }
                }
                else
                {
                    sb.Append($"<{wrd}>");
                }
            }
        }

    }
}
