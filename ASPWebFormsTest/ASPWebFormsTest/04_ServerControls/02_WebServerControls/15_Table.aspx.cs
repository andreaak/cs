using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._04_ServerControls._02_WebServerControls
{
    public partial class _15_Table : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Table table = new Table();

            table.CellPadding = 5; // Отступ текста от краев ячейки. (в пикселях).
            table.CellSpacing = 5; // Отступ между ячейками. (в пикселях).
            table.BorderWidth = 1; // Толщина рамки.
            table.BorderStyle = BorderStyle.Solid; // Стиль рамки.

            // Ряд.
            TableRow row;

            // Ячейка.
            TableCell cell;

            for (int i = 1; i <= 10; i++)
            {
                // Создаем ряд.
                row = new TableRow();

                for (int j = 1; j <= 10; j++)
                {
                    // Создаем ячейку.
                    cell = new TableCell();

                    // Если это первый ряд или первая ячейка ряда, то указываем красный фон.
                    if (i == 1 || j == 1)
                    {
                        cell.BackColor = Color.Red; // указываем красный фон.
                        cell.ForeColor = Color.Yellow; // указываем желтый цвет текста.
                    }

                    // Указываем стили ячейке.
                    cell.BorderWidth = 1; // Толщина рамки.
                    cell.BorderStyle = BorderStyle.Dashed; // Стиль рамки.

                    // Указываем текст ячейки.
                    cell.Text = (i * j).ToString();

                    // Добавляем ячейку в ряд.
                    row.Cells.Add(cell);
                }

                // Добавляем ряд в таблицу.
                table.Rows.Add(row);
            }

            PlaceHoolder1.Controls.Add(table);
        }
    }
}