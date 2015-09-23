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
            this.btnNew = new System.Windows.Forms.Button();
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.btnList = new System.Windows.Forms.Button();
            this.gbStep1.SuspendLayout();
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
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(89, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(135, 47);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "Start New Election";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // ofdOpen
            // 
            this.ofdOpen.FileName = "openFileDialog1";
            this.ofdOpen.Filter = "Excel File|*.xlsx";
            // 
            // gbStep1
            // 
            this.gbStep1.Controls.Add(this.btnList);
            this.gbStep1.Controls.Add(this.btnNew);
            this.gbStep1.Font = new System.Drawing.Font("Quicksand", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStep1.Location = new System.Drawing.Point(12, 60);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(526, 100);
            this.gbStep1.TabIndex = 9;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Setup an Election";
            // 
            // btnList
            // 
            this.btnList.Enabled = false;
            this.btnList.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnList.Location = new System.Drawing.Point(313, 30);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(135, 47);
            this.btnList.TabIndex = 6;
            this.btnList.Text = "Export Elector List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // electionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 180);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblElection;
        private System.Windows.Forms.ComboBox cbTerm;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.OpenFileDialog ofdOpen;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.Button btnList;
    }
}