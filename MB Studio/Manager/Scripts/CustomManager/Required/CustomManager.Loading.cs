/*
 * MyClassManager - C# Scripting
*/

public MyClassManager()
{
	//InitializeComponent();//is always executed at first
}

protected override void ToolForm_Shown(object sender, EventArgs e)
{
	base.ToolForm_Shown(sender, e);
}

protected override void InitializeControls()
{
	base.InitializeControls();
}

protected override void LoadSettingsAndLists()
{
	base.LoadSettingsAndLists();
}

protected override Skriptum GetNewTypeFromClass(string[] raw_data)
{
	return MyClass(raw_data);
}

