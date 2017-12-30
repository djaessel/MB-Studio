namespace MB_Studio
{
    partial class Loader
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
            this.information_lbl = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pleaseWait_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // information_lbl
            // 
            this.information_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.information_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.information_lbl.ForeColor = System.Drawing.Color.White;
            this.information_lbl.Location = new System.Drawing.Point(0, 0);
            this.information_lbl.Name = "information_lbl";
            this.information_lbl.Size = new System.Drawing.Size(320, 32);
            this.information_lbl.TabIndex = 0;
            this.information_lbl.Text = "Loading ";
            this.information_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(296, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // pleaseWait_lbl
            // 
            this.pleaseWait_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.pleaseWait_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pleaseWait_lbl.ForeColor = System.Drawing.Color.White;
            this.pleaseWait_lbl.Location = new System.Drawing.Point(12, 62);
            this.pleaseWait_lbl.Name = "pleaseWait_lbl";
            this.pleaseWait_lbl.Size = new System.Drawing.Size(100, 25);
            this.pleaseWait_lbl.TabIndex = 2;
            this.pleaseWait_lbl.Text = "Please wait...";
            this.pleaseWait_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(320, 96);
            this.Controls.Add(this.pleaseWait_lbl);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.information_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loader";
            this.Opacity = 0.92D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loader";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label information_lbl;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label pleaseWait_lbl;
    }
}