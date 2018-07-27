using importantLib;

namespace MB_Studio.Main
{
    partial class ProjectProperties
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Project Folder & Module");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("#Set1");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("#Set2");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("#Set3");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("#Set4");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("#Set5");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("#Set6");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Itemsets", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("TroopManager", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.min_btn = new System.Windows.Forms.Button();
            this.maxnorm_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.extraOptions_tree = new System.Windows.Forms.TreeView();
            this.itemsets_panel = new System.Windows.Forms.Panel();
            this.searchItems_AllItems_SearchTextBox = new importantLib.SearchTextBox();
            this.add_btn = new System.Windows.Forms.Button();
            this.items_lb = new System.Windows.Forms.ListBox();
            this.setItemFlags_btn = new System.Windows.Forms.Button();
            this.selectedItemFlags_lbl = new System.Windows.Forms.Label();
            this.selectedItemFlags_txt = new System.Windows.Forms.TextBox();
            this.usedItemREMOVE_btn = new System.Windows.Forms.Button();
            this.usedItemDOWN_btn = new System.Windows.Forms.Button();
            this.usedItemUP_btn = new System.Windows.Forms.Button();
            this.setName_lbl = new System.Windows.Forms.Label();
            this.setName_txt = new System.Windows.Forms.TextBox();
            this.valueInfo_lbl = new System.Windows.Forms.Label();
            this.search_btn = new System.Windows.Forms.Button();
            this.abort_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.searchItems_SearchTextBox = new importantLib.SearchTextBox();
            this.usedItems_lb = new System.Windows.Forms.ListBox();
            this.swingDamage_lbl = new System.Windows.Forms.Label();
            this.swingDamage_txt = new System.Windows.Forms.TextBox();
            this.thrustDamage_lbl = new System.Windows.Forms.Label();
            this.thrustDamage_txt = new System.Windows.Forms.TextBox();
            this.legArmor_lbl = new System.Windows.Forms.Label();
            this.legArmor_txt = new System.Windows.Forms.TextBox();
            this.bodyArmor_lbl = new System.Windows.Forms.Label();
            this.bodyArmor_txt = new System.Windows.Forms.TextBox();
            this.headArmor_lbl = new System.Windows.Forms.Label();
            this.headArmor_txt = new System.Windows.Forms.TextBox();
            this.projects_panel = new System.Windows.Forms.Panel();
            this.projectFolder_btn = new System.Windows.Forms.Button();
            this.projectFolder_txt = new System.Windows.Forms.TextBox();
            this.projectFolder_lbl = new System.Windows.Forms.Label();
            this.saveProjectFolder_btn = new System.Windows.Forms.Button();
            this.copyDefaultVariables_cb = new System.Windows.Forms.CheckBox();
            this.selectDestinationMod_btn = new System.Windows.Forms.Button();
            this.destinationMod_cbb = new System.Windows.Forms.ComboBox();
            this.destinationMod_lbl = new System.Windows.Forms.Label();
            this.save_DestinationMod_btn = new System.Windows.Forms.Button();
            this.selectOriginalMod_btn = new System.Windows.Forms.Button();
            this.originalMod_cbb = new System.Windows.Forms.ComboBox();
            this.originalMod_lbl = new System.Windows.Forms.Label();
            this.save_OriginalMod_btn = new System.Windows.Forms.Button();
            this.itemsets_panel.SuspendLayout();
            this.projects_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.min_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.min_btn.ForeColor = System.Drawing.Color.White;
            this.min_btn.Location = new System.Drawing.Point(1092, 0);
            this.min_btn.Name = "min_btn";
            this.min_btn.Size = new System.Drawing.Size(32, 28);
            this.min_btn.TabIndex = 19;
            this.min_btn.Text = "_";
            this.min_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.min_btn.UseVisualStyleBackColor = false;
            this.min_btn.Click += new System.EventHandler(this.Min_btn_Click);
            // 
            // maxnorm_btn
            // 
            this.maxnorm_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxnorm_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.maxnorm_btn.Enabled = false;
            this.maxnorm_btn.FlatAppearance.BorderSize = 0;
            this.maxnorm_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.maxnorm_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.maxnorm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxnorm_btn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxnorm_btn.ForeColor = System.Drawing.Color.White;
            this.maxnorm_btn.Location = new System.Drawing.Point(1124, 1);
            this.maxnorm_btn.Name = "maxnorm_btn";
            this.maxnorm_btn.Size = new System.Drawing.Size(32, 26);
            this.maxnorm_btn.TabIndex = 18;
            this.maxnorm_btn.Text = "◼";
            this.maxnorm_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.maxnorm_btn.UseVisualStyleBackColor = false;
            // 
            // exit_btn
            // 
            this.exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_btn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.ForeColor = System.Drawing.Color.White;
            this.exit_btn.Location = new System.Drawing.Point(1156, 1);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(32, 26);
            this.exit_btn.TabIndex = 17;
            this.exit_btn.Text = "X";
            this.exit_btn.UseVisualStyleBackColor = false;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.title_lbl.Location = new System.Drawing.Point(0, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(1187, 34);
            this.title_lbl.TabIndex = 16;
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // extraOptions_tree
            // 
            this.extraOptions_tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.extraOptions_tree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.extraOptions_tree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.extraOptions_tree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extraOptions_tree.ForeColor = System.Drawing.Color.White;
            this.extraOptions_tree.LineColor = System.Drawing.Color.White;
            this.extraOptions_tree.Location = new System.Drawing.Point(8, 42);
            this.extraOptions_tree.Name = "extraOptions_tree";
            treeNode1.Name = "projectsFolder_node";
            treeNode1.Text = "Project Folder & Module";
            treeNode2.Name = "set_1";
            treeNode2.Text = "#Set1";
            treeNode3.Name = "set_2";
            treeNode3.Text = "#Set2";
            treeNode4.Name = "set_3";
            treeNode4.Text = "#Set3";
            treeNode5.Name = "set_4";
            treeNode5.Text = "#Set4";
            treeNode6.Name = "set_5";
            treeNode6.Text = "#Set5";
            treeNode7.Name = "set_6";
            treeNode7.Text = "#Set6";
            treeNode8.Name = "itemsets_node";
            treeNode8.Text = "Itemsets";
            treeNode9.Name = "troopManager_node";
            treeNode9.Text = "TroopManager";
            this.extraOptions_tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode9});
            this.extraOptions_tree.Size = new System.Drawing.Size(192, 258);
            this.extraOptions_tree.TabIndex = 20;
            this.extraOptions_tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ExtraOptions_tree_AfterSelect);
            // 
            // itemsets_panel
            // 
            this.itemsets_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsets_panel.Controls.Add(this.searchItems_AllItems_SearchTextBox);
            this.itemsets_panel.Controls.Add(this.add_btn);
            this.itemsets_panel.Controls.Add(this.items_lb);
            this.itemsets_panel.Controls.Add(this.setItemFlags_btn);
            this.itemsets_panel.Controls.Add(this.selectedItemFlags_lbl);
            this.itemsets_panel.Controls.Add(this.selectedItemFlags_txt);
            this.itemsets_panel.Controls.Add(this.usedItemREMOVE_btn);
            this.itemsets_panel.Controls.Add(this.usedItemDOWN_btn);
            this.itemsets_panel.Controls.Add(this.usedItemUP_btn);
            this.itemsets_panel.Controls.Add(this.setName_lbl);
            this.itemsets_panel.Controls.Add(this.setName_txt);
            this.itemsets_panel.Controls.Add(this.valueInfo_lbl);
            this.itemsets_panel.Controls.Add(this.search_btn);
            this.itemsets_panel.Controls.Add(this.abort_btn);
            this.itemsets_panel.Controls.Add(this.save_btn);
            this.itemsets_panel.Controls.Add(this.searchItems_SearchTextBox);
            this.itemsets_panel.Controls.Add(this.usedItems_lb);
            this.itemsets_panel.Controls.Add(this.swingDamage_lbl);
            this.itemsets_panel.Controls.Add(this.swingDamage_txt);
            this.itemsets_panel.Controls.Add(this.thrustDamage_lbl);
            this.itemsets_panel.Controls.Add(this.thrustDamage_txt);
            this.itemsets_panel.Controls.Add(this.legArmor_lbl);
            this.itemsets_panel.Controls.Add(this.legArmor_txt);
            this.itemsets_panel.Controls.Add(this.bodyArmor_lbl);
            this.itemsets_panel.Controls.Add(this.bodyArmor_txt);
            this.itemsets_panel.Controls.Add(this.headArmor_lbl);
            this.itemsets_panel.Controls.Add(this.headArmor_txt);
            this.itemsets_panel.Location = new System.Drawing.Point(204, 42);
            this.itemsets_panel.Name = "itemsets_panel";
            this.itemsets_panel.Size = new System.Drawing.Size(981, 258);
            this.itemsets_panel.TabIndex = 21;
            this.itemsets_panel.Visible = false;
            // 
            // searchItems_AllItems_SearchTextBox
            // 
            this.searchItems_AllItems_SearchTextBox.BackColor = System.Drawing.Color.DimGray;
            this.searchItems_AllItems_SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchItems_AllItems_SearchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItems_AllItems_SearchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchItems_AllItems_SearchTextBox.Location = new System.Drawing.Point(221, 20);
            this.searchItems_AllItems_SearchTextBox.Name = "searchItems_AllItems_SearchTextBox";
            this.searchItems_AllItems_SearchTextBox.Size = new System.Drawing.Size(287, 22);
            this.searchItems_AllItems_SearchTextBox.TabIndex = 36;
            this.searchItems_AllItems_SearchTextBox.Text = "Search ...";
            this.searchItems_AllItems_SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_AllItems_TextChanged);
            // 
            // add_btn
            // 
            this.add_btn.BackColor = System.Drawing.Color.Gray;
            this.add_btn.FlatAppearance.BorderSize = 0;
            this.add_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.add_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.add_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add_btn.ForeColor = System.Drawing.Color.White;
            this.add_btn.Location = new System.Drawing.Point(509, 43);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(80, 128);
            this.add_btn.TabIndex = 35;
            this.add_btn.TabStop = false;
            this.add_btn.Text = "ADD";
            this.add_btn.UseVisualStyleBackColor = false;
            this.add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // items_lb
            // 
            this.items_lb.BackColor = System.Drawing.Color.DimGray;
            this.items_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.items_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.items_lb.ForeColor = System.Drawing.Color.White;
            this.items_lb.FormattingEnabled = true;
            this.items_lb.ItemHeight = 18;
            this.items_lb.Location = new System.Drawing.Point(221, 43);
            this.items_lb.Name = "items_lb";
            this.items_lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.items_lb.Size = new System.Drawing.Size(287, 128);
            this.items_lb.TabIndex = 34;
            // 
            // setItemFlags_btn
            // 
            this.setItemFlags_btn.BackColor = System.Drawing.Color.Gray;
            this.setItemFlags_btn.FlatAppearance.BorderSize = 0;
            this.setItemFlags_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.setItemFlags_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.setItemFlags_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setItemFlags_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setItemFlags_btn.ForeColor = System.Drawing.Color.White;
            this.setItemFlags_btn.Location = new System.Drawing.Point(822, 172);
            this.setItemFlags_btn.Name = "setItemFlags_btn";
            this.setItemFlags_btn.Size = new System.Drawing.Size(65, 22);
            this.setItemFlags_btn.TabIndex = 33;
            this.setItemFlags_btn.TabStop = false;
            this.setItemFlags_btn.Text = "SET";
            this.setItemFlags_btn.UseVisualStyleBackColor = false;
            this.setItemFlags_btn.Click += new System.EventHandler(this.SetItemFlags_btn_Click);
            // 
            // selectedItemFlags_lbl
            // 
            this.selectedItemFlags_lbl.AutoSize = true;
            this.selectedItemFlags_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedItemFlags_lbl.ForeColor = System.Drawing.Color.White;
            this.selectedItemFlags_lbl.Location = new System.Drawing.Point(602, 174);
            this.selectedItemFlags_lbl.Name = "selectedItemFlags_lbl";
            this.selectedItemFlags_lbl.Size = new System.Drawing.Size(84, 16);
            this.selectedItemFlags_lbl.TabIndex = 32;
            this.selectedItemFlags_lbl.Text = "Item Flags:";
            // 
            // selectedItemFlags_txt
            // 
            this.selectedItemFlags_txt.BackColor = System.Drawing.Color.DimGray;
            this.selectedItemFlags_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedItemFlags_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedItemFlags_txt.ForeColor = System.Drawing.Color.White;
            this.selectedItemFlags_txt.Location = new System.Drawing.Point(690, 172);
            this.selectedItemFlags_txt.Name = "selectedItemFlags_txt";
            this.selectedItemFlags_txt.Size = new System.Drawing.Size(131, 22);
            this.selectedItemFlags_txt.TabIndex = 31;
            // 
            // usedItemREMOVE_btn
            // 
            this.usedItemREMOVE_btn.BackColor = System.Drawing.Color.Gray;
            this.usedItemREMOVE_btn.FlatAppearance.BorderSize = 0;
            this.usedItemREMOVE_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.usedItemREMOVE_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.usedItemREMOVE_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usedItemREMOVE_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedItemREMOVE_btn.ForeColor = System.Drawing.Color.White;
            this.usedItemREMOVE_btn.Location = new System.Drawing.Point(888, 129);
            this.usedItemREMOVE_btn.Name = "usedItemREMOVE_btn";
            this.usedItemREMOVE_btn.Size = new System.Drawing.Size(80, 42);
            this.usedItemREMOVE_btn.TabIndex = 30;
            this.usedItemREMOVE_btn.TabStop = false;
            this.usedItemREMOVE_btn.Text = "REMOVE";
            this.usedItemREMOVE_btn.UseVisualStyleBackColor = false;
            this.usedItemREMOVE_btn.Click += new System.EventHandler(this.UsedItemREMOVE_btn_Click);
            // 
            // usedItemDOWN_btn
            // 
            this.usedItemDOWN_btn.BackColor = System.Drawing.Color.Gray;
            this.usedItemDOWN_btn.FlatAppearance.BorderSize = 0;
            this.usedItemDOWN_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.usedItemDOWN_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.usedItemDOWN_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usedItemDOWN_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedItemDOWN_btn.ForeColor = System.Drawing.Color.White;
            this.usedItemDOWN_btn.Location = new System.Drawing.Point(888, 86);
            this.usedItemDOWN_btn.Name = "usedItemDOWN_btn";
            this.usedItemDOWN_btn.Size = new System.Drawing.Size(80, 42);
            this.usedItemDOWN_btn.TabIndex = 29;
            this.usedItemDOWN_btn.TabStop = false;
            this.usedItemDOWN_btn.Text = "DOWN";
            this.usedItemDOWN_btn.UseVisualStyleBackColor = false;
            this.usedItemDOWN_btn.Click += new System.EventHandler(this.UsedItemDOWN_btn_Click);
            // 
            // usedItemUP_btn
            // 
            this.usedItemUP_btn.BackColor = System.Drawing.Color.Gray;
            this.usedItemUP_btn.FlatAppearance.BorderSize = 0;
            this.usedItemUP_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.usedItemUP_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.usedItemUP_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.usedItemUP_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedItemUP_btn.ForeColor = System.Drawing.Color.White;
            this.usedItemUP_btn.Location = new System.Drawing.Point(888, 43);
            this.usedItemUP_btn.Name = "usedItemUP_btn";
            this.usedItemUP_btn.Size = new System.Drawing.Size(80, 42);
            this.usedItemUP_btn.TabIndex = 28;
            this.usedItemUP_btn.TabStop = false;
            this.usedItemUP_btn.Text = "UP";
            this.usedItemUP_btn.UseVisualStyleBackColor = false;
            this.usedItemUP_btn.Click += new System.EventHandler(this.UsedItemUP_btn_Click);
            // 
            // setName_lbl
            // 
            this.setName_lbl.AutoSize = true;
            this.setName_lbl.ForeColor = System.Drawing.Color.White;
            this.setName_lbl.Location = new System.Drawing.Point(74, 27);
            this.setName_lbl.Name = "setName_lbl";
            this.setName_lbl.Size = new System.Drawing.Size(54, 13);
            this.setName_lbl.TabIndex = 27;
            this.setName_lbl.Text = "Set Name";
            // 
            // setName_txt
            // 
            this.setName_txt.Location = new System.Drawing.Point(134, 24);
            this.setName_txt.Name = "setName_txt";
            this.setName_txt.Size = new System.Drawing.Size(69, 20);
            this.setName_txt.TabIndex = 26;
            // 
            // valueInfo_lbl
            // 
            this.valueInfo_lbl.AutoSize = true;
            this.valueInfo_lbl.ForeColor = System.Drawing.Color.White;
            this.valueInfo_lbl.Location = new System.Drawing.Point(124, 182);
            this.valueInfo_lbl.Name = "valueInfo_lbl";
            this.valueInfo_lbl.Size = new System.Drawing.Size(88, 13);
            this.valueInfo_lbl.TabIndex = 25;
            this.valueInfo_lbl.Text = "(e.g. 5 or 5-10)";
            // 
            // search_btn
            // 
            this.search_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.search_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_btn.ForeColor = System.Drawing.Color.White;
            this.search_btn.Location = new System.Drawing.Point(32, 216);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(309, 23);
            this.search_btn.TabIndex = 24;
            this.search_btn.Text = "Search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // abort_btn
            // 
            this.abort_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.abort_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.abort_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.abort_btn.ForeColor = System.Drawing.Color.White;
            this.abort_btn.Location = new System.Drawing.Point(662, 216);
            this.abort_btn.Name = "abort_btn";
            this.abort_btn.Size = new System.Drawing.Size(309, 23);
            this.abort_btn.TabIndex = 23;
            this.abort_btn.Text = "Abort";
            this.abort_btn.UseVisualStyleBackColor = true;
            this.abort_btn.Click += new System.EventHandler(this.Abort_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.save_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_btn.ForeColor = System.Drawing.Color.White;
            this.save_btn.Location = new System.Drawing.Point(347, 216);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(309, 23);
            this.save_btn.TabIndex = 22;
            this.save_btn.Text = "Save";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // searchItems_SearchTextBox
            // 
            this.searchItems_SearchTextBox.BackColor = System.Drawing.Color.DimGray;
            this.searchItems_SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchItems_SearchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItems_SearchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchItems_SearchTextBox.Location = new System.Drawing.Point(600, 20);
            this.searchItems_SearchTextBox.Name = "searchItems_SearchTextBox";
            this.searchItems_SearchTextBox.Size = new System.Drawing.Size(287, 22);
            this.searchItems_SearchTextBox.TabIndex = 21;
            this.searchItems_SearchTextBox.Text = "Search ...";
            this.searchItems_SearchTextBox.TextChanged += new System.EventHandler(this.SearchItems_SearchTextBox_TextChanged);
            // 
            // usedItems_lb
            // 
            this.usedItems_lb.BackColor = System.Drawing.Color.DimGray;
            this.usedItems_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usedItems_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usedItems_lb.ForeColor = System.Drawing.Color.White;
            this.usedItems_lb.FormattingEnabled = true;
            this.usedItems_lb.ItemHeight = 18;
            this.usedItems_lb.Location = new System.Drawing.Point(600, 43);
            this.usedItems_lb.Name = "usedItems_lb";
            this.usedItems_lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.usedItems_lb.Size = new System.Drawing.Size(287, 128);
            this.usedItems_lb.TabIndex = 10;
            this.usedItems_lb.SelectedIndexChanged += new System.EventHandler(this.UsedItems_lb_SelectedIndexChanged);
            // 
            // swingDamage_lbl
            // 
            this.swingDamage_lbl.AutoSize = true;
            this.swingDamage_lbl.ForeColor = System.Drawing.Color.White;
            this.swingDamage_lbl.Location = new System.Drawing.Point(39, 158);
            this.swingDamage_lbl.Name = "swingDamage_lbl";
            this.swingDamage_lbl.Size = new System.Drawing.Size(89, 13);
            this.swingDamage_lbl.TabIndex = 9;
            this.swingDamage_lbl.Text = "Swing Damage";
            // 
            // swingDamage_txt
            // 
            this.swingDamage_txt.Location = new System.Drawing.Point(134, 155);
            this.swingDamage_txt.Name = "swingDamage_txt";
            this.swingDamage_txt.Size = new System.Drawing.Size(69, 20);
            this.swingDamage_txt.TabIndex = 8;
            // 
            // thrustDamage_lbl
            // 
            this.thrustDamage_lbl.AutoSize = true;
            this.thrustDamage_lbl.ForeColor = System.Drawing.Color.White;
            this.thrustDamage_lbl.Location = new System.Drawing.Point(58, 132);
            this.thrustDamage_lbl.Name = "thrustDamage_lbl";
            this.thrustDamage_lbl.Size = new System.Drawing.Size(70, 13);
            this.thrustDamage_lbl.TabIndex = 7;
            this.thrustDamage_lbl.Text = "Thrust Damage";
            // 
            // thrustDamage_txt
            // 
            this.thrustDamage_txt.Location = new System.Drawing.Point(134, 129);
            this.thrustDamage_txt.Name = "thrustDamage_txt";
            this.thrustDamage_txt.Size = new System.Drawing.Size(69, 20);
            this.thrustDamage_txt.TabIndex = 6;
            // 
            // legArmor_lbl
            // 
            this.legArmor_lbl.AutoSize = true;
            this.legArmor_lbl.ForeColor = System.Drawing.Color.White;
            this.legArmor_lbl.Location = new System.Drawing.Point(68, 106);
            this.legArmor_lbl.Name = "legArmor_lbl";
            this.legArmor_lbl.Size = new System.Drawing.Size(59, 13);
            this.legArmor_lbl.TabIndex = 5;
            this.legArmor_lbl.Text = "Leg Armor";
            // 
            // legArmor_txt
            // 
            this.legArmor_txt.Location = new System.Drawing.Point(134, 103);
            this.legArmor_txt.Name = "legArmor_txt";
            this.legArmor_txt.Size = new System.Drawing.Size(69, 20);
            this.legArmor_txt.TabIndex = 4;
            // 
            // bodyArmor_lbl
            // 
            this.bodyArmor_lbl.AutoSize = true;
            this.bodyArmor_lbl.ForeColor = System.Drawing.Color.White;
            this.bodyArmor_lbl.Location = new System.Drawing.Point(59, 80);
            this.bodyArmor_lbl.Name = "bodyArmor_lbl";
            this.bodyArmor_lbl.Size = new System.Drawing.Size(69, 13);
            this.bodyArmor_lbl.TabIndex = 3;
            this.bodyArmor_lbl.Text = "Body Armor";
            // 
            // bodyArmor_txt
            // 
            this.bodyArmor_txt.Location = new System.Drawing.Point(134, 77);
            this.bodyArmor_txt.Name = "bodyArmor_txt";
            this.bodyArmor_txt.Size = new System.Drawing.Size(69, 20);
            this.bodyArmor_txt.TabIndex = 2;
            // 
            // headArmor_lbl
            // 
            this.headArmor_lbl.AutoSize = true;
            this.headArmor_lbl.ForeColor = System.Drawing.Color.White;
            this.headArmor_lbl.Location = new System.Drawing.Point(68, 54);
            this.headArmor_lbl.Name = "headArmor_lbl";
            this.headArmor_lbl.Size = new System.Drawing.Size(60, 13);
            this.headArmor_lbl.TabIndex = 1;
            this.headArmor_lbl.Text = "Head Armor";
            // 
            // headArmor_txt
            // 
            this.headArmor_txt.Location = new System.Drawing.Point(134, 51);
            this.headArmor_txt.Name = "headArmor_txt";
            this.headArmor_txt.Size = new System.Drawing.Size(69, 20);
            this.headArmor_txt.TabIndex = 0;
            // 
            // projects_panel
            // 
            this.projects_panel.Controls.Add(this.projectFolder_btn);
            this.projects_panel.Controls.Add(this.projectFolder_txt);
            this.projects_panel.Controls.Add(this.projectFolder_lbl);
            this.projects_panel.Controls.Add(this.saveProjectFolder_btn);
            this.projects_panel.Controls.Add(this.copyDefaultVariables_cb);
            this.projects_panel.Controls.Add(this.selectDestinationMod_btn);
            this.projects_panel.Controls.Add(this.destinationMod_cbb);
            this.projects_panel.Controls.Add(this.destinationMod_lbl);
            this.projects_panel.Controls.Add(this.save_DestinationMod_btn);
            this.projects_panel.Controls.Add(this.selectOriginalMod_btn);
            this.projects_panel.Controls.Add(this.originalMod_cbb);
            this.projects_panel.Controls.Add(this.originalMod_lbl);
            this.projects_panel.Controls.Add(this.save_OriginalMod_btn);
            this.projects_panel.Location = new System.Drawing.Point(207, 46);
            this.projects_panel.Name = "projects_panel";
            this.projects_panel.Size = new System.Drawing.Size(978, 251);
            this.projects_panel.TabIndex = 22;
            this.projects_panel.Visible = false;
            // 
            // projectFolder_btn
            // 
            this.projectFolder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.projectFolder_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.projectFolder_btn.FlatAppearance.BorderSize = 0;
            this.projectFolder_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.projectFolder_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.projectFolder_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.projectFolder_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectFolder_btn.ForeColor = System.Drawing.Color.White;
            this.projectFolder_btn.Location = new System.Drawing.Point(569, 75);
            this.projectFolder_btn.Name = "projectFolder_btn";
            this.projectFolder_btn.Size = new System.Drawing.Size(44, 22);
            this.projectFolder_btn.TabIndex = 44;
            this.projectFolder_btn.Text = "...";
            this.projectFolder_btn.UseVisualStyleBackColor = false;
            this.projectFolder_btn.Click += new System.EventHandler(this.ProjectFolder_OpenFolderDialog_btn_Click);
            // 
            // projectFolder_txt
            // 
            this.projectFolder_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectFolder_txt.Location = new System.Drawing.Point(323, 75);
            this.projectFolder_txt.Name = "projectFolder_txt";
            this.projectFolder_txt.Size = new System.Drawing.Size(246, 22);
            this.projectFolder_txt.TabIndex = 43;
            // 
            // projectFolder_lbl
            // 
            this.projectFolder_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectFolder_lbl.AutoSize = true;
            this.projectFolder_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.projectFolder_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectFolder_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.projectFolder_lbl.Location = new System.Drawing.Point(207, 78);
            this.projectFolder_lbl.Name = "projectFolder_lbl";
            this.projectFolder_lbl.Size = new System.Drawing.Size(110, 16);
            this.projectFolder_lbl.TabIndex = 42;
            this.projectFolder_lbl.Text = "Project Folder:";
            this.projectFolder_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveProjectFolder_btn
            // 
            this.saveProjectFolder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProjectFolder_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.saveProjectFolder_btn.FlatAppearance.BorderSize = 0;
            this.saveProjectFolder_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.saveProjectFolder_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.saveProjectFolder_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveProjectFolder_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveProjectFolder_btn.ForeColor = System.Drawing.Color.White;
            this.saveProjectFolder_btn.Location = new System.Drawing.Point(619, 75);
            this.saveProjectFolder_btn.Name = "saveProjectFolder_btn";
            this.saveProjectFolder_btn.Size = new System.Drawing.Size(44, 22);
            this.saveProjectFolder_btn.TabIndex = 41;
            this.saveProjectFolder_btn.Text = "Save";
            this.saveProjectFolder_btn.UseVisualStyleBackColor = false;
            this.saveProjectFolder_btn.Click += new System.EventHandler(this.SaveProjectFolder_btn_Click);
            // 
            // copyDefaultVariables_cb
            // 
            this.copyDefaultVariables_cb.AutoSize = true;
            this.copyDefaultVariables_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyDefaultVariables_cb.ForeColor = System.Drawing.Color.White;
            this.copyDefaultVariables_cb.Location = new System.Drawing.Point(323, 198);
            this.copyDefaultVariables_cb.Name = "copyDefaultVariables_cb";
            this.copyDefaultVariables_cb.Size = new System.Drawing.Size(179, 20);
            this.copyDefaultVariables_cb.TabIndex = 40;
            this.copyDefaultVariables_cb.Text = "CopyDefaultVariables";
            this.copyDefaultVariables_cb.UseVisualStyleBackColor = true;
            // 
            // selectDestinationMod_btn
            // 
            this.selectDestinationMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectDestinationMod_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.selectDestinationMod_btn.FlatAppearance.BorderSize = 0;
            this.selectDestinationMod_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.selectDestinationMod_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.selectDestinationMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectDestinationMod_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectDestinationMod_btn.ForeColor = System.Drawing.Color.White;
            this.selectDestinationMod_btn.Location = new System.Drawing.Point(569, 150);
            this.selectDestinationMod_btn.Name = "selectDestinationMod_btn";
            this.selectDestinationMod_btn.Size = new System.Drawing.Size(44, 24);
            this.selectDestinationMod_btn.TabIndex = 39;
            this.selectDestinationMod_btn.Text = "...";
            this.selectDestinationMod_btn.UseVisualStyleBackColor = false;
            // 
            // destinationMod_cbb
            // 
            this.destinationMod_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationMod_cbb.FormattingEnabled = true;
            this.destinationMod_cbb.Location = new System.Drawing.Point(323, 150);
            this.destinationMod_cbb.Name = "destinationMod_cbb";
            this.destinationMod_cbb.Size = new System.Drawing.Size(246, 24);
            this.destinationMod_cbb.TabIndex = 38;
            // 
            // destinationMod_lbl
            // 
            this.destinationMod_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationMod_lbl.AutoSize = true;
            this.destinationMod_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.destinationMod_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationMod_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.destinationMod_lbl.Location = new System.Drawing.Point(172, 154);
            this.destinationMod_lbl.Name = "destinationMod_lbl";
            this.destinationMod_lbl.Size = new System.Drawing.Size(145, 16);
            this.destinationMod_lbl.TabIndex = 37;
            this.destinationMod_lbl.Text = "Destination Module:";
            this.destinationMod_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // save_DestinationMod_btn
            // 
            this.save_DestinationMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.save_DestinationMod_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.save_DestinationMod_btn.FlatAppearance.BorderSize = 0;
            this.save_DestinationMod_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.save_DestinationMod_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.save_DestinationMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_DestinationMod_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_DestinationMod_btn.ForeColor = System.Drawing.Color.White;
            this.save_DestinationMod_btn.Location = new System.Drawing.Point(619, 150);
            this.save_DestinationMod_btn.Name = "save_DestinationMod_btn";
            this.save_DestinationMod_btn.Size = new System.Drawing.Size(44, 24);
            this.save_DestinationMod_btn.TabIndex = 36;
            this.save_DestinationMod_btn.Text = "Save";
            this.save_DestinationMod_btn.UseVisualStyleBackColor = false;
            this.save_DestinationMod_btn.Click += new System.EventHandler(this.Save_DestinationMod_btn_Click);
            // 
            // selectOriginalMod_btn
            // 
            this.selectOriginalMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectOriginalMod_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.selectOriginalMod_btn.FlatAppearance.BorderSize = 0;
            this.selectOriginalMod_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.selectOriginalMod_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.selectOriginalMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectOriginalMod_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectOriginalMod_btn.ForeColor = System.Drawing.Color.White;
            this.selectOriginalMod_btn.Location = new System.Drawing.Point(569, 118);
            this.selectOriginalMod_btn.Name = "selectOriginalMod_btn";
            this.selectOriginalMod_btn.Size = new System.Drawing.Size(44, 24);
            this.selectOriginalMod_btn.TabIndex = 35;
            this.selectOriginalMod_btn.Text = "...";
            this.selectOriginalMod_btn.UseVisualStyleBackColor = false;
            // 
            // originalMod_cbb
            // 
            this.originalMod_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalMod_cbb.FormattingEnabled = true;
            this.originalMod_cbb.Location = new System.Drawing.Point(323, 118);
            this.originalMod_cbb.Name = "originalMod_cbb";
            this.originalMod_cbb.Size = new System.Drawing.Size(246, 24);
            this.originalMod_cbb.TabIndex = 34;
            // 
            // originalMod_lbl
            // 
            this.originalMod_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalMod_lbl.AutoSize = true;
            this.originalMod_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.originalMod_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalMod_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.originalMod_lbl.Location = new System.Drawing.Point(196, 122);
            this.originalMod_lbl.Name = "originalMod_lbl";
            this.originalMod_lbl.Size = new System.Drawing.Size(121, 16);
            this.originalMod_lbl.TabIndex = 33;
            this.originalMod_lbl.Text = "Original Module:";
            this.originalMod_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // save_OriginalMod_btn
            // 
            this.save_OriginalMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.save_OriginalMod_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.save_OriginalMod_btn.FlatAppearance.BorderSize = 0;
            this.save_OriginalMod_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.save_OriginalMod_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.save_OriginalMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_OriginalMod_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_OriginalMod_btn.ForeColor = System.Drawing.Color.White;
            this.save_OriginalMod_btn.Location = new System.Drawing.Point(619, 118);
            this.save_OriginalMod_btn.Name = "save_OriginalMod_btn";
            this.save_OriginalMod_btn.Size = new System.Drawing.Size(44, 24);
            this.save_OriginalMod_btn.TabIndex = 32;
            this.save_OriginalMod_btn.Text = "Save";
            this.save_OriginalMod_btn.UseVisualStyleBackColor = false;
            this.save_OriginalMod_btn.Click += new System.EventHandler(this.Save_Mod_btn_Click);
            // 
            // ProjectProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1187, 308);
            this.Controls.Add(this.extraOptions_tree);
            this.Controls.Add(this.min_btn);
            this.Controls.Add(this.maxnorm_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.projects_panel);
            this.Controls.Add(this.itemsets_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjectProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.ExtraOptions_Load);
            this.itemsets_panel.ResumeLayout(false);
            this.itemsets_panel.PerformLayout();
            this.projects_panel.ResumeLayout(false);
            this.projects_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button min_btn;
        private System.Windows.Forms.Button maxnorm_btn;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.TreeView extraOptions_tree;
        private System.Windows.Forms.Panel itemsets_panel;
        private System.Windows.Forms.Label swingDamage_lbl;
        private System.Windows.Forms.TextBox swingDamage_txt;
        private System.Windows.Forms.Label thrustDamage_lbl;
        private System.Windows.Forms.TextBox thrustDamage_txt;
        private System.Windows.Forms.Label legArmor_lbl;
        private System.Windows.Forms.TextBox legArmor_txt;
        private System.Windows.Forms.Label bodyArmor_lbl;
        private System.Windows.Forms.TextBox bodyArmor_txt;
        private System.Windows.Forms.Label headArmor_lbl;
        private System.Windows.Forms.TextBox headArmor_txt;
        private System.Windows.Forms.ListBox usedItems_lb;
        private SearchTextBox searchItems_SearchTextBox;
        private System.Windows.Forms.Button abort_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.Label valueInfo_lbl;
        private System.Windows.Forms.Label setName_lbl;
        private System.Windows.Forms.TextBox setName_txt;
        private System.Windows.Forms.Button setItemFlags_btn;
        private System.Windows.Forms.Label selectedItemFlags_lbl;
        private System.Windows.Forms.TextBox selectedItemFlags_txt;
        private System.Windows.Forms.Button usedItemREMOVE_btn;
        private System.Windows.Forms.Button usedItemDOWN_btn;
        private System.Windows.Forms.Button usedItemUP_btn;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.ListBox items_lb;
        private SearchTextBox searchItems_AllItems_SearchTextBox;
        private System.Windows.Forms.Panel projects_panel;
        private System.Windows.Forms.Label originalMod_lbl;
        private System.Windows.Forms.Button save_OriginalMod_btn;
        private System.Windows.Forms.ComboBox originalMod_cbb;
        private System.Windows.Forms.Button selectDestinationMod_btn;
        private System.Windows.Forms.ComboBox destinationMod_cbb;
        private System.Windows.Forms.Label destinationMod_lbl;
        private System.Windows.Forms.Button save_DestinationMod_btn;
        private System.Windows.Forms.Button selectOriginalMod_btn;
        private System.Windows.Forms.CheckBox copyDefaultVariables_cb;
        private System.Windows.Forms.Button projectFolder_btn;
        private System.Windows.Forms.TextBox projectFolder_txt;
        private System.Windows.Forms.Label projectFolder_lbl;
        private System.Windows.Forms.Button saveProjectFolder_btn;
    }
}