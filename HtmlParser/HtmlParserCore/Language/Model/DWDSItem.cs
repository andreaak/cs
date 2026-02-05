using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HtmlParser.Language
{
    public class DWDSItem
    {
        public string De { get; set; }
        public string Index { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
        public string OtherData { get; set; }
        public WordType Type { get; set; }
        public IList<DWDSItem> InnenItems { get; set; }

        public virtual void Write(StreamWriter sw)
        {
            sw.WriteLine(InnenItems.Any() ? InnenItems[0].Type.ToString().ToLowerInvariant() : "");
            sw.WriteLine("");
            sw.WriteLine(De);
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("");

            var example = GetDescription();
            sw.WriteLine(example);
            sw.WriteLine("");
            sw.WriteLine("");
        }

        public string GetDescription()
        {
            IList<string> res = new List<string>();

            foreach (var itm in InnenItems)
            {
                res.Add(GetDescription1(itm));
            }

            return ((string.IsNullOrEmpty(OtherData) ? "" : $"/{OtherData}/ ## ") + string.Join(" ## ", res)).Trim();
        }

        private string GetDescription1(DWDSItem item)
        {
            IList<string> res = new List<string>();

            var st = $"{item.Index} {item.Definition} {GetExample(item.Example)}".Replace("  ", " ").Trim();

            foreach (var itm in item.InnenItems)
            {
                res.Add($"{itm.Index} {itm.Definition} {GetExample(itm.Example)}".Replace("  ", " ").Trim());
            }

            return st + (res.Any() ? " # " + string.Join(" # ", res) : "" );
        }

        private string GetExample(string example)
        {
            return string.IsNullOrEmpty(example) ? string.Empty : "[Ex]: " + example;
        }
    }

}