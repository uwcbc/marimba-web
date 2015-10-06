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
            for (int i = 0; i < clsStorage.currentClub.iBudget; i++)
            {
                string[] budgetText = new string[8];
                budgetText[0] = clsStorage.currentClub.terms[clsStorage.currentClub.budget[i].term].strName;
                budgetText[1] = clsStorage.currentClub.budget[i].name;
                budgetText[2] = ""; // debit amount
                budgetText[3] = ""; // credit amount
                budgetText[4] = clsStorage.currentClub.budget[i].cat;
                budgetText[5] = clsStorage.moneyTypeToString(clsStorage.currentClub.budget[i].type);
                budgetText[6] = clsStorage.currentClub.budget[i].dateOccur.ToShortDateString();
                budgetText[7] = Convert.ToString(i);

                // credit amounts
                if (clsStorage.currentClub.budget[i].type == (int)club.money.Revenue)
                {
                    budgetText[3] = clsStorage.currentClub.budget[i].value.ToString("C");
                    budgetList.Add(new ListViewItem(budgetText));
                }
                //debit items
                else
                {
                    budgetText[2] = clsStorage.currentClub.budget[i].value.ToString("C");
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
                Form edit = new addBudgetItem(Convert.ToInt32(lvMain.SelectedItems[0].SubItems[7].Text));
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
                    clsStorage.currentClub.addHistory(lvMain.SelectedItems[0].SubItems[1].Text, history.changeType.deleteBudget);
                    lvMain.Items.RemoveAt(lvMain.SelectedIndices[0]);                   
                    lvMain.EndUpdate();
                }
            }
        }
    }
}
