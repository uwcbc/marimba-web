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
    public partial class frmEditUser : Form
    {

        public frmEditUser()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // for now all we should be able to edit is their privilege
            // should change to something else if we record more user info
            bool result = clsStorage.currentClub.editUserPrivilege(cboUserID.Text, cboPrivileges.Text);
            if (result)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
                MessageBox.Show("User successfully edited.");
                clsStorage.currentClub.addHistory(cboUserID.Text, history.changeType.editUser);

                cboUserID.Text = "";
                cboPrivileges.Text = "";
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("That user does not exist. Please make sure you have selected a valid user.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you user you want to delete the user " + cboUserID.Text + "?", "Delete this User?", MessageBoxButtons.YesNo)
                 == DialogResult.Yes)
            {
                bool result = clsStorage.currentClub.deleteUser(cboUserID.Text);
                if (result)
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    MessageBox.Show("User successfully deleted.");
                    clsStorage.currentClub.addHistory(cboUserID.Text, history.changeType.deleteUser);

                    cboUserID.Items.Remove(cboUserID.Text);
                    cboUserID.Text = "";
                    cboPrivileges.Text = "";
                }
                else
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("That user does not exist. Please make sure you have selected a valid user.");
                }
            }
        }

        private void cboUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUserID.SelectedIndex == -1)
            {
                return;
            }

            string[] user = clsStorage.currentClub.findUser(cboUserID.Text);
            if (user == null)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("That user does not exist. Please make sure you have selected a valid user.");
            }

            cboPrivileges.Text = user[2];
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            foreach (string[] user in clsStorage.currentClub.strUsers)
            {
                cboUserID.Items.Add(user[0]);
            }
        }

        private void frmEditUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            cboUserID.SelectedIndex = -1;
            cboUserID.Items.Clear();
            cboPrivileges.SelectedIndex = -1;
        }
    }
}
