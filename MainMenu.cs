using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ImapX;

namespace Marimba
{
    public partial class MainMenu : Form
    {
        delegate void DelegateVoid();
        public ListViewItem[] emailList;
        string strLocation = "";
        public MembershipMenu memberMenu = new MembershipMenu();
        public sendEmail sendemail = new sendEmail();
        public moneyMenu moneyMenu = new moneyMenu();
        public electionForm electionPlanner = new electionForm();
        public editSettings settings = new editSettings();
        public viewAssetList assetListViewer = new viewAssetList();
        public frmEditUser userEditor = new frmEditUser();

        public MainMenu(string[] args)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
                strLocation = String.Join(" ", args);
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
           if (Properties.Settings.Default.FileLocation == null)
           {
                if (sfdSave.ShowDialog() == DialogResult.OK)
                {
                    clsStorage.currentClub = new club(sfdSave.FileName);
                    clsStorage.currentClub.saveClub();
                    //store the file location as the default location
                    //the user moving the file around could be a problem
                    Properties.Settings.Default.FileLocation = sfdSave.FileName;                   
                    Properties.Settings.Default.Save();
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    clsStorage.unsavedChanges = false;
                }

           }
           else
           {
                 clsStorage.currentClub.saveClub();
                 Properties.Settings.Default.Save();
                 if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
                 clsStorage.unsavedChanges = false;
           }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.Hide();
            Form login = new Login(strLocation);
            login.ShowDialog();
            //if we never logged in, close the program
            if (!clsStorage.loggedin)
                this.Close();
            //guest login, go straight to attendance
            //I might change this later to allow signing up members
            else if (clsStorage.currentClub.strUser == null)
            {
                clsStorage.currentClub.strUser = "Guest";
                Form attendance = new signin();
                if (clsStorage.currentClub.sTerm == 0)
                {
                    if(Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("Cannot sign-in users because no term currently exists. Exiting Marimba.");
                }
                else
                {
                    this.Hide();
                    attendance.ShowDialog();
                }
                this.Close();
            }
            else
            {
                lblWelcome.Text += clsStorage.currentClub.strUser;
                populateHistory();
                //I think this should be removed... I'll confirm later
                if (clsStorage.currentClub.strPrivilege == "Admin")
                {
                    //btnNewTerm.Enabled = true;
                    //btnNewUser.Enabled = true;
                }
                //load email
                //use a bit of threading so that loading the email doesn't interfere with loading the entire main menu
                Thread t = new Thread(emailLogin);
                t.Start();
            }
        }

        void emailLogin()
        {
            try
            {     
                if (clsStorage.currentClub.clubEmail.login())
                {
                    emailList = clsStorage.currentClub.clubEmail.folderItems().ToArray();
                    this.Invoke(new DelegateVoid(addEmailToListView));
                }
                else
                {
                    emailList = new ListViewItem[1] { new ListViewItem("Error in loading email. Please confirm that you are connected to the Internet and that the password in settings is correct.") };
                    this.Invoke(new DelegateVoid(addEmailToListView));
                }
            }
            //yeah, yeah, this is really bad programming practice
            catch
            {
                try
                {            
                emailList = new ListViewItem[1] { new ListViewItem("Error in loading email. Please confirm that you are connected to the Internet and that the password in settings is correct.") };
                this.Invoke(new DelegateVoid(addEmailToListView));
                }
                catch{ }
            }
        }

        void addEmailToListView()
        {
            lvEmail.Items.AddRange(emailList);
            lvEmail.UseWaitCursor = false;
        }

        public void btnNewUser_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form newUserForm = new NewUser();
            newUserForm.ShowDialog();
        }

        public void btnNewTerm_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form addTerm = new newTerm();
            addTerm.ShowDialog();
        }

        public void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form change = new changePassword();
            change.ShowDialog();
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            populateHistory();
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
        }

        public void populateHistory()
        {
            lvHistory.Items.Clear();
            //show the last twenty changes
            for (int i = clsStorage.currentClub.iHistory-1; i >= Math.Max(0, clsStorage.currentClub.iHistory - 20); i--)
                lvHistory.Items.Add(clsStorage.currentClub.historyList[i].toString());
        }

        public void btnHistory_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form allHistory = new viewHistory();
            allHistory.ShowDialog();
        }

        public void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                clsStorage.currentClub.saveClub(sfdSave.FileName);
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
                clsStorage.unsavedChanges = false;
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsStorage.unsavedChanges && MessageBox.Show("Would you like to save the changes made?", "Save Changes", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                btnSaveAs_Click(sender, e);
            }
            else if (clsStorage.currentClub != null)
            {
                clsStorage.currentClub.br.Close();
                if (clsStorage.loggedin)
                    clsStorage.currentClub.clubEmail.logout();
            }
        }

        public void btnAbout_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form aboutForm = new about();
            aboutForm.ShowDialog();
        }

        private void lvEmail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvEmail.SelectedIndices[0] != -1) //something is selected
            {
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
                
                emailBrowser webDesign = new emailBrowser(emailBrowser.purpose.receive, Convert.ToInt32(lvEmail.SelectedItems[0].SubItems[4].Text));
                webDesign.Show();
            }
        }

        private void bwReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ribbon1.Export_Status.Value = Math.Min(e.ProgressPercentage, 100);
        }

        private void bwReport_DoWork(object sender, DoWorkEventArgs e)
        {
            //this is used for exporting the .mrb file because it takes a while
            excelFile.exportMrb(Program.home.memberMenu.svdSave.FileName);
            memberMenu.svdSave.FileName = "";
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
        }

        private void bwReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ribbon1.Export_Status.Value = 0;
            ribbon1.ExpClub.IsEnabled = true;
        }
    }
}
