using importantLib;
using MB_Decompiler;
using MB_Decompiler_Library.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MB_Decompiler_GUI
{
    public partial class DecompilerGUI : SpecialForm
    {
        private const string ALL_FILES = "ALL FILES";
        private const string NO_FILES = "NO_FILES";
        private const string FILES_PROCESSED = "FILES PROCESSED: ";

        private bool useAllFiles = false;
        private List<int> fileIndicies = new List<int>();

        public DecompilerGUI()
        {
            InitializeComponent();
            listView.ItemChecked += ListView_ItemChecked;
            useAll_btn.TextChanged += UseAll_btn_TextChanged;
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
        }

        private void UseAll_btn_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = !checkBox1.Enabled;
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!useAllFiles)
            {
                RefreshFileIndices();
                if (listView.CheckedItems.Count < listView.Items.Count)
                    useAll_btn.Text = ALL_FILES;
                else
                    useAll_btn.Text = NO_FILES;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            title_lbl.Text = Text;
            ProgramConsole.Initialize(); // null - change this later if you want!!!
            foreach (string item in CodeReader.Files)
                listView.Items.Add(item);
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProcessSelected_btn_Click(object sender, EventArgs e)
        {
            string infoText = string.Empty;
            if (IsAllInfoActive) // means all files are used
            {
                for (int i = 0; i < fileIndicies.Count; i++)
                {
                    int index = fileIndicies[i];
                    CodeReader.Reset();
                    CodeReader cr = new CodeReader(CodeReader.ModPath + CodeReader.Files[index]);
                    SourceWriter sw = new SourceWriter();
                    if (sw.WriteObjectType(cr.ReadObjectType(index), index))
                        infoText += FILES_PROCESSED.Remove(4, 1) + listView.Items[index].Text;
                    else
                        infoText += "ERROR! - 0x1 - CONTACT SOFTWARE PRODUCER";
                    if (i < fileIndicies.Count - 1)
                        infoText += GetCodeReaderInfo() + Environment.NewLine;
                }
            }
            else
                infoText = FILES_PROCESSED + SourceWriter.WriteAllObjects();
            infoText += GetCodeReaderInfo();
            if (IsAllInfoActive)
                infoText += Environment.NewLine + FILES_PROCESSED + fileIndicies.Count;
            readInfo_txt.Text = infoText;
        }

        private bool IsAllInfoActive { get { return (!useAll_btn.Text.Equals(NO_FILES) || checkBox1.Checked); } }

        private string GetCodeReaderInfo()
        {
            return " - OVERFLOW: " + CodeReader.Overflow + " - OBJECTS READ: " + CodeReader.ObjectsRead;
        }

        private void UseAll_btn_Click(object sender, EventArgs e)
        {
            bool _checked = false;

            if (useAll_btn.Text.Equals(ALL_FILES))
            {
                _checked = !_checked;
                useAll_btn.Text = NO_FILES;
            }
            else
                useAll_btn.Text = ALL_FILES;

            useAllFiles = !useAllFiles;
            listView.SuspendLayout();
            foreach (ListViewItem item in listView.Items)
                item.Checked = _checked;
            listView.ResumeLayout();
            useAllFiles = !useAllFiles;

            RefreshFileIndices();
        }

        private void RefreshFileIndices()
        {
            if (fileIndicies.Count > 0)
                fileIndicies.Clear();
            foreach (ListViewItem item in listView.CheckedItems)
                fileIndicies.Add(listView.Items.IndexOf(item));
        }

    }
}
