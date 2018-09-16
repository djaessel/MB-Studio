namespace MB_Studio_Updater
{
    partial class GUI
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
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            this.console_richtxt = new System.Windows.Forms.RichTextBox();
            this.show_hide_details_btn = new System.Windows.Forms.Button();
            this.update_pb = new System.Windows.Forms.ProgressBar();
            this.progressInfo_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(704, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(736, 0);
            // 
            // title_lbl
            // 
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.Size = new System.Drawing.Size(704, 24);
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // console_richtxt
            // 
            this.console_richtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console_richtxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.console_richtxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.console_richtxt.ForeColor = System.Drawing.Color.LawnGreen;
            this.console_richtxt.Location = new System.Drawing.Point(12, 106);
            this.console_richtxt.Name = "console_richtxt";
            this.console_richtxt.ReadOnly = true;
            this.console_richtxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.console_richtxt.Size = new System.Drawing.Size(744, 394);
            this.console_richtxt.TabIndex = 0;
            this.console_richtxt.TabStop = false;
            this.console_richtxt.Text = "";
            this.console_richtxt.Visible = false;
            // 
            // show_hide_details_btn
            // 
            this.show_hide_details_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.show_hide_details_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.show_hide_details_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_hide_details_btn.Location = new System.Drawing.Point(12, 77);
            this.show_hide_details_btn.Name = "show_hide_details_btn";
            this.show_hide_details_btn.Size = new System.Drawing.Size(105, 23);
            this.show_hide_details_btn.TabIndex = 1;
            this.show_hide_details_btn.Text = "Show details";
            this.show_hide_details_btn.UseVisualStyleBackColor = false;
            this.show_hide_details_btn.Click += new System.EventHandler(this.Show_hide_details_btn_Click);
            // 
            // update_pb
            // 
            this.update_pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.update_pb.Location = new System.Drawing.Point(12, 36);
            this.update_pb.Name = "update_pb";
            this.update_pb.Size = new System.Drawing.Size(744, 10);
            this.update_pb.TabIndex = 0;
            // 
            // progressInfo_lbl
            // 
            this.progressInfo_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressInfo_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressInfo_lbl.Location = new System.Drawing.Point(12, 49);
            this.progressInfo_lbl.Name = "progressInfo_lbl";
            this.progressInfo_lbl.Size = new System.Drawing.Size(744, 20);
            this.progressInfo_lbl.TabIndex = 2;
            this.progressInfo_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 512);
            this.Controls.Add(this.progressInfo_lbl);
            this.Controls.Add(this.update_pb);
            this.Controls.Add(this.show_hide_details_btn);
            this.Controls.Add(this.console_richtxt);
            this.Name = "GUI";
            this.Text = "MB Studio Updater - GUI";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.console_richtxt, 0);
            this.Controls.SetChildIndex(this.show_hide_details_btn, 0);
            this.Controls.SetChildIndex(this.update_pb, 0);
            this.Controls.SetChildIndex(this.progressInfo_lbl, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox console_richtxt;
        private System.Windows.Forms.Button show_hide_details_btn;
        private System.Windows.Forms.ProgressBar update_pb;
        private System.Windows.Forms.Label progressInfo_lbl;
    }
}