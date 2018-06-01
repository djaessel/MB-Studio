using System;
using System.Collections.Generic;
using MB_Studio.Manager.Support.External;
using MB_Decompiler_Library.Objects;
using skillhunter;

namespace MB_Studio.Manager
{
    public class MyClassManager : ToolForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
				components.Dispose();

            base.Dispose(disposing);
        }

        private new void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
		
        public MyClassManager() : base(Skriptum.ObjectType.MyClass)
        {
            InitializeComponent();
			
			/SCRIPT MyClassManager
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
			
			/SCRIPT LoadSettingsAndLists
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
			//return new MyClass(raw_data);
			return new Skriptum(raw_data);
        }
		
        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
			/SCRIPT AddFromOtherMod
			
            //base.AddFromOtherMod(f);
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);
			
			/SCRIPT Language_cbb_SelectedIndexChanged
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            MyClass skriptum = (MyClass)type;

			/SCRIPT SetupType
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            base.Save_translation_btn_Click(sender, e);
			
			/SCRIPT Save_translation_btn_Click
        }

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = string.Empty;
			tmp += values[0].Split()[0] + ';';
            tmp += name_txt.Text.Replace(' ', '_') + ';';

			/SCRIPT SaveTypeByIndex
			
            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            MyClass c = new MyClass(valuesX);

            MB_Studio.SavePseudoCodeByType(c, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, changed);
        }
    }
}

