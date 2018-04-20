using System.Collections.Generic;
using System.Text;
using System.Xml;
using TextConverter.Models;

namespace TextConverter
{
    public class XMLWriteHelper
    {
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

                    foreach (var lang in CSVHelper.Items)
                    {
                        string value = word.GetItem(lang);
                        if(!string.IsNullOrEmpty(value))
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

                    writer.WriteElementString("infinitive", word.Infinitive.Trim());
                    writer.WriteElementString("infinitive_tr", word.InfinitiveTranscription.Trim());

                    writer.WriteElementString("pastSimple", word.PastSimple.Trim());
                    writer.WriteElementString("pastSimple_tr", word.PastSimpleTranscription.Trim());

                    writer.WriteElementString("pastParticiple", word.PastPaticiple.Trim());
                    writer.WriteElementString("pastParticiple_tr", word.PastPaticipleTranscription.Trim());

                    writer.WriteElementString("translation", word.Translation.Trim());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
