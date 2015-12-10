using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Marimba
{
    public partial class addBudgetItem : Form
    {
        int iIndex;
        private Label lblAsset;
        private ComboBox cbAsset;
        int[] assetIndexes;
        bool depSelected = false;

        public addBudgetItem(int iEdit)
        {
            iIndex = iEdit;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //check for empty fields
                if (txtDescription.Text == "" || txtValue.Text == "" || cbType.Text == "" || cbCat.Text == "" || cbTerm.Text == "")
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("All fields but the comment field are required.");
                }
                else if (depSelected && cbAsset.SelectedIndex == -1)
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.error.Play();
                    MessageBox.Show("Select the asset for depreciation.");
                }
                else if (iIndex == -1) //otherwise, add the budget item
                {
                    if(depSelected)
                        clsStorage.currentClub.addBudget(Convert.ToDouble(txtValue.Text), txtDescription.Text, mcDateOccurred.SelectionStart,
                            mcDateAccount.SelectionStart, cbCat.Text, cbType.SelectedIndex, cbTerm.SelectedIndex, txtOther.Text, assetIndexes[cbAsset.SelectedIndex]);
                    else
                        clsStorage.currentClub.addBudget(Convert.ToDouble(txtValue.Text), txtDescription.Text, mcDateOccurred.SelectionStart,
                            mcDateAccount.SelectionStart, cbCat.Text, cbType.SelectedIndex, cbTerm.SelectedIndex, txtOther.Text);
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    MessageBox.Show("Item added successfully.");
                    clsStorage.currentClub.addHistory(txtDescription.Text, history.changeType.addBudget);
                    clearall();
                }
                else //editing a budget item
                {
                    clsStorage.currentClub.budget.ElementAt(iIndex).name = txtDescription.Text;
                    clsStorage.currentClub.budget.ElementAt(iIndex).value = Convert.ToDouble(txtValue.Text);
                    clsStorage.currentClub.budget.ElementAt(iIndex).type = cbType.SelectedIndex;
                    clsStorage.currentClub.budget.ElementAt(iIndex).term = cbTerm.SelectedIndex;
                    clsStorage.currentClub.budget.ElementAt(iIndex).cat = cbCat.Text;
                    clsStorage.currentClub.budget.ElementAt(iIndex).comment = txtOther.Text;
                    clsStorage.currentClub.budget.ElementAt(iIndex).dateOccur = mcDateOccurred.SelectionStart;
                    clsStorage.currentClub.budget.ElementAt(iIndex).dateAccount = mcDateAccount.SelectionStart;
                    //if there is depreciation, record the asset
                    if (depSelected)
                        clsStorage.currentClub.budget.ElementAt(iIndex).depOfAsset = assetIndexes[cbAsset.SelectedIndex];
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    MessageBox.Show("Item edited successfully.");
                    clsStorage.currentClub.addHistory(txtDescription.Text, history.changeType.editBudget);
                    this.Close();
                }
            }
            catch
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Adding budget item failed. Make sure the value was entered as a proper number without a dollar sign.");
            }
        }

        private void addBudgetItem_Load(object sender, EventArgs e)
        {
            cbTerm.Items.AddRange(clsStorage.currentClub.termNames());
            //if this is being used to edit, make a few changes
            if(iIndex != -1)
            {
                btnAdd.Text = "Edit";
                budgetItem currentItem = clsStorage.currentClub.budget.ElementAt(iIndex);
                txtDescription.Text = currentItem.name;
                txtValue.Text = Convert.ToString(currentItem.value);
                txtOther.Text = currentItem.comment;
                cbCat.Text = currentItem.cat;
                cbTerm.SelectedIndex = currentItem.term;
                cbType.SelectedIndex = currentItem.type;
                //if depreciation, also change the asset
                if (depSelected)
                    //first, search the array index for the location of the asset
                    //then, point the combobox to it
                    for(int i = 0; i < assetIndexes.Length; i++)
                        //if we found it
                        if(assetIndexes[i] == currentItem.depOfAsset)
                        {
                            cbAsset.SelectedIndex = i;
                            break;
                        }
                mcDateOccurred.SetDate(currentItem.dateOccur);
                mcDateAccount.SetDate(currentItem.dateAccount);
            }
            else
                //if we default to selecting current term, do so!
                //notice we only do this if we are NOT editing
                if (Properties.Settings.Default.selectCurrentTerm)
                    cbTerm.SelectedIndex = clsStorage.currentClub.sTerm - 1;
        }

        void clearall()
        {
            txtDescription.Text = "";
            txtOther.Text = "";
            txtValue.Text = "";
            cbCat.Text = "";
            cbTerm.Text = "";
            cbType.SelectedIndex = -1;
            mcDateOccurred.SetDate(DateTime.Today);
            mcDateAccount.SetDate(DateTime.Today);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if depreciation is selected, make room for the select asset controls
            if(cbType.SelectedIndex == 1)
            {
                //cbAsset
                cbAsset = new ComboBox();
                cbAsset.DropDownStyle = ComboBoxStyle.DropDownList;
                cbAsset.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                cbAsset.FormattingEnabled = true;

                //create a list of assets for the combobox
                assetIndexes = clsStorage.currentClub.assetList(iIndex != -1);
                string[] strAssets = new string[assetIndexes.Length];
                for (int i = 0; i < assetIndexes.Length; i++)
                    strAssets[i] = String.Format("{0} - ({1} Remaining)", clsStorage.currentClub.budget[assetIndexes[i]].name, clsStorage.currentClub.valueAfterDepreciation(assetIndexes[i]).ToString("C"));

                cbAsset.Items.AddRange(strAssets);

                cbAsset.Location = cbCat.Location;
                cbAsset.Size = new System.Drawing.Size(494, 24);
                cbAsset.TabIndex = 6;
                this.Controls.Add(cbAsset);

                //lblAsset
                lblAsset = new Label();
                lblAsset.AutoSize = true;
                lblAsset.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblAsset.Location = lblCategory.Location;
                lblAsset.Text = "Asset";

                this.Controls.Add(lblAsset);

                //move everything else away
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
                //change the tab indexes
                cbCat.TabIndex++;
                cbTerm.TabIndex++;
                mcDateAccount.TabIndex++;
                mcDateOccurred.TabIndex++;
                txtOther.TabIndex++;
                btnAdd.TabIndex++;

                depSelected = true;
            }
            //if depreciation was selected, but no longer is, move everything back
            else if (depSelected)
            {
                cbAsset.Dispose();
                lblAsset.Dispose();

                //move everything else back
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
                //change the tab indexes
                cbCat.TabIndex--;
                cbTerm.TabIndex--;
                mcDateAccount.TabIndex--;
                mcDateOccurred.TabIndex--;
                txtOther.TabIndex--;
                btnAdd.TabIndex--;

                //mark as depreciation no longer selected
                depSelected = false;
            }
        }
    }
}
