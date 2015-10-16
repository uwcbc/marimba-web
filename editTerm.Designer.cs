namespace Marimba
{
    partial class frmEditTerm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditTerm));
            this.tlpEditTerm = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectTerm = new System.Windows.Forms.Label();
            this.cboTermList = new System.Windows.Forms.ComboBox();
            this.lbRehearsalDatesListing = new System.Windows.Forms.ListBox();
            this.lblRehearsalDates = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnAddRehearsalDate = new System.Windows.Forms.Button();
            this.btnDeleteRehearsalDate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tlpEditTerm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpEditTerm
            // 
            this.tlpEditTerm.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpEditTerm.ColumnCount = 2;
            this.tlpEditTerm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditTerm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEditTerm.Controls.Add(this.cboTermList, 1, 0);
            this.tlpEditTerm.Controls.Add(this.lblSelectTerm, 0, 0);
            this.tlpEditTerm.Controls.Add(this.lbRehearsalDatesListing, 1, 1);
            this.tlpEditTerm.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tlpEditTerm.Controls.Add(this.btnSave, 0, 2);
            this.tlpEditTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEditTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F);
            this.tlpEditTerm.Location = new System.Drawing.Point(0, 0);
            this.tlpEditTerm.Name = "tlpEditTerm";
            this.tlpEditTerm.RowCount = 3;
            this.tlpEditTerm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpEditTerm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpEditTerm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpEditTerm.Size = new System.Drawing.Size(543, 354);
            this.tlpEditTerm.TabIndex = 0;
            // 
            // lblSelectTerm
            // 
            this.lblSelectTerm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSelectTerm.AutoSize = true;
            this.lblSelectTerm.Location = new System.Drawing.Point(81, 14);
            this.lblSelectTerm.Name = "lblSelectTerm";
            this.lblSelectTerm.Size = new System.Drawing.Size(109, 16);
            this.lblSelectTerm.TabIndex = 2;
            this.lblSelectTerm.Text = "Select a Term:";
            // 
            // cboTermList
            // 
            this.cboTermList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboTermList.FormattingEnabled = true;
            this.cboTermList.Location = new System.Drawing.Point(286, 10);
            this.cboTermList.Name = "cboTermList";
            this.cboTermList.Size = new System.Drawing.Size(241, 24);
            this.cboTermList.TabIndex = 3;
            // 
            // lbRehearsalDatesListing
            // 
            this.lbRehearsalDatesListing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbRehearsalDatesListing.FormattingEnabled = true;
            this.lbRehearsalDatesListing.ItemHeight = 16;
            this.lbRehearsalDatesListing.Location = new System.Drawing.Point(286, 76);
            this.lbRehearsalDatesListing.Name = "lbRehearsalDatesListing";
            this.lbRehearsalDatesListing.Size = new System.Drawing.Size(241, 196);
            this.lbRehearsalDatesListing.TabIndex = 4;
            // 
            // lblRehearsalDates
            // 
            this.lblRehearsalDates.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRehearsalDates.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblRehearsalDates, 2);
            this.lblRehearsalDates.Location = new System.Drawing.Point(70, 34);
            this.lblRehearsalDates.Name = "lblRehearsalDates";
            this.lblRehearsalDates.Size = new System.Drawing.Size(123, 16);
            this.lblRehearsalDates.TabIndex = 5;
            this.lblRehearsalDates.Text = "Rehearsal Dates";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblRehearsalDates, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnMoveDown, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnMoveUp, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddRehearsalDate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteRehearsalDate, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(264, 256);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMoveUp.Location = new System.Drawing.Point(148, 116);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(100, 23);
            this.btnMoveUp.TabIndex = 6;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMoveDown.Location = new System.Drawing.Point(148, 201);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(100, 23);
            this.btnMoveDown.TabIndex = 7;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            // 
            // btnAddRehearsalDate
            // 
            this.btnAddRehearsalDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddRehearsalDate.Location = new System.Drawing.Point(28, 116);
            this.btnAddRehearsalDate.Name = "btnAddRehearsalDate";
            this.btnAddRehearsalDate.Size = new System.Drawing.Size(75, 23);
            this.btnAddRehearsalDate.TabIndex = 8;
            this.btnAddRehearsalDate.Text = "Add";
            this.btnAddRehearsalDate.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRehearsalDate
            // 
            this.btnDeleteRehearsalDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeleteRehearsalDate.Location = new System.Drawing.Point(28, 201);
            this.btnDeleteRehearsalDate.Name = "btnDeleteRehearsalDate";
            this.btnDeleteRehearsalDate.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRehearsalDate.TabIndex = 9;
            this.btnDeleteRehearsalDate.Text = "Delete";
            this.btnDeleteRehearsalDate.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpEditTerm.SetColumnSpan(this.btnSave, 2);
            this.btnSave.Location = new System.Drawing.Point(234, 319);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmEditTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 354);
            this.Controls.Add(this.tlpEditTerm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditTerm";
            this.Text = "Edit Term";
            this.tlpEditTerm.ResumeLayout(false);
            this.tlpEditTerm.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpEditTerm;
        private System.Windows.Forms.Label lblSelectTerm;
        private System.Windows.Forms.ComboBox cboTermList;
        private System.Windows.Forms.ListBox lbRehearsalDatesListing;
        private System.Windows.Forms.Label lblRehearsalDates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnAddRehearsalDate;
        private System.Windows.Forms.Button btnDeleteRehearsalDate;
        private System.Windows.Forms.Button btnSave;
    }
}