namespace MB_Studio
{
    partial class ImportsManagerGUI
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
            this.importsData_lb = new System.Windows.Forms.ListBox();
            this.imports_lb = new System.Windows.Forms.ListBox();
            this.description_txt = new System.Windows.Forms.TextBox();
            this.code_txt = new System.Windows.Forms.TextBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.abort_btn = new System.Windows.Forms.Button();
            this.newImports_lb = new System.Windows.Forms.ListBox();
            this.addNewImports_btn = new System.Windows.Forms.Button();
            this.cancelNewImports_btn = new System.Windows.Forms.Button();
            this.importsCount_lbl = new System.Windows.Forms.Label();
            this.descriptionCount_lbl = new System.Windows.Forms.Label();
            this.codeCount_lbl = new System.Windows.Forms.Label();
            this.importsCountX_lbl = new System.Windows.Forms.Label();
            this.descriptionCountX_lbl = new System.Windows.Forms.Label();
            this.codeCountX_lbl = new System.Windows.Forms.Label();
            this.availableImportsCountX_lbl = new System.Windows.Forms.Label();
            this.availableImportsCount_lbl = new System.Windows.Forms.Label();
            this.cancelCurImpDel_btn = new System.Windows.Forms.Button();
            this.deleteCurImp_btn = new System.Windows.Forms.Button();
            this.addNewImport_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // importsData_lb
            // 
            this.importsData_lb.FormattingEnabled = true;
            this.importsData_lb.Location = new System.Drawing.Point(12, 12);
            this.importsData_lb.Name = "importsData_lb";
            this.importsData_lb.Size = new System.Drawing.Size(397, 173);
            this.importsData_lb.TabIndex = 0;
            this.importsData_lb.SelectedIndexChanged += new System.EventHandler(this.ImportsData_lb_SelectedIndexChanged);
            // 
            // imports_lb
            // 
            this.imports_lb.FormattingEnabled = true;
            this.imports_lb.Location = new System.Drawing.Point(12, 205);
            this.imports_lb.Name = "imports_lb";
            this.imports_lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.imports_lb.Size = new System.Drawing.Size(397, 134);
            this.imports_lb.Sorted = true;
            this.imports_lb.TabIndex = 1;
            // 
            // description_txt
            // 
            this.description_txt.Location = new System.Drawing.Point(12, 398);
            this.description_txt.Multiline = true;
            this.description_txt.Name = "description_txt";
            this.description_txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.description_txt.Size = new System.Drawing.Size(797, 164);
            this.description_txt.TabIndex = 2;
            // 
            // code_txt
            // 
            this.code_txt.Location = new System.Drawing.Point(12, 587);
            this.code_txt.Multiline = true;
            this.code_txt.Name = "code_txt";
            this.code_txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.code_txt.Size = new System.Drawing.Size(797, 145);
            this.code_txt.TabIndex = 3;
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(12, 747);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(397, 50);
            this.save_btn.TabIndex = 4;
            this.save_btn.Text = "SAVE";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // abort_btn
            // 
            this.abort_btn.Location = new System.Drawing.Point(445, 747);
            this.abort_btn.Name = "abort_btn";
            this.abort_btn.Size = new System.Drawing.Size(364, 50);
            this.abort_btn.TabIndex = 5;
            this.abort_btn.Text = "ABORT";
            this.abort_btn.UseVisualStyleBackColor = true;
            this.abort_btn.Click += new System.EventHandler(this.Abort_btn_Click);
            // 
            // newImports_lb
            // 
            this.newImports_lb.FormattingEnabled = true;
            this.newImports_lb.Location = new System.Drawing.Point(444, 205);
            this.newImports_lb.Name = "newImports_lb";
            this.newImports_lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.newImports_lb.Size = new System.Drawing.Size(364, 95);
            this.newImports_lb.Sorted = true;
            this.newImports_lb.TabIndex = 6;
            // 
            // addNewImports_btn
            // 
            this.addNewImports_btn.Location = new System.Drawing.Point(445, 306);
            this.addNewImports_btn.Name = "addNewImports_btn";
            this.addNewImports_btn.Size = new System.Drawing.Size(219, 33);
            this.addNewImports_btn.TabIndex = 7;
            this.addNewImports_btn.Text = "Add";
            this.addNewImports_btn.UseVisualStyleBackColor = true;
            this.addNewImports_btn.Click += new System.EventHandler(this.AddNewImports_btn_Click);
            // 
            // cancelNewImports_btn
            // 
            this.cancelNewImports_btn.Location = new System.Drawing.Point(669, 306);
            this.cancelNewImports_btn.Name = "cancelNewImports_btn";
            this.cancelNewImports_btn.Size = new System.Drawing.Size(139, 33);
            this.cancelNewImports_btn.TabIndex = 8;
            this.cancelNewImports_btn.Text = "Cancel";
            this.cancelNewImports_btn.UseVisualStyleBackColor = true;
            this.cancelNewImports_btn.Click += new System.EventHandler(this.CancelNewImports_btn_Click);
            // 
            // importsCount_lbl
            // 
            this.importsCount_lbl.AutoSize = true;
            this.importsCount_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importsCount_lbl.Location = new System.Drawing.Point(478, 41);
            this.importsCount_lbl.Name = "importsCount_lbl";
            this.importsCount_lbl.Size = new System.Drawing.Size(114, 20);
            this.importsCount_lbl.TabIndex = 9;
            this.importsCount_lbl.Text = "Import Lines:";
            // 
            // descriptionCount_lbl
            // 
            this.descriptionCount_lbl.AutoSize = true;
            this.descriptionCount_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionCount_lbl.Location = new System.Drawing.Point(440, 111);
            this.descriptionCount_lbl.Name = "descriptionCount_lbl";
            this.descriptionCount_lbl.Size = new System.Drawing.Size(153, 20);
            this.descriptionCount_lbl.TabIndex = 10;
            this.descriptionCount_lbl.Text = "Description Lines:";
            // 
            // codeCount_lbl
            // 
            this.codeCount_lbl.AutoSize = true;
            this.codeCount_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeCount_lbl.Location = new System.Drawing.Point(489, 140);
            this.codeCount_lbl.Name = "codeCount_lbl";
            this.codeCount_lbl.Size = new System.Drawing.Size(104, 20);
            this.codeCount_lbl.TabIndex = 11;
            this.codeCount_lbl.Text = "Code Lines:";
            // 
            // importsCountX_lbl
            // 
            this.importsCountX_lbl.AutoSize = true;
            this.importsCountX_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importsCountX_lbl.Location = new System.Drawing.Point(595, 41);
            this.importsCountX_lbl.Name = "importsCountX_lbl";
            this.importsCountX_lbl.Size = new System.Drawing.Size(0, 20);
            this.importsCountX_lbl.TabIndex = 12;
            // 
            // descriptionCountX_lbl
            // 
            this.descriptionCountX_lbl.AutoSize = true;
            this.descriptionCountX_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionCountX_lbl.Location = new System.Drawing.Point(595, 111);
            this.descriptionCountX_lbl.Name = "descriptionCountX_lbl";
            this.descriptionCountX_lbl.Size = new System.Drawing.Size(0, 20);
            this.descriptionCountX_lbl.TabIndex = 13;
            // 
            // codeCountX_lbl
            // 
            this.codeCountX_lbl.AutoSize = true;
            this.codeCountX_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeCountX_lbl.Location = new System.Drawing.Point(595, 140);
            this.codeCountX_lbl.Name = "codeCountX_lbl";
            this.codeCountX_lbl.Size = new System.Drawing.Size(0, 20);
            this.codeCountX_lbl.TabIndex = 14;
            // 
            // availableImportsCountX_lbl
            // 
            this.availableImportsCountX_lbl.AutoSize = true;
            this.availableImportsCountX_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableImportsCountX_lbl.Location = new System.Drawing.Point(595, 71);
            this.availableImportsCountX_lbl.Name = "availableImportsCountX_lbl";
            this.availableImportsCountX_lbl.Size = new System.Drawing.Size(0, 20);
            this.availableImportsCountX_lbl.TabIndex = 16;
            // 
            // availableImportsCount_lbl
            // 
            this.availableImportsCount_lbl.AutoSize = true;
            this.availableImportsCount_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableImportsCount_lbl.Location = new System.Drawing.Point(440, 71);
            this.availableImportsCount_lbl.Name = "availableImportsCount_lbl";
            this.availableImportsCount_lbl.Size = new System.Drawing.Size(152, 20);
            this.availableImportsCount_lbl.TabIndex = 15;
            this.availableImportsCount_lbl.Text = "Available Imports:";
            // 
            // cancelCurImpDel_btn
            // 
            this.cancelCurImpDel_btn.Location = new System.Drawing.Point(237, 345);
            this.cancelCurImpDel_btn.Name = "cancelCurImpDel_btn";
            this.cancelCurImpDel_btn.Size = new System.Drawing.Size(172, 33);
            this.cancelCurImpDel_btn.TabIndex = 18;
            this.cancelCurImpDel_btn.Text = "Cancel";
            this.cancelCurImpDel_btn.UseVisualStyleBackColor = true;
            this.cancelCurImpDel_btn.Click += new System.EventHandler(this.CancelImpListBox_btn_Click);
            // 
            // deleteCurImp_btn
            // 
            this.deleteCurImp_btn.Location = new System.Drawing.Point(12, 345);
            this.deleteCurImp_btn.Name = "deleteCurImp_btn";
            this.deleteCurImp_btn.Size = new System.Drawing.Size(219, 33);
            this.deleteCurImp_btn.TabIndex = 17;
            this.deleteCurImp_btn.Text = "Delete";
            this.deleteCurImp_btn.UseVisualStyleBackColor = true;
            this.deleteCurImp_btn.Click += new System.EventHandler(this.DeleteCurImp_btn_Click);
            // 
            // addNewImport_txt
            // 
            this.addNewImport_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewImport_txt.Location = new System.Drawing.Point(444, 349);
            this.addNewImport_txt.Name = "addNewImport_txt";
            this.addNewImport_txt.Size = new System.Drawing.Size(364, 26);
            this.addNewImport_txt.TabIndex = 19;
            // 
            // ImportsManagerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 813);
            this.Controls.Add(this.addNewImport_txt);
            this.Controls.Add(this.cancelCurImpDel_btn);
            this.Controls.Add(this.deleteCurImp_btn);
            this.Controls.Add(this.availableImportsCountX_lbl);
            this.Controls.Add(this.availableImportsCount_lbl);
            this.Controls.Add(this.codeCountX_lbl);
            this.Controls.Add(this.descriptionCountX_lbl);
            this.Controls.Add(this.importsCountX_lbl);
            this.Controls.Add(this.codeCount_lbl);
            this.Controls.Add(this.descriptionCount_lbl);
            this.Controls.Add(this.importsCount_lbl);
            this.Controls.Add(this.cancelNewImports_btn);
            this.Controls.Add(this.addNewImports_btn);
            this.Controls.Add(this.newImports_lb);
            this.Controls.Add(this.abort_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.code_txt);
            this.Controls.Add(this.description_txt);
            this.Controls.Add(this.imports_lb);
            this.Controls.Add(this.importsData_lb);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ImportsManagerGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportsManagerGUI";
            this.Load += new System.EventHandler(this.ImportsManagerGUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox importsData_lb;
        private System.Windows.Forms.ListBox imports_lb;
        private System.Windows.Forms.TextBox description_txt;
        private System.Windows.Forms.TextBox code_txt;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button abort_btn;
        private System.Windows.Forms.ListBox newImports_lb;
        private System.Windows.Forms.Button addNewImports_btn;
        private System.Windows.Forms.Button cancelNewImports_btn;
        private System.Windows.Forms.Label importsCount_lbl;
        private System.Windows.Forms.Label descriptionCount_lbl;
        private System.Windows.Forms.Label codeCount_lbl;
        private System.Windows.Forms.Label importsCountX_lbl;
        private System.Windows.Forms.Label descriptionCountX_lbl;
        private System.Windows.Forms.Label codeCountX_lbl;
        private System.Windows.Forms.Label availableImportsCountX_lbl;
        private System.Windows.Forms.Label availableImportsCount_lbl;
        private System.Windows.Forms.Button cancelCurImpDel_btn;
        private System.Windows.Forms.Button deleteCurImp_btn;
        private System.Windows.Forms.TextBox addNewImport_txt;
    }
}