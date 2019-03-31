using MB_Studio_Library.Objects;
using MB_Studio.Manager.Support.External;
using System.Collections.Generic;
using MB_Studio_Library.IO;

namespace MB_Studio.Manager
{
    public partial class StringManager : ToolForm
    {
        public StringManager() : base(Skriptum.ObjectType.GameString)
        {
            InitializeComponent();

            name_lbl.Text = "Text:";
            name_lbl.Left = id_lbl.Right - name_lbl.Width;

            singleNameTranslation_lbl.Text = "Translation:";
            singleNameTranslation_lbl.Left = singleNameTranslation_txt.Left - singleNameTranslation_lbl.Width - 6;
        }

        protected override void LoadSettingsAndLists()
        {
            HasPluralTranslation = false;

            base.LoadSettingsAndLists();
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            // TODO: add from other mod

            base.AddFromOtherMod(f);
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            GameString gameString = (GameString)type;
            id_txt.Text = "str_" + id_txt.Text;
            name_txt.Text = gameString.Text;

            Language_cbb_SelectedIndexChanged();
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0] + ';' + values[1];

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            GameString s = new GameString(valuesX);

            CodeWriter.SavePseudoCodeByType(s, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }
    }
}
