using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;

namespace WsdlGeneration
{
    public partial class MessageListBox : ResizableListBox
    {
        // Fields
        private IContainer components;
        private ImageList IconList;
        private Font m_HeadingFont;
        private ImageList imageList1;
        private const int m_MainTextOffset = 30;

        // Methods
        public MessageListBox()
        {
            this.InitializeComponent_();
            this.m_HeadingFont = new Font(this.Font, FontStyle.Bold);
            base.MeasureItem += new MeasureItemEventHandler(this.MeasureItemHandler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            this.m_HeadingFont.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent_()
        {
            this.components = new Container();
            ResourceManager manager = new ResourceManager(typeof(MessageListBox));
            this.IconList = new ImageList(this.components);
            this.IconList.ColorDepth = ColorDepth.Depth8Bit;
            this.IconList.ImageSize = new Size(0x10, 0x10);
            try
            {
                this.IconList.ImageStream = (ImageListStreamer)manager.GetObject("imageList1.ImageStream");
            }
            catch (System.Exception ex)
            {

            }
            this.IconList.TransparentColor = Color.Transparent;
        }

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            string str;
            ParseMessageEventArgs args = base.Items[e.Index];
            Size size = new Size(base.Width - 60, this.Font.Height);
            StringBuilder builder = new StringBuilder(args.MessageText);
            int startIndex = 0;
            int num7 = 0;
            while (builder.Length > 0)
            {
                int num2;
                int num3;
                int num6;
                int index = builder.ToString().IndexOf('\n');
                if (index > 0)
                {
                    str = builder.ToString(startIndex, index - startIndex);
                    num6 = index + 1;
                }
                else
                {
                    index = builder.Length;
                    num6 = index;
                    str = builder.ToString();
                }
                e.Graphics.MeasureString(str, this.Font, (SizeF)size, StringFormat.GenericDefault, out num3, out num2);
                if (num3 < index)
                {
                    int num8 = str.LastIndexOf(' ', num3 - 1, num3);
                    if (num8 != -1)
                    {
                        num6 = num8 + 1;
                    }
                    else
                    {
                        num6 = num3;
                    }
                }
                num7 += num2;
                builder = builder.Remove(startIndex, num6);
            }
            builder = null;
            str = null;
            int num = num7 * this.Font.Height;
            e.ItemHeight = (this.IconList.ImageSize.Height + num) + 4;
            e.ItemWidth = size.Width;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            Rectangle bounds = e.Bounds;
            Color controlText = SystemColors.ControlText;
            ParseMessageEventArgs args = base.Items[e.Index];
            if (e.State == DrawItemState.Selected)
            {
                using (Brush brush = new SolidBrush(SystemColors.Highlight))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                controlText = SystemColors.HighlightText;
            }
            if (args.MessageType != ParseMessageType.None)
            {
                this.IconList.Draw(e.Graphics, bounds.Left + 1, bounds.Top + 2, (int)args.MessageType);
            }
            using (SolidBrush brush2 = new SolidBrush(controlText))
            {
                string str;
                e.Graphics.DrawString(args.LineHeader, this.m_HeadingFont, brush2, (float)((bounds.Left + this.IconList.ImageSize.Width) + 5), (float)((bounds.Top + this.IconList.ImageSize.Height) - this.m_HeadingFont.Height));
                int linesFilled = 0;
                int charactersFitted = 0;
                Size size = new Size(base.Width - 60, this.Font.Height);
                StringBuilder builder = new StringBuilder(args.MessageText);
                int startIndex = 0;
                int num3 = (bounds.Top + this.IconList.ImageSize.Height) + 2;
                while (builder.Length > 0)
                {
                    int num6;
                    int index = builder.ToString().IndexOf('\n');
                    if (index > 0)
                    {
                        str = builder.ToString(startIndex, index - startIndex);
                        num6 = index + 1;
                    }
                    else
                    {
                        index = builder.Length;
                        num6 = index;
                        str = builder.ToString();
                    }
                    e.Graphics.MeasureString(str, this.Font, (SizeF)size, StringFormat.GenericDefault, out charactersFitted, out linesFilled);
                    if (charactersFitted < index)
                    {
                        int num7 = str.LastIndexOf(' ', charactersFitted - 1, charactersFitted);
                        if (num7 != -1)
                        {
                            num6 = num7 + 1;
                        }
                        else
                        {
                            num6 = charactersFitted;
                        }
                        str = builder.ToString(startIndex, num6 - startIndex);
                    }
                    e.Graphics.DrawString(str, this.Font, brush2, (float)(bounds.Left + 30), (float)num3);
                    num3 += this.Font.Height;
                    builder = builder.Remove(startIndex, num6);
                }
                builder = null;
                str = null;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageListBox));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pen.bmp");
            this.ResumeLayout(false);

        }
    }
}
