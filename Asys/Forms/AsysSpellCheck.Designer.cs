namespace AsysEditor.Forms
{
    partial class AsysSpellCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsysSpellCheck));
            this.lblLoading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMisspelledWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCorrection = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(12, 135);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(214, 17);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "Loading dictionary, please wait...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Found misspelled word:";
            // 
            // txtMisspelledWord
            // 
            this.txtMisspelledWord.Enabled = false;
            this.txtMisspelledWord.Location = new System.Drawing.Point(12, 29);
            this.txtMisspelledWord.Name = "txtMisspelledWord";
            this.txtMisspelledWord.Size = new System.Drawing.Size(257, 22);
            this.txtMisspelledWord.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Did you mean:";
            // 
            // txtCorrection
            // 
            this.txtCorrection.Location = new System.Drawing.Point(12, 74);
            this.txtCorrection.Name = "txtCorrection";
            this.txtCorrection.Size = new System.Drawing.Size(257, 22);
            this.txtCorrection.TabIndex = 4;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(190, 103);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(79, 29);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 103);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(79, 29);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // AsysSpellCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 162);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtCorrection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMisspelledWord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsysSpellCheck";
            this.Text = "Spell Checker";
            this.Load += new System.EventHandler(this.AsysSpellCheck_Load);
            this.Shown += new System.EventHandler(this.AsysSpellCheck_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMisspelledWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCorrection;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnRefresh;
    }
}