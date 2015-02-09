using System;
using System.Collections.Generic;
using System.Text;

namespace Assist
{
    public class SelectedLine
    {
        public List<string> beforeStrings;
        public List<string> afterStrings;
        public string _selectionString;
        public string _selection;
        public int _lineNumber;
        public int _lineOffset;
        public bool _isRight;

        public SelectedLine(string selection, int findInLine, int findOffset)
        {
            this._selection = selection;
            this._lineNumber = findInLine;
            this._lineOffset = findOffset;
            beforeStrings = new List<string>();
            afterStrings = new List<string>();
        }

        public void AddStringBefore(string str)
        {
            beforeStrings.Add(str);
        }

        public void AddStringAfter(string str)
        {
            afterStrings.Add(str);
        }

        public void AddSelection(string str)
        {
            _selectionString = str;
            SetTypeOfSelection(str);
        }

        public void SetTypeOfSelection(string str)
        {
            _isRight = true;
            int selIndex = str.IndexOf(_selection);
            int eqIndex = str.IndexOf('=');
            if (str.IndexOf(_selection) > str.IndexOf('='))
            {
                return;
            }
            char[] symbols = { '!', '<', '>' };
            for (int i = eqIndex -1; i > 0; i--)
            {
                if (str[i].Equals(' '))
                {
                    continue;
                }
                string chBefore = str[i].ToString();
                if (chBefore.IndexOfAny(symbols) != -1)
                {
                    return;
                }
                else
                {
                    break;
                }
            }

            char[] symbols2 = { '=' };
            for (int i = eqIndex + 1 ; i < str.Length; i++)
            {
                if (str[i].Equals(' '))
                {
                    continue;
                }
                string chAfter = str[i].ToString();
                if (chAfter.IndexOfAny(symbols2) != -1)
                {
                    return;
                }
                else
                {
                    break;
                }
            }
            _isRight = false;
        }
    }
}
