using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using HtmlParser.Language.Model;

namespace HtmlParser.Language
{

    internal interface ILanguageParser
    {
        void Parse(IList<string> lines);
    }

    public class LanguageParser : HtmlParser
    {
        private const string SoundsFolder = "D:\\Temp\\Deutch\\DeutschAndroid\\Sounds\\de";

        private static readonly ReplaceItem[] NORMALIZATION = { new ReplaceItem("ä", "!a") ,
            new ReplaceItem("ö", "!o"),
            new ReplaceItem("ü", "!u"),
            new ReplaceItem("ß", "!s")};
        
        protected bool _order;
        protected WordType _type;

        public LanguageParser(bool order, WordType type)
        {
            _order = order;
            _type = type;
        }

        protected void UploadSound(string hostUrl, string word, string ext, string parentPath)
        {
            try
            {
                var normalized = Normalize(word);
                string folder = parentPath + "\\" + (normalized.StartsWith("!") ? normalized.Substring(0, 2) : normalized.Substring(0, 1));
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string path = $"{folder}\\{Normalize(word)}.{ext}";

                //if (!File.Exists(path))
                {
                    //Console.WriteLine($"Upload sound {hostUrl}");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(hostUrl);
                    HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream output = File.OpenWrite(path))
                        using (Stream input = resp.GetResponseStream())
                        {
                            if (input != null)
                            {
                                input.CopyTo(output);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected IList<HtmlNode> GetVerbFormsContainer(HtmlDocument document)
        {
            return document?.DocumentNode.SelectNodes(".//div[@class='ft-single-table']");
        }

        protected IList<WordClass> GetWords(IList<TranslationContainer> trContainers, string de)
        {
            return GetWords(trContainers, de, dei => new WordClass
            {
                De = dei
            });
        }
        protected IList<WordClass> GetWordsWithoutEmpty(IList<TranslationContainer> trContainers, string de)
        {
            return GetWords(trContainers, de, dei => null);
        }

        protected IList<WordClass> GetWords(IList<TranslationContainer> trContainers, string de, Func<string, WordClass> emptyCreator)
        {
            return trContainers.Select(trContainer =>
            {
                var node = trContainer.Node;
                if (node == null)
                {
                    return emptyCreator(de);
                }

                var word = GetWord(trContainer, de);
                if (trContainer.Node != null)
                {
                    word.Ru = string.Join("/", trContainer.Node.GetTranslations(de).Distinct());
                }
                return word;
            }).Where(w => w != null).ToArray();
        }

        private WordClass GetWord(TranslationContainer trContainer, string de)
        {
            var node = trContainer.Node;
            var allNodes = trContainer.AllNodes;
            
            var wordClass = node.GetWordClass();

            WordClass word;

            switch (wordClass)
            {
                case "verb":
                    word = GetVerb(node, de, allNodes);
                    break;
                case "subst":
                    word = GetSubstantiv(node, de, allNodes);
                    break;
                default:
                    word = GetWordClass(node, de, allNodes);
                    break;
            }

            return word;
        }

        protected void UploadSound(ITranslationItem trNode, string de)
        {
            //var soundNode = trNode.SelectSingleNode(".//div[@class='translations first']//dl");
            var soundNode = trNode?.RomHeadNode;
            if (soundNode == null)
            {
                return;
            }
            string hostUrl = $"https://sounds.pons.com/audio_tts/de/{soundNode.Id}?target=mp3";
            UploadSound(hostUrl, de, "mp3", SoundsFolder);
        }
        
        protected IrrVerb GetIrregularVerb(ITranslationItem trItem, string de, string part2Verb)
        {
            var word = new IrrVerb
            {
                De = de,
                Part2Verb = part2Verb
            };

            var attributes = trItem.GetAttributes();
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.Info = attributes.Info;
            word.DeTranscription = attributes.Phonetics;
            word.VerbClass = attributes.VerbClass;

            return word;
        }

        //private string GetTranscription(string language, string word)// lang = "deu"
        //{
        //    var hostUrl = "https://www.artlebedev.ru/tools/transcriptor/ajax.html";
        //    var values = new Dictionary<string, string>
        //    {
        //        { "lang", language },
        //        { "text", word }
        //    };

        //    var content = new FormUrlEncodedContent(values);
        //    HttpClient client = new HttpClient();
        //    var response = client.PostAsync(hostUrl, content).Result;

        //    var responseString = response.Content.ReadAsStringAsync().Result;

        //    TranscriptionResponse deptObj = JsonSerializer.Deserialize<TranscriptionResponse>(responseString, new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    });

        //    return deptObj.Text;
        //}

        protected Verb GetVerb(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = new Verb
            {
                De = de
            };

            var attributes = trItem.GetAttributes();
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics;
            word.Info = attributes.Info;
            word.VerbClass = attributes.VerbClass;
            if (!string.IsNullOrEmpty(attributes.HeadWord) && attributes.HeadWord.Contains("|") )
            {
                word.De = attributes.HeadWord;
            }
            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }
            return word;
        }

        private WordClass GetWordClass(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = new WordClass
            {
                De = de
            };

            var attributes = trItem.GetAttributes();
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics.RemoveNewLine();

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }
            return word;
        }

        private Substantiv GetSubstantiv(ITranslationItem trItem, string de, IEnumerable<ITranslationItem> allItems)
        {
            var word = new Substantiv
            {
                De = de
            };

            var attributes = trItem.GetAttributes();
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.GetWordClass();
            word.DeTranscription = attributes.Phonetics;
            word.Flexion = attributes.Flexion;
            word.Genus = attributes.Genus;
            word.Artikle = attributes.Artikle;

            if (string.IsNullOrEmpty(word.DeTranscription))
            {
                word.DeTranscription = trItem.GetTranscription(allItems);
            }
            return word;
        }

        protected string Normalize(string word)
        {
            word = word.ToLower();
            foreach (var symbol in NORMALIZATION)
            {
                if (word.Contains(symbol.Source))
                {
                    word = word.Replace(symbol.Source, symbol.Dest);
                }
            }
            return word;
        }
    }

    public enum WordType
    {
        First = 0,
        Verb,
        Subst,
        Adv, 
        Adj,
        Konj,
        All
    }
}