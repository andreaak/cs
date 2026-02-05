using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HtmlParser.Language
{
    public class DicLeoContainerItem
    {
        public string De { get; set; }
        public bool IsSich { get; set; }
        public IList<DicLeoItem> Items { get; set; }

        public virtual void Write(StreamWriter sw)
        {
            sw.WriteLine("verb");
            sw.WriteLine("");
            sw.WriteLine(De);
            sw.WriteLine(IsSich ? "refl" : "");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine(GetDescription());
            sw.WriteLine("");
        }

        public string GetDescription()
        {
            IList<string> res = new List<string>();


            var grouped = Items.GroupBy(GetDeDescription).ToArray();

            foreach (var group in grouped)
            {
                res.Add(group.Key + " -- " + string.Join(" / ", group.Select(g => g.Ru)));
            }


            //foreach (var itm in Items)
            //{
            //    res.Add(GetDescription1(itm));
            //}

            return string.Join(" ## ", res).Trim();
        }

        //private string GetDescription1(DicLeoItem item)
        //{
        //    return ((item.IsSich ? "sich " : "") + $"{item.Praposition} -- {item.Ru}").Trim();
        //}

        private string GetDeDescription(DicLeoItem item)
        {
            return ((item.IsSich ? "sich " : "") + $"{item.Praposition}").Trim();
        }
    }


    public class DicLeoItem
    {
        public string De { get; set; }
        public string Ru { get; set; }
        public string Praposition { get; set; }
        public bool IsSich { get; set; }
    }
}