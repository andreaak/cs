using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HtmlParser.Language.Extensions;

namespace HtmlParser.Language.Containers
{
    public class DicLeoContainerFactory
    {
        protected string HostUrl => "https://dict.leo.org/russisch-deutsch/";
        private string[] prepositions = { "an", "auf", "aus", "von", "über", "für", "um", "gegen", "in", "vor", "bei", "mit", "nach", "zu", "als", "bis" };

        protected readonly string _word;
        protected readonly WordType _type;
        protected IList<DicLeoItem> _containers;

        public DicLeoContainerFactory(string de, WordType type)
        {
            _word = de;
            _type = type;
        }

        public IList<DicLeoItem> GetWords(string separator = "/")
        {
            var trContainers = GetTranslationContainer();

            if (trContainers == null)
            {
                Console.WriteLine($"Not found {_word}");
                return Array.Empty<DicLeoItem>();
            }

            if (trContainers.Count == 0)
            {
                Console.WriteLine($"Prepositions not found {_word}");
                return Array.Empty<DicLeoItem>();
            }

            _containers = trContainers;
            return trContainers;
        }

        protected IList<DicLeoItem> GetTranslationContainer()
        {
            var document = new HtmlParser().GetHtml(HostUrl + _word);
            if (document == null)
            {
                return null;
            }


            var artikelNode = document.DocumentNode.SelectSingleNode(".//div[@data-dz-name='verb']//tbody");
            if (artikelNode == null)
            {
                return null;
            }


            var list = new List<DicLeoItem>();
            var rows = artikelNode.SelectNodes(".//tr");

            foreach (var row in rows)
            {
                var ru = row.SelectSingleNode(".//td[@lang='ru']");
                var de = row.SelectSingleNode(".//td[@lang='de']");

                var item = GetTranslationItem(ru, de);
                if (item != null)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        private DicLeoItem GetTranslationItem(HtmlNode ruNode, HtmlNode deNode)
        {
            var rem = ruNode.SelectNodes(".//span[@title='vollendeter Aspekt'] | .//span[@title='unvollendeter Aspekt']");
            if (rem != null)
            {
                foreach (var r in rem)
                {
                    r.Remove();
                }
            }

            ruNode.InnerHtml = ruNode.InnerHtml.Replace(" <br>", ",").Replace("<br>", ",");


            var ru = ruNode.InnerText.Trim().PonsNormalize();
            var de = deNode.InnerText.Trim().PonsNormalize().Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            
            var deItems = de.Split(new []{" ", "(", ")"}, StringSplitOptions.RemoveEmptyEntries);

            if (prepositions.Any(i => deItems.Any(d => d == i)))
            {
                var isSich = de.Contains("sich");
                if (isSich)
                {
                    de = de.Replace("sich", " ");
                }

                de = de.Replace("Dat.", " Dat.")
                    .Replace("Akk.", " Akk.")
                    .Replace("Nom.", " Nom.")
                    .PonsNormalize();

                return new DicLeoItem
                {
                    De = _word,
                    Ru = ru,
                    Praposition = de,
                    IsSich = isSich
                }; 
            }

            return null;
        }


    }
}