using System;
using System.IO;
using System.Linq;

namespace HtmlParser.Language
{
    public class TranslateDeRuParser : LanguageParser
    {
        public void Parse()
        {
            string file = "list.txt";

            var lines = File.ReadLines(file);

            var list = lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => l.Trim())
                    .Select(Parse)
                    .OrderBy(l => l.De)
                    .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected WordClass Parse(string de)
        {
            Console.WriteLine(de);
            
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var document = GetHtml(hostUrl + de);

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@class='romhead']");
            if (trNode == null)
            {
                Console.WriteLine("Not found");
                return new WordClass
                {
                    De = de
                };
            }
            var wordClassNode = trNode.SelectSingleNode(".//span[@class='wordclass']");
            var wordClass = wordClassNode.InnerText.ToLower();

            WordClass word;

            switch (wordClass)
            {
                case "verb" :
                    word = GetVerb(trNode, de);
                    break;
                case "subst" :
                    word = GetSubstantiv(trNode, de);
                    break;
                default:
                    word = GetWordClass(trNode, de);
                    break;
            }

            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//div[@class='target']//a");
            word.Ru = trNode.InnerText.Trim().Replace("\u0341", "");

            //hostUrl = $"https://api.lingvolive.com/sounds?uri=Universal%20(De-Ru)%2F{word}.wav";
            //UploadSound(hostUrl, word.De, "wav");

            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//dl[@data-translation='0']");
            hostUrl = $"https://sounds.pons.com/audio_tts/de/{trNode.Id}?target=mp3";
            UploadSound(hostUrl, word.De, "mp3", "D:\\Temp\\Sounds\\");

            return word;
        }
    }
}