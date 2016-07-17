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
    public partial class electionForm : Form
    {
        public electionForm()
        {
            InitializeComponent();
        }

        private void electionForm_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(clsStorage.currentClub.termNames());

            //enable all of the buttons
            btnList.Enabled = true;

            //if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = clsStorage.currentClub.listTerms.Count - 1;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                clsStorage.currentClub.currentElection = new election(clsStorage.currentClub, cbTerm.SelectedIndex);
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //first, set up string array to be sent to Excel

                    //for now, exclude the identifier code, as exporting it is not exactly necessary when Marimba can just e-mail the codes to users
                    //I also want to avoid exporting the list of codes to Excel when practically the exact same list could be imported with little effort

                    string[,] output = new string[4 + clsStorage.currentClub.currentElection.iElectors + clsStorage.currentClub.currentElection.iAlmostElectors, 3];
                    output[0, 0] = "Name";
                    output[0, 1] = "E-Mail Address";
                    for (int i = 0; i < clsStorage.currentClub.currentElection.iElectors; i++)
                    {
                        output[i+1,0] = clsStorage.currentClub.currentElection.electorList[i].strName;
                        output[i+1,1] = clsStorage.currentClub.currentElection.electorList[i].strEmail;
                    }
                    output[clsStorage.currentClub.currentElection.iElectors+2, 0] = "Members Owing Club Membership Fees";
                    output[clsStorage.currentClub.currentElection.iElectors + 3, 0] = "Name";
                    output[clsStorage.currentClub.currentElection.iElectors + 3, 1] = "E-Mail Address";
                    for (int i = 0; i < clsStorage.currentClub.currentElection.iAlmostElectors; i++)
                    {
                        output[i+4+clsStorage.currentClub.currentElection.iElectors,0] = clsStorage.currentClub.currentElection.almostElector[i].strName;
                        output[i + 4 + clsStorage.currentClub.currentElection.iElectors, 1] = clsStorage.currentClub.currentElection.almostElector[i].strEmail;
                    }
                    //now that the string array is set up, save it
                    excelFile.saveExcel(output, svdSave.FileName);
                }
                else if (svdSave.FilterIndex == 2)
                {
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("Name");
                        firstrow.Add("E-Mail Address");
                        //firstrow.Add("Identifier Code");
                        writer.WriteRow(firstrow);
                        for (int i = 0; i < clsStorage.currentClub.currentElection.iElectors; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(clsStorage.currentClub.currentElection.electorList[i].strName);
                            row.Add(clsStorage.currentClub.currentElection.electorList[i].strEmail);
                            //row.Add(clsStorage.currentClub.currentElection.electorList[i].strCode);
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
                        for (int i = 0; i < clsStorage.currentClub.currentElection.iAlmostElectors; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(clsStorage.currentClub.currentElection.almostElector[i].strName);
                            row.Add(clsStorage.currentClub.currentElection.almostElector[i].strEmail);
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                }
            }
        }
    }
}
