using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    [Serializable]
    public class PropertyItem : BaseItem
    {
        public PropertyItem()
        {
        }

        public PropertyItem(string allData)
        {
            string[] nsc = ParseItemName(allData, ref name);
            ParseNamespaceAndClass(nsc, ref this.namespace_, ref this.class_);
        }

        public PropertyItem(string namespaceString, string class_, string name, bool static_, string language, BaseItem parent)
            : base(namespaceString, class_, name, DataType.property, static_, language, 0, 0, 0, "", parent)
        {
        }

        public PropertyItem(string namespaceString, string class_, string name, bool static_, string language,
                        int startLine, int endLine, int currentLine, string fileName, BaseItem parent)
            : base(namespaceString, class_, name, DataType.property, static_, language, startLine, endLine, currentLine, fileName, parent)
        {
        }


        public override void SetParam(string param)
        {
            
        }


        #region DESCRIPTION

        public override string GetDescription()
        {
            return string.Format("{0}.{1}.{2}", NamespaceStr, class_, name);
        }

        #endregion

        public override int Width
        {
            get { return (int)(Settings_.Widthes[DataType.property] * ScaleFactor); }
        }

        public override int Height
        {
            get { return (int)(Settings_.Heights[DataType.property] * ScaleFactor); }
        }

        public override bool Drawable
        {
            get { return true; }
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}
