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
    public partial class Profile : Form
    {
        //iID is the member's ID in the club
        int iID;
        //these three are used for handling other instruments
        TextBox txtOtherInstrument;
        Label lblOtherInstrument;
        bool bOtherInstrument;
        public Profile(int iID)
        {
            InitializeComponent();
            this.iID = iID;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtStudentNumber.Text))
                txtStudentNumber.Text = "0";
            if(bOtherInstrument)
                clsStorage.currentClub.members[iID].editMember(txtFirstName.Text, txtLastName.Text, cbClass.SelectedIndex, Convert.ToUInt32(txtStudentNumber.Text),
                cbFaculty.SelectedIndex, cbInstrument.Text, txtOtherInstrument.Text, txtEmail.Text, txtOther.Text, clsStorage.currentClub.members[iID].signupTime, cbShirtSize.SelectedIndex);
            else
            clsStorage.currentClub.members[iID].editMember(txtFirstName.Text, txtLastName.Text, cbClass.SelectedIndex, Convert.ToUInt32(txtStudentNumber.Text),
                cbFaculty.SelectedIndex, cbInstrument.Text, txtEmail.Text, txtOther.Text, clsStorage.currentClub.members[iID].signupTime, cbShirtSize.SelectedIndex);
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
            MessageBox.Show("Member successfully edited");
            clsStorage.currentClub.addHistory(clsStorage.currentClub.formatedName(iID), history.changeType.editMember);
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            //load the instrument(s)
            instrumentLoad();

            //load details about member
            lblTitle.Text = clsStorage.currentClub.formatedName(iID);
            txtFirstName.Text = clsStorage.currentClub.members[iID].strFName;
            txtLastName.Text = clsStorage.currentClub.members[iID].strLName;
            txtEmail.Text = clsStorage.currentClub.members[iID].strEmail;
            txtOther.Text = clsStorage.currentClub.members[iID].strOther;
            txtStudentNumber.Text = Convert.ToString(clsStorage.currentClub.members[iID].uiStudentNumber);
            cbClass.SelectedIndex = (int)clsStorage.currentClub.members[iID].type;
            cbFaculty.SelectedIndex = (int)clsStorage.currentClub.members[iID].memberFaculty;
            cbInstrument.Text = member.instrumentToString(clsStorage.currentClub.members[iID].curInstrument);
            cbShirtSize.SelectedIndex = (int)clsStorage.currentClub.members[iID].size;
            //load history about member
            //and some fun facts :)
            //Number of terms
            int iTerms = 0;
            //these two are for tracking attendance records
            int iAttended = 0;
            int iTotal = 0;
            string strTerms = "";
            //this will be used to look for outstanding fees
            //NOTE TO SELF: perhaps a way to handle members who show up for one rehearsal, but don't come back
            //dFeesOwed calculates the total membership fees owed over all terms
            //dFeesPaid calculates the total membership fees paid over all terms (excluding discounts)
            double dFeesOwed = 0, dFeesPaid = 0;
            for (int i = 0; i < clsStorage.currentClub.sTerm; i++)
            {
                int iTermIndex = clsStorage.currentClub.terms[i].memberSearch(Convert.ToInt16(iID));
                if (iTermIndex != -1 && !clsStorage.currentClub.terms[i].checkLimbo(iTermIndex))
                {
                    //count number of terms
                    iTerms++;
                    //count attendance records
                    iTotal += clsStorage.currentClub.terms[i].sRehearsals;
                    iAttended += clsStorage.currentClub.terms[i].iMemberAttendance(clsStorage.currentClub.terms[i].memberSearch(Convert.ToInt16(iID)));
                    strTerms += String.Format(" - {0} \r\n", clsStorage.currentClub.terms[i].strName);
                    dFeesOwed += clsStorage.currentClub.terms[i].membershipFees;
                }
                else if (iTermIndex != -1) //limbo member
                {
                    strTerms += String.Format(" - {0} (not full member)\r\n", clsStorage.currentClub.terms[i].strName);
                }
            }
            lblHistory.Text = String.Format("Number of terms: {0}\r\n{1}", iTerms, strTerms);
            lblHistory.Text += String.Format("Attended {0} of {1} rehearsals. Attendance percentage: {2}%\r\n",
                iAttended, iTotal, Math.Round(Convert.ToDouble(iAttended) / iTotal * 100, 2));
            //Fees paid
            //also check if all membership fees have been paid
            double dTotal = 0;
            string strFees = "";
            for(int i = 0; i<clsStorage.currentClub.sTerm;i++)
            {
                //check if member is even in term
                int iTermIndex = clsStorage.currentClub.terms[i].memberSearch(Convert.ToInt16(iID));
                if (iTermIndex != -1)
                {
                    //membership fee
                    if (clsStorage.currentClub.terms[i].feesPaid[iTermIndex, 0] != 0)
                    {
                        dTotal += clsStorage.currentClub.terms[i].feesPaid[iTermIndex, 0];
                        strFees += String.Format("{0}\r\n- Membership Fee - ${1}\r\n", clsStorage.currentClub.terms[i].strName,
                            clsStorage.currentClub.terms[i].feesPaid[iTermIndex, 0]);
                        //mark fees paid
                        //regardless of if there was a discount, add the whole membership fee
                        //this is to calculate any outstanding membership fees
                        dFeesPaid += clsStorage.currentClub.terms[i].membershipFees;
                    }
                    //other fees
                    //we assume the membership fee is the first fee to be paid
                    for (int j = 0; j < clsStorage.currentClub.terms[i].iOtherFees; j++)
                    {
                        if (clsStorage.currentClub.terms[i].feesPaid[iTermIndex, j + 1] != 0)
                        {
                            dTotal += clsStorage.currentClub.terms[i].feesPaid[iTermIndex, j + 1];
                            strFees += String.Format("- {0} - ${1}\r\n", clsStorage.currentClub.terms[i].strOtherFees[j],
                                clsStorage.currentClub.terms[i].feesPaid[iTermIndex, j + 1]);
                        }
                    }
                }
            }
            lblHistory.Text += String.Format("\r\nTotal Fees Paid: ${0}\r\n{1}", dTotal, strFees);
            if (dFeesOwed <= dFeesPaid)
                lblHistory.Text += "All Membership Fees Paid";
            else
                lblHistory.Text += "Membership Fees Outstanding, $ " + Convert.ToString(dFeesOwed-dFeesPaid);


            //this used to be a problem, but doesn't seem to be anymore...
            //ugh... needs more debugging

            /*
            //check for and handle other instruments
            //it doesn't seem to load earlier, and I can't figure out why
            //this is a nice alternative to tracking down that bug
            if (clsStorage.currentClub.members[iID].curInstrument == member.instrument.other || (clsStorage.currentClub.members[iID].bMultipleInstruments &&
                clsStorage.currentClub.members[iID].playsInstrument[(int)member.instrument.other]))
            {
                showOther();
                txtOtherInstrument.Text = clsStorage.currentClub.members[iID].strOtherInstrument;
            }*/
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            //here we are merely deactivating the member
            //this is for people we would like to keep record of for UWCBC glory
            //but no longer need to receive e-mails
            clsStorage.currentClub.addHistory(clsStorage.currentClub.formatedName(iID), history.changeType.deactivate);
            clsStorage.currentClub.members[iID].editMember(clsStorage.currentClub.members[iID].strFName, clsStorage.currentClub.members[iID].strLName,
                (int)clsStorage.currentClub.members[iID].type, clsStorage.currentClub.members[iID].uiStudentNumber,
                (int)clsStorage.currentClub.members[iID].memberFaculty, clsStorage.currentClub.members[iID].strOtherInstrument,
                "", "",
                clsStorage.currentClub.members[iID].signupTime, -1);
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
            MessageBox.Show("Member deactivated.");            
        }

        private void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            //the reason for not deleting a member is to not mess up every single member's profile number
            //as well as all the term information if the unsubscribe actually attended rehearsals
            //instead, we remove everything we had and make that data anonymous

            clsStorage.currentClub.addHistory(clsStorage.currentClub.formatedName(iID), history.changeType.unsubscribe);
            clsStorage.currentClub.members[iID].editMember("♪Unsubscribed", "♪Unsubscribed", (int)member.membertype.Other, 0, -1,
                "","", "", clsStorage.currentClub.members[iID].signupTime, -1);
            if (Properties.Settings.Default.playSounds)
                sound.success.Play();
            MessageBox.Show("Member unsubscribed.");
            this.Close();
        }

        private void cbMultiple_CheckedChanged(object sender, EventArgs e)
        {
            //if we are now allowing multiple instruments, give the user the ability to change the instruments
            if(cbMultiple.Checked)
            {
                btnEditMultiple.Visible = true;
                if (!clsStorage.currentClub.members[iID].bMultipleInstruments)
                    btnEditMultiple_Click(sender, e);
            }
            //if removing the instruments, change that too
            else
            {
                btnEditMultiple.Visible = false;
            }
        }

        private void btnEditMultiple_Click(object sender, EventArgs e)
        {
            editMultiInstruments emiEdit = new editMultiInstruments(iID);
            emiEdit.ShowDialog();
            //the member's instruments may have changed, so just reload it all
            instrumentLoad();
        }

        void instrumentLoad()
        {
            //add the instruments to the combo box
            cbInstrument.BeginUpdate();
            List<string> listInstruments = new List<string>();

            cbInstrument.Items.Clear();
            //if the member does not play multiple instruments, just give the regular list
            //otherwise, limit the list to instruments the member actually plays
            if (!clsStorage.currentClub.members[iID].bMultipleInstruments)
            {
                foreach (member.instrument instrument in Enum.GetValues(typeof(member.instrument)))
                    listInstruments.Add(member.instrumentToString(instrument));
            }
            else
            {
                int numberOfInstruments = Enum.GetValues(typeof(member.instrument)).Length;
                for (int i = 0; i < numberOfInstruments; i++)
                    if (clsStorage.currentClub.members[iID].playsInstrument[i])
                        listInstruments.Add(member.instrumentToString((member.instrument)i));
            }
            listInstruments.Sort();
            cbInstrument.Items.AddRange(listInstruments.ToArray());
            cbInstrument.EndUpdate();

            //check for multiple instrument player
            if (clsStorage.currentClub.members[iID].bMultipleInstruments)
                cbMultiple.Checked = true;
            else
                cbMultiple.Checked = false;

            //check for and handle other instruments
            if (clsStorage.currentClub.members[iID].curInstrument == member.instrument.other || (clsStorage.currentClub.members[iID].bMultipleInstruments &&
                clsStorage.currentClub.members[iID].playsInstrument[(int)member.instrument.other]))
            {
                showOther();
                txtOtherInstrument.Text = clsStorage.currentClub.members[iID].strOtherInstrument;
            }
        }

        void showOther()
        {
            //first create the textbox and label, then move everything out of the way
            this.Height += 42;
            tcMain.Height += 42;
            txtOtherInstrument = new TextBox();
            lblOtherInstrument = new Label();
            lblOtherInstrument.AutoSize = true;
            lblOtherInstrument.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblOtherInstrument.Location = new System.Drawing.Point(lblEmail.Location.X, lblEmail.Location.Y);
            lblOtherInstrument.Size = new System.Drawing.Size(95, 18);
            lblOtherInstrument.Text = "Other Instrument";
            txtOtherInstrument.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtOtherInstrument.Location = new Point(txtEmail.Location.X, txtEmail.Location.Y);
            txtOtherInstrument.Size = new System.Drawing.Size(302, 24);
            txtOtherInstrument.TabIndex = 8;

            //now, move everything out of the way
            //move everything else
            lblEmail.Location = new Point(lblEmail.Location.X, lblEmail.Location.Y + 42);
            txtEmail.Location = new Point(txtEmail.Location.X, txtEmail.Location.Y + 42);
            txtEmail.TabIndex++;
            lblOther.Location = new Point(lblOther.Location.X, lblOther.Location.Y + 42);
            txtOther.Location = new Point(txtOther.Location.X, txtOther.Location.Y + 42);
            txtOther.TabIndex++;
            lblShirtSize.Location = new Point(lblShirtSize.Location.X, lblShirtSize.Location.Y + 42);
            cbShirtSize.Location = new Point(cbShirtSize.Location.X, cbShirtSize.Location.Y + 42);
            cbShirtSize.TabIndex++;

            //now add the two controls to the form
            tpDetails.Controls.Add(lblOtherInstrument);
            tpDetails.Controls.Add(txtOtherInstrument);

            //mark other has being selected
            bOtherInstrument = true;
        }

        void hideOther()
        {
            lblOtherInstrument.Dispose();
            txtOtherInstrument.Dispose();
            lblEmail.Location = new Point(lblEmail.Location.X, lblEmail.Location.Y - 42);
            txtEmail.Location = new Point(txtEmail.Location.X, txtEmail.Location.Y - 42);
            txtEmail.TabIndex--;
            lblOther.Location = new Point(lblOther.Location.X, lblOther.Location.Y - 42);
            txtOther.Location = new Point(txtOther.Location.X, txtOther.Location.Y - 42);
            txtOther.TabIndex--;
            lblShirtSize.Location = new Point(lblShirtSize.Location.X, lblShirtSize.Location.Y - 42);
            cbShirtSize.Location = new Point(cbShirtSize.Location.X, cbShirtSize.Location.Y - 42);
            cbShirtSize.TabIndex--;
            this.Height -= 42;
            tcMain.Height -= 42;

            //mark as other not being selected
            bOtherInstrument = false;
        }

        private void cbInstrument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInstrument.Text == "Other" && !bOtherInstrument)
                showOther();
            //confirm that member does not play "other" instrument
            else if (cbInstrument.Text != "Other" && (!clsStorage.currentClub.members[iID].bMultipleInstruments || !clsStorage.currentClub.members[iID].playsInstrument[(int)member.instrument.other])
                && bOtherInstrument)
                hideOther();
        }
    }
}
