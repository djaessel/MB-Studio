namespace MB_Decompiler_GUI
{
    partial class DecompilerGUI
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private new void InitializeComponent()
        {
            this.exit_btn = new System.Windows.Forms.Button();
            this.processSelected_btn = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.useAll_btn = new System.Windows.Forms.Button();
            this.readInfo_txt = new System.Windows.Forms.TextBox();
            this.allInfos_cb = new System.Windows.Forms.CheckBox();
            this.title_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // exit_btn
            // 
            this.exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_btn.Location = new System.Drawing.Point(515, 2);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(32, 23);
            this.exit_btn.TabIndex = 0;
            this.exit_btn.Text = "X";
            this.exit_btn.UseVisualStyleBackColor = true;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // processSelected_btn
            // 
            this.processSelected_btn.Location = new System.Drawing.Point(12, 25);
            this.processSelected_btn.Name = "processSelected_btn";
            this.processSelected_btn.Size = new System.Drawing.Size(68, 27);
            this.processSelected_btn.TabIndex = 4;
            this.processSelected_btn.Text = "PROCESS";
            this.processSelected_btn.UseVisualStyleBackColor = true;
            this.processSelected_btn.Click += new System.EventHandler(this.ProcessSelected_btn_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BackColor = System.Drawing.Color.NavajoWhite;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.CheckBoxes = true;
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.Location = new System.Drawing.Point(85, 26);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Scrollable = false;
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(422, 290);
            this.listView.TabIndex = 6;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            // 
            // useAll_btn
            // 
            this.useAll_btn.Location = new System.Drawing.Point(12, 58);
            this.useAll_btn.Name = "useAll_btn";
            this.useAll_btn.Size = new System.Drawing.Size(68, 27);
            this.useAll_btn.TabIndex = 7;
            this.useAll_btn.Text = "ALL FILES";
            this.useAll_btn.UseVisualStyleBackColor = true;
            this.useAll_btn.Click += new System.EventHandler(this.UseAll_btn_Click);
            // 
            // readInfo_txt
            // 
            this.readInfo_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readInfo_txt.BackColor = System.Drawing.Color.NavajoWhite;
            this.readInfo_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readInfo_txt.Location = new System.Drawing.Point(85, 316);
            this.readInfo_txt.Multiline = true;
            this.readInfo_txt.Name = "readInfo_txt";
            this.readInfo_txt.ReadOnly = true;
            this.readInfo_txt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.readInfo_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.readInfo_txt.Size = new System.Drawing.Size(439, 258);
            this.readInfo_txt.TabIndex = 8;
            this.readInfo_txt.TabStop = false;
            // 
            // allInfos_cb
            // 
            this.allInfos_cb.AutoSize = true;
            this.allInfos_cb.Enabled = false;
            this.allInfos_cb.Location = new System.Drawing.Point(12, 91);
            this.allInfos_cb.Name = "allInfos_cb";
            this.allInfos_cb.Size = new System.Drawing.Size(63, 17);
            this.allInfos_cb.TabIndex = 9;
            this.allInfos_cb.Text = "All Infos";
            this.allInfos_cb.UseVisualStyleBackColor = true;
            // 
            // title_lbl
            // 
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.Location = new System.Drawing.Point(0, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(550, 24);
            this.title_lbl.TabIndex = 10;
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DecompilerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(550, 586);
            this.Controls.Add(this.allInfos_cb);
            this.Controls.Add(this.readInfo_txt);
            this.Controls.Add(this.useAll_btn);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.processSelected_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.title_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DecompilerGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MB Decompiler - GUI";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Button processSelected_btn;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button useAll_btn;
        private System.Windows.Forms.TextBox readInfo_txt;
        private System.Windows.Forms.CheckBox allInfos_cb;
        private System.Windows.Forms.Label title_lbl;
    }
}

