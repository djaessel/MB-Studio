using MB_Studio_Library.Objects;
using MB_Studio.Manager.Support.External;
using System;
using System.Collections.Generic;
using MB_Studio_Library.IO;
using System.IO;

namespace MB_Studio.Manager
{
    public partial class SoundManager : ToolForm
    {
        private static string SoundFolderPath { get; set; } = null;

        public SoundManager() : base(Skriptum.ObjectType.Sound)
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
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();

            openFile_dialog.InitialDirectory = CodeReader.ModPath;
            SoundFolderPath = CodeReader.ModPath + "\\Sounds\\";
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Sound sound = (Sound)type;

            #region Flags

            sound2D_cb.Checked = (sound.FlagsGZ & 0x00000001) == 0x00000001;
            looping_cb.Checked = (sound.FlagsGZ & 0x00000002) == 0x00000002;
            randomStartPosition_cb.Checked = (sound.FlagsGZ & 0x00000004) == 0x00000004;
            streamFromHD_cb.Checked = (sound.FlagsGZ & 0x00000008) == 0x00000008;
            alwaysSendViaNetwork_cb.Checked = (sound.FlagsGZ & 0x00100000) == 0x00100000;

            priority_tb.Value = (int)(sound.FlagsGZ & 0x000000F0) >> 4;
            volume_tb.Value = (int)(sound.FlagsGZ & 0x00000F00) >> 8;

            #endregion

            #region Soundfiles

            foreach (var soundFile in sound.SoundFiles)
                soundFiles_lb.Items.Add(soundFile.Name);

            #endregion
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0] + ';';
            tmp += values[1] + ';';
            tmp += GetFlags() + ";";

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            Sound s = new Sound(valuesX);

            CodeWriter.SavePseudoCodeByType(s, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }

        private uint GetFlags()
        {
            uint flags = 0;

            if (sound2D_cb.Checked)
                flags |= 0x00000001;

            if (looping_cb.Checked)
                flags |= 0x00000002;

            if (randomStartPosition_cb.Checked)
                flags |= 0x00000004;

            if (streamFromHD_cb.Checked)
                flags |= 0x00000008;

            if (alwaysSendViaNetwork_cb.Checked)
                flags |= 0x00100000;

            flags |= (uint)priority_tb.Value << 4;
            flags |= (uint)volume_tb.Value << 8;

            return flags;
        }

        private void AddSound_btn_Click(object sender, EventArgs e)
        {
            openFile_dialog.ShowDialog();
        }

        private void OpenFile_dialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] filePaths = openFile_dialog.FileNames;
            string[] fileNames = openFile_dialog.SafeFileNames;
            for (int i = 0; i < filePaths.Length; i++)
            {
                File.Copy(filePaths[i], SoundFolderPath + fileNames[i]);
            }
            soundFiles_lb.Items.AddRange(fileNames);
        }

        private void RemoveSound_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = soundFiles_lb.SelectedIndex;
            string filePath = SoundFolderPath + soundFiles_lb.Items[selectedIndex].ToString();
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private void MoveUpSound_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = soundFiles_lb.SelectedIndex;
            if (selectedIndex > 0)
            {
                MoveSoundFileListItem(selectedIndex, true);
            }
        }

        private void MoveDownSound_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = soundFiles_lb.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < soundFiles_lb.Items.Count - 1)
            {
                MoveSoundFileListItem(selectedIndex, false);
            }
        }

        private void MoveSoundFileListItem(int index, bool up)
        {
            int adder = 1;
            if (up)
                adder *= -1;

            string tmp = soundFiles_lb.Items[index + adder].ToString();
            soundFiles_lb.Items[index + adder] = soundFiles_lb.Items[index].ToString();
            soundFiles_lb.Items[index] = tmp;
        }
    }
}
