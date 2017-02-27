namespace AsysEditor.Forms
{
    partial class AsysChangelogViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsysChangelogViewer));
            this.richTextBoxPrintCtrl1 = new AsysControls.RichTextBoxPrintCtrl();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxPrintCtrl1
            // 
            this.richTextBoxPrintCtrl1.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxPrintCtrl1.Name = "richTextBoxPrintCtrl1";
            this.richTextBoxPrintCtrl1.ReadOnly = true;
            this.richTextBoxPrintCtrl1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxPrintCtrl1.Size = new System.Drawing.Size(534, 282);
            this.richTextBoxPrintCtrl1.TabIndex = 0;
            this.richTextBoxPrintCtrl1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AsysChangelogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 343);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBoxPrintCtrl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AsysChangelogViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asys Changelog";
            this.ResumeLayout(false);

        }

        #endregion

        private AsysControls.RichTextBoxPrintCtrl richTextBoxPrintCtrl1;
        private System.Windows.Forms.Button button1;
    }
}