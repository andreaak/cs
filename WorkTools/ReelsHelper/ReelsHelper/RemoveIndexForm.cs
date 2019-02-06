using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ReelsHelper
{
    public partial class RemoveIndexForm : Form
    {
        IList<Reel> reels;

        public RemoveIndexForm(IList<Reel> reels)
        {
            InitializeComponent();
            this.reels = reels;
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            IList<int> replace = ParseReplaceSymbols(textBoxReplace.Text);
            IList<int> with = ParseReplaceSymbols(textBoxWith.Text);
            if(!replace.Any() || replace.Count != with.Count)
            {
                return;
            }
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < replace.Count; i++)
            {
                dict[replace[i]] = with[i];
            }
            ReplaceIndexes(dict);
        }

        private IList<int> ParseReplaceSymbols(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                return Enumerable.Empty<int>().ToArray();
            }
            var parsed = text?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> res = new List<int>();
            foreach (var item in parsed)
            {
                int temp;
                if(int.TryParse(item, out temp))
                {
                    res.Add(temp);
                }
            }
            return res;
        }

        private void ReplaceIndexes(Dictionary<int, int> dict)
        {
            IList<Reel> newReels = new List<Reel>();

            foreach (Reel reel in reels)
            {
                Reel newReel = reel.Clone();
                newReels.Add(newReel);

                MarkReplacedIndexes(newReel, dict);

                int correction = 0;
                foreach (var item in dict.OrderBy(item => item.Key))
                {
                    ReplaceIndex(newReel, item.Key - correction++);
                }
                newReel.Indexes = newReel.Indexes
                .Select(item => (item > 0 ? item : -item)).ToArray();
            }
            textBoxResult.Text = JSonParser.CreateJson(newReels.Select(item => item.Indexes.ToArray()).ToArray())
                .Replace("],[", "],\r\n[");
        }

        private void MarkReplacedIndexes(Reel reel, Dictionary<int, int> dict)
        {
            reel.Indexes = reel.Indexes
                .Select(item => Process(item, dict)).ToArray();
        }

        private int Process(int item, Dictionary<int, int> dict)
        {
            int value;
            if (dict.TryGetValue(item, out value))
            {
                return -value;
            }
            return item;
        }

        private void ReplaceIndex(Reel reel, int replace)
        {
            reel.Indexes = reel.Indexes
                .Select(item => Process(item, replace)).ToArray();
        }

        private int Process(int item, int replace)
        {
            //if (item == replace)
            //{
            //    item = - with;
            //}
            if (item > replace)
            {
                --item;
            }
            return item;
        }
    }
}
