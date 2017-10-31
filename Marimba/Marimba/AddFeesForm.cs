using Marimba.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Marimba
{
    public partial class AddFeesForm : Form
    {
        List<ListViewItem> memberlist;

        public AddFeesForm()
        {
            InitializeComponent();            
        }

        private void cbTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvMembers.Items.Clear();
            cbFee.Text = "";
            cbFee.SelectedIndex = -1;
            cbFee.Items.Clear();
            if (cbTerm.SelectedIndex >= 0) {
                cbFee.Items.Add("Membership Fee");
                if(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees > 0)
                    cbFee.Items.AddRange(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].otherFeesNames);
                //bold rehearsal dates on the calendar
                mcDate.RemoveAllBoldedDates();
                mcDate.BoldedDates = ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].rehearsalDates;
                //allow exporting
                btnExport.Enabled = true;
            }
            btnExport.Enabled = false;
        }

        private void addFees_Load(object sender, EventArgs e)
        {
            cbTerm.Text = "";
            cbTerm.SelectedIndex = -1;
            cbTerm.Items.Clear();
            cbFee.Text = "";
            cbFee.SelectedIndex = -1;
            cbFee.Items.Clear();
            cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());
            memberlist = new List<ListViewItem>();
            lvMembers.SmallImageList = Program.home.instrumentSmall;
            //if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //check that all relevant information was provided
                if (cbTerm.Text == "" || cbFee.Text == "" || txtAmount.Text == "" || lvMembers.CheckedItems.Count == 0)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("Adding fees failed. Please make sure all information was filled out.");
                }
                else
                {
                    int length = lvMembers.CheckedItems.Count;
                    //these two arrays are used for sending digital receipts
                    string[] memberNames = new string[length];
                    string[] memberEmails = new string[length];
                    int memberIndex;
                    string strComment;
                    if (length == 1)
                        strComment = "1 payment of $" + txtAmount.Text + ": ";
                    else
                        strComment = Convert.ToString(length) + " payments of $" + txtAmount.Text + ": ";
                    for (int i = 0; i < length; i++)
                    {
                        memberIndex = Convert.ToInt32(lvMembers.CheckedItems[i].SubItems[1].Text);
                        //first, mark in the term that the fees have been paid
                        //this effectively adds the amount the member paid
                        //currently, it does not check if a member has already paid
                        ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[memberIndex, cbFee.SelectedIndex] += Convert.ToDouble(txtAmount.Text);
                        ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaidDate[memberIndex, cbFee.SelectedIndex] = mcDate.SelectionStart;
                        //next, add an item in the budget
                        strComment += ClsStorage.currentClub.GetFormattedName(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]) + ";";
                        memberNames[i] = ClsStorage.currentClub.GetFormattedName(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]);
                        memberEmails[i] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]].email;
                    }
                    //remove the last semicolon
                    strComment = strComment.Remove(strComment.Length - 1);
                    //Note: For now, assuming date of collection and deposit are the same for membership fees
                    //Obviously this isn't true
                    //I will need to address this later
                    ClsStorage.currentClub.AddBudget(Convert.ToDouble(txtAmount.Text) * length,
                        cbFee.Text,
                        mcDate.SelectionStart,
                        mcDate.SelectionStart,
                        cbFee.Text,
                        TransactionType.Revenue,
                        cbTerm.SelectedIndex,
                        strComment);
                    //send digital receipts
                    if(checkReceipt.Checked)
                    {
                        //create message
                        string strBody;
                        strBody = string.Format("<html>{0}{1}{2}<br><br>Processed by: {3}<br>Fee Paid: {4}<br>Amount Paid: ${5}<br>Date Paid: {6}<br><br>{7}</html>", ClsStorage.receiptMessage[0],
                            cbTerm.Text, ClsStorage.receiptMessage[1], ClsStorage.currentClub.strCurrentUser, cbFee.Text, txtAmount.Text, mcDate.SelectionStart.ToShortDateString(), ClsStorage.receiptMessage[2]);

                        if (Properties.Settings.Default.attachSig)
                            strBody += "<br>" + ClsStorage.currentClub.clubEmail.CreateSignature();

                        bool success = true;
                        while (!ClsStorage.currentClub.clubEmail.SendMessage(memberEmails, memberNames, ClsStorage.currentClub.strName + " " + ClsStorage.receiptSubject, strBody, EmailPurpose.Bcc))
                        {
                            DialogResult result = MessageBox.Show("Sending digital receipts failed. Would you like to try again?", "Failed to send digital receipts", MessageBoxButtons.YesNo);
                            
                            // don't try again and we didn't suceed
                            if (result == DialogResult.No)
                            {
                                success = false;
                                break;
                            }
                            else // try again
                            {
                                continue;
                            }
                        }

                        if (success)
                        {
                            MessageBox.Show("Digital receipts sent.");
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                    MessageBox.Show("Successfully added all fees.");
                    ClsStorage.currentClub.AddHistory(Convert.ToString(lvMembers.CheckedItems.Count), ChangeType.AddFees);
                    clearall();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Adding fees failed. Please make sure the amount entered is a proper number.");
            }
        }

        void clearall()
        {
            txtAmount.Text = "";
            cbFee.Text = "";
            mcDate.SetDate(DateTime.Today);
            lvMembers.Items.Clear();
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            //export the membership fees list for the term
            if (svdSave.ShowDialog() == DialogResult.OK)
            {
                //1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    //first, set up string array to be sent to Excel
                    object[,] output = new object[1 + ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers, 5 + ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees];
                    output[0, 0] = "First Name";
                    output[0, 1] = "Last Name";
                    output[0, 2] = "Instrument";
                    output[0, 3] = "Member Status";
                    output[0, 4] = "Membership Fee";
                    for (int i = 0; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees; i++)
                        output[0, 5 + i] = ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].otherFeesNames[i];
                    for (int i = 0; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers; i++)
                    {
                        output[i + 1, 0] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].firstName;
                        output[i + 1, 1] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].lastName;
                        if (ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument == Member.Instrument.Other)
                            output[i + 1, 2] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].otherInstrument;
                        else
                            output[i + 1, 2] = Member.instrumentToString(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument);
                        if (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].checkLimbo(i))
                            output[i + 1, 3] = "Inactive";
                        else
                            output[i + 1, 3] = "Active";
                        for (int j = 0; j <= ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees; j++)
                            output[i + 1, 4 + j] = ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, j].ToString("C");
                    }
                    //now that the string array is set up, save it
                    ExcelFile.saveExcelRowHighlight(output, svdSave.FileName,
                        new List<ExcelHighlightingInfo> {
                            new ExcelHighlightingInfo { column = 5, matchExpression = "$0.00", colour = System.Drawing.Color.Yellow },
                            new ExcelHighlightingInfo { column = 4, matchExpression = "Inactive", colour = System.Drawing.Color.Gray }
                        });
                }
                else if (svdSave.FilterIndex == 2)
                {
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("First Name");
                        firstrow.Add("Last Name");
                        firstrow.Add("Instrument");
                        firstrow.Add("Member Status");
                        firstrow.Add("Membership Fee");
                        //add the names of the other fees
                        for (int i = 0; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees; i++)
                            firstrow.Add(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].otherFeesNames[i]);
                        writer.WriteRow(firstrow);
                        //add all of the member details
                        for (int i = 0; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].firstName);
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].lastName);
                            if (ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument == Member.Instrument.Other)
                                row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].otherInstrument);
                            else
                                row.Add(Member.instrumentToString(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument));
                            //report limbo status
                            //for reporting purposes, we'll call it "active" or "inactive"
                            if (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].checkLimbo(i))
                                row.Add("Inactive");
                            else
                                row.Add("Active");
                            for (int j = 0; j <= ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numOtherFees; j++)
                                row.Add("$ " + Convert.ToString(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, j]));
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
        }

        private void cbFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //membership fee
            if (cbFee.SelectedIndex < 0)
            {
                return;
            }

            if (cbFee.SelectedIndex == 0)
                txtAmount.Text = Convert.ToString(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].membershipFees);
            //other fee
            else
                txtAmount.Text = Convert.ToString(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].otherFeesAmounts[cbFee.SelectedIndex-1]);
            lvMembers.BeginUpdate();
            lvMembers.Items.Clear();
            memberlist.Clear();
            if(cbTerm.SelectedIndex >=0)
            {
                for(int i = 0; i<ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers;i++)
                {
                    if (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, cbFee.SelectedIndex] == 0)
                        memberlist.Add(new ListViewItem(new string[2]{ClsStorage.currentClub.GetFormattedName(ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]),
                            Convert.ToString(i)}, Member.instrumentIconIndex(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument)));
                }
                lvMembers.Items.AddRange(memberlist.ToArray());
                lvMembers.Sort();
                lvMembers.EndUpdate();
            }
        }

        private void lvMembers_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvMembers.SelectedItems.Count != 0)
                lvMembers.SelectedItems[0].Checked = !lvMembers.SelectedItems[0].Checked;
        }
    }
}
