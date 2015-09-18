using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    public partial class changePassword : Form
    {
        public changePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtConfirm.Text == txtNew.Text && clsStorage.currentClub.editUser(clsStorage.currentClub.strUser, txtOld.Text, txtNew.Text))
            {
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
                MessageBox.Show("Successfully changed password.");
                clsStorage.unsavedChanges = true;
                this.Close();
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Changing password was unsuccessful. Please confirm old password is correct.");
            }
        }
    }
}
