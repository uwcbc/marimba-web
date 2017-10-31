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

    public partial class SignInForm : Form
    {
        Label[] attendance;
        int iSignin, iSignup;
        List<ListViewItem> termMembers;
        List<ListViewItem> otherMembers;
        string lastSearch;
        int rehearsalindex;
        public SignInForm()
        {
            InitializeComponent();
        }

        private void signin_Load(object sender, EventArgs e)
        {
            iSignin = 0;
            iSignup = 0;
            lastSearch = "";
            cbMultiple.Enabled = false;
            rehearsalindex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today);
            lblTitle.Text = ClsStorage.currentClub.strName + " Sign-in";
            lblDate.Text = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].strName + " - " + DateTime.Today.ToLongDateString();

            //add the instruments to the combo box
            cbInstrument.BeginUpdate();
            List<string> listInstruments = new List<string>();
            foreach (Member.Instrument instrument in Enum.GetValues(typeof(Member.Instrument)))
                listInstruments.Add(Member.instrumentToString(instrument));
            listInstruments.Sort();
            cbInstrument.Items.AddRange(listInstruments.ToArray());
            cbInstrument.EndUpdate();

            //set up the table for attendance information
            tlpAttendance.ColumnCount = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numRehearsals;
            tlpAttendance.ColumnStyles.Clear();
            //set up listviews
            lvSignedIn.SmallImageList = Program.home.instrumentSmall;
            lvSearch.LargeImageList = Program.home.instrumentLarge;
            
            //fill up the list with members
            //create two lists; one of term members and the other of other members
            termMembers = new List<ListViewItem>();
            otherMembers = new List<ListViewItem>();
            ListViewItem temp;
            int indexInTerm;
            for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
            {
                //skip if it is an unsubscribed member or deactivated member
                if (ClsStorage.currentClub.members[i].IsSubscribed() && ClsStorage.currentClub.members[i].email != "")
                {
                    if (ClsStorage.currentClub.members[i].curInstrument != Member.Instrument.Other)
                        temp = new ListViewItem(new string[6] {ClsStorage.currentClub.GetFirstAndLastName(i), Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument),
                        ClsStorage.currentClub.members[i].email, Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                        ClsStorage.currentClub.members[i].signupTime.ToString(), Convert.ToString(ClsStorage.currentClub.members[i].sID)},
                        Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                    else
                        temp = new ListViewItem(new string[6] {ClsStorage.currentClub.GetFirstAndLastName(i), ClsStorage.currentClub.members[i].otherInstrument,
                        ClsStorage.currentClub.members[i].email, Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                        ClsStorage.currentClub.members[i].signupTime.ToString(), Convert.ToString(ClsStorage.currentClub.members[i].sID)},
                        Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                    //if the member is not in the term, add them to the other members list
                    //otherwise, add them to the term members list
                    indexInTerm = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(ClsStorage.currentClub.members[i].sID);
                    if (indexInTerm == -1)
                        otherMembers.Add(temp);
                    //do not add members who are already signed in
                    else if (rehearsalindex == -1 || !ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[indexInTerm,rehearsalindex])
                        termMembers.Add(temp);
                }
            }
            termMembers = termMembers.OrderBy(a => a, new CompareListItemsClass(0, SortOrder.Ascending)).ToList();
            otherMembers = otherMembers.OrderBy(a => a, new CompareListItemsClass(0, SortOrder.Ascending)).ToList();
            termMembers.AddRange(otherMembers);
            lvSearch.BeginUpdate();
            lvSearch.Items.AddRange(termMembers.ToArray());
            lvSearch.View = View.Tile;
            lvSearch.EndUpdate();
            //attendance at the bottom
            attendance = new Label[tlpAttendance.ColumnCount];
            for (int i = 0; i<tlpAttendance.ColumnCount;i++)
            {
                attendance[i] = new Label();
                attendance[i].Dock = DockStyle.Fill;
                attendance[i].Text = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalDates[i].ToShortDateString();
                attendance[i].Font = new System.Drawing.Font("Quicksand", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                attendance[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tlpAttendance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100/tlpAttendance.ColumnCount));
                tlpAttendance.Controls.Add(attendance[i], i, 0);
            }
            //fill Signed in list
            for (int i = 0; i < ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numMembers; i++)
            {
                short sID = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].members[i];
                if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today) >=0 &&
                    ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[i,ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today)])
                    lvSignedIn.Items.Insert(0, new ListViewItem(ClsStorage.currentClub.members[sID].firstName + " " + ClsStorage.currentClub.members[sID].lastName, Member.instrumentIconIndex(ClsStorage.currentClub.members[sID].curInstrument)));
            }
            lvSignedIn.Sorting = SortOrder.Ascending;
            lvSignedIn.Sort();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            short userID = -1;
            //first, check if a user was selected
            //if not, we have to add them
            if(lvSearch.SelectedItems.Count == 0) //new user
            {               
                try
                {
                    //check for missing information
                    if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "" ||
                        ((txtStudentNumber.Text == "" || cbFaculty.Text == "") && cbClass.Text == "UW Student") ||
                        cbInstrument.Text == "")
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        MessageBox.Show("Please fill in the missing information.");
                    }
                    else if ((cbClass.Text == "UW Undergrad Student" || cbClass.Text == "UW Grad Student") && txtStudentNumber.Text.Length != 8)
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        MessageBox.Show("The student number entered is not a UW student number. Please correct it.");
                    }
                    else
                    {
                        if (cbClass.Text != "UW Undergrad Student" && cbClass.Text != "UW Grad Student")
                            txtStudentNumber.Text = "0";
                        //no missing info, then add the member!
                        if (ClsStorage.currentClub.AddMember(txtFirstName.Text, txtLastName.Text, (Member.MemberType)cbClass.SelectedIndex,
                            Convert.ToUInt32(txtStudentNumber.Text), cbFaculty.SelectedIndex, cbInstrument.Text, "", txtEmail.Text,
                            "", cbSize.SelectedIndex))
                        {
                            if (Properties.Settings.Default.playSounds)
                                Sound.Welcome.Play();
                            MessageBox.Show("Registered new member.");
                            iSignup++;
                            //this line is necessary for also adding the member to the term
                            userID = Convert.ToInt16(ClsStorage.currentClub.iMember - 1);
                        }
                        else //as of writing this comment, this cannot actually fail yet
                        {
                            if (Properties.Settings.Default.playSounds)
                                Sound.Error.Play();
                            MessageBox.Show("Registering new member failed. You may already be registered.");
                        }
                    }
                }
                catch
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("Bad input was entered. Make sure the student number entered has only numbers and no spaces.");
                }
            }
            else
                userID = Convert.ToInt16(lvSearch.SelectedItems[0].SubItems[5].Text);
            //first, check if the member is currently part of the term
            int termIndex = 0;
            //edit the member
            if (userID != -1)
            {
                ClsStorage.currentClub.members[userID].EditMember(txtFirstName.Text, txtLastName.Text, (Member.MemberType)cbClass.SelectedIndex,
                            Convert.ToUInt32(txtStudentNumber.Text), cbFaculty.SelectedIndex, cbInstrument.Text, txtEmail.Text,
                            "", ClsStorage.currentClub.members[userID].signupTime, cbSize.SelectedIndex);
                termIndex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(userID);
                if (termIndex < 0) //member not currently part of the term
                {
                    //now to add the member as part of the term
                    //this process is entirely invisible to the end user
                    if (!ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].addMember(userID))
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        MessageBox.Show("Adding user to current term failed.");
                    }
                    else
                        termIndex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numMembers - 1;
                }
                //the person signing in is now a member and marked as part of the term
                //now we just have to sign them in
                
                if (rehearsalindex != -1)
                {
                    //show record on list, but first make sure they are not already signed in
                    lvSignedIn.BeginUpdate();
                    if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[termIndex, rehearsalindex] == false)
                        lvSignedIn.Items.Insert(0, new ListViewItem(ClsStorage.currentClub.members[userID].firstName + " " + ClsStorage.currentClub.members[userID].lastName, Member.instrumentIconIndex(ClsStorage.currentClub.members[userID].curInstrument)));
                    lvSignedIn.Sort();
                    lvSignedIn.EndUpdate();
                    //mark attendance
                    ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[termIndex, rehearsalindex] = true;
                    iSignin++;
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                }
                //these few lines are for the case where we weren't searching, but immediately selected a member
                if (txtSearch.Text == "" && lvSearch.SelectedItems.Count > 0)
                {
                    termMembers.Remove(lvSearch.SelectedItems[0]);
                    lvSearch.Items.Remove(lvSearch.SelectedItems[0]);
                }
                lvSearch.SelectedItems.Clear();
                //once sign in is done, clear all of the information
                txtEmail.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtStudentNumber.Text = "";
                cbClass.SelectedIndex = -1;
                cbFaculty.Text = "";
                cbFees.Checked = false;
                cbInstrument.Text = "";
                cbSize.SelectedIndex = -1;
                cbMultiple.Enabled = false;
                cbMultiple.Checked = false;
                for (int i = 0; i < ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numRehearsals; i++)
                    attendance[i].BackColor = SystemColors.Control;             
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void signin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //just storing a bit of history
            if (iSignup > 0)
                ClsStorage.currentClub.AddHistory(Convert.ToString(iSignup), ChangeType.Signup);
            if (iSignin > 0)
            {
                ClsStorage.currentClub.AddHistory(Convert.ToString(iSignin), ChangeType.Signin);
                //save if we are a guest account
                if (ClsStorage.currentClub.strCurrentUser == "Guest")
                {
                    if (sfdSave.ShowDialog() == DialogResult.OK)
                    {
                        ClsStorage.currentClub.SaveClub(sfdSave.FileName);
                        Properties.Settings.Default.Save();
                        if (Properties.Settings.Default.playSounds)
                            Sound.Success.Play();
                        ClsStorage.unsavedChanges = false;
                    }
                }
            }
        }

        private void lvSearch_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //we've selected a user, so load them
            int iUserIndex;
            if (e.Item != null)
            {
                
                //find the user ID
                iUserIndex = Convert.ToInt32(e.Item.SubItems[5].Text);
                //populate the fields with the user's information
                txtFirstName.Text = ClsStorage.currentClub.members[iUserIndex].firstName;
                txtLastName.Text = ClsStorage.currentClub.members[iUserIndex].lastName;
                txtEmail.Text = ClsStorage.currentClub.members[iUserIndex].email;
                txtStudentNumber.Text = Convert.ToString(ClsStorage.currentClub.members[iUserIndex].uiStudentNumber);
                cbClass.SelectedIndex = (int)ClsStorage.currentClub.members[iUserIndex].type;
                cbFaculty.SelectedIndex = (int)ClsStorage.currentClub.members[iUserIndex].memberFaculty;
                //disable cbMultiple to prevent the edit multiple instrument dialog from unnecessarily appearing
                cbMultiple.Enabled = false;
                cbMultiple.Checked = ClsStorage.currentClub.members[iUserIndex].bMultipleInstruments;
                cbMultiple.Enabled = true;

                //load the instruments
                instrumentLoad(iUserIndex);

                if (ClsStorage.currentClub.members[iUserIndex].curInstrument == Member.Instrument.Other)
                    cbInstrument.Text = ClsStorage.currentClub.members[iUserIndex].otherInstrument;
                else
                    cbInstrument.Text = Member.instrumentToString(ClsStorage.currentClub.members[iUserIndex].curInstrument);
                cbSize.SelectedIndex = (int)ClsStorage.currentClub.members[iUserIndex].size;
                //if already a member of the term, check if the membership fee has been paid
                //also populate attendance
                int termIndex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(Convert.ToInt16(iUserIndex));
                if (termIndex != -1)
                {
                    cbFees.Checked = (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].feesPaid[termIndex, 0] != 0);
                    for (int i = 0; i <= ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].rehearsalIndex(DateTime.Today); i++)
                    {
                        if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[termIndex, i]) //member was here
                            attendance[i].BackColor = Color.Green;
                        else //member was not here
                            attendance[i].BackColor = Color.Red;

                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //if the user has pressed the enter key, just ignore it
            if (txtSearch.Text.EndsWith("\r\n"))
                txtSearch.Text = txtSearch.Text.Remove(txtSearch.Text.Length - 2);
            //do a slightly more efficient search if the user is entering data
            if (txtSearch.Text.Contains(lastSearch))
            {
                List<ListViewItem> tempList = new List<ListViewItem>();
                foreach (ListViewItem name in termMembers)
                {
                    if (name.SubItems[0].Text.ToLower().Contains(txtSearch.Text.ToLower()))
                        tempList.Add(name);
                }
                termMembers = tempList;
                //using beginupdate and endupdate makes the visual performance very smooth
                lvSearch.BeginUpdate();
                lvSearch.Items.Clear();
                lvSearch.Items.AddRange(termMembers.ToArray());
                lvSearch.EndUpdate();
                lastSearch = txtSearch.Text;
            }
            //if the member has reduced their search, well... it's not as quick and efficient
            //we really do have to do a full search... there's no way around it, at least not that I can think of
            else
            {
                int indexInTerm;
                //fill up the list with members
                //create two lists; one of term members and the other of other members
                termMembers = new List<ListViewItem>();
                otherMembers = new List<ListViewItem>();
                ListViewItem temp;
                for (int i = 0; i < ClsStorage.currentClub.iMember; i++)
                {
                    //skip if it is an unsubscribed member
                    if (ClsStorage.currentClub.members[i].IsSubscribed() && ClsStorage.currentClub.members[i].email != "")
                    {
                        if (ClsStorage.currentClub.members[i].curInstrument != Member.Instrument.Other)
                            temp = new ListViewItem(new string[6] {ClsStorage.currentClub.GetFirstAndLastName(i), Member.instrumentToString(ClsStorage.currentClub.members[i].curInstrument),
                        ClsStorage.currentClub.members[i].email, Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                        ClsStorage.currentClub.members[i].signupTime.ToString(), Convert.ToString(ClsStorage.currentClub.members[i].sID)},
                            Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        else
                            temp = new ListViewItem(new string[6] {ClsStorage.currentClub.GetFirstAndLastName(i), ClsStorage.currentClub.members[i].otherInstrument,
                        ClsStorage.currentClub.members[i].email, Convert.ToString(ClsStorage.currentClub.members[i].uiStudentNumber),
                        ClsStorage.currentClub.members[i].signupTime.ToString(), Convert.ToString(ClsStorage.currentClub.members[i].sID)},
                            Member.instrumentIconIndex(ClsStorage.currentClub.members[i].curInstrument));
                        //if the member is not in the term, add them to the other members list
                        //otherwise, add them to the term members list
                        indexInTerm = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(ClsStorage.currentClub.members[i].sID);
                        if (indexInTerm == -1 && temp.SubItems[0].Text.ToLower().Contains(txtSearch.Text.ToLower()))
                            otherMembers.Add(temp);
                        else if (temp.SubItems[0].Text.ToLower().Contains(txtSearch.Text.ToLower()) && rehearsalindex != -1 && (!ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[indexInTerm,rehearsalindex]))
                            termMembers.Add(temp);
                    }
                }
                termMembers = termMembers.OrderBy(a => a, new CompareListItemsClass(0, SortOrder.Ascending)).ToList();
                otherMembers = otherMembers.OrderBy(a => a, new CompareListItemsClass(0, SortOrder.Ascending)).ToList();
                termMembers.AddRange(otherMembers);
                lvSearch.BeginUpdate();
                lvSearch.Items.Clear();
                lvSearch.Items.AddRange(termMembers.ToArray());
                lvSearch.EndUpdate();
                lastSearch = txtSearch.Text;
            }
        }

        private void lvSearch_DoubleClick(object sender, EventArgs e)
        {
            //check if anything has actually been selected
            if (lvSearch.SelectedItems.Count > 0)
                btnSignIn_Click(sender, e);
        }

        private void signin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                //if txtSearch has focused, we will select whomever is the first member in the items, assuming there is one
                if (txtSearch.Focused)
                    if (lvSearch.Items.Count > 0)
                    {
                        lvSearch.Items[0].Selected = true;
                        this.Focus();
                    }

            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //this let's us use enter to easily sign someone in
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                if (lvSearch.Items.Count > 0)
                {
                    lvSearch.Items[0].Selected = true;
                    btnSignIn.Focus();
                }
            }
        }

        private void lvSearch_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lvSearch.DoDragDrop(lvSearch.SelectedItems, DragDropEffects.Move);
        }

        private void lvSearch_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lvSignedIn_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void lvSignedIn_DragDrop(object sender, DragEventArgs e)
        {
            //Return if the items are not selected in the ListView control.
            if (lvSearch.SelectedItems.Count == 0)
                return;
            if (rehearsalindex != -1)
            {
                short userID = Convert.ToInt16(lvSearch.SelectedItems[0].SubItems[5].Text);
                int termIndex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].memberSearch(userID);
                if (termIndex < 0) //member not currently part of the term
                {
                    //now to add the member as part of the term
                    //this process is entirely invisible to the end user
                    if (!ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].addMember(userID))
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        MessageBox.Show("Adding user to current term failed.");
                    }
                    else
                        termIndex = ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numMembers - 1;
                }
                //the person signing in is now a member and marked as part of the term
                //now we just have to sign them in
                
                //show record on list, but first make sure they are not already signed in
                lvSignedIn.BeginUpdate();
                if (ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[termIndex, rehearsalindex] == false)
                    lvSignedIn.Items.Insert(0, new ListViewItem(ClsStorage.currentClub.members[userID].firstName + " " + ClsStorage.currentClub.members[userID].lastName, Member.instrumentIconIndex(ClsStorage.currentClub.members[userID].curInstrument)));
                lvSignedIn.Sort();
                lvSignedIn.EndUpdate();
                //mark attendance
                ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].attendance[termIndex, rehearsalindex] = true;
                iSignin++;
                if (Properties.Settings.Default.playSounds)
                    Sound.Success.Play();
            }
            //these few lines are for the case where we weren't searching, but immediately selected a member
            if (txtSearch.Text == "")
            {
                termMembers.Remove(lvSearch.SelectedItems[0]);
                lvSearch.Items.Remove(lvSearch.SelectedItems[0]);               
            }
            lvSearch.SelectedItems.Clear();
            //once sign in is done, clear all of the information
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtStudentNumber.Text = "";
            cbClass.SelectedIndex = -1;
            cbFaculty.Text = "";
            cbFees.Checked = false;
            cbInstrument.Text = "";
            cbSize.SelectedIndex = -1;
            cbMultiple.Enabled = false;
            cbMultiple.Checked = false;
            for (int i = 0; i < ClsStorage.currentClub.listTerms[ClsStorage.currentClub.listTerms.Count - 1].numRehearsals; i++)
                attendance[i].BackColor = SystemColors.Control;
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void cbMultiple_CheckedChanged(object sender, EventArgs e)
        {
            btnEditMultiple.Visible = cbMultiple.Checked;
            if (cbMultiple.Checked && cbMultiple.Enabled)
                btnEditMultiple_Click(sender, e);
        }

        private void btnEditMultiple_Click(object sender, EventArgs e)
        {
            editMultiInstruments emiEdit;
            emiEdit = new editMultiInstruments(Convert.ToInt32(lvSearch.SelectedItems[0].SubItems[5].Text));
            emiEdit.ShowDialog();
            //the member's instruments may have changed, so just reload it all
            instrumentLoad(Convert.ToInt32(lvSearch.SelectedItems[0].SubItems[5].Text));
        }

        void instrumentLoad(int iID)
        {
            //add the instruments to the combo box
            cbInstrument.BeginUpdate();
            List<string> listInstruments = new List<string>();

            cbInstrument.Items.Clear();
            //if the member does not play multiple instruments, just give the regular list
            //otherwise, limit the list to instruments the member actually plays
            if (!ClsStorage.currentClub.members[iID].bMultipleInstruments)
            {
                foreach (Member.Instrument instrument in Enum.GetValues(typeof(Member.Instrument)))
                    listInstruments.Add(Member.instrumentToString(instrument));
            }
            else
            {
                int numberOfInstruments = Enum.GetValues(typeof(Member.Instrument)).Length;
                for (int i = 1; i < numberOfInstruments; i++)
                    if (ClsStorage.currentClub.members[iID].playsInstrument[i])
                        listInstruments.Add(Member.instrumentToString((Member.Instrument)i));
            }
            listInstruments.Sort();
            cbInstrument.Items.AddRange(listInstruments.ToArray());
            cbInstrument.EndUpdate();

            //check for multiple instrument player
            cbMultiple.Checked = ClsStorage.currentClub.members[iID].bMultipleInstruments;

        }
    }
}
