namespace Marimba
{
    partial class MainMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.lvHistory = new System.Windows.Forms.ListView();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.lblHistory = new System.Windows.Forms.Label();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.instrumentLarge = new System.Windows.Forms.ImageList(this.components);
            this.instrumentSmall = new System.Windows.Forms.ImageList(this.components);
            this.lvEmail = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bwReport = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // lvHistory
            // 
            this.lvHistory.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvHistory.Location = new System.Drawing.Point(16, 198);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(722, 125);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.List;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Quicksand", 12.22641F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(12, 151);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(94, 19);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome ";
            // 
            // sfdSave
            // 
            this.sfdSave.DefaultExt = "mrb";
            this.sfdSave.Filter = "Marimba File|*.mrb";
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistory.Location = new System.Drawing.Point(13, 177);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(118, 16);
            this.lblHistory.TabIndex = 14;
            this.lblHistory.Text = "Recent activity:";
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Top;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(750, 139);
            this.elementHost1.TabIndex = 17;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // instrumentLarge
            // 
            this.instrumentLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("instrumentLarge.ImageStream")));
            this.instrumentLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.instrumentLarge.Images.SetKeyName(0, "alto sax.ico");
            this.instrumentLarge.Images.SetKeyName(1, "altoclarinet.ico");
            this.instrumentLarge.Images.SetKeyName(2, "baritonesax.ico");
            this.instrumentLarge.Images.SetKeyName(3, "bassclarinet.ico");
            this.instrumentLarge.Images.SetKeyName(4, "bassoon.ico");
            this.instrumentLarge.Images.SetKeyName(5, "baton.ico");
            this.instrumentLarge.Images.SetKeyName(6, "black_guitar.ico");
            this.instrumentLarge.Images.SetKeyName(7, "clarinet.ico");
            this.instrumentLarge.Images.SetKeyName(8, "contrabass.ico");
            this.instrumentLarge.Images.SetKeyName(9, "drum kit.ico");
            this.instrumentLarge.Images.SetKeyName(10, "drum.ico");
            this.instrumentLarge.Images.SetKeyName(11, "euphonium.ico");
            this.instrumentLarge.Images.SetKeyName(12, "flute.ico");
            this.instrumentLarge.Images.SetKeyName(13, "Horn.ico");
            this.instrumentLarge.Images.SetKeyName(14, "malletpercussion.ico");
            this.instrumentLarge.Images.SetKeyName(15, "musicstand.ico");
            this.instrumentLarge.Images.SetKeyName(16, "oboe.ico");
            this.instrumentLarge.Images.SetKeyName(17, "piano.ico");
            this.instrumentLarge.Images.SetKeyName(18, "piccolo.ico");
            this.instrumentLarge.Images.SetKeyName(19, "sopranosax.ico");
            this.instrumentLarge.Images.SetKeyName(20, "tenorsax.ico");
            this.instrumentLarge.Images.SetKeyName(21, "timpani.ico");
            this.instrumentLarge.Images.SetKeyName(22, "trombone.ico");
            this.instrumentLarge.Images.SetKeyName(23, "trumpet.ico");
            this.instrumentLarge.Images.SetKeyName(24, "elec_bass.ico");
            // 
            // instrumentSmall
            // 
            this.instrumentSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("instrumentSmall.ImageStream")));
            this.instrumentSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.instrumentSmall.Images.SetKeyName(0, "alto sax.ico");
            this.instrumentSmall.Images.SetKeyName(1, "altoclarinet.ico");
            this.instrumentSmall.Images.SetKeyName(2, "baritonesax.ico");
            this.instrumentSmall.Images.SetKeyName(3, "bassclarinet.ico");
            this.instrumentSmall.Images.SetKeyName(4, "bassoon.ico");
            this.instrumentSmall.Images.SetKeyName(5, "baton.ico");
            this.instrumentSmall.Images.SetKeyName(6, "black_guitar.ico");
            this.instrumentSmall.Images.SetKeyName(7, "clarinet.ico");
            this.instrumentSmall.Images.SetKeyName(8, "contrabass.ico");
            this.instrumentSmall.Images.SetKeyName(9, "drum kit.ico");
            this.instrumentSmall.Images.SetKeyName(10, "drum.ico");
            this.instrumentSmall.Images.SetKeyName(11, "euphonium.ico");
            this.instrumentSmall.Images.SetKeyName(12, "flute.ico");
            this.instrumentSmall.Images.SetKeyName(13, "Horn.ico");
            this.instrumentSmall.Images.SetKeyName(14, "malletpercussion.ico");
            this.instrumentSmall.Images.SetKeyName(15, "musicstand.ico");
            this.instrumentSmall.Images.SetKeyName(16, "oboe.ico");
            this.instrumentSmall.Images.SetKeyName(17, "piano.ico");
            this.instrumentSmall.Images.SetKeyName(18, "piccolo.ico");
            this.instrumentSmall.Images.SetKeyName(19, "sopranosax.ico");
            this.instrumentSmall.Images.SetKeyName(20, "tenorsax.ico");
            this.instrumentSmall.Images.SetKeyName(21, "timpani.ico");
            this.instrumentSmall.Images.SetKeyName(22, "trombone.ico");
            this.instrumentSmall.Images.SetKeyName(23, "trumpet.ico");
            this.instrumentSmall.Images.SetKeyName(24, "elec_bass.ico");
            // 
            // lvEmail
            // 
            this.lvEmail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvEmail.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvEmail.FullRowSelect = true;
            this.lvEmail.Location = new System.Drawing.Point(16, 329);
            this.lvEmail.MultiSelect = false;
            this.lvEmail.Name = "lvEmail";
            this.lvEmail.Size = new System.Drawing.Size(722, 310);
            this.lvEmail.TabIndex = 18;
            this.lvEmail.UseCompatibleStateImageBehavior = false;
            this.lvEmail.UseWaitCursor = true;
            this.lvEmail.View = System.Windows.Forms.View.Details;
            this.lvEmail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvEmail_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "From";
            this.columnHeader1.Width = 183;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Subject";
            this.columnHeader2.Width = 290;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Date";
            this.columnHeader3.Width = 175;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            this.columnHeader4.Width = 50;
            // 
            // bwReport
            // 
            this.bwReport.WorkerReportsProgress = true;
            this.bwReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwReport_DoWork);
            this.bwReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwReport_ProgressChanged);
            this.bwReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwReport_RunWorkerCompleted);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 654);
            this.Controls.Add(this.lvEmail);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lvHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "Marimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblHistory;
        public System.Windows.Forms.Integration.ElementHost elementHost1;
        public Ribbon ribbon1;
        public System.Windows.Forms.ImageList instrumentLarge;
        public System.Windows.Forms.ImageList instrumentSmall;
        public System.Windows.Forms.ListView lvEmail;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        public System.ComponentModel.BackgroundWorker bwReport;
        public System.Windows.Forms.SaveFileDialog sfdSave;
    }
}