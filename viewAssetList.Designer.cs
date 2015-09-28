namespace Marimba
{
    partial class viewAssetList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(viewAssetList));
            this.assetListView = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currentValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.originalValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // assetListView
            // 
            this.assetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.currentValue,
            this.originalValue});
            this.assetListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assetListView.FullRowSelect = true;
            this.assetListView.Location = new System.Drawing.Point(0, 0);
            this.assetListView.MultiSelect = false;
            this.assetListView.Name = "assetListView";
            this.assetListView.Size = new System.Drawing.Size(664, 441);
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
            // viewAssetList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 441);
            this.Controls.Add(this.assetListView);
            this.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "viewAssetList";
            this.Text = "Asset List";
            this.Load += new System.EventHandler(this.viewAssetList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView assetListView;
        private System.Windows.Forms.ColumnHeader currentValue;
        private System.Windows.Forms.ColumnHeader originalValue;
        private System.Windows.Forms.ColumnHeader name;
    }
}