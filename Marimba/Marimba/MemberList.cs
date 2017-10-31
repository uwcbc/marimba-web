﻿namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;

    public partial class MemberList : Form
    {
        // this form has one of two purposes:
        // 1) for profile access
        // 2) to select a group of people (for email or adding to the attendance)
        // false means profile
        // true means email
        bool selectGroup;

        string lastSearch;

        public ListViewColumnSorter lvmColumnSorter;

        List<ListViewItem> memberlist;

        public MemberList(bool selectGroup)
        {
            this.selectGroup = selectGroup;
            InitializeComponent();
            lvMain.MultiSelect = selectGroup;
            btnSelect.Visible = selectGroup;
        }

        private void MemberList_Load(object sender, EventArgs e)
        {
            lastSearch = String.Empty;
            memberlist = new List<ListViewItem>();
            lvmColumnSorter = new ListViewColumnSorter();
            this.lvMain.ListViewItemSorter = lvmColumnSorter;

            lvMain.LargeImageList = Program.home.instrumentLarge;
            lvMain.SmallImageList = Program.home.instrumentSmall;
            // fill up the list with members
            for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
            {
                if (ClsStorage.currentClub.members[i].IsSubscribed())
                {
                    if (ClsStorage.currentClub.members[i].curInstrument != Member.Instrument.Other)
                        memberlist.Add(new ListViewItem(
                            new string[6]
                            {
                                ClsStorage.currentClub.GetFirstAndLastName(i),
                                Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument),
                                ClsStorage.currentClub.members[i].email,
                                Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                ClsStorage.currentClub.members[i].signupTime.ToString(),
                                Convert.ToString(ClsStorage.currentClub.members[i].sID)
                            },
                            Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument)));
                    else
                        memberlist.Add(new ListViewItem(
                            new string[6]
                            {
                                ClsStorage.currentClub.GetFirstAndLastName(i),
                                ClsStorage.currentClub.members[i].otherInstrument,
                                ClsStorage.currentClub.members[i].email,
                                Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                ClsStorage.currentClub.members[i].signupTime.ToString(),
                                Convert.ToString(ClsStorage.currentClub.members[i].sID)
                            },
                            Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument)));
                }
                    
            }
            // set comboboxes appropriately to defaults
            cbDisplay.SelectedIndex = 3;
            cbSearchMode.SelectedIndex = 0;
            // prepare stuff for emailing
            ClsStorage.selectedMembersList.Clear();
            lvMain.Items.AddRange(memberlist.ToArray());
        }

        private void lvMain_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvmColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
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
            this.lvMain.Sort();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
        }

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            triggerSelection();
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            lvmColumnSorter.SortColumn = 0;
            lvmColumnSorter.Order = SortOrder.Ascending;
            // Perform the sort with these new sort options.
            this.lvMain.Sort();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
        }

        private void btnInstrument_Click(object sender, EventArgs e)
        {
            lvmColumnSorter.SortColumn = 1;
            lvmColumnSorter.Order = SortOrder.Ascending;
            // Perform the sort with these new sort options.
            this.lvMain.Sort();
            if (Properties.Settings.Default.playSounds)
                Sound.Click.Play();
        }

        private void cbDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDisplay.SelectedIndex)
            {
                case 0:
                    lvMain.View = View.SmallIcon;
                    break;
                case 1:
                    lvMain.View = View.LargeIcon;
                    break;
                case 2:
                    lvMain.View = View.Details;
                    break;
                case 3:
                    lvMain.View = View.Tile;
                    break;
                case 4:
                    lvMain.View = View.List;
                    break;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // do a slightly more efficient search if the user is entering data
            if (txtSearch.Text.Contains(lastSearch))
            {
                List<ListViewItem> tempList = new List<ListViewItem>();
                foreach (ListViewItem name in memberlist)
                {
                    if (name.SubItems[cbSearchMode.SelectedIndex].Text.ToLower().Contains(txtSearch.Text.ToLower()))
                        tempList.Add(name);
                }
                memberlist = tempList;
                // using beginupdate and endupdate makes the visual performance very smooth
                lvMain.BeginUpdate();
                lvMain.Items.Clear();
                lvMain.Items.AddRange(memberlist.ToArray());
                lvMain.EndUpdate();
                lastSearch = txtSearch.Text;
            }
            else
            {
                // if the member has reduced their search, well... it's not as quick and efficient
                // we really do have to do a full search... there's no way around it, at least not that I can think of

                memberlist.Clear();
                ListViewItem temp;
                for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                {
                    // skip if it is an unsubscribed member
                    // also check for if we are only considering members in the current term
                    if (ClsStorage.currentClub.members[i].IsSubscribed() && (!cbCurrentTerm.Checked || ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(Convert.ToInt16(i)) >= 0))
                    {
                        if (ClsStorage.currentClub.members[i].curInstrument != Member.Instrument.Other)
                            temp = new ListViewItem(
                                new string[6]
                                {
                                    ClsStorage.currentClub.GetFirstAndLastName(i),
                                    Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument),
                                    ClsStorage.currentClub.members[i].email,
                                    Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                    ClsStorage.currentClub.members[i].signupTime.ToString(),
                                    Convert.ToString(ClsStorage.currentClub.members[i].sID)
                                },
                                Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        else
                            temp = new ListViewItem(
                                new string[6]
                                {
                                    ClsStorage.currentClub.GetFirstAndLastName(i),
                                    ClsStorage.currentClub.members[i].otherInstrument,
                                    ClsStorage.currentClub.members[i].email,
                                    Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                    ClsStorage.currentClub.members[i].signupTime.ToString(),
                                    Convert.ToString(ClsStorage.currentClub.members[i].sID)
                                },
                                Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        if (temp.SubItems[cbSearchMode.SelectedIndex].Text.ToLower().Contains(txtSearch.Text.ToLower()))
                            memberlist.Add(temp);
                        
                    }
                }
                lvMain.BeginUpdate();
                lvMain.Items.Clear();
                lvMain.Items.AddRange(memberlist.ToArray());
                lvMain.EndUpdate();
                lastSearch = txtSearch.Text;
            }
        }

        private void cbSearchMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this will trigger the search mode to reset itself
            txtSearch.Text = String.Empty;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            triggerSelection();
        }

        private void triggerSelection()
        {
            // something is selected
            if (lvMain.SelectedIndices[0] != -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();

                // profile usage
                if (!selectGroup)
                {
                    // insert pop-up with member's profile
                    Form memberprofile = new Profile(Convert.ToInt32(lvMain.SelectedItems[0].SubItems[5].Text));
                    memberprofile.ShowDialog();

                    // if someone was unsubscribed, remove them
                    if (!ClsStorage.currentClub.members[Convert.ToInt32(lvMain.SelectedItems[0].SubItems[5].Text)].IsSubscribed())
                        lvMain.Items.RemoveAt(lvMain.SelectedIndices[0]);
                }
                else
                {
                    // sending to another window
                    // add any selected members to the list
                    foreach (ListViewItem recipient in lvMain.SelectedItems)
                        ClsStorage.selectedMembersList.Add(Convert.ToInt32(recipient.SubItems[5].Text));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void cbCurrentTerm_CheckedChanged(object sender, EventArgs e)
        {
            // if going from checked to unchecked, that's easy
            // just eliminate members who don't satisfy the criteria
            if (cbCurrentTerm.Checked)
            {
                List<ListViewItem> tempList = new List<ListViewItem>();
                foreach (ListViewItem name in memberlist)
                {
                    // search the current term for the member
                    if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(Convert.ToInt16(name.SubItems[5].Text)) >= 0)
                        tempList.Add(name);
                }
                memberlist = tempList;

                // using beginupdate and endupdate makes the visual performance very smooth
                lvMain.BeginUpdate();
                lvMain.Items.Clear();
                lvMain.Items.AddRange(memberlist.ToArray());
                lvMain.EndUpdate();
                lastSearch = txtSearch.Text;
            }
            else
            {
                // the hard way
                // basically, redo everything that's been previously done

                memberlist.Clear();
                ListViewItem temp;
                for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                {
                    if (ClsStorage.currentClub.members[i].IsSubscribed())
                    {
                        if (ClsStorage.currentClub.members[i].curInstrument != Member.Instrument.Other)
                            temp = new ListViewItem(
                                new string[6]
                                {
                                    ClsStorage.currentClub.GetFirstAndLastName(i),
                                    Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument),
                                    ClsStorage.currentClub.members[i].email,
                                    Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                    ClsStorage.currentClub.members[i].signupTime.ToString(),
                                    Convert.ToString(ClsStorage.currentClub.members[i].sID)
                                },
                                Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        else
                            temp = new ListViewItem(
                                new string[6]
                                {
                                    ClsStorage.currentClub.GetFirstAndLastName(i),
                                    ClsStorage.currentClub.members[i].otherInstrument,
                                    ClsStorage.currentClub.members[i].email,
                                    Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                                    ClsStorage.currentClub.members[i].signupTime.ToString(),
                                    Convert.ToString(ClsStorage.currentClub.members[i].sID)
                                },
                                Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        if (temp.SubItems[cbSearchMode.SelectedIndex].Text.ToLower().Contains(txtSearch.Text.ToLower()))
                            memberlist.Add(temp);

                    }
                }
                lvMain.BeginUpdate();
                lvMain.Items.Clear();
                lvMain.Items.AddRange(memberlist.ToArray());
                lvMain.EndUpdate();
                lastSearch = txtSearch.Text;
            }
        }
    }

    public class CompareListItemsClass : IComparer<ListViewItem>
    {
        private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
        private SortOrder OrderOfSort;
        private int ColumnToSort;

        public CompareListItemsClass(int columnIndex, SortOrder sortOrder)
        {
            ColumnToSort = columnIndex;
            OrderOfSort = sortOrder;
        }

        public int Compare(ListViewItem listviewX, ListViewItem listviewY)
        {
            int compareResult;
            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return -compareResult;
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /*int IComparer<ListViewItem>.Compare(ListViewItem x, ListViewItem y)
        {
            throw new NotImplementedException();
        }*/
    }

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int ColumnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewColumnSorter" /> class. Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            ColumnToSort = 0;

            // Initialize the sort order to 'none'
            OrderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }

            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }

            get
            {
                return OrderOfSort;
            }
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return -compareResult;
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

    }
}
