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
    public partial class mergeMrb : Form
    {
        string filelocation;
        public mergeMrb()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if(ofdOpen.ShowDialog() == DialogResult.OK)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
                lblLocation.Text = "File to merge: " + ofdOpen.FileName;
                filelocation = ofdOpen.FileName;
                btnMerge.Enabled = true;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {

                    club slave = new club(filelocation);
                    slave.loadClub();
                    if (clsStorage.currentClub.findUser(clsStorage.currentClub.strUser) >= 0 &&
                        slave.findUser(clsStorage.currentClub.strUser) >= 0 && clsStorage.currentClub.strName == slave.strName
                        && clsStorage.currentClub.sTerm == slave.sTerm)
                    {
                        clsStorage.currentClub = club.mergeClub(clsStorage.currentClub, slave);
                        if (Properties.Settings.Default.playSounds)
                            sound.success.Play();
                        this.Close();
                    }
                    else
                    {
                        if (Properties.Settings.Default.playSounds)
                            sound.error.Play();
                        MessageBox.Show("Merging failed. Make sure the two files are for the same club, the user is a user on" +
                            "both files, and both files have the same terms.");
                    }

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("The file you are trying to load is currently in use. The file cannot be loaded right now.");
            }
            catch
            {
                //I'm currently not sure what might go wrong, so this error message is pretty generic.
                MessageBox.Show("Merging .mrb files failed.");
            }
        }
    }
}
