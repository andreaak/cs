using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GrabDatabase.ParseQuery
{
    public static class Options
    {
        private static IDictionary<string, Color> lexemColors;
        public static IDictionary<string, Color> LexemColors
        {
            get 
            {
                if(lexemColors == null)
                {
                    lexemColors = GetLexemColors();
                }
                return lexemColors;
            }
        }

        private static IDictionary<string, Color> GetLexemColors()
        {
            Dictionary<string, Color> lexemColors = new Dictionary<string, Color>();
            lexemColors.Add("SELECT", Color.Blue);
            lexemColors.Add("INSERT", Color.Blue);
            lexemColors.Add("INTO", Color.Blue);
            lexemColors.Add("VALUES", Color.Blue);
            lexemColors.Add("UPDATE", Color.Blue);
            lexemColors.Add("SET", Color.Blue);
            lexemColors.Add("DELETE", Color.Blue);
            lexemColors.Add("ALL", Color.Purple);
            lexemColors.Add("UNION", Color.Purple);

            lexemColors.Add("FROM", Color.DarkOrange);
            lexemColors.Add("WHERE", Color.DarkMagenta);
            lexemColors.Add("AND", Color.Blue);
            lexemColors.Add("OR", Color.Blue);
            lexemColors.Add("?", Color.Red);
            //ORACLE
            lexemColors.Add("NUMBER", Color.DarkCyan);
            lexemColors.Add("VARCHAR", Color.DarkCyan);
            lexemColors.Add("VARCHAR2", Color.DarkCyan);
            lexemColors.Add("CURSOR", Color.DarkCyan);
            //SQL Server
            lexemColors.Add("INTEGER", Color.DarkCyan);
            lexemColors.Add("DECIMAL", Color.DarkCyan);
            lexemColors.Add("MONEY", Color.DarkCyan);
            
            lexemColors.Add("CREATE", Color.RoyalBlue);
            lexemColors.Add("FUNCTION", Color.RoyalBlue);
            lexemColors.Add("PROCEDURE", Color.RoyalBlue);
            lexemColors.Add("PROC", Color.RoyalBlue);
            lexemColors.Add("IN", Color.RoyalBlue);
            lexemColors.Add("OUT", Color.RoyalBlue);
            lexemColors.Add("AS", Color.RoyalBlue);
            lexemColors.Add("IS", Color.RoyalBlue);
            lexemColors.Add("BEGIN", Color.RoyalBlue);
            lexemColors.Add("RETURN", Color.RoyalBlue);
            lexemColors.Add("COMMIT", Color.RoyalBlue);
            lexemColors.Add("END", Color.RoyalBlue);
            
            lexemColors.Add("IF", Color.RoyalBlue);
            lexemColors.Add("ELSIF", Color.RoyalBlue);
            lexemColors.Add("ELSE", Color.RoyalBlue);
            lexemColors.Add("THEN", Color.RoyalBlue);
            lexemColors.Add("END IF", Color.RoyalBlue);
            
            lexemColors.Add("FOR", Color.RoyalBlue);
            lexemColors.Add("EXIT WHEN", Color.RoyalBlue);
            lexemColors.Add("WHILE", Color.RoyalBlue);
            lexemColors.Add("LOOP", Color.RoyalBlue);
            lexemColors.Add("END LOOP", Color.RoyalBlue);
            
            lexemColors.Add("OPEN", Color.RoyalBlue);
            lexemColors.Add("FETCH", Color.RoyalBlue);
            lexemColors.Add("@@FETCH_STATUS", Color.RoyalBlue);
            lexemColors.Add("CLOSE", Color.RoyalBlue);

            lexemColors.Add("EXCEPTION", Color.RoyalBlue);
            lexemColors.Add("WHEN", Color.RoyalBlue);
            
            lexemColors.Add("DECLARE", Color.RoyalBlue);
            lexemColors.Add("EXEC", Color.RoyalBlue);
            
            return lexemColors;
        }

        public static readonly Font SmallFont = new Font("TimesNewRoman", 8f, FontStyle.Regular);
        public static readonly Font MediumFont = new Font("TimesNewRoman", 10f, FontStyle.Regular);
        public static readonly Font LargeFont = new Font("TimesNewRoman", 12f, FontStyle.Regular);

        public static readonly Color CommentsColor = Color.MediumSeaGreen;
    }
}
