using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.Model;

namespace HtmlParser.Language.Containers
{
    public class PonsEnTranslationContainerFactory : BasePonsTranslationContainerFactory
    {
        protected override string HostUrl => "https://en.pons.com/translate/english-russian";
        protected override string HostUrl2 => "https://en.pons.com/translate/english-russian?q=";

        public PonsEnTranslationContainerFactory(string de, WordType type)
            : base(de, type)
        { }

        protected override WordType GetWordType(string wordClass)
        {
            return wordClass.GetEnType();
        }

        public void UploadSound(string reserveLink = null)
        {
            var trNode = (_containers ?? GetTranslationContainer())?.FirstOrDefault()?.Node;
            var soundId = trNode?.SoundId;


            string hostUrl = GetRequestUrl(soundId, $"https://sounds.pons.com/audio_tts/en/{soundId}?target=mp3", reserveLink);
            if (string.IsNullOrEmpty(hostUrl))
            {
                return;
            }
            hostUrl.UploadSound(_word, "mp3", Constants.EnSoundsFolder, false);

            hostUrl = GetRequestUrl(soundId, $"https://sounds.pons.com/audio_tts/en_us/{soundId}?target=mp3", reserveLink);
            if (string.IsNullOrEmpty(hostUrl))
            {
                return;
            }
            hostUrl.UploadSound(_word, "mp3", Constants.UsSoundsFolder, false);
        }

        protected override IEnumerable<PonsItem> Parse(IList<HtmlNode> nodes)
        {
            return nodes.Select(i => EnPonsItem.ParseNode(i, _word)).Where(i => i != null && i.TranslationItems.Any(t => t.Values.Count != 0 || !string.IsNullOrEmpty(t.Sense))).ToArray();
        }

        protected override Substantiv GetSubstantivItem(string de)
        {
            var word = new Substantiv
            {
                De = de.ToLower()
            };
            return word;
        }

        //protected override string GetWordRu(string de, string separator, TranslationContainer trContainer)
        //{
        //    return string.Join(separator, trContainer.Node.GetTranslations(de).Distinct());
        //}
    }
}