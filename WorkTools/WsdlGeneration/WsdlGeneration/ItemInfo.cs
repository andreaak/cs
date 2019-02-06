using System;
using System.Collections.Generic;
using System.Text;

namespace WsdlGeneration
{
    public class ItemInfo
    {
        // Fields
        private bool _bHeightValid;
        private int _iHeight;
        private ParseMessageEventArgs _pmeaMessage;

        // Methods
        public ItemInfo(ParseMessageEventArgs pmea)
        {
            this._iHeight = 0;
            this._bHeightValid = false;
            this._pmeaMessage = pmea;
        }

        public ItemInfo(int Height, bool HeightValid, ParseMessageEventArgs pmea)
        {
            this._iHeight = Height;
            this._bHeightValid = HeightValid;
            this._pmeaMessage = pmea;
        }

        // Properties
        public int Height
        {
            get
            {
                return this._iHeight;
            }
            set
            {
                this._iHeight = value;
                this._bHeightValid = true;
            }
        }

        public bool HeightValid
        {
            get
            {
                return this._bHeightValid;
            }
            set
            {
                this._bHeightValid = value;
            }
        }

        public ParseMessageEventArgs Message
        {
            get
            {
                return this._pmeaMessage;
            }
        }
    }
}
