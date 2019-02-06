using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{

    [Serializable]
    public class CycleItem : BaseItem
    {
        string condition;
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public CycleItem()
        {
        }

        public CycleItem(string allData)
        {
            string[] nsc = ParseItemName(allData, ref this.name);
            ParseNamespaceAndClass(nsc, ref this.namespace_, ref this.class_);
        }

        public CycleItem(string namespaceString, string class_, string name, string condition, bool static_, string language, BaseItem parent)
        : base(namespaceString, class_, name, DataType.cycle, static_, language, 0, 0, 0, "", parent)
        {
            this.condition = condition;
        }

        public CycleItem(string namespace_, string class_, string name, string condition, string language, BaseItem parent)
            : this(namespace_, class_, name, condition, false, language, parent)
        {
        }

        public override void SetParam(string param)
        {
            this.condition = param;
        }

        public override string GetParameters()
        {
            return condition;
        }

        #region DESCRIPTION

        public override string GetDescription()
        {
            return string.Format("{0}", condition);
        }

        #endregion

        public override int Width
        {
            get { return (int)(Settings_.Widthes[DataType.cycle] * ScaleFactor); }
        }

        public override int Height
        {
            get { return (int)(Settings_.Heights[DataType.cycle] * ScaleFactor); }
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
