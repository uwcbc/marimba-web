﻿namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using Marimba.Utility;

    /// <summary>
    /// Interaction logic for Ribbon.xaml
    /// </summary>
    public partial class Ribbon : UserControl
    {
        public Ribbon()
        {
            InitializeComponent();
            if (ClsStorage.currentClub.strCurrentUserPrivilege == "Exec")
            {
                Admin.Visibility = Visibility.Hidden;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnSave_Click(sender, e);
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnSaveAs_Click(sender, e);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnHistory_Click(sender, e);
        }

        private void RefreshHistButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnRefresh_Click(sender, e);
        }

        private void RefreshEmailButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.attemptLogin();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnAbout_Click(sender, e);
        }

        private void PasswordButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnChangePassword_Click(sender, e);
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnSignIn_Click(sender, e);
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnSignUp_Click(sender, e);
        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnAttendance_Click(sender, e);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnProfile_Click(sender, e);
        }

        private void Mail_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.sendemail.ShowDialog();
        }

        private void ExpMember_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnExport_Click(sender, e);
        }

        private void ExpXlsxMember_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.svdSave.FilterIndex = 1;
        }

        private void ExpCsvMember_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.svdSave.FilterIndex = 2;
        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnNewUser_Click(sender, e);
        }

        private void NewTerm_Click(object sender, RoutedEventArgs e)
        {
            Program.home.btnNewTerm_Click(sender, e);
        }

        private void Election_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.electionPlanner.ShowDialog();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.btnBudget_Click(sender, e);
        }

        private void New_Record_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.btnAdd_Click(sender, e);
        }

        private void TermSummary_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.btnTermSummary_Click(sender, e);
        }

        private void AssetList_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.assetListViewer.ShowDialog();
        }

        private void ExpAccount_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.btnExport_Click(sender, e);
        }

        private void ExpXlsxAccount_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.svdSave.FilterIndex = 1;
        }

        private void ExpCsvAccount_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.svdSave.FilterIndex = 2;
        }

        private void Fees_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.btnFees_Click(sender, e);
        }

        private void ExpFees_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.moneyMenu.newfees.cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
            Program.home.moneyMenu.newfees.btnExport_Click(sender, e);
        }

        private void ExpXlsxFees_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.newfees.svdSave.FilterIndex = 1;
        }

        private void ExpCsvFees_Click(object sender, RoutedEventArgs e)
        {
            Program.home.moneyMenu.newfees.svdSave.FilterIndex = 2;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.settings.ShowDialog();
        }

        private void ExpClub_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();

            // we will borrow a svdSave window
            if (Program.home.memberMenu.svdSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Program.home.memberMenu.svdSave.FilterIndex = 1;
                ExpClub.IsEnabled = false;
                // call this so we can have a progress bar
                Program.home.bwReport.RunWorkerAsync();
            }
        }

        private void ImpClub_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();

            // we will borrow a ofdOpen window
            Program.home.memberMenu.ofdOpen.Filter = "Excel File|*.xlsx";
            if (Program.home.memberMenu.ofdOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                // we will borrow a sfdSave window
                if (Program.home.sfdSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Club saveChanges = ExcelFile.loadFromExcel(Program.home.memberMenu.ofdOpen.FileName, Program.home.sfdSave.FileName, ClsStorage.currentClub);
                    saveChanges.SaveClub();
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
        }

        private void Perger_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to purge inactive members? This action cannot be undone.", "Purge Inactive Members", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ClsStorage.currentClub.PurgeOldMembers();
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
                ClsStorage.currentClub.AddHistory(null, ChangeType.PurgeMembers);
                MessageBox.Show("Successfully purged old members from club.");
            }
        }

        private void Google_Button_Click(object sender, RoutedEventArgs e)
        {
            Program.home.memberMenu.btnGoogleDoc_Click(sender, e);
            if (Properties.Settings.Default.playSounds)
                Sound.Success.Play();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Program.home.userEditor.ShowDialog();
        }
    }
}
