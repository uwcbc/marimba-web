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
    public partial class addFees : Form
    {
        List<ListViewItem> memberlist;

        public addFees()
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
                if(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees > 0)
                    cbFee.Items.AddRange(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].strOtherFees);
                //bold rehearsal dates on the calendar
                mcDate.RemoveAllBoldedDates();
                mcDate.BoldedDates = clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].rehearsalDates;
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
            cbTerm.Items.AddRange(clsStorage.currentClub.termNames());
            memberlist = new List<ListViewItem>();
            lvMembers.SmallImageList = Program.home.instrumentSmall;
            //if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = clsStorage.currentClub.listTerms.Count - 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //check that all relevant information was provided
                if (cbTerm.Text == "" || cbFee.Text == "" || txtAmount.Text == "" || lvMembers.CheckedItems.Count == 0)
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
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
                        clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[memberIndex, cbFee.SelectedIndex] += Convert.ToDouble(txtAmount.Text);
                        clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaidDate[memberIndex, cbFee.SelectedIndex] = mcDate.SelectionStart;
                        //next, add an item in the budget
                        strComment += clsStorage.currentClub.formatedName(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]) + ";";
                        memberNames[i] = clsStorage.currentClub.formatedName(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]);
                        memberEmails[i] = clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[memberIndex]].strEmail;
                    }
                    //remove the last semicolon
                    strComment = strComment.Remove(strComment.Length - 1);
                    //Note: For now, assuming date of collection and deposit are the same for membership fees
                    //Obviously this isn't true
                    //I will need to address this later
                    clsStorage.currentClub.addBudget(Convert.ToDouble(txtAmount.Text) * length,
                        cbFee.Text,
                        mcDate.SelectionStart,
                        mcDate.SelectionStart,
                        cbFee.Text,
                        Enumerations.TransactionType.Revenue,
                        cbTerm.SelectedIndex,
                        strComment);
                    //send digital receipts
                    if(checkReceipt.Checked)
                    {
                        //create message
                        string strBody;
                        strBody = string.Format("<html>{0}{1}{2}<br><br>Processed by: {3}<br>Fee Paid: {4}<br>Amount Paid: ${5}<br>Date Paid: {6}<br><br>{7}</html>", clsStorage.receiptMessage[0],
                            cbTerm.Text,clsStorage.receiptMessage[1],clsStorage.currentClub.strCurrentUser, cbFee.Text,txtAmount.Text,mcDate.SelectionStart.ToShortDateString(), clsStorage.receiptMessage[2]);

                        if (Properties.Settings.Default.attachSig)
                            strBody += "<br>" + clsStorage.currentClub.clubEmail.createSignature();

                        bool success = true;
                        while (!clsStorage.currentClub.clubEmail.sendMessage(memberEmails, memberNames, clsStorage.currentClub.strName + " " + clsStorage.receiptSubject, strBody, Enumerations.EmailPurpose.Bcc))
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
                        sound.success.Play();
                    MessageBox.Show("Successfully added all fees.");
                    clsStorage.currentClub.addHistory(Convert.ToString(lvMembers.CheckedItems.Count), Enumerations.ChangeType.AddFees);
                    clearall();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
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
                    object[,] output = new object[1 + clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].sMembers, 5 + clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees];
                    output[0, 0] = "First Name";
                    output[0, 1] = "Last Name";
                    output[0, 2] = "Instrument";
                    output[0, 3] = "Member Status";
                    output[0, 4] = "Membership Fee";
                    for (int i = 0; i < clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees; i++)
                        output[0, 5 + i] = clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].strOtherFees[i];
                    for (int i = 0; i < clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].sMembers; i++)
                    {
                        output[i + 1, 0] = clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strFName;
                        output[i + 1, 1] = clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strLName;
                        if (clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument == member.instrument.other)
                            output[i + 1, 2] = clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strOtherInstrument;
                        else
                            output[i + 1, 2] = member.instrumentToString(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument);
                        if (clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].checkLimbo(i))
                            output[i + 1, 3] = "Inactive";
                        else
                            output[i + 1, 3] = "Active";
                        for (int j = 0; j <= clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees; j++)
                            output[i + 1, 4+j] = clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, j].ToString("C");
                    }
                    //now that the string array is set up, save it
                    excelFile.saveExcelRowHighlight(output, svdSave.FileName,
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
                        for (int i = 0; i < clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees; i++)
                            firstrow.Add(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].strOtherFees[i]);
                        writer.WriteRow(firstrow);
                        //add all of the member details
                        for (int i = 0; i < clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].sMembers; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strFName);
                            row.Add(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strLName);
                            if (clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument == member.instrument.other)
                                row.Add(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].strOtherInstrument);
                            else
                                row.Add(member.instrumentToString(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument));
                            //report limbo status
                            //for reporting purposes, we'll call it "active" or "inactive"
                            if (clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].checkLimbo(i))
                                row.Add("Inactive");
                            else
                                row.Add("Active");
                            for (int j = 0; j <= clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].iOtherFees; j++)
                                row.Add("$ " + Convert.ToString(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, j]));
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
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
                txtAmount.Text = Convert.ToString(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].membershipFees);
            //other fee
            else
                txtAmount.Text = Convert.ToString(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].dOtherFees[cbFee.SelectedIndex-1]);
            lvMembers.BeginUpdate();
            lvMembers.Items.Clear();
            memberlist.Clear();
            if(cbTerm.SelectedIndex >=0)
            {
                for(int i = 0; i<clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].sMembers;i++)
                {
                    if (clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].feesPaid[i, cbFee.SelectedIndex] == 0)
                        memberlist.Add(new ListViewItem(new string[2]{clsStorage.currentClub.formatedName(clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]),
                            Convert.ToString(i)}, member.instrumentIconIndex(clsStorage.currentClub.members[clsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[i]].curInstrument)));
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
