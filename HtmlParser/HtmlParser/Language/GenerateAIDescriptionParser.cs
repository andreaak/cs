using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HtmlParser.Language.Extensions;
using HtmlParser.Language.HTMLClients;
using HtmlParser.Language.Model;
using OpenAI.Audio;

namespace HtmlParser.Language
{
    public class GenerateAIDescriptionParser : LanguageParser, ILanguageParser
    {
        private bool break_;
        private string lang;


        public GenerateAIDescriptionParser(bool order, WordType type, string lang)
            : base(order, type)
        {
            this.lang = lang;
        }

        public void Parse(IList<string> lines)
        {

            string first = null;
            string second = null;

            StringBuilder sb = new StringBuilder();
            var words = new List<DeRuItem>();


            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (string.IsNullOrEmpty(first))
                    {
                        first = sb.ToString().Trim();
                        sb.Clear();
                    } 
                    else 
                    {
                        second = sb.ToString().Trim();
                        sb.Clear();

                        words.Add(new DeRuItem
                        {
                            De = first,
                            Ru = second
                        });
                        first = second = null;
                    }
                }
                else
                {
                    sb.AppendLine(line);
                }
            }

            var gpt = new GPT();
            string file = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            //gpt.GenerateSound(file, words, voice:GeneratedSpeechVoice.Verse);
        }
    }

    public class DeRuItem
    {
        public string De { get; set; }
        public string Ru { get; set; }
    }
}