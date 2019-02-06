using System;
using System.Collections.Generic;
using System.Text;

namespace DiffgramAnalyzer
{
    public enum RowStatus
    {
        inserted,
        modified,
        deleted,
        before,
        unchanged,
    }
    public class Row
    {
        public string name;
        public RowStatus status;
        public List<Cell> cells;

        public Row(string name, RowStatus status)
        {
            this.name = name;
            this.status = status;
            cells = new List<Cell>();
        }

        public void AddCell(Cell cell)
        {
            if (!cells.Contains(cell))
            {
                cells.Add(cell);
            }
        }

        public Cell GetCell(string cellName)
        {
            foreach (Cell cell in cells)
            {
                if (cell.name.Equals(cellName))
                {
                    return cell;
                }
            }
            return null;
        }
    }
}
