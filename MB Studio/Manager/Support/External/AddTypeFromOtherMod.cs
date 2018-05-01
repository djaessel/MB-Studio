using skillhunter;
using importantLib;
using MB_Decompiler_Library.IO;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MB_Studio.Manager.Support.External
{
    public partial class AddTypeFromOtherMod : SpecialFormBlack
    {
        #region Attributes / Consts

        public int ObjectTypeID { get; private set; } = 0;
        protected string originalModuleName = string.Empty;
        public bool TypeMode { get; protected set; } = true;
        public Skriptum SelectedType { get; protected set; } = null;
        protected List<Skriptum> types = new List<Skriptum>();
        protected const string DEFAULT_SELECTION_TEXT = " < SELECT >";
        protected static List<string> moduleNames = new List<string>();

        #endregion

        #region Loading

        public AddTypeFromOtherMod() : base()
        {
            Construktor(0);
        }

        public AddTypeFromOtherMod(int objectTypeID) : base()
        {
            Construktor(objectTypeID);
        }

        private void Construktor(int objectTypeID)
        {
            ObjectTypeID = objectTypeID;

            InitializeComponent();

            if (ToolForm.OpenBrfManager == null) return;
            if (!ToolForm.OpenBrfManager.IsShown) return;

            originalModuleName = ToolForm.OpenBrfManager.ModName;

            if (moduleNames.Count == 0)
                moduleNames.AddRange(ToolForm.OpenBrfManager.GetAllModuleNames());
        }

        protected virtual void AddTypeFromOtherMod_Load(object sender, EventArgs e)
        {
            module_cbb.Items.AddRange(moduleNames.ToArray());
            foreach (Control c in Controls)
                if (GetNameEndOfControl(c).Equals("cbb"))
                    c.Text = DEFAULT_SELECTION_TEXT;
        }

        #endregion

        #region Events

        protected virtual void Module_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ToolForm.OpenBrfManager == null) return;

            ToolForm.OpenBrfManager.ChangeModule(module_cbb.SelectedItem.ToString());

            types.Clear();
            type_cbb.Items.Clear();

            CodeReader cr = new CodeReader(ToolForm.OpenBrfManager.ModPath + '\\' + CodeReader.Files[ObjectTypeID]);
            types.AddRange(cr.ReadObjectType(ObjectTypeID));//remove mod inrelevant info in Read Method of type or add other needed types
            foreach (Skriptum type in types)
                type_cbb.Items.Add(type.ID);

            type_cbb.Text = DEFAULT_SELECTION_TEXT;

            bool b = (types.Count != 0);

            if (!b) return;

            type_cbb.Enabled = b;
            type_rb.Enabled = b;
        }

        private void Type_rb_CheckedChanged(object sender, EventArgs e)
        {
            DeactivateAllOtherModes(type_cbb.Name, type_cbb.Text);
        }

        protected virtual void Types_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedType = GetTypeByID(type_cbb.SelectedItem.ToString());
            addTypeFromMod_btn.Enabled = true;
            TypeMode = true;
        }

        private void AddTypeFromMod_btn_Click(object sender, EventArgs e)
        {
            AddTypeFromModFinish();
        }

        protected virtual void AddTypeFromModFinish()
        {
            ResetToOriginalModule();
            Close();
        }

        protected virtual void Exit_btn_Click(object sender, EventArgs e)
        {
            ResetToOriginalModule();
            SelectedType = null;
        }

        #endregion

        #region Useful Methods

        protected void ResetToOriginalModule()
        {
            ToolForm.OpenBrfManager.ChangeModule(originalModuleName);
            //maybe load default or empty screen here
        }

        protected Skriptum GetTypeByID(string typeID)
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

        protected void DeactivateAllOtherModes(string curName, string curSelText)
        {
            foreach (Control c in Controls)
                if (GetNameEndOfControl(c).Equals("cbb"))
                    c.Enabled = (c.Name.Equals(module_cbb.Name) || c.Name.Equals(curName));

            addTypeFromMod_btn.Enabled = !curSelText.Equals(DEFAULT_SELECTION_TEXT);
        }

        #endregion
    }
}
