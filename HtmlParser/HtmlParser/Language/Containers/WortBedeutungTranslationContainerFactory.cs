using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;

namespace HtmlParser.Language.Containers
{
    public class WortBedeutungTranslationContainerFactory
    {
        //string hostUrl = "https://www.wortbedeutung.info/";
        string hostUrl = "http://www.wortbedeutung.info/";

        public const string Cookie =
            "FCCDCF=%5Bnull%2Cnull%2Cnull%2C%5B%22CQrM50AQrM50AEsACBDECyFoAP_gAEPgAAYgMGoB7C7cbCFCCDJ3ILsEEABHQJAAYsAwBAIAAgABDBIQIBwCgEEaBASIFCACCAAAIAABAAAkGAAAAUAAIAABAABEAAwAIBAIIAAAgAEAAAIAAAAoCIgAEQCAAAAEBEAAkAgAAAIAWEAAAAAAAACBAAAAAAAAAAAAAAEAAAMAAQAAwAAAgAgAgAAAAAAAAAAIAAAAAAAAAAAAAAAQMAAAAAAAAAAAAAAAABAAAAAAAQgAAAAAAAAAAAAAAAAAAAAAAAAAIMGoB7C7cbCFCCDBXILsEEABXQJAAYsAwBAIAAgABDBIQIAwCkEESBACIECAACAAAIAABAAAoEAgAAEAAAAABAABEAAwAIBAIAAAAgAEAQAAAAAAICIgAEQCAAAAEBEAAgQgAAAIAWEAAAAAAAACBAAAAAAAAAAAAAAEAAAMAACAAwAAAgAgAgAAAAAAAAAAIEAAAAAAAAAAAAAAQMAAAAAAAAAAAAAAAAAAAAAAAAAgAAEAAAAAAAAAAAAAAAABAAAAAIAA.IMGoB7C7cbCFCCDJ3ILsEEABXQJAAYsAwBAIAAgABDBIQIBwCkEEaBASIFCACCAAAIAABAAAsGAgAAUAAIAABAABEAAwAIBAIIAAAgAEAQAIAAAAoCIgAEQCAAAAEBEAAkQgAAAIAWEAAAAAAAACBAAAAAAAAAAAAAAEAAAMAASAAwAAAgAgAgAAAAAAAAAAIEAAAAAAAAAAAAAAQMAAAAAAAAAAAAAAAABAAAAAAAQgAAEAAAAAAAAAAAAAAAABAAAAAIA%22%2C%222~61.70.89.122.144.161.196.230.385.442.445.494.495.550.576.827.1025.1029.1033.1047.1097.1126.1171.1301.1342.1725.1942.2068.2074.2109.2223.2224.2416.2567.2568.2575.2577.2657.2699.2778.2813.2822.2869.2878.2920.2963.3005.3023.3126.3253.5231.13731.15731.16831.33931~dv.%22%2C%22DA9C5CCA-9768-4606-9CE4-648F39F21B97%22%5D%2Cnull%2Cnull%2C%5B%5B32%2C%22%5B%5C%22144d7db3-b47e-4cd6-9d4a-2812efa6f5c6%5C%22%2C%5B1790508634%2C423000000%5D%5D%22%5D%5D%5D; verify=j; WortbedeutungRec=x; WortbedeutungBan=x; cf_clearance=W1z9EP2RCt0o4xnj4RQKpd.vn1aJpW_67PlSaspWjZE-1790762840-1.2.1.1-rnEHn.mawX7uf8mU2UgKmHq2PciCbPcXULdO8L32Tj5gsM4rLoSnB6J.th9fiw7pPVRSMb3hkol6qJVhX4DVV7KB7Th1JBWP41X124koI_WD3PIablWAZMd4lvLSg3YuBFhvnJPEVxUxH4d_sV_E3xwnZYmk53fcYqXCBmWlSN.xohhvdhIwYNhn.yclolaoKSOJ8bhJlyJVloK1e._ruGlgvoLaqDM8FUsK3lDwMGDzFJVhDtJOed8XXY4IYz31OicOhsj2.HmRy77gkFQ4X5bYtJgN.RQegohYJwdNlWZNHf615aymCll0MrAkSVR1Kw__haUAsWJCjpiMR2zhPS3B0zcmrdgjpzweROaib0n2AGwXi3J0CWBbzvCAFLN.iRrzzoSkrM38lh5NA40emboeGEGc_ndg78qlenwLuW2qrLPuc_v84JPPnxGrX03r5aE3n1NbueFWhbwHKs4KZvAvrkNU6ZU7RJ0rbUChdzraJoXdbcrDm4OPAD44NXiZ; __gads=ID=0a0a1929464434cb:T=1790622153:RT=1790762841:S=ALNI_MZBCEtIS3SqROFg-hdNZdf_Ne80Qw; __eoi=ID=efa82f130ca2fd47:T=1790622153:RT=1790762841:S=AA-Afja3dJtjg11K11oxH_ldXqNe; FCNEC=%5B%5B%22AKsRol9d39R2OYnxclPxcFn8PJJ7p4InfFZmqtfuKagnMuDuhtNUKvhUBmnGMSaeJIg_ZLj5s--4AvmrgFA67L0MPnLISjt_i5npqoL41nlvvkP5VcPreuHOBevi17CN8lfHmk6OUPvbvphXuykg3Yl--9arVNDrMA%3D%3D%22%5D%5D";


        private string _word;
        private WordType _wordClass;
        private HtmlDocument _document;
        private AIProvider _ai;

        public WortBedeutungTranslationContainerFactory(string word, WordType wordClass)
        {
            _word = word.Replace("|", "");
            _wordClass = wordClass;
            _ai = new AIProvider();
        }

        public WortBedeutungItem GetDe()
        {
            try
            {
                var document = GetDocument();

                var value = document?.DocumentNode.SelectSingleNode(".//div[@id='spaltelinks']");
                if (value == null)
                {
                    return null;
                }

                if (value.InnerText.Contains("fung hilft uns, Spam und Missbrauch zu verhindern."))
                {
                    Console.WriteLine("Not found " + _word);
                    return null;
                }

                if (value.InnerText.Contains("404 - Seite nicht gefunden"))
                {
                    Console.WriteLine("Not found " + _word);
                    return null;
                }

                WortBedeutungItem itm = new WortBedeutungItem();

                itm.De = _word;

                var type = value.SelectSingleNode(".//h3[contains(normalize-space(.), 'Wortart')]");
                if (type == null)
                {
                    Console.WriteLine("Error " + _word);
                    return null;
                }
                
                itm.Type = ParseWordType(type);



                var dl = value.SelectSingleNode(".//h4[normalize-space()='Bedeutung/Definition']/following::dl[1]");
                itm.Bedeutungen = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Aussprache/Betonung']/following::dl[1]");
                itm.Betonung = ParseList(dl).FirstOrDefault()?.FLanguage;

                dl = value.SelectSingleNode(".//h4[normalize-space()='Synonyme']/following::dl[1]");
                itm.Synonyms = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Gegensatzwörter']/following::dl[1]");
                itm.Antonyms = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Übergeordnete Begriffe']/following::dl[1]");
                itm.UbergeordneteBegriffe = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Untergeordnete Begriffe']/following::dl[1]");
                itm.UntergeordneteBegriffe = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Beispielsätze']/following::dl[1]");
                itm.Examples = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Redensart/Redewendungen']/following::dl[1]");
                itm.Redewendungen = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Sprichwörter']/following::dl[1]");
                itm.WortCombinations = ParseList(dl, true);

                dl = value.SelectSingleNode(".//h4[normalize-space()='Abgeleitete Wörter/Wortbildungen']/following::dl[1]");
                itm.WordsBuilding = ParseList(dl, true);

                value = document?.DocumentNode.SelectSingleNode(".//div[@id='spalterechts']");
                if (value == null)
                {
                    return itm;
                }

                var description = value.SelectSingleNode(".//h2[@id='einfach']/following::p[1]")?.InnerText.Trim()
                    .Replace("&bdquo;", "").Replace("&ldquo;", "").Replace("\n", "##");
                itm.Description = ParseItem(description);

                var count = value.SelectNodes(".//h2[@id='haeufigkeit']/following::table[1]//td[@class='tdfill']")?.Count ?? 0;
                itm.Frequency = count;

                return itm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }

        private WordType ParseWordType(HtmlNode node)
        {

            var types = node.InnerText.Split(new []{"Wortart:"}, StringSplitOptions.RemoveEmptyEntries);

            WordType wt = WordType.None;
            
            foreach (var type in types)
            {
                var wortart = type.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0].Trim().ToLower();
                switch (wortart)
                {
                    case "verb":
                        wt = wt.Set(WordType.Verb);
                        break;
                    case "adjektiv":
                        wt = wt.Set(WordType.Adj);
                        break;
                    case "indefinitpronomen":
                        wt = wt.Set(WordType.Pron);
                        break;
                    case "substantiv":
                        wt = wt.Set(WordType.Subst);
                        break;

                }
            }

            return wt;
        }

        private SimpleItem ParseItem(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return null;
            }

            
            return new SimpleItem
            {
                FLanguage = item,
                Ru = _ai.GetTranslation(item)
            };
        }

        private IList<SimpleItem> ParseList(HtmlNode dlNode, bool translate = false)
        {
            var list = new List<SimpleItem>();
            if (dlNode == null)
            {
                return list;
            }
            foreach (var node in dlNode.SelectNodes("./dd"))
            {

                string txt = node.InnerText.Trim();
                if (txt.Contains(") "))
                {
                    var tmp = txt.Split(new[] { ") " }, 2, StringSplitOptions.None);

                    var ids = new List<string>();
                    if (tmp[0].Contains("+") || tmp[0].Contains("-"))
                    {
                        var tmpIds = tmp[0].Replace(" ", "").Split(new[] { "+" , "-"}, StringSplitOptions.None);
                        ids.AddRange(tmpIds);
                    }
                    else
                    {
                        ids.Add(tmp[0]);
                    }

                    var ru = translate ? _ai.GetTranslation(tmp[1]) : "";

                    foreach (var id in ids)
                    {
                        list.Add(new SimpleItem
                        {
                            Id = id.Trim(),
                            FLanguage = tmp[1],
                            Ru = ru
                        });
                    }

                }
                else
                {
                    list.Add(new SimpleItem
                    {
                        FLanguage = txt,
                        Ru = translate ? _ai.GetTranslation(txt) : ""
                    });
                }
            }
            return list;
        }

        private HtmlDocument GetDocument()
        {

            return new HtmlReader().GetHtml(GetUrl(_word), Cookie, isRepeat: false);
        }

        private string GetUrl(string de)
        {
            return $"{hostUrl}{Uri.EscapeDataString(de)}/";


            //de = de.Replace("ü", "%c3%bc")
            //    .Replace("ö", "%c3%b6")
            //    .Replace("ä", "%c3%a4")
            //    .Replace("ß", "%c3%9f");

            //return $"{hostUrl}{de}/";
        }
    }

    public class WortBedeutungItem
    {
        public string De { get; set; }
        public IList<SimpleItem> Bedeutungen { get; set; }
        public string Betonung { get; set; }
        public WordType Type { get; set; }

        public IList<SimpleItem> Synonyms { get; set; }
        public IList<SimpleItem> Sinnverwandte { get; set; }
        public IList<SimpleItem> Antonyms { get; set; }
        public IList<SimpleItem> UbergeordneteBegriffe { get; set; }
        public IList<SimpleItem> UntergeordneteBegriffe { get; set; }
        public IList<SimpleItem> Examples { get; set; }
        public IList<SimpleItem> Redewendungen { get; set; }
        public IList<SimpleItem> WortCombinations { get; set; }
        public IList<SimpleItem> WordsBuilding { get; set; }
        public IList<SimpleItem> Translation { get; set; }
        public SimpleItem Description { get; set; }
        public int Frequency { get; set; }

        public string GetDescription()
        {
            return De + "  " + Type.GetDEStringType()
                   + "##" + Betonung 
                   + GetDescription("Bedeutungen", Bedeutungen) 
                   + GetDescription("Synonyms", Synonyms) 
                   + GetDescription("Sinnverwandte", Sinnverwandte) 
                   + GetDescription("Antonyms", Antonyms) 
                   + GetDescription("Übergeordnete Begriffe", UbergeordneteBegriffe) 
                   + GetDescription("Untergeordnete Begriffe", UntergeordneteBegriffe) 
                   + GetDescription("Examples", Examples) 
                   + GetDescription("Redewendungen", Redewendungen) 
                   + GetDescription("WortCombinations", WortCombinations) 
                   + GetDescription("WordsBuilding", WortCombinations)
                   + GetDescription("Description", Description)
                   + GetDescription("Translation", Translation)
                   //+ "####" + $"Frequency: {Frequency}"
                ;
        }

        private string GetDescription(string thema, IList<SimpleItem> items)
        {
            return (items == null || items.Count == 0) ? "" : $"####{thema}:####{string.Join("##", items.Select(item => item.GetDescription()))}";
        }

        private string GetDescription(string thema, SimpleItem item)
        {
            return item != null ? $"####{thema}:####{item.GetDescription()}" : "";
        }
    }

    public class SimpleItem
    {
        public string Id { get; set; }
        public string FLanguage { get; set; }
        public string Ru { get; set; }

        public string GetDescription()
        {
            return !string.IsNullOrEmpty(Id) ? $"{Id}) {FLanguage}##{Ru}" : $"{FLanguage}##{Ru}";
        }
    }

}