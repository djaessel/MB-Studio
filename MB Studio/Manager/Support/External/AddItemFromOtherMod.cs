using skillhunter;
using System;
using System.Collections.Generic;

namespace MB_Studio.Manager.Support.External
{
    public partial class AddItemFromOtherMod : AddTypeFromOtherMod
    {
        #region Attributes

        public string SelectedMeshName { get; private set; } = null;
        private List<string> curMeshNames = new List<string>();

        #endregion
        
        #region Loading

        public AddItemFromOtherMod() : base(5)//5 = ITEM
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        #region Overrides

        protected override void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Module_cbb_SelectedIndexChanged(sender, e);

            curMeshNames = ToolForm.OpenBrfManager.GetCurrentModuleAllMeshResourceNames();//commRes = false

            meshName_cbb.Items.Clear();
            meshName_cbb.Items.AddRange(curMeshNames.ToArray());
            meshName_cbb.Text = DEFAULT_SELECTION_TEXT;

            bool b = (curMeshNames.Count != 0);

            meshName_rb.Enabled = b;

            b = (b && types.Count == 0);

            meshName_cbb.Enabled = b;
            //TypeMode = !b;

            if (!b) return;//prevent event trigger

            meshName_rb.Checked = b;
        }

        protected override void AddTypeFromModFinish()
        {
            string curModName = ToolForm.OpenBrfManager.ModName;

            if (type_rb.Checked && !type_cbb.Text.Equals(DEFAULT_SELECTION_TEXT))
            {
                foreach (string mesh in ((Item)SelectedType).Meshes)
                {
                    if (!ToolForm.OpenBrfManager.ModName.Equals(curModName))
                        ToolForm.OpenBrfManager.ChangeModule(curModName);

                    string[] meshData = mesh.Split();//meshData[1] -> modifiers (maybe use later for selection position)
                    ToolForm.OpenBrfManager.SelectItemNameByKind(meshData[0]);
                    ToolForm.OpenBrfManager.AddSelectedMeshsToMod(originalModuleName);
                }
            }
            else if (meshName_rb.Checked && !meshName_cbb.Text.Equals(DEFAULT_SELECTION_TEXT))
            {
                ToolForm.OpenBrfManager.AddSelectedMeshsToMod(originalModuleName);
            }

            base.AddTypeFromModFinish();
        }

        protected override void Types_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Types_cbb_SelectedIndexChanged(sender, e);

            Item item = (Item)SelectedType;
            item.SetFactions(new List<int>() { 0 });//prevent Faction Problems

            List<string> meshes = item.Meshes;
            ToolForm.OpenBrfManager.SelectItemNameByKind(meshes[meshes.Count - 1].Split()[0]);
        }

        protected override void Exit_btn_Click(object sender, EventArgs e)
        {
            base.Exit_btn_Click(sender, e);

            SelectedMeshName = null;
        }

        #endregion

        private void MeshName_rb_CheckedChanged(object sender, EventArgs e)
        {
            DeactivateAllOtherModes(meshName_cbb.Name, meshName_cbb.Text);
        }

        private void MeshName_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeMode = false;
            SelectedMeshName = meshName_cbb.SelectedItem.ToString();
            ToolForm.OpenBrfManager.SelectItemNameByKind(SelectedMeshName);
            addTypeFromMod_btn.Enabled = true;
        }

        #endregion
    }
}
