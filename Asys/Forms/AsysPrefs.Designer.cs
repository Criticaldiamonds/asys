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
            this.btnApply = new System.Windows.Forms.Button();
            this.chkWinSizeLoc = new System.Windows.Forms.CheckBox();
            this.chkDisableUpdater = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AsysEditor.Properties.Resources.Asys_small_81x105;
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Preferences";
            // 
            // chkWelcome
            // 
            this.chkWelcome.AutoSize = true;
            this.chkWelcome.Location = new System.Drawing.Point(9, 100);
            this.chkWelcome.Margin = new System.Windows.Forms.Padding(2);
            this.chkWelcome.Name = "chkWelcome";
            this.chkWelcome.Size = new System.Drawing.Size(151, 17);
            this.chkWelcome.TabIndex = 2;
            this.chkWelcome.Text = "Show Welcome on startup";
            this.chkWelcome.UseVisualStyleBackColor = true;
            // 
            // chkChangelog
            // 
            this.chkChangelog.AutoSize = true;
            this.chkChangelog.Location = new System.Drawing.Point(9, 122);
            this.chkChangelog.Margin = new System.Windows.Forms.Padding(2);
            this.chkChangelog.Name = "chkChangelog";
            this.chkChangelog.Size = new System.Drawing.Size(157, 17);
            this.chkChangelog.TabIndex = 3;
            this.chkChangelog.Text = "Show Changelog on startup";
            this.chkChangelog.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(189, 184);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(61, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkWinSizeLoc
            // 
            this.chkWinSizeLoc.AutoSize = true;
            this.chkWinSizeLoc.Location = new System.Drawing.Point(10, 145);
            this.chkWinSizeLoc.Margin = new System.Windows.Forms.Padding(2);
            this.chkWinSizeLoc.Name = "chkWinSizeLoc";
            this.chkWinSizeLoc.Size = new System.Drawing.Size(178, 17);
            this.chkWinSizeLoc.TabIndex = 5;
            this.chkWinSizeLoc.Text = "Save window Location and Size";
            this.chkWinSizeLoc.UseVisualStyleBackColor = true;
            // 
            // chkDisableUpdater
            // 
            this.chkDisableUpdater.AutoSize = true;
            this.chkDisableUpdater.Location = new System.Drawing.Point(9, 168);
            this.chkDisableUpdater.Name = "chkDisableUpdater";
            this.chkDisableUpdater.Size = new System.Drawing.Size(139, 17);
            this.chkDisableUpdater.TabIndex = 6;
            this.chkDisableUpdater.Text = "Disable update checker";
            this.chkDisableUpdater.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(154, 169);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Check for Updates";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AsysPrefs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 217);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkDisableUpdater);
            this.Controls.Add(this.chkWinSizeLoc);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.chkChangelog);
            this.Controls.Add(this.chkWelcome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chkWinSizeLoc;
        private System.Windows.Forms.CheckBox chkDisableUpdater;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}