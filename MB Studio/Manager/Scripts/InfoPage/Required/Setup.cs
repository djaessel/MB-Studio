/*
 * MyClassManager - C# Scripting
*/

protected override void ResetControls()
{
	text_txt.ResetText();
}

protected override void ResetGroupBox(GroupBox groupBox, List<string> exclude = null)
{
	
}

protected override void SetupType(Skriptum skriptum)
{
    text_txt.Text = skriptum.Text;

    Language_cbb_SelectedIndexChanged();
}

