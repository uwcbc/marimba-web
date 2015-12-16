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
    public partial class MembershipMenu : Form
    {
        public MembershipMenu()
        {
            InitializeComponent();
        }

        public void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form signUp = new SignUp();
            signUp.ShowDialog();
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //first, set up string array to be sent to Excel
                    object[,] output = new object[1 + clsStorage.currentClub.iMember, 10];
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
                    for (int i = 0; i < clsStorage.currentClub.iMember; i++)
                    {
                        //skip over unsubscribed members
                        if (clsStorage.currentClub.members[i].strFName != "♪Unsubscribed")
                        {
                            
                            output[j + 1, 0] = clsStorage.currentClub.members[i].strFName;
                            output[j + 1, 1] = clsStorage.currentClub.members[i].strLName;
                            output[j + 1, 2] = member.toString(clsStorage.currentClub.members[i].type);
                            output[j + 1, 3] = clsStorage.currentClub.members[i].uiStudentNumber;
                            output[j + 1, 4] = member.toString(clsStorage.currentClub.members[i].memberFaculty);
                            if (clsStorage.currentClub.members[i].curInstrument == member.instrument.other)
                                output[j + 1, 5] = clsStorage.currentClub.members[i].strOtherInstrument;
                            else
                                output[j + 1, 5] = member.instrumentToString(clsStorage.currentClub.members[i].curInstrument);
                            output[j + 1, 6] = clsStorage.currentClub.members[i].strEmail;
                            output[j + 1, 7] = clsStorage.currentClub.members[i].size.ToString();
                            output[j + 1, 8] = clsStorage.currentClub.members[i].strOther;
                            output[j + 1, 9] = clsStorage.currentClub.members[i].signupTime;
                            j++;
                        }
                    }
                    //now that the string array is set up, save it
                    excelFile.saveExcel(output, svdSave.FileName, false, "J", "dd/mm/yy hh:mm:ss AM/PM");
                }
                //2 = CSV file
                else if (svdSave.FilterIndex == 2)
                {
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
                        for (int i = 0; i < clsStorage.currentClub.iMember; i++)
                        {
                            //skip over unsubscribed members
                            if (clsStorage.currentClub.members[i].strFName != "♪Unsubscribed")
                            {
                                CsvRow row = new CsvRow();
                                row.Add(clsStorage.currentClub.members[i].strFName);
                                row.Add(clsStorage.currentClub.members[i].strLName);
                                row.Add(member.toString(clsStorage.currentClub.members[i].type));
                                row.Add(Convert.ToString(clsStorage.currentClub.members[i].uiStudentNumber));
                                row.Add(member.toString(clsStorage.currentClub.members[i].memberFaculty));
                                if (clsStorage.currentClub.members[i].curInstrument == member.instrument.other)
                                    row.Add(clsStorage.currentClub.members[i].strOtherInstrument);
                                else
                                    row.Add(member.instrumentToString(clsStorage.currentClub.members[i].curInstrument));
                                row.Add(clsStorage.currentClub.members[i].strEmail);
                                row.Add(clsStorage.currentClub.members[i].size.ToString());
                                row.Add(clsStorage.currentClub.members[i].strOther);
                                row.Add(clsStorage.currentClub.members[i].signupTime.ToString());
                                writer.WriteRow(row);
                            }
                        }
                    }
                }
                if (Properties.Settings.Default.playSounds)
                    sound.success.Play();
            }
        }

        public void btnSignIn_Click(object sender, EventArgs e)
        {
            Form signinform = new signin();
            if (clsStorage.currentClub.listTerms == null || clsStorage.currentClub.listTerms.Count == 0)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Cannot sign-in users because no term currently exists.");
            }
            else if (clsStorage.currentClub.listTerms[clsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today) != -1 ||
                MessageBox.Show("Today is not a rehearsal date. No attendance data will be recorded. Do you want to continue?", "Not Rehearsal Date", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                Application.DoEvents();
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
                signinform.ShowDialog();
            }
        }

        public void btnAttendance_Click(object sender, EventArgs e)
        {
            Form attendance = new Attendance();
            Application.DoEvents();
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
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
                        //Note: This is designed to read our Google form format
                        //In the event a different Google Doc is created, this will be rendered obsolete
                        CsvRow row = new CsvRow();
                        reader.ReadRow(row);
                        while (reader.ReadRow(row) && row[0] != "")
                        {
                            clsStorage.currentClub.addMember(row[1], row[2], 0,
                                Convert.ToUInt32(row[3]), member.stringToFaculty(row[6]), row[5], row[4], "", Convert.ToDateTime(row[0]));
                        }
                    }
                    clsStorage.currentClub.addHistory("", history.changeType.importMembers);
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Adding some of the members failed. Please make sure all of the data is valid.");
            }
        }

        public void btnProfile_Click(object sender, EventArgs e)
        {
            Form listofmembers = new MemberList(false);
            Application.DoEvents();
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            listofmembers.ShowDialog();
        }

        private void btnFees_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            Form newfees = new addFees();
            newfees.ShowDialog();
        }
    }
}
