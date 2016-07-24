namespace Marimba
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class AssetList : Form
    {
        public ListViewColumnSorter lvmColumnSorter;
        Dictionary<int, BudgetItem> budgetItemDictionary;

        public AssetList()
        {
            InitializeComponent();
        }

        private void viewAssetList_Load(object sender, EventArgs e)
        {
            updateAssetList(chkBoxShowDepreciated.Checked);
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
                Sound.Click.Play();
        }

        private void viewAssetList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.assetListView.SelectedIndices[0] != -1)
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Click.Play();
                int key = Convert.ToInt32(assetListView.SelectedItems[0].SubItems[3].Text);
                BudgetItem toEdit = budgetItemDictionary[key];
                Form edit = new BudgetItemForm(ClsStorage.currentClub.budget.IndexOf(toEdit));
                edit.ShowDialog();
                edit.Dispose();
            }
        }

        private void viewAssetList_FormClosed(object sender, FormClosedEventArgs e)
        {
            budgetItemDictionary = null;
        }

        private void chkBoxShowDepreciated_CheckedChanged(object sender, EventArgs e)
        {
            updateAssetList(chkBoxShowDepreciated.Checked);
        }

        private void chkBoxShowDepTransactions_CheckedChanged(object sender, EventArgs e)
        {
            updateAssetList(chkBoxShowDepreciated.Checked);
        }

        /// <summary>
        /// Update the asset list in the Window
        /// </summary>
        /// <param name="showDepreciated">Whether to show completely-depreciated assets</param>
        private void updateAssetList(bool showDepreciated) {
            assetListView.BeginUpdate();
            assetListView.Items.Clear();
            
            // reset sorting
            lvmColumnSorter = new ListViewColumnSorter();
            this.assetListView.ListViewItemSorter = lvmColumnSorter;

            // get the indices of all assets in the budget
            BudgetItem[] assets = ClsStorage.currentClub.GetAssetList(showDepreciated);
            budgetItemDictionary = new Dictionary<int, BudgetItem>(assets.Length);

            List<ListViewItem> assetList = new List<ListViewItem>();
            ListViewItem item;
            for (int i = 0; i < assets.Length; i++)
            {
                // get the current budget item
                BudgetItem currentBudgetItem = assets[i];

                // get the fields that will be displayed
                string[] itemText = new string[4];
                itemText[0] = currentBudgetItem.name;
                itemText[1] = ClsStorage.currentClub.CalculateValueAfterDepreciation(currentBudgetItem).ToString("C");
                itemText[2] = currentBudgetItem.value.ToString("C");
                itemText[3] = Convert.ToString(i);
                budgetItemDictionary.Add(i, assets[i]);

                // add the current item to the list to be displayed
                item = new ListViewItem(itemText);
                assetList.Add(item);
            }

            // update the list
            assetListView.Items.AddRange(assetList.ToArray());
            assetListView.EndUpdate();
        }
    }
}
