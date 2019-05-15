namespace MB_Studio.Manager
{
    partial class SoundManager
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
            this.showGroup_1_btn = new System.Windows.Forms.Button();
            this.groupBox_1_gb = new System.Windows.Forms.GroupBox();
            this.sound2D_cb = new System.Windows.Forms.CheckBox();
            this.priority_tb = new System.Windows.Forms.TrackBar();
            this.volume_tb = new System.Windows.Forms.TrackBar();
            this.volume_lbl = new System.Windows.Forms.Label();
            this.priority_lbl = new System.Windows.Forms.Label();
            this.looping_cb = new System.Windows.Forms.CheckBox();
            this.streamFromHD_cb = new System.Windows.Forms.CheckBox();
            this.alwaysSendViaNetwork_cb = new System.Windows.Forms.CheckBox();
            this.randomStartPosition_cb = new System.Windows.Forms.CheckBox();
            this.soundFiles_lb = new System.Windows.Forms.ListBox();
            this.removeSound_btn = new System.Windows.Forms.Button();
            this.addSound_btn = new System.Windows.Forms.Button();
            this.moveUpSound_btn = new System.Windows.Forms.Button();
            this.moveDownSound_btn = new System.Windows.Forms.Button();
            this.openFile_dialog = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.groupBox_1_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priority_tb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volume_tb)).BeginInit();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "Manager";
            // 
            // plural_name_lbl
            // 
            this.plural_name_lbl.Location = new System.Drawing.Point(415, 112);
            this.plural_name_lbl.Size = new System.Drawing.Size(91, 16);
            this.plural_name_lbl.Text = "Description:";
            // 
            // name_lbl
            // 
            this.name_lbl.Location = new System.Drawing.Point(453, 87);
            this.name_lbl.Size = new System.Drawing.Size(53, 16);
            this.name_lbl.Text = "Name:";
            this.name_lbl.Visible = false;
            // 
            // name_txt
            // 
            this.name_txt.Visible = false;
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.groupBox_1_gb);
            this.toolPanel.Controls.Add(this.showGroup_1_btn);
            this.toolPanel.Size = new System.Drawing.Size(778, 60);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_0_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_1_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_1_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_0_gb, 0);
            // 
            // showGroup_0_btn
            // 
            this.showGroup_0_btn.Tag = "25";
            // 
            // groupBox_0_gb
            // 
            this.groupBox_0_gb.Controls.Add(this.checkBox1);
            this.groupBox_0_gb.Controls.Add(this.moveUpSound_btn);
            this.groupBox_0_gb.Controls.Add(this.moveDownSound_btn);
            this.groupBox_0_gb.Controls.Add(this.addSound_btn);
            this.groupBox_0_gb.Controls.Add(this.removeSound_btn);
            this.groupBox_0_gb.Controls.Add(this.soundFiles_lb);
            this.groupBox_0_gb.Text = "Sound Files";
            this.groupBox_0_gb.Controls.SetChildIndex(this.save_translation_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_cbb, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.soundFiles_lb, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.removeSound_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.addSound_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.moveDownSound_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.moveUpSound_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.checkBox1, 0);
            // 
            // language_lbl
            // 
            this.language_lbl.Visible = false;
            // 
            // pluralNameTranslation_lbl
            // 
            this.pluralNameTranslation_lbl.Location = new System.Drawing.Point(370, 63);
            this.pluralNameTranslation_lbl.Size = new System.Drawing.Size(91, 16);
            this.pluralNameTranslation_lbl.Text = "Description:";
            // 
            // pluralNameTranslation_txt
            // 
            this.pluralNameTranslation_txt.Multiline = true;
            this.pluralNameTranslation_txt.Size = new System.Drawing.Size(257, 95);
            // 
            // singleNameTranslation_lbl
            // 
            this.singleNameTranslation_lbl.Location = new System.Drawing.Point(408, 38);
            this.singleNameTranslation_lbl.Size = new System.Drawing.Size(53, 16);
            this.singleNameTranslation_lbl.Text = "Name:";
            this.singleNameTranslation_lbl.Visible = false;
            // 
            // singleNameTranslation_txt
            // 
            this.singleNameTranslation_txt.Visible = false;
            // 
            // language_cbb
            // 
            this.language_cbb.Visible = false;
            // 
            // save_translation_btn
            // 
            this.save_translation_btn.Visible = false;
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            // 
            // showGroup_1_btn
            // 
            this.showGroup_1_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_1_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_1_btn.Location = new System.Drawing.Point(3, 33);
            this.showGroup_1_btn.Name = "showGroup_1_btn";
            this.showGroup_1_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_1_btn.TabIndex = 24;
            this.showGroup_1_btn.Tag = "-5";
            this.showGroup_1_btn.Text = "v";
            this.showGroup_1_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_1_gb
            // 
            this.groupBox_1_gb.Controls.Add(this.sound2D_cb);
            this.groupBox_1_gb.Controls.Add(this.priority_tb);
            this.groupBox_1_gb.Controls.Add(this.volume_tb);
            this.groupBox_1_gb.Controls.Add(this.volume_lbl);
            this.groupBox_1_gb.Controls.Add(this.priority_lbl);
            this.groupBox_1_gb.Controls.Add(this.looping_cb);
            this.groupBox_1_gb.Controls.Add(this.streamFromHD_cb);
            this.groupBox_1_gb.Controls.Add(this.alwaysSendViaNetwork_cb);
            this.groupBox_1_gb.Controls.Add(this.randomStartPosition_cb);
            this.groupBox_1_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_1_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_1_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_1_gb.Location = new System.Drawing.Point(39, 31);
            this.groupBox_1_gb.Name = "groupBox_1_gb";
            this.groupBox_1_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_1_gb.TabIndex = 23;
            this.groupBox_1_gb.TabStop = false;
            this.groupBox_1_gb.Text = "Flags";
            // 
            // sound2D_cb
            // 
            this.sound2D_cb.AutoSize = true;
            this.sound2D_cb.Location = new System.Drawing.Point(383, 40);
            this.sound2D_cb.Name = "sound2D_cb";
            this.sound2D_cb.Size = new System.Drawing.Size(100, 24);
            this.sound2D_cb.TabIndex = 0;
            this.sound2D_cb.Text = "2D Sound";
            this.sound2D_cb.UseVisualStyleBackColor = true;
            // 
            // priority_tb
            // 
            this.priority_tb.Location = new System.Drawing.Point(618, 69);
            this.priority_tb.Margin = new System.Windows.Forms.Padding(0);
            this.priority_tb.Maximum = 15;
            this.priority_tb.Name = "priority_tb";
            this.priority_tb.Size = new System.Drawing.Size(104, 45);
            this.priority_tb.TabIndex = 10;
            this.priority_tb.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // volume_tb
            // 
            this.volume_tb.Location = new System.Drawing.Point(618, 25);
            this.volume_tb.Maximum = 15;
            this.volume_tb.Name = "volume_tb";
            this.volume_tb.Size = new System.Drawing.Size(104, 45);
            this.volume_tb.TabIndex = 9;
            this.volume_tb.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // volume_lbl
            // 
            this.volume_lbl.AutoSize = true;
            this.volume_lbl.Location = new System.Drawing.Point(547, 36);
            this.volume_lbl.Name = "volume_lbl";
            this.volume_lbl.Size = new System.Drawing.Size(67, 20);
            this.volume_lbl.TabIndex = 8;
            this.volume_lbl.Text = "Volume:";
            // 
            // priority_lbl
            // 
            this.priority_lbl.AutoSize = true;
            this.priority_lbl.Location = new System.Drawing.Point(555, 79);
            this.priority_lbl.Name = "priority_lbl";
            this.priority_lbl.Size = new System.Drawing.Size(60, 20);
            this.priority_lbl.TabIndex = 7;
            this.priority_lbl.Text = "Priority:";
            // 
            // looping_cb
            // 
            this.looping_cb.AutoSize = true;
            this.looping_cb.Location = new System.Drawing.Point(19, 40);
            this.looping_cb.Name = "looping_cb";
            this.looping_cb.Size = new System.Drawing.Size(85, 24);
            this.looping_cb.TabIndex = 4;
            this.looping_cb.Text = "Looping";
            this.looping_cb.UseVisualStyleBackColor = true;
            // 
            // streamFromHD_cb
            // 
            this.streamFromHD_cb.AutoSize = true;
            this.streamFromHD_cb.Location = new System.Drawing.Point(19, 69);
            this.streamFromHD_cb.Name = "streamFromHD_cb";
            this.streamFromHD_cb.Size = new System.Drawing.Size(144, 24);
            this.streamFromHD_cb.TabIndex = 3;
            this.streamFromHD_cb.Text = "Stream from HD";
            this.streamFromHD_cb.UseVisualStyleBackColor = true;
            // 
            // alwaysSendViaNetwork_cb
            // 
            this.alwaysSendViaNetwork_cb.AutoSize = true;
            this.alwaysSendViaNetwork_cb.Location = new System.Drawing.Point(173, 69);
            this.alwaysSendViaNetwork_cb.Name = "alwaysSendViaNetwork_cb";
            this.alwaysSendViaNetwork_cb.Size = new System.Drawing.Size(199, 24);
            this.alwaysSendViaNetwork_cb.TabIndex = 2;
            this.alwaysSendViaNetwork_cb.Text = "Always send via network";
            this.alwaysSendViaNetwork_cb.UseVisualStyleBackColor = true;
            // 
            // randomStartPosition_cb
            // 
            this.randomStartPosition_cb.AutoSize = true;
            this.randomStartPosition_cb.Location = new System.Drawing.Point(173, 39);
            this.randomStartPosition_cb.Name = "randomStartPosition_cb";
            this.randomStartPosition_cb.Size = new System.Drawing.Size(188, 24);
            this.randomStartPosition_cb.TabIndex = 1;
            this.randomStartPosition_cb.Text = "Random Start Position";
            this.randomStartPosition_cb.UseVisualStyleBackColor = true;
            // 
            // soundFiles_lb
            // 
            this.soundFiles_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.soundFiles_lb.FormattingEnabled = true;
            this.soundFiles_lb.ItemHeight = 20;
            this.soundFiles_lb.Location = new System.Drawing.Point(7, 38);
            this.soundFiles_lb.Name = "soundFiles_lb";
            this.soundFiles_lb.Size = new System.Drawing.Size(346, 102);
            this.soundFiles_lb.TabIndex = 21;
            // 
            // removeSound_btn
            // 
            this.removeSound_btn.BackColor = System.Drawing.Color.DimGray;
            this.removeSound_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeSound_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSound_btn.Location = new System.Drawing.Point(352, 89);
            this.removeSound_btn.Name = "removeSound_btn";
            this.removeSound_btn.Size = new System.Drawing.Size(35, 50);
            this.removeSound_btn.TabIndex = 26;
            this.removeSound_btn.Tag = "";
            this.removeSound_btn.Text = "-";
            this.removeSound_btn.UseVisualStyleBackColor = false;
            this.removeSound_btn.Click += new System.EventHandler(this.RemoveSound_btn_Click);
            // 
            // addSound_btn
            // 
            this.addSound_btn.BackColor = System.Drawing.Color.DimGray;
            this.addSound_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addSound_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addSound_btn.Location = new System.Drawing.Point(352, 39);
            this.addSound_btn.Name = "addSound_btn";
            this.addSound_btn.Size = new System.Drawing.Size(35, 51);
            this.addSound_btn.TabIndex = 27;
            this.addSound_btn.Tag = "";
            this.addSound_btn.Text = "+";
            this.addSound_btn.UseVisualStyleBackColor = false;
            this.addSound_btn.Click += new System.EventHandler(this.AddSound_btn_Click);
            // 
            // moveUpSound_btn
            // 
            this.moveUpSound_btn.BackColor = System.Drawing.Color.DimGray;
            this.moveUpSound_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveUpSound_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveUpSound_btn.Location = new System.Drawing.Point(386, 39);
            this.moveUpSound_btn.Name = "moveUpSound_btn";
            this.moveUpSound_btn.Size = new System.Drawing.Size(35, 51);
            this.moveUpSound_btn.TabIndex = 29;
            this.moveUpSound_btn.Tag = "";
            this.moveUpSound_btn.Text = "ʌ";
            this.moveUpSound_btn.UseVisualStyleBackColor = false;
            this.moveUpSound_btn.Click += new System.EventHandler(this.MoveUpSound_btn_Click);
            // 
            // moveDownSound_btn
            // 
            this.moveDownSound_btn.BackColor = System.Drawing.Color.DimGray;
            this.moveDownSound_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveDownSound_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveDownSound_btn.Location = new System.Drawing.Point(386, 89);
            this.moveDownSound_btn.Name = "moveDownSound_btn";
            this.moveDownSound_btn.Size = new System.Drawing.Size(35, 50);
            this.moveDownSound_btn.TabIndex = 28;
            this.moveDownSound_btn.Tag = "";
            this.moveDownSound_btn.Text = "v";
            this.moveDownSound_btn.UseVisualStyleBackColor = false;
            this.moveDownSound_btn.Click += new System.EventHandler(this.MoveDownSound_btn_Click);
            // 
            // openFile_dialog
            // 
            this.openFile_dialog.DefaultExt = "wav";
            this.openFile_dialog.Filter = "WAV-Files|*.wav";
            this.openFile_dialog.Multiselect = true;
            this.openFile_dialog.Title = "Add Sound File (.wav)";
            this.openFile_dialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFile_dialog_FileOk);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(442, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(199, 24);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "Always send via network";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // SoundManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 250);
            this.Name = "SoundManager";
            this.Text = "SoundManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.groupBox_1_gb.ResumeLayout(false);
            this.groupBox_1_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.priority_tb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volume_tb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGroup_1_btn;
        private System.Windows.Forms.GroupBox groupBox_1_gb;
        private System.Windows.Forms.Label volume_lbl;
        private System.Windows.Forms.Label priority_lbl;
        private System.Windows.Forms.CheckBox looping_cb;
        private System.Windows.Forms.CheckBox streamFromHD_cb;
        private System.Windows.Forms.CheckBox alwaysSendViaNetwork_cb;
        private System.Windows.Forms.CheckBox randomStartPosition_cb;
        private System.Windows.Forms.CheckBox sound2D_cb;
        private System.Windows.Forms.TrackBar priority_tb;
        private System.Windows.Forms.TrackBar volume_tb;
        private System.Windows.Forms.Button moveUpSound_btn;
        private System.Windows.Forms.Button moveDownSound_btn;
        private System.Windows.Forms.Button addSound_btn;
        private System.Windows.Forms.Button removeSound_btn;
        private System.Windows.Forms.ListBox soundFiles_lb;
        private System.Windows.Forms.OpenFileDialog openFile_dialog;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
