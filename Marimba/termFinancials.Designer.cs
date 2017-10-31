namespace Marimba
{
    partial class termFinancials
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpBalanceSheet = new System.Windows.Forms.TabPage();
            this.lvBalance = new System.Windows.Forms.ListView();
            this.chCat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpIncome = new System.Windows.Forms.TabPage();
            this.lvIncome = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpCash = new System.Windows.Forms.TabPage();
            this.lvCash = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblSelect = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tcMain.SuspendLayout();
            this.tpBalanceSheet.SuspendLayout();
            this.tpIncome.SuspendLayout();
            this.tpCash.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tlpMain.SetColumnSpan(this.tcMain, 3);
            this.tcMain.Controls.Add(this.tpBalanceSheet);
            this.tcMain.Controls.Add(this.tpIncome);
            this.tcMain.Controls.Add(this.tpCash);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(3, 73);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(633, 417);
            this.tcMain.TabIndex = 0;
            // 
            // tpBalanceSheet
            // 
            this.tpBalanceSheet.Controls.Add(this.lvBalance);
            this.tpBalanceSheet.Location = new System.Drawing.Point(4, 26);
            this.tpBalanceSheet.Name = "tpBalanceSheet";
            this.tpBalanceSheet.Padding = new System.Windows.Forms.Padding(3);
            this.tpBalanceSheet.Size = new System.Drawing.Size(625, 387);
            this.tpBalanceSheet.TabIndex = 0;
            this.tpBalanceSheet.Text = "Balance Sheet";
            this.tpBalanceSheet.UseVisualStyleBackColor = true;
            // 
            // lvBalance
            // 
            this.lvBalance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCat,
            this.ch1,
            this.ch2,
            this.ch3});
            this.lvBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBalance.Location = new System.Drawing.Point(3, 3);
            this.lvBalance.Name = "lvBalance";
            this.lvBalance.Size = new System.Drawing.Size(619, 381);
            this.lvBalance.TabIndex = 0;
            this.lvBalance.UseCompatibleStateImageBehavior = false;
            this.lvBalance.View = System.Windows.Forms.View.Details;
            // 
            // chCat
            // 
            this.chCat.Text = "";
            this.chCat.Width = 343;
            // 
            // ch1
            // 
            this.ch1.Text = "";
            this.ch1.Width = 90;
            // 
            // ch2
            // 
            this.ch2.Text = "";
            this.ch2.Width = 87;
            // 
            // ch3
            // 
            this.ch3.Text = "";
            this.ch3.Width = 78;
            // 
            // tpIncome
            // 
            this.tpIncome.Controls.Add(this.lvIncome);
            this.tpIncome.Location = new System.Drawing.Point(4, 26);
            this.tpIncome.Name = "tpIncome";
            this.tpIncome.Padding = new System.Windows.Forms.Padding(3);
            this.tpIncome.Size = new System.Drawing.Size(625, 387);
            this.tpIncome.TabIndex = 1;
            this.tpIncome.Text = "Income Statement";
            this.tpIncome.UseVisualStyleBackColor = true;
            // 
            // lvIncome
            // 
            this.lvIncome.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvIncome.Location = new System.Drawing.Point(3, 3);
            this.lvIncome.Name = "lvIncome";
            this.lvIncome.Size = new System.Drawing.Size(619, 381);
            this.lvIncome.TabIndex = 1;
            this.lvIncome.UseCompatibleStateImageBehavior = false;
            this.lvIncome.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 343;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 87;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "";
            this.columnHeader4.Width = 78;
            // 
            // tpCash
            // 
            this.tpCash.Controls.Add(this.lvCash);
            this.tpCash.Location = new System.Drawing.Point(4, 26);
            this.tpCash.Name = "tpCash";
            this.tpCash.Padding = new System.Windows.Forms.Padding(3);
            this.tpCash.Size = new System.Drawing.Size(625, 387);
            this.tpCash.TabIndex = 2;
            this.tpCash.Text = "Cash Flow Statement";
            this.tpCash.UseVisualStyleBackColor = true;
            // 
            // lvCash
            // 
            this.lvCash.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCash.Location = new System.Drawing.Point(3, 3);
            this.lvCash.Name = "lvCash";
            this.lvCash.Size = new System.Drawing.Size(619, 381);
            this.lvCash.TabIndex = 2;
            this.lvCash.UseCompatibleStateImageBehavior = false;
            this.lvCash.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "";
            this.columnHeader5.Width = 343;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "";
            this.columnHeader6.Width = 90;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "";
            this.columnHeader7.Width = 87;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "";
            this.columnHeader8.Width = 78;
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // cbTerm
            // 
            this.cbTerm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(128, 22);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(216, 25);
            this.cbTerm.TabIndex = 2;
            this.cbTerm.SelectedIndexChanged += new System.EventHandler(this.cbTerm_SelectedIndexChanged);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(442, 23);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(178, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Selected Sheet";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelect.Location = new System.Drawing.Point(3, 26);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(105, 18);
            this.lblSelect.TabIndex = 1;
            this.lblSelect.Text = "Select Term";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tlpMain.Controls.Add(this.lblSelect, 0, 0);
            this.tlpMain.Controls.Add(this.tcMain, 0, 1);
            this.tlpMain.Controls.Add(this.btnExport, 2, 0);
            this.tlpMain.Controls.Add(this.cbTerm, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(639, 493);
            this.tlpMain.TabIndex = 4;
            // 
            // termFinancials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 493);
            this.Controls.Add(this.tlpMain);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MinimumSize = new System.Drawing.Size(446, 335);
            this.Name = "termFinancials";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.termFinancials_Load);
            this.tcMain.ResumeLayout(false);
            this.tpBalanceSheet.ResumeLayout(false);
            this.tpIncome.ResumeLayout(false);
            this.tpCash.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpBalanceSheet;
        private System.Windows.Forms.TabPage tpIncome;
        private System.Windows.Forms.TabPage tpCash;
        private System.Windows.Forms.ListView lvBalance;
        private System.Windows.Forms.ColumnHeader chCat;
        private System.Windows.Forms.ColumnHeader ch1;
        private System.Windows.Forms.ColumnHeader ch2;
        private System.Windows.Forms.ColumnHeader ch3;
        private System.Windows.Forms.ListView lvIncome;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView lvCash;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
    }
}