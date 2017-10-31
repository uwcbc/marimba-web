﻿namespace Marimba
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

    public partial class Attendance : Form
    {
        bool attendancechanges = false;
        int oldTermIndex = -1;
        ColumnHeader[] lvColumns;
        public ListViewColumnSorter lvmColumnSorter;

        public Attendance()
        {
            InitializeComponent();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
            // add columns to listview
            // to improve performance, we are fixing the number of headers rather than make it dynamic
            // assume no more than eleven rehearsals
            // the first one is to count a member's attendance
            lvAttendance.SmallImageList = Program.home.instrumentSmall;
            setLvColumns(13);
            // this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());
            lvmColumnSorter = new ListViewColumnSorter();
            this.lvAttendance.ListViewItemSorter = lvmColumnSorter;

            // if we default to selecting current term, do so!
            if (Properties.Settings.Default.selectCurrentTerm)
                cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
        }

        private void setLvColumns(int iColumns)
        {
            lvColumns = new ColumnHeader[iColumns];
            for (int i = 0; i < iColumns; i++)
            {
                lvColumns[i] = (System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader());
                lvColumns[i].Text = String.Empty;
                lvColumns[i].Width = 100;
            }
            lvColumns[0].Width = 300; // Name of attendee
            lvColumns[1].Width = 30; // Total number of rehearsals attended
            this.lvAttendance.Columns.AddRange(lvColumns);
        }

        /*
         * Call this function to create a ListViewItem for club member "memberTermID" of term "iTerm" to "list"
         * Use "lastRehearsal" in order to determine formatting
         */
        private void loadMemberToAttendanceList(List<ListViewItem> list, int iTerm, int memberTermID, int lastRehearsal)
        {

            /* load information about the member into a string array */
            string[] attendance = new string[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 3];
            attendance[0] = ClsStorage.currentClub.GetFormattedName(ClsStorage.currentClub.listTerms[iTerm].members[memberTermID]);
            attendance[1] = Convert.ToString(ClsStorage.currentClub.listTerms[iTerm].iMemberAttendance(memberTermID));
            attendance[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2] = Convert.ToString(memberTermID);
            bool[] memberAttendance = ClsStorage.currentClub.listTerms[iTerm].memberAttendance(memberTermID);
            for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; i++)
            {
                if (memberAttendance[i])
                {
                    attendance[i + 2] = "Y";
                }
                else
                {
                    attendance[i + 2] = "N";
                }
            }

            /* actually add it to the list, and format it */
            list.Add(new ListViewItem(attendance, Member.instrumentIconIndex(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[memberTermID]].curInstrument)));
            list.Last().UseItemStyleForSubItems = false;
            for (int i = 2; i <= ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 1; i++)
            {
                // member was here
                if (memberAttendance[i - 2])
                    list.Last().SubItems[i].BackColor = Color.Green;
                else if (i - 2 <= lastRehearsal)
                    // member was not here
                    list.Last().SubItems[i].BackColor = Color.Red;
                else
                    // rehearsal has not happened yet
                    list.Last().SubItems[i].BackColor = SystemColors.Control;
            }

            /* hide term index from view */
            list.Last().SubItems[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2].ForeColor = SystemColors.Window;
        }

        private void cbTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if attendance changes were made, record them
            if (attendancechanges)
            {
                ClsStorage.currentClub.AddHistory(ClsStorage.currentClub.listTerms[oldTermIndex].strName, ChangeType.EditAttendance);
                attendancechanges = false;
            }
            oldTermIndex = cbTerm.SelectedIndex;

            // these two lines reset any sorting done if we changes terms
            // this is necessary so the backcoloring turns out correct

            List<ListViewItem> attendanceList = new List<ListViewItem>();

            lvmColumnSorter = new ListViewColumnSorter();
            this.lvAttendance.ListViewItemSorter = lvmColumnSorter;
            lvAttendance.BeginUpdate();
            lvAttendance.Items.Clear();
            int iTerm = cbTerm.SelectedIndex;
            lvAttendance.Columns.Clear();
            setLvColumns(ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 3);
            string[] attendance = new string[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 3];
            attendance[0] = String.Empty;
            attendance[1] = String.Empty;
            attendance[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2] = String.Empty;

            // set up first two rows, list of dates
            for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; i++)
                // date
                lvAttendance.Columns[i + 2].Text = ClsStorage.currentClub.listTerms[iTerm].rehearsalDates[i].ToShortDateString();

            // number of attendances on that date
            for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; i++)
                attendance[i + 2] = Convert.ToString(ClsStorage.currentClub.listTerms[iTerm].iRehearsalAttendance(i));

            attendanceList.Add(new ListViewItem(attendance));

            // now do the main attendance record
            int lastRehearsal = ClsStorage.currentClub.listTerms[iTerm].recentRehearsal(DateTime.Today);
            for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numMembers; i++)
            {
                loadMemberToAttendanceList(attendanceList, iTerm, i, lastRehearsal);
            }
            lvAttendance.Items.AddRange(attendanceList.ToArray());
            lvAttendance.EndUpdate();

            this.lblTotalRehearsals.Text = "Total Rehearsals: " + ClsStorage.currentClub.listTerms[iTerm].numRehearsals.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            int iTerm = cbTerm.SelectedIndex;
            if (iTerm == -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Please select a term to export.");
            }
            else if (iTerm != -1 && svdSave.ShowDialog() == DialogResult.OK)
            {
                // 1 = excel file
                if (svdSave.FilterIndex == 1)
                {
                    // first, set up string array to be sent to Excel
                    object[,] output = new object[1 + ClsStorage.currentClub.listTerms[iTerm].numMembers, 6 + ClsStorage.currentClub.listTerms[iTerm].numRehearsals];
                    output[0, 0] = "First Name";
                    output[0, 1] = "Last Name";
                    output[0, 2] = "Student ID";
                    output[0, 3] = "Instrument";
                    output[0, 4] = "E-Mail Address";
                    output[0, 5] = "Shirt Size";
                    for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; i++)
                        output[0, 6 + i] = ClsStorage.currentClub.listTerms[iTerm].rehearsalDates[i];
                    for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numMembers; i++)
                    {
                        output[i + 1, 0] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].firstName;
                        output[i + 1, 1] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].lastName;
                        output[i + 1, 2] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].uiStudentNumber;
                        if (ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].curInstrument == Member.Instrument.Other)
                            output[i + 1, 3] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].otherInstrument;
                        else
                            output[i + 1, 3] = Member.instrumentToString(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].curInstrument);
                        output[i + 1, 4] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].email;
                        output[i + 1, 5] = ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].size.ToString();
                        for (int j = 0; j < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; j++)
                        {
                            if (ClsStorage.currentClub.listTerms[iTerm].attendance[i, j])
                                output[i + 1, 6 + j] = "Y";
                            else
                                output[i + 1, 6 + j] = "N";
                        }
                    }
                    // now that the string array is set up, save it
                    ExcelFile.saveExcel(output, svdSave.FileName, "dd/MM/yyyy");
                }
                else if (svdSave.FilterIndex == 2)
                {
                    // 2 = csv file
                    using (CsvFileWriter writer = new CsvFileWriter(svdSave.FileName))
                    {
                        CsvRow firstrow = new CsvRow();
                        firstrow.Add("First Name");
                        firstrow.Add("Last Name");
                        firstrow.Add("Student ID");
                        firstrow.Add("Instrument");
                        firstrow.Add("E-Mail Address");
                        firstrow.Add("Shirt Size");
                        for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; i++)
                            firstrow.Add(ClsStorage.currentClub.listTerms[iTerm].rehearsalDates[i].ToLongDateString());
                        writer.WriteRow(firstrow);
                        for (int i = 0; i < ClsStorage.currentClub.listTerms[iTerm].numMembers; i++)
                        {
                            CsvRow row = new CsvRow();
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].firstName);
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].lastName);
                            row.Add(Convert.ToString(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].uiStudentNumber));
                            if (ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].curInstrument == Member.Instrument.Other)
                                row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].otherInstrument);
                            else
                                row.Add(Member.instrumentToString(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].curInstrument));
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].email);
                            row.Add(ClsStorage.currentClub.members[ClsStorage.currentClub.listTerms[iTerm].members[i]].size.ToString());
                            for (int j = 0; j < ClsStorage.currentClub.listTerms[iTerm].numRehearsals; j++)
                            {
                                if (ClsStorage.currentClub.listTerms[iTerm].attendance[i, j])
                                    row.Add("Y");
                                else
                                    row.Add("N");
                            }
                            writer.WriteRow(row);
                        }
                    }
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
            }
        }

        private void lvAttendance_MouseDown(object sender, MouseEventArgs e)
        {
            int iTerm = cbTerm.SelectedIndex;
            // figure out which rehearsal was clicked
            Point mousePosition = lvAttendance.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hit = lvAttendance.HitTest(mousePosition);
            // make sure something exists that was clicked
            if (hit.Item != null)
            {
                int rowindex = hit.Item.Index;
                int columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);
                // another existence check
                if (columnindex >= 2 && rowindex != -1 && columnindex < ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2)
                {
                    lvAttendance.BeginUpdate();
                    // check if the rehearsal date has past
                    if ((ClsStorage.currentClub.listTerms[iTerm].rehearsalDates[columnindex - 2] - DateTime.Today).Days <= 0)
                    {
                        // if member attended rehearsal
                        if (lvAttendance.Items[rowindex].SubItems[columnindex].Text == "Y")
                        {
                            int userIndex = Convert.ToInt32(lvAttendance.Items[rowindex].SubItems[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2].Text);
                            if (Properties.Settings.Default.playSounds)
                                Sound.Hover.Play();
                            lvAttendance.Items[rowindex].SubItems[columnindex].Text = "N";
                            lvAttendance.Items[rowindex].SubItems[columnindex].BackColor = Color.Red;
                            ClsStorage.currentClub.listTerms[iTerm].attendance[userIndex, columnindex - 2] = !ClsStorage.currentClub.listTerms[iTerm].attendance[userIndex, columnindex - 2];
                            lvAttendance.Items[rowindex].SubItems[1].Text = Convert.ToString(Convert.ToInt32(lvAttendance.Items[rowindex].SubItems[1].Text) - 1);
                            // check how the columns are currently sorted
                            // if sorted descending, handle appropriately
                            if (lvAttendance.Items[0].SubItems[columnindex].Text == "Y" || lvAttendance.Items[0].SubItems[columnindex].Text == "N")
                                lvAttendance.Items[ClsStorage.currentClub.listTerms[iTerm].numMembers].SubItems[columnindex].Text = 
                                    Convert.ToString(Convert.ToInt32(lvAttendance.Items[ClsStorage.currentClub.listTerms[iTerm].numMembers].SubItems[columnindex].Text) - 1);
                            else
                                lvAttendance.Items[0].SubItems[columnindex].Text = Convert.ToString(Convert.ToInt32(lvAttendance.Items[0].SubItems[columnindex].Text) - 1);
                            // mark unsaved changes
                            attendancechanges = true;
                        }
                        else if (lvAttendance.Items[rowindex].SubItems[columnindex].Text == "N")
                        {
                            int userIndex = Convert.ToInt32(lvAttendance.Items[rowindex].SubItems[ClsStorage.currentClub.listTerms[iTerm].numRehearsals + 2].Text);
                            if (Properties.Settings.Default.playSounds)
                                Sound.Hover.Play();
                            lvAttendance.Items[rowindex].SubItems[columnindex].Text = "Y";
                            lvAttendance.Items[rowindex].SubItems[columnindex].BackColor = Color.Green;
                            // this is a complicated line, but is what changes the attendance record
                            ClsStorage.currentClub.listTerms[iTerm].attendance[userIndex, columnindex - 2] = !ClsStorage.currentClub.listTerms[iTerm].attendance[userIndex, columnindex - 2];
                            lvAttendance.Items[rowindex].SubItems[1].Text = Convert.ToString(Convert.ToInt32(lvAttendance.Items[rowindex].SubItems[1].Text) + 1);
                            // check how the columns are currently sorted
                            // if sorted descending, handle appropriately
                            if (lvAttendance.Items[0].SubItems[columnindex].Text == "Y" || lvAttendance.Items[0].SubItems[columnindex].Text == "N")
                                lvAttendance.Items[ClsStorage.currentClub.listTerms[iTerm].numMembers].SubItems[columnindex].Text =
                                    Convert.ToString(Convert.ToInt32(lvAttendance.Items[ClsStorage.currentClub.listTerms[iTerm].numMembers].SubItems[columnindex].Text) + 1);
                            else
                                lvAttendance.Items[0].SubItems[columnindex].Text = Convert.ToString(Convert.ToInt32(lvAttendance.Items[0].SubItems[columnindex].Text) + 1);
                            attendancechanges = true;
                        }
                    }
                    lvAttendance.EndUpdate();
                }
            }
        }

        private void Attendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if attendance changes were made, record them
            if (attendancechanges)
            {
                ClsStorage.currentClub.AddHistory(ClsStorage.currentClub.listTerms[oldTermIndex].strName, ChangeType.EditAttendance);
                attendancechanges = false;
            }
        }

        private void lvAttendance_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvmColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                // do not allow the date columns to be descending sorted
                if (lvmColumnSorter.Order == SortOrder.Ascending)
                {
                    lvmColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvmColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvmColumnSorter.SortColumn = e.Column;
                lvmColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvAttendance.Sort();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
        }

        private void lvAttendance_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // something is selected
            if (lvAttendance.SelectedIndices[0] != -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();
                // insert pop-up with member's profile
                Form memberprofile = new Profile(
                    ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].members[Convert.ToInt32(lvAttendance.SelectedItems[0].SubItems[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numRehearsals + 2].Text)]);
                memberprofile.ShowDialog();
                memberprofile.Dispose();
            }
        }

        private void lvAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            // note to self: there is probably a more efficient way of checking if something is selected
            if (e.KeyCode == Keys.Delete && lvAttendance.SelectedIndices.Count != 0)
            {
                if (MessageBox.Show(
                    "Would you like to remove " + lvAttendance.SelectedItems[0].SubItems[0].Text + " from the term?",
                    "Remove Member From Term",
                    MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                {
                    // try to remove the member
                    if (ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].removeMember(Convert.ToInt16(lvAttendance.SelectedItems[0].SubItems[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numRehearsals + 2].Text)))
                    {  
                        if (Properties.Settings.Default.playSounds)
                            Sound.Click.Play();

                        // we were successful, so add history and remove from the listview
                        ClsStorage.currentClub.AddHistory(String.Format("{0}@{1}", lvAttendance.SelectedItems[0].SubItems[0].Text, cbTerm.Text), ChangeType.RemoveFromTerm);
                        
                        // iRemove is the index of the member to remove
                        int iRemove = lvAttendance.SelectedIndices[0];
                        for (int i = iRemove; i < ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numMembers; i++)
                            lvAttendance.Items[i + 1].SubItems[ClsStorage.currentClub.listTerms[cbTerm.SelectedIndex].numRehearsals + 2].Text = Convert.ToString(i - 1);
                        lvAttendance.Items.RemoveAt(iRemove);
                    }
                    else
                        // we failed, probably because the member still has attendance
                        MessageBox.Show("Removing member failed. Please confirm the member has no attendance this term.");

                }
            }
        }

        private void btnAddMembers_Click(object sender, EventArgs e)
        {
            int iTerm = cbTerm.SelectedIndex;
            if (iTerm == -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Please select a term to add members to.");
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();

                ClsStorage.selectedMembersList.Clear();

                /* this gets a bunch of member indices into clsStorage.selectedMembersList */
                /* the MemberList load function clears selectedMembersList for us, so we don't need to worry about it */
                MemberList tmp = new MemberList(true);
                tmp.ShowDialog();
                tmp.Dispose();

                List<int> addedList = new List<int>();
                foreach (int i in ClsStorage.selectedMembersList)
                {
                    if (!ClsStorage.currentClub.listTerms[iTerm].addMember((short)i))
                    {
                        MessageBox.Show("Too many members in the given term. Please add fewer members.");
                        MessageBox.Show("Now reverting...");
                        foreach (int j in addedList)
                        {
                            ClsStorage.currentClub.listTerms[iTerm].removeMember((short)j);
                        }
                        MessageBox.Show("Finished reverting...");
                        return;
                    }
                    addedList.Add(i);
                }

                foreach (int i in ClsStorage.selectedMembersList)
                {
                    string name = ClsStorage.currentClub.GetFormattedName(i);
                    string selectedTermName = cbTerm.Text;
                    ClsStorage.currentClub.AddHistory(String.Format("{0}@{1}", name, selectedTermName), ChangeType.AddToTerm);
                }

                /* display our additions on the form */
                lvAttendance.BeginUpdate();

                List<ListViewItem> attendanceList = new List<ListViewItem>();
                int lastRehearsal = ClsStorage.currentClub.listTerms[iTerm].recentRehearsal(DateTime.Today);
                foreach (int i in ClsStorage.selectedMembersList) {
                    int memberTermID = ClsStorage.currentClub.listTerms[iTerm].memberSearch((short)i);
                    loadMemberToAttendanceList(attendanceList, iTerm, memberTermID, lastRehearsal);
                }
                lvAttendance.Items.AddRange(attendanceList.ToArray());
                lvAttendance.EndUpdate();

                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
            }
        }
    }
}
