using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{
    public class TranslateRuDeParser : LanguageParser, ILanguageParser
    {
        public TranslateRuDeParser(bool order, WordType type)
        : base(order, type)
        { }
        
        public void Parse(IList<string> lines)
        {
            var temp = lines.Where(l => !string.IsNullOrEmpty(l))
                .SelectMany(l => Parse(l.Trim()));

            var list = _order ?
                temp.OrderBy(l => l.De).ToArray() :
                temp.ToArray();

            using (var sw = File.CreateText("out.txt"))
            {
                foreach (var item in list)
                {
                    item.Write(sw);
                }
            }
        }

        protected IList<WordClass> Parse(string ru)
        {
            Console.WriteLine(ru);

            var hostUrl = "https://de.pons.com/%C3%BCbersetzung/russisch-deutsch/";

            var document = GetHtml(hostUrl + ru);

            string de = null;
            string wordClass = null;
            string link = null;

            var translationContainerRu = GetTranslationContainer(document, ru);
            if (translationContainerRu?.FirstOrDefault()?.Node != null)
            {
                if (_type == WordType.Other)
                {
                    wordClass = translationContainerRu.First().Node.GetWordClass();
                }
                de = translationContainerRu.First().Node.GetTranslation(ru);
                link = translationContainerRu.First().Node.GetLink();
            }

            if (string.IsNullOrEmpty(de))
            {
                Console.WriteLine($"Not found {ru}");
                return new List<WordClass>
                {
                    new WordClass
                    {
                        Ru = ru
                    }
                };
            }

            hostUrl = link == null ? $"https://de.pons.com/%C3%BCbersetzung/deutsch-russisch/{de}" : $"https://de.pons.com{link}";
            document = GetHtml(hostUrl);

            if (_type == WordType.Other
                && !string.IsNullOrEmpty(wordClass))
            {
                _type = (WordType)Enum.Parse(typeof(WordType), wordClass, true);
            }
            var translationContainerDe = GetTranslationContainer(document, de, _type);
            if (translationContainerDe == null)
            {
                Console.WriteLine($"Not found {de}");
                if (_type == WordType.Subst)
                {
                    var nd = document.DocumentNode.SelectSingleNode(".//div[@class='results']//div[@class='target']");
                    var genus = nd?.SelectSingleNode(".//span[@class='genus']")?.InnerText.Trim().ToLower();
                    return new List<WordClass>
                    { new Substantiv
                    {
                        De = de,
                        Ru = ru,
                        Genus = genus,
                        Artikle = GetArtikle(genus)
                    }};
                }
                return new List<WordClass> {new WordClass
                {
                    De = de,
                    Ru = ru,
                    
                }};
            }

            if (translationContainerDe.Count == 0)
            {
                return new List<WordClass> {new WordClass
                {
                    De = de,
                    Ru = ru,
                    //DeTranscription = GetTranscription(translationContainerDe.Node, translationContainerDe.AllNodes)
                }};
            }

            var word = GetWords(translationContainerDe, de).First();
            word.Ru = ru;
            UploadSound(translationContainerDe.First().Node, word.De);

            return new List<WordClass> { word };
        }
    }
}