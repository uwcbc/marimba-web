namespace Marimba
{
    partial class budgetView
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
            this.lvMain = new System.Windows.Forms.ListView();
            this.term = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.desc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.debit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.credit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvMain
            // 
            this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.term,
            this.desc,
            this.debit,
            this.credit,
            this.cat,
            this.type,
            this.date});
            this.lvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMain.Font = new System.Drawing.Font("Quicksand", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvMain.FullRowSelect = true;
            this.lvMain.Location = new System.Drawing.Point(0, 0);
            this.lvMain.MultiSelect = false;
            this.lvMain.Name = "lvMain";
            this.lvMain.Size = new System.Drawing.Size(988, 519);
            this.lvMain.TabIndex = 0;
            this.lvMain.UseCompatibleStateImageBehavior = false;
            this.lvMain.View = System.Windows.Forms.View.Details;
            this.lvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvMain_ColumnClick);
            this.lvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMain_KeyDown);
            this.lvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMain_MouseDoubleClick);
            // 
            // term
            // 
            this.term.Text = "Term";
            this.term.Width = 82;
            // 
            // desc
            // 
            this.desc.Text = "Description";
            this.desc.Width = 373;
            // 
            // debit
            // 
            this.debit.Text = "Debit";
            this.debit.Width = 70;
            // 
            // credit
            // 
            this.credit.Text = "Credit";
            this.credit.Width = 70;
            // 
            // cat
            // 
            this.cat.Text = "Category";
            this.cat.Width = 148;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 93;
            // 
            // date
            // 
            this.date.Text = "Date Occurred";
            this.date.Width = 148;
            // 
            // budgetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 519);
            this.Controls.Add(this.lvMain);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.Name = "budgetView";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.budgetView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvMain;
        private System.Windows.Forms.ColumnHeader term;
        private System.Windows.Forms.ColumnHeader desc;
        private System.Windows.Forms.ColumnHeader debit;
        private System.Windows.Forms.ColumnHeader cat;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader credit;
    }
}