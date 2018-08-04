using System;
using System.Collections.Generic;
using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio.Main
{
    public partial class ImportsManagerGUI : SpecialForm
    {
        private DataBankList[] dataBanks;
        private ImportsManager impManager = new ImportsManager(CodeReader.FILES_PATH);

        public ImportsManagerGUI()
        {
            InitializeComponent();
        }

        private void ImportsManagerGUI_Load(object sender, EventArgs e)
        {
            LoadDataBank();
            importsData_lb.SelectedIndex = 0;
        }

        private void LoadDataBank(DataBankList[] dataBankX = null)
        {
            importsData_lb.Items.Clear();
            if (dataBankX == null)
                dataBanks = impManager.ReadDataBankInfos();
            else
                dataBanks = dataBankX;
            foreach (DataBankList item in dataBanks)
                importsData_lb.Items.Add(item.Kind);
        }

        private void ImportsData_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            imports_lb.Items.Clear();
            newImports_lb.Items.Clear();
            addNewImport_txt.ResetText();
            description_txt.ResetText();
            code_txt.ResetText();

            string descriptionTXT = string.Empty;
            string codeTXT = string.Empty;
            DataBankList currentDataBank = dataBanks[importsData_lb.SelectedIndex];

            for (int i = 0; i < currentDataBank.Imports.Length; i++)
                imports_lb.Items.Add(currentDataBank.Imports[i]);

            for (int i = 0; i < currentDataBank.DescriptionLines.Length; i++)
                descriptionTXT += currentDataBank.DescriptionLines[i] + Environment.NewLine;

            for (int i = 0; i < currentDataBank.CodeLines.Length; i++)
                codeTXT += currentDataBank.CodeLines[i] + Environment.NewLine;

            description_txt.Text = descriptionTXT;
            code_txt.Text = codeTXT;

            importsCountX_lbl.Text = currentDataBank.Imports.Length.ToString();
            availableImportsCountX_lbl.Text = "0"; // change later with header / imports folder active
            descriptionCountX_lbl.Text = currentDataBank.DescriptionLines.Length.ToString();
            codeCountX_lbl.Text = currentDataBank.CodeLines.Length.ToString();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            List<List<string>> infos = new List<List<string>>();

            for (int i = 3; i != 0; i--)
                infos.Add(new List<string>());

            foreach (string import in imports_lb.Items)
                infos[0].Add(import);

            string[] split = description_txt.Text.Replace(Environment.NewLine, "\n").TrimEnd('\n').Split('\n');
            if (split[0].Length > 0)
                foreach (string descriptionLine in split)
                    infos[1].Add(descriptionLine.TrimStart('#'));

            split = code_txt.Text.Replace(Environment.NewLine, "\n").TrimEnd('\n').Split('\n');
            if (split[0].Length > 0)
                foreach (string codeLine in split)
                    infos[2].Add(codeLine);

            impManager.WriteDataBankInfos(dataBanks[importsData_lb.SelectedIndex].ObjectType, infos);
            LoadDataBank(impManager.ReadDataBankInfos());
        }

        private void Abort_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteCurImp_btn_Click(object sender, EventArgs e)
        {
            List<int> indices = new List<int>();
            foreach (int impIdx in imports_lb.SelectedIndices)
                indices.Add(impIdx);
            indices.Reverse();
            foreach (int impIdx in indices)
            {
                newImports_lb.Items.Add(imports_lb.Items[impIdx]);
                imports_lb.Items.RemoveAt(impIdx);
            }
        }

        private void AddNewImports_btn_Click(object sender, EventArgs e)
        {
            if (newImports_lb.SelectedIndices.Count > 0)
            {
                List<int> indices = new List<int>();
                foreach (int impIdx in newImports_lb.SelectedIndices)
                    indices.Add(impIdx);
                indices.Reverse();
                foreach (int impIdx in indices)
                {
                    imports_lb.Items.Add(newImports_lb.Items[impIdx]);
                    newImports_lb.Items.RemoveAt(impIdx);
                }
            }
            else
                imports_lb.Items.Add(addNewImport_txt.Text);
        }

        private void CancelImpListBox_btn_Click(object sender, EventArgs e)
        {
            imports_lb.ClearSelected();
        }

        private void CancelNewImports_btn_Click(object sender, EventArgs e)
        {
            newImports_lb.ClearSelected();
        }
    }
}
