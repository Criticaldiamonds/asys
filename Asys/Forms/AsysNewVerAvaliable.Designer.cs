namespace AsysEditor.Forms
{
    partial class AsysNewVerAvaliable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsysNewVerAvaliable));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurVer = new System.Windows.Forms.Label();
            this.lblNewVer = new System.Windows.Forms.Label();
            this.lnkDownloadNow = new System.Windows.Forms.LinkLabel();
            this.btnWait = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.chkSkip = new System.Windows.Forms.CheckBox();
            this.chkNever = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version of Asys is avaliable!";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Update Asys now to recieve the newest features and bugfixes.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurVer
            // 
            this.lblCurVer.AutoSize = true;
            this.lblCurVer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurVer.Location = new System.Drawing.Point(11, 79);
            this.lblCurVer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurVer.Name = "lblCurVer";
            this.lblCurVer.Size = new System.Drawing.Size(149, 19);
            this.lblCurVer.TabIndex = 2;
            this.lblCurVer.Text = "Current Version: 0.0.0";
            // 
            // lblNewVer
            // 
            this.lblNewVer.AutoSize = true;
            this.lblNewVer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVer.Location = new System.Drawing.Point(11, 98);
            this.lblNewVer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(130, 19);
            this.lblNewVer.TabIndex = 3;
            this.lblNewVer.Text = "New Version: 1.1.1";
            // 
            // lnkDownloadNow
            // 
            this.lnkDownloadNow.AutoSize = true;
            this.lnkDownloadNow.LinkColor = System.Drawing.Color.Blue;
            this.lnkDownloadNow.Location = new System.Drawing.Point(118, 127);
            this.lnkDownloadNow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkDownloadNow.Name = "lnkDownloadNow";
            this.lnkDownloadNow.Size = new System.Drawing.Size(67, 13);
            this.lnkDownloadNow.TabIndex = 4;
            this.lnkDownloadNow.TabStop = true;
            this.lnkDownloadNow.Text = "Update Now";
            this.lnkDownloadNow.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkDownloadNow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadNow_LinkClicked);
            // 
            // btnWait
            // 
            this.btnWait.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWait.Location = new System.Drawing.Point(249, 138);
            this.btnWait.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnWait.Name = "btnWait";
            this.btnWait.Size = new System.Drawing.Size(131, 28);
            this.btnWait.TabIndex = 5;
            this.btnWait.Text = "Remind me later";
            this.btnWait.UseVisualStyleBackColor = true;
            this.btnWait.Click += new System.EventHandler(this.btnWait_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 149);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(234, 17);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 127);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(102, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View the Changelog";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // chkSkip
            // 
            this.chkSkip.AutoSize = true;
            this.chkSkip.Location = new System.Drawing.Point(249, 101);
            this.chkSkip.Name = "chkSkip";
            this.chkSkip.Size = new System.Drawing.Size(103, 17);
            this.chkSkip.TabIndex = 9;
            this.chkSkip.Text = "Skip this version";
            this.chkSkip.UseVisualStyleBackColor = true;
            // 
            // chkNever
            // 
            this.chkNever.AutoSize = true;
            this.chkNever.Location = new System.Drawing.Point(249, 116);
            this.chkNever.Name = "chkNever";
            this.chkNever.Size = new System.Drawing.Size(140, 17);
            this.chkNever.TabIndex = 10;
            this.chkNever.Text = "Don\'t check for updates";
            this.chkNever.UseVisualStyleBackColor = true;
            // 
            // AsysNewVerAvaliable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 184);
            this.Controls.Add(this.chkNever);
            this.Controls.Add(this.chkSkip);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnWait);
            this.Controls.Add(this.lnkDownloadNow);
            this.Controls.Add(this.lblNewVer);
            this.Controls.Add(this.lblCurVer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AsysNewVerAvaliable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asys Updater";
            this.Load += new System.EventHandler(this.AsysNewVerAvaliable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurVer;
        private System.Windows.Forms.Label lblNewVer;
        private System.Windows.Forms.LinkLabel lnkDownloadNow;
        private System.Windows.Forms.Button btnWait;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkSkip;
        private System.Windows.Forms.CheckBox chkNever;
    }
}