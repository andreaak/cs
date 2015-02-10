using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{

    [Serializable]
    public class BranchItem : BaseItem
    {
        public BranchItem()
        {
        }

        //for parsing
        public BranchItem(string allData)
        {
            string[] nsc = ParseItemName(allData, ref this.name);
            ParseNamespaceAndClass(nsc, ref this.namespace_, ref this.class_);
        }

        public BranchItem(string namespaceString, string class_, string name, string condition, bool static_, string language, 
            int startLine, int endLine, int currentLine, string fileName, BaseItem parent)
            : base(namespaceString, class_, name, DataType.branch, static_, language, startLine, endLine, currentLine, fileName, parent)
        {
        }

        public void AddBlock(string condition, DataType itemType,int startLine, int endLine, int currentLine)
        {
            items.Add(new BranchBlockItem(condition, itemType, startLine, endLine, currentLine, this));
        }

        public void RemoveBlock(string condition, DataType itemType, int startLine, int endLine, int currentLine)
        {
            items.Remove(new BranchBlockItem(condition, itemType, startLine, endLine, currentLine, this));
        }

        public void AddBlocks(List<BaseItem> blocks)
        {
            foreach (BranchBlockItem block in blocks)
            {
                AddBlock(block.Condition, block.Type_, block.StartLine, block.EndLine, block.CurrentLine);
            }
        }

        public override void SetParam(string param)
        {
            
        }

        public override void Copy(List<BaseItem> items_)
        {
            items.Clear();
            foreach (BranchBlockItem item in items_)
            {
                BranchBlockItem temp = new BranchBlockItem(item);
                items.Add(temp);
            }
        }

        #region DESCRIPTION

        public override string GetDescription()
        {
            return "Branch";
        }

        #endregion

        public override int Width
        {
            get { return 0; }
        }

        public override int Height
        {
            get { return 0; }
        }

        public override bool Drawable
        {
            get { return false; }
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}
