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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version of Asys is avaliable!";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Update Asys now to recieve the newest features and bugfixes.";
            // 
            // lblCurVer
            // 
            this.lblCurVer.AutoSize = true;
            this.lblCurVer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurVer.Location = new System.Drawing.Point(12, 106);
            this.lblCurVer.Name = "lblCurVer";
            this.lblCurVer.Size = new System.Drawing.Size(191, 24);
            this.lblCurVer.TabIndex = 2;
            this.lblCurVer.Text = "Current Version: 0.0.0";
            // 
            // lblNewVer
            // 
            this.lblNewVer.AutoSize = true;
            this.lblNewVer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVer.Location = new System.Drawing.Point(12, 130);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(164, 24);
            this.lblNewVer.TabIndex = 3;
            this.lblNewVer.Text = "New Version: 1.1.1";
            // 
            // lnkDownloadNow
            // 
            this.lnkDownloadNow.AutoSize = true;
            this.lnkDownloadNow.LinkColor = System.Drawing.Color.Blue;
            this.lnkDownloadNow.Location = new System.Drawing.Point(9, 176);
            this.lnkDownloadNow.Name = "lnkDownloadNow";
            this.lnkDownloadNow.Size = new System.Drawing.Size(101, 17);
            this.lnkDownloadNow.TabIndex = 4;
            this.lnkDownloadNow.TabStop = true;
            this.lnkDownloadNow.Text = "Download Now";
            this.lnkDownloadNow.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkDownloadNow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadNow_LinkClicked);
            // 
            // btnWait
            // 
            this.btnWait.Location = new System.Drawing.Point(309, 176);
            this.btnWait.Name = "btnWait";
            this.btnWait.Size = new System.Drawing.Size(98, 42);
            this.btnWait.TabIndex = 5;
            this.btnWait.Text = "Not Now";
            this.btnWait.UseVisualStyleBackColor = true;
            this.btnWait.Click += new System.EventHandler(this.btnWait_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 197);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(295, 21);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(116, 176);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(184, 21);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Run Installer when Done";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 154);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(133, 17);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View the Changelog";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AsysNewVerAvaliable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 226);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnWait);
            this.Controls.Add(this.lnkDownloadNow);
            this.Controls.Add(this.lblNewVer);
            this.Controls.Add(this.lblCurVer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}