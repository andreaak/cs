using System.IO;
using System.Linq;

namespace HtmlParser.Language
{
    class TranscriptionResponse
    {
        public string Text { get; set; }
    }

    public class TranslateRuDeParser : LanguageParser
    {
        public void Parse()
        {
            string file = "list.txt";

            var lines = File.ReadLines(file);

            var list = lines.Where(l => !string.IsNullOrEmpty(l))
                .Select(l => Parse(l.Trim()))
                .ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected WordClass Parse(string ru)
        {
            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/russisch-deutsch/";

            var document = GetHtml(hostUrl + ru);

            var trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//div[@class='target']//a");
            var de = trNode.InnerText.Trim();

            hostUrl = "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/";
            document = GetHtml(hostUrl + de);
            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='romhead']");

            var wordClass = trNode.SelectSingleNode(".//span[@class='wordclass']").InnerText.ToLower();

            WordClass word;

            switch (wordClass)
            {
                case "verb":
                    word = GetVerb(trNode, de);
                    break;
                case "subst":
                    word = GetSubstantiv(trNode, de);
                    break;
                default:
                    word = GetWordClass(trNode, de);
                    break;
            }

            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//div[@class='target']//a");
            word.Ru = trNode.InnerText.Trim();

            //hostUrl = $"https://api.lingvolive.com/sounds?uri=Universal%20(De-Ru)%2F{word}.wav";
            //UploadSound(word.De, hostUrl);

            trNode = document.DocumentNode.SelectSingleNode(".//div[@class='translations first']//dl[@data-translation='0']");
            hostUrl = $"https://sounds.pons.com/audio_tts/de/{trNode.Id}?target=mp3";
            UploadSound(hostUrl, word.De, "mp3", "D:\\Temp\\Sounds\\");

            return word;
        }


    }
}