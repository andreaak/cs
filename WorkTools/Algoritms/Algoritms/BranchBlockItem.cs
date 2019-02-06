using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    [Serializable]
    public class BranchBlockItem : BaseItem
    {
        string condition;
        public string Condition
        {
            get { return condition; }
            set {
                if (Type_ == DataType.branchCondition)
                {
                    condition = value;
                }
            }
        }
        
        public BranchBlockItem()
        {
        }

        public BranchBlockItem(string condition, DataType itemType, int startLine, int endLine, int currentLine, BaseItem parent)
            : base(parent.NamespaceStr, parent.Class_, parent.Name, itemType, false, parent.Language, 
            startLine, endLine, currentLine, parent.FileName, parent)

        {
            if (itemType == DataType.branchCondition)
            {
                this.condition = condition;
            }
        }

        public BranchBlockItem(BranchBlockItem item)
            : this(item.condition, item.Type_, item.StartLine, item.EndLine, item.CurrentLine, item.Parent)
        {
            this.items = item.items;
        }

        public override string GetDescription()
        {
            if (Type_ == DataType.branchElse)
            {
                return "Else";
            }
            return condition;
        }

        public override void SetParam(string param)
        {
            condition = param;
        }

        public override string GetParameters()
        {
            return condition;
        }

        public string GetDescription(string param)
        {
            if (Type_ == DataType.branchElse)
            {
                return string.Concat("Else", condition);
            }
            else
            {
                return string.Concat("Condition", "--->", condition);
            }
        }

        public override int Width
        {
            get
            {
                if (Type_ == DataType.branchCondition)
                {
                    return Settings_.Widthes[DataType.branchCondition];
                }
                return Settings_.Widthes[DataType.branchElse];
            }
        }

        public override int Height
        {
            get
            {
                if (Type_ == DataType.branchCondition)
                {
                    return (int)(Settings_.Heights[DataType.branchCondition] * ScaleFactor);
                }
                return (int)(Settings_.Heights[DataType.branchElse] * ScaleFactor);
            }            
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
