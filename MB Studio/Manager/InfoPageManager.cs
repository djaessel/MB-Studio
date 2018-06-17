using System;
using System.Collections.Generic;
using System.IO;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects;
using MB_Studio.Manager.Support.External;

namespace MB_Studio.Manager
{
    public partial class InfoPageManager : ToolForm
    {
        public InfoPageManager() : base(Skriptum.ObjectType.InfoPage)
        {
            InitializeComponent();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new InfoPage(raw_data);
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            base.AddFromOtherMod(f);
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);

            InteractWithTextLanguage();
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            InfoPage infoPage = (InfoPage)type;

            text_txt.Text = infoPage.Text;

            Language_cbb_SelectedIndexChanged();
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);

            InteractWithTextLanguage(true);
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0].Split()[0] + ';';
            tmp += name_txt.Text.Replace(' ', '_') + ';';
            tmp += text_txt.Text.Replace(' ', '_');

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            InfoPage i = new InfoPage(valuesX);

            MB_Studio.SavePseudoCodeByType(i, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }

        protected override void ResetControls()
        {
            base.ResetControls();

            text_txt.ResetText();
        }

        private void InteractWithTextLanguage(bool setValue = false)
        {
            int index = typeSelect_lb.SelectedIndex - 1;
            if (index >= 0)
            {
                string v = Prefix + id_txt.Text + "_text";
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
                                lines[i] = tmp[0] + '|' + text_txt.Text.Replace(' ', '_');
                                File.WriteAllLines(filePath, lines);
                                return;
                            }
                            else
                                v = tmp[1].Replace('_', ' ');
                            found = true;
                        }
                    }
                    if (!found)
                        v = ((InfoPage)types[index]).Text;
                    text_txt.Text = v;
                }
                //else
                //    System.Windows.Forms.MessageBox.Show("PATH DOESN'T EXIST --> CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT)" + Environment.NewLine + CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT));
            }
        }
    }
}
