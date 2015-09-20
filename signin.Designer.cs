namespace Marimba
{
    partial class signin
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lvSignedIn = new System.Windows.Forms.ListView();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.cbInstrument = new System.Windows.Forms.ComboBox();
            this.lblInstrument = new System.Windows.Forms.Label();
            this.cbFaculty = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStudentNumber = new System.Windows.Forms.TextBox();
            this.lblStudentNumber = new System.Windows.Forms.Label();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblFeesPaid = new System.Windows.Forms.Label();
            this.cbFees = new System.Windows.Forms.CheckBox();
            this.tlpAttendance = new System.Windows.Forms.TableLayoutPanel();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblShirtSize = new System.Windows.Forms.Label();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.lvSearch = new System.Windows.Forms.ListView();
            this.FName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.instrument = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StudentID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cbMultiple = new System.Windows.Forms.CheckBox();
            this.btnEditMultiple = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lblTitle, 4);
            this.lblTitle.Font = new System.Drawing.Font("Quicksand Bold", 12.22641F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(153, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(46, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // lvSignedIn
            // 
            this.lvSignedIn.AllowDrop = true;
            this.lvSignedIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSignedIn.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSignedIn.Location = new System.Drawing.Point(1175, 3);
            this.lvSignedIn.Name = "lvSignedIn";
            this.tlpMain.SetRowSpan(this.lvSignedIn, 13);
            this.lvSignedIn.Size = new System.Drawing.Size(296, 697);
            this.lvSignedIn.TabIndex = 24;
            this.lvSignedIn.UseCompatibleStateImageBehavior = false;
            this.lvSignedIn.View = System.Windows.Forms.View.List;
            this.lvSignedIn.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvSignedIn_DragDrop);
            this.lvSignedIn.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSignedIn_DragEnter);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 90);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(144, 40);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Find Member";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.lblDate, 4);
            this.lblDate.Font = new System.Drawing.Font("Quicksand Bold", 12.22641F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(153, 58);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(49, 19);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Date";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(153, 588);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(430, 24);
            this.txtEmail.TabIndex = 8;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(3, 583);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(144, 35);
            this.lblEmail.TabIndex = 16;
            this.lblEmail.Text = "E-Mail Address";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbInstrument
            // 
            this.cbInstrument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tlpMain.SetColumnSpan(this.cbInstrument, 2);
            this.cbInstrument.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbInstrument.FormattingEnabled = true;
            this.cbInstrument.Location = new System.Drawing.Point(739, 553);
            this.cbInstrument.Name = "cbInstrument";
            this.cbInstrument.Size = new System.Drawing.Size(430, 24);
            this.cbInstrument.TabIndex = 7;
            // 
            // lblInstrument
            // 
            this.lblInstrument.AutoSize = true;
            this.lblInstrument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstrument.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrument.Location = new System.Drawing.Point(589, 548);
            this.lblInstrument.Name = "lblInstrument";
            this.lblInstrument.Size = new System.Drawing.Size(144, 35);
            this.lblInstrument.TabIndex = 14;
            this.lblInstrument.Text = "Instrument";
            this.lblInstrument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbFaculty
            // 
            this.cbFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFaculty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFaculty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFaculty.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFaculty.FormattingEnabled = true;
            this.cbFaculty.Items.AddRange(new object[] {
            "Applied Health Science",
            "Arts",
            "Engineering",
            "Environment",
            "Mathematics",
            "Science"});
            this.cbFaculty.Location = new System.Drawing.Point(153, 553);
            this.cbFaculty.Name = "cbFaculty";
            this.cbFaculty.Size = new System.Drawing.Size(430, 24);
            this.cbFaculty.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 548);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 35);
            this.label1.TabIndex = 12;
            this.label1.Text = "Faculty\r\n(if UW student)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStudentNumber
            // 
            this.txtStudentNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.SetColumnSpan(this.txtStudentNumber, 2);
            this.txtStudentNumber.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudentNumber.Location = new System.Drawing.Point(739, 518);
            this.txtStudentNumber.MaxLength = 8;
            this.txtStudentNumber.Name = "txtStudentNumber";
            this.txtStudentNumber.Size = new System.Drawing.Size(430, 24);
            this.txtStudentNumber.TabIndex = 5;
            // 
            // lblStudentNumber
            // 
            this.lblStudentNumber.AutoSize = true;
            this.lblStudentNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStudentNumber.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentNumber.Location = new System.Drawing.Point(589, 513);
            this.lblStudentNumber.Name = "lblStudentNumber";
            this.lblStudentNumber.Size = new System.Drawing.Size(144, 35);
            this.lblStudentNumber.TabIndex = 10;
            this.lblStudentNumber.Text = "Student Number\r\n(if UW student)";
            this.lblStudentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbClass
            // 
            this.cbClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Items.AddRange(new object[] {
            "UW Undergrad Student",
            "UW Grad Student",
            "UW Alumni",
            "Other"});
            this.cbClass.Location = new System.Drawing.Point(153, 518);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(430, 24);
            this.cbClass.TabIndex = 4;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClass.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClass.Location = new System.Drawing.Point(3, 513);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(144, 35);
            this.lblClass.TabIndex = 8;
            this.lblClass.Text = "Type";
            this.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.SetColumnSpan(this.txtLastName, 2);
            this.txtLastName.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(739, 483);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(430, 24);
            this.txtLastName.TabIndex = 3;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastName.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(589, 478);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(144, 35);
            this.lblLastName.TabIndex = 6;
            this.lblLastName.Text = "Last Name";
            this.lblLastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstName.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(153, 483);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(430, 24);
            this.txtFirstName.TabIndex = 2;
            // 
            // btnSignIn
            // 
            this.btnSignIn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpMain.SetColumnSpan(this.btnSignIn, 5);
            this.btnSignIn.Font = new System.Drawing.Font("Quicksand Bold", 12.22641F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.Location = new System.Drawing.Point(536, 663);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(100, 30);
            this.btnSignIn.TabIndex = 10;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFirstName.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(3, 478);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(144, 35);
            this.lblFirstName.TabIndex = 4;
            this.lblFirstName.Text = "First Name";
            this.lblFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFeesPaid
            // 
            this.lblFeesPaid.AutoSize = true;
            this.lblFeesPaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFeesPaid.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeesPaid.Location = new System.Drawing.Point(589, 618);
            this.lblFeesPaid.Name = "lblFeesPaid";
            this.lblFeesPaid.Size = new System.Drawing.Size(144, 35);
            this.lblFeesPaid.TabIndex = 18;
            this.lblFeesPaid.Text = "Fees Paid?";
            this.lblFeesPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbFees
            // 
            this.cbFees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFees.AutoSize = true;
            this.cbFees.Enabled = false;
            this.cbFees.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFees.Location = new System.Drawing.Point(739, 628);
            this.cbFees.Name = "cbFees";
            this.cbFees.Size = new System.Drawing.Size(212, 14);
            this.cbFees.TabIndex = 19;
            this.cbFees.UseVisualStyleBackColor = true;
            // 
            // tlpAttendance
            // 
            this.tlpAttendance.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpAttendance.ColumnCount = 3;
            this.tlpMain.SetColumnSpan(this.tlpAttendance, 6);
            this.tlpAttendance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpAttendance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpAttendance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpAttendance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAttendance.Location = new System.Drawing.Point(3, 706);
            this.tlpAttendance.Name = "tlpAttendance";
            this.tlpAttendance.RowCount = 1;
            this.tlpAttendance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAttendance.Size = new System.Drawing.Size(1468, 51);
            this.tlpAttendance.TabIndex = 23;
            // 
            // sfdSave
            // 
            this.sfdSave.DefaultExt = "mrb";
            this.sfdSave.Filter = "Marimba File|*.mrb";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 6;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 301F));
            this.tlpMain.Controls.Add(this.btnEditMultiple, 4, 10);
            this.tlpMain.Controls.Add(this.cbMultiple, 2, 10);
            this.tlpMain.Controls.Add(this.lblShirtSize, 0, 11);
            this.tlpMain.Controls.Add(this.lvSignedIn, 5, 0);
            this.tlpMain.Controls.Add(this.tlpAttendance, 0, 13);
            this.tlpMain.Controls.Add(this.lblName, 0, 2);
            this.tlpMain.Controls.Add(this.lblFirstName, 0, 7);
            this.tlpMain.Controls.Add(this.lblLastName, 2, 7);
            this.tlpMain.Controls.Add(this.lblStudentNumber, 2, 8);
            this.tlpMain.Controls.Add(this.lblInstrument, 2, 9);
            this.tlpMain.Controls.Add(this.lblFeesPaid, 2, 11);
            this.tlpMain.Controls.Add(this.lblClass, 0, 8);
            this.tlpMain.Controls.Add(this.label1, 0, 9);
            this.tlpMain.Controls.Add(this.lblEmail, 0, 10);
            this.tlpMain.Controls.Add(this.lblDate, 1, 1);
            this.tlpMain.Controls.Add(this.lblTitle, 1, 0);
            this.tlpMain.Controls.Add(this.btnSignIn, 0, 12);
            this.tlpMain.Controls.Add(this.cbSize, 1, 11);
            this.tlpMain.Controls.Add(this.txtEmail, 1, 10);
            this.tlpMain.Controls.Add(this.cbFaculty, 1, 9);
            this.tlpMain.Controls.Add(this.cbClass, 1, 8);
            this.tlpMain.Controls.Add(this.txtFirstName, 1, 7);
            this.tlpMain.Controls.Add(this.cbInstrument, 3, 9);
            this.tlpMain.Controls.Add(this.txtStudentNumber, 3, 8);
            this.tlpMain.Controls.Add(this.cbFees, 3, 11);
            this.tlpMain.Controls.Add(this.txtLastName, 3, 7);
            this.tlpMain.Controls.Add(this.lvSearch, 0, 3);
            this.tlpMain.Controls.Add(this.txtSearch, 1, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Font = new System.Drawing.Font("Quicksand", 12.22641F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 14;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.33186F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.33186F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.33186F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.00443F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(1474, 760);
            this.tlpMain.TabIndex = 0;
            // 
            // lblShirtSize
            // 
            this.lblShirtSize.AutoSize = true;
            this.lblShirtSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblShirtSize.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShirtSize.Location = new System.Drawing.Point(3, 618);
            this.lblShirtSize.Name = "lblShirtSize";
            this.lblShirtSize.Size = new System.Drawing.Size(144, 35);
            this.lblShirtSize.TabIndex = 20;
            this.lblShirtSize.Text = "Shirt Size";
            this.lblShirtSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbSize
            // 
            this.cbSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSize.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Items.AddRange(new object[] {
            "XS",
            "S",
            "M",
            "L",
            "XL",
            "XXL"});
            this.cbSize.Location = new System.Drawing.Point(153, 623);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(430, 24);
            this.cbSize.TabIndex = 9;
            // 
            // lvSearch
            // 
            this.lvSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FName,
            this.instrument,
            this.Email,
            this.StudentID});
            this.tlpMain.SetColumnSpan(this.lvSearch, 5);
            this.lvSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSearch.Font = new System.Drawing.Font("Quicksand Bold", 10.86792F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSearch.Location = new System.Drawing.Point(3, 133);
            this.lvSearch.Name = "lvSearch";
            this.tlpMain.SetRowSpan(this.lvSearch, 3);
            this.lvSearch.Size = new System.Drawing.Size(1166, 312);
            this.lvSearch.TabIndex = 1;
            this.lvSearch.TileSize = new System.Drawing.Size(350, 128);
            this.lvSearch.UseCompatibleStateImageBehavior = false;
            this.lvSearch.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvSearch_ItemDrag);
            this.lvSearch.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvSearch_ItemSelectionChanged);
            this.lvSearch.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSearch_DragEnter);
            this.lvSearch.DoubleClick += new System.EventHandler(this.lvSearch_DoubleClick);
            // 
            // FName
            // 
            this.FName.Text = "First Name";
            // 
            // instrument
            // 
            this.instrument.Text = "Instrument";
            // 
            // Email
            // 
            this.Email.Text = "Email Address";
            // 
            // StudentID
            // 
            this.StudentID.Text = "Student ID";
            // 
            // txtSearch
            // 
            this.txtSearch.AcceptsReturn = true;
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.SetColumnSpan(this.txtSearch, 4);
            this.txtSearch.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(153, 96);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1016, 27);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // cbMultiple
            // 
            this.cbMultiple.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMultiple.AutoSize = true;
            this.tlpMain.SetColumnSpan(this.cbMultiple, 2);
            this.cbMultiple.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMultiple.Location = new System.Drawing.Point(589, 590);
            this.cbMultiple.Name = "cbMultiple";
            this.cbMultiple.Size = new System.Drawing.Size(210, 20);
            this.cbMultiple.TabIndex = 25;
            this.cbMultiple.Text = "Plays Multiple Instruments";
            this.cbMultiple.UseVisualStyleBackColor = true;
            this.cbMultiple.CheckedChanged += new System.EventHandler(this.cbMultiple_CheckedChanged);
            // 
            // btnEditMultiple
            // 
            this.btnEditMultiple.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnEditMultiple.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMultiple.Location = new System.Drawing.Point(957, 586);
            this.btnEditMultiple.Name = "btnEditMultiple";
            this.btnEditMultiple.Size = new System.Drawing.Size(159, 29);
            this.btnEditMultiple.TabIndex = 26;
            this.btnEditMultiple.Text = "Edit Instruments";
            this.btnEditMultiple.UseVisualStyleBackColor = true;
            this.btnEditMultiple.Visible = false;
            this.btnEditMultiple.Click += new System.EventHandler(this.btnEditMultiple_Click);
            // 
            // signin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 760);
            this.Controls.Add(this.tlpMain);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MinimumSize = new System.Drawing.Size(631, 491);
            this.Name = "signin";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.signin_FormClosing);
            this.Load += new System.EventHandler(this.signin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.signin_KeyDown);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lvSignedIn;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.ComboBox cbInstrument;
        private System.Windows.Forms.Label lblInstrument;
        private System.Windows.Forms.ComboBox cbFaculty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStudentNumber;
        private System.Windows.Forms.Label lblStudentNumber;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblFeesPaid;
        private System.Windows.Forms.CheckBox cbFees;
        private System.Windows.Forms.TableLayoutPanel tlpAttendance;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.Label lblShirtSize;
        private System.Windows.Forms.ListView lvSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ColumnHeader FName;
        private System.Windows.Forms.ColumnHeader instrument;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ColumnHeader StudentID;
        private System.Windows.Forms.CheckBox cbMultiple;
        private System.Windows.Forms.Button btnEditMultiple;
    }
}