using System.Collections.Generic;

namespace TextConverter
{
    public class WordItem
    {
        public const string TranscriptionSuffix = "_tr";

        Dictionary<string, string> words = new Dictionary<string, string>();
        Dictionary<string, string> trans = new Dictionary<string, string>();

        public void AddItem(string lang, string value)
        {
            if(lang.EndsWith(TranscriptionSuffix))
            {
                trans[lang.Replace(TranscriptionSuffix, "")] = value;
            }
            else
            {
                words[lang] = value;
            }
        }

        public string GetItem(string lang)
        {
            if (lang.EndsWith(TranscriptionSuffix))
            {
                string key = lang.Replace(TranscriptionSuffix, "");
                string value;
                return trans.TryGetValue(key, out value) ? value : "";
            }
            else
            {
                string value;
                return words.TryGetValue(lang, out value) ? value : "";
            }
        }
    }
}
