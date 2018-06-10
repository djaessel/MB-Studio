using MB_Decompiler_Library.Objects;
using MB_Studio.Manager.Support.External;
using System;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    public partial class QuestManager : ToolForm
    {
        public QuestManager() : base(Skriptum.ObjectType.QUEST)
        {
            InitializeComponent();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new Quest(raw_data);
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            base.AddFromOtherMod(f);
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);

            //InteractWithTextLanguage();
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Quest quest = (Quest)type;

            #region Translation / Description

            //description_txt.Text = skill.Description;

            Language_cbb_SelectedIndexChanged();

            #endregion

            #region Flags & MaxLevel



            #endregion
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);

            //InteractWithTextLanguage(true);
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0].Split()[0] + ';';
            tmp += name_txt.Text.Replace(' ', '_') + ';';
            tmp += GetFlags() + ";";

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            Quest q = new Quest(valuesX);

            MB_Studio.SavePseudoCodeByType(q, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }

        private int GetFlags()
        {
            int flags = 0;



            return flags;
        }

        protected override void ResetControls()
        {
            base.ResetControls();


        }

        /*private void InteractWithTextLanguage(bool setValue = false)
        {
            int index = typeSelect_lb.SelectedIndex - 1;
            if (index >= 0)
            {
                string v = Prefix + id_txt.Text + "_desc";
                string filePath = CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT, GetLanguageFromIndex(language_cbb.SelectedIndex));
                if (File.Exists(filePath))
                {
                    bool found = false;
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] tmp = lines[i].Split('|');
                        if (tmp[0].Equals(v))
                        {
                            if (setValue)
                            {
                                lines[i] = tmp[0] + '|' + description_txt.Text.Replace(' ', '_');
                                File.WriteAllLines(filePath, lines);
                                return;
                            }
                            else
                                v = tmp[1].Replace('_', ' ');
                            found = true;
                        }
                    }
                    if (!found)
                        v = ((Skill)types[index]).Description;
                    description_txt.Text = v;
                }
                //else
                //    System.Windows.Forms.MessageBox.Show("PATH DOESN'T EXIST --> CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT)" + Environment.NewLine + CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT));
            }
        }*/
    }
}
