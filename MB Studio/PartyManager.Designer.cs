namespace MB_Studio
{
    partial class PartyManager
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
            this.map_icon_cbb = new System.Windows.Forms.ComboBox();
            this.carries_gb = new System.Windows.Forms.GroupBox();
            this.carries_goods_txt = new System.Windows.Forms.TextBox();
            this.carries_gold_txt = new System.Windows.Forms.TextBox();
            this.carries_gold_lbl = new System.Windows.Forms.Label();
            this.carries_goods_lbl = new System.Windows.Forms.Label();
            this.quest_party_cb = new System.Windows.Forms.CheckBox();
            this.map_icon_lbl = new System.Windows.Forms.Label();
            this.label_gb = new System.Windows.Forms.GroupBox();
            this.no_label_rb = new System.Windows.Forms.RadioButton();
            this.small_label_rb = new System.Windows.Forms.RadioButton();
            this.medium_label_rb = new System.Windows.Forms.RadioButton();
            this.large_label_rb = new System.Windows.Forms.RadioButton();
            this.set_gb = new System.Windows.Forms.GroupBox();
            this.no_set_rb = new System.Windows.Forms.RadioButton();
            this.village_rb = new System.Windows.Forms.RadioButton();
            this.castle_rb = new System.Windows.Forms.RadioButton();
            this.town_rb = new System.Windows.Forms.RadioButton();
            this.show_faction_cb = new System.Windows.Forms.CheckBox();
            this.dont_attack_civilians_cb = new System.Windows.Forms.CheckBox();
            this.civilian_cb = new System.Windows.Forms.CheckBox();
            this.limit_members_cb = new System.Windows.Forms.CheckBox();
            this.hide_defenders_cb = new System.Windows.Forms.CheckBox();
            this.auto_remove_in_town_cb = new System.Windows.Forms.CheckBox();
            this.default_behavior_cb = new System.Windows.Forms.CheckBox();
            this.always_visible_cb = new System.Windows.Forms.CheckBox();
            this.is_static_cb = new System.Windows.Forms.CheckBox();
            this.is_ship_cb = new System.Windows.Forms.CheckBox();
            this.disabled_cb = new System.Windows.Forms.CheckBox();
            this.showGroup_2_btn = new System.Windows.Forms.Button();
            this.groupBox_2_gb = new System.Windows.Forms.GroupBox();
            this.ai_target_p_cbb = new System.Windows.Forms.ComboBox();
            this.party_template_cbb = new System.Windows.Forms.ComboBox();
            this.menuID_cbb = new System.Windows.Forms.ComboBox();
            this.faction_cbb = new System.Windows.Forms.ComboBox();
            this.ai_bhvr_cbb = new System.Windows.Forms.ComboBox();
            this.ai_target_p_lbl = new System.Windows.Forms.Label();
            this.ai_bhvr_lbl = new System.Windows.Forms.Label();
            this.faction_lbl = new System.Windows.Forms.Label();
            this.party_template_lbl = new System.Windows.Forms.Label();
            this.menuID_lbl = new System.Windows.Forms.Label();
            this.showGroup_3_btn = new System.Windows.Forms.Button();
            this.groupBox_3_gb = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.direction_in_degrees_txt = new System.Windows.Forms.TextBox();
            this.direction_in_degrees_lbl = new System.Windows.Forms.Label();
            this.initial_coordinates_gb = new System.Windows.Forms.GroupBox();
            this.y_axis_txt = new System.Windows.Forms.TextBox();
            this.x_axis_txt = new System.Windows.Forms.TextBox();
            this.y_axis_lbl = new System.Windows.Forms.Label();
            this.x_axis_lbl = new System.Windows.Forms.Label();
            this.personality_gb = new System.Windows.Forms.GroupBox();
            this.banditness_cb = new System.Windows.Forms.CheckBox();
            this.aggressiveness_lbl = new System.Windows.Forms.Label();
            this.aggressiveness_num = new System.Windows.Forms.NumericUpDown();
            this.courage_lbl = new System.Windows.Forms.Label();
            this.courage_num = new System.Windows.Forms.NumericUpDown();
            this.showGroup_4_btn = new System.Windows.Forms.Button();
            this.groupBox_4_gb = new System.Windows.Forms.GroupBox();
            this.stackDownTroop_btn = new System.Windows.Forms.Button();
            this.stackUpTroop_btn = new System.Windows.Forms.Button();
            this.stackRemoveTroop_btn = new System.Windows.Forms.Button();
            this.stackAddTroop_btn = new System.Windows.Forms.Button();
            this.troops_lbl = new System.Windows.Forms.Label();
            this.troops_lb = new System.Windows.Forms.ListBox();
            this.stackTroopCount_txt = new System.Windows.Forms.TextBox();
            this.stackTroopCount_lbl = new System.Windows.Forms.Label();
            this.stack_troop_lbl = new System.Windows.Forms.Label();
            this.stack_troops_lb = new System.Windows.Forms.ListBox();
            this.is_prisoner_cb = new System.Windows.Forms.CheckBox();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.groupBox_1_gb.SuspendLayout();
            this.carries_gb.SuspendLayout();
            this.label_gb.SuspendLayout();
            this.set_gb.SuspendLayout();
            this.groupBox_2_gb.SuspendLayout();
            this.groupBox_3_gb.SuspendLayout();
            this.initial_coordinates_gb.SuspendLayout();
            this.personality_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aggressiveness_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.courage_num)).BeginInit();
            this.groupBox_4_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "ToolForm";
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.groupBox_2_gb);
            this.toolPanel.Controls.Add(this.groupBox_3_gb);
            this.toolPanel.Controls.Add(this.showGroup_4_btn);
            this.toolPanel.Controls.Add(this.groupBox_4_gb);
            this.toolPanel.Controls.Add(this.showGroup_3_btn);
            this.toolPanel.Controls.Add(this.showGroup_2_btn);
            this.toolPanel.Controls.Add(this.showGroup_1_btn);
            this.toolPanel.Controls.Add(this.groupBox_1_gb);
            this.toolPanel.Size = new System.Drawing.Size(778, 142);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_1_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_1_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_2_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_3_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_4_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_4_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_3_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_2_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_0_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_0_btn, 0);
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
            this.showGroup_1_btn.Location = new System.Drawing.Point(3, 32);
            this.showGroup_1_btn.Name = "showGroup_1_btn";
            this.showGroup_1_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_1_btn.TabIndex = 24;
            this.showGroup_1_btn.Tag = "125";
            this.showGroup_1_btn.Text = "v";
            this.showGroup_1_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_1_gb
            // 
            this.groupBox_1_gb.Controls.Add(this.map_icon_cbb);
            this.groupBox_1_gb.Controls.Add(this.carries_gb);
            this.groupBox_1_gb.Controls.Add(this.quest_party_cb);
            this.groupBox_1_gb.Controls.Add(this.map_icon_lbl);
            this.groupBox_1_gb.Controls.Add(this.label_gb);
            this.groupBox_1_gb.Controls.Add(this.set_gb);
            this.groupBox_1_gb.Controls.Add(this.show_faction_cb);
            this.groupBox_1_gb.Controls.Add(this.dont_attack_civilians_cb);
            this.groupBox_1_gb.Controls.Add(this.civilian_cb);
            this.groupBox_1_gb.Controls.Add(this.limit_members_cb);
            this.groupBox_1_gb.Controls.Add(this.hide_defenders_cb);
            this.groupBox_1_gb.Controls.Add(this.auto_remove_in_town_cb);
            this.groupBox_1_gb.Controls.Add(this.default_behavior_cb);
            this.groupBox_1_gb.Controls.Add(this.always_visible_cb);
            this.groupBox_1_gb.Controls.Add(this.is_static_cb);
            this.groupBox_1_gb.Controls.Add(this.is_ship_cb);
            this.groupBox_1_gb.Controls.Add(this.disabled_cb);
            this.groupBox_1_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_1_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_1_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_1_gb.Location = new System.Drawing.Point(39, 30);
            this.groupBox_1_gb.Name = "groupBox_1_gb";
            this.groupBox_1_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_1_gb.TabIndex = 23;
            this.groupBox_1_gb.TabStop = false;
            this.groupBox_1_gb.Tag = "";
            this.groupBox_1_gb.Text = "Flags";
            // 
            // map_icon_cbb
            // 
            this.map_icon_cbb.FormattingEnabled = true;
            this.map_icon_cbb.Location = new System.Drawing.Point(289, 118);
            this.map_icon_cbb.Name = "map_icon_cbb";
            this.map_icon_cbb.Size = new System.Drawing.Size(242, 28);
            this.map_icon_cbb.TabIndex = 37;
            // 
            // carries_gb
            // 
            this.carries_gb.Controls.Add(this.carries_goods_txt);
            this.carries_gb.Controls.Add(this.carries_gold_txt);
            this.carries_gb.Controls.Add(this.carries_gold_lbl);
            this.carries_gb.Controls.Add(this.carries_goods_lbl);
            this.carries_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.carries_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carries_gb.ForeColor = System.Drawing.Color.White;
            this.carries_gb.Location = new System.Drawing.Point(216, 158);
            this.carries_gb.Name = "carries_gb";
            this.carries_gb.Size = new System.Drawing.Size(185, 78);
            this.carries_gb.TabIndex = 36;
            this.carries_gb.TabStop = false;
            this.carries_gb.Text = "Carries";
            // 
            // carries_goods_txt
            // 
            this.carries_goods_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carries_goods_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carries_goods_txt.Location = new System.Drawing.Point(70, 23);
            this.carries_goods_txt.Name = "carries_goods_txt";
            this.carries_goods_txt.Size = new System.Drawing.Size(109, 22);
            this.carries_goods_txt.TabIndex = 36;
            this.carries_goods_txt.Text = "0";
            this.carries_goods_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // carries_gold_txt
            // 
            this.carries_gold_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.carries_gold_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carries_gold_txt.Location = new System.Drawing.Point(70, 47);
            this.carries_gold_txt.Name = "carries_gold_txt";
            this.carries_gold_txt.Size = new System.Drawing.Size(109, 22);
            this.carries_gold_txt.TabIndex = 35;
            this.carries_gold_txt.Text = "0";
            this.carries_gold_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // carries_gold_lbl
            // 
            this.carries_gold_lbl.AutoSize = true;
            this.carries_gold_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carries_gold_lbl.Location = new System.Drawing.Point(19, 51);
            this.carries_gold_lbl.Name = "carries_gold_lbl";
            this.carries_gold_lbl.Size = new System.Drawing.Size(45, 16);
            this.carries_gold_lbl.TabIndex = 34;
            this.carries_gold_lbl.Text = "Gold:";
            // 
            // carries_goods_lbl
            // 
            this.carries_goods_lbl.AutoSize = true;
            this.carries_goods_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carries_goods_lbl.Location = new System.Drawing.Point(6, 25);
            this.carries_goods_lbl.Name = "carries_goods_lbl";
            this.carries_goods_lbl.Size = new System.Drawing.Size(58, 16);
            this.carries_goods_lbl.TabIndex = 32;
            this.carries_goods_lbl.Text = "Goods:";
            // 
            // quest_party_cb
            // 
            this.quest_party_cb.AutoSize = true;
            this.quest_party_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quest_party_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quest_party_cb.Location = new System.Drawing.Point(294, 32);
            this.quest_party_cb.Name = "quest_party_cb";
            this.quest_party_cb.Size = new System.Drawing.Size(104, 20);
            this.quest_party_cb.TabIndex = 30;
            this.quest_party_cb.Text = "Quest Party";
            this.quest_party_cb.UseVisualStyleBackColor = true;
            // 
            // map_icon_lbl
            // 
            this.map_icon_lbl.AutoSize = true;
            this.map_icon_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.map_icon_lbl.Location = new System.Drawing.Point(213, 124);
            this.map_icon_lbl.Name = "map_icon_lbl";
            this.map_icon_lbl.Size = new System.Drawing.Size(75, 16);
            this.map_icon_lbl.TabIndex = 29;
            this.map_icon_lbl.Text = "Map Icon:";
            // 
            // label_gb
            // 
            this.label_gb.Controls.Add(this.no_label_rb);
            this.label_gb.Controls.Add(this.small_label_rb);
            this.label_gb.Controls.Add(this.medium_label_rb);
            this.label_gb.Controls.Add(this.large_label_rb);
            this.label_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_gb.ForeColor = System.Drawing.Color.White;
            this.label_gb.Location = new System.Drawing.Point(117, 110);
            this.label_gb.Name = "label_gb";
            this.label_gb.Size = new System.Drawing.Size(90, 126);
            this.label_gb.TabIndex = 27;
            this.label_gb.TabStop = false;
            this.label_gb.Text = "Label";
            // 
            // no_label_rb
            // 
            this.no_label_rb.AutoSize = true;
            this.no_label_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.no_label_rb.Location = new System.Drawing.Point(10, 22);
            this.no_label_rb.Name = "no_label_rb";
            this.no_label_rb.Size = new System.Drawing.Size(59, 20);
            this.no_label_rb.TabIndex = 33;
            this.no_label_rb.Text = "None";
            this.no_label_rb.UseVisualStyleBackColor = true;
            // 
            // small_label_rb
            // 
            this.small_label_rb.AutoSize = true;
            this.small_label_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.small_label_rb.Location = new System.Drawing.Point(10, 48);
            this.small_label_rb.Name = "small_label_rb";
            this.small_label_rb.Size = new System.Drawing.Size(60, 20);
            this.small_label_rb.TabIndex = 28;
            this.small_label_rb.Text = "Small";
            this.small_label_rb.UseVisualStyleBackColor = true;
            // 
            // medium_label_rb
            // 
            this.medium_label_rb.AutoSize = true;
            this.medium_label_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.medium_label_rb.Location = new System.Drawing.Point(10, 74);
            this.medium_label_rb.Name = "medium_label_rb";
            this.medium_label_rb.Size = new System.Drawing.Size(74, 20);
            this.medium_label_rb.TabIndex = 27;
            this.medium_label_rb.Text = "Medium";
            this.medium_label_rb.UseVisualStyleBackColor = true;
            // 
            // large_label_rb
            // 
            this.large_label_rb.AutoSize = true;
            this.large_label_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.large_label_rb.Location = new System.Drawing.Point(10, 100);
            this.large_label_rb.Name = "large_label_rb";
            this.large_label_rb.Size = new System.Drawing.Size(61, 20);
            this.large_label_rb.TabIndex = 26;
            this.large_label_rb.Text = "Large";
            this.large_label_rb.UseVisualStyleBackColor = true;
            // 
            // set_gb
            // 
            this.set_gb.Controls.Add(this.no_set_rb);
            this.set_gb.Controls.Add(this.village_rb);
            this.set_gb.Controls.Add(this.castle_rb);
            this.set_gb.Controls.Add(this.town_rb);
            this.set_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.set_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.set_gb.ForeColor = System.Drawing.Color.White;
            this.set_gb.Location = new System.Drawing.Point(21, 110);
            this.set_gb.Name = "set_gb";
            this.set_gb.Size = new System.Drawing.Size(90, 126);
            this.set_gb.TabIndex = 26;
            this.set_gb.TabStop = false;
            this.set_gb.Text = "Sets";
            // 
            // no_set_rb
            // 
            this.no_set_rb.AutoSize = true;
            this.no_set_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.no_set_rb.Location = new System.Drawing.Point(11, 22);
            this.no_set_rb.Name = "no_set_rb";
            this.no_set_rb.Size = new System.Drawing.Size(59, 20);
            this.no_set_rb.TabIndex = 32;
            this.no_set_rb.Text = "None";
            this.no_set_rb.UseVisualStyleBackColor = true;
            this.no_set_rb.CheckedChanged += new System.EventHandler(this.None_Set_rb_CheckedChanged);
            // 
            // village_rb
            // 
            this.village_rb.AutoSize = true;
            this.village_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.village_rb.Location = new System.Drawing.Point(11, 48);
            this.village_rb.Name = "village_rb";
            this.village_rb.Size = new System.Drawing.Size(68, 20);
            this.village_rb.TabIndex = 31;
            this.village_rb.Text = "Village";
            this.village_rb.UseVisualStyleBackColor = true;
            this.village_rb.CheckedChanged += new System.EventHandler(this.Village_rb_CheckedChanged);
            // 
            // castle_rb
            // 
            this.castle_rb.AutoSize = true;
            this.castle_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.castle_rb.Location = new System.Drawing.Point(11, 74);
            this.castle_rb.Name = "castle_rb";
            this.castle_rb.Size = new System.Drawing.Size(64, 20);
            this.castle_rb.TabIndex = 30;
            this.castle_rb.Text = "Castle";
            this.castle_rb.UseVisualStyleBackColor = true;
            this.castle_rb.CheckedChanged += new System.EventHandler(this.Castle_rb_CheckedChanged);
            // 
            // town_rb
            // 
            this.town_rb.AutoSize = true;
            this.town_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.town_rb.Location = new System.Drawing.Point(11, 100);
            this.town_rb.Name = "town_rb";
            this.town_rb.Size = new System.Drawing.Size(59, 20);
            this.town_rb.TabIndex = 29;
            this.town_rb.Text = "Town";
            this.town_rb.UseVisualStyleBackColor = true;
            this.town_rb.CheckedChanged += new System.EventHandler(this.Town_rb_CheckedChanged);
            // 
            // show_faction_cb
            // 
            this.show_faction_cb.AutoSize = true;
            this.show_faction_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_faction_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_faction_cb.Location = new System.Drawing.Point(433, 32);
            this.show_faction_cb.Name = "show_faction_cb";
            this.show_faction_cb.Size = new System.Drawing.Size(116, 20);
            this.show_faction_cb.TabIndex = 22;
            this.show_faction_cb.Text = "Show Faction";
            this.show_faction_cb.UseVisualStyleBackColor = true;
            // 
            // dont_attack_civilians_cb
            // 
            this.dont_attack_civilians_cb.AutoSize = true;
            this.dont_attack_civilians_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dont_attack_civilians_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dont_attack_civilians_cb.Location = new System.Drawing.Point(433, 58);
            this.dont_attack_civilians_cb.Name = "dont_attack_civilians_cb";
            this.dont_attack_civilians_cb.Size = new System.Drawing.Size(165, 20);
            this.dont_attack_civilians_cb.TabIndex = 21;
            this.dont_attack_civilians_cb.Text = "Dont attack Civilians";
            this.dont_attack_civilians_cb.UseVisualStyleBackColor = true;
            // 
            // civilian_cb
            // 
            this.civilian_cb.AutoSize = true;
            this.civilian_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.civilian_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.civilian_cb.Location = new System.Drawing.Point(433, 84);
            this.civilian_cb.Name = "civilian_cb";
            this.civilian_cb.Size = new System.Drawing.Size(75, 20);
            this.civilian_cb.TabIndex = 20;
            this.civilian_cb.Text = "Civilian";
            this.civilian_cb.UseVisualStyleBackColor = true;
            // 
            // limit_members_cb
            // 
            this.limit_members_cb.AutoSize = true;
            this.limit_members_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.limit_members_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.limit_members_cb.Location = new System.Drawing.Point(294, 58);
            this.limit_members_cb.Name = "limit_members_cb";
            this.limit_members_cb.Size = new System.Drawing.Size(124, 20);
            this.limit_members_cb.TabIndex = 18;
            this.limit_members_cb.Text = "Limit Members";
            this.limit_members_cb.UseVisualStyleBackColor = true;
            // 
            // hide_defenders_cb
            // 
            this.hide_defenders_cb.AutoSize = true;
            this.hide_defenders_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hide_defenders_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hide_defenders_cb.Location = new System.Drawing.Point(294, 84);
            this.hide_defenders_cb.Name = "hide_defenders_cb";
            this.hide_defenders_cb.Size = new System.Drawing.Size(133, 20);
            this.hide_defenders_cb.TabIndex = 17;
            this.hide_defenders_cb.Text = "Hide Defenders";
            this.hide_defenders_cb.UseVisualStyleBackColor = true;
            // 
            // auto_remove_in_town_cb
            // 
            this.auto_remove_in_town_cb.AutoSize = true;
            this.auto_remove_in_town_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.auto_remove_in_town_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auto_remove_in_town_cb.Location = new System.Drawing.Point(114, 84);
            this.auto_remove_in_town_cb.Name = "auto_remove_in_town_cb";
            this.auto_remove_in_town_cb.Size = new System.Drawing.Size(174, 20);
            this.auto_remove_in_town_cb.TabIndex = 16;
            this.auto_remove_in_town_cb.Text = "Auto Remove in Town";
            this.auto_remove_in_town_cb.UseVisualStyleBackColor = true;
            // 
            // default_behavior_cb
            // 
            this.default_behavior_cb.AutoSize = true;
            this.default_behavior_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.default_behavior_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.default_behavior_cb.Location = new System.Drawing.Point(114, 58);
            this.default_behavior_cb.Name = "default_behavior_cb";
            this.default_behavior_cb.Size = new System.Drawing.Size(139, 20);
            this.default_behavior_cb.TabIndex = 15;
            this.default_behavior_cb.Text = "Default Behavior";
            this.default_behavior_cb.UseVisualStyleBackColor = true;
            // 
            // always_visible_cb
            // 
            this.always_visible_cb.AutoSize = true;
            this.always_visible_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.always_visible_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.always_visible_cb.Location = new System.Drawing.Point(114, 32);
            this.always_visible_cb.Name = "always_visible_cb";
            this.always_visible_cb.Size = new System.Drawing.Size(125, 20);
            this.always_visible_cb.TabIndex = 14;
            this.always_visible_cb.Text = "Always Visible";
            this.always_visible_cb.UseVisualStyleBackColor = true;
            // 
            // is_static_cb
            // 
            this.is_static_cb.AutoSize = true;
            this.is_static_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.is_static_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.is_static_cb.Location = new System.Drawing.Point(21, 84);
            this.is_static_cb.Name = "is_static_cb";
            this.is_static_cb.Size = new System.Drawing.Size(63, 20);
            this.is_static_cb.TabIndex = 13;
            this.is_static_cb.Text = "Static";
            this.is_static_cb.UseVisualStyleBackColor = true;
            // 
            // is_ship_cb
            // 
            this.is_ship_cb.AutoSize = true;
            this.is_ship_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.is_ship_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.is_ship_cb.Location = new System.Drawing.Point(21, 58);
            this.is_ship_cb.Name = "is_ship_cb";
            this.is_ship_cb.Size = new System.Drawing.Size(55, 20);
            this.is_ship_cb.TabIndex = 12;
            this.is_ship_cb.Text = "Ship";
            this.is_ship_cb.UseVisualStyleBackColor = true;
            // 
            // disabled_cb
            // 
            this.disabled_cb.AutoSize = true;
            this.disabled_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.disabled_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disabled_cb.Location = new System.Drawing.Point(21, 32);
            this.disabled_cb.Name = "disabled_cb";
            this.disabled_cb.Size = new System.Drawing.Size(87, 20);
            this.disabled_cb.TabIndex = 11;
            this.disabled_cb.Text = "Disabled";
            this.disabled_cb.UseVisualStyleBackColor = true;
            // 
            // showGroup_2_btn
            // 
            this.showGroup_2_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_2_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_2_btn.Location = new System.Drawing.Point(3, 60);
            this.showGroup_2_btn.Name = "showGroup_2_btn";
            this.showGroup_2_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_2_btn.TabIndex = 26;
            this.showGroup_2_btn.Tag = "0";
            this.showGroup_2_btn.Text = "v";
            this.showGroup_2_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_2_gb
            // 
            this.groupBox_2_gb.Controls.Add(this.ai_target_p_cbb);
            this.groupBox_2_gb.Controls.Add(this.party_template_cbb);
            this.groupBox_2_gb.Controls.Add(this.menuID_cbb);
            this.groupBox_2_gb.Controls.Add(this.faction_cbb);
            this.groupBox_2_gb.Controls.Add(this.ai_bhvr_cbb);
            this.groupBox_2_gb.Controls.Add(this.ai_target_p_lbl);
            this.groupBox_2_gb.Controls.Add(this.ai_bhvr_lbl);
            this.groupBox_2_gb.Controls.Add(this.faction_lbl);
            this.groupBox_2_gb.Controls.Add(this.party_template_lbl);
            this.groupBox_2_gb.Controls.Add(this.menuID_lbl);
            this.groupBox_2_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_2_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_2_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_2_gb.Location = new System.Drawing.Point(39, 58);
            this.groupBox_2_gb.Name = "groupBox_2_gb";
            this.groupBox_2_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_2_gb.TabIndex = 0;
            this.groupBox_2_gb.TabStop = false;
            this.groupBox_2_gb.Tag = "";
            this.groupBox_2_gb.Text = "MenuID - PartyTemplateID - FactionID - AI Behavior + Target";
            // 
            // ai_target_p_cbb
            // 
            this.ai_target_p_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ai_target_p_cbb.FormattingEnabled = true;
            this.ai_target_p_cbb.Location = new System.Drawing.Point(460, 60);
            this.ai_target_p_cbb.Name = "ai_target_p_cbb";
            this.ai_target_p_cbb.Size = new System.Drawing.Size(192, 24);
            this.ai_target_p_cbb.TabIndex = 55;
            this.ai_target_p_cbb.Text = "hold";
            // 
            // party_template_cbb
            // 
            this.party_template_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.party_template_cbb.FormattingEnabled = true;
            this.party_template_cbb.Location = new System.Drawing.Point(139, 60);
            this.party_template_cbb.Name = "party_template_cbb";
            this.party_template_cbb.Size = new System.Drawing.Size(192, 24);
            this.party_template_cbb.TabIndex = 54;
            this.party_template_cbb.Text = "hold";
            // 
            // menuID_cbb
            // 
            this.menuID_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuID_cbb.FormattingEnabled = true;
            this.menuID_cbb.Items.AddRange(new object[] {
            "None"});
            this.menuID_cbb.Location = new System.Drawing.Point(139, 34);
            this.menuID_cbb.Name = "menuID_cbb";
            this.menuID_cbb.Size = new System.Drawing.Size(192, 24);
            this.menuID_cbb.TabIndex = 53;
            this.menuID_cbb.Text = "hold";
            // 
            // faction_cbb
            // 
            this.faction_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faction_cbb.FormattingEnabled = true;
            this.faction_cbb.Location = new System.Drawing.Point(139, 86);
            this.faction_cbb.Name = "faction_cbb";
            this.faction_cbb.Size = new System.Drawing.Size(192, 24);
            this.faction_cbb.TabIndex = 52;
            this.faction_cbb.Text = "hold";
            // 
            // ai_bhvr_cbb
            // 
            this.ai_bhvr_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ai_bhvr_cbb.FormattingEnabled = true;
            this.ai_bhvr_cbb.Items.AddRange(new object[] {
            "hold",
            "travel_to_party",
            "patrol_location",
            "patrol_party",
            "attack_party",
            "avoid_party",
            "travel_to_point",
            "negotiate_party",
            "in_town",
            "travel_to_ship",
            "escort_party",
            "driven_by_party"});
            this.ai_bhvr_cbb.Location = new System.Drawing.Point(460, 34);
            this.ai_bhvr_cbb.Name = "ai_bhvr_cbb";
            this.ai_bhvr_cbb.Size = new System.Drawing.Size(192, 24);
            this.ai_bhvr_cbb.TabIndex = 51;
            // 
            // ai_target_p_lbl
            // 
            this.ai_target_p_lbl.AutoSize = true;
            this.ai_target_p_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ai_target_p_lbl.Location = new System.Drawing.Point(337, 63);
            this.ai_target_p_lbl.Name = "ai_target_p_lbl";
            this.ai_target_p_lbl.Size = new System.Drawing.Size(117, 16);
            this.ai_target_p_lbl.TabIndex = 50;
            this.ai_target_p_lbl.Text = "AI-Target Party:";
            // 
            // ai_bhvr_lbl
            // 
            this.ai_bhvr_lbl.AutoSize = true;
            this.ai_bhvr_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ai_bhvr_lbl.Location = new System.Drawing.Point(362, 37);
            this.ai_bhvr_lbl.Name = "ai_bhvr_lbl";
            this.ai_bhvr_lbl.Size = new System.Drawing.Size(93, 16);
            this.ai_bhvr_lbl.TabIndex = 48;
            this.ai_bhvr_lbl.Text = "AI-Behavior:";
            // 
            // faction_lbl
            // 
            this.faction_lbl.AutoSize = true;
            this.faction_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.faction_lbl.Location = new System.Drawing.Point(70, 89);
            this.faction_lbl.Name = "faction_lbl";
            this.faction_lbl.Size = new System.Drawing.Size(63, 16);
            this.faction_lbl.TabIndex = 42;
            this.faction_lbl.Text = "Faction:";
            // 
            // party_template_lbl
            // 
            this.party_template_lbl.AutoSize = true;
            this.party_template_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.party_template_lbl.Location = new System.Drawing.Point(15, 63);
            this.party_template_lbl.Name = "party_template_lbl";
            this.party_template_lbl.Size = new System.Drawing.Size(118, 16);
            this.party_template_lbl.TabIndex = 40;
            this.party_template_lbl.Text = "Party Template:";
            // 
            // menuID_lbl
            // 
            this.menuID_lbl.AutoSize = true;
            this.menuID_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuID_lbl.Location = new System.Drawing.Point(84, 37);
            this.menuID_lbl.Name = "menuID_lbl";
            this.menuID_lbl.Size = new System.Drawing.Size(49, 16);
            this.menuID_lbl.TabIndex = 38;
            this.menuID_lbl.Text = "Menu:";
            // 
            // showGroup_3_btn
            // 
            this.showGroup_3_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_3_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_3_btn.Location = new System.Drawing.Point(3, 88);
            this.showGroup_3_btn.Name = "showGroup_3_btn";
            this.showGroup_3_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_3_btn.TabIndex = 28;
            this.showGroup_3_btn.Tag = "25";
            this.showGroup_3_btn.Text = "v";
            this.showGroup_3_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_3_gb
            // 
            this.groupBox_3_gb.Controls.Add(this.label1);
            this.groupBox_3_gb.Controls.Add(this.direction_in_degrees_txt);
            this.groupBox_3_gb.Controls.Add(this.direction_in_degrees_lbl);
            this.groupBox_3_gb.Controls.Add(this.initial_coordinates_gb);
            this.groupBox_3_gb.Controls.Add(this.personality_gb);
            this.groupBox_3_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_3_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_3_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_3_gb.Location = new System.Drawing.Point(39, 86);
            this.groupBox_3_gb.Name = "groupBox_3_gb";
            this.groupBox_3_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_3_gb.TabIndex = 27;
            this.groupBox_3_gb.TabStop = false;
            this.groupBox_3_gb.Tag = "";
            this.groupBox_3_gb.Text = "Personality - Initial Coordinates - Direction in Degrees";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(712, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Hint: soldier = [a=8,c=9]; merchant [a=0,c=7]; escorted_merchant = [a=0,c=11]; ba" +
    "ndit = [a=3,c=8,b=true]";
            this.label1.Visible = false;
            // 
            // direction_in_degrees_txt
            // 
            this.direction_in_degrees_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.direction_in_degrees_txt.Location = new System.Drawing.Point(623, 65);
            this.direction_in_degrees_txt.Name = "direction_in_degrees_txt";
            this.direction_in_degrees_txt.Size = new System.Drawing.Size(100, 22);
            this.direction_in_degrees_txt.TabIndex = 50;
            this.direction_in_degrees_txt.Text = "0";
            // 
            // direction_in_degrees_lbl
            // 
            this.direction_in_degrees_lbl.AutoSize = true;
            this.direction_in_degrees_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.direction_in_degrees_lbl.Location = new System.Drawing.Point(543, 68);
            this.direction_in_degrees_lbl.Name = "direction_in_degrees_lbl";
            this.direction_in_degrees_lbl.Size = new System.Drawing.Size(74, 16);
            this.direction_in_degrees_lbl.TabIndex = 49;
            this.direction_in_degrees_lbl.Text = "Direction:";
            // 
            // initial_coordinates_gb
            // 
            this.initial_coordinates_gb.Controls.Add(this.y_axis_txt);
            this.initial_coordinates_gb.Controls.Add(this.x_axis_txt);
            this.initial_coordinates_gb.Controls.Add(this.y_axis_lbl);
            this.initial_coordinates_gb.Controls.Add(this.x_axis_lbl);
            this.initial_coordinates_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.initial_coordinates_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initial_coordinates_gb.ForeColor = System.Drawing.Color.White;
            this.initial_coordinates_gb.Location = new System.Drawing.Point(315, 35);
            this.initial_coordinates_gb.Name = "initial_coordinates_gb";
            this.initial_coordinates_gb.Size = new System.Drawing.Size(216, 79);
            this.initial_coordinates_gb.TabIndex = 45;
            this.initial_coordinates_gb.TabStop = false;
            this.initial_coordinates_gb.Text = "Initial Coordinates";
            // 
            // y_axis_txt
            // 
            this.y_axis_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.y_axis_txt.Location = new System.Drawing.Point(76, 50);
            this.y_axis_txt.Name = "y_axis_txt";
            this.y_axis_txt.Size = new System.Drawing.Size(130, 22);
            this.y_axis_txt.TabIndex = 49;
            this.y_axis_txt.Text = "0";
            // 
            // x_axis_txt
            // 
            this.x_axis_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x_axis_txt.Location = new System.Drawing.Point(76, 23);
            this.x_axis_txt.Name = "x_axis_txt";
            this.x_axis_txt.Size = new System.Drawing.Size(130, 22);
            this.x_axis_txt.TabIndex = 48;
            this.x_axis_txt.Text = "0";
            // 
            // y_axis_lbl
            // 
            this.y_axis_lbl.AutoSize = true;
            this.y_axis_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.y_axis_lbl.Location = new System.Drawing.Point(14, 53);
            this.y_axis_lbl.Name = "y_axis_lbl";
            this.y_axis_lbl.Size = new System.Drawing.Size(56, 16);
            this.y_axis_lbl.TabIndex = 47;
            this.y_axis_lbl.Text = "Y-Axis:";
            // 
            // x_axis_lbl
            // 
            this.x_axis_lbl.AutoSize = true;
            this.x_axis_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x_axis_lbl.Location = new System.Drawing.Point(14, 26);
            this.x_axis_lbl.Name = "x_axis_lbl";
            this.x_axis_lbl.Size = new System.Drawing.Size(55, 16);
            this.x_axis_lbl.TabIndex = 46;
            this.x_axis_lbl.Text = "X-Axis:";
            // 
            // personality_gb
            // 
            this.personality_gb.Controls.Add(this.banditness_cb);
            this.personality_gb.Controls.Add(this.aggressiveness_lbl);
            this.personality_gb.Controls.Add(this.aggressiveness_num);
            this.personality_gb.Controls.Add(this.courage_lbl);
            this.personality_gb.Controls.Add(this.courage_num);
            this.personality_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.personality_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personality_gb.ForeColor = System.Drawing.Color.White;
            this.personality_gb.Location = new System.Drawing.Point(9, 34);
            this.personality_gb.Name = "personality_gb";
            this.personality_gb.Size = new System.Drawing.Size(291, 80);
            this.personality_gb.TabIndex = 44;
            this.personality_gb.TabStop = false;
            this.personality_gb.Text = "Personality";
            // 
            // banditness_cb
            // 
            this.banditness_cb.AutoSize = true;
            this.banditness_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.banditness_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banditness_cb.Location = new System.Drawing.Point(211, 32);
            this.banditness_cb.Name = "banditness_cb";
            this.banditness_cb.Size = new System.Drawing.Size(68, 20);
            this.banditness_cb.TabIndex = 48;
            this.banditness_cb.Text = "Bandit";
            this.banditness_cb.UseVisualStyleBackColor = true;
            // 
            // aggressiveness_lbl
            // 
            this.aggressiveness_lbl.AutoSize = true;
            this.aggressiveness_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aggressiveness_lbl.Location = new System.Drawing.Point(16, 50);
            this.aggressiveness_lbl.Name = "aggressiveness_lbl";
            this.aggressiveness_lbl.Size = new System.Drawing.Size(124, 16);
            this.aggressiveness_lbl.TabIndex = 47;
            this.aggressiveness_lbl.Text = "Aggressiveness:";
            // 
            // aggressiveness_num
            // 
            this.aggressiveness_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aggressiveness_num.Location = new System.Drawing.Point(146, 48);
            this.aggressiveness_num.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.aggressiveness_num.Name = "aggressiveness_num";
            this.aggressiveness_num.Size = new System.Drawing.Size(43, 22);
            this.aggressiveness_num.TabIndex = 46;
            // 
            // courage_lbl
            // 
            this.courage_lbl.AutoSize = true;
            this.courage_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.courage_lbl.Location = new System.Drawing.Point(69, 22);
            this.courage_lbl.Name = "courage_lbl";
            this.courage_lbl.Size = new System.Drawing.Size(71, 16);
            this.courage_lbl.TabIndex = 45;
            this.courage_lbl.Text = "Courage:";
            // 
            // courage_num
            // 
            this.courage_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.courage_num.Location = new System.Drawing.Point(146, 20);
            this.courage_num.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.courage_num.Name = "courage_num";
            this.courage_num.Size = new System.Drawing.Size(43, 22);
            this.courage_num.TabIndex = 44;
            // 
            // showGroup_4_btn
            // 
            this.showGroup_4_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_4_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_4_btn.Location = new System.Drawing.Point(3, 116);
            this.showGroup_4_btn.Name = "showGroup_4_btn";
            this.showGroup_4_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_4_btn.TabIndex = 30;
            this.showGroup_4_btn.Tag = "25";
            this.showGroup_4_btn.Text = "v";
            this.showGroup_4_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_4_gb
            // 
            this.groupBox_4_gb.Controls.Add(this.stackDownTroop_btn);
            this.groupBox_4_gb.Controls.Add(this.stackUpTroop_btn);
            this.groupBox_4_gb.Controls.Add(this.stackRemoveTroop_btn);
            this.groupBox_4_gb.Controls.Add(this.stackAddTroop_btn);
            this.groupBox_4_gb.Controls.Add(this.troops_lbl);
            this.groupBox_4_gb.Controls.Add(this.troops_lb);
            this.groupBox_4_gb.Controls.Add(this.stackTroopCount_txt);
            this.groupBox_4_gb.Controls.Add(this.stackTroopCount_lbl);
            this.groupBox_4_gb.Controls.Add(this.stack_troop_lbl);
            this.groupBox_4_gb.Controls.Add(this.stack_troops_lb);
            this.groupBox_4_gb.Controls.Add(this.is_prisoner_cb);
            this.groupBox_4_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_4_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_4_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_4_gb.Location = new System.Drawing.Point(39, 114);
            this.groupBox_4_gb.Name = "groupBox_4_gb";
            this.groupBox_4_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_4_gb.TabIndex = 29;
            this.groupBox_4_gb.TabStop = false;
            this.groupBox_4_gb.Tag = "";
            this.groupBox_4_gb.Text = "List of Stacks";
            // 
            // stackDownTroop_btn
            // 
            this.stackDownTroop_btn.BackColor = System.Drawing.Color.DimGray;
            this.stackDownTroop_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stackDownTroop_btn.Location = new System.Drawing.Point(289, 109);
            this.stackDownTroop_btn.Name = "stackDownTroop_btn";
            this.stackDownTroop_btn.Size = new System.Drawing.Size(82, 29);
            this.stackDownTroop_btn.TabIndex = 56;
            this.stackDownTroop_btn.Text = "DOWN";
            this.stackDownTroop_btn.UseVisualStyleBackColor = false;
            this.stackDownTroop_btn.Click += new System.EventHandler(this.StackDownTroop_btn_Click);
            // 
            // stackUpTroop_btn
            // 
            this.stackUpTroop_btn.BackColor = System.Drawing.Color.DimGray;
            this.stackUpTroop_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stackUpTroop_btn.Location = new System.Drawing.Point(289, 78);
            this.stackUpTroop_btn.Name = "stackUpTroop_btn";
            this.stackUpTroop_btn.Size = new System.Drawing.Size(82, 29);
            this.stackUpTroop_btn.TabIndex = 55;
            this.stackUpTroop_btn.Text = "UP";
            this.stackUpTroop_btn.UseVisualStyleBackColor = false;
            this.stackUpTroop_btn.Click += new System.EventHandler(this.StackUpTroop_btn_Click);
            // 
            // stackRemoveTroop_btn
            // 
            this.stackRemoveTroop_btn.BackColor = System.Drawing.Color.DimGray;
            this.stackRemoveTroop_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stackRemoveTroop_btn.Location = new System.Drawing.Point(289, 47);
            this.stackRemoveTroop_btn.Name = "stackRemoveTroop_btn";
            this.stackRemoveTroop_btn.Size = new System.Drawing.Size(82, 29);
            this.stackRemoveTroop_btn.TabIndex = 54;
            this.stackRemoveTroop_btn.Text = "Remove";
            this.stackRemoveTroop_btn.UseVisualStyleBackColor = false;
            this.stackRemoveTroop_btn.Click += new System.EventHandler(this.StackRemoveTroop_btn_Click);
            // 
            // stackAddTroop_btn
            // 
            this.stackAddTroop_btn.BackColor = System.Drawing.Color.DimGray;
            this.stackAddTroop_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stackAddTroop_btn.Location = new System.Drawing.Point(656, 47);
            this.stackAddTroop_btn.Name = "stackAddTroop_btn";
            this.stackAddTroop_btn.Size = new System.Drawing.Size(60, 91);
            this.stackAddTroop_btn.TabIndex = 53;
            this.stackAddTroop_btn.Text = "ADD";
            this.stackAddTroop_btn.UseVisualStyleBackColor = false;
            this.stackAddTroop_btn.Click += new System.EventHandler(this.StackAddTroop_btn_Click);
            // 
            // troops_lbl
            // 
            this.troops_lbl.AutoSize = true;
            this.troops_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.troops_lbl.Location = new System.Drawing.Point(387, 28);
            this.troops_lbl.Name = "troops_lbl";
            this.troops_lbl.Size = new System.Drawing.Size(62, 16);
            this.troops_lbl.TabIndex = 52;
            this.troops_lbl.Text = "Troops:";
            // 
            // troops_lb
            // 
            this.troops_lb.BackColor = System.Drawing.Color.DimGray;
            this.troops_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.troops_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.troops_lb.ForeColor = System.Drawing.Color.White;
            this.troops_lb.FormattingEnabled = true;
            this.troops_lb.ItemHeight = 18;
            this.troops_lb.Location = new System.Drawing.Point(390, 47);
            this.troops_lb.Name = "troops_lb";
            this.troops_lb.Size = new System.Drawing.Size(264, 92);
            this.troops_lb.TabIndex = 51;
            // 
            // stackTroopCount_txt
            // 
            this.stackTroopCount_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackTroopCount_txt.Location = new System.Drawing.Point(70, 109);
            this.stackTroopCount_txt.Name = "stackTroopCount_txt";
            this.stackTroopCount_txt.Size = new System.Drawing.Size(79, 22);
            this.stackTroopCount_txt.TabIndex = 50;
            this.stackTroopCount_txt.Text = "0";
            this.stackTroopCount_txt.TextChanged += new System.EventHandler(this.StackTroopCount_txt_TextChanged);
            // 
            // stackTroopCount_lbl
            // 
            this.stackTroopCount_lbl.AutoSize = true;
            this.stackTroopCount_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stackTroopCount_lbl.Location = new System.Drawing.Point(17, 112);
            this.stackTroopCount_lbl.Name = "stackTroopCount_lbl";
            this.stackTroopCount_lbl.Size = new System.Drawing.Size(51, 16);
            this.stackTroopCount_lbl.TabIndex = 49;
            this.stackTroopCount_lbl.Text = "Count:";
            // 
            // stack_troop_lbl
            // 
            this.stack_troop_lbl.AutoSize = true;
            this.stack_troop_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stack_troop_lbl.Location = new System.Drawing.Point(6, 28);
            this.stack_troop_lbl.Name = "stack_troop_lbl";
            this.stack_troop_lbl.Size = new System.Drawing.Size(101, 16);
            this.stack_troop_lbl.TabIndex = 40;
            this.stack_troop_lbl.Text = "Stack Troops";
            // 
            // stack_troops_lb
            // 
            this.stack_troops_lb.BackColor = System.Drawing.Color.DimGray;
            this.stack_troops_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stack_troops_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stack_troops_lb.ForeColor = System.Drawing.Color.White;
            this.stack_troops_lb.FormattingEnabled = true;
            this.stack_troops_lb.ItemHeight = 18;
            this.stack_troops_lb.Location = new System.Drawing.Point(9, 47);
            this.stack_troops_lb.Name = "stack_troops_lb";
            this.stack_troops_lb.Size = new System.Drawing.Size(279, 56);
            this.stack_troops_lb.TabIndex = 39;
            this.stack_troops_lb.SelectedIndexChanged += new System.EventHandler(this.Stack_troops_lb_SelectedIndexChanged);
            // 
            // is_prisoner_cb
            // 
            this.is_prisoner_cb.AutoSize = true;
            this.is_prisoner_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.is_prisoner_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.is_prisoner_cb.Location = new System.Drawing.Point(176, 110);
            this.is_prisoner_cb.Name = "is_prisoner_cb";
            this.is_prisoner_cb.Size = new System.Drawing.Size(82, 20);
            this.is_prisoner_cb.TabIndex = 15;
            this.is_prisoner_cb.Text = "Prisoner";
            this.is_prisoner_cb.UseVisualStyleBackColor = true;
            this.is_prisoner_cb.CheckedChanged += new System.EventHandler(this.Is_prisoner_cb_CheckedChanged);
            // 
            // PartyManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 332);
            this.Name = "PartyManager";
            this.Opacity = 1D;
            this.Text = "PartyManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.groupBox_1_gb.ResumeLayout(false);
            this.groupBox_1_gb.PerformLayout();
            this.carries_gb.ResumeLayout(false);
            this.carries_gb.PerformLayout();
            this.label_gb.ResumeLayout(false);
            this.label_gb.PerformLayout();
            this.set_gb.ResumeLayout(false);
            this.set_gb.PerformLayout();
            this.groupBox_2_gb.ResumeLayout(false);
            this.groupBox_2_gb.PerformLayout();
            this.groupBox_3_gb.ResumeLayout(false);
            this.groupBox_3_gb.PerformLayout();
            this.initial_coordinates_gb.ResumeLayout(false);
            this.initial_coordinates_gb.PerformLayout();
            this.personality_gb.ResumeLayout(false);
            this.personality_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aggressiveness_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.courage_num)).EndInit();
            this.groupBox_4_gb.ResumeLayout(false);
            this.groupBox_4_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGroup_1_btn;
        private System.Windows.Forms.GroupBox groupBox_1_gb;
        private System.Windows.Forms.CheckBox disabled_cb;
        private System.Windows.Forms.CheckBox auto_remove_in_town_cb;
        private System.Windows.Forms.CheckBox default_behavior_cb;
        private System.Windows.Forms.CheckBox always_visible_cb;
        private System.Windows.Forms.CheckBox is_static_cb;
        private System.Windows.Forms.CheckBox is_ship_cb;
        private System.Windows.Forms.GroupBox label_gb;
        private System.Windows.Forms.RadioButton small_label_rb;
        private System.Windows.Forms.RadioButton medium_label_rb;
        private System.Windows.Forms.RadioButton large_label_rb;
        private System.Windows.Forms.GroupBox set_gb;
        private System.Windows.Forms.CheckBox show_faction_cb;
        private System.Windows.Forms.CheckBox dont_attack_civilians_cb;
        private System.Windows.Forms.CheckBox civilian_cb;
        private System.Windows.Forms.CheckBox limit_members_cb;
        private System.Windows.Forms.CheckBox hide_defenders_cb;
        private System.Windows.Forms.Label map_icon_lbl;
        private System.Windows.Forms.CheckBox quest_party_cb;
        private System.Windows.Forms.Label carries_gold_lbl;
        private System.Windows.Forms.Label carries_goods_lbl;
        private System.Windows.Forms.TextBox carries_gold_txt;
        private System.Windows.Forms.RadioButton village_rb;
        private System.Windows.Forms.RadioButton castle_rb;
        private System.Windows.Forms.RadioButton town_rb;
        private System.Windows.Forms.RadioButton no_set_rb;
        private System.Windows.Forms.RadioButton no_label_rb;
        private System.Windows.Forms.GroupBox carries_gb;
        private System.Windows.Forms.Button showGroup_2_btn;
        private System.Windows.Forms.GroupBox groupBox_2_gb;
        private System.Windows.Forms.Label menuID_lbl;
        private System.Windows.Forms.Label party_template_lbl;
        private System.Windows.Forms.Label faction_lbl;
        private System.Windows.Forms.Label ai_target_p_lbl;
        private System.Windows.Forms.Label ai_bhvr_lbl;
        private System.Windows.Forms.Button showGroup_3_btn;
        private System.Windows.Forms.GroupBox groupBox_3_gb;
        private System.Windows.Forms.GroupBox personality_gb;
        private System.Windows.Forms.Label aggressiveness_lbl;
        private System.Windows.Forms.NumericUpDown aggressiveness_num;
        private System.Windows.Forms.Label courage_lbl;
        private System.Windows.Forms.NumericUpDown courage_num;
        private System.Windows.Forms.GroupBox initial_coordinates_gb;
        private System.Windows.Forms.TextBox x_axis_txt;
        private System.Windows.Forms.Label y_axis_lbl;
        private System.Windows.Forms.Label x_axis_lbl;
        private System.Windows.Forms.TextBox y_axis_txt;
        private System.Windows.Forms.TextBox direction_in_degrees_txt;
        private System.Windows.Forms.Label direction_in_degrees_lbl;
        private System.Windows.Forms.Button showGroup_4_btn;
        private System.Windows.Forms.GroupBox groupBox_4_gb;
        private System.Windows.Forms.CheckBox is_prisoner_cb;
        private System.Windows.Forms.TextBox stackTroopCount_txt;
        private System.Windows.Forms.Label stackTroopCount_lbl;
        private System.Windows.Forms.Label stack_troop_lbl;
        private System.Windows.Forms.ListBox stack_troops_lb;
        private System.Windows.Forms.Label troops_lbl;
        private System.Windows.Forms.ListBox troops_lb;
        protected System.Windows.Forms.Button stackDownTroop_btn;
        protected System.Windows.Forms.Button stackUpTroop_btn;
        protected System.Windows.Forms.Button stackRemoveTroop_btn;
        protected System.Windows.Forms.Button stackAddTroop_btn;
        private System.Windows.Forms.CheckBox banditness_cb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox carries_goods_txt;
        private System.Windows.Forms.ComboBox map_icon_cbb;
        private System.Windows.Forms.ComboBox ai_bhvr_cbb;
        private System.Windows.Forms.ComboBox faction_cbb;
        private System.Windows.Forms.ComboBox menuID_cbb;
        private System.Windows.Forms.ComboBox ai_target_p_cbb;
        private System.Windows.Forms.ComboBox party_template_cbb;
    }
}
