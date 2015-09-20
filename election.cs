using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Marimba
{
    class election
    {
        //these variables are for the list
        public int iElectors;
        public int iAlmostElectors;
        public List<elector> electorList;
        public List<elector> almostElector;
        Random rnd;

        //these variables are for the candidates and voting
        public List<candidate> listOfCandidates = new List<candidate>();
        public int iPositions;
        public string[] strPositions;

        public int iTermIndex;

        public int totalBallots, rejectBallots;
        /// <summary>
        /// A # of candidates by # of positions array, adding up all of the ballots
        /// </summary>
        public int[,] ballotTally;
        /// <summary>
        /// The list of indexes of the candidates who got elected
        /// </summary>
        public int[] iElected;

        public struct candidate
        {
            public int index;
            public int[] preferences;
            public string strBlurb;
            public bool bElected;
        }

        public struct ballot
        {
            public string strCode;
            public string[] strPositionVote;
            public bool bVerified;
        }

        public election(club mainClub, int iTermIndex)
        {
            //creating an election is a lot of work!
            //we have a few special structures just for this
            //first, create a list of electors
            //start with the 2 terms back, then do 1 term back
            //240 is 120*2, the maximum possible number we could handle for an election
            electorList = new List<elector>();
            //this is so we can let people who forgot to pay membership fees know that if they want to vote
            //they need to settle their debts
            almostElector = new List<elector>();
            iElectors = 0;
            iAlmostElectors = 0;
            rnd = new Random();
            this.iTermIndex = iTermIndex;
            updateElectorList(mainClub, true);

            totalBallots = 0;
            rejectBallots = 0;
        }

        public election(BinaryReader br)
        {
            iTermIndex = br.ReadInt32();
            elector nextElector = new elector();
            electorList = new List<elector>();
            almostElector = new List<elector>();
            totalBallots = 0;
            rejectBallots = 0;
            iAlmostElectors = br.ReadInt32();
            for (int i = 0; i < iAlmostElectors; i++)
            {
                nextElector.id = br.ReadInt32();
                nextElector.strName = br.ReadString();
                nextElector.strEmail = br.ReadString();
                almostElector.Add(nextElector);
                //note to self: add id's here
            }
            iElectors = br.ReadInt32();
            for (int i = 0; i < iElectors; i++)
            {
                nextElector.id = br.ReadInt32();
                nextElector.strName = br.ReadString();
                nextElector.strEmail = br.ReadString();
                nextElector.strCode = br.ReadString();
                electorList.Add(nextElector);
                //note to self: add id's here
            }
            iPositions = br.ReadInt32();
            strPositions = new string[iPositions];
            for(int i = 0; i < iPositions; i++)
                strPositions[i] = br.ReadString();
            int numCandidates = br.ReadInt32();
            for (int i = 0; i < numCandidates; i++)
            {
                candidate nextCandidate = new candidate();
                nextCandidate.index = br.ReadInt32();
                nextCandidate.preferences = new int[iPositions];
                for (int j = 0; j < iPositions; j++)
                    nextCandidate.preferences[j] = br.ReadInt32();
                nextCandidate.strBlurb = clsStorage.reverseCleanNewLine(br.ReadString());
                nextCandidate.bElected = br.ReadBoolean();
                listOfCandidates.Add(nextCandidate);
            }
        }

        public election(StreamReader sr)
        {
            iTermIndex = Convert.ToInt32(sr.ReadLine());
            elector nextElector = new elector();
            electorList = new List<elector>();
            almostElector = new List<elector>();
            totalBallots = 0;
            rejectBallots = 0;
            iAlmostElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iAlmostElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();
                almostElector.Add(nextElector);
                //note to self: add id's here
            }
            iElectors = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < iElectors; i++)
            {
                nextElector.id = Convert.ToInt32(sr.ReadLine());
                nextElector.strName = sr.ReadLine();
                nextElector.strEmail = sr.ReadLine();
                nextElector.strCode = sr.ReadLine();
                electorList.Add(nextElector);
                //note to self: add id's here
            }
            iPositions = Convert.ToInt32(sr.ReadLine());
            strPositions = new string[iPositions];
            for (int i = 0; i < iPositions; i++)
                strPositions[i] = sr.ReadLine();
            int numCandidates = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < numCandidates; i++)
            {
                candidate nextCandidate = new candidate();
                nextCandidate.index = Convert.ToInt32(sr.ReadLine());
                nextCandidate.preferences = new int[iPositions];
                for (int j = 0; j < iPositions; j++)
                    nextCandidate.preferences[j] = Convert.ToInt32(sr.ReadLine());
                nextCandidate.strBlurb = sr.ReadLine();
                nextCandidate.bElected = Convert.ToBoolean(sr.ReadLine());
                listOfCandidates.Add(nextCandidate);
            }
        }

        public void updateElectorList(club mainClub, bool newCode)
        {
            //Note to self: This will have to be fixed for adding new users without creating new codes
            int term1index, term2index;
            bool term1okay, term2okay;
            for(short i = 0; i<mainClub.iMember;i++)
            {
                term2index = -1;
                term2okay = true;
                term1index = mainClub.terms[iTermIndex].memberSearch(i);
                term1okay = (term1index == -1) || mainClub.terms[iTermIndex].limboMembers[term1index] ||
                        (mainClub.terms[iTermIndex].feesPaid[term1index, 0] > 0);
                if (iTermIndex != 0)//There are two terms to go back to
                {
                    term2index = mainClub.terms[iTermIndex-1].memberSearch(i);
                    term2okay = (term2index == -1) || mainClub.terms[iTermIndex - 1].limboMembers[term2index] ||
                        (mainClub.terms[iTermIndex - 1].feesPaid[term2index, 0] > 0);
                }
                if (((term1index != -1 && !mainClub.terms[iTermIndex].limboMembers[term1index]) ||
                    (term2index != -1 && !mainClub.terms[iTermIndex - 1].limboMembers[term2index]))
                    && term1okay && term2okay && (clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad))
                {
                    elector temp = new elector();

                    temp.strName = mainClub.formatedName(i);
                    temp.strEmail = mainClub.members[i].strEmail;
                    temp.id = i;

                    //if we are looking for a new code for the user
                    if (newCode)
                        temp.strCode = codeGenerator(rnd);

                    electorList.Add(temp);
                    iElectors++;
                }
                else if((clsStorage.currentClub.members[i].type == member.membertype.UWUnderGrad || clsStorage.currentClub.members[i].type == member.membertype.UWGrad) &&
                    ((term1index != -1 && !mainClub.terms[iTermIndex].limboMembers[term1index]) || (term2index != -1 && !mainClub.terms[iTermIndex - 1].limboMembers[term2index])))
                {
                    elector temp = new elector();
                    temp.strName = mainClub.formatedName(i);
                    temp.strEmail = mainClub.members[i].strEmail;
                    temp.id = i;

                    almostElector.Add(temp);
                    iAlmostElectors++;
                }
            }

            /*
            if (iTermIndex != 0)//There are two terms to go back to
            {
                for (int i = 0; i < mainClub.terms[iTermIndex - 1].sMembers; i++)
                {
                    //if not a limbo member and a (possibly discounted) membership fee has been paid
                    //and is a UW Student
                    if (!mainClub.terms[iTermIndex - 1].limboMembers[i] &&
                        mainClub.terms[iTermIndex - 1].feesPaid[i, 0] > 0 && clsStorage.currentClub.members[mainClub.terms[iTermIndex-1].members[i]].type == member.membertype.UWStudent)
                    {
                        electorList[iElectors].strName = mainClub.formatedName(mainClub.terms[iTermIndex - 1].members[i]);
                        electorList[iElectors].strEmail = mainClub.members[mainClub.terms[iTermIndex - 1].members[i]].strEmail;
                        //if we are looking for a new code for the user
                        if (newCode)
                            electorList[iElectors].strCode = codeGenerator(rnd);
                        iElectors++;
                    }
                    //member who has not paid
                    //otherwise would qualify (i.e. is UW student)
                    else if (!mainClub.terms[iTermIndex - 1].limboMembers[i] && clsStorage.currentClub.members[mainClub.terms[iTermIndex-1].members[i]].type == member.membertype.UWStudent)
                    {
                        almostElector[iAlmostElectors].strName = mainClub.formatedName(mainClub.terms[iTermIndex - 1].members[i]);
                        almostElector[iAlmostElectors].strEmail = mainClub.members[mainClub.terms[iTermIndex - 1].members[i]].strEmail;
                        iAlmostElectors++;
                    }
                }
            }
            //repeat, but for the current term
            for (int i = 0; i < mainClub.terms[iTermIndex].sMembers; i++)
            {
                //if not a limbo member and a (possibly discounted) membership fee has been paid
                if (!mainClub.terms[iTermIndex].limboMembers[i] &&
                    mainClub.terms[iTermIndex].feesPaid[i, 0] > 0 && clsStorage.currentClub.members[mainClub.terms[iTermIndex].members[i]].type == member.membertype.UWStudent)
                {
                    electorList[iElectors].strName = mainClub.formatedName(mainClub.terms[iTermIndex].members[i]);
                    electorList[iElectors].strEmail = mainClub.members[mainClub.terms[iTermIndex].members[i]].strEmail;
                    electorList[iElectors].strCode = codeGenerator(rnd);
                    iElectors++;
                }
                //member who has not paid
                else if (!mainClub.terms[iTermIndex].limboMembers[i] && clsStorage.currentClub.members[mainClub.terms[iTermIndex].members[i]].type == member.membertype.UWStudent)
                {
                    almostElector[iAlmostElectors].strName = mainClub.formatedName(mainClub.terms[iTermIndex].members[i]);
                    almostElector[iAlmostElectors].strEmail = mainClub.members[mainClub.terms[iTermIndex].members[i]].strEmail;
                    iAlmostElectors++;
                }
            }
             * */
        }

        public List<int> reminderListIndex()
        {
            List<int> output = new List<int>();
            for (int i = 0; i < iAlmostElectors; i++)
                output.Add(almostElector[i].id);
            return output;
        }

        public List<int> electorListIndex()
        {
            List<int> output = new List<int>();
            for (int i = 0; i < iElectors; i++)
                output.Add(electorList[i].id);
            return output;
        }

        public static string reminder = "Hello,\r\nWe are currently conducting elections for the upcoming term, and our records indicate " +
            "you have not paid all of your membership fees. While this is the case, you are not eligible to vote in our elections. Please " +
            "reply back to arrange to correct this.\r\nThank you,\r\n";

        public static string codeNotification = "Hello,\r\nIt is time for this term's elections. As a member of one of the past two terms, " +
            "you are eligible to vote. You can find your unique identifier code at the bottom of this e-mail. Use your code in the following " +
            "form to vote: *insert URL here*.";
        public static string codeGenerator(Random rnd)
        {            
            char[] cCode = new char[16];
            //avoid the = since it messes up Excel
            cCode[0] = (char)rnd.Next(62,126);
            for(int i = 1; i < 16; i++)
            {
                int iGen = rnd.Next(32, 126);
                cCode[i] = (char)iGen;
            }
            return String.Concat(cCode);
        }

        public struct elector
        {
            public int id;
            public string strName;
            public string strEmail;
            //I don't like having the code stored alongside the elector information
            //This should eventually be changed
            public string strCode;
        }

        /// <summary>
        /// Returns the index of the user who acclaims the position.
        /// </summary>
        /// <param name="index">Index of the position to check if acclaimed</param>
        /// <returns>Index of user who acclaims position. Returns -1 if no one acclaims. Returns -2 if no one applied</returns>
        public int acclaimedPosition(int index)
        {
            int output = -2;
            //check each candidate to see if they wanted this position as their first choice
            for (int i = 0; i < listOfCandidates.Count; i++ )
            {
                //if (s)he marked it as his/her first choice
                if (listOfCandidates[i].preferences[index] == 0 && output == -2)
                    output = i;
                //if anyone applies for the position, but does not mark it as number 1, the position cannot be acclaimed
                else if (listOfCandidates[i].preferences[index] < iPositions)
                    return -1;
            }
            //return the index of the user who acclaims it
            return output;
        }

        /// <summary>
        /// Removes acclaiming candidates from application in other roles
        /// </summary>
        /// <param name="positionIndex">Index of the position</param>
        /// <param name="memberIndex">Index of the member</param>
        public void processAcclaim(int positionIndex, int candidateIndex)
        {
            //they won, so there is no need to be applied for other positions
            for (int k = 0; k < iPositions; k++)
                if (positionIndex != k)
                    listOfCandidates[candidateIndex].preferences[k] = iPositions;
        }

        /// <summary>
        /// Checks the ballots to ensure they correspond to a member's code
        /// </summary>
        /// <param name="ballotList">The list of ballots</param>
        /// <returns>The list of verified ballots</returns>
        public List<ballot> checkedBallots(List<ballot> ballotList)
        {
            List<ballot> output = new List<ballot>();
            for(int i = 0; i < ballotList.Count; i++)
            {
                //if the ballot has already been verified, just break
                if (ballotList[i].bVerified)
                {
                    output.Add(ballotList[i]);
                    break;
                }
                //new ballots
                totalBallots++;
                if (ballotCodeLegit(ballotList[i]))
                    output.Add(new ballot { strCode = ballotList[i].strCode, strPositionVote = ballotList[i].strPositionVote, bVerified = true });
                else
                    rejectBallots++;
            }
            return output;
        }

        private bool ballotCodeLegit(ballot bal)
        {
            //if there is no code, reject ballot
            if(bal.strCode == "")
                return false;
            for (int i = 0; i < electorList.Count; i++)
            {
                if (bal.strCode == electorList[i].strCode)
                {
                    //remove this person's code, as they have voted
                    electorList[i] = new elector { id = electorList[i].id, strCode = "", strEmail = electorList[i].strEmail, strName = electorList[i].strName };
                    return true;
                }
            }
            //we did not find the code, so it is not legitimate
            //ballot is invalid
            return false;
        }

        private int indexOfCandidate(string strCandidate)
        {
            for(int i = 0; i < listOfCandidates.Count; i++)
                //if we find a match, return the index of it
                if (clsStorage.currentClub.firstAndLastName(listOfCandidates[i].index) == strCandidate)
                    return i;
            return -1;
        }

        /// <summary>
        /// Checks if a candidate is already registered in the election
        /// </summary>
        /// <param name="candidateIndex">The index of the candidate in the member list</param>
        /// <returns>The index of the candidate in registration. Returns -1 if not found.</returns>
        public int candidateRegistered(int candidateIndex)
        {
            for (int i = 0; i < listOfCandidates.Count; i++)
                //if we find a match, return the index of it
                if (listOfCandidates[i].index == candidateIndex)
                    return i;
            return -1;
        }

        public void countBallots(List<ballot> listOfBallots)
        {
            iElected = new int[iPositions];
            ballotTally = new int[iPositions, listOfCandidates.Count];
            for (int i = 0; i < iElected.Length; i++)
                iElected[i] = -1;

            //go through the positions
            //if acclaimed, then immediately elect said member
            //if not acclaimed, then count the ballots for that position
            for (int i = 0; i < iPositions; i++)
            {
                int checkAcclaim = acclaimedPosition(i);
                //if position is acclaimed
                if (checkAcclaim > -1)
                    iElected[i] = checkAcclaim;
                else
                {
                    foreach (ballot bal in listOfBallots)
                    {
                        int checkCandidate = indexOfCandidate(bal.strPositionVote[i]);
                        if (checkCandidate != -1)
                            ballotTally[i, checkCandidate]++;
                        //ignore any candidates entered that are not entered correctly
                    }
                }
            }

            //ballots are all counted, now we just have to pick winners
            //the first loop is to allow for people to win their not first choice
            //the second loop is to go through the positions
            for(int i = 0; i < iPositions; i++)
            {
                for(int j = 0; j < iPositions; j++)
                {
                    //confirm that no one has one this position yet
                    if (iElected[j] == -1)
                    {
                        int proposedElected = biggestUnelectedWinner(j);
                        //if the member in fact wanted this position, they now get it!
                        //congratulations!
                        if (proposedElected != -1 && listOfCandidates[proposedElected].preferences[j] <= i)
                        {
                            candidate changeElectedStatus = listOfCandidates[proposedElected];
                            changeElectedStatus.bElected = true;
                            listOfCandidates[proposedElected] = changeElectedStatus;
                            iElected[j] = proposedElected;
                        }
                        //if not, no one wins that position quite yet
                        //they might win a position they prefer more
                    }
                }              
            }

            //at this point, we should now have our list of candidates who have been elected
            //awesome!
        }

        private int biggestUnelectedWinner(int positionIndex)
        {
            int largestIndex = -1;
            int largestVotes = -1;
            for(int i = 0; i<listOfCandidates.Count; i++)
            {
                //skip candidates that have been elected
                if (listOfCandidates[i].bElected)
                    continue;
                if (largestVotes < ballotTally[positionIndex, i])
                {
                    largestIndex = i;
                    largestVotes = ballotTally[positionIndex, i];
                }
                //no one wins in the event of a tie
                else if (largestVotes == ballotTally[positionIndex, i])
                    largestIndex = -1;
            }

            return largestIndex;
        }

        public void saveElection(StreamWriter sw)
        {
            sw.WriteLine(iTermIndex);

            sw.WriteLine(iAlmostElectors);
            for (int i = 0; i < iAlmostElectors; i++)
            {
                sw.WriteLine(almostElector[i].id);
                sw.WriteLine(almostElector[i].strName);
                sw.WriteLine(almostElector[i].strEmail);
            }
            sw.WriteLine(iElectors);
            for (int i = 0; i < iElectors; i++)
            {
                sw.WriteLine(electorList[i].id);
                sw.WriteLine(electorList[i].strName);
                sw.WriteLine(electorList[i].strEmail);
                sw.WriteLine(electorList[i].strCode);
            }
            sw.WriteLine(iPositions);
            for (int i = 0; i < iPositions; i++)
                sw.WriteLine(strPositions[i]);
            sw.WriteLine(listOfCandidates.Count);
            foreach (candidate cand in listOfCandidates)
            {
                sw.WriteLine(cand.index);
                for (int i = 0; i < iPositions; i++)
                    sw.WriteLine(cand.preferences[i]);
                sw.WriteLine(clsStorage.cleanNewLine(cand.strBlurb));
                sw.WriteLine(cand.bElected);
            }
        }
    }

    public partial class createPositions : Form
    {

        public createPositions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newTerm));
            this.lblName = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();

            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(24, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(123, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "List Positions, One Per Line";

            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(78, 250);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(126, 27);
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Create Positions";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);

            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(24, 48);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(241, 192);
            this.txtName.TabIndex = 2;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 300);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "createPositions";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtName;
        public string[] strPositions;

        private void btnCreate_Click(object sender, EventArgs e)
        {
            strPositions = txtName.Text.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            this.Close();
        }
    }
}
