using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Note
{
    public class UTILS
    {

        public static string GetValidFileName(string nodeText)
        {
            char[] del = { '\\', '/', '*', '?', '\"', '<', '>', '|' , '\n', '\r', ':'};
            StringBuilder outStr = new StringBuilder(nodeText.Length);
            foreach (char ch in nodeText)
            {
                if (!del.Contains(ch))
                {
                    outStr.Append(ch);
                }
                else
                {
                    outStr.Append(' ');
                }
            }
            return outStr.ToString();
        }

        public static bool IsActiveRow(DataRow row)
        {
            return row.RowState != DataRowState.Deleted;
        }
    }
}
