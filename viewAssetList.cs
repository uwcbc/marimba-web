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
    public partial class viewAssetList : Form
    {

        public ListViewColumnSorter lvmColumnSorter;

        public viewAssetList()
        {
            InitializeComponent();
        }

        private void viewAssetList_Load(object sender, EventArgs e)
        {
            assetListView.BeginUpdate();
            assetListView.Items.Clear();
            
            // reset sorting
            lvmColumnSorter = new ListViewColumnSorter();
            this.assetListView.ListViewItemSorter = lvmColumnSorter;

            // get the indices of all assets in the budget
            int[] assetIndexes = clsStorage.currentClub.assetList(true);

            List<ListViewItem> assetList = new List<ListViewItem>();
            ListViewItem item;
            for (int i = 0; i < assetIndexes.Length; i++)
            {
                // get the current budget item
                budgetItem currentBudgetItem = clsStorage.currentClub.budget[assetIndexes[i]];

                // get the fields that will eb displayed
                string[] itemText = new string[4];
                itemText[0] = currentBudgetItem.name;
                itemText[1] = clsStorage.currentClub.valueAfterDepreciation(assetIndexes[i]).ToString("C");
                itemText[2] = currentBudgetItem.value.ToString("C");
                itemText[3] = Convert.ToString(assetIndexes[i]);

                // add the current item to the list to be displayed
                item = new ListViewItem(itemText);
                assetList.Add(item);
            }

            // update the list
            assetListView.Items.AddRange(assetList.ToArray());
            assetListView.EndUpdate();
        }

        private void viewAssetList_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.assetListView.Sort();
            if (Properties.Settings.Default.playSounds)
                sound.click.Play();
        }

        private void viewAssetList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.assetListView.SelectedIndices[0] != -1)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.click.Play();
                Form edit = new addBudgetItem(Convert.ToInt32(assetListView.SelectedItems[0].SubItems[3].Text));
                edit.ShowDialog();
                edit.Dispose();
            }
        }
    }
}
