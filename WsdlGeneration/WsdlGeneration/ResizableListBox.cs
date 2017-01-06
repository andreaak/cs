using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace WsdlGeneration
{
    public class ResizableListBox : Panel
    {
        // Fields
        private bool m_AllowMultiSelect = true;
        private bool m_CtrlPressed = false;
        private ListBoxList m_Items = new ListBoxList();
        private ArrayList m_SelectedItemIndices = new ArrayList();
        private ArrayList m_SelectedItems = new ArrayList();

        // Events
        public event DrawItemEventHandler DrawItem;

        public event MeasureItemEventHandler MeasureItem;

        public event EventHandler SelectedIndexChanged;

        // Methods
        public ResizableListBox()
        {
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.Opaque | ControlStyles.UserPaint, true);
            this.BackColor = Color.White;
            this.AutoScroll = true;
            base.HScroll = false;
            this.m_Items.OnItemInserted += new InsertEventHandler(this.ItemInserted);
        }

        private void AddSelectedItem(int index)
        {
            if (index == -1)
            {
                this.m_SelectedItemIndices.Clear();
                this.m_SelectedItems.Clear();
            }
            else
            {
                this.m_SelectedItemIndices.Add(index);
                this.m_SelectedItems.Add(this.m_Items[index]);
            }
            this.OnSelectedIndexChanged(new EventArgs());
        }

        private int ItemHitTest(int X, int Y)
        {
            int y = base.AutoScrollPosition.Y;
            for (int i = 0; i < this.m_Items.Count; i++)
            {
                y += this.m_Items.Info(i).Height;
                if (Y < y)
                {
                    return i;
                }
            }
            return -1;
        }

        private void ItemInserted(int index)
        {
            for (int i = 0; i < this.m_SelectedItemIndices.Count; i++)
            {
                int num2 = (int)this.m_SelectedItemIndices[i];
                if (num2 >= index)
                {
                    this.m_SelectedItemIndices[i] = num2 + 1;
                }
            }
        }

        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            Rectangle bounds = e.Bounds;
            Color controlText = SystemColors.ControlText;
            if (e.State == DrawItemState.Selected)
            {
                using (Brush brush = new SolidBrush(SystemColors.Highlight))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                controlText = SystemColors.HighlightText;
            }
            using (SolidBrush brush2 = new SolidBrush(controlText))
            {
                e.Graphics.DrawString(this.m_Items[e.Index].ToString(), this.Font, brush2, (float)bounds.Left, (float)bounds.Top);
            }
            if (this.DrawItem != null)
            {
                this.DrawItem(this, e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.m_CtrlPressed = e.Control;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            this.m_CtrlPressed = e.Control;
        }

        protected virtual void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = this.Font.Height;
            if (this.MeasureItem != null)
            {
                this.MeasureItem(this, e);
            }
            this.m_Items.Info(e.Index).Height = e.ItemHeight;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.Focus();
            if (e.Button == MouseButtons.Left)
            {
                int item = this.ItemHitTest(e.X, e.Y);
                if (item >= 0)
                {
                    if (this.m_CtrlPressed && this.m_AllowMultiSelect)
                    {
                        if (this.m_SelectedItemIndices.Contains(item))
                        {
                            this.RemoveSelectedItem(item);
                        }
                        else
                        {
                            this.AddSelectedItem(item);
                        }
                    }
                    else
                    {
                        if (this.m_SelectedItemIndices.Contains(item) && (this.m_SelectedItemIndices.Count == 1))
                        {
                            return;
                        }
                        this.m_SelectedItemIndices.Clear();
                        this.m_SelectedItems.Clear();
                        this.AddSelectedItem(item);
                    }
                    base.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics graphics = pe.Graphics;
            Rectangle rect = new Rectangle();
            int y = base.AutoScrollPosition.Y;
            int height = 0;
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                graphics.FillRectangle(brush, base.ClientRectangle);
            }
            for (int i = 0; i < this.m_Items.Count; i++)
            {
                if (!this.m_Items.Info(i).HeightValid)
                {
                    MeasureItemEventArgs e = new MeasureItemEventArgs(graphics, i);
                    this.OnMeasureItem(e);
                }
                int num4 = this.m_Items.Info(i).Height;
                if (((y + num4) >= 0) && (y < base.ClientRectangle.Height))
                {
                    rect.Location = new Point(base.AutoScrollPosition.X, y);
                    rect.Size = new Size(base.ClientRectangle.Right, num4);
                    DrawItemState state = this.m_SelectedItemIndices.Contains(i) ? DrawItemState.Selected : DrawItemState.Default;
                    DrawItemEventArgs args2 = new DrawItemEventArgs(graphics, this.Font, rect, i, state, this.ForeColor, this.BackColor);
                    this.OnDrawItem(args2);
                }
                y += num4;
                height += num4;
            }
            base.AutoScrollMinSize = new Size(base.Width - 30, height);
        }

        protected override void OnResize(EventArgs e)
        {
            for (int i = 0; i < this.m_Items.Count; i++)
            {
                this.m_Items.Info(i).HeightValid = false;
            }
            base.OnResize(e);
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
            {
                this.SelectedIndexChanged(this, e);
            }
        }

        private void RemoveSelectedItem(int index)
        {
            this.m_SelectedItemIndices.Remove(index);
            this.m_SelectedItems.Remove(this.m_Items[index]);
            this.OnSelectedIndexChanged(new EventArgs());
        }

        // Properties
        public ListBoxList Items
        {
            get
            {
                return this.m_Items;
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (this.m_SelectedItemIndices.Count > 0)
                {
                    return (int)this.m_SelectedItemIndices[0];
                }
                return -1;
            }
            set
            {
                if ((value < this.m_Items.Count) && (value >= -1))
                {
                    this.AddSelectedItem(value);
                }
            }
        }

        public object SelectedItem
        {
            get
            {
                if (this.m_SelectedItems.Count > 0)
                {
                    return this.m_SelectedItems[0];
                }
                return null;
            }
            set
            {
                int index = this.m_Items.IndexOf(value);
                if (index >= 0)
                {
                    this.m_SelectedItemIndices.Clear();
                    this.m_SelectedItems.Clear();
                    this.AddSelectedItem(index);
                }
            }
        }

        public ArrayList SelectedItemIndices
        {
            get
            {
                return this.m_SelectedItemIndices;
            }
        }

        public ArrayList SelectedItems
        {
            get
            {
                return this.m_SelectedItems;
            }
        }
    }

}
