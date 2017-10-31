namespace Marimba
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

    public partial class BudgetItemForm : Form
    {
        int iIndex;
        private Label lblAsset;
        private ComboBox cbAsset;
        BudgetItem[] assets;
        bool depSelected = false;

        public BudgetItemForm(int iEdit)
        {
            iIndex = iEdit;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // check for empty fields
                if (txtDescription.Text == String.Empty || txtValue.Text == String.Empty || cbType.Text == String.Empty || cbCat.Text == String.Empty || cbTerm.Text == String.Empty)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("All fields but the comment field are required.");
                }
                else if (depSelected && cbAsset.SelectedIndex == -1)
                {
                    if (Properties.Settings.Default.playSounds)
                        Sound.Error.Play();
                    MessageBox.Show("Select the asset for depreciation.");
                }
                else if (iIndex == -1)
                {
                    // otherwise, add the budget item
                    if (depSelected)
                        ClsStorage.currentClub.AddBudget(
                            Convert.ToDouble(txtValue.Text),
                            txtDescription.Text,
                            mcDateOccurred.SelectionStart,
                            mcDateAccount.SelectionStart,
                            cbCat.Text,
                            (TransactionType)cbType.SelectedIndex,
                            cbTerm.SelectedIndex,
                            txtOther.Text,
                            assets[cbAsset.SelectedIndex]);
                    else
                        ClsStorage.currentClub.AddBudget(
                            Convert.ToDouble(txtValue.Text),
                            txtDescription.Text,
                            mcDateOccurred.SelectionStart,
                            mcDateAccount.SelectionStart,
                            cbCat.Text,
                            (TransactionType)cbType.SelectedIndex,
                            cbTerm.SelectedIndex,
                            txtOther.Text);

                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                    MessageBox.Show("Item added successfully.");
                    ClsStorage.currentClub.AddHistory(txtDescription.Text, ChangeType.AddBudget);
                    ClearAll();
                }
                else
                {
                    // editing a budget item
                    BudgetItem currentItem = ClsStorage.currentClub.budget[iIndex];
                    currentItem.name = txtDescription.Text;
                    currentItem.value = Convert.ToDouble(txtValue.Text);
                    currentItem.type = (TransactionType)cbType.SelectedIndex;
                    currentItem.term = cbTerm.SelectedIndex;
                    currentItem.cat = cbCat.Text;
                    currentItem.comment = txtOther.Text;
                    currentItem.dateOccur = mcDateOccurred.SelectionStart;
                    currentItem.dateAccount = mcDateAccount.SelectionStart;
                    // if there is depreciation, record the asset
                    if (depSelected)
                        currentItem.depOfAsset = assets[cbAsset.SelectedIndex];
                    if (Properties.Settings.Default.playSounds)
                        Sound.Success.Play();
                    MessageBox.Show("Item edited successfully.");
                    ClsStorage.currentClub.AddHistory(txtDescription.Text, ChangeType.EditBudget);
                    this.Close();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    Sound.Error.Play();
                MessageBox.Show("Adding budget item failed. Make sure the value was entered as a proper number without a dollar sign.");
            }
        }

        private void addBudgetItem_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(ClsStorage.currentClub.GetTermNames());
            // if this is being used to edit, make a few changes
            if (iIndex != -1)
            {
                btnAdd.Text = "Edit";
                BudgetItem currentItem = ClsStorage.currentClub.budget[iIndex];
                txtDescription.Text = currentItem.name;
                txtValue.Text = Convert.ToString(currentItem.value);
                txtOther.Text = currentItem.comment;
                cbCat.Text = currentItem.cat;
                cbTerm.SelectedIndex = currentItem.term;
                cbType.SelectedIndex = (int)currentItem.type;
                // if depreciation, also change the asset
                if (depSelected)
                    // first, search the array index for the location of the asset
                    // then, point the combobox to it
                    for (int i = 0; i < assets.Length; i++)
                        if (assets[i] == currentItem.depOfAsset)
                        {
                            cbAsset.SelectedIndex = i;
                            break;
                        }
                mcDateOccurred.SetDate(currentItem.dateOccur);
                mcDateAccount.SetDate(currentItem.dateAccount);
            }
            else
                // if we default to selecting current term, do so!
                // notice we only do this if we are NOT editing
                if (Properties.Settings.Default.selectCurrentTerm)
                    cbTerm.SelectedIndex = ClsStorage.currentClub.listTerms.Count - 1;
        }

        void ClearAll()
        {
            txtDescription.Text = String.Empty;
            txtOther.Text = String.Empty;
            txtValue.Text = String.Empty;
            cbCat.Text = String.Empty;
            cbTerm.Text = String.Empty;
            cbType.SelectedIndex = -1;
            mcDateOccurred.SetDate(DateTime.Today);
            mcDateAccount.SetDate(DateTime.Today);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if depreciation is selected, make room for the select asset controls
            if (cbType.SelectedIndex == 1)
            {
                // cbAsset
                cbAsset = new ComboBox();
                cbAsset.DropDownStyle = ComboBoxStyle.DropDownList;
                cbAsset.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                cbAsset.FormattingEnabled = true;

                // create a list of assets for the combobox
                assets = ClsStorage.currentClub.GetAssetList(iIndex != -1);
                string[] strAssets = new string[assets.Length];
                for (int i = 0; i < assets.Length; i++)
                    strAssets[i] = String.Format("{0} - ({1} Remaining)", assets[i].name, ClsStorage.currentClub.CalculateValueAfterDepreciation(assets[i]).ToString("C"));

                cbAsset.Items.AddRange(strAssets);

                cbAsset.Location = cbCat.Location;
                cbAsset.Size = new System.Drawing.Size(494, 24);
                cbAsset.TabIndex = 6;
                this.Controls.Add(cbAsset);

                // lblAsset
                lblAsset = new Label();
                lblAsset.AutoSize = true;
                lblAsset.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
                lblAsset.Location = lblCategory.Location;
                lblAsset.Text = "Asset";

                this.Controls.Add(lblAsset);

                // move everything else away
                this.Height += 43;
                lblCategory.Location = new Point(lblCategory.Location.X, lblCategory.Location.Y + 43);
                cbCat.Location = new Point(cbCat.Location.X, cbCat.Location.Y + 43);
                lblTerm.Location = new Point(lblTerm.Location.X, lblTerm.Location.Y + 43);
                cbTerm.Location = new Point(cbTerm.Location.X, cbTerm.Location.Y + 43);
                lblDateAccount.Location = new Point(lblDateAccount.Location.X, lblDateAccount.Location.Y + 43);
                lblDateOccurred.Location = new Point(lblDateOccurred.Location.X, lblDateOccurred.Location.Y + 43);
                mcDateAccount.Location = new Point(mcDateAccount.Location.X, mcDateAccount.Location.Y + 43);
                mcDateOccurred.Location = new Point(mcDateOccurred.Location.X, mcDateOccurred.Location.Y + 43);
                lblComment.Location = new Point(lblComment.Location.X, lblComment.Location.Y + 43);
                txtOther.Location = new Point(txtOther.Location.X, txtOther.Location.Y + 43);
                btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y + 43);
                // change the tab indexes
                cbCat.TabIndex++;
                cbTerm.TabIndex++;
                mcDateAccount.TabIndex++;
                mcDateOccurred.TabIndex++;
                txtOther.TabIndex++;
                btnAdd.TabIndex++;

                depSelected = true;
            }
            else if (depSelected)
            {
                // if depreciation was selected, but no longer is, move everything back
                cbAsset.Dispose();
                lblAsset.Dispose();

                // move everything else back
                this.Height -= 43;
                lblCategory.Location = new Point(lblCategory.Location.X, lblCategory.Location.Y - 43);
                cbCat.Location = new Point(cbCat.Location.X, cbCat.Location.Y - 43);
                lblTerm.Location = new Point(lblTerm.Location.X, lblTerm.Location.Y - 43);
                cbTerm.Location = new Point(cbTerm.Location.X, cbTerm.Location.Y - 43);
                lblDateAccount.Location = new Point(lblDateAccount.Location.X, lblDateAccount.Location.Y - 43);
                lblDateOccurred.Location = new Point(lblDateOccurred.Location.X, lblDateOccurred.Location.Y - 43);
                mcDateAccount.Location = new Point(mcDateAccount.Location.X, mcDateAccount.Location.Y - 43);
                mcDateOccurred.Location = new Point(mcDateOccurred.Location.X, mcDateOccurred.Location.Y - 43);
                lblComment.Location = new Point(lblComment.Location.X, lblComment.Location.Y - 43);
                txtOther.Location = new Point(txtOther.Location.X, txtOther.Location.Y - 43);
                btnAdd.Location = new Point(btnAdd.Location.X, btnAdd.Location.Y - 43);
                // change the tab indexes
                cbCat.TabIndex--;
                cbTerm.TabIndex--;
                mcDateAccount.TabIndex--;
                mcDateOccurred.TabIndex--;
                txtOther.TabIndex--;
                btnAdd.TabIndex--;

                // mark as depreciation no longer selected
                depSelected = false;
            }
        }
    }
}
