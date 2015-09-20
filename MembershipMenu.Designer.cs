namespace Marimba
{
    partial class MembershipMenu
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
            this.btnSignIn = new System.Windows.Forms.Button();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.btnFees = new System.Windows.Forms.Button();
            this.btnMassSignUp = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.svdSave = new System.Windows.Forms.SaveFileDialog();
            this.btnGoogleDoc = new System.Windows.Forms.Button();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnSignIn
            // 
            this.btnSignIn.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.Location = new System.Drawing.Point(29, 30);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(151, 27);
            this.btnSignIn.TabIndex = 0;
            this.btnSignIn.Text = "Sign-in";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnAttendance
            // 
            this.btnAttendance.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendance.Location = new System.Drawing.Point(29, 89);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(151, 27);
            this.btnAttendance.TabIndex = 1;
            this.btnAttendance.Text = "Attendance";
            this.btnAttendance.UseVisualStyleBackColor = true;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.Location = new System.Drawing.Point(29, 148);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(151, 27);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnSignUp
            // 
            this.btnSignUp.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignUp.Location = new System.Drawing.Point(225, 30);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(151, 27);
            this.btnSignUp.TabIndex = 3;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // btnFees
            // 
            this.btnFees.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFees.Location = new System.Drawing.Point(225, 207);
            this.btnFees.Name = "btnFees";
            this.btnFees.Size = new System.Drawing.Size(151, 27);
            this.btnFees.TabIndex = 4;
            this.btnFees.Text = "Fees";
            this.btnFees.UseVisualStyleBackColor = true;
            this.btnFees.Click += new System.EventHandler(this.btnFees_Click);
            // 
            // btnMassSignUp
            // 
            this.btnMassSignUp.Enabled = false;
            this.btnMassSignUp.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMassSignUp.Location = new System.Drawing.Point(225, 89);
            this.btnMassSignUp.Name = "btnMassSignUp";
            this.btnMassSignUp.Size = new System.Drawing.Size(151, 27);
            this.btnMassSignUp.TabIndex = 5;
            this.btnMassSignUp.Text = "Mass Sign Up";
            this.btnMassSignUp.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(29, 207);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(151, 27);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export List";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // svdSave
            // 
            this.svdSave.Filter = "Excel File|*.xlsx|CSV File|*.csv";
            // 
            // btnGoogleDoc
            // 
            this.btnGoogleDoc.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoogleDoc.Location = new System.Drawing.Point(225, 148);
            this.btnGoogleDoc.Name = "btnGoogleDoc";
            this.btnGoogleDoc.Size = new System.Drawing.Size(151, 27);
            this.btnGoogleDoc.TabIndex = 7;
            this.btnGoogleDoc.Text = "Google Doc Import";
            this.btnGoogleDoc.UseVisualStyleBackColor = true;
            this.btnGoogleDoc.Click += new System.EventHandler(this.btnGoogleDoc_Click);
            // 
            // ofdOpen
            // 
            this.ofdOpen.Filter = "CSV File|*.csv";
            // 
            // MembershipMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 264);
            this.Controls.Add(this.btnGoogleDoc);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnMassSignUp);
            this.Controls.Add(this.btnFees);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnAttendance);
            this.Controls.Add(this.btnSignIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "MembershipMenu";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.Button btnFees;
        private System.Windows.Forms.Button btnMassSignUp;
        private System.Windows.Forms.Button btnExport;
        public System.Windows.Forms.SaveFileDialog svdSave;
        private System.Windows.Forms.Button btnGoogleDoc;
        public System.Windows.Forms.OpenFileDialog ofdOpen;
    }
}