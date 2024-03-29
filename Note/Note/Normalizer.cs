﻿using Note.Domain;
using System.Linq;
using DevExpress.XtraRichEdit;

namespace Note
{
    public class Normalizer
    {
        protected readonly RichEditControl control = new RichEditControl();

        public  void Normalize(DatabaseManager dataManager)
        {
            var items = dataManager.GetTextData().Where(item => 
                                                        (string.IsNullOrEmpty(item.HtmlText) || string.IsNullOrEmpty(item.PlainText))
                                                        && !string.IsNullOrEmpty(item.RtfText));



            foreach (var item in items)
            {
                control.RtfText = item.RtfText;
                string plainText = control.Text;
                string htmlText = control.HtmlText;
                dataManager.UpdateTextData(item.ID, item.RtfText, plainText, htmlText);
            }
        }
    }
}
