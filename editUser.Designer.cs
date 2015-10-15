namespace Marimba
{
    partial class frmEditUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditUser));
            this.tlpEditUser = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboUserID = new System.Windows.Forms.ComboBox();
            this.cboPrivileges = new System.Windows.Forms.ComboBox();
            this.tlpEditUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpEditUser
            // 
            this.tlpEditUser.ColumnCount = 2;
            this.tlpEditUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpEditUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpEditUser.Controls.Add(this.lblUserID, 0, 0);
            this.tlpEditUser.Controls.Add(this.label2, 0, 1);
            this.tlpEditUser.Controls.Add(this.btnSave, 0, 2);
            this.tlpEditUser.Controls.Add(this.btnDelete, 0, 3);
            this.tlpEditUser.Controls.Add(this.cboUserID, 1, 0);
            this.tlpEditUser.Controls.Add(this.cboPrivileges, 1, 1);
            this.tlpEditUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditUser.Font = new System.Drawing.Font("Quicksand", 8.830189F);
            this.tlpEditUser.Location = new System.Drawing.Point(0, 0);
            this.tlpEditUser.Name = "tlpEditUser";
            this.tlpEditUser.RowCount = 4;
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpEditUser.Size = new System.Drawing.Size(365, 185);
            this.tlpEditUser.TabIndex = 0;
            // 
            // lblUserID
            // 
            this.lblUserID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(36, 20);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(55, 14);
            this.lblUserID.TabIndex = 5;
            this.lblUserID.Text = "User ID:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Privileges:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEditUser.SetColumnSpan(this.btnSave, 2);
            this.btnSave.Location = new System.Drawing.Point(145, 117);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEditUser.SetColumnSpan(this.btnDelete, 2);
            this.btnDelete.Location = new System.Drawing.Point(145, 154);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboUserID
            // 
            this.cboUserID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboUserID.FormattingEnabled = true;
            this.cboUserID.Location = new System.Drawing.Point(172, 17);
            this.cboUserID.Name = "cboUserID";
            this.cboUserID.Size = new System.Drawing.Size(148, 22);
            this.cboUserID.TabIndex = 0;
            this.cboUserID.SelectedIndexChanged += new System.EventHandler(this.cboUserID_SelectedIndexChanged);
            // 
            // cboPrivileges
            // 
            this.cboPrivileges.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboPrivileges.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPrivileges.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrivileges.FormattingEnabled = true;
            this.cboPrivileges.Items.AddRange(new object[] {
            "Exec",
            "Admin"});
            this.cboPrivileges.Location = new System.Drawing.Point(172, 72);
            this.cboPrivileges.Name = "cboPrivileges";
            this.cboPrivileges.Size = new System.Drawing.Size(148, 22);
            this.cboPrivileges.TabIndex = 1;
            // 
            // frmEditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 185);
            this.Controls.Add(this.tlpEditUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditUser";
            this.Text = "Edit User";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditUser_FormClosed);
            this.Load += new System.EventHandler(this.frmEditUser_Load);
            this.tlpEditUser.ResumeLayout(false);
            this.tlpEditUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpEditUser;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboUserID;
        private System.Windows.Forms.ComboBox cboPrivileges;


    }
}