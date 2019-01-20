namespace MB_Studio
{
    partial class MenuDesigner
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
        private new void InitializeComponent()
        {
            this.background_pb = new System.Windows.Forms.PictureBox();
            this.options_panel = new System.Windows.Forms.Panel();
            this.menuText_panel = new System.Windows.Forms.Panel();
            this.menuText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.background_pb)).BeginInit();
            this.menuText_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(743, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(775, 1);
            // 
            // title_lbl
            // 
            this.title_lbl.Size = new System.Drawing.Size(743, 24);
            this.title_lbl.Text = "Form";
            // 
            // background_pb
            // 
            this.background_pb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.background_pb.ImageLocation = ".\\files\\images\\bg2.jpeg";
            this.background_pb.Location = new System.Drawing.Point(1, 24);
            this.background_pb.Name = "background_pb";
            this.background_pb.Size = new System.Drawing.Size(806, 468);
            this.background_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.background_pb.TabIndex = 17;
            this.background_pb.TabStop = false;
            // 
            // options_panel
            // 
            this.options_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.options_panel.AutoScroll = true;
            this.options_panel.BackColor = System.Drawing.Color.Transparent;
            this.options_panel.Location = new System.Drawing.Point(17, 258);
            this.options_panel.Name = "options_panel";
            this.options_panel.Size = new System.Drawing.Size(758, 218);
            this.options_panel.TabIndex = 27;
            // 
            // menuText_panel
            // 
            this.menuText_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuText_panel.AutoScroll = true;
            this.menuText_panel.BackColor = System.Drawing.Color.Transparent;
            this.menuText_panel.Controls.Add(this.menuText);
            this.menuText_panel.Location = new System.Drawing.Point(17, 56);
            this.menuText_panel.Name = "menuText_panel";
            this.menuText_panel.Size = new System.Drawing.Size(758, 199);
            this.menuText_panel.TabIndex = 28;
            // 
            // menuText
            // 
            this.menuText.AutoSize = true;
            this.menuText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuText.ForeColor = System.Drawing.Color.Black;
            this.menuText.Location = new System.Drawing.Point(10, 10);
            this.menuText.Name = "menuText";
            this.menuText.Size = new System.Drawing.Size(0, 20);
            this.menuText.TabIndex = 0;
            // 
            // MenuDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(808, 494);
            this.Controls.Add(this.menuText_panel);
            this.Controls.Add(this.options_panel);
            this.Controls.Add(this.background_pb);
            this.DoubleBuffered = true;
            this.Name = "MenuDesigner";
            this.Text = "MenuDesigner";
            this.Load += new System.EventHandler(this.MenuDesigner_Load);
            this.Controls.SetChildIndex(this.background_pb, 0);
            this.Controls.SetChildIndex(this.options_panel, 0);
            this.Controls.SetChildIndex(this.menuText_panel, 0);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            ((System.ComponentModel.ISupportInitialize)(this.background_pb)).EndInit();
            this.menuText_panel.ResumeLayout(false);
            this.menuText_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox background_pb;
        private System.Windows.Forms.Panel options_panel;
        private System.Windows.Forms.Panel menuText_panel;
        private System.Windows.Forms.Label menuText;
    }
}