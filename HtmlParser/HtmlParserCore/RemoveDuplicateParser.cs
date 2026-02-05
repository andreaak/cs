using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace HtmlParser
{


    public class RemoveDuplicateParser : LinkParser
    {
        public void Parse()
        {
            string file = @"D:\Projects\files.txt";

            string directory = @"D:\Projects\OFC\oc-core-m\";
            
            var lines = File.ReadLines(file);

            var list = lines.Where(l => !string.IsNullOrEmpty(l)).ToArray();

            var files = new HashSet<string>(list).ToList().OrderBy(t => t);

            using (var sw = File.CreateText(@"D:\Projects\files_out.txt"))
            {
                foreach (var item in files.Where(f => File.Exists(directory + f)))
                {
                    sw.WriteLine(item);
                }
            }

            using (var sw = File.CreateText(@"D:\Projects\files_notexist_out.txt"))
            {
                foreach (var item in files.Where(f => !File.Exists(directory + f)))
                {
                    sw.WriteLine(item);
                }
            }
        }


        protected override string GetHost()
        {
            return "https://www.translate.ru";
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            throw new NotImplementedException();
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            throw new NotImplementedException();
        }

        protected override string GetUrI(string dir, string url)
        {
            return url.Contains("http") ? url : GetHost() + url;
        }

    }
}