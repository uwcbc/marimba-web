using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Marimba
{
    public partial class Login : Form
    {
        string strLocation = "";
        public Login(string strLocation)
        {
            InitializeComponent();
            this.strLocation = strLocation;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //if we opened from a file
                string check = Properties.Settings.Default.FileLocation;
                if (strLocation != "")
                {
                    clsStorage.currentClub = new club(strLocation);
                    clsStorage.currentClub.loadClub();
                    lblFile.Text = "File: " + strLocation;
                }
                //if we have no user preferences or our user preferences point to a file that can't be found
                else if (Properties.Settings.Default.FileLocation == null || Properties.Settings.Default.FileLocation == "" ||
                    !File.Exists(Properties.Settings.Default.FileLocation))
                {
                    if (ofdOpen.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.FileLocation = ofdOpen.FileName;
                        clsStorage.currentClub = new club(ofdOpen.FileName);
                        clsStorage.currentClub.loadClub();
                        lblFile.Text = "File: " + ofdOpen.FileName;
                    }
                    else
                        this.Close();

                }
                else
                {
                    clsStorage.currentClub = new club(Properties.Settings.Default.FileLocation);
                    clsStorage.currentClub.loadClub();
                    lblFile.Text = "File: " + Properties.Settings.Default.FileLocation;
                }
            }
            catch(System.IO.IOException)
            {
                MessageBox.Show("The file you are trying to load is currently in use. The file cannot be loaded right now.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsStorage.currentClub.loginUser(txtID.Text, txtPassword.Text))
            {
                clsStorage.currentClub.loadEncryptedSection();
                Program.home.ribbon1 = new Marimba.Ribbon();
                Program.home.elementHost1.Child = Program.home.ribbon1;
                Program.home.moneyMenu.newfees.cbTerm.Items.AddRange(clsStorage.currentClub.termNames());
                Program.home.Visible = false;
                //this line makes Marimba wait until it is fully loaded
                Application.DoEvents();
                if (Properties.Settings.Default.playSounds)
                    sound.welcome.Play();
                clsStorage.loggedin = true;
                this.Close();
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Login failed. Please check credentials.");
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdOpen.ShowDialog() == DialogResult.OK)
                {
                    clsStorage.currentClub = new club(ofdOpen.FileName);
                    clsStorage.currentClub.loadClub();
                    Properties.Settings.Default.FileLocation = ofdOpen.FileName;
                    lblFile.Text = "File: " + Properties.Settings.Default.FileLocation;
                    if (Properties.Settings.Default.playSounds)
                        sound.click.Play();
                }
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("The file you are trying to load is currently in use. The file cannot be loaded right now.");
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            clsStorage.loggedin = true;
            this.Close();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //check if a club was loaded
            //if so, close the filestream
            if (lblFile.Text != "File: " && !clsStorage.loggedin)
                clsStorage.currentClub.br.Close();
        }
    }
}
