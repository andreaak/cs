using System.Data;
using System.Linq;
using System.Text;

namespace Note
{
    public static class Utils
    {

        public static string GetValidFileName(string nodeText)
        {
            char[] forbiddenSymbols = { '\\', '/', '*', '?', '\"', '<', '>', '|', '\n', '\r', ':' };
            StringBuilder outStr = new StringBuilder(nodeText.Length);
            foreach (char ch in nodeText)
            {
                if (!forbiddenSymbols.Contains(ch))
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
            return row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached;
        }
    }
}
