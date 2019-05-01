using MB_Studio_Library.Objects;
using MB_Studio.Manager.Support.External;
using System;
using System.Collections.Generic;
using MB_Studio_Library.IO;

namespace MB_Studio.Manager
{
    public partial class QuestManager : ToolForm
    {
        public QuestManager() : base(Skriptum.ObjectType.Quest)
        {
            InitializeComponent();
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            // TODO: add from other mod

            base.AddFromOtherMod(f);
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);

            string desc = translations[language_cbb.SelectedIndex][1];
            if (desc == null)
                desc = plural_name_txt.Text;
            pluralNameTranslation_txt.Text = desc;
        }

        protected override void LoadSettingsAndLists()
        {
            HasSecondTranslation = true;
            SecondSuffix = "text";

            base.LoadSettingsAndLists();
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Quest quest = (Quest)type;

            #region Translation / Description

            plural_name_txt.Text = quest.Description;

            Language_cbb_SelectedIndexChanged();

            #endregion

            #region Flags

            show_progression_cb.Checked = quest.ShowProgression;
            random_quest_cb.Checked = quest.RandomQuest;

            #endregion
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);

            //InteractWithTextLanguage(true);
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0] + ';';
            tmp += values[1] + ';';
            tmp += GetFlags() + ";";
            tmp += plural_name_txt.Text + ";";

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            Quest q = new Quest(valuesX);

            CodeWriter.SavePseudoCodeByType(q, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }

        private int GetFlags()
        {
            int flags = 0;

            if (show_progression_cb.Checked)
                flags |= 0x1;

            if (random_quest_cb.Checked)
                flags |= 0x2;

            return flags;
        }


        private void Show_progression_cb_CheckedChanged(object sender, EventArgs e)
        {
            ((Quest)types[CurrentTypeIndex]).ShowProgression = show_progression_cb.Checked;
        }

        private void Random_quest_cb_CheckedChanged(object sender, EventArgs e)
        {
            ((Quest)types[CurrentTypeIndex]).RandomQuest = random_quest_cb.Checked;
        }
    }
}
