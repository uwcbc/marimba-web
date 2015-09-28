namespace Marimba
{
    partial class Profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Profile));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.lblOther = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.cbInstrument = new System.Windows.Forms.ComboBox();
            this.lblInstrument = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStudentNumber = new System.Windows.Forms.TextBox();
            this.lblStudentNumber = new System.Windows.Forms.Label();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpDetails = new System.Windows.Forms.TabPage();
            this.btnEditMultiple = new System.Windows.Forms.Button();
            this.cbMultiple = new System.Windows.Forms.CheckBox();
            this.cbShirtSize = new System.Windows.Forms.ComboBox();
            this.lblShirtSize = new System.Windows.Forms.Label();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.lblHistory = new System.Windows.Forms.Label();
            this.tpUnsubscribe = new System.Windows.Forms.TabPage();
            this.btnUnsubscribe = new System.Windows.Forms.Button();
            this.lblUnsubscribe = new System.Windows.Forms.Label();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpDetails.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.tpUnsubscribe.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(432, 12);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(159, 33);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit Changes";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Quicksand", 12.22641F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(21, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(59, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Name";
            // 
            // txtOther
            // 
            this.txtOther.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOther.Location = new System.Drawing.Point(285, 392);
            this.txtOther.Multiline = true;
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(302, 78);
            this.txtOther.TabIndex = 9;
            // 
            // lblOther
            // 
            this.lblOther.AutoSize = true;
            this.lblOther.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOther.Location = new System.Drawing.Point(18, 395);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(193, 80);
            this.lblOther.TabIndex = 31;
            this.lblOther.Text = "Other\r\n(Anything else we should\r\nknow about you, including\r\nother concert band\r\ni" +
    "nstruments you play.)";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(285, 350);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(302, 23);
            this.txtEmail.TabIndex = 8;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(18, 353);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(110, 16);
            this.lblEmail.TabIndex = 29;
            this.lblEmail.Text = "E-Mail Address";
            // 
            // cbInstrument
            // 
            this.cbInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbInstrument.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInstrument.FormattingEnabled = true;
            this.cbInstrument.Location = new System.Drawing.Point(285, 262);
            this.cbInstrument.Name = "cbInstrument";
            this.cbInstrument.Size = new System.Drawing.Size(302, 24);
            this.cbInstrument.TabIndex = 5;
            this.cbInstrument.SelectedIndexChanged += new System.EventHandler(this.cbInstrument_SelectedIndexChanged);
            // 
            // lblInstrument
            // 
            this.lblInstrument.AutoSize = true;
            this.lblInstrument.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrument.Location = new System.Drawing.Point(18, 265);
            this.lblInstrument.Name = "lblInstrument";
            this.lblInstrument.Size = new System.Drawing.Size(86, 16);
            this.lblInstrument.TabIndex = 27;
            this.lblInstrument.Text = "Instrument";
            // 
            // cbFaculty
            // 
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Items.AddRange(new object[] {
            "Applied Health Science",
            "Arts",
            "Engineering",
            "Environment",
            "Mathematics",
            "Science"});
            this.cbFaculty.Location = new System.Drawing.Point(285, 202);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(302, 24);
            this.cbFaculty.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 32);
            this.label2.TabIndex = 25;
            this.label2.Text = "Faculty\r\n(if UW student)";
            // 
            // txtStudentNumber
            // 
            this.txtStudentNumber.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentNumber.Location = new System.Drawing.Point(285, 142);
            this.txtStudentNumber.MaxLength = 8;
            this.txtStudentNumber.Name = "txtStudentNumber";
            this.txtStudentNumber.Size = new System.Drawing.Size(302, 23);
            this.txtStudentNumber.TabIndex = 3;
            // 
            // lblStudentNumber
            // 
            this.lblStudentNumber.AutoSize = true;
            this.lblStudentNumber.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentNumber.Location = new System.Drawing.Point(18, 145);
            this.lblStudentNumber.Name = "lblStudentNumber";
            this.lblStudentNumber.Size = new System.Drawing.Size(126, 32);
            this.lblStudentNumber.TabIndex = 23;
            this.lblStudentNumber.Text = "Student Number\r\n(if UW student)";
            // 
            // cbClass
            // 
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Items.AddRange(new object[] {
            "UW Undergrad Student",
            "UW Grad Student",
            "UW Alumni",
            "Other"});
            this.cbClass.Location = new System.Drawing.Point(285, 100);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(302, 24);
            this.cbClass.TabIndex = 2;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClass.Location = new System.Drawing.Point(18, 103);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(43, 16);
            this.lblClass.TabIndex = 21;
            this.lblClass.Text = "Type";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(285, 58);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(302, 23);
            this.txtLastName.TabIndex = 1;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(18, 61);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(84, 16);
            this.lblLastName.TabIndex = 19;
            this.lblLastName.Text = "Last Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(285, 16);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(302, 23);
            this.txtFirstName.TabIndex = 0;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(18, 19);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(85, 16);
            this.lblFirstName.TabIndex = 17;
            this.lblFirstName.Text = "First Name";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpDetails);
            this.tcMain.Controls.Add(this.tpHistory);
            this.tcMain.Controls.Add(this.tpUnsubscribe);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcMain.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(0, 55);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(603, 574);
            this.tcMain.TabIndex = 33;
            // 
            // tpDetails
            // 
            this.tpDetails.Controls.Add(this.btnEditMultiple);
            this.tpDetails.Controls.Add(this.cbMultiple);
            this.tpDetails.Controls.Add(this.cbShirtSize);
            this.tpDetails.Controls.Add(this.lblShirtSize);
            this.tpDetails.Controls.Add(this.lblFirstName);
            this.tpDetails.Controls.Add(this.txtOther);
            this.tpDetails.Controls.Add(this.txtFirstName);
            this.tpDetails.Controls.Add(this.lblOther);
            this.tpDetails.Controls.Add(this.lblLastName);
            this.tpDetails.Controls.Add(this.txtEmail);
            this.tpDetails.Controls.Add(this.txtLastName);
            this.tpDetails.Controls.Add(this.lblEmail);
            this.tpDetails.Controls.Add(this.lblClass);
            this.tpDetails.Controls.Add(this.cbInstrument);
            this.tpDetails.Controls.Add(this.cbClass);
            this.tpDetails.Controls.Add(this.lblInstrument);
            this.tpDetails.Controls.Add(this.lblStudentNumber);
            this.tpDetails.Controls.Add(this.cbFaculty);
            this.tpDetails.Controls.Add(this.txtStudentNumber);
            this.tpDetails.Controls.Add(this.label2);
            this.tpDetails.Location = new System.Drawing.Point(4, 25);
            this.tpDetails.Name = "tpDetails";
            this.tpDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetails.Size = new System.Drawing.Size(595, 545);
            this.tpDetails.TabIndex = 0;
            this.tpDetails.Text = "Details";
            this.tpDetails.UseVisualStyleBackColor = true;
            // 
            // btnEditMultiple
            // 
            this.btnEditMultiple.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMultiple.Location = new System.Drawing.Point(354, 301);
            this.btnEditMultiple.Name = "btnEditMultiple";
            this.btnEditMultiple.Size = new System.Drawing.Size(159, 33);
            this.btnEditMultiple.TabIndex = 7;
            this.btnEditMultiple.Text = "Edit Instruments";
            this.btnEditMultiple.UseVisualStyleBackColor = true;
            this.btnEditMultiple.Visible = false;
            this.btnEditMultiple.Click += new System.EventHandler(this.btnEditMultiple_Click);
            // 
            // cbMultiple
            // 
            this.cbMultiple.AutoSize = true;
            this.cbMultiple.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMultiple.Location = new System.Drawing.Point(21, 307);
            this.cbMultiple.Name = "cbMultiple";
            this.cbMultiple.Size = new System.Drawing.Size(210, 20);
            this.cbMultiple.TabIndex = 6;
            this.cbMultiple.Text = "Plays Multiple Instruments";
            this.cbMultiple.UseVisualStyleBackColor = true;
            this.cbMultiple.CheckedChanged += new System.EventHandler(this.cbMultiple_CheckedChanged);
            // 
            // cbShirtSize
            // 
            this.cbShirtSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbShirtSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbShirtSize.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShirtSize.FormattingEnabled = true;
            this.cbShirtSize.Items.AddRange(new object[] {
            "XS",
            "S",
            "M",
            "L",
            "XL",
            "XXL"});
            this.cbShirtSize.Location = new System.Drawing.Point(287, 506);
            this.cbShirtSize.Name = "cbShirtSize";
            this.cbShirtSize.Size = new System.Drawing.Size(302, 24);
            this.cbShirtSize.TabIndex = 10;
            // 
            // lblShirtSize
            // 
            this.lblShirtSize.AutoSize = true;
            this.lblShirtSize.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShirtSize.Location = new System.Drawing.Point(18, 509);
            this.lblShirtSize.Name = "lblShirtSize";
            this.lblShirtSize.Size = new System.Drawing.Size(76, 16);
            this.lblShirtSize.TabIndex = 33;
            this.lblShirtSize.Text = "Shirt Size";
            // 
            // tpHistory
            // 
            this.tpHistory.AutoScroll = true;
            this.tpHistory.Controls.Add(this.lblHistory);
            this.tpHistory.Location = new System.Drawing.Point(4, 25);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(595, 545);
            this.tpHistory.TabIndex = 1;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistory.Location = new System.Drawing.Point(3, 3);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(61, 17);
            this.lblHistory.TabIndex = 0;
            this.lblHistory.Text = "History";
            // 
            // tpUnsubscribe
            // 
            this.tpUnsubscribe.Controls.Add(this.btnUnsubscribe);
            this.tpUnsubscribe.Controls.Add(this.lblUnsubscribe);
            this.tpUnsubscribe.Controls.Add(this.btnDeactivate);
            this.tpUnsubscribe.Location = new System.Drawing.Point(4, 25);
            this.tpUnsubscribe.Name = "tpUnsubscribe";
            this.tpUnsubscribe.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnsubscribe.Size = new System.Drawing.Size(595, 545);
            this.tpUnsubscribe.TabIndex = 2;
            this.tpUnsubscribe.Text = "Unsubscribe";
            this.tpUnsubscribe.UseVisualStyleBackColor = true;
            // 
            // btnUnsubscribe
            // 
            this.btnUnsubscribe.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnsubscribe.Location = new System.Drawing.Point(220, 371);
            this.btnUnsubscribe.Name = "btnUnsubscribe";
            this.btnUnsubscribe.Size = new System.Drawing.Size(133, 42);
            this.btnUnsubscribe.TabIndex = 2;
            this.btnUnsubscribe.Text = "Unsubscribe";
            this.btnUnsubscribe.UseVisualStyleBackColor = true;
            this.btnUnsubscribe.Click += new System.EventHandler(this.btnUnsubscribe_Click);
            // 
            // lblUnsubscribe
            // 
            this.lblUnsubscribe.AutoSize = true;
            this.lblUnsubscribe.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnsubscribe.Location = new System.Drawing.Point(53, 36);
            this.lblUnsubscribe.Name = "lblUnsubscribe";
            this.lblUnsubscribe.Size = new System.Drawing.Size(479, 136);
            this.lblUnsubscribe.TabIndex = 1;
            this.lblUnsubscribe.Text = resources.GetString("lblUnsubscribe.Text");
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeactivate.Location = new System.Drawing.Point(220, 259);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(133, 42);
            this.btnDeactivate.TabIndex = 0;
            this.btnDeactivate.Text = "Deactivate";
            this.btnDeactivate.UseVisualStyleBackColor = true;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 629);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "Profile";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.Profile_Load);
            this.tcMain.ResumeLayout(false);
            this.tpDetails.ResumeLayout(false);
            this.tpDetails.PerformLayout();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            this.tpUnsubscribe.ResumeLayout(false);
            this.tpUnsubscribe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.ComboBox cbInstrument;
        private System.Windows.Forms.Label lblInstrument;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStudentNumber;
        private System.Windows.Forms.Label lblStudentNumber;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpDetails;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.TabPage tpUnsubscribe;
        private System.Windows.Forms.Button btnUnsubscribe;
        private System.Windows.Forms.Label lblUnsubscribe;
        private System.Windows.Forms.Button btnDeactivate;
        private System.Windows.Forms.ComboBox cbShirtSize;
        private System.Windows.Forms.Label lblShirtSize;
        private System.Windows.Forms.CheckBox cbMultiple;
        private System.Windows.Forms.Button btnEditMultiple;
    }
}