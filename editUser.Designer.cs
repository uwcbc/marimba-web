namespace Marimba
{
    partial class editUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editUser));
            this.tlpEditUser = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.tlpEditUser.Controls.Add(this.button1, 0, 2);
            this.tlpEditUser.Controls.Add(this.button2, 0, 3);
            this.tlpEditUser.Controls.Add(this.cboUserID, 1, 0);
            this.tlpEditUser.Controls.Add(this.cboPrivileges, 1, 1);
            this.tlpEditUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditUser.Font = new System.Drawing.Font("Quicksand", 8.830189F);
            this.tlpEditUser.Location = new System.Drawing.Point(0, 0);
            this.tlpEditUser.Name = "tlpEditUser";
            this.tlpEditUser.RowCount = 4;
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEditUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpEditUser.Size = new System.Drawing.Size(365, 296);
            this.tlpEditUser.TabIndex = 0;
            // 
            // lblUserID
            // 
            this.lblUserID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(36, 30);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(55, 14);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "User ID:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Privileges:";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEditUser.SetColumnSpan(this.button1, 2);
            this.button1.Location = new System.Drawing.Point(145, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEditUser.SetColumnSpan(this.button2, 2);
            this.button2.Location = new System.Drawing.Point(145, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cboUserID
            // 
            this.cboUserID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboUserID.FormattingEnabled = true;
            this.cboUserID.Location = new System.Drawing.Point(172, 26);
            this.cboUserID.Name = "cboUserID";
            this.cboUserID.Size = new System.Drawing.Size(148, 22);
            this.cboUserID.TabIndex = 4;
            // 
            // cboPrivileges
            // 
            this.cboPrivileges.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboPrivileges.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPrivileges.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrivileges.FormattingEnabled = true;
            this.cboPrivileges.Location = new System.Drawing.Point(172, 100);
            this.cboPrivileges.Name = "cboPrivileges";
            this.cboPrivileges.Size = new System.Drawing.Size(148, 22);
            this.cboPrivileges.TabIndex = 5;
            // 
            // editUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 296);
            this.Controls.Add(this.tlpEditUser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "editUser";
            this.Text = "Edit User";
            this.tlpEditUser.ResumeLayout(false);
            this.tlpEditUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpEditUser;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cboUserID;
        private System.Windows.Forms.ComboBox cboPrivileges;


    }
}