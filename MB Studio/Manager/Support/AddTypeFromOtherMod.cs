using brfManager;
using skillhunter;
using importantLib;
using MB_Decompiler_Library.IO;
using System;
using System.Collections.Generic;

namespace MB_Studio
{
    public partial class AddTypeFromOtherMod : SpecialFormBlack
    {
        private int objectTypeID;
        private string originalModuleName;

        //public string SelectedMeshName { get; private set; } = null;
        public Skriptum SelectedType { get; private set; } = null;

        private List<Skriptum> types = new List<Skriptum>();
        private List<string> curMeshNames = new List<string>();

        private const string DEFAULT_SELECTION_TEXT = " < SELECT >";

        private static List<string> moduleNames = new List<string>();
        //private static List<List<string>> allMeshNames = new List<List<string>>();

        private static OpenBrfManager openBrfManager = null;

        public AddTypeFromOtherMod(ref OpenBrfManager openBrfManager, int objectTypeID) : base()
        {
            this.objectTypeID = objectTypeID;

            originalModuleName = openBrfManager.ModName;

            if (AddTypeFromOtherMod.openBrfManager == null)
                AddTypeFromOtherMod.openBrfManager = openBrfManager;

            //if (allMeshNames.Count == 0)//load all optional - maybe later
            //    allMeshNames.AddRange(openBrfManager.GetAllMeshResourceNames(out moduleNames));

            if (moduleNames.Count == 0)
                moduleNames.AddRange(openBrfManager.GetAllModuleNames());

            InitializeComponent();
        }

        private void AddTypeFromOtherMod_Load(object sender, EventArgs e)
        {
            module_cbb.Items.AddRange(moduleNames.ToArray());
            module_cbb.Text = DEFAULT_SELECTION_TEXT;
            types_cbb.Text = DEFAULT_SELECTION_TEXT;
        }

        private void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = module_cbb.SelectedIndex;
            if (openBrfManager == null) return;

            openBrfManager.ChangeModule(module_cbb.SelectedItem.ToString());

            types.Clear();
            types_cbb.Items.Clear();

            CodeReader cr = new CodeReader(openBrfManager.ModPath + '\\' + CodeReader.Files[objectTypeID]);
            types.AddRange(cr.ReadItem());
            foreach (Skriptum type in types)
                types_cbb.Items.Add(type.ID);

            types_cbb.Text = DEFAULT_SELECTION_TEXT;
        }

        private void AddTypeFromMod_btn_Click(object sender, EventArgs e)
        {
            string curModName = openBrfManager.ModName;

            /* CODE AUSGELAGERT */

            openBrfManager.ChangeModule(originalModuleName);

            Close();
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
    }
}
