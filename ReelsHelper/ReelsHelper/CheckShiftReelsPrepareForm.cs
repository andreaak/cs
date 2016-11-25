using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace ReelsHelper
{
    public partial class CheckShiftReelsPrepareForm : Form
    {
        IList<Reel> reels;

        public CheckShiftReelsPrepareForm(IList<Reel> reels)
        {
            InitializeComponent();
            this.reels = reels;
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            IList<string> tallSymbols = textBoxTallSymbols.Text.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (tallSymbols.Count == 0)
            {
                return;
            }

            IList<TallSymbol> talls = ParseTallSymbols(tallSymbols);
            if (talls.Count == 0)
            {
                return;
            }

            IList<ErrorShift> errors = SimulateReels(reels, talls);
            listView1.Items.Clear();
            foreach (var error in errors)
            {
                listView1.Items.Add(new ListViewItem(new string[] {error.Reel.ToString(), error.Shift.ToString(), error.IconId.ToString() }));
            }

        }

        private List<ErrorShift> SimulateReels(IList<Reel> reels, IList<TallSymbol> talls)
        {
            List<ErrorShift> errors = new List<ErrorShift>();
            for (int i = 0; i < reels.Count; i++)
            {
                for (int j = 0; j < reels[i].Count; j++)
                {
                    IList<ErrorShift> error = SimulateReel(reels, talls, i, j);
                    if (error.Count != 0)
                    {
                        errors.AddRange(error);
                    }
                }
            }
            return errors;
        }

        public IList<ErrorShift> SimulateReel(IList<Reel> reels, IList<TallSymbol> talls, int reel, int shiftIndex)
        {
            shiftIndex = GetSymbolShift(talls, reels[reel], shiftIndex);

            var linesCount = reels[0].Indexes.Count;
            var resReel = new List<int>();

            for (int line = 0; line < linesCount; line++)
            {
                var lineIndex = (line + shiftIndex) % reels[reel].Indexes.Count;
                int iconIndex = reels[reel][lineIndex] ?? -1;
                resReel.Add(iconIndex);
            }

            var errors = new List<ErrorShift>();

            bool isCountActive = false;
            int tallIconIndex = -1;
            int tallIconHeight = 0;
            foreach (int icon in resReel)
            {
                if (talls.Any(item => item.Id == icon))
                {
                    if (isCountActive && tallIconIndex != icon)//new tall icon
                    {
                        if (talls.First(item => item.Id == tallIconIndex).Height != tallIconHeight)
                        {
                            errors.Add(new ErrorShift { Reel = reel, Shift = shiftIndex, IconId = tallIconIndex });
                        }
                        isCountActive = false;
                        tallIconIndex = -1;
                        tallIconHeight = 0;
                    }
                    isCountActive = true;
                    tallIconIndex = icon;
                    tallIconHeight++;
                }
                if (talls.All(item => item.Id != icon))
                {
                    if (isCountActive
                        && talls.First(item => item.Id == tallIconIndex).Height != tallIconHeight)
                    {
                        errors.Add(new ErrorShift() { Reel = reel, Shift = shiftIndex, IconId = tallIconIndex });
                    }
                    isCountActive = false;
                    tallIconIndex = -1;
                    tallIconHeight = 0;
                }
            }
            return errors;
        }

        private int GetSymbolShift(IList<TallSymbol> symbols, Reel reel, int shift)
        {
            var count = reel.Indexes.Count;

            for (var i = 0; i < count; i++)
            {
                var index_a = (i + shift) % count;
                var index_b = (i + shift + 1) % count;

                if (symbols.All(symbol => symbol.Id != reel.Indexes[index_a]) && symbols.All(symbol => symbol.Id != reel.Indexes[index_b]) || reel.Indexes[index_a] != reel.Indexes[index_b])
                {
                    return index_b;
                }
            }

            return 0;
        }

        private IList<TallSymbol> ParseTallSymbols(IList<string> tallSymbols)
        {
            IList<TallSymbol> list = new List<TallSymbol>();
            foreach (var symbol in tallSymbols)
            {
                IList<string> symbolItems = symbol.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                int id;
                int height;
                if (int.TryParse(symbolItems[0], out id) && int.TryParse(symbolItems[1], out height))
                {
                    list.Add(new TallSymbol { Id = id, Height = height });
                }
            }
            return list;
        }
    }

    public class ErrorShift
    {
        public int Reel { get; set; }

        public int Shift { get; set; }

        public int IconId { get; set; }
    }

    public class TallSymbol
    {
        public int Id { get; set; }

        public int Height { get; set; }
    }
}
