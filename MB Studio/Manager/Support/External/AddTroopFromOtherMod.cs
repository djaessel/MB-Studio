using MB_Studio_Library.IO;
using MB_Studio_Library.Objects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            if (!type_rb.Checked || type_cbb.Text.Equals(DEFAULT_SELECTION_TEXT)) return;

            DialogResult result = MessageBox.Show(
                "Items des Troops ebenfalls importieren?",
                Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1
            );

            if (result != DialogResult.Yes && result != DialogResult.No) return;

            Troop troop = (Troop)SelectedType;

            if (result == DialogResult.Yes)
            {
                CodeReader cr = new CodeReader(ToolForm.OpenBrfManager.ModulesPath.TrimEnd('\\') + '\\' + ToolForm.OpenBrfManager.ModName + "\\item_kinds1.txt");
                List<Item> itemsR = cr.ReadItem();
                //foreach (int itemIdx in troop.Items)
                //    AddItemFromOtherMod.AddItemMeshesToMod(itemsR[itemIdx], originalModuleName);
            }

            /// ASK FOR ADDING OR DELETE FOR EACH OF THEM !!!
            
            string itmx = string.Empty;
            for (int i = 0; i < 64; i++)
                itmx += "-1 0 ";
            troop.SetItems(itmx);

            troop.SetSkills("0 0 0 0 0 0");

            troop.SetSceneCode("0");

            troop.UpgradeTroop1 = -1;
            troop.UpgradeTroop2 = -1;

            troop.FactionID = 0;

            //troop.DialogImage = "0";//not possible - or try adding image if not 0 / none

            /// ASK FOR ADDING OR DELETE FOR EACH OF THEM !!!

            SelectedType = troop;

            // CHECK SetupType(Skriptum s) Method in TroopManager!!!

            base.AddTypeFromModFinish();
        }

        protected override void Types_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Types_cbb_SelectedIndexChanged(sender, e);

            Troop troop = (Troop)SelectedType;

            List<Item> items = new List<Item>();
            //foreach (int item in troop.Items)
            //{
            //items.Add(CodeReader.Items[item]);
            //}

            ToolForm.OpenBrfManager.Troop3DPreviewClearData();
            foreach (Item item in items)
            {
                foreach (var mesh in item.Meshes)
                {
                    //check bone usw.
                    ToolForm.OpenBrfManager.AddMeshToTroop3DPreview(mesh.Name);
                }
            }
            //ToolForm.OpenBrfManager.Troop3DPreviewShow();
        }

        #endregion

        #endregion
    }
}
