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
    //int index = CurrentTypeIndex - 1;
    //if (index >= 0)
    //{
    //    // TODO: save or process language data
    //}
}

