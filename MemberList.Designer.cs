namespace Marimba
{
    partial class MemberList
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
            this.lvMain = new System.Windows.Forms.ListView();
            this.FName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Instrument = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EMail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Signuptime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpMember = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnName = new System.Windows.Forms.Button();
            this.btnInstrument = new System.Windows.Forms.Button();
            this.cbDisplay = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cbSearchMode = new System.Windows.Forms.ComboBox();
            this.lblSearchMode = new System.Windows.Forms.Label();
            this.cbCurrentTerm = new System.Windows.Forms.CheckBox();
            this.tlpMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.AllowColumnReorder = true;
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FName,
            this.Instrument,
            this.EMail,
            this.ID,
            this.Signuptime});
            this.tlpMember.SetColumnSpan(this.lvMain, 6);
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Font = new System.Drawing.Font("Quicksand Book", 12.22641F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(3, 103);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(1452, 447);
            this.lvMain.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMain_ColumnClick);
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            // 
            // FName
            // 
            this.FName.Text = "Name";
            this.FName.Width = 300;
            // 
            // Instrument
            // 
            this.Instrument.Text = "Instrument";
            this.Instrument.Width = 133;
            // 
            // EMail
            // 
            this.EMail.Text = "E-Mail Address";
            this.EMail.Width = 251;
            // 
            // ID
            // 
            this.ID.Text = "Student ID";
            this.ID.Width = 133;
            // 
            // Signuptime
            // 
            this.Signuptime.Text = "Sign-up Time";
            this.Signuptime.Width = 210;
            // 
            // tlpMember
            // 
            this.tlpMember.ColumnCount = 6;
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpMember.Controls.Add(this.btnSelect, 5, 0);
            this.tlpMember.Controls.Add(this.lvMain, 0, 2);
            this.tlpMember.Controls.Add(this.btnName, 0, 0);
            this.tlpMember.Controls.Add(this.btnInstrument, 0, 1);
            this.tlpMember.Controls.Add(this.cbDisplay, 5, 1);
            this.tlpMember.Controls.Add(this.txtSearch, 2, 1);
            this.tlpMember.Controls.Add(this.lblSearch, 2, 0);
            this.tlpMember.Controls.Add(this.cbSearchMode, 3, 1);
            this.tlpMember.Controls.Add(this.lblSearchMode, 3, 0);
            this.tlpMember.Controls.Add(this.cbCurrentTerm, 4, 1);
            this.tlpMember.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMember.Location = new System.Drawing.Point(0, 0);
            this.tlpMember.Name = "tlpMember";
            this.tlpMember.RowCount = 3;
            this.tlpMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMember.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMember.Size = new System.Drawing.Size(1458, 553);
            this.tlpMember.TabIndex = 1;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelect.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(1266, 11);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(135, 28);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "Select Members";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnName
            // 
            this.btnName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnName.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnName.Location = new System.Drawing.Point(53, 11);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(135, 28);
            this.btnName.TabIndex = 1;
            this.btnName.Text = "Sort by Name";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // btnInstrument
            // 
            this.btnInstrument.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInstrument.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstrument.Location = new System.Drawing.Point(45, 61);
            this.btnInstrument.Name = "btnInstrument";
            this.btnInstrument.Size = new System.Drawing.Size(152, 28);
            this.btnInstrument.TabIndex = 2;
            this.btnInstrument.Text = "Sort by Instrument";
            this.btnInstrument.UseVisualStyleBackColor = true;
            this.btnInstrument.Click += new System.EventHandler(this.btnInstrument_Click);
            // 
            // cbDisplay
            // 
            this.cbDisplay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisplay.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDisplay.FormattingEnabled = true;
            this.cbDisplay.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "Details",
            "Tile",
            "List"});
            this.cbDisplay.Location = new System.Drawing.Point(1255, 64);
            this.cbDisplay.Name = "cbDisplay";
            this.cbDisplay.Size = new System.Drawing.Size(157, 22);
            this.cbDisplay.TabIndex = 3;
            this.cbDisplay.SelectedIndexChanged += new System.EventHandler(this.cbDisplay_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(487, 64);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(236, 21);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(581, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(48, 14);
            this.lblSearch.TabIndex = 6;
            this.lblSearch.Text = "Search";
            // 
            // cbSearchMode
            // 
            this.cbSearchMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSearchMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchMode.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSearchMode.FormattingEnabled = true;
            this.cbSearchMode.Items.AddRange(new object[] {
            "Name",
            "Instrument",
            "Email Address",
            "Student Number"});
            this.cbSearchMode.Location = new System.Drawing.Point(741, 64);
            this.cbSearchMode.Name = "cbSearchMode";
            this.cbSearchMode.Size = new System.Drawing.Size(211, 22);
            this.cbSearchMode.TabIndex = 5;
            this.cbSearchMode.SelectedIndexChanged += new System.EventHandler(this.cbSearchMode_SelectedIndexChanged);
            // 
            // lblSearchMode
            // 
            this.lblSearchMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSearchMode.AutoSize = true;
            this.lblSearchMode.Font = new System.Drawing.Font("Quicksand Book", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchMode.Location = new System.Drawing.Point(804, 18);
            this.lblSearchMode.Name = "lblSearchMode";
            this.lblSearchMode.Size = new System.Drawing.Size(85, 14);
            this.lblSearchMode.TabIndex = 7;
            this.lblSearchMode.Text = "Search Mode";
            // 
            // cbCurrentTerm
            // 
            this.cbCurrentTerm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCurrentTerm.AutoSize = true;
            this.cbCurrentTerm.Font = new System.Drawing.Font("Quicksand Book", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCurrentTerm.Location = new System.Drawing.Point(985, 59);
            this.cbCurrentTerm.Name = "cbCurrentTerm";
            this.cbCurrentTerm.Size = new System.Drawing.Size(208, 32);
            this.cbCurrentTerm.TabIndex = 9;
            this.cbCurrentTerm.Text = "Only show members in current\r\nterm";
            this.cbCurrentTerm.UseVisualStyleBackColor = true;
            this.cbCurrentTerm.CheckedChanged += new System.EventHandler(this.cbCurrentTerm_CheckedChanged);
            // 
            // MemberList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 553);
            this.Controls.Add(this.tlpMember);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MinimumSize = new System.Drawing.Size(668, 392);
            this.Name = "MemberList";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.MemberList_Load);
            this.tlpMember.ResumeLayout(false);
            this.tlpMember.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader FName;
        private System.Windows.Forms.ColumnHeader Instrument;
        private System.Windows.Forms.ColumnHeader EMail;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Signuptime;
        private System.Windows.Forms.TableLayoutPanel tlpMember;
        private System.Windows.Forms.Button btnInstrument;
        private System.Windows.Forms.Button btnName;
        private System.Windows.Forms.ComboBox cbDisplay;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cbSearchMode;
        private System.Windows.Forms.Label lblSearchMode;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.CheckBox cbCurrentTerm;
    }
}