namespace Marimba
{
    partial class AddFeesForm
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
            this.lblMembers = new System.Windows.Forms.Label();
            this.cbFee = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.lblTerm = new System.Windows.Forms.Label();
            this.mcDate = new System.Windows.Forms.MonthCalendar();
            this.lblFee = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.lblReceipt = new System.Windows.Forms.Label();
            this.checkReceipt = new System.Windows.Forms.CheckBox();
            this.lvMembers = new System.Windows.Forms.ListView();
            this.FName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpAddFees = new System.Windows.Forms.TableLayoutPanel();
            this.tlpAddFees.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMembers
            // 
            this.lblMembers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMembers.AutoSize = true;
            this.lblMembers.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembers.Location = new System.Drawing.Point(28, 116);
            this.lblMembers.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(73, 16);
            this.lblMembers.TabIndex = 4;
            this.lblMembers.Text = "Members";
            // 
            // cbFee
            // 
            this.cbFee.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbFee.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFee.FormattingEnabled = true;
            this.cbFee.Location = new System.Drawing.Point(160, 63);
            this.cbFee.Name = "cbFee";
            this.cbFee.Size = new System.Drawing.Size(281, 24);
            this.cbFee.TabIndex = 3;
            this.cbFee.SelectedIndexChanged += new System.EventHandler(this.cbFee_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAddFees.SetColumnSpan(this.btnAdd, 2);
            this.btnAdd.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(198, 684);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 29);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtAmount.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(160, 402);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(281, 23);
            this.txtAmount.TabIndex = 7;
            // 
            // cbTerm
            // 
            this.cbTerm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(160, 13);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(281, 24);
            this.cbTerm.TabIndex = 1;
            this.cbTerm.SelectedIndexChanged += new System.EventHandler(this.cbTerm_SelectedIndexChanged);
            // 
            // lblTerm
            // 
            this.lblTerm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTerm.AutoSize = true;
            this.lblTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerm.Location = new System.Drawing.Point(42, 17);
            this.lblTerm.Name = "lblTerm";
            this.lblTerm.Size = new System.Drawing.Size(45, 16);
            this.lblTerm.TabIndex = 0;
            this.lblTerm.Text = "Term";
            // 
            // mcDate
            // 
            this.mcDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mcDate.Location = new System.Drawing.Point(159, 452);
            this.mcDate.Margin = new System.Windows.Forms.Padding(30, 9, 9, 9);
            this.mcDate.MaxSelectionCount = 1;
            this.mcDate.Name = "mcDate";
            this.mcDate.TabIndex = 9;
            // 
            // lblFee
            // 
            this.lblFee.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFee.AutoSize = true;
            this.lblFee.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFee.Location = new System.Drawing.Point(47, 67);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(34, 16);
            this.lblFee.TabIndex = 2;
            this.lblFee.Text = "Fee";
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(16, 406);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(97, 16);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount Paid";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(43, 525);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 16);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "Date";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpAddFees.SetColumnSpan(this.btnExport, 2);
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(123, 729);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(225, 29);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export Selected Term\'s Fees";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // lblReceipt
            // 
            this.lblReceipt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblReceipt.AutoSize = true;
            this.lblReceipt.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceipt.Location = new System.Drawing.Point(18, 636);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(93, 32);
            this.lblReceipt.TabIndex = 12;
            this.lblReceipt.Text = "Send Digital\r\nReceipt";
            // 
            // checkReceipt
            // 
            this.checkReceipt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkReceipt.AutoSize = true;
            this.checkReceipt.Checked = true;
            this.checkReceipt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReceipt.Location = new System.Drawing.Point(159, 645);
            this.checkReceipt.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.checkReceipt.Name = "checkReceipt";
            this.checkReceipt.Size = new System.Drawing.Size(15, 14);
            this.checkReceipt.TabIndex = 13;
            this.checkReceipt.UseVisualStyleBackColor = true;
            // 
            // lvMembers
            // 
            this.lvMembers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FName});
            this.lvMembers.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMembers.Location = new System.Drawing.Point(160, 116);
            this.lvMembers.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.Size = new System.Drawing.Size(281, 259);
            this.lvMembers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvMembers.TabIndex = 14;
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMembers_MouseClick);
            // 
            // FName
            // 
            this.FName.Text = "Name";
            this.FName.Width = 277;
            // 
            // tlpAddFees
            // 
            this.tlpAddFees.ColumnCount = 2;
            this.tlpAddFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.54237F));
            this.tlpAddFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.45763F));
            this.tlpAddFees.Controls.Add(this.lblTerm, 0, 0);
            this.tlpAddFees.Controls.Add(this.checkReceipt, 1, 5);
            this.tlpAddFees.Controls.Add(this.btnAdd, 0, 6);
            this.tlpAddFees.Controls.Add(this.lvMembers, 1, 2);
            this.tlpAddFees.Controls.Add(this.lblReceipt, 0, 5);
            this.tlpAddFees.Controls.Add(this.cbTerm, 1, 0);
            this.tlpAddFees.Controls.Add(this.lblFee, 0, 1);
            this.tlpAddFees.Controls.Add(this.mcDate, 1, 4);
            this.tlpAddFees.Controls.Add(this.lblDate, 0, 4);
            this.tlpAddFees.Controls.Add(this.txtAmount, 1, 3);
            this.tlpAddFees.Controls.Add(this.cbFee, 1, 1);
            this.tlpAddFees.Controls.Add(this.lblAmount, 0, 3);
            this.tlpAddFees.Controls.Add(this.lblMembers, 0, 2);
            this.tlpAddFees.Controls.Add(this.btnExport, 0, 7);
            this.tlpAddFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAddFees.Location = new System.Drawing.Point(0, 0);
            this.tlpAddFees.Name = "tlpAddFees";
            this.tlpAddFees.RowCount = 8;
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.555298F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.555298F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.71024F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.555298F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.59016F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.555298F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.739207F));
            this.tlpAddFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.739207F));
            this.tlpAddFees.Size = new System.Drawing.Size(472, 767);
            this.tlpAddFees.TabIndex = 15;
            // 
            // addFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 767);
            this.Controls.Add(this.tlpAddFees);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "addFees";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.addFees_Load);
            this.tlpAddFees.ResumeLayout(false);
            this.tlpAddFees.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMembers;
        private System.Windows.Forms.ComboBox cbFee;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAmount;
        public System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Label lblTerm;
        private System.Windows.Forms.MonthCalendar mcDate;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnExport;
        public System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.Label lblReceipt;
        private System.Windows.Forms.CheckBox checkReceipt;
        private System.Windows.Forms.ListView lvMembers;
        private System.Windows.Forms.ColumnHeader FName;
        private System.Windows.Forms.TableLayoutPanel tlpAddFees;
    }
}