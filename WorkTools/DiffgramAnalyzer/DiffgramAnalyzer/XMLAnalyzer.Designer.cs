namespace DiffgramAnalyzer
{
    partial class XMLAnalyzer
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
            this.toolStripButtonParseDifgramm = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonParseDs = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStripButtonFormatXml = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonParseDifgramm,
            this.toolStripButtonParseDs,
            this.toolStripButtonFormatXml});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(651, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonParseDifgramm
            // 
            this.toolStripButtonParseDifgramm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonParseDifgramm.Image = global::DiffgramAnalyzer.Properties.Resources._1;
            this.toolStripButtonParseDifgramm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonParseDifgramm.Name = "toolStripButtonParseDifgramm";
            this.toolStripButtonParseDifgramm.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonParseDifgramm.Text = "Parse Diff";
            this.toolStripButtonParseDifgramm.Click += new System.EventHandler(this.toolStripButtonParse_Click);
            // 
            // toolStripButtonParseDs
            // 
            this.toolStripButtonParseDs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonParseDs.Image = global::DiffgramAnalyzer.Properties.Resources.WORD_PROCESSOR;
            this.toolStripButtonParseDs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonParseDs.Name = "toolStripButtonParseDs";
            this.toolStripButtonParseDs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonParseDs.Text = "Parse Dataset";
            this.toolStripButtonParseDs.Click += new System.EventHandler(this.toolStripButtonParseDs_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(651, 676);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // toolStripButtonFormatXml
            // 
            this.toolStripButtonFormatXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFormatXml.Image = global::DiffgramAnalyzer.Properties.Resources.reload;
            this.toolStripButtonFormatXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFormatXml.Name = "toolStripButtonFormatXml";
            this.toolStripButtonFormatXml.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonFormatXml.Text = "Format Xml";
            this.toolStripButtonFormatXml.Click += new System.EventHandler(this.toolStripButtonFormatXml_Click);
            // 
            // XMLAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 698);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "XMLAnalyzer";
            this.Text = "XML Analyzer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonParseDifgramm;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonParseDs;
        private System.Windows.Forms.ToolStripButton toolStripButtonFormatXml;
    }
}