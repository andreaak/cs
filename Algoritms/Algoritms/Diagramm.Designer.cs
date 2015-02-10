namespace Algoritms
{
    partial class Diagramm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonShowParameters = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxScale = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonShowParameters,
            this.toolStripLabel1,
            this.toolStripTextBoxScale});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonShowParameters
            // 
            this.toolStripButtonShowParameters.CheckOnClick = true;
            this.toolStripButtonShowParameters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowParameters.Image = global::Algoritms.Properties.Resources.WORD_PROCESSOR;
            this.toolStripButtonShowParameters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowParameters.Name = "toolStripButtonShowParameters";
            this.toolStripButtonShowParameters.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowParameters.Text = "Show Parameters";
            this.toolStripButtonShowParameters.CheckedChanged += new System.EventHandler(this.toolStripButtonShowParameters_CheckedChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(36, 22);
            this.toolStripLabel1.Text = "Scale:";
            // 
            // toolStripTextBoxScale
            // 
            this.toolStripTextBoxScale.Enabled = false;
            this.toolStripTextBoxScale.Name = "toolStripTextBoxScale";
            this.toolStripTextBoxScale.Size = new System.Drawing.Size(25, 25);
            this.toolStripTextBoxScale.Text = "100";
            // 
            // Diagramm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 535);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Diagramm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diagramm";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ShowMacrosPicture_MouseWheel);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Diagramm_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Diagramm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Diagramm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Diagramm_MouseMove);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowParameters;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxScale;
    }
}