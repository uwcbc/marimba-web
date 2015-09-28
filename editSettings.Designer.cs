namespace Marimba
{
    partial class editSettings
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpPref = new System.Windows.Forms.TabPage();
            this.lblEmailPassword = new System.Windows.Forms.Label();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbAttachSignature = new System.Windows.Forms.CheckBox();
            this.cbAutoSave = new System.Windows.Forms.CheckBox();
            this.cbCurTerm = new System.Windows.Forms.CheckBox();
            this.cbSound = new System.Windows.Forms.CheckBox();
            this.tpAdmin = new System.Windows.Forms.TabPage();
            this.gbEncryption = new System.Windows.Forms.GroupBox();
            this.btnChangeKey = new System.Windows.Forms.Button();
            this.gbEmail = new System.Windows.Forms.GroupBox();
            this.cbSMTP = new System.Windows.Forms.CheckBox();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.lblSMTPPort = new System.Windows.Forms.Label();
            this.txtSMTP = new System.Windows.Forms.TextBox();
            this.lblSMTP = new System.Windows.Forms.Label();
            this.cbSSL = new System.Windows.Forms.CheckBox();
            this.txtImap = new System.Windows.Forms.TextBox();
            this.lblImap = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpPref.SuspendLayout();
            this.tpAdmin.SuspendLayout();
            this.gbEncryption.SuspendLayout();
            this.gbEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpPref);
            this.tcMain.Controls.Add(this.tpAdmin);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcMain.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(504, 412);
            this.tcMain.TabIndex = 0;
            // 
            // tpPref
            // 
            this.tpPref.Controls.Add(this.lblEmailPassword);
            this.tpPref.Controls.Add(this.txtEmailPassword);
            this.tpPref.Controls.Add(this.lblPosition);
            this.tpPref.Controls.Add(this.lblName);
            this.tpPref.Controls.Add(this.txtPosition);
            this.tpPref.Controls.Add(this.txtName);
            this.tpPref.Controls.Add(this.cbAttachSignature);
            this.tpPref.Controls.Add(this.cbAutoSave);
            this.tpPref.Controls.Add(this.cbCurTerm);
            this.tpPref.Controls.Add(this.cbSound);
            this.tpPref.Location = new System.Drawing.Point(4, 24);
            this.tpPref.Name = "tpPref";
            this.tpPref.Padding = new System.Windows.Forms.Padding(3);
            this.tpPref.Size = new System.Drawing.Size(496, 384);
            this.tpPref.TabIndex = 0;
            this.tpPref.Text = "Preferences";
            this.tpPref.UseVisualStyleBackColor = true;
            // 
            // lblEmailPassword
            // 
            this.lblEmailPassword.AutoSize = true;
            this.lblEmailPassword.Location = new System.Drawing.Point(30, 292);
            this.lblEmailPassword.Name = "lblEmailPassword";
            this.lblEmailPassword.Size = new System.Drawing.Size(116, 15);
            this.lblEmailPassword.TabIndex = 9;
            this.lblEmailPassword.Text = "E-mail Password:";
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.Location = new System.Drawing.Point(167, 289);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.PasswordChar = '♪';
            this.txtEmailPassword.Size = new System.Drawing.Size(304, 23);
            this.txtEmailPassword.TabIndex = 8;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(30, 245);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(61, 15);
            this.lblPosition.TabIndex = 7;
            this.lblPosition.Text = "Position:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 204);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 15);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Name:";
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(130, 242);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(341, 23);
            this.txtPosition.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(130, 201);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(341, 23);
            this.txtName.TabIndex = 4;
            // 
            // cbAttachSignature
            // 
            this.cbAttachSignature.AutoSize = true;
            this.cbAttachSignature.Location = new System.Drawing.Point(33, 163);
            this.cbAttachSignature.Name = "cbAttachSignature";
            this.cbAttachSignature.Size = new System.Drawing.Size(278, 19);
            this.cbAttachSignature.TabIndex = 3;
            this.cbAttachSignature.Text = "Attach signature when sending e-mails";
            this.cbAttachSignature.UseVisualStyleBackColor = true;
            this.cbAttachSignature.CheckedChanged += new System.EventHandler(this.cbAttachSignature_CheckedChanged);
            // 
            // cbAutoSave
            // 
            this.cbAutoSave.AutoSize = true;
            this.cbAutoSave.Location = new System.Drawing.Point(33, 119);
            this.cbAutoSave.Name = "cbAutoSave";
            this.cbAutoSave.Size = new System.Drawing.Size(342, 19);
            this.cbAutoSave.TabIndex = 2;
            this.cbAutoSave.Text = "Automatically save file after any change is made";
            this.cbAutoSave.UseVisualStyleBackColor = true;
            // 
            // cbCurTerm
            // 
            this.cbCurTerm.AutoSize = true;
            this.cbCurTerm.Location = new System.Drawing.Point(33, 73);
            this.cbCurTerm.Name = "cbCurTerm";
            this.cbCurTerm.Size = new System.Drawing.Size(244, 19);
            this.cbCurTerm.TabIndex = 1;
            this.cbCurTerm.Text = "Select current term automatically";
            this.cbCurTerm.UseVisualStyleBackColor = true;
            // 
            // cbSound
            // 
            this.cbSound.AutoSize = true;
            this.cbSound.Location = new System.Drawing.Point(33, 29);
            this.cbSound.Name = "cbSound";
            this.cbSound.Size = new System.Drawing.Size(113, 19);
            this.cbSound.TabIndex = 0;
            this.cbSound.Text = "Enable sound";
            this.cbSound.UseVisualStyleBackColor = true;
            // 
            // tpAdmin
            // 
            this.tpAdmin.Controls.Add(this.gbEncryption);
            this.tpAdmin.Controls.Add(this.gbEmail);
            this.tpAdmin.Location = new System.Drawing.Point(4, 24);
            this.tpAdmin.Name = "tpAdmin";
            this.tpAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdmin.Size = new System.Drawing.Size(496, 384);
            this.tpAdmin.TabIndex = 1;
            this.tpAdmin.Text = "Admin";
            this.tpAdmin.UseVisualStyleBackColor = true;
            // 
            // gbEncryption
            // 
            this.gbEncryption.Controls.Add(this.btnChangeKey);
            this.gbEncryption.Location = new System.Drawing.Point(8, 254);
            this.gbEncryption.Name = "gbEncryption";
            this.gbEncryption.Size = new System.Drawing.Size(482, 100);
            this.gbEncryption.TabIndex = 1;
            this.gbEncryption.TabStop = false;
            this.gbEncryption.Text = "Encryption";
            // 
            // btnChangeKey
            // 
            this.btnChangeKey.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeKey.Location = new System.Drawing.Point(175, 43);
            this.btnChangeKey.Name = "btnChangeKey";
            this.btnChangeKey.Size = new System.Drawing.Size(133, 23);
            this.btnChangeKey.TabIndex = 3;
            this.btnChangeKey.Text = "Change Key";
            this.btnChangeKey.UseVisualStyleBackColor = true;
            this.btnChangeKey.Click += new System.EventHandler(this.btnChangeKey_Click);
            // 
            // gbEmail
            // 
            this.gbEmail.Controls.Add(this.cbSMTP);
            this.gbEmail.Controls.Add(this.txtSMTPPort);
            this.gbEmail.Controls.Add(this.lblSMTPPort);
            this.gbEmail.Controls.Add(this.txtSMTP);
            this.gbEmail.Controls.Add(this.lblSMTP);
            this.gbEmail.Controls.Add(this.cbSSL);
            this.gbEmail.Controls.Add(this.txtImap);
            this.gbEmail.Controls.Add(this.lblImap);
            this.gbEmail.Controls.Add(this.lblEmail);
            this.gbEmail.Controls.Add(this.txtEmail);
            this.gbEmail.Location = new System.Drawing.Point(8, 6);
            this.gbEmail.Name = "gbEmail";
            this.gbEmail.Size = new System.Drawing.Size(482, 241);
            this.gbEmail.TabIndex = 0;
            this.gbEmail.TabStop = false;
            this.gbEmail.Text = "Email";
            // 
            // cbSMTP
            // 
            this.cbSMTP.AutoSize = true;
            this.cbSMTP.Checked = true;
            this.cbSMTP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSMTP.Location = new System.Drawing.Point(6, 211);
            this.cbSMTP.Name = "cbSMTP";
            this.cbSMTP.Size = new System.Drawing.Size(178, 19);
            this.cbSMTP.TabIndex = 16;
            this.cbSMTP.Text = "SSL Required for SMTP";
            this.cbSMTP.UseVisualStyleBackColor = true;
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.Location = new System.Drawing.Point(146, 173);
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(330, 23);
            this.txtSMTPPort.TabIndex = 15;
            // 
            // lblSMTPPort
            // 
            this.lblSMTPPort.AutoSize = true;
            this.lblSMTPPort.Location = new System.Drawing.Point(6, 176);
            this.lblSMTPPort.Name = "lblSMTPPort";
            this.lblSMTPPort.Size = new System.Drawing.Size(77, 15);
            this.lblSMTPPort.TabIndex = 14;
            this.lblSMTPPort.Text = "SMTP Port:";
            // 
            // txtSMTP
            // 
            this.txtSMTP.Location = new System.Drawing.Point(146, 134);
            this.txtSMTP.Name = "txtSMTP";
            this.txtSMTP.Size = new System.Drawing.Size(330, 23);
            this.txtSMTP.TabIndex = 13;
            // 
            // lblSMTP
            // 
            this.lblSMTP.AutoSize = true;
            this.lblSMTP.Location = new System.Drawing.Point(6, 137);
            this.lblSMTP.Name = "lblSMTP";
            this.lblSMTP.Size = new System.Drawing.Size(95, 15);
            this.lblSMTP.TabIndex = 12;
            this.lblSMTP.Text = "SMTP Server:";
            // 
            // cbSSL
            // 
            this.cbSSL.AutoSize = true;
            this.cbSSL.Checked = true;
            this.cbSSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSSL.Location = new System.Drawing.Point(9, 100);
            this.cbSSL.Name = "cbSSL";
            this.cbSSL.Size = new System.Drawing.Size(173, 19);
            this.cbSSL.TabIndex = 11;
            this.cbSSL.Text = "SSL Required for IMAP";
            this.cbSSL.UseVisualStyleBackColor = true;
            // 
            // txtImap
            // 
            this.txtImap.Location = new System.Drawing.Point(146, 58);
            this.txtImap.Name = "txtImap";
            this.txtImap.Size = new System.Drawing.Size(330, 23);
            this.txtImap.TabIndex = 10;
            // 
            // lblImap
            // 
            this.lblImap.AutoSize = true;
            this.lblImap.Location = new System.Drawing.Point(6, 61);
            this.lblImap.Name = "lblImap";
            this.lblImap.Size = new System.Drawing.Size(90, 15);
            this.lblImap.TabIndex = 9;
            this.lblImap.Text = "IMAP Server:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(6, 24);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(105, 15);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "E-Mail Address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(146, 21);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(330, 23);
            this.txtEmail.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(134, 429);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(274, 429);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // editSettings
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(504, 464);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "editSettings";
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.editSettings_Load);
            this.tcMain.ResumeLayout(false);
            this.tpPref.ResumeLayout(false);
            this.tpPref.PerformLayout();
            this.tpAdmin.ResumeLayout(false);
            this.gbEncryption.ResumeLayout(false);
            this.gbEmail.ResumeLayout(false);
            this.gbEmail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpPref;
        private System.Windows.Forms.CheckBox cbSound;
        private System.Windows.Forms.TabPage tpAdmin;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbCurTerm;
        private System.Windows.Forms.CheckBox cbAutoSave;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox cbAttachSignature;
        private System.Windows.Forms.Label lblEmailPassword;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.GroupBox gbEmail;
        private System.Windows.Forms.TextBox txtImap;
        private System.Windows.Forms.Label lblImap;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.CheckBox cbSSL;
        private System.Windows.Forms.CheckBox cbSMTP;
        private System.Windows.Forms.TextBox txtSMTPPort;
        private System.Windows.Forms.Label lblSMTPPort;
        private System.Windows.Forms.TextBox txtSMTP;
        private System.Windows.Forms.Label lblSMTP;
        private System.Windows.Forms.GroupBox gbEncryption;
        private System.Windows.Forms.Button btnChangeKey;
    }
}