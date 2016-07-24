namespace Marimba
{
    partial class BudgetItemForm
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
            this.txtOther = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.cbCat = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblDateOccurred = new System.Windows.Forms.Label();
            this.mcDateOccurred = new System.Windows.Forms.MonthCalendar();
            this.lblTerm = new System.Windows.Forms.Label();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.lblDateAccount = new System.Windows.Forms.Label();
            this.mcDateAccount = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // txtOther
            // 
            this.txtOther.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOther.Location = new System.Drawing.Point(252, 456);
            this.txtOther.Multiline = true;
            this.txtOther.Name = "txtOther";
            this.txtOther.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOther.Size = new System.Drawing.Size(494, 78);
            this.txtOther.TabIndex = 13;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(73, 459);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(67, 17);
            this.lblComment.TabIndex = 12;
            this.lblComment.Text = "Comment";
            // 
            // cbCat
            // 
            this.cbCat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCat.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCat.FormattingEnabled = true;
            this.cbCat.Items.AddRange(new object[] {
            "Advertising",
            "Buttons",
            "Clothing",
            "Copyright/Administrative Fees",
            "Donations",
            "Equipment Rentals",
            "Feds Settlement",
            "Food/Beverages",
            "Instruments",
            "Maintenance",
            "Miscellaneous",
            "Music Equipment",
            "Printing",
            "Rental Subsidy",
            "Sheet Music",
            "Waived Membership Fee"});
            this.cbCat.Location = new System.Drawing.Point(252, 154);
            this.cbCat.Name = "cbCat";
            this.cbCat.Size = new System.Drawing.Size(494, 25);
            this.cbCat.TabIndex = 7;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(49, 157);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(65, 17);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Asset",
            "Depreciation",
            "Expense",
            "Revenue"});
            this.cbType.Location = new System.Drawing.Point(252, 111);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(494, 25);
            this.cbType.TabIndex = 5;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(252, 66);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(494, 23);
            this.txtValue.TabIndex = 3;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(49, 69);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(44, 17);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "Value";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(252, 23);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(494, 23);
            this.txtDescription.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(337, 556);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 28);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(49, 26);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(89, 16);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(49, 114);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(40, 17);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type";
            // 
            // lblDateOccurred
            // 
            this.lblDateOccurred.AutoSize = true;
            this.lblDateOccurred.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateOccurred.Location = new System.Drawing.Point(69, 237);
            this.lblDateOccurred.Name = "lblDateOccurred";
            this.lblDateOccurred.Size = new System.Drawing.Size(170, 17);
            this.lblDateOccurred.TabIndex = 10;
            this.lblDateOccurred.Text = "Date of Purchase/Receipt";
            // 
            // mcDateOccurred
            // 
            this.mcDateOccurred.Location = new System.Drawing.Point(55, 264);
            this.mcDateOccurred.MaxSelectionCount = 1;
            this.mcDateOccurred.Name = "mcDateOccurred";
            this.mcDateOccurred.TabIndex = 11;
            // 
            // lblTerm
            // 
            this.lblTerm.AutoSize = true;
            this.lblTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerm.Location = new System.Drawing.Point(49, 202);
            this.lblTerm.Name = "lblTerm";
            this.lblTerm.Size = new System.Drawing.Size(41, 17);
            this.lblTerm.TabIndex = 8;
            this.lblTerm.Text = "Term";
            // 
            // cbTerm
            // 
            this.cbTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(252, 199);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(494, 25);
            this.cbTerm.TabIndex = 9;
            // 
            // lblDateAccount
            // 
            this.lblDateAccount.AutoSize = true;
            this.lblDateAccount.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateAccount.Location = new System.Drawing.Point(505, 237);
            this.lblDateAccount.Name = "lblDateAccount";
            this.lblDateAccount.Size = new System.Drawing.Size(120, 17);
            this.lblDateAccount.TabIndex = 15;
            this.lblDateAccount.Text = "Date Into Account";
            // 
            // mcDateAccount
            // 
            this.mcDateAccount.Location = new System.Drawing.Point(460, 264);
            this.mcDateAccount.MaxSelectionCount = 1;
            this.mcDateAccount.Name = "mcDateAccount";
            this.mcDateAccount.TabIndex = 16;
            // 
            // addBudgetItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 596);
            this.Controls.Add(this.mcDateAccount);
            this.Controls.Add(this.lblDateAccount);
            this.Controls.Add(this.cbTerm);
            this.Controls.Add(this.lblTerm);
            this.Controls.Add(this.mcDateOccurred);
            this.Controls.Add(this.lblDateOccurred);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.txtOther);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.cbCat);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "addBudgetItem";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.addBudgetItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.ComboBox cbCat;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblDateOccurred;
        private System.Windows.Forms.MonthCalendar mcDateOccurred;
        private System.Windows.Forms.Label lblTerm;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Label lblDateAccount;
        private System.Windows.Forms.MonthCalendar mcDateAccount;
    }
}