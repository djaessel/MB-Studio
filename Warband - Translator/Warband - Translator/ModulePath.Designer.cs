namespace WarbandTranslator
{
    partial class ModulePath
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
            this.path_txt = new System.Windows.Forms.TextBox();
            this.path_lbl = new System.Windows.Forms.Label();
            this.searchPath_btn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ok_btn = new System.Windows.Forms.Button();
            this.abort_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // path_txt
            // 
            this.path_txt.BackColor = System.Drawing.Color.DarkGray;
            this.path_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.path_txt.ForeColor = System.Drawing.Color.Black;
            this.path_txt.Location = new System.Drawing.Point(49, 12);
            this.path_txt.Name = "path_txt";
            this.path_txt.Size = new System.Drawing.Size(287, 20);
            this.path_txt.TabIndex = 0;
            // 
            // path_lbl
            // 
            this.path_lbl.AutoSize = true;
            this.path_lbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.path_lbl.Location = new System.Drawing.Point(11, 14);
            this.path_lbl.Name = "path_lbl";
            this.path_lbl.Size = new System.Drawing.Size(32, 13);
            this.path_lbl.TabIndex = 1;
            this.path_lbl.Text = "Path:";
            // 
            // searchPath_btn
            // 
            this.searchPath_btn.Location = new System.Drawing.Point(342, 11);
            this.searchPath_btn.Name = "searchPath_btn";
            this.searchPath_btn.Size = new System.Drawing.Size(34, 22);
            this.searchPath_btn.TabIndex = 2;
            this.searchPath_btn.Text = "...";
            this.searchPath_btn.UseVisualStyleBackColor = true;
            this.searchPath_btn.Click += new System.EventHandler(this.searchPath_btn_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(12, 38);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(190, 22);
            this.ok_btn.TabIndex = 3;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // abort_btn
            // 
            this.abort_btn.Location = new System.Drawing.Point(208, 38);
            this.abort_btn.Name = "abort_btn";
            this.abort_btn.Size = new System.Drawing.Size(168, 22);
            this.abort_btn.TabIndex = 4;
            this.abort_btn.Text = "ABORT";
            this.abort_btn.UseVisualStyleBackColor = true;
            this.abort_btn.Click += new System.EventHandler(this.abort_btn_Click);
            // 
            // ModulePath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(388, 63);
            this.Controls.Add(this.abort_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.searchPath_btn);
            this.Controls.Add(this.path_lbl);
            this.Controls.Add(this.path_txt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ModulePath";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Module - Path";
            this.Load += new System.EventHandler(this.ModulePath_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path_txt;
        private System.Windows.Forms.Label path_lbl;
        private System.Windows.Forms.Button searchPath_btn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button abort_btn;
    }
}