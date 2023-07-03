using System;
using System.IO;
using System.Linq;

namespace HtmlParser.Language
{
    public class TranslateDeRuVerbParser : LanguageParser
    {
        public void Parse()
        {
            string file = "list.txt";

            var lines = File.ReadLines(file);

            var list = lines.Where(l => !string.IsNullOrEmpty(l))
                    .Select(l => l.Trim())
                    .Select(Parse)
                    .OrderBy(l => l.Infinitive.De)
                    .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected IrregularDeVerb Parse(string de)
        {
            Console.WriteLine(de);

            var items = de.Split(new [] {"\t"}, StringSplitOptions.RemoveEmptyEntries);

            string infinitive = items[0];
            string present = items[1];
            string prateritum = items[2];
            string part2 = items[3];
            string ru = items[4];

            return new IrregularDeVerb
            {
                Infinitive = GetWordClass(infinitive),
                Present = GetWordClass(present),
                Prateritum = GetWordClass(prateritum),
                Part2 = GetPartClass(part2),
                Ru = ru
            };
        }

        private WordClass GetWordClass(string wordDe)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";


            var document = GetHtml(hostUrl + wordDe);

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@class='romhead']");
            if (trNode == null)
            {
                Console.WriteLine($"Not found {wordDe}");
                return new WordClass
                {
                    De = wordDe
                };
            }

            WordClass word = GetVerb(trNode, wordDe);


            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//dl[@data-translation='0']");
            hostUrl = $"https://sounds.pons.com/audio_tts/de/{trNode.Id}?target=mp3";
            UploadSound(hostUrl, word.De, "mp3", "D:\\Temp\\Sounds\\");

            return word;
        }

        private IrrVerb GetPartClass(string wordDe)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";

            var items = wordDe.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            var document = GetHtml(hostUrl + items[1]);

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@class='romhead']");
            if (trNode == null)
            {
                Console.WriteLine($"Not found {wordDe}");
                return new IrrVerb
                {
                    De = wordDe
                };
            }

            var word = GetVerb(trNode, items[1], items[0]);


            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//dl[@data-translation='0']");
            hostUrl = $"https://sounds.pons.com/audio_tts/de/{trNode.Id}?target=mp3";
            UploadSound(hostUrl, word.De, "mp3", "D:\\Temp\\Sounds\\");

            return word;
        }
    }
}