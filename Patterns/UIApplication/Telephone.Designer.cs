namespace UIApplication
{
    partial class Telephone
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonInitiateCall = new System.Windows.Forms.Button();
            this.buttonReceiverDown = new System.Windows.Forms.Button();
            this.buttonReceiverUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelViewState = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelBell = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLine = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelReceiver = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonInitiateCall
            // 
            this.buttonInitiateCall.Location = new System.Drawing.Point(537, 388);
            this.buttonInitiateCall.Name = "buttonInitiateCall";
            this.buttonInitiateCall.Size = new System.Drawing.Size(124, 23);
            this.buttonInitiateCall.TabIndex = 0;
            this.buttonInitiateCall.Text = "Initiate Call";
            this.buttonInitiateCall.UseVisualStyleBackColor = true;
            this.buttonInitiateCall.Click += new System.EventHandler(this.buttonInitiateCall_Click);
            // 
            // buttonReceiverDown
            // 
            this.buttonReceiverDown.Location = new System.Drawing.Point(71, 240);
            this.buttonReceiverDown.Name = "buttonReceiverDown";
            this.buttonReceiverDown.Size = new System.Drawing.Size(158, 61);
            this.buttonReceiverDown.TabIndex = 0;
            this.buttonReceiverDown.Text = "Receiver\r\nDown";
            this.buttonReceiverDown.UseVisualStyleBackColor = true;
            this.buttonReceiverDown.Click += new System.EventHandler(this.buttonReceiverDown_Click);
            // 
            // buttonReceiverUp
            // 
            this.buttonReceiverUp.Location = new System.Drawing.Point(253, 240);
            this.buttonReceiverUp.Name = "buttonReceiverUp";
            this.buttonReceiverUp.Size = new System.Drawing.Size(158, 61);
            this.buttonReceiverUp.TabIndex = 0;
            this.buttonReceiverUp.Text = "Receiver Up";
            this.buttonReceiverUp.UseVisualStyleBackColor = true;
            this.buttonReceiverUp.Click += new System.EventHandler(this.buttonReceiverUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current view state:";
            // 
            // labelViewState
            // 
            this.labelViewState.AutoSize = true;
            this.labelViewState.Location = new System.Drawing.Point(144, 388);
            this.labelViewState.Name = "labelViewState";
            this.labelViewState.Size = new System.Drawing.Size(33, 13);
            this.labelViewState.TabIndex = 2;
            this.labelViewState.Text = "None";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(455, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bell:";
            // 
            // labelBell
            // 
            this.labelBell.AutoSize = true;
            this.labelBell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelBell.Location = new System.Drawing.Point(488, 32);
            this.labelBell.Name = "labelBell";
            this.labelBell.Size = new System.Drawing.Size(33, 13);
            this.labelBell.TabIndex = 2;
            this.labelBell.Text = "Silent";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Line:";
            // 
            // labelLine
            // 
            this.labelLine.AutoSize = true;
            this.labelLine.BackColor = System.Drawing.Color.Red;
            this.labelLine.Location = new System.Drawing.Point(488, 65);
            this.labelLine.Name = "labelLine";
            this.labelLine.Size = new System.Drawing.Size(21, 13);
            this.labelLine.TabIndex = 2;
            this.labelLine.Text = "Off";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(455, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Receiver:";
            // 
            // labelReceiver
            // 
            this.labelReceiver.AutoSize = true;
            this.labelReceiver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelReceiver.Location = new System.Drawing.Point(514, 97);
            this.labelReceiver.Name = "labelReceiver";
            this.labelReceiver.Size = new System.Drawing.Size(35, 13);
            this.labelReceiver.TabIndex = 2;
            this.labelReceiver.Text = "Down";
            // 
            // Telephone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 423);
            this.Controls.Add(this.labelReceiver);
            this.Controls.Add(this.labelLine);
            this.Controls.Add(this.labelBell);
            this.Controls.Add(this.labelViewState);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReceiverUp);
            this.Controls.Add(this.buttonReceiverDown);
            this.Controls.Add(this.buttonInitiateCall);
            this.Name = "Telephone";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Telephone_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInitiateCall;
        private System.Windows.Forms.Button buttonReceiverDown;
        private System.Windows.Forms.Button buttonReceiverUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelViewState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBell;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelReceiver;
    }
}

