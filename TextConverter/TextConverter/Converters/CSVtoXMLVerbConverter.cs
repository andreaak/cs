using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TextConverter.Models;
using static TextConverter.DownloadHelper;

namespace TextConverter
{
    public class CSVtoXMLVerbConverter : WordConverter, IWordConverter
    {
        public string InExtension => ".csv";
        public string OutExtension => ".xml";

        public async Task ConvertFileAsync(string filePath, string outputFilePath, WordData data)
        {
            var verbs = CSVHelper.ReadVerbs(data.Path);
            await ProcessVerbsAsync(verbs, data);
            XMLHelper.WriteVerbs(outputFilePath, verbs);
            CSVHelper.WriteVerbs(outputFilePath + InExtension, verbs);
        }

        private async Task ProcessVerbsAsync(IList<EnVerbItem> verbs, WordData data)
        {
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;

                foreach (var verb in verbs)
                {
                    if (string.IsNullOrEmpty(verb.InfinitiveTranscription) || data.DownloadTranscription)
                    {
                        //var temps = GetWords(verb.Infinitive);
                        data.InfoAction?.Invoke(verb.Infinitive);
                        verb.InfinitiveTranscription = await DownloadVerbTranscriptionAsync(web, verb.Infinitive, data);
                    }
                    if (string.IsNullOrEmpty(verb.PastSimpleTranscription) || data.DownloadTranscription)
                    {
                        //var temps = GetWords(verb.PastSimple);
                        data.InfoAction?.Invoke(verb.PastSimple);
                        verb.PastSimpleTranscription = await DownloadVerbTranscriptionAsync(web, verb.PastSimple, data);
                    }
                    if (string.IsNullOrEmpty(verb.PastPaticipleTranscription) || data.DownloadTranscription)
                    {
                        //var temps = GetWords(verb.PastPaticiple);
                        data.InfoAction?.Invoke(verb.PastPaticiple);
                        verb.PastPaticipleTranscription = await DownloadVerbTranscriptionAsync(web, verb.PastPaticiple, data);
                    }
                }
            }
        }

        private async Task<string> DownloadVerbTranscriptionAsync(WebClient web, string verb, WordData data)
        {
            string normalized = GetNormalized(verb);
            string tr;
            if (IsMultiWord(normalized))
            {
                tr = await DownloadMultiWordsTranscriptionAsync(web, normalized, data);
            }
            else
            {
                tr = await DownloadTranscriptionAsync(web, normalized, ParseTranscription, data);
            }
            return tr;
        }
    }
}
