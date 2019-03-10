using MB_Studio_Library.Objects;
using MB_Studio.Manager.Support.External;
using System;
using System.Collections.Generic;
using MB_Studio_Library.IO;

namespace MB_Studio.Manager
{
    public partial class StringManager : ToolForm
    {
        public StringManager() : base(Skriptum.ObjectType.GameString)
        {
            InitializeComponent();
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            base.AddFromOtherMod(f);
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);


        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            GameString gameString = (GameString)type;

            //Language_cbb_SelectedIndexChanged();
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);


        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0] + ';';
            tmp += values[1] + ';';

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            GameString s = new GameString(valuesX);

            CodeWriter.SavePseudoCodeByType(s, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }
    }
}
