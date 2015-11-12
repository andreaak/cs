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
            lexemColors.Add("DISTINCT", Color.Blue);
            
            lexemColors.Add("FROM", Color.SaddleBrown);

            lexemColors.Add("ON", Color.SaddleBrown);
            lexemColors.Add("JOIN", Color.SaddleBrown);
            lexemColors.Add("CROSS JOIN", Color.SaddleBrown);
            lexemColors.Add("INNER JOIN", Color.SaddleBrown);
            lexemColors.Add("LEFT JOIN", Color.SaddleBrown);
            lexemColors.Add("LEFT OUTER JOIN", Color.SaddleBrown);
            lexemColors.Add("RIGHT JOIN", Color.SaddleBrown);
            lexemColors.Add("RIGHT OUTER JOIN", Color.SaddleBrown);
            lexemColors.Add("FULL OUTER JOIN", Color.SaddleBrown);
            lexemColors.Add("NATURAL JOIN", Color.SaddleBrown);
            lexemColors.Add("USING", Color.SaddleBrown);
            
            lexemColors.Add("WHERE", Color.DarkMagenta);
            lexemColors.Add("AND", Color.DarkMagenta);
            lexemColors.Add("OR", Color.DarkMagenta);
            lexemColors.Add("?", Color.Red);
            lexemColors.Add("IN", Color.DarkMagenta);
            lexemColors.Add("BETWEEN", Color.DarkMagenta);
            lexemColors.Add("LIKE", Color.DarkMagenta);
            lexemColors.Add("NOT", Color.DarkMagenta);
            lexemColors.Add("EXISTS", Color.DarkMagenta);
            lexemColors.Add("ANY", Color.DarkMagenta);
            lexemColors.Add("ALL", Color.DarkMagenta);
            
            lexemColors.Add("MAX", Color.Brown);
            lexemColors.Add("MIN", Color.Brown);
            lexemColors.Add("COUNT", Color.Brown);
            lexemColors.Add("AVG", Color.Brown);
            lexemColors.Add("SUM", Color.Brown);
            
            lexemColors.Add("UNION", Color.Aqua);
            lexemColors.Add("UNION ALL", Color.Aqua);
            lexemColors.Add("EXCEPT", Color.Aqua);
            lexemColors.Add("INTERSECT", Color.Aqua);
            lexemColors.Add("MINUS", Color.Aqua);
            
            lexemColors.Add("GROUP  BY", Color.DeepSkyBlue);
            lexemColors.Add("HAVING", Color.DeepSkyBlue);

            lexemColors.Add("ORDER  BY", Color.DarkSlateBlue);
            lexemColors.Add("ASC", Color.DarkSlateBlue);
            lexemColors.Add("DESC", Color.DarkSlateBlue);
            
            lexemColors.Add("INSERT", Color.Blue);
            lexemColors.Add("INTO", Color.Blue);
            lexemColors.Add("VALUES", Color.Blue);
            
            lexemColors.Add("UPDATE", Color.Blue);
            lexemColors.Add("SET", Color.Blue);
            
            lexemColors.Add("DELETE", Color.Blue);

            lexemColors.Add("CREATE", Color.Blue);
            lexemColors.Add("ALTER", Color.Blue);
            lexemColors.Add("ADD", Color.Blue);
            lexemColors.Add("DROP", Color.Blue);
            lexemColors.Add("MODIFY", Color.Blue);
            lexemColors.Add("TABLE", Color.Blue);
            lexemColors.Add("COLUMN", Color.Blue);
            lexemColors.Add("SEQUENCE", Color.Blue);
            lexemColors.Add("DATATYPE", Color.Blue);

            lexemColors.Add("COMMIT", Color.Blue);
            lexemColors.Add("ROLLBACK", Color.Blue);
            lexemColors.Add("AUTOCOMMIT", Color.Blue);
            
            lexemColors.Add("CONSTRAINT", Color.Blue);
            lexemColors.Add("RESTRICT", Color.Blue);
            lexemColors.Add("CASCADE", Color.Blue);

            //VARIABLES
            lexemColors.Add("NULL", Color.DarkCyan);
            //ORACLE
            lexemColors.Add("NUMBER", Color.DarkCyan);
            lexemColors.Add("VARCHAR", Color.DarkCyan);
            lexemColors.Add("VARCHAR2", Color.DarkCyan);
            lexemColors.Add("CURSOR", Color.DarkCyan);
            lexemColors.Add("DATETIME", Color.DarkCyan);
            lexemColors.Add("DATE", Color.DarkCyan);
            lexemColors.Add("TIME", Color.DarkCyan);
            lexemColors.Add("INTERVAL", Color.DarkCyan);
            lexemColors.Add("YEAR", Color.DarkCyan);
            lexemColors.Add("TO MONTH", Color.DarkCyan);
            lexemColors.Add("DAY", Color.DarkCyan);
            lexemColors.Add("TO SECOND", Color.DarkCyan);
            //SQL Server
            lexemColors.Add("INTEGER", Color.DarkCyan);
            lexemColors.Add("DECIMAL", Color.DarkCyan);
            lexemColors.Add("MONEY", Color.DarkCyan);
            lexemColors.Add("CHAR", Color.DarkCyan);
            lexemColors.Add("INT", Color.DarkCyan);
            lexemColors.Add("BIT", Color.DarkCyan);
            lexemColors.Add("BIGINT", Color.DarkCyan);
            lexemColors.Add("TINYINT", Color.DarkCyan);
            
            lexemColors.Add("NOT NULL", Color.DarkCyan);
            lexemColors.Add("PRIMARY KEY", Color.DarkCyan);
            lexemColors.Add("UNIQUE", Color.DarkCyan);
            lexemColors.Add("CHECK", Color.DarkCyan);
            lexemColors.Add("DEFAULT", Color.DarkCyan);
            lexemColors.Add("FOREIGN KEY", Color.DarkCyan);
            lexemColors.Add("REFERENCES", Color.DarkCyan);
            lexemColors.Add("ON UPDATE", Color.DarkCyan);
            lexemColors.Add("ON DELETE", Color.DarkCyan);
            lexemColors.Add("SET NULL", Color.DarkCyan);
            lexemColors.Add("SET DEFAULT", Color.DarkCyan);
            lexemColors.Add("NO ACTION", Color.DarkCyan);
            
            lexemColors.Add("NEXTVAL", Color.DarkCyan);
            lexemColors.Add("MINVALUE", Color.DarkCyan);
            lexemColors.Add("START WITH", Color.DarkCyan);
            lexemColors.Add("INCREMENT BY", Color.DarkCyan);
            lexemColors.Add("CACHE", Color.DarkCyan);
            lexemColors.Add("AUTOINCREMENT", Color.DarkCyan);
            lexemColors.Add("IDENTITY", Color.DarkCyan);
            lexemColors.Add("AUTO_INCREMENT", Color.DarkCyan);

            lexemColors.Add("DECLARE", Color.RoyalBlue);
            lexemColors.Add("FUNCTION", Color.RoyalBlue);
            lexemColors.Add("PROCEDURE", Color.RoyalBlue);
            lexemColors.Add("PROC", Color.RoyalBlue);
            lexemColors.Add("OUT", Color.RoyalBlue);
            lexemColors.Add("AS", Color.RoyalBlue);
            lexemColors.Add("IS", Color.RoyalBlue);
            lexemColors.Add("BEGIN", Color.RoyalBlue);
            lexemColors.Add("RETURN", Color.RoyalBlue);
            lexemColors.Add("END", Color.RoyalBlue);
            
            lexemColors.Add("IF", Color.RoyalBlue);
            lexemColors.Add("ELSIF", Color.RoyalBlue);
            lexemColors.Add("ELSE", Color.RoyalBlue);
            lexemColors.Add("THEN", Color.RoyalBlue);
            lexemColors.Add("END IF", Color.RoyalBlue);
            
            lexemColors.Add("CASE", Color.RoyalBlue);
            lexemColors.Add("WHEN", Color.RoyalBlue);
            lexemColors.Add("COALESCE", Color.RoyalBlue);
            lexemColors.Add("NULLIF", Color.RoyalBlue);
            
            lexemColors.Add("FOR", Color.RoyalBlue);
            lexemColors.Add("EXIT WHEN", Color.RoyalBlue);
            lexemColors.Add("WHILE", Color.RoyalBlue);
            lexemColors.Add("LOOP", Color.RoyalBlue);
            lexemColors.Add("END LOOP", Color.RoyalBlue);
            
            lexemColors.Add("CURSOR FOR", Color.RoyalBlue);
            lexemColors.Add("OPEN", Color.RoyalBlue);
            lexemColors.Add("FETCH", Color.RoyalBlue);
            lexemColors.Add("@@FETCH_STATUS", Color.RoyalBlue);
            lexemColors.Add("CLOSE", Color.RoyalBlue);

            lexemColors.Add("EXCEPTION", Color.RoyalBlue);

            lexemColors.Add("EXECUTE", Color.Blue);
            lexemColors.Add("EXEC", Color.Blue);


            lexemColors.Add("IS NULL", Color.DarkMagenta);
            lexemColors.Add("IS NOT NULL", Color.DarkMagenta);

            lexemColors.Add("TRIGGER", Color.Blue);
            //Oracle
            lexemColors.Add("BEFORE INSERT ON", Color.Blue);
            lexemColors.Add("BEFORE UPDATE ON", Color.Blue);
            lexemColors.Add("BEFORE DELETE ON", Color.Blue);
            lexemColors.Add("AFTER INSERT ON", Color.Blue);
            lexemColors.Add("AFTER UPDATE ON", Color.Blue);
            lexemColors.Add("AFTER DELETE ON", Color.Blue);
            //SqlServer
            lexemColors.Add("FOR INSERT", Color.Blue);
            lexemColors.Add("FOR UPDATE", Color.Blue);
            lexemColors.Add("FOR DELETE", Color.Blue);
            lexemColors.Add("RAISERROR", Color.Red);

            lexemColors.Add("VIEW", Color.Blue);
            lexemColors.Add("WITH CHECK OPTION", Color.Blue);
            lexemColors.Add("WITH LOCAL CHECK OPTION", Color.Blue);
            lexemColors.Add("WITH CASCADED CHECK OPTION", Color.Blue);


            return lexemColors;
        }

        public static readonly Font SmallFont = new Font("TimesNewRoman", 8f, FontStyle.Regular);
        public static readonly Font MediumFont = new Font("TimesNewRoman", 10f, FontStyle.Regular);
        public static readonly Font LargeFont = new Font("TimesNewRoman", 12f, FontStyle.Regular);

        public static readonly Color CommentsColor = Color.MediumSeaGreen;
    }
}
