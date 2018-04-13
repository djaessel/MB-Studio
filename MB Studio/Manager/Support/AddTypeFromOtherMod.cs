using brfManager;
using skillhunter;
using importantLib;
using MB_Decompiler_Library.IO;
using System;
using System.Collections.Generic;

namespace MB_Studio
{
    public partial class AddItemFromOtherMod : SpecialFormBlack
    {
        private string originalModuleName;

        public enum MODES
        {
            NONE,
            MESH,
            TYPE
        }

        public MODES MODE { get; private set; } = MODES.NONE;
        public string SelectedMeshName { get; private set; } = null;
        public Skriptum SelectedType { get; private set; } = null;

        private bool useMesh;

        private List<Skriptum> types = new List<Skriptum>();
        private List<string> curMeshNames = new List<string>();

        private static List<string> moduleNames = new List<string>();
        //private static List<List<string>> allMeshNames = new List<List<string>>();

        private static OpenBrfManager openBrfManager = null;

        public AddItemFromOtherMod(ref OpenBrfManager openBrfManager, bool useMesh = false) : base()
        {
            this.useMesh = useMesh;

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

            if (!useMesh) return;

            meshName_cbb.Visible = true;
            meshName_rb.Visible = true;
            Height += 32;
        }

        private void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = module_cbb.SelectedIndex;
            if (openBrfManager == null) return;

            openBrfManager.ChangeModule(module_cbb.SelectedItem.ToString());

            types.Clear();
            item_cbb.Items.Clear();

            CodeReader cr = new CodeReader(openBrfManager.ModPath + '\\' + CodeReader.Files[(int)Skriptum.ObjectType.ITEM]);
            types.AddRange(cr.ReadItem());
            foreach (Item item in types)
                item_cbb.Items.Add(item.ID);

            bool b = (item_cbb.Items.Count != 0 || meshName_cbb.Items.Count != 0);

            addKind_gb.Enabled = b;
            addItemFromMod_btn.Enabled = b;

            if (!b) return;

            item_cbb.Text = " < SELECT ITEM >";

            b = (item_cbb.Items.Count != 0);
            type_rb.Checked = b;
            type_rb.Enabled = b;

            if (!useMesh) return;

            meshName_cbb.Items.Clear();
            curMeshNames = openBrfManager.GetCurrentModuleAllMeshResourceNames();//false
            meshName_cbb.Items.AddRange(curMeshNames.ToArray());

            if (!b)
            {
                meshName_cbb.Text = " < SELECT MESH >";
                b = (meshName_cbb.Items.Count != 0);//maybe change mesh and item position in code or just activate only one later
                meshName_rb.Checked = b;
            }

            meshName_rb.Enabled = (meshName_cbb.Items.Count != 0);
        }

        private void AddItemFromMod_btn_Click(object sender, EventArgs e)
        {
            string curModName = openBrfManager.ModName;

            if (MODE == MODES.TYPE)
            {
                if (SelectedType.ObjectTyp == Skriptum.ObjectType.ITEM)
                {
                    foreach (string mesh in ((Item)SelectedType).Meshes)
                    {
                        if (!openBrfManager.ModName.Equals(curModName))
                            openBrfManager.ChangeModule(curModName);

                        string[] meshData = mesh.Split();//meshData[1] -> modifiers (maybe use later for selection position)
                        openBrfManager.SelectItemNameByKind(meshData[0]);
                        openBrfManager.AddSelectedMeshsToMod(originalModuleName);
                    }
                }
            }
            else if (MODE == MODES.MESH && useMesh)
            {
                openBrfManager.AddSelectedMeshsToMod(originalModuleName);
            }

            openBrfManager.ChangeModule(originalModuleName);

            Close();
        }

        private void Type_rb_CheckedChanged(object sender, EventArgs e)
        {
            item_cbb.Enabled = type_rb.Checked;
            if (type_rb.Checked)
                MODE = MODES.TYPE;
        }

        private void Type_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedType = GetTypeByID(item_cbb.SelectedItem.ToString());
            if (SelectedType.ObjectTyp == Skriptum.ObjectType.ITEM)
            {
                Item item = (Item)SelectedType;
                item.SetFactions(new List<int>() { 0 });//prevent Faction Problems
                List<string> meshes = item.Meshes;
                openBrfManager.SelectItemNameByKind(meshes[meshes.Count - 1].Split()[0]);
            }
        }

        private void MeshName_rb_CheckedChanged(object sender, EventArgs e)
        {
            meshName_cbb.Enabled = meshName_rb.Checked;
            if (meshName_rb.Checked)
                MODE = MODES.MESH;
        }

        private void MeshName_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMeshName = meshName_cbb.SelectedItem.ToString();
            openBrfManager.SelectItemNameByKind(SelectedMeshName);
        }

        private Skriptum GetTypeByID(string typeID)
        {
            Skriptum typeX = null;
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].ID.Equals(typeID))
                {
                    typeX = types[i];
                    i = types.Count;
                }
            }
            return typeX;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            MODE = MODES.NONE;
        }
    }
}
