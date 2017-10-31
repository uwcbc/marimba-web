namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class editSettings : Form
    {
        public editSettings()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // preferences
            // store these on the user's machine
            Properties.Settings.Default.playSounds = cbSound.Checked;
            Properties.Settings.Default.selectCurrentTerm = cbCurTerm.Checked;
            Properties.Settings.Default.autoSave = cbAutoSave.Checked;
            Properties.Settings.Default.signatureName = txtName.Text;
            Properties.Settings.Default.signaturePosition = txtPosition.Text;
            Properties.Settings.Default.attachSig = cbAttachSignature.Checked;
            Properties.Settings.Default.emailPassword = txtEmailPassword.Text;
            ClsStorage.currentClub.clubEmail.ChangePassword(txtEmailPassword.Text);
            Properties.Settings.Default.Save();

            // admin settings
            // these should be stored with the club's file
            ClsStorage.currentClub.emailAddress = txtEmail.Text;
            ClsStorage.currentClub.imapServerAddress = txtImap.Text;
            ClsStorage.currentClub.bImap = cbSSL.Checked;
            ClsStorage.currentClub.smptServerAddress = txtSMTP.Text;
            ClsStorage.currentClub.smtpRequiresSSL = Convert.ToInt32(txtSMTPPort.Text);
            ClsStorage.currentClub.imapRequiresSSL = cbSMTP.Checked;

            if (Properties.Settings.Default.playSounds)
                Sound.Success.Play();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void editSettings_Load(object sender, EventArgs e)
        {
            cbSound.Checked = Properties.Settings.Default.playSounds;
            cbCurTerm.Checked = Properties.Settings.Default.selectCurrentTerm;
            cbAutoSave.Checked = Properties.Settings.Default.autoSave;
            txtName.Text = Properties.Settings.Default.signatureName;
            txtPosition.Text = Properties.Settings.Default.signaturePosition;
            cbAttachSignature.Checked = Properties.Settings.Default.attachSig;
            txtEmailPassword.Text = Properties.Settings.Default.emailPassword;

            txtEmail.Text = ClsStorage.currentClub.emailAddress;
            txtImap.Text = ClsStorage.currentClub.imapServerAddress;
            cbSSL.Checked = ClsStorage.currentClub.bImap;
            txtSMTP.Text = ClsStorage.currentClub.smptServerAddress;
            txtSMTPPort.Text = Convert.ToString(ClsStorage.currentClub.smtpRequiresSSL);
            cbSMTP.Checked = ClsStorage.currentClub.imapRequiresSSL;
            
            // when ready, email settings will go here

            // only admins have access to admin tab
            if (ClsStorage.currentClub.strCurrentUserPrivilege == "Exec")
                tpAdmin.Dispose();
        }

        private void cbAttachSignature_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = cbAttachSignature.Checked;
            txtPosition.Enabled = cbAttachSignature.Checked;
        }

        private void btnChangeKey_Click(object sender, EventArgs e)
        {
            ClsStorage.currentClub.UpdateKey();
            if (Properties.Settings.Default.playSounds)
                Sound.Success.Play();
        }
    }
}
