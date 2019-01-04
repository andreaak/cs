using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using TextConverter.Models;
using static TextConverter.Models.WordItem;

namespace TextConverter
{
    public class XMLHelper
    {
        public static IList<WordItem> ReadWords(string path)
        {
            var xml = XDocument.Load(path);
            var res = xml.Root.Descendants("word").Select(CreateItem).ToArray();
            return res;
        }

        private static WordItem CreateItem(XElement element)
        {
            var item = new WordItem();
            foreach (var name in Items)
            {
                var value = element.Element(name)?.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    item.AddItem(name, value);
                }
               
            }
            item.RankFile = element.Element(RankTag)?.Value;
            return item;
        }

        public static void WriteWords(string path, IList<WordItem> words)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("words");

                foreach (WordItem word in words)
                {
                    writer.WriteStartElement("word");
                    if(!string.IsNullOrEmpty(word.RankFile))
                    {
                        writer.WriteElementString(RankTag, word.RankFile);
                    }

                    foreach (var lang in Items)
                    {
                        string value = word.GetItem(lang);
                        if (!string.IsNullOrEmpty(value))
                        {
                            writer.WriteElementString(lang, value);
                        }
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static void WriteVerbs(string path, IList<EnVerbItem> words)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = new UTF8Encoding(false);

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("verbs");

                foreach (var word in words)
                {
                    writer.WriteStartElement("verb");

                    writer.WriteElementString("infinitive", word.Infinitive.Trim().ToLowerInvariant());
                    writer.WriteElementString("infinitive_tr", word.InfinitiveTranscription.Trim());

                    writer.WriteElementString("pastSimple", word.PastSimple.Trim().ToLowerInvariant());
                    writer.WriteElementString("pastSimple_tr", word.PastSimpleTranscription.Trim());

                    writer.WriteElementString("pastParticiple", word.PastPaticiple.Trim().ToLowerInvariant());
                    writer.WriteElementString("pastParticiple_tr", word.PastPaticipleTranscription.Trim());

                    writer.WriteElementString("translation", word.Translation.Trim().ToLowerInvariant());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
