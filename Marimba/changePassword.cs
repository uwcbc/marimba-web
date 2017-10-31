namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class changePassword : Form
    {
        public changePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtConfirm.Text == txtNew.Text && ClsStorage.currentClub.EditUser(ClsStorage.currentClub.strCurrentUser, txtOld.Text, txtNew.Text))
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
                MessageBox.Show("Successfully changed password.");
                ClsStorage.unsavedChanges = true;
                this.Close();
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                string errorMessage = "Changing password was unsuccessful. ";
                if (txtConfirm.Text != txtNew.Text)
                {
                    errorMessage += "Please confirm that you have typed in your new password correctly.";
                }
                else
                {
                    errorMessage += "Please confirm that Please confirm old password is correct.";
                }
                MessageBox.Show(errorMessage);
            }
        }
    }
}
