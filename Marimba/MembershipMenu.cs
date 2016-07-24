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

    public partial class MembershipMenu : Form
    {
        public MembershipMenu()
        {
            InitializeComponent();
        }

        public void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form signUp = new SignUpForm();
            signUp.ShowDialog();
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                // 1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    // first, set up string array to be sent to Excel
                    object[,] output = new object[1 + ClsStorage.currentClub.iMember, 10];
                    output[0, 0] = "First Name";
                    output[0, 1] = "Last Name";
                    output[0, 2] = "Type";
                    output[0, 3] = "Student ID";
                    output[0, 4] = "Faculty";
                    output[0, 5] = "Instrument";
                    output[0, 6] = "E-Mail Address";
                    output[0, 7] = "Shirt Size";
                    output[0, 8] = "Other";
                    output[0, 9] = "Sign-Up Time";
                    int j = 0;
                    for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                    {
                        // skip over unsubscribed members
                        if (ClsStorage.currentClub.members[i].firstName != "♪Unsubscribed")
                        {
                            
                            output[j + 1, 0] = ClsStorage.currentClub.members[i].firstName;
                            output[j + 1, 1] = ClsStorage.currentClub.members[i].lastName;
                            output[j + 1, 2] = Member.ToString(ClsStorage.currentClub.members[i].type);
                            output[j + 1, 3] = ClsStorage.currentClub.members[i].uiStudentNumber;
                            output[j + 1, 4] = Member.ToString(ClsStorage.currentClub.members[i].memberFaculty);
                            if (ClsStorage.currentClub.members[i].curInstrument == Member.Instrument.Other)
                                output[j + 1, 5] = ClsStorage.currentClub.members[i].otherInstrument;
                            else
                                output[j + 1, 5] = Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument);
                            output[j + 1, 6] = ClsStorage.currentClub.members[i].email;
                            output[j + 1, 7] = ClsStorage.currentClub.members[i].size.ToString();
                            output[j + 1, 8] = ClsStorage.currentClub.members[i].comments;
                            output[j + 1, 9] = ClsStorage.currentClub.members[i].signupTime;
                            j++;
                        }
                    }
                    // now that the string array is set up, save it
                    ExcelFile.saveExcel(output, svdSave.FileName, "dd/MM/yy hh:mm:ss AM/PM");
                }
                else if (svdSave.FilterIndex == 2)
                {
                    // 2 = CSV file
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("First Name");
                        firstrow.Add("Last Name");
                        firstrow.Add("Type");
                        firstrow.Add("Student ID");
                        firstrow.Add("Faculty");
                        firstrow.Add("Instrument");
                        firstrow.Add("E-Mail Address");
                        firstrow.Add("Shirt Size");
                        firstrow.Add("Other");
                        firstrow.Add("Sign-Up Time");
                        writer.WriteRow(firstrow);
                        for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                        {
                            // skip over unsubscribed members
                            if (ClsStorage.currentClub.members[i].firstName != "♪Unsubscribed")
                            {
                                CsvRow row = new CsvRow();
                                row.Add(ClsStorage.currentClub.members[i].firstName);
                                row.Add(ClsStorage.currentClub.members[i].lastName);
                                row.Add(Member.ToString(ClsStorage.currentClub.members[i].type));
                                row.Add(Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber));
                                row.Add(Member.ToString(ClsStorage.currentClub.members[i].memberFaculty));
                                if (ClsStorage.currentClub.members[i].curInstrument == Member.Instrument.Other)
                                    row.Add(ClsStorage.currentClub.members[i].otherInstrument);
                                else
                                    row.Add(Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument));
                                row.Add(ClsStorage.currentClub.members[i].email);
                                row.Add(ClsStorage.currentClub.members[i].size.ToString());
                                row.Add(ClsStorage.currentClub.members[i].comments);
                                row.Add(ClsStorage.currentClub.members[i].signupTime.ToString());
                                writer.WriteRow(row);
                            }
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
        }

        public void btnSignIn_Click(object sender, EventArgs e)
        {
            Form signinform = new SignInForm();
            if (ClsStorage.currentClub.listTerms.Count == 0)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Cannot sign-in users because no term currently exists.");
            }
            else if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today) != -1 ||
                MessageBox.Show("Today is not a rehearsal date. No attendance data will be recorded. Do you want to continue?", "Not Rehearsal Date", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                Application.DoEvents();
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();
                signinform.ShowDialog();
            }
        }

        public void btnAttendance_Click(object sender, EventArgs e)
        {
            Form attendance = new Attendance();
            Application.DoEvents();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            attendance.ShowDialog();
        }

        public void btnGoogleDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdOpen.ShowDialog() == DialogResult.OK)
                {
                    using (CsvFileReader reader = new CsvFileReader(ofdOpen.FileName))
                    {
                        // Note: This is designed to read our Google form format
                        // In the event a different Google Doc is created, this will be rendered obsolete
                        CsvRow row = new CsvRow();
                        reader.ReadRow(row);
                        while (reader.ReadRow(row) && row[0] != String.Empty)
                        {
                            ClsStorage.currentClub.AddMember(row[1], row[2], 0, Convert.ToUInt32(row[3]), Member.ParseFaculty(row[6]), row[5], row[4], String.Empty, Convert.ToDateTime(row[0]));
                        }
                    }
                    ClsStorage.currentClub.AddHistory(String.Empty, ChangeType.ImportMembers);
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Adding some of the members failed. Please make sure all of the data is valid.");
            }
        }

        public void btnProfile_Click(object sender, EventArgs e)
        {
            Form listofmembers = new MemberList(false);
            Application.DoEvents();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            listofmembers.ShowDialog();
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            Form newfees = new AddFeesForm();
            newfees.ShowDialog();
        }
    }
}
