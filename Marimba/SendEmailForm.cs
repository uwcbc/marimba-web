using Marimba.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Marimba
{
    public partial class SendEmailForm : Form
    {
        int loadMore = 40;

        public SendEmailForm()
        {
            InitializeComponent();
        }

        private void btnLoadMore_Click(object sender, EventArgs e)
        {
            // load 40 more
            loadMore += 40;
            // don't do more than 480 emails... please
            // that's too much
            lvEmail.BeginUpdate();
            if (loadMore <= 480)
                lvEmail.Items.AddRange(ClsStorage.currentClub.clubEmail.folderItems(loadMore - 40, loadMore).ToArray());
            lvEmail.EndUpdate();
        }

        private void sendEmail_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            lvEmail.Items.AddRange(ClsStorage.currentClub.clubEmail.GetFolderItems().ToArray());
        }

        private void lvEmail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvEmail.SelectedIndices[0] != -1) //something is selected
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();

                EmailForm webDesign = new EmailForm(EmailPurpose.Receive, Convert.ToInt32(lvEmail.SelectedItems[0].SubItems[4].Text));
                webDesign.Show();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MemberList temp = new MemberList(true);
            switch(cbTo.SelectedIndex)
            {
                //To Individual Member
                case 0:
                    temp.ShowDialog();
                    //check if something was selected
                    if (ClsStorage.selectedMembersList.Count > 0)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Click.Play();
                        EmailForm webDesign = new EmailForm(EmailPurpose.Send, -1, ClsStorage.selectedMembersList);
                        webDesign.ShowDialog();
                    }

                    break;
                //BCC Individual Member
                case 1:
                    temp.ShowDialog();
                    //check if something was selected
                    if (ClsStorage.selectedMembersList.Count > 0)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Click.Play();
                        EmailForm webDesign = new EmailForm(EmailPurpose.Bcc, -1, ClsStorage.selectedMembersList);
                        webDesign.ShowDialog();
                    }
                    break;
                //all active members in current term
                case 2:
                    //clear the list
                    ClsStorage.selectedMembersList.Clear();

                    //now, add everyone in the term to it
                    for(int i = 0; i < ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count-1].numMembers; i++)
                        if (!ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].checkLimbo(i))
                            ClsStorage.selectedMembersList.Add(ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].members[i]);

                    //check if something was selected
                    if (ClsStorage.selectedMembersList.Count > 0)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Click.Play();
                        EmailForm webDesign = new EmailForm(EmailPurpose.Bcc, -1, ClsStorage.selectedMembersList);
                        webDesign.ShowDialog();
                    }
                    
                    break;
                //all members in current term
                //similar idea to case 1 without the check
                case 3:
                    //clear the list
                    ClsStorage.selectedMembersList.Clear();

                    //now, add everyone in the term to it
                    for (int i = 0; i < ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numMembers; i++)
                        ClsStorage.selectedMembersList.Add(ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].members[i]);

                    //check if something was selected
                    if (ClsStorage.selectedMembersList.Count > 0)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Click.Play();
                        EmailForm webDesign = new EmailForm(EmailPurpose.Bcc, -1, ClsStorage.selectedMembersList);
                        webDesign.ShowDialog();
                    }

                    break;
                //To Entire Mailing List
                case 4:
                    //check if something was selected
                    if (Properties.Settings.Default.playSounds)
                        Sound.Click.Play();
                    EmailForm massEmailMaker = new EmailForm(EmailPurpose.MassEmail);
                    massEmailMaker.ShowDialog();
                    break;
            }
        }
    }
}
