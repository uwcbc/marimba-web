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
        List<election.ballot> importedBallots;
        public electionForm()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //make sure the user isn't doing something stupid
            //like erasing the old codes
            if (cbTerm.SelectedIndex != -1 && (clsStorage.currentClub.currentElection == null || MessageBox.Show(
                "This will overwrite the last election created. All codes will be lost. This cannot be undone. Are you sure you want to create a new election?",
                "Create New Election", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                clsStorage.currentClub.currentElection = new election(clsStorage.currentClub, cbTerm.SelectedIndex);
                btnCode.Enabled = true;
                btnImport.Enabled = true;
                btnReminder.Enabled = true;
                btnList.Enabled = true;
                btnAddCandidate.Enabled = true;
                btnExportBallot.Enabled = true;
                clsStorage.currentClub.electionSaved = true;

                //create positions
                createPositions cpForm = new createPositions();
                while (cpForm.strPositions == null)
                    cpForm.ShowDialog();

                clsStorage.currentClub.currentElection.iPositions = cpForm.strPositions.Length;
                clsStorage.currentClub.currentElection.strPositions = cpForm.strPositions;

                clsStorage.currentClub.addHistory("", history.changeType.setupElection);
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
            }
        }

        private void electionForm_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(clsStorage.currentClub.termNames());
            //load a saved election
            if(clsStorage.currentClub.electionSaved)
            {
                //enable all of the buttons
                btnCode.Enabled = true;
                btnImport.Enabled = true;
                btnReminder.Enabled = true;
                btnList.Enabled = true;
                btnAddCandidate.Enabled = true;
                btnExportBallot.Enabled = true;
            }

            //if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = clsStorage.currentClub.sTerm - 1;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (ofdOpen.ShowDialog() == DialogResult.OK)
            {
                //import the ballots
                importedBallots = new List<election.ballot>();
                importedBallots = excelFile.importBallots(ofdOpen.FileName, clsStorage.currentClub.currentElection);

                //next, check and verify the ballots
                importedBallots = clsStorage.currentClub.currentElection.checkedBallots(importedBallots);

                //Count the ballots
                clsStorage.currentClub.currentElection.countBallots(importedBallots);

                //export the results
                if (svdSave.ShowDialog() == DialogResult.OK)
                {
                    //1 = excel file
                    if (svdSave.FilterIndex == 1)
                    {
                        excelFile.exportElectionResults(clsStorage.currentClub.currentElection, svdSave.FileName);
                    }
                    else
                        MessageBox.Show("Exporting to .csv has not been implemented.");
                }
            }
        }

        private void btnReminder_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();

            emailBrowser webDesign = new emailBrowser(emailBrowser.purpose.bcc, -1, clsStorage.currentClub.currentElection.reminderListIndex());
            webDesign.ShowDialog();
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();

            if (Properties.Settings.Default.playSounds)
                sound.click.Play();

            emailBrowser webDesign = new emailBrowser(clsStorage.currentClub.currentElection);
            webDesign.ShowDialog();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //first, set up string array to be sent to Excel

                    //for now, exclude the identifier code, as exporting it is not exactly necessary when Marimba can just e-mail the codes to users
                    //I also want to avoid exporting the list of codes to Excel when practically the exact same list could be imported with little effort

                    string[,] output = new string[4 + clsStorage.currentClub.currentElection.iElectors + clsStorage.currentClub.currentElection.iAlmostElectors, 3];
                    output[0, 0] = "Name";
                    output[0, 1] = "E-Mail Address";
                    //output[0, 2] = "Identifier Code";
                    for (int i = 0; i < clsStorage.currentClub.currentElection.iElectors; i++)
                    {
                        output[i+1,0] = clsStorage.currentClub.currentElection.electorList[i].strName;
                        output[i+1,1] = clsStorage.currentClub.currentElection.electorList[i].strEmail;
                        //output[i+1,2] = clsStorage.currentClub.currentElection.electorList[i].strCode;
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
                }
            }
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
        }

        private void btnAddCandidate_Click(object sender, EventArgs e)
        {
            //first, determine which candidate to add
            MemberList temp = new MemberList(true);
            temp.ShowDialog();
            //check if something was selected
            if (clsStorage.iEmailMemberIndexList.Count > 0)
            {
                //check if the member is already a candidate
                if (clsStorage.currentClub.currentElection.candidateRegistered(clsStorage.iEmailMemberIndexList[0]) > -1)
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("Selected member is already registered as a candidate in the election.");
                }
                else
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.click.Play();
                    newCandidate ncForm = new newCandidate(clsStorage.currentClub.currentElection.strPositions, clsStorage.iEmailMemberIndexList[0]);
                    ncForm.ShowDialog();
                }
            }
        }

        private void btnExportBallot_Click(object sender, EventArgs e)
        {
            int[] acclaimIndex = new int[clsStorage.currentClub.currentElection.iPositions];
            //go through each position, and check for acclaimations
            //do it several times
            //this is to handle cases where one person's acclaimation allows another person to acclaim their position
            for (int j = 0; j < clsStorage.currentClub.currentElection.iPositions-1; j++)
            {
                for (int i = 0; i < clsStorage.currentClub.currentElection.iPositions; i++)
                {
                    acclaimIndex[i] = clsStorage.currentClub.currentElection.acclaimedPosition(i);
                    //if the position was acclaimed
                    if (acclaimIndex[i] > -1)
                    {
                        //process acclaim
                        clsStorage.currentClub.currentElection.processAcclaim(i, acclaimIndex[i]);
                    }
                }
            }

            //at this point, we have a list of which positions were acclaimed and which ones weren't even applied for
            //do another loop to write this all to be exported to Excel
            string[,] ballotInfo = new string[4 * clsStorage.currentClub.currentElection.iPositions, 2 + clsStorage.currentClub.currentElection.listOfCandidates.Count];
            //go through each member
            foreach(election.candidate eleMember in clsStorage.currentClub.currentElection.listOfCandidates)
            {
                //if the member applied for a position, mark them on the ballot
                for (int j = 0; j < clsStorage.currentClub.currentElection.iPositions; j++)
                    if (eleMember.preferences[j] < clsStorage.currentClub.currentElection.iPositions)
                        //find the next available spot on the ballot list
                        for(int k = 1; k < 1 + clsStorage.currentClub.currentElection.listOfCandidates.Count;k++)
                            if (String.IsNullOrEmpty(ballotInfo[4 * j + 2, k]))
                            {
                                ballotInfo[4 * j + 2, k] = clsStorage.currentClub.firstAndLastName(eleMember.index);
                                break;
                            }
            }

            //fill in the rest of the ballot info
            for(int i = 0; i < clsStorage.currentClub.currentElection.iPositions;i++)
            {
                ballotInfo[4 * i, 0] = "Position";
                ballotInfo[4 * i, 1] = clsStorage.currentClub.currentElection.strPositions[i];
                ballotInfo[4 * i + 1, 0] = "Status";
                if (acclaimIndex[i] == -2)
                    ballotInfo[4 * i + 1, 1] = "Vacant";
                else if (acclaimIndex[i] == -1)
                {
                    ballotInfo[4 * i + 1, 1] = "To Be Elected";
                    ballotInfo[4 * i + 2, 0] = "Candidates";
                }
                else
                {
                    ballotInfo[4 * i + 1, 1] = "Acclaimed";
                    ballotInfo[4 * i + 2, 0] = "Acclaimed by";
                }
            }

            //finally, export the ballot information

            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    excelFile.saveExcel(ballotInfo, svdSave.FileName);
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                }
                else if (svdSave.FilterIndex == 2)
                {
                    MessageBox.Show("Exporting ballots is currently only implemented with .xlsx export.");
                }
            }

        }
    }
}
