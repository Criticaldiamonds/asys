namespace AsysEditor.Forms
{
    partial class AsysDevMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AsysDevMsg));
            this.picAsysLogo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.chkDontShow = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lnkHelp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picAsysLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picAsysLogo
            // 
            this.picAsysLogo.Image = global::AsysEditor.Properties.Resources.Asys_full_151x259;
            this.picAsysLogo.Location = new System.Drawing.Point(7, 12);
            this.picAsysLogo.Name = "picAsysLogo";
            this.picAsysLogo.Size = new System.Drawing.Size(100, 208);
            this.picAsysLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAsysLogo.TabIndex = 0;
            this.picAsysLogo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Message from the developer:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(118, 45);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(333, 136);
            this.txtMessage.TabIndex = 9;
            // 
            // chkDontShow
            // 
            this.chkDontShow.AutoSize = true;
            this.chkDontShow.Location = new System.Drawing.Point(118, 187);
            this.chkDontShow.Name = "chkDontShow";
            this.chkDontShow.Size = new System.Drawing.Size(225, 21);
            this.chkDontShow.TabIndex = 3;
            this.chkDontShow.Text = "Don\'t show &this message again";
            this.chkDontShow.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(371, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 33);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lnkHelp
            // 
            this.lnkHelp.AutoSize = true;
            this.lnkHelp.Location = new System.Drawing.Point(337, 188);
            this.lnkHelp.Name = "lnkHelp";
            this.lnkHelp.Size = new System.Drawing.Size(16, 17);
            this.lnkHelp.TabIndex = 5;
            this.lnkHelp.TabStop = true;
            this.lnkHelp.Text = "?";
            this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
            // 
            // AsysDevMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 232);
            this.Controls.Add(this.lnkHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkDontShow);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picAsysLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsysDevMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asys";
            this.Load += new System.EventHandler(this.AsysDevMsg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAsysLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picAsysLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.CheckBox chkDontShow;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkHelp;
    }
}