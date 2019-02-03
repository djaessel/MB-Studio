namespace MB_Studio.Main
{
    partial class PathSelector
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
            this.selectedDir_txt = new System.Windows.Forms.TextBox();
            this.openSelectDialog_btn = new System.Windows.Forms.Button();
            this.dir_lbl = new System.Windows.Forms.Label();
            this.folderBrowser_dlg = new System.Windows.Forms.FolderBrowserDialog();
            this.check_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.Enabled = false;
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(448, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(480, 0);
            // 
            // title_lbl
            // 
            this.title_lbl.Size = new System.Drawing.Size(448, 24);
            this.title_lbl.Text = "Form";
            // 
            // selectedDir_txt
            // 
            this.selectedDir_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedDir_txt.Location = new System.Drawing.Point(90, 42);
            this.selectedDir_txt.Name = "selectedDir_txt";
            this.selectedDir_txt.Size = new System.Drawing.Size(334, 22);
            this.selectedDir_txt.TabIndex = 1;
            // 
            // openSelectDialog_btn
            // 
            this.openSelectDialog_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openSelectDialog_btn.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openSelectDialog_btn.Location = new System.Drawing.Point(430, 42);
            this.openSelectDialog_btn.Name = "openSelectDialog_btn";
            this.openSelectDialog_btn.Size = new System.Drawing.Size(32, 23);
            this.openSelectDialog_btn.TabIndex = 2;
            this.openSelectDialog_btn.Text = "...";
            this.openSelectDialog_btn.UseVisualStyleBackColor = true;
            this.openSelectDialog_btn.Click += new System.EventHandler(this.OpenSelectDialog_btn_Click);
            // 
            // dir_lbl
            // 
            this.dir_lbl.AutoSize = true;
            this.dir_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dir_lbl.Location = new System.Drawing.Point(10, 45);
            this.dir_lbl.Name = "dir_lbl";
            this.dir_lbl.Size = new System.Drawing.Size(75, 16);
            this.dir_lbl.TabIndex = 3;
            this.dir_lbl.Text = "Directory:";
            // 
            // folderBrowser_dlg
            // 
            this.folderBrowser_dlg.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // check_btn
            // 
            this.check_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.check_btn.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.check_btn.Location = new System.Drawing.Point(468, 42);
            this.check_btn.Name = "check_btn";
            this.check_btn.Size = new System.Drawing.Size(32, 23);
            this.check_btn.TabIndex = 4;
            this.check_btn.Text = "√";
            this.check_btn.UseVisualStyleBackColor = true;
            this.check_btn.Click += new System.EventHandler(this.Check_btn_Click);
            // 
            // PathSelector
            // 
            this.ClientSize = new System.Drawing.Size(512, 80);
            this.Controls.Add(this.check_btn);
            this.Controls.Add(this.dir_lbl);
            this.Controls.Add(this.openSelectDialog_btn);
            this.Controls.Add(this.selectedDir_txt);
            this.Name = "PathSelector";
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.selectedDir_txt, 0);
            this.Controls.SetChildIndex(this.openSelectDialog_btn, 0);
            this.Controls.SetChildIndex(this.dir_lbl, 0);
            this.Controls.SetChildIndex(this.check_btn, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox selectedDir_txt;
        private System.Windows.Forms.Button openSelectDialog_btn;
        private System.Windows.Forms.Label dir_lbl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser_dlg;
        private System.Windows.Forms.Button check_btn;
    }
}
