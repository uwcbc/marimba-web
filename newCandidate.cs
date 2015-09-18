using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marimba
{
    public partial class newCandidate : Form
    {
        bool fromMember = false;
        int iPositions;
        Label[] positionLabels;
        ComboBox[] positionRank;
        election.candidate createdCandidate = new election.candidate();

        public newCandidate(string[] strPositions, int iIndex)
        {
            createdCandidate.index = iIndex;

            InitializeComponent();
            lvCandidate.LargeImageList = Program.home.instrumentSmall;

            //add candidate
            lvCandidate.BeginUpdate();
            lvCandidate.Items.Clear();

            lvCandidate.Items.Add(new ListViewItem(new string[2] { clsStorage.currentClub.firstAndLastName(iIndex), clsStorage.currentClub.members[iIndex].uiStudentNumber.ToString() },
                        member.instrumentIconIndex(clsStorage.currentClub.members[iIndex].curInstrument)));

            lvCandidate.EndUpdate();

            //set up the labels and combo boxes for preferences
            iPositions = strPositions.Length;

            positionLabels = new Label[iPositions];
            positionRank = new ComboBox[iPositions];
            string[] positionRankings = new string[iPositions+1];
            for(int i = 0; i < iPositions; i++)
                positionRankings[i] = Convert.ToString(i+1);
            positionRankings[iPositions] = "Not Applying";

            for(int i = 0; i < iPositions; i++)
            {
                this.Height += 50;

                positionLabels[i] = new Label();
                positionLabels[i].Anchor = System.Windows.Forms.AnchorStyles.Left;
                positionLabels[i].AutoSize = true;
                positionLabels[i].Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                positionLabels[i].Text = strPositions[i];

                positionRank[i] = new ComboBox();
                positionRank[i].Anchor = System.Windows.Forms.AnchorStyles.None;
                positionRank[i].AutoSize = true;
                positionRank[i].Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                positionRank[i].DropDownStyle = ComboBoxStyle.DropDownList;

                positionRank[i].Items.AddRange(positionRankings);

                positionRank[i].SelectedIndex = iPositions;

                this.tlpMain.RowCount++;
                this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
                tlpMain.Controls.Add(positionLabels[i], 1, i + 1);
                tlpMain.Controls.Add(positionRank[i], 0, i + 1);
            }


            //fix up the rest of the form
            this.tlpMain.RowCount += 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.Controls.Add(this.lblBlurb, 0, 1 + iPositions);
            this.tlpMain.Controls.Add(this.txtBlurb, 1, 1 + iPositions);
            this.tlpMain.Controls.Add(this.btnSubmit, 0, 2 + iPositions);
            tlpMain.SetColumnSpan(btnSubmit, 2);

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //first, validate that all the rankings are in good shape
            bool validityCheck = false;
            bool[] ranksSelected = new bool[iPositions];
            for (int i = 0; i < iPositions; i++)
                validityCheck = validityCheck || positionRank[i].SelectedIndex != iPositions;

            if (!validityCheck)
                MessageBox.Show("At least one positions must be applied for.");
            else
            {
                for(int i = 0; i < iPositions; i++)
                {
                    //if the position has already been selected, we have a problem
                    if (positionRank[i].SelectedIndex != iPositions && ranksSelected[positionRank[i].SelectedIndex])
                    {
                        MessageBox.Show("One rank has been selected twice. All ranks must be unique.");
                        validityCheck = false;
                        break;
                    }
                    //otherwise, mark it so it cannot be used again
                    else if (positionRank[i].SelectedIndex != iPositions)
                        ranksSelected[positionRank[i].SelectedIndex] = true;
                }

                //next, check that the positions have been used in order
                //that is, one must use up their nth choice before using their n+1th choice

                for (int i = 0; i < iPositions-1; i++)
                {
                    if (!ranksSelected[i] && ranksSelected[i + 1])
                    {
                        MessageBox.Show("The smallest ranks must be used before the larger ranks.");
                        validityCheck = false;
                        break;
                    }
                }

                //check to make sure there is a blurb entered
                if(txtBlurb.Text == "")
                {
                    validityCheck = false;
                    MessageBox.Show("An election blurb must be entered for each candidate");
                }

                //if we have passed everything, we are good to add the candidate
                if(validityCheck)
                {
                    createdCandidate.strBlurb = txtBlurb.Text;
                    createdCandidate.preferences = new int[iPositions];
                    for (int i = 0; i < iPositions; i++)
                        createdCandidate.preferences[i] = positionRank[i].SelectedIndex;

                    //finally, add the candidate to the election
                    clsStorage.currentClub.currentElection.listOfCandidates.Add(createdCandidate);

                    //close the window, we are done
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    this.Close();
                }
                

            }
                

        }
    }
}
