namespace Marimba
{
    partial class electionForm
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
            this.lblElection = new System.Windows.Forms.Label();
            this.cbTerm = new System.Windows.Forms.ComboBox();
            this.btnReminder = new System.Windows.Forms.Button();
            this.btnCode = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.btnAddCandidate = new System.Windows.Forms.Button();
            this.btnExportBallot = new System.Windows.Forms.Button();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.gbStep3 = new System.Windows.Forms.GroupBox();
            this.gbStep4 = new System.Windows.Forms.GroupBox();
            this.gbStep1.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.gbStep3.SuspendLayout();
            this.gbStep4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblElection
            // 
            this.lblElection.AutoSize = true;
            this.lblElection.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElection.Location = new System.Drawing.Point(38, 24);
            this.lblElection.Name = "lblElection";
            this.lblElection.Size = new System.Drawing.Size(198, 16);
            this.lblElection.TabIndex = 0;
            this.lblElection.Text = "Term before election term:";
            // 
            // cbTerm
            // 
            this.cbTerm.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTerm.FormattingEnabled = true;
            this.cbTerm.Location = new System.Drawing.Point(272, 21);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(188, 24);
            this.cbTerm.TabIndex = 1;
            // 
            // btnReminder
            // 
            this.btnReminder.Enabled = false;
            this.btnReminder.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReminder.Location = new System.Drawing.Point(385, 33);
            this.btnReminder.Name = "btnReminder";
            this.btnReminder.Size = new System.Drawing.Size(135, 47);
            this.btnReminder.TabIndex = 2;
            this.btnReminder.Text = "Notify Unpaid Members";
            this.btnReminder.UseVisualStyleBackColor = true;
            this.btnReminder.Click += new System.EventHandler(this.btnReminder_Click);
            // 
            // btnCode
            // 
            this.btnCode.Enabled = false;
            this.btnCode.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCode.Location = new System.Drawing.Point(196, 30);
            this.btnCode.Name = "btnCode";
            this.btnCode.Size = new System.Drawing.Size(135, 47);
            this.btnCode.TabIndex = 3;
            this.btnCode.Text = "E-Mail Election Codes";
            this.btnCode.UseVisualStyleBackColor = true;
            this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(196, 34);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(135, 47);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import Results From .xlsx File";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(196, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(135, 47);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "Start New Election";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnList
            // 
            this.btnList.Enabled = false;
            this.btnList.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnList.Location = new System.Drawing.Point(385, 31);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(135, 47);
            this.btnList.TabIndex = 6;
            this.btnList.Text = "Export Elector List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // btnAddCandidate
            // 
            this.btnAddCandidate.Enabled = false;
            this.btnAddCandidate.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCandidate.Location = new System.Drawing.Point(196, 33);
            this.btnAddCandidate.Name = "btnAddCandidate";
            this.btnAddCandidate.Size = new System.Drawing.Size(135, 47);
            this.btnAddCandidate.TabIndex = 7;
            this.btnAddCandidate.Text = "Add Candidate";
            this.btnAddCandidate.UseVisualStyleBackColor = true;
            this.btnAddCandidate.Click += new System.EventHandler(this.btnAddCandidate_Click);
            // 
            // btnExportBallot
            // 
            this.btnExportBallot.Enabled = false;
            this.btnExportBallot.Font = new System.Drawing.Font("Quicksand", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportBallot.Location = new System.Drawing.Point(6, 30);
            this.btnExportBallot.Name = "btnExportBallot";
            this.btnExportBallot.Size = new System.Drawing.Size(135, 47);
            this.btnExportBallot.TabIndex = 8;
            this.btnExportBallot.Text = "Export Ballot";
            this.btnExportBallot.UseVisualStyleBackColor = true;
            this.btnExportBallot.Click += new System.EventHandler(this.btnExportBallot_Click);
            // 
            // ofdOpen
            // 
            this.ofdOpen.FileName = "openFileDialog1";
            this.ofdOpen.Filter = "Excel File|*.xlsx";
            // 
            // gbStep1
            // 
            this.gbStep1.Controls.Add(this.btnNew);
            this.gbStep1.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStep1.Location = new System.Drawing.Point(12, 60);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(526, 100);
            this.gbStep1.TabIndex = 9;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Step 1: Start new election";
            // 
            // gbStep2
            // 
            this.gbStep2.Controls.Add(this.btnAddCandidate);
            this.gbStep2.Controls.Add(this.btnReminder);
            this.gbStep2.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStep2.Location = new System.Drawing.Point(12, 176);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(526, 100);
            this.gbStep2.TabIndex = 10;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Step 2: Open nominations for candidates";
            // 
            // gbStep3
            // 
            this.gbStep3.Controls.Add(this.btnExportBallot);
            this.gbStep3.Controls.Add(this.btnCode);
            this.gbStep3.Controls.Add(this.btnList);
            this.gbStep3.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStep3.Location = new System.Drawing.Point(12, 298);
            this.gbStep3.Name = "gbStep3";
            this.gbStep3.Size = new System.Drawing.Size(526, 100);
            this.gbStep3.TabIndex = 11;
            this.gbStep3.TabStop = false;
            this.gbStep3.Text = "Step 3: Open Poll, Send Out E-Mail Codes";
            // 
            // gbStep4
            // 
            this.gbStep4.Controls.Add(this.btnImport);
            this.gbStep4.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStep4.Location = new System.Drawing.Point(12, 404);
            this.gbStep4.Name = "gbStep4";
            this.gbStep4.Size = new System.Drawing.Size(526, 100);
            this.gbStep4.TabIndex = 10;
            this.gbStep4.TabStop = false;
            this.gbStep4.Text = "Step 4: Close poll, count ballots";
            // 
            // electionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 519);
            this.Controls.Add(this.gbStep4);
            this.Controls.Add(this.gbStep3);
            this.Controls.Add(this.gbStep2);
            this.Controls.Add(this.gbStep1);
            this.Controls.Add(this.cbTerm);
            this.Controls.Add(this.lblElection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "electionForm";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.electionForm_Load);
            this.gbStep1.ResumeLayout(false);
            this.gbStep2.ResumeLayout(false);
            this.gbStep3.ResumeLayout(false);
            this.gbStep4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblElection;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Button btnReminder;
        private System.Windows.Forms.Button btnCode;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.Button btnAddCandidate;
        private System.Windows.Forms.Button btnExportBallot;
        private System.Windows.Forms.OpenFileDialog ofdOpen;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.GroupBox gbStep3;
        private System.Windows.Forms.GroupBox gbStep4;
    }
}