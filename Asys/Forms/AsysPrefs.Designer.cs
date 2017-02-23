namespace AsysEditor.Forms
{
    partial class AsysPrefs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsysPrefs));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkWelcome = new System.Windows.Forms.CheckBox();
            this.chkChangelog = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.chkWinSizeLoc = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AsysEditor.Properties.Resources.Asys_small_81x105;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Preferences";
            // 
            // chkWelcome
            // 
            this.chkWelcome.AutoSize = true;
            this.chkWelcome.Location = new System.Drawing.Point(12, 123);
            this.chkWelcome.Name = "chkWelcome";
            this.chkWelcome.Size = new System.Drawing.Size(194, 21);
            this.chkWelcome.TabIndex = 2;
            this.chkWelcome.Text = "Show Welcome on startup";
            this.chkWelcome.UseVisualStyleBackColor = true;
            // 
            // chkChangelog
            // 
            this.chkChangelog.AutoSize = true;
            this.chkChangelog.Location = new System.Drawing.Point(12, 150);
            this.chkChangelog.Name = "chkChangelog";
            this.chkChangelog.Size = new System.Drawing.Size(204, 21);
            this.chkChangelog.TabIndex = 3;
            this.chkChangelog.Text = "Show Changelog on startup";
            this.chkChangelog.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(267, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkWinSizeLoc
            // 
            this.chkWinSizeLoc.AutoSize = true;
            this.chkWinSizeLoc.Location = new System.Drawing.Point(13, 178);
            this.chkWinSizeLoc.Name = "chkWinSizeLoc";
            this.chkWinSizeLoc.Size = new System.Drawing.Size(228, 21);
            this.chkWinSizeLoc.TabIndex = 5;
            this.chkWinSizeLoc.Text = "Save window Location and Size";
            this.chkWinSizeLoc.UseVisualStyleBackColor = true;
            // 
            // AsysPrefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 218);
            this.Controls.Add(this.chkWinSizeLoc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkChangelog);
            this.Controls.Add(this.chkWelcome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsysPrefs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.AsysPrefs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkWelcome;
        private System.Windows.Forms.CheckBox chkChangelog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkWinSizeLoc;
    }
}