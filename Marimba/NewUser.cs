using Marimba.Utility;
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
    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "")
                MessageBox.Show("Please enter a User ID");
            else if (txtPassword.Text == "" || txtConfirm.Text == "")
                MessageBox.Show("Please enter a password");
            else if (cbPrivileges.Text == "")
                MessageBox.Show("Please select user privileges");
            else if (txtPassword.Text != txtConfirm.Text)
                MessageBox.Show("The passwords entered do not match.");
            else
            {
                if (clsStorage.currentClub.addUser(txtUserID.Text, txtPassword.Text, cbPrivileges.Text))
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    MessageBox.Show("User successfully added");
                    clsStorage.currentClub.addHistory(txtUserID.Text, Enumerations.ChangeType.AddUser);
                    this.Close();
                }
                else
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("A user with this name already exists. Please choose another name");
                }
            }
        }
    }
}
