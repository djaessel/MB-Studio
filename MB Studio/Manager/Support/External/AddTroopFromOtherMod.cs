using MB_Decompiler_Library.IO;
using skillhunter;
using System;
using System.Collections.Generic;

namespace MB_Studio.Manager.Support.External
{
    public partial class AddTroopFromOtherMod : AddTypeFromOtherMod
    {
        #region Attributes
        #endregion
        
        #region Loading

        public AddTroopFromOtherMod() : base(4)//4 = TROOP
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        #region Overrides

        protected override void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Module_cbb_SelectedIndexChanged(sender, e);


        }

        protected override void AddTypeFromModFinish()
        {
            //string curModName = ToolForm.OpenBrfManager.ModName;

            //if (type_rb.Checked && !type_cbb.Text.Equals(DEFAULT_SELECTION_TEXT))
            //{
                /*foreach (string mesh in ((Item)SelectedType).Meshes)
                {
                    if (!ToolForm.OpenBrfManager.ModName.Equals(curModName))
                        ToolForm.OpenBrfManager.ChangeModule(curModName);

                    string[] meshData = mesh.Split();//meshData[1] -> modifiers (maybe use later for selection position)
                    ToolForm.OpenBrfManager.SelectItemNameByKind(meshData[0]);
                    ToolForm.OpenBrfManager.AddSelectedMeshsToMod(originalModuleName);
                }*/
            //}

            base.AddTypeFromModFinish();
        }

        protected override void Types_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Types_cbb_SelectedIndexChanged(sender, e);

            Troop troop = (Troop)SelectedType;
            //prevent factions and other Problems

            List<Item> items = new List<Item>();
            //foreach (int item in troop.Items)
            //    items.Add(CodeReader.Items[item]);

            ToolForm.OpenBrfManager.Troop3DPreviewClearData();

            foreach (Item item in items)
            {
                foreach (string mesh in item.Meshes)
                {
                    //check bone usw.
                    ToolForm.OpenBrfManager.AddMeshToTroop3DPreview(mesh);
                }
            }

            ToolForm.OpenBrfManager.Troop3DPreviewShow();
        }

        #endregion

        #endregion
    }
}
