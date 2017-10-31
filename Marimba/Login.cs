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
    using System.IO;

    public partial class Login : Form
    {
        string strLocation = String.Empty;

        public Login(string strLocation)
        {
            InitializeComponent();
            this.strLocation = strLocation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // if we opened from a file
                string check = Properties.Settings.Default.FileLocation;
                if (strLocation != String.Empty)
                {
                    ClsStorage.currentClub = new Club(strLocation);
                    ClsStorage.currentClub.LoadClub();
                    lblFile.Text = "File: " + strLocation;
                }
                else if (Properties.Settings.Default.FileLocation == null || Properties.Settings.Default.FileLocation == String.Empty ||
                    !File.Exists(Properties.Settings.Default.FileLocation))
                {
                    // if we have no user preferences or our user preferences point to a file that can't be found
                    if (ofdOpen.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.FileLocation = ofdOpen.FileName;
                        ClsStorage.currentClub = new Club(ofdOpen.FileName);
                        ClsStorage.currentClub.LoadClub();
                        lblFile.Text = "File: " + ofdOpen.FileName;
                    }
                    else
                        this.Close();

                }
                else
                {
                    ClsStorage.currentClub = new Club(Properties.Settings.Default.FileLocation);
                    ClsStorage.currentClub.LoadClub();
                    lblFile.Text = "File: " + Properties.Settings.Default.FileLocation;
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("The file you are trying to load is currently in use. The file cannot be loaded right now.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ClsStorage.currentClub.LoginUser(txtID.Text, txtPassword.Text))
            {
                ClsStorage.currentClub.LoadEncryptedSection();
                Program.home.ribbon1 = new Marimba.Ribbon();
                Program.home.elementHost1.Child = Program.home.ribbon1;
                Program.home.moneyMenu.newfees.cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());
                Program.home.Visible = false;
                // this line makes Marimba wait until it is fully loaded
                Application.DoEvents();
                if (Properties.Settings.Default.playSounds)
                    Sound.Welcome.Play();
                ClsStorage.loggedin = true;
                this.Close();
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Login failed. Please check credentials.");
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdOpen.ShowDialog() == DialogResult.OK)
                {
                    ClsStorage.currentClub = new Club(ofdOpen.FileName);
                    ClsStorage.currentClub.LoadClub();
                    Properties.Settings.Default.FileLocation = ofdOpen.FileName;
                    lblFile.Text = "File: " + Properties.Settings.Default.FileLocation;
                    if (Properties.Settings.Default.playSounds)
                        Sound.Click.Play();
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("The file you are trying to load is currently in use. The file cannot be loaded right now.");
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            ClsStorage.loggedin = true;
            this.Close();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            // check if a club was loaded
            // if so, close the filestream
            if (lblFile.Text != "File: " && !ClsStorage.loggedin)
                ClsStorage.currentClub.br.Close();
        }
    }
}
