namespace Marimba
{
    partial class AssetList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetList));
            this.assetListView = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currentValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.originalValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assetListPanel = new System.Windows.Forms.TableLayoutPanel();
            this.chkBoxShowDepTransactions = new System.Windows.Forms.CheckBox();
            this.chkBoxShowDepreciated = new System.Windows.Forms.CheckBox();
            this.assetListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // assetListView
            // 
            this.assetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.currentValue,
            this.originalValue});
            this.assetListPanel.SetColumnSpan(this.assetListView, 2);
            this.assetListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assetListView.FullRowSelect = true;
            this.assetListView.Location = new System.Drawing.Point(3, 53);
            this.assetListView.MultiSelect = false;
            this.assetListView.Name = "assetListView";
            this.assetListView.Size = new System.Drawing.Size(658, 385);
            this.assetListView.TabIndex = 0;
            this.assetListView.UseCompatibleStateImageBehavior = false;
            this.assetListView.View = System.Windows.Forms.View.Details;
            this.assetListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.viewAssetList_ColumnClick);
            this.assetListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.viewAssetList_MouseDoubleClick);
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 385;
            // 
            // currentValue
            // 
            this.currentValue.Text = "Current Value";
            this.currentValue.Width = 125;
            // 
            // originalValue
            // 
            this.originalValue.Text = "Original Value";
            this.originalValue.Width = 125;
            // 
            // assetListPanel
            // 
            this.assetListPanel.ColumnCount = 2;
            this.assetListPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.assetListPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.assetListPanel.Controls.Add(this.assetListView, 0, 1);
            this.assetListPanel.Controls.Add(this.chkBoxShowDepTransactions, 0, 0);
            this.assetListPanel.Controls.Add(this.chkBoxShowDepreciated, 1, 0);
            this.assetListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assetListPanel.Location = new System.Drawing.Point(0, 0);
            this.assetListPanel.Name = "assetListPanel";
            this.assetListPanel.RowCount = 2;
            this.assetListPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.assetListPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.assetListPanel.Size = new System.Drawing.Size(664, 441);
            this.assetListPanel.TabIndex = 1;
            // 
            // chkBoxShowDepTransactions
            // 
            this.chkBoxShowDepTransactions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkBoxShowDepTransactions.AutoSize = true;
            this.chkBoxShowDepTransactions.Location = new System.Drawing.Point(37, 15);
            this.chkBoxShowDepTransactions.Name = "chkBoxShowDepTransactions";
            this.chkBoxShowDepTransactions.Size = new System.Drawing.Size(258, 20);
            this.chkBoxShowDepTransactions.TabIndex = 1;
            this.chkBoxShowDepTransactions.Text = "Show Depreciation Transactions";
            this.chkBoxShowDepTransactions.UseVisualStyleBackColor = true;
            this.chkBoxShowDepTransactions.Visible = false;
            this.chkBoxShowDepTransactions.CheckStateChanged += new System.EventHandler(this.chkBoxShowDepreciated_CheckedChanged);
            // 
            // chkBoxShowDepreciated
            // 
            this.chkBoxShowDepreciated.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkBoxShowDepreciated.AutoSize = true;
            this.chkBoxShowDepreciated.Location = new System.Drawing.Point(351, 15);
            this.chkBoxShowDepreciated.Name = "chkBoxShowDepreciated";
            this.chkBoxShowDepreciated.Size = new System.Drawing.Size(293, 20);
            this.chkBoxShowDepreciated.TabIndex = 2;
            this.chkBoxShowDepreciated.Text = "Show Completely-Depreciated Assets";
            this.chkBoxShowDepreciated.UseVisualStyleBackColor = true;
            this.chkBoxShowDepreciated.CheckStateChanged += new System.EventHandler(this.chkBoxShowDepTransactions_CheckedChanged);
            // 
            // viewAssetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 441);
            this.Controls.Add(this.assetListPanel);
            this.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "viewAssetList";
            this.Text = "Marimba";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.viewAssetList_FormClosed);
            this.Load += new System.EventHandler(this.viewAssetList_Load);
            this.assetListPanel.ResumeLayout(false);
            this.assetListPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView assetListView;
        private System.Windows.Forms.ColumnHeader currentValue;
        private System.Windows.Forms.ColumnHeader originalValue;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.TableLayoutPanel assetListPanel;
        private System.Windows.Forms.CheckBox chkBoxShowDepTransactions;
        private System.Windows.Forms.CheckBox chkBoxShowDepreciated;
    }
}