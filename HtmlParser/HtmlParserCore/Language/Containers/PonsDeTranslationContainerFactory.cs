using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.Containers
{
    public class PonsDeTranslationContainerFactory : BasePonsTranslationContainerFactory
    {
        protected override string HostUrl => "https://de.pons.com/%C3%BCbersetzung/deutsch-russisch";
        protected override string HostUrl2 => "https://de.pons.com/text-%C3%BCbersetzung/deutsch-russisch?q=";
        protected string HostUrlText => "https://de.pons.com/text-%C3%BCbersetzung/deutsch-russisch?q=";

        public PonsDeTranslationContainerFactory(string de, WordType type)
            : base(de, type)
        { }

        public IList<WordClass> GetWords()
        {
            var trContainers = GetTranslationContainer();

            if (trContainers == null || trContainers.Count == 0)
            {
                Console.WriteLine($"Not found {_word}");
                var wordsNotFound = new List<WordClass>()
                {
                    new WordClass
                    {
                        De = _word
                    }
                };
                return wordsNotFound;
            }

            _containers = trContainers;
            return GetWords(trContainers, _word, dei => new WordClass
            {
                De = _word
            });
        }

        public string GetTranslation()
        {
            var rr = HttpUtility.UrlEncode(_word);

            var document = new HtmlParser().GetHtml(HostUrlText + rr);
            if (document == null)
            {
                return null;
            }
            var res = document.DocumentNode?.SelectSingleNode(".//div[@data-e2e='input-slot-target']")?.InnerText.PonsNormalize();
            return res;
        }

        public void UploadSound(string reserveLink = null)
        {
            var trNode = (_containers ?? GetTranslationContainer())?.FirstOrDefault()?.Node;
            var soundId = trNode?.SoundId;

            string hostUrl = GetRequestUrl(soundId, $"https://sounds.pons.com/audio_tts/de/{soundId}?target=mp3", reserveLink);
            if (string.IsNullOrEmpty(hostUrl))
            {
                return;
            }
            hostUrl.UploadSound(_word.Replace("|", ""), "mp3", Constants.SoundsFolder);
        }

        public IrrVerb GetIrregularVerb(string part2Verb)
        {
            var word = new IrrVerb
            {
                De = _word,
                Part2Verb = part2Verb
            };

            var container = (_containers ?? GetTranslationContainer())?.FirstOrDefault();
            var trItem = container?.Node ?? container?.AllNodes.First();
            var attributes = trItem?.Attributes;
            if (attributes == null)
            {
                return word;
            }

            word.WrdClass = trItem.Attributes.WordClass;
            word.Info = attributes.Info;
            word.DeTranscription = attributes.Phonetics;
            word.VerbClass = attributes.VerbClass;

            return word;
        }

        protected override WordType GetWordType(string wordClass)
        {
            return wordClass.GetDeType();
        }
    }
}