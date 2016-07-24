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

    using Marimba.Utility;

    public partial class NewUser : Form
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == String.Empty)
                MessageBox.Show("Please enter a User ID");
            else if (txtPassword.Text == String.Empty || txtConfirm.Text == String.Empty)
                MessageBox.Show("Please enter a password");
            else if (cbPrivileges.Text == String.Empty)
                MessageBox.Show("Please select user privileges");
            else if (txtPassword.Text != txtConfirm.Text)
                MessageBox.Show("The passwords entered do not match.");
            else
            {
                if (ClsStorage.currentClub.AddUser(txtUserID.Text, txtPassword.Text, cbPrivileges.Text))
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                    MessageBox.Show("User successfully added");
                    ClsStorage.currentClub.AddHistory(txtUserID.Text, ChangeType.AddUser);
                    this.Close();
                }
                else
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("A user with this name already exists. Please choose another name");
                }
            }
        }
    }
}
