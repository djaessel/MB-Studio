using skillhunter;
using MB_Decompiler_Library.Objects;
using MB_Studio.Manager.Support.External;
using System;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    public partial class StringManager : ToolForm
    {
        public StringManager() : base(Skriptum.ObjectType.GAME_STRING)
        {
            InitializeComponent();
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new GameString(raw_data);
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
            string tmp = values[0].Split()[0] + ';';
            tmp += name_txt.Text.Replace(' ', '_') + ';';

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            GameString s = new GameString(valuesX);

            MB_Studio.SavePseudoCodeByType(s, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }
    }
}
