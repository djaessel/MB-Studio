using brfManager;
using skillhunter;
using importantLib;
using MB_Decompiler_Library.IO;
using System;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    public partial class AddItemFromOtherMod : SpecialFormBlack
    {
        private string originalModuleName;

        public enum MODES
        {
            NONE,
            MESH,
            ITEM,
            //...
        }

        public MODES MODE { get; private set; } = MODES.NONE;
        public string SelectedMeshName { get; private set; } = null;
        public Item SelectedItem { get; private set; } = null;

        private List<Item> items = new List<Item>();
        private List<string> curMeshNames = new List<string>();

        private static List<string> moduleNames = new List<string>();
        //private static List<List<string>> allMeshNames = new List<List<string>>();

        private static OpenBrfManager openBrfManager = null;

        public AddItemFromOtherMod(ref OpenBrfManager openBrfManager) : base()
        {
            originalModuleName = openBrfManager.ModName;

            if (AddItemFromOtherMod.openBrfManager == null)
                AddItemFromOtherMod.openBrfManager = openBrfManager;

            //if (allMeshNames.Count == 0)//load all optional - maybe later
            //    allMeshNames.AddRange(openBrfManager.GetAllMeshResourceNames(out moduleNames));

            if (moduleNames.Count == 0)
                moduleNames.AddRange(openBrfManager.GetAllModuleNames());

            InitializeComponent();
        }

        private void AddItemFromOtherMod_Load(object sender, EventArgs e)
        {
            module_cbb.Items.AddRange(moduleNames.ToArray());
            module_cbb.SelectedIndex = 0;
        }

        private void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = module_cbb.SelectedIndex;
            if (openBrfManager != null)
            {
                openBrfManager.ChangeModule(module_cbb.SelectedItem.ToString());

                items.Clear();
                item_cbb.Items.Clear();

                CodeReader cr = new CodeReader(openBrfManager.ModPath + '\\' + CodeReader.Files[(int)Skriptum.ObjectType.ITEM]);
                items.AddRange(cr.ReadItem());
                foreach (Item item in items)
                    item_cbb.Items.Add(item.ID);

                bool b = (item_cbb.Items.Count != 0);
                if (b)
                    item_cbb.SelectedIndex = 0;
                item_rb.Checked = b;
                item_rb.Enabled = b;

                meshName_cbb.Items.Clear();
                curMeshNames = openBrfManager.GetCurrentModuleAllMeshResourceNames();
                meshName_cbb.Items.AddRange(curMeshNames.ToArray());

                b = (meshName_cbb.Items.Count != 0);
                if (b)
                    meshName_cbb.SelectedIndex = 0;
                meshName_rb.Checked = b;
                meshName_rb.Enabled = b;
            }
        }

        private void AddItemFromMod_btn_Click(object sender, EventArgs e)
        {
            //if (MODE == MODES.MESH)
            //    openBrfManager.AddSelectedMeshsToMod(originalModuleName);
            //else
                openBrfManager.ChangeModule(originalModuleName);

            Close();
        }

        private void MeshName_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMeshName = meshName_cbb.SelectedItem.ToString();
            openBrfManager.SelectItemNameByKind(SelectedMeshName);
        }

        private void Item_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = GetItemByID(item_cbb.SelectedItem.ToString());
            List<string> meshes = SelectedItem.Meshes;
            openBrfManager.SelectItemNameByKind(meshes[meshes.Count - 1].Split()[0]);
        }

        private void MeshName_rb_CheckedChanged(object sender, EventArgs e)
        {
            meshName_cbb.Enabled = meshName_rb.Checked;
            if (meshName_rb.Checked)
                MODE = MODES.MESH;
        }

        private void Item_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_cbb.Enabled = item_rb.Checked;
            if (item_rb.Checked)
                MODE = MODES.ITEM;
        }

        private Item GetItemByID(string itemID)
        {
            Item itemX = null;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID.Equals(itemID))
                {
                    itemX = items[i];
                    i = items.Count;
                }
            }
            return itemX;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            MODE = MODES.NONE;
        }
    }
}
