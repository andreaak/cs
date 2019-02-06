using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Assist
{
    public class Options
    {
        static Options instance;
        static char separator = '|';

        #region PROPERTIES
        private bool highlightText;
        public bool HighlightText
        {
            get { return highlightText; }
            set { highlightText = value; }         
        }

        private bool setTextInControl;
        public bool SetTextInControl
        {
            get { return setTextInControl; }
            set { setTextInControl = value; }             
        }

        private bool setLogger;
        public bool SetLogger
        {
            get { return setLogger; }
            set { setLogger = value; }        
        }

        private string loggerFilePath;
        public string LoggerFilePath
        {
            get { return loggerFilePath; }
            set { loggerFilePath = value; }          
        }

        private int subRowCount = 2;
        public int SubRowCount
        {
            get { return subRowCount; }
            set { subRowCount = value; }              
        }

        private List<String> skipInDebug;
        public List<String> SkipInDebug
        {
            get { return skipInDebug; }
            set { skipInDebug = value; }         
        }

        private string guidR;
        public string GuidR
        {
            get { return guidR; }
            set { guidR = value; }          
        }

        private string guidL;
        public string GuidL
        {
            get { return guidL; }
            set { guidL = value; }  
        }

        string fontType;
        public string FontType
        {
            get { return fontType; }
            set { fontType = value; }
        }
        
        FontStyle fontStyle;
        public FontStyle FontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; }
        }
        
        float fontSize;
        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        
        Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        
        string fontType2;
        public string FontType2
        {
            get { return fontType2; }
            set { fontType2 = value; }
        }
        
        FontStyle fontStyle2;
        public FontStyle FontStyle2
        {
            get { return fontStyle2; }
            set { fontStyle2 = value; }
        }
        
        float fontSize2;
        public float FontSize2
        {
            get { return fontSize2; }
            set { fontSize2 = value; }
        }
        
        Color color2;
        public Color Color2
        {
            get { return color2; }
            set { color2 = value; }
        }

        private List<string> guids;
        public List<string> Guids
        {
            get { return guids; }
        }

        #endregion

        public static Options GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Options();
                }
                return instance;
            }
        }

        private Options()
        {
            ReadDataFromReg();
        }

        #region REGISTRY_METHODS

        public void ReReadDataFromReg()
        {
            ReadDataFromReg();
        }

        private void ReadDataFromReg()
        {
            Dictionary<string, object> values;
            if (WorkWithRegistry.ReadFromReg(out values) || values == null)
            {
                GetOptions(values);
                GetGUIDs(values);
                GetFontsData(values);
            }

            Dictionary<string, object> temp;
            guids = WorkWithRegistry.ReadGuidFromReg(out temp);
        }

        private void GetOptions(Dictionary<string, object> values)
        {
            string key = "highlightText";
            if (values.ContainsKey(key))
            {
                bool.TryParse(values[key].ToString(), out highlightText);
            }

            key = "setTextInControl";
            if (values.ContainsKey(key))
            {
                bool.TryParse(values[key].ToString(), out setTextInControl);
            }

            key = "subRowCount";
            if (values.ContainsKey(key))
            {
                int.TryParse(values[key].ToString(), out subRowCount);
            }            
            
            key = "setLogger";
            if (values.ContainsKey(key))
            {
                bool.TryParse(values[key].ToString(), out setLogger);
            }

            key = "loggerFilePath";
            if (values.ContainsKey(key))
            {
                loggerFilePath = values[key].ToString();
            }

            key = "skipInDebug";
            if (values.ContainsKey(key))
            {
                skipInDebug = UTILS.GetSepDataFromString(values[key].ToString(), separator);
            }
            else
            {
                skipInDebug = new List<string>();
            }
        }

        private void GetGUIDs(Dictionary<string, object> values)
        {

            string key = "guidR";
            if (values.ContainsKey(key))
            {
                guidR = values[key].ToString();
            }

            key = "guidL";
            if (values.ContainsKey(key))
            {
                guidL = values[key].ToString();
            }
        }

        private void GetFontsData(Dictionary<string, object> values)
        {
            string key = "fontType";
            if (values.ContainsKey(key))
            {
                fontType = values[key].ToString();
            }

            key = "fontSize";
            if (values.ContainsKey(key))
            {
                float.TryParse(values[key].ToString(), out fontSize);
            }

            key = "fontStyle";
            if (values.ContainsKey(key))
            {
                fontStyle = (FontStyle)Enum.Parse(fontStyle.GetType(), values[key].ToString(), true);
            }

            key = "color";
            if (values.ContainsKey(key))
            {
                color = UTILS.GetColor(values, key);
            }

            key = "fontType2";
            if (values.ContainsKey(key))
            {
                fontType2 = values[key].ToString();
            }

            key = "fontSize2";
            if (values.ContainsKey(key))
            {
                float.TryParse(values[key].ToString(), out fontSize2);
            }

            key = "fontStyle2";
            if (values.ContainsKey(key))
            {
                fontStyle2 = (FontStyle)Enum.Parse(fontStyle2.GetType(), values[key].ToString(), true);
            }

            key = "color2";
            if (values.ContainsKey(key))
            {
                color2 = UTILS.GetColor(values, key);
            }
        }

        public void SaveDataToReg()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("highlightText", highlightText);
            values.Add("setTextInControl", setTextInControl);
            values.Add("subRowCount", subRowCount);

            values.Add("setLogger", setLogger);
            values.Add("loggerFilePath", loggerFilePath);
            values.Add("guidR", guidR);
            values.Add("guidL", guidL);

            values.Add("skipInDebug", string.Join(separator.ToString(), skipInDebug.ToArray()));

            values.Add("fontType", fontType);
            values.Add("fontSize", fontSize);
            values.Add("fontStyle", fontStyle.ToString());
            values.Add("color", UTILS.GetRGBA(color));

            values.Add("fontType2", fontType2);
            values.Add("fontSize2", fontSize2);
            values.Add("fontStyle2", fontStyle2.ToString());
            values.Add("color2", UTILS.GetRGBA(color2));
            
            // Обновить регистр новой настройкой 
            WorkWithRegistry.WriteToReg(values);
        }

        #endregion
    }
}
