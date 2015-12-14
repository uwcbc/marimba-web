using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marimba
{
    public partial class editSettings : Form
    {
        public editSettings()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //preferences
            //store these on the user's machine
            Properties.Settings.Default.playSounds = cbSound.Checked;
            Properties.Settings.Default.selectCurrentTerm = cbCurTerm.Checked;
            Properties.Settings.Default.autoSave = cbAutoSave.Checked;
            Properties.Settings.Default.signatureName = txtName.Text;
            Properties.Settings.Default.signaturePosition = txtPosition.Text;
            Properties.Settings.Default.attachSig = cbAttachSignature.Checked;
            Properties.Settings.Default.emailPassword = txtEmailPassword.Text;
            clsStorage.currentClub.clubEmail.changePassword(txtEmailPassword.Text);
            Properties.Settings.Default.Save();

            //admin settings
            //these should be stored with the club's file
            clsStorage.currentClub.strEmail = txtEmail.Text;
            clsStorage.currentClub.strImap = txtImap.Text;
            clsStorage.currentClub.bImap = cbSSL.Checked;
            clsStorage.currentClub.strSmtp = txtSMTP.Text;
            clsStorage.currentClub.iSmtp = Convert.ToInt32(txtSMTPPort.Text);
            clsStorage.currentClub.bSmtp = cbSMTP.Checked;

            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
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

            txtEmail.Text = clsStorage.currentClub.strEmail;
            txtImap.Text = clsStorage.currentClub.strImap;
            cbSSL.Checked = clsStorage.currentClub.bImap;
            txtSMTP.Text = clsStorage.currentClub.strSmtp;
            txtSMTPPort.Text = Convert.ToString(clsStorage.currentClub.iSmtp);
            cbSMTP.Checked = clsStorage.currentClub.bSmtp;
            
            //when ready, email settings will go here

            //only admins have access to admin tab
            if (clsStorage.currentClub.strCurrentUserPrivilege == "Exec")
                tpAdmin.Dispose();
        }

        private void cbAttachSignature_CheckedChanged(object sender, EventArgs e)
        {
            txtName.Enabled = cbAttachSignature.Checked;
            txtPosition.Enabled = cbAttachSignature.Checked;
        }

        private void btnChangeKey_Click(object sender, EventArgs e)
        {
            clsStorage.currentClub.updateKey();
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
        }
    }
}
