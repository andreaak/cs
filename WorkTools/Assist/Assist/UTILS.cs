using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;

namespace Assist
{
    class UTILS
    {
        static char separator = ';';
        
        public static Color GetColor(Dictionary<string, object> values, string key)
        {
            string colorString = values[key].ToString();
            string[] temp = colorString.Split(separator);
            int r, g, b, a;
            if (int.TryParse(temp[0], out r)
                && int.TryParse(temp[1], out g)
                && int.TryParse(temp[2], out b)
                && int.TryParse(temp[3], out a)
                )
            {
                return Color.FromArgb(a, r, g, b);
            }
            return Color.Black;
        }

        public static string GetRGBA(Color color)
        {
            string colorString = string.Format("{0}{4}{1}{4}{2}{4}{3}", color.R, color.G, color.B, color.A, separator);
            return colorString;
        }

        public static List<string> GetSepDataFromGrid(System.Windows.Forms.DataGridView dataGridView1)
        {
            List<string> list = new List<string>();
            foreach (System.Windows.Forms.DataGridViewRow item in dataGridView1.Rows)
            {
                list.Add(Convert.ToString(item.Cells[0].Value));
            }
            return list;
        }

        public static List<string> GetSepDataFromString(string p, char separator2)
        {
            return new List<string>(p.Split(new char[] { separator2 }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static void FillGrid(System.Windows.Forms.DataGridView dataGridView1, List<string> list)
        {
            dataGridView1.Rows.Clear();
            foreach (string item in list)
            {
                int number = dataGridView1.Rows.Add();
                dataGridView1.Rows[number].Cells[0].Value = item;
            }
           
        }
    }
}
