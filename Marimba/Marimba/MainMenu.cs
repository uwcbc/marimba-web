namespace Marimba
{
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
    using Marimba.Utility;

    public partial class MainMenu : Form
    {
        public ListViewItem[] emailList;
        string strLocation = String.Empty;
        public MembershipMenu memberMenu = new MembershipMenu();
        public SendEmailForm sendemail = new SendEmailForm();
        public moneyMenu moneyMenu = new moneyMenu();
        public electionForm electionPlanner = new electionForm();
        public editSettings settings = new editSettings();
        public AssetList assetListViewer = new AssetList();
        public frmEditUser userEditor = new frmEditUser();

        public MainMenu(string[] args)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
                strLocation = String.Join(" ", args);
            }
        }

        delegate void DelegateVoid();

        public void btnSave_Click(object sender, EventArgs e)
        {
           if (Properties.Settings.Default.FileLocation == null)
           {
                if (sfdSave.ShowDialog() == DialogResult.OK)
                {
                    ClsStorage.currentClub = new Club(sfdSave.FileName);
                    ClsStorage.currentClub.SaveClub();
                    // store the file location as the default location
                    // the user moving the file around could be a problem
                    Properties.Settings.Default.FileLocation = sfdSave.FileName;                   
                    Properties.Settings.Default.Save();
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                    ClsStorage.unsavedChanges = false;
                }

           }
           else
           {
                 ClsStorage.currentClub.SaveClub();
                 Properties.Settings.Default.Save();
                 if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
                 ClsStorage.unsavedChanges = false;
           }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.Hide();
            Form login = new Login(strLocation);
            login.ShowDialog();
            // if we never logged in, close the program
            if (!ClsStorage.loggedin)
                this.Close();
            // guest login, go straight to attendance
            // I might change this later to allow signing up members
            else if (ClsStorage.currentClub.strCurrentUser == null)
            {
                ClsStorage.currentClub.strCurrentUser = "Guest";
                Form attendance = new SignInForm();
                if (ClsStorage.currentClub.listTerms == null || ClsStorage.currentClub.listTerms.Count == 0)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
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
                lblWelcome.Text += ClsStorage.currentClub.strCurrentUser;
                populateHistory();

                // load email
                // use a bit of threading so that loading the email doesn't interfere with loading the entire main menu
                Thread t = new Thread(emailLogin);
                t.Start();
            }
        }

        void emailLogin()
        {
            try
            {     
                if (ClsStorage.currentClub.clubEmail.login())
                {
                    emailList = ClsStorage.currentClub.clubEmail.GetFolderItems().ToArray();
                    this.Invoke(new DelegateVoid(addEmailToListView));
                }
                else
                {
                    emailList = new ListViewItem[1] { new ListViewItem("Error in loading email. Please confirm that you are connected to the Internet and that the password in settings is correct.") };
                    this.Invoke(new DelegateVoid(addEmailToListView));
                }
            }
            catch
            {
                // yeah, yeah, this is really bad programming practice
                try
                {            
                    emailList = new ListViewItem[1] { new ListViewItem("Error in loading email. Please confirm that you are connected to the Internet and that the password in settings is correct.") };
                    this.Invoke(new DelegateVoid(addEmailToListView));
                }
                catch
                {
                    // do nothing, give up
                }
            }
        }

        void addEmailToListView()
        {
            lvEmail.Items.Clear();
            lvEmail.Items.AddRange(emailList);
            lvEmail.UseWaitCursor = false;
        }

        public void btnNewUser_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form newUserForm = new NewUser();
            newUserForm.ShowDialog();
        }

        public void btnNewTerm_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form addTerm = new newTerm();
            addTerm.ShowDialog();
        }

        public void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form change = new changePassword();
            change.ShowDialog();
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            populateHistory();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
        }

        public void populateHistory()
        {
            lvHistory.Items.Clear();
            // show the last twenty changes
            for (int i = ClsStorage.currentClub.historyList.Count - 1; i >= Math.Max(0, ClsStorage.currentClub.historyList.Count - 20); i--)
                lvHistory.Items.Add(ClsStorage.currentClub.historyList[i].ToString());
        }

        public void btnHistory_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form allHistory = new viewHistory();
            allHistory.ShowDialog();
        }

        public void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                ClsStorage.currentClub.SaveClub(sfdSave.FileName);
                Properties.Settings.Default.Save();
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
                ClsStorage.unsavedChanges = false;
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsStorage.unsavedChanges && MessageBox.Show("Would you like to save the changes made?", "Save Changes", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                btnSaveAs_Click(sender, e);
            }
            else if (ClsStorage.currentClub != null)
            {
                ClsStorage.currentClub.br.Close();
                if (ClsStorage.loggedin)
                    ClsStorage.currentClub.clubEmail.logout();
            }
        }

        public void btnAbout_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void lvEmail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // something is selected
            if (lvEmail.SelectedIndices[0] != -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();

                EmailForm webDesign = new EmailForm(EmailPurpose.Receive, Convert.ToInt32(lvEmail.SelectedItems[0].SubItems[4].Text));
                webDesign.Show();
            }
        }

        private void bwReport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ribbon1.Export_Status.Value = Math.Min(e.ProgressPercentage, 100);
        }

        private void bwReport_DoWork(object sender, DoWorkEventArgs e)
        {
            // this is used for exporting the .mrb file because it takes a while
            ExcelFile.exportMrb(Program.home.memberMenu.svdSave.FileName);
            memberMenu.svdSave.FileName = String.Empty;
            if (Properties.Settings.Default.playSounds)
                Sound.Success.Play();
        }

        private void bwReport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ribbon1.Export_Status.Value = 0;
            ribbon1.ExpClub.IsEnabled = true;
        }

        public void attemptLogin()
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            // load email
            // use a bit of threading so that loading the email doesn't interfere with loading the entire main menu
            Thread t = new Thread(emailLogin);
            t.Start();
        }
    }
}
