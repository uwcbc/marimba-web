namespace Marimba
{
    partial class emailBrowser
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
            this.lvFromTo = new System.Windows.Forms.ListView();
            this.Fname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnForward = new System.Windows.Forms.Button();
            this.wbrView = new System.Windows.Forms.WebBrowser();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtbWrite = new System.Windows.Forms.RichTextBox();
            this.btnReply = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFromTo
            // 
            this.lvFromTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFromTo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Fname,
            this.email});
            this.tlpMain.SetColumnSpan(this.lvFromTo, 2);
            this.lvFromTo.Location = new System.Drawing.Point(153, 3);
            this.lvFromTo.Name = "lvFromTo";
            this.lvFromTo.Size = new System.Drawing.Size(777, 44);
            this.lvFromTo.TabIndex = 0;
            this.lvFromTo.TileSize = new System.Drawing.Size(250, 36);
            this.lvFromTo.UseCompatibleStateImageBehavior = false;
            this.lvFromTo.View = System.Windows.Forms.View.Tile;
            this.lvFromTo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFromTo_MouseDoubleClick);
            // 
            // Fname
            // 
            this.Fname.Text = "Name";
            // 
            // email
            // 
            this.email.Text = "Email Address";
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(3, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(144, 14);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "From";
            // 
            // lblSubject
            // 
            this.lblSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(3, 63);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(144, 14);
            this.lblSubject.TabIndex = 2;
            this.lblSubject.Text = "Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.SetColumnSpan(this.txtSubject, 2);
            this.txtSubject.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(153, 59);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(777, 21);
            this.txtSubject.TabIndex = 1;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.btnForward, 1, 2);
            this.tlpMain.Controls.Add(this.lvFromTo, 1, 0);
            this.tlpMain.Controls.Add(this.txtSubject, 1, 1);
            this.tlpMain.Controls.Add(this.lblFrom, 0, 0);
            this.tlpMain.Controls.Add(this.lblSubject, 0, 1);
            this.tlpMain.Controls.Add(this.wbrView, 0, 3);
            this.tlpMain.Controls.Add(this.btnSend, 0, 4);
            this.tlpMain.Controls.Add(this.rtbWrite, 2, 2);
            this.tlpMain.Controls.Add(this.btnReply, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 5;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(933, 627);
            this.tlpMain.TabIndex = 4;
            // 
            // btnForward
            // 
            this.btnForward.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnForward.Location = new System.Drawing.Point(187, 98);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(75, 23);
            this.btnForward.TabIndex = 6;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Visible = false;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // wbrView
            // 
            this.wbrView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbrView.Location = new System.Drawing.Point(3, 133);
            this.wbrView.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrView.Name = "wbrView";
            this.wbrView.Size = new System.Drawing.Size(144, 451);
            this.wbrView.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpMain.SetColumnSpan(this.btnSend, 3);
            this.btnSend.Font = new System.Drawing.Font("Quicksand", 8.830189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(429, 595);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtbWrite
            // 
            this.rtbWrite.AutoWordSelection = true;
            this.rtbWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbWrite.EnableAutoDragDrop = true;
            this.rtbWrite.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbWrite.Location = new System.Drawing.Point(303, 93);
            this.rtbWrite.Name = "rtbWrite";
            this.tlpMain.SetRowSpan(this.rtbWrite, 2);
            this.rtbWrite.Size = new System.Drawing.Size(627, 491);
            this.rtbWrite.TabIndex = 3;
            this.rtbWrite.Text = "";
            // 
            // btnReply
            // 
            this.btnReply.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReply.Location = new System.Drawing.Point(37, 98);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(75, 23);
            this.btnReply.TabIndex = 5;
            this.btnReply.Text = "Reply";
            this.btnReply.UseVisualStyleBackColor = true;
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // emailBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 627);
            this.Controls.Add(this.tlpMain);
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.Name = "emailBrowser";
            this.Text = "Marimba";
            this.Load += new System.EventHandler(this.emailBrowser_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFromTo;
        private System.Windows.Forms.ColumnHeader Fname;
        private System.Windows.Forms.ColumnHeader email;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.WebBrowser wbrView;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RichTextBox rtbWrite;
        private System.Windows.Forms.Button btnReply;
        private System.Windows.Forms.Button btnForward;
    }
}