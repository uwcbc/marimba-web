namespace Marimba
{
    partial class addFees
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
            this.SuspendLayout();
            // 
            // lblMembers
            // 
            this.lblMembers.AutoSize = true;
            this.lblMembers.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembers.Location = new System.Drawing.Point(30, 126);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(83, 18);
            this.lblMembers.TabIndex = 4;
            this.lblMembers.Text = "Members";
            // 
            // cbFee
            // 
            this.cbFee.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFee.FormattingEnabled = true;
            this.cbFee.Location = new System.Drawing.Point(150, 76);
            this.cbFee.Name = "cbFee";
            this.cbFee.Size = new System.Drawing.Size(281, 25);
            this.cbFee.TabIndex = 3;
            this.cbFee.SelectedIndexChanged += new System.EventHandler(this.cbFee_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(202, 682);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 29);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(150, 397);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(281, 24);
            this.txtAmount.TabIndex = 7;
            // 
            // cbTerm
            // 
            this.cbTerm.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(150, 26);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(281, 25);
            this.cbTerm.TabIndex = 1;
            this.cbTerm.SelectedIndexChanged += new System.EventHandler(this.cbTerm_SelectedIndexChanged);
            // 
            // lblTerm
            // 
            this.lblTerm.AutoSize = true;
            this.lblTerm.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerm.Location = new System.Drawing.Point(30, 29);
            this.lblTerm.Name = "lblTerm";
            this.lblTerm.Size = new System.Drawing.Size(50, 18);
            this.lblTerm.TabIndex = 0;
            this.lblTerm.Text = "Term";
            // 
            // mcDate
            // 
            this.mcDate.Location = new System.Drawing.Point(150, 446);
            this.mcDate.MaxSelectionCount = 1;
            this.mcDate.Name = "mcDate";
            this.mcDate.TabIndex = 9;
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFee.Location = new System.Drawing.Point(30, 79);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(38, 18);
            this.lblFee.TabIndex = 2;
            this.lblFee.Text = "Fee";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(30, 400);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(108, 18);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount Paid";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(30, 447);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(46, 18);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "Date";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(202, 717);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 29);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // lblReceipt
            // 
            this.lblReceipt.AutoSize = true;
            this.lblReceipt.Font = new System.Drawing.Font("Quicksand Book", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceipt.Location = new System.Drawing.Point(30, 639);
            this.lblReceipt.Name = "lblReceipt";
            this.lblReceipt.Size = new System.Drawing.Size(106, 36);
            this.lblReceipt.TabIndex = 12;
            this.lblReceipt.Text = "Send Digital\r\nReceipt";
            // 
            // checkReceipt
            // 
            this.checkReceipt.AutoSize = true;
            this.checkReceipt.Checked = true;
            this.checkReceipt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReceipt.Location = new System.Drawing.Point(150, 651);
            this.checkReceipt.Name = "checkReceipt";
            this.checkReceipt.Size = new System.Drawing.Size(15, 14);
            this.checkReceipt.TabIndex = 13;
            this.checkReceipt.UseVisualStyleBackColor = true;
            // 
            // lvMembers
            // 
            this.lvMembers.CheckBoxes = true;
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FName});
            this.lvMembers.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMembers.Location = new System.Drawing.Point(150, 126);
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.Size = new System.Drawing.Size(281, 248);
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
            // addFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 767);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.checkReceipt);
            this.Controls.Add(this.lblReceipt);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblFee);
            this.Controls.Add(this.mcDate);
            this.Controls.Add(this.lblTerm);
            this.Controls.Add(this.cbTerm);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbFee);
            this.Controls.Add(this.lblMembers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "addFees";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.addFees_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}