using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    class DownloadHelper
    {
        private const string OutputWordsSounds = "Sounds/{1}_{3}/{2}_{1}/{0}_{1}.{3}";
        private const string OutputVerbsSounds = "Sounds/Irregular/{1}_{3}/{2}_{1}/{0}_{1}.{3}";
        //private const string SoundDestination = "http://wooordhunt.ru/data/sound/word/{0}/mp3/{1}.mp3";
        private const string SoundDestination = "http://wooordhunt.ru/data/sound/word/{0}/{2}/{1}.{2}";

        public static void DownloadVerbSounds(WebClient web, string verbsCombined, string soundFormat, Action<string> infoAction, Action<string> errorAction)
        {
            var verbs = verbsCombined.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var verb in verbs)
            {
                DownloadSounds(web, GetNormalized(verb), OutputVerbsSounds, soundFormat, infoAction, errorAction);
            }
        }

        public static bool DownloadWordsSounds(WebClient web, string normalized, string soundFormat,
            Action<string> infoAction, Action<string> errorAction)
        {
            return DownloadSounds(web, normalized, OutputWordsSounds, soundFormat, infoAction, errorAction);
        }

        private static bool DownloadSounds(WebClient web, string normalized, string destination, string soundFormat,
            Action<string> infoAction, Action<string> errorAction)
        {
            bool res = true;
            try
            {
                infoAction(normalized);
                if (!IsWordFileExist(normalized, Us, destination, soundFormat))
                {
                    res = DownloadSound(web, normalized, Us, destination, soundFormat);
                }
                if (!IsWordFileExist(normalized, Uk, destination, soundFormat))
                {
                    res = DownloadSound(web, normalized, Uk, destination, soundFormat);
                }
            }
            catch (Exception ex)
            {
                errorAction(ex.Message);
                res = false;
            }
            return res;
        }

        private static bool DownloadSound(WebClient web, string normalized, string region,
            string destination, string soundFormat)
        {
            string res = string.Format(SoundDestination, region, normalized, soundFormat);
            var bt = web.DownloadData(res);
            if (IsError(bt)) //html page
            {
                return false;
            }

            string dest = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" +
                        string.Format(destination, normalized, region, normalized[0], soundFormat);
            string folder = Path.GetDirectoryName(dest);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllBytes(dest, bt);
            return true;
        }

        private static bool IsWordFileExist(string normalized, string region, string destination, string soundFormat)
        {
            return File.Exists(string.Format(destination, normalized, region, normalized[0], soundFormat));
        }

        private static bool IsError(byte[] bt)
        {
            return bt.Length < 4 ||
                   (bt[0] == 60 && bt[1] == 33 && bt[2] == 68 && bt[3] == 79);//html page
        }

        //Transcription

        public static async Task<string> DownloadHtmlAsync(WebClient web, string normalized, Action<string> errorAction)
        {
            string html = null;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                html = await web.DownloadStringTaskAsync(res);
            }
            catch (Exception ex)
            {
                errorAction?.Invoke(ex.Message);
            }
            return html;
        }

        public static async Task<string> DownloadTranscriptionAsync(WebClient web, string normalized,
            Func<string, string, string, string> parseAction, Action<string> errorAction, string region)
        {
            string tr = null;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                var html = await web.DownloadStringTaskAsync(res);
                tr = parseAction(html, normalized, region);
            }
            catch (Exception ex)
            {
                errorAction?.Invoke(ex.Message);
            }
            return tr;
        }

        public static string ParseTranscription(string html, string normalized, string region)
        {
            string transcription = ParseTranscriptionWithoutBrackets(html, normalized, region);
            if (string.IsNullOrEmpty(transcription))
            {
                return string.Empty;
            }
            return "[" + transcription + "]";
        }

        public static string ParseTranscriptionWithoutBrackets(string html, string normalized, string region)
        {
            const string usPattern = "<span title=\"американская транскрипция слова {0}\" class=\"transcription\">";
            const string ukPattern = "<span title=\"британская транскрипция слова {0}\" class=\"transcription\">";
            string startPattern = region == "us" ? usPattern : ukPattern;
            string search = string.Format(startPattern, normalized);
            int startIndex = html.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return string.Empty;
            }
            startIndex += search.Length;

            string endPattern = "</span>";
            int endIndex = html.IndexOf(endPattern, startIndex, StringComparison.OrdinalIgnoreCase);
            string output = html.Substring(startIndex, endIndex - startIndex).Replace("|", "").Trim();
            return output;
        }

        public static async Task<int> DownloadRankAsync(WebClient web, string normalized, Action<string> errorAction)
        {
            int rank = int.MaxValue;
            try
            {
                string res = string.Format("http://wooordhunt.ru/word/{0}", normalized);
                var html = await web.DownloadStringTaskAsync(res);
                rank = ParseRank(html, normalized);
            }
            catch (Exception ex)
            {
                errorAction?.Invoke(ex.Message);
            }
            return rank;
        }

        public static int ParseRank(string html, string normalized)
        {
            const string pattern = "<span id=\"rank_box\">";
            int startIndex = html.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return int.MaxValue;
            }
            startIndex += pattern.Length;

            string endPattern = "</span>";
            int endIndex = html.IndexOf(endPattern, startIndex, StringComparison.OrdinalIgnoreCase);
            string output = html.Substring(startIndex, endIndex - startIndex).Replace(" ", "").Trim();
            int i = 0;
            for (; i < output.Length; i++)
            {
                if(!char.IsNumber(output[i]))
                {
                    break;
                }
            }
            int rank;
            if(int.TryParse(output.Substring(0, i), out rank))
            {
                return rank;
            }
            return int.MaxValue;
        }

        public static string ParseRu(string html, string normalized, string region)
        {
            const string pattern = "<span class=\"t_inline_en\">";

            string search = string.Format(pattern, normalized);
            int startIndex = html.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (startIndex < 0)
            {
                return string.Empty;
            }
            startIndex += search.Length;

            string endPattern = "</span>";
            int endIndex = html.IndexOf(endPattern, startIndex, StringComparison.OrdinalIgnoreCase);
            string output = html.Substring(startIndex, endIndex - startIndex).Replace(",", " /").Trim();
            return output;
        }

        public static string GetNormalized(string word)
        {
            return word.Trim().ToLower();
        }
    }
}
