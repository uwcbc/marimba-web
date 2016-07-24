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

    public partial class electionForm : Form
    {
        public electionForm()
        {
            InitializeComponent();
        }

        private void electionForm_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());

            // enable all of the buttons
            btnList.Enabled = true;

            // if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                ClsStorage.currentClub.currentElection = new Election(ClsStorage.currentClub, cbTerm.SelectedIndex);

                // 1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    // first, set up string array to be sent to Excel
                    string[,] output = new string[4 + ClsStorage.currentClub.currentElection.electorList.Count + ClsStorage.currentClub.currentElection.almostElector.Count, 3];
                    output[0, 0] = "Name";
                    output[0, 1] = "E-Mail Address";
                    for (int i = 0; i < ClsStorage.currentClub.currentElection.electorList.Count; i++)
                    {
                        output[i + 1, 0] = ClsStorage.currentClub.currentElection.electorList[i].strName;
                        output[i + 1, 1] = ClsStorage.currentClub.currentElection.electorList[i].strEmail;
                    }

                    output[ClsStorage.currentClub.currentElection.electorList.Count + 2, 0] = "Members Owing Club Membership Fees";
                    output[ClsStorage.currentClub.currentElection.electorList.Count + 3, 0] = "Name";
                    output[ClsStorage.currentClub.currentElection.electorList.Count + 3, 1] = "E-Mail Address";
                    for (int i = 0; i < ClsStorage.currentClub.currentElection.almostElector.Count; i++)
                    {
                        output[i + 4 + ClsStorage.currentClub.currentElection.electorList.Count, 0] = ClsStorage.currentClub.currentElection.almostElector[i].strName;
                        output[i + 4 + ClsStorage.currentClub.currentElection.electorList.Count, 1] = ClsStorage.currentClub.currentElection.almostElector[i].strEmail;
                    }

                    // now that the string array is set up, save it
                    ExcelFile.saveExcel(output, svdSave.FileName);
                }
                else if (svdSave.FilterIndex == 2)
                {
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("Name");
                        firstrow.Add("E-Mail Address");
                        // firstrow.Add("Identifier Code");
                        writer.WriteRow(firstrow);
                        for (int i = 0; i < ClsStorage.currentClub.currentElection.electorList.Count; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(ClsStorage.currentClub.currentElection.electorList[i].strName);
                            row.Add(ClsStorage.currentClub.currentElection.electorList[i].strEmail);
                            writer.WriteRow(row);
                        }
                        writer.WriteRow(new CsvRow());
                        CsvRow almostElectorTitle = new CsvRow();
                        almostElectorTitle.Add("Members Owing Club Membership Fees");
                        writer.WriteRow(almostElectorTitle);
                        firstrow = new CsvRow();
                        firstrow.Add("Name");
                        firstrow.Add("E-Mail Address");
                        writer.WriteRow(firstrow);
                        for (int i = 0; i < ClsStorage.currentClub.currentElection.almostElector.Count; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(ClsStorage.currentClub.currentElection.almostElector[i].strName);
                            row.Add(ClsStorage.currentClub.currentElection.almostElector[i].strEmail);
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
        }
    }
}
