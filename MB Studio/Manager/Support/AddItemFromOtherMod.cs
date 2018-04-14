using System;
using System.Collections.Generic;
using System.Windows.Forms;
using brfManager;

namespace MB_Studio.Manager.Support
{
    public partial class AddItemFromOtherMod : AddTypeFromOtherMod
    {
        public AddItemFromOtherMod(ref OpenBrfManager openBrfManager, int objectTypeID) : base(ref openBrfManager, objectTypeID)
        {

        }

        /*
            AddTypeFromOtherMod_Load

            meshName_cbb.Visible = true;
            meshName_rb.Visible = true;
            Height += 32;
        */

        /*
            Module_cbb_SelectedIndexChanged

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
        */

        /*
            AddTypeFromMod_btn_Click

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
                else if (MODE == MODES.MESH && useMesh)
                {
                    openBrfManager.AddSelectedMeshsToMod(originalModuleName);
                }
        */

        /*
            Type_cbb_SelectedIndexChanged

            SelectedType = GetTypeByID(types_cbb.SelectedItem.ToString());

            Item item = (Item)SelectedType;
            item.SetFactions(new List<int>() { 0 });//prevent Faction Problems
            List<string> meshes = item.Meshes;
            openBrfManager.SelectItemNameByKind(meshes[meshes.Count - 1].Split()[0]);
        */

        /*
            MeshName_cbb_SelectedIndexChanged

            SelectedMeshName = meshName_cbb.SelectedItem.ToString();
            openBrfManager.SelectItemNameByKind(SelectedMeshName);
        */
    }
}
