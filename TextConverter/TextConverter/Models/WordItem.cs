using System.Collections.Generic;

namespace TextConverter.Models
{
    public class WordItem
    {
        public const string Us = "us";
        public const string Uk = "uk";

        public const string Ru = "ru";
        public const string En = "en";
        public const string EnTr = "en_tr";
        public const string De = "de";
        public const string RankTag = "rank";

        public static readonly string[] Items = { Ru, En, EnTr, De };

        public const string TranscriptionSuffix = "_tr";

        public int Rank { get; set; } = int.MaxValue;

        public string RankFile
        {
            get
            {
                if(Rank < int.MaxValue)
                {
                    return Rank.ToString();
                }
                return null;
            }
            set
            {
                Rank = string.IsNullOrEmpty(value) ? int.MaxValue : int.Parse(value); 
            }
        }

    Dictionary<string, string> words = new Dictionary<string, string>();
        Dictionary<string, string> trans = new Dictionary<string, string>();

        public void AddItem(string lang, string value)
        {
            if (lang.EndsWith(TranscriptionSuffix))
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
