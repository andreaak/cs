using System;
using System.Collections.Generic;
using System.Text;

namespace Assist
{
    class BlockData
    {
        int startLine;
        int endLine;
        List<string> data;
        bool isSub;

        public BlockData(Dictionary<int, int> lineNumbers, List<string> data)
        {
            foreach (int start in lineNumbers.Keys)
            {
                startLine = start;
                endLine = lineNumbers[startLine];
                break;
            }
            this.data = data;
        }

        public BlockData(Dictionary<int, int> lineNumbers, List<string> data, bool isSub)
            :this(lineNumbers, data)
        {
            this.isSub = isSub;
        }
    }
}
