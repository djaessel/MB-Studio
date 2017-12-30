namespace MB_Studio
{
    partial class ProjectObject
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.icon_pb = new System.Windows.Forms.PictureBox();
            this.projectName_lbl = new System.Windows.Forms.Label();
            this.projectPath_lbl = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.icon_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // icon_pb
            // 
            this.icon_pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.icon_pb.Location = new System.Drawing.Point(8, 8);
            this.icon_pb.Name = "icon_pb";
            this.icon_pb.Size = new System.Drawing.Size(40, 40);
            this.icon_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon_pb.TabIndex = 0;
            this.icon_pb.TabStop = false;
            // 
            // projectName_lbl
            // 
            this.projectName_lbl.AutoSize = true;
            this.projectName_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectName_lbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.projectName_lbl.Location = new System.Drawing.Point(58, 10);
            this.projectName_lbl.Name = "projectName_lbl";
            this.projectName_lbl.Size = new System.Drawing.Size(0, 20);
            this.projectName_lbl.TabIndex = 1;
            // 
            // projectPath_lbl
            // 
            this.projectPath_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.projectPath_lbl.AutoSize = true;
            this.projectPath_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectPath_lbl.ForeColor = System.Drawing.Color.LightGray;
            this.projectPath_lbl.Location = new System.Drawing.Point(58, 32);
            this.projectPath_lbl.Name = "projectPath_lbl";
            this.projectPath_lbl.Size = new System.Drawing.Size(0, 16);
            this.projectPath_lbl.TabIndex = 2;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1550;
            this.toolTip.BackColor = System.Drawing.Color.Gray;
            this.toolTip.ForeColor = System.Drawing.Color.WhiteSmoke;
            // 
            // ProjectObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.projectPath_lbl);
            this.Controls.Add(this.projectName_lbl);
            this.Controls.Add(this.icon_pb);
            this.Name = "ProjectObject";
            this.Size = new System.Drawing.Size(240, 56);
            ((System.ComponentModel.ISupportInitialize)(this.icon_pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox icon_pb;
        private System.Windows.Forms.Label projectName_lbl;
        private System.Windows.Forms.Label projectPath_lbl;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
