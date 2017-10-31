namespace Marimba
{
    partial class newTerm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newTerm));
            this.lblName = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRehearsalStart = new System.Windows.Forms.Label();
            this.mcRehearsalStart = new System.Windows.Forms.MonthCalendar();
            this.updNumRehearsals = new System.Windows.Forms.NumericUpDown();
            this.lblNumRehearsals = new System.Windows.Forms.Label();
            this.lblMembershipFee = new System.Windows.Forms.Label();
            this.txtMembershipFee = new System.Windows.Forms.TextBox();
            this.lblOther = new System.Windows.Forms.Label();
            this.txtOther = new System.Windows.Forms.TextBox();
            this.lblTermStart = new System.Windows.Forms.Label();
            this.lblTermEnd = new System.Windows.Forms.Label();
            this.mcTermStart = new System.Windows.Forms.MonthCalendar();
            this.mcTermEnd = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.updNumRehearsals)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(185, 72);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(123, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name of Term";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(339, 512);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(126, 27);
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Create Term";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(381, 69);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(241, 24);
            this.txtName.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Quicksand", 10.86792F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(328, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 18);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create New Term";
            // 
            // lblRehearsalStart
            // 
            this.lblRehearsalStart.AutoSize = true;
            this.lblRehearsalStart.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRehearsalStart.Location = new System.Drawing.Point(574, 111);
            this.lblRehearsalStart.Name = "lblRehearsalStart";
            this.lblRehearsalStart.Size = new System.Drawing.Size(175, 18);
            this.lblRehearsalStart.TabIndex = 3;
            this.lblRehearsalStart.Text = "Rehearsal Start Date";
            // 
            // mcRehearsalStart
            // 
            this.mcRehearsalStart.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcRehearsalStart.Location = new System.Drawing.Point(542, 138);
            this.mcRehearsalStart.MaxSelectionCount = 1;
            this.mcRehearsalStart.Name = "mcRehearsalStart";
            this.mcRehearsalStart.TabIndex = 4;
            // 
            // updNumRehearsals
            // 
            this.updNumRehearsals.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updNumRehearsals.Location = new System.Drawing.Point(381, 341);
            this.updNumRehearsals.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.updNumRehearsals.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updNumRehearsals.Name = "updNumRehearsals";
            this.updNumRehearsals.Size = new System.Drawing.Size(241, 24);
            this.updNumRehearsals.TabIndex = 6;
            this.updNumRehearsals.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNumRehearsals
            // 
            this.lblNumRehearsals.AutoSize = true;
            this.lblNumRehearsals.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRehearsals.Location = new System.Drawing.Point(185, 343);
            this.lblNumRehearsals.Name = "lblNumRehearsals";
            this.lblNumRehearsals.Size = new System.Drawing.Size(186, 18);
            this.lblNumRehearsals.TabIndex = 5;
            this.lblNumRehearsals.Text = "Number of Rehearsals";
            // 
            // lblMembershipFee
            // 
            this.lblMembershipFee.AutoSize = true;
            this.lblMembershipFee.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembershipFee.Location = new System.Drawing.Point(185, 382);
            this.lblMembershipFee.Name = "lblMembershipFee";
            this.lblMembershipFee.Size = new System.Drawing.Size(143, 18);
            this.lblMembershipFee.TabIndex = 7;
            this.lblMembershipFee.Text = "Membership Fee";
            // 
            // txtMembershipFee
            // 
            this.txtMembershipFee.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMembershipFee.Location = new System.Drawing.Point(381, 379);
            this.txtMembershipFee.Name = "txtMembershipFee";
            this.txtMembershipFee.Size = new System.Drawing.Size(241, 24);
            this.txtMembershipFee.TabIndex = 8;
            this.txtMembershipFee.Text = "20.00";
            // 
            // lblOther
            // 
            this.lblOther.AutoSize = true;
            this.lblOther.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOther.Location = new System.Drawing.Point(185, 420);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(189, 54);
            this.lblOther.TabIndex = 9;
            this.lblOther.Text = "Other Fees\r\n(enter one per line)\r\n(e.g. \"Uniform Fee,15\")";
            // 
            // txtOther
            // 
            this.txtOther.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOther.Location = new System.Drawing.Point(381, 417);
            this.txtOther.Multiline = true;
            this.txtOther.Name = "txtOther";
            this.txtOther.Size = new System.Drawing.Size(241, 76);
            this.txtOther.TabIndex = 10;
            this.txtOther.Text = "Uniform Fee,15";
            // 
            // lblTermStart
            // 
            this.lblTermStart.AutoSize = true;
            this.lblTermStart.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTermStart.Location = new System.Drawing.Point(73, 111);
            this.lblTermStart.Name = "lblTermStart";
            this.lblTermStart.Size = new System.Drawing.Size(138, 18);
            this.lblTermStart.TabIndex = 12;
            this.lblTermStart.Text = "Term Start Date";
            // 
            // lblTermEnd
            // 
            this.lblTermEnd.AutoSize = true;
            this.lblTermEnd.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTermEnd.Location = new System.Drawing.Point(336, 111);
            this.lblTermEnd.Name = "lblTermEnd";
            this.lblTermEnd.Size = new System.Drawing.Size(128, 18);
            this.lblTermEnd.TabIndex = 13;
            this.lblTermEnd.Text = "Term End Date";
            // 
            // mcTermStart
            // 
            this.mcTermStart.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcTermStart.Location = new System.Drawing.Point(24, 138);
            this.mcTermStart.MaxSelectionCount = 1;
            this.mcTermStart.Name = "mcTermStart";
            this.mcTermStart.TabIndex = 14;
            // 
            // mcTermEnd
            // 
            this.mcTermEnd.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcTermEnd.Location = new System.Drawing.Point(283, 138);
            this.mcTermEnd.MaxSelectionCount = 1;
            this.mcTermEnd.Name = "mcTermEnd";
            this.mcTermEnd.TabIndex = 15;
            // 
            // newTerm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 557);
            this.Controls.Add(this.mcTermEnd);
            this.Controls.Add(this.mcTermStart);
            this.Controls.Add(this.lblTermEnd);
            this.Controls.Add(this.lblTermStart);
            this.Controls.Add(this.txtOther);
            this.Controls.Add(this.lblOther);
            this.Controls.Add(this.txtMembershipFee);
            this.Controls.Add(this.lblMembershipFee);
            this.Controls.Add(this.lblNumRehearsals);
            this.Controls.Add(this.updNumRehearsals);
            this.Controls.Add(this.mcRehearsalStart);
            this.Controls.Add(this.lblRehearsalStart);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "newTerm";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            ((System.ComponentModel.ISupportInitialize)(this.updNumRehearsals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRehearsalStart;
        private System.Windows.Forms.MonthCalendar mcRehearsalStart;
        private System.Windows.Forms.NumericUpDown updNumRehearsals;
        private System.Windows.Forms.Label lblNumRehearsals;
        private System.Windows.Forms.Label lblMembershipFee;
        private System.Windows.Forms.TextBox txtMembershipFee;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.TextBox txtOther;
        private System.Windows.Forms.Label lblTermStart;
        private System.Windows.Forms.Label lblTermEnd;
        private System.Windows.Forms.MonthCalendar mcTermStart;
        private System.Windows.Forms.MonthCalendar mcTermEnd;
    }
}