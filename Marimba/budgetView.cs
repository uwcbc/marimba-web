using Marimba.Utility;
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    public partial class budgetView : Form
    {
        public ListViewColumnSorter lvmColumnSorter;
        Dictionary<int, budgetItem> budgetItemDictionary;

        public budgetView()
        {
            InitializeComponent();
        }

        private void budgetView_Load(object sender, EventArgs e)
        {
            lvMain.BeginUpdate();
            lvmColumnSorter = new ListViewColumnSorter();
            this.lvMain.ListViewItemSorter = lvmColumnSorter;
            List<ListViewItem> budgetList = new List<ListViewItem>();
            budgetItemDictionary = new Dictionary<int, budgetItem>(clsStorage.currentClub.budget.Count);
            int i = 0;
            foreach (budgetItem item in clsStorage.currentClub.budget)
            {
                string[] budgetText = new string[8];
                budgetText[0] = clsStorage.currentClub.listTerms[item.term].strName; // name of term
                budgetText[1] = item.name; // name of financial transaction
                budgetText[2] = ""; // debit amount
                budgetText[3] = ""; // credit amount
                budgetText[4] = item.cat; // the type of thing being credited or debited
                budgetText[5] = item.type.ToString(); // "Asset", "Depreciation", "Revenue" or "Expense"
                budgetText[6] = item.dateOccur.ToShortDateString(); // date of transaction
                budgetText[7] = Convert.ToString(i); // internal index for this item and is not displayed; used so that we can double-click an item to open up its edit item window
                budgetItemDictionary.Add(i, item);
                i++;

                // credit amounts
                if (item.type == Enumerations.TransactionType.Revenue)
                {
                    budgetText[3] = item.value.ToString("C");
                    budgetList.Add(new ListViewItem(budgetText));
                }
                //debit items
                else
                {
                    budgetText[2] = item.value.ToString("C");
                    budgetList.Add(new ListViewItem(budgetText));
                }
            }
            lvMain.Items.AddRange(budgetList.ToArray());
            lvMain.EndUpdate();
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
                sound.click.Play();
        }

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMain.SelectedIndices[0] != -1) //something is selected
            {
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
                //open edit budget item dialog box
                int key = Convert.ToInt32(lvMain.SelectedItems[0].SubItems[7].Text);
                budgetItem toEdit = budgetItemDictionary[key];
                Form edit = new addBudgetItem(clsStorage.currentClub.budget.IndexOf(toEdit));
                edit.ShowDialog();
                edit.Dispose();
            }
        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            //note to self: there is probably a more efficient way of checking if something is selected
            if (e.KeyCode == Keys.Delete && lvMain.SelectedIndices.Count != 0)
            {
                if (MessageBox.Show("Would you like to delete " + lvMain.SelectedItems[0].SubItems[1].Text + "?", "Delete Budget Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {                    
                    clsStorage.currentClub.deleteBudget(Convert.ToInt32(lvMain.SelectedItems[0].SubItems[7].Text));
                    if (Properties.Settings.Default.playSounds)
                        sound.click.Play();
                    lvMain.BeginUpdate();
                    clsStorage.currentClub.addHistory(lvMain.SelectedItems[0].SubItems[1].Text, Enumerations.ChangeType.DeleteBudget);
                    lvMain.Items.RemoveAt(lvMain.SelectedIndices[0]);                   
                    lvMain.EndUpdate();
                }
            }
        }

        private void budgetView_FormClosed(object sender, FormClosedEventArgs e)
        {
            budgetItemDictionary = null;
        }
    }
}
