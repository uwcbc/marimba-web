namespace Marimba
{
    partial class editMultiInstruments
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lvPlays = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblNotPlayed = new System.Windows.Forms.Label();
            this.lblPlays = new System.Windows.Forms.Label();
            this.lvAvailable = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.lvPlays, 2, 1);
            this.tlpMain.Controls.Add(this.lblNotPlayed, 0, 0);
            this.tlpMain.Controls.Add(this.lblPlays, 2, 0);
            this.tlpMain.Controls.Add(this.lvAvailable, 0, 1);
            this.tlpMain.Controls.Add(this.btnAdd, 1, 2);
            this.tlpMain.Controls.Add(this.btnRemove, 1, 3);
            this.tlpMain.Controls.Add(this.btnSubmit, 1, 4);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(644, 458);
            this.tlpMain.TabIndex = 0;
            // 
            // lvPlays
            // 
            this.lvPlays.AllowDrop = true;
            this.lvPlays.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvPlays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlays.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPlays.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvPlays.Location = new System.Drawing.Point(387, 43);
            this.lvPlays.Name = "lvPlays";
            this.tlpMain.SetRowSpan(this.lvPlays, 4);
            this.lvPlays.Size = new System.Drawing.Size(254, 412);
            this.lvPlays.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlays.TabIndex = 3;
            this.lvPlays.UseCompatibleStateImageBehavior = false;
            this.lvPlays.View = System.Windows.Forms.View.Details;
            this.lvPlays.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvPlays_ItemDrag);
            this.lvPlays.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvPlays_DragDrop);
            this.lvPlays.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvPlays_DragEnter);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 210;
            // 
            // lblNotPlayed
            // 
            this.lblNotPlayed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNotPlayed.AutoSize = true;
            this.lblNotPlayed.Font = new System.Drawing.Font("Quicksand Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotPlayed.Location = new System.Drawing.Point(78, 11);
            this.lblNotPlayed.Name = "lblNotPlayed";
            this.lblNotPlayed.Size = new System.Drawing.Size(103, 18);
            this.lblNotPlayed.TabIndex = 0;
            this.lblNotPlayed.Text = "Instruments";
            // 
            // lblPlays
            // 
            this.lblPlays.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlays.AutoSize = true;
            this.lblPlays.Font = new System.Drawing.Font("Quicksand Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlays.Location = new System.Drawing.Point(433, 11);
            this.lblPlays.Name = "lblPlays";
            this.lblPlays.Size = new System.Drawing.Size(161, 18);
            this.lblPlays.TabIndex = 1;
            this.lblPlays.Text = "Instruments Played";
            // 
            // lvAvailable
            // 
            this.lvAvailable.AllowDrop = true;
            this.lvAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAvailable.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAvailable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvAvailable.Location = new System.Drawing.Point(3, 43);
            this.lvAvailable.Name = "lvAvailable";
            this.tlpMain.SetRowSpan(this.lvAvailable, 4);
            this.lvAvailable.Size = new System.Drawing.Size(253, 412);
            this.lvAvailable.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAvailable.TabIndex = 2;
            this.lvAvailable.UseCompatibleStateImageBehavior = false;
            this.lvAvailable.View = System.Windows.Forms.View.Details;
            this.lvAvailable.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvAvailable_ItemDrag);
            this.lvAvailable.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvAvailable_DragDrop);
            this.lvAvailable.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvAvailable_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 210;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(284, 217);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemove.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(284, 257);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(284, 362);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // editMultiInstruments
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 458);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "editMultiInstruments";
            this.ShowInTaskbar = false;
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.editMultiInstruments_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ListView lvPlays;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblNotPlayed;
        private System.Windows.Forms.Label lblPlays;
        private System.Windows.Forms.ListView lvAvailable;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSubmit;
    }
}