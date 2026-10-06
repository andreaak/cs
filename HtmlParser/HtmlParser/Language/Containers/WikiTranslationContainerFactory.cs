using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;

namespace HtmlParser.Language.Containers
{
    public class WikiTranslationContainerFactory
    {
        string hostUrl = "https://de.wiktionary.org/wiki/";

        public const string Cookie =
            "GeoIP=DE:BW:Heilbronn:49.13:9.23:v4; dewiktionarymwuser-sessionId=5a2ce9f7faf282c0bb73; WMF-Last-Access=01-Oct-2026; WMF-Last-Access-Global=01-Oct-2026; NetworkProbeLimit=0.001; WMF-Uniq=WE7nXshIZH66b4uzjMa-DgOfAAwDAFvdjNlpvBVnXix4O1yUFfP3JzoTFTvKiC-S";

        string url = "https://de.wiktionary.org/wiki/aus%C3%BCben";

        string cookie = "GeoIP=DE:BW:Heilbronn:49.13:9.23:v4; dewiktionarymwuser-sessionId=5a2ce9f7faf282c0bb73; WMF-Last-Access=01-Oct-2026; WMF-Last-Access-Global=01-Oct-2026; NetworkProbeLimit=0.001; WMF-Uniq=WE7nXshIZH66b4uzjMa-DgOfAAwDAFvdjNlpvBVnXix4O1yUFfP3JzoTFTvKiC-S";


        private string _word;
        private WordType _wordClass;
        private HtmlDocument _document;
        private AIProvider _ai;

        public WikiTranslationContainerFactory(string word, WordType wordClass)
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

                var value = document?.DocumentNode.SelectSingleNode(".//div[@id='mw-content-text']//section[@data-mw-section-id=2]");
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

                var type = value.SelectSingleNode(".//div[1]/h3");
                if (type == null)
                {
                    Console.WriteLine("Error " + _word);
                    return null;
                }
                
                itm.Type = ParseWordType(type);



                var dl = value.SelectSingleNode(".//p[normalize-space()='Bedeutungen:']/following::dl[1]");
                itm.Bedeutungen = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Aussprache:']/following::dl[1]//span[@class='ipa']");
                itm.Betonung = dl.InnerText;

                dl = value.SelectSingleNode(".//p[normalize-space()='Synonyme:']/following::dl[1]");
                itm.Synonyms = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Sinnverwandte Wörter:']/following::dl[1]");
                itm.Sinnverwandte = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Gegenwörter:']/following::dl[1]");
                itm.Antonyms = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Oberbegriffe:']/following::dl[1]");
                itm.UbergeordneteBegriffe = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Unterbegriffe:']/following::dl[1]");
                itm.UntergeordneteBegriffe = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Beispiele:']/following::dl[1]");
                itm.Examples = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Redewendungen:']/following::dl[1]");
                itm.Redewendungen = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Sprichwörter:']/following::dl[1]");
                itm.WortCombinations = ParseList(dl, true);

                dl = value.SelectSingleNode(".//p[normalize-space()='Wortbildungen:']/following::dl[1]");
                itm.WordsBuilding = ParseList(dl, true);

                value = document?.DocumentNode.SelectSingleNode(".//div[@id='mw-content-text']//section[@data-mw-section-id=3]");
                if (value == null)
                {
                    return itm;
                }

                var nodes = value.SelectNodes(".//div[@class='printNav']");
                itm.Translation = ParseList(nodes, false);

                //var description = value.SelectSingleNode(".//h2[@id='einfach']/following::p[1]")?.InnerText.Trim()
                //    .Replace("&bdquo;", "").Replace("&ldquo;", "").Replace("\n", "##");
                //itm.Description = ParseItem(description);

                //var count = value.SelectNodes(".//h2[@id='haeufigkeit']/following::table[1]//td[@class='tdfill']")?.Count ?? 0;
                //itm.Frequency = count;

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

        private IList<SimpleItem> ParseList(HtmlNode dlNode, bool translate = false)
        {
            var list = new List<SimpleItem>();
            if (dlNode == null)
            {
                return list;
            }
            foreach (var node in dlNode.SelectNodes("./dd"))
            {

                string txt = node.InnerText.PonsNormalize().Trim();
                if (txt.StartsWith("[") && txt.Contains("] "))
                {
                    var items = txt.Split(new[] { "[" }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(i => !i.Trim().EndsWith("]"))
                        .Select(i => "[" + i)
                        .ToArray();


                    foreach (var item in items)
                    {
                        var ids = new List<string>();
                        var temp = item.Split(new[] { "]"}, StringSplitOptions.RemoveEmptyEntries);

                        string idText = temp[0].Replace("[", "");

                        if (idText.Contains("+") || idText.Contains("-") || idText.Contains(", "))
                        {
                            var tmpIds = idText.Replace(" ", "").Split(new[] { "+", "-", "," }, StringSplitOptions.None);
                            ids.AddRange(tmpIds);
                        }
                        else
                        {
                            ids.Add(idText);
                        }

                        string value = temp.Skip(1).FirstOrDefault()?.PonsNormalize();
                        var ru = translate ? _ai.GetTranslation(value) : "";

                        foreach (var id in ids)
                        {
                            list.Add(new SimpleItem
                            {
                                Id = id.Trim(),
                                FLanguage = value,
                                Ru = ru
                            });
                        }
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


        private IList<SimpleItem> ParseList(IList<HtmlNode> nodes, bool translate = false)
        {
            var list = new List<SimpleItem>();
            if (nodes == null)
            {
                return list;
            }
            foreach (var node in nodes)
            {
                var idText = node.SelectSingleNode(".//div[@class='NavHead uetabelle__head']").InnerText?.Trim();
                var id = idText?.Split(new[] { "]" }, 2, StringSplitOptions.None)?[0].Replace("[", "");


                var spans = node.SelectNodes(".//table[1]//ul/li//span[@lang='en']");
                if (spans == null)
                {
                    continue;
                }
                
                string tmp = string.Join(", ", spans.Select(s => s.InnerText?.Trim().PonsNormalize()));

                list.Add(new SimpleItem
                {
                    Id = id,
                    FLanguage = tmp,
                });
            }
            
            return list;
        }

        private HtmlDocument GetDocument()
        {

            return new HtmlReader().GetHtml(GetUrl(_word), Cookie, isRepeat: false);
        }

        private string GetUrl(string de)
        {
            return $"{hostUrl}{Uri.EscapeDataString(de)}";


            //de = de.Replace("ü", "%c3%bc")
            //    .Replace("ö", "%c3%b6")
            //    .Replace("ä", "%c3%a4")
            //    .Replace("ß", "%c3%9f");

            //return $"{hostUrl}{de}/";
        }
    }



}