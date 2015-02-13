using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    class UTILS
    {
        public static bool MoveMethod(BaseItem parentItem, BaseItem currentItem, bool isUp)
        {
            int index = parentItem.Items.IndexOf(currentItem);

            List<BaseItem> temp = null;
            if (isUp)
            {
                temp = MoveUp(index, parentItem, currentItem);
            }
            else
            {
                temp = MoveDown(index, parentItem);

            }
            if (temp == null)
            {
                return false;
            }
            parentItem.SetItems(temp);
            return true;
        }

        private static List<BaseItem> MoveUp(int index, BaseItem parentItem, BaseItem currentItem)
        {
            List<BaseItem> temp = new List<BaseItem>();
            if (index == 0 || currentItem.Type_ == DataType.branchElse)
            {
                return null;
            }
            for (int i = 0; i < parentItem.Items.Count; i++)
            {
                if (i == index)
                {
                    continue;
                }
                if (i == (index - 1))
                {
                    temp.Add(parentItem.Items[index]);
                }
                temp.Add(parentItem.Items[i]);
            }
            return temp;
        }

        private static List<BaseItem> MoveDown(int index, BaseItem parentItem)
        {
            List<BaseItem> temp = new List<BaseItem>();
            if (index == parentItem.Items.Count - 1 || parentItem.Items[index + 1].Type_ == DataType.branchElse)
            {
                return null;
            }
            for (int i = 0; i < parentItem.Items.Count; i++)
            {
                if (i == index)
                {
                    temp.Add(parentItem.Items[i + 1]);
                    temp.Add(parentItem.Items[i]);
                    i++;
                    continue;
                }
                temp.Add(parentItem.Items[i]);
            }
            return temp;
        }
    }
}
