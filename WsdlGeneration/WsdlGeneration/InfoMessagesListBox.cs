using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WsdlGeneration;
using System.Drawing;

namespace WsdlGeneration
{
    public class InfoMessagesListBox : UserControl
    {
        // Fields
        private CheckBox cbxCanceled;
        private CheckBox cbxErrors;
        private CheckBox cbxInfo;
        private CheckBox cbxWarnings;
        private IContainer components = null;
        private MessageListBox messageListBox;
        private InfoMessages messages;

        // Methods
        public InfoMessagesListBox()
        {
            this.InitializeComponent();
        }

        private void cbx_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshMessagesList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cbxErrors = new CheckBox();
            this.cbxWarnings = new CheckBox();
            this.cbxInfo = new CheckBox();
            this.messageListBox = new MessageListBox();
            this.cbxCanceled = new CheckBox();
            base.SuspendLayout();
            this.cbxErrors.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cbxErrors.AutoSize = true;
            this.cbxErrors.Checked = true;
            this.cbxErrors.CheckState = CheckState.Checked;
            this.cbxErrors.Location = new Point(5, 0x193);
            this.cbxErrors.Name = "cbxErrors";
            this.cbxErrors.Size = new Size(0x45, 0x15);
            this.cbxErrors.TabIndex = 1;
            this.cbxErrors.Text = "Errors";
            this.cbxErrors.UseVisualStyleBackColor = true;
            this.cbxErrors.CheckedChanged += new EventHandler(this.cbx_CheckedChanged);
            this.cbxWarnings.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cbxWarnings.AutoSize = true;
            this.cbxWarnings.Checked = true;
            this.cbxWarnings.CheckState = CheckState.Checked;
            this.cbxWarnings.Location = new Point(0x65, 0x193);
            this.cbxWarnings.Name = "cbxWarnings";
            this.cbxWarnings.Size = new Size(90, 0x15);
            this.cbxWarnings.TabIndex = 2;
            this.cbxWarnings.Text = "Warnings";
            this.cbxWarnings.UseVisualStyleBackColor = true;
            this.cbxWarnings.CheckedChanged += new EventHandler(this.cbx_CheckedChanged);
            this.cbxInfo.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cbxInfo.AutoSize = true;
            this.cbxInfo.Checked = true;
            this.cbxInfo.CheckState = CheckState.Checked;
            this.cbxInfo.Location = new Point(0xd7, 0x193);
            this.cbxInfo.Name = "cbxInfo";
            this.cbxInfo.Size = new Size(0x35, 0x15);
            this.cbxInfo.TabIndex = 3;
            this.cbxInfo.Text = "Info";
            this.cbxInfo.UseVisualStyleBackColor = true;
            this.cbxInfo.CheckedChanged += new EventHandler(this.cbx_CheckedChanged);
            this.messageListBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.messageListBox.AutoScroll = true;
            this.messageListBox.AutoScrollMinSize = new Size(0x19b, 0);
            this.messageListBox.BackColor = Color.White;
            this.messageListBox.Location = new Point(3, 3);
            this.messageListBox.Name = "messageListBox";
            this.messageListBox.SelectedIndex = -1;
            this.messageListBox.SelectedItem = null;
            this.messageListBox.Size = new Size(0x1b9, 0x18a);
            this.messageListBox.TabIndex = 0;
            this.cbxCanceled.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.cbxCanceled.AutoSize = true;
            this.cbxCanceled.Location = new Point(0x11d, 0x193);
            this.cbxCanceled.Name = "cbxCanceled";
            this.cbxCanceled.Size = new Size(0x76, 0x15);
            this.cbxCanceled.TabIndex = 4;
            this.cbxCanceled.Text = "Canceled files";
            this.cbxCanceled.UseVisualStyleBackColor = true;
            this.cbxCanceled.CheckedChanged += new EventHandler(this.cbx_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.cbxCanceled);
            base.Controls.Add(this.cbxInfo);
            base.Controls.Add(this.cbxWarnings);
            base.Controls.Add(this.cbxErrors);
            base.Controls.Add(this.messageListBox);
            base.Name = "InfoMessagesListBox";
            base.Size = new Size(0x1bf, 0x1ad);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void RefreshMessagesList()
        {
            this.messageListBox.Items.Clear();
            if (this.messages != null)
            {
                foreach (Message message in this.messages.Messages)
                {
                    if ((message.Level == InfoLevel.Error) && this.cbxErrors.Checked)
                    {
                        this.messageListBox.Items.Add(new ParseMessageEventArgs(ParseMessageType.Error, message.Title, message.DisplayMessageText));
                    }
                    else if ((message.Level == InfoLevel.Warning) && this.cbxWarnings.Checked)
                    {
                        this.messageListBox.Items.Add(new ParseMessageEventArgs(ParseMessageType.Warning, message.Title, message.DisplayMessageText));
                    }
                    else if ((message.Level == InfoLevel.Info) && this.cbxInfo.Checked)
                    {
                        this.messageListBox.Items.Add(new ParseMessageEventArgs(ParseMessageType.Info, message.Title, message.DisplayMessageText));
                    }
                    else if ((message.Level == InfoLevel.Canceled) && this.cbxCanceled.Checked)
                    {
                        this.messageListBox.Items.Add(new ParseMessageEventArgs(ParseMessageType.None, message.Title, message.DisplayMessageText));
                    }
                }
            }
            this.messageListBox.Invalidate();
        }

        // Properties
        internal InfoMessages Messages
        {
            get
            {
                return this.messages;
            }
            set
            {
                this.messages = value;
                this.RefreshMessagesList();
            }
        }
    }
}
