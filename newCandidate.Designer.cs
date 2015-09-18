namespace Marimba
{
    partial class newCandidate
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
            this.lblMember = new System.Windows.Forms.Label();
            this.lvCandidate = new System.Windows.Forms.ListView();
            this.Fname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblBlurb = new System.Windows.Forms.Label();
            this.txtBlurb = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lblMember, 0, 0);
            this.tlpMain.Controls.Add(this.lvCandidate, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.Size = new System.Drawing.Size(479, 384);
            this.tlpMain.TabIndex = 0;
            // 
            // lblMember
            // 
            this.lblMember.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMember.AutoSize = true;
            this.lblMember.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMember.Location = new System.Drawing.Point(24, 184);
            this.lblMember.Name = "lblMember";
            this.lblMember.Size = new System.Drawing.Size(61, 15);
            this.lblMember.TabIndex = 0;
            this.lblMember.Text = "Member";
            // 
            // lvCandidate
            // 
            this.lvCandidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCandidate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Fname,
            this.email});
            this.lvCandidate.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvCandidate.Location = new System.Drawing.Point(113, 170);
            this.lvCandidate.Name = "lvCandidate";
            this.lvCandidate.Size = new System.Drawing.Size(363, 44);
            this.lvCandidate.TabIndex = 4;
            this.lvCandidate.TileSize = new System.Drawing.Size(250, 36);
            this.lvCandidate.UseCompatibleStateImageBehavior = false;
            this.lvCandidate.View = System.Windows.Forms.View.Tile;
            // 
            // Fname
            // 
            this.Fname.Text = "Name";
            // 
            // email
            // 
            this.email.Text = "Email Address";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(257, 180);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblBlurb
            // 
            this.lblBlurb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBlurb.AutoSize = true;
            this.lblBlurb.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlurb.Location = new System.Drawing.Point(24, 224);
            this.lblBlurb.Name = "lblBlurb";
            this.lblBlurb.Size = new System.Drawing.Size(41, 15);
            this.lblBlurb.TabIndex = 1;
            this.lblBlurb.Text = "Blurb";
            // 
            // txtBlurb
            // 
            this.txtBlurb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBlurb.Font = new System.Drawing.Font("Quicksand Book", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBlurb.Location = new System.Drawing.Point(93, 123);
            this.txtBlurb.Multiline = true;
            this.txtBlurb.Name = "txtBlurb";
            this.txtBlurb.Size = new System.Drawing.Size(383, 218);
            this.txtBlurb.TabIndex = 2;
            // 
            // newCandidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 384);
            this.Controls.Add(this.tlpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::Marimba.Properties.Resources.Marimba_logo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "newCandidate";
            this.Text = "Marimba";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.Label lblBlurb;
        private System.Windows.Forms.TextBox txtBlurb;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ListView lvCandidate;
        private System.Windows.Forms.ColumnHeader Fname;
        private System.Windows.Forms.ColumnHeader email;
    }
}