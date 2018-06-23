/*
 * MyClassManager - C# Scripting
*/

protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
{
    InteractWithTextLanguage();
}

protected override void Save_translation_btn_Click(object sender, EventArgs e)
{
	InteractWithTextLanguage(true);
}

private void InteractWithTextLanguage(bool setValue = false)
{
    int index = CurrentTypeIndex - 1;
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
    }
}

