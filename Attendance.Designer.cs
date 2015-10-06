namespace Marimba
{
    partial class Attendance
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
            this.btnExport = new System.Windows.Forms.Button();
            this.lblTerm = new System.Windows.Forms.Label();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lvAttendance = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTotalRehearsals = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnExport.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(403, 23);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(84, 34);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblTerm
            // 
            this.lblTerm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTerm.AutoSize = true;
            this.lblTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerm.Location = new System.Drawing.Point(3, 32);
            this.lblTerm.Name = "lblTerm";
            this.lblTerm.Size = new System.Drawing.Size(93, 16);
            this.lblTerm.TabIndex = 2;
            this.lblTerm.Text = "Select Term";
            // 
            // cbTerm
            // 
            this.cbTerm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(153, 28);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(244, 24);
            this.cbTerm.TabIndex = 3;
            this.cbTerm.SelectedIndexChanged += new System.EventHandler(this.cbTerm_SelectedIndexChanged);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File |*.xlsx|CSV File|*.csv";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 4;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 540F));
            this.tlpMain.Controls.Add(this.lblTerm, 0, 0);
            this.tlpMain.Controls.Add(this.cbTerm, 1, 0);
            this.tlpMain.Controls.Add(this.btnExport, 2, 0);
            this.tlpMain.Controls.Add(this.lvAttendance, 0, 1);
            this.tlpMain.Controls.Add(this.lblTotalRehearsals, 3, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1030, 511);
            this.tlpMain.TabIndex = 4;
            // 
            // lvAttendance
            // 
            this.lvAttendance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.tlpMain.SetColumnSpan(this.lvAttendance, 4);
            this.lvAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAttendance.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAttendance.Location = new System.Drawing.Point(3, 83);
            this.lvAttendance.Name = "lvAttendance";
            this.lvAttendance.Size = new System.Drawing.Size(1024, 425);
            this.lvAttendance.TabIndex = 4;
            this.lvAttendance.UseCompatibleStateImageBehavior = false;
            this.lvAttendance.View = System.Windows.Forms.View.Details;
            this.lvAttendance.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAttendance_ColumnClick);
            this.lvAttendance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvAttendance_KeyDown);
            this.lvAttendance.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvAttendance_MouseDoubleClick);
            this.lvAttendance.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvAttendance_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 297;
            // 
            // lblTotalRehearsals
            // 
            this.lblTotalRehearsals.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalRehearsals.AutoSize = true;
            this.lblTotalRehearsals.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotalRehearsals.Font = new System.Drawing.Font("Quicksand", 10.18868F);
            this.lblTotalRehearsals.Location = new System.Drawing.Point(493, 32);
            this.lblTotalRehearsals.Name = "lblTotalRehearsals";
            this.lblTotalRehearsals.Size = new System.Drawing.Size(0, 16);
            this.lblTotalRehearsals.TabIndex = 5;
            // 
            // Attendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 511);
            this.Controls.Add(this.tlpMain);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MinimumSize = new System.Drawing.Size(515, 346);
            this.Name = "Attendance";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Attendance_FormClosing);
            this.Load += new System.EventHandler(this.Attendance_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblTerm;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ListView lvAttendance;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lblTotalRehearsals;
    }
}