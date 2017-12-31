using importantLib;
using System;
using static skillhunter.Skriptum;

namespace MB_Studio
{
    public partial class TriggerSelector : SpecialFormBlack
    {
        private ObjectType type;
        private string selectedTrigger;

        private string[] selection;
        private double[] selectionValues;

        private static readonly string[] item_trigger = new string[] { "ti_on_init_item", "ti_on_weapon_attack", "ti_on_missile_hit", "ti_on_shield_hit" };
        private static readonly double[] item_check_interval = new double[] { -50d, -51d, -52d, -80d };

        public TriggerSelector()
        {
            type = ObjectType.SCRIPT;
            Init();
        }

        public TriggerSelector(ObjectType type)
        {
            this.type = type;
            Init();
        }

        public string[] TriggerNames { get { return selection; } }
        public double[] TriggerCheckIntervals { get { return selectionValues; } }

        private void Init()
        {
            if (type == ObjectType.ITEM)
            {
                selection = item_trigger;
                selectionValues = item_check_interval;
            }
            else
            {
                selection = new string[0];
                selectionValues = new double[0];
            }
            InitializeComponent();
        }

        private void TriggerSelector_Load(object sender, EventArgs e)
        {
            if (selection.Length != 0)
            {
                foreach (string t in selection)
                    trigger_cbb.Items.Add(ImportantMethods.ToUpperAfterBlank(t.Substring(t.IndexOf('_') + 1).Replace('_', ' ')));
                trigger_cbb.SelectedIndex = 0;
            }
        }

        public string SelectedTrigger
        {
            get
            {
                string ret = string.Empty;
                if (trigger_cbb.SelectedIndex >= 0)
                    ret = trigger_cbb.SelectedItem.ToString();
                return ret;
            }
        }

        public double SelectedCheckInterval
        {
            get
            {
                double ret = 0d;
                int idx = trigger_cbb.SelectedIndex;
                if (idx >= 0)
                    ret = item_check_interval[idx];
                return ret;
            }
        }

        private void AddTrigger_btn_Click(object sender, EventArgs e)
        {
            if (trigger_cbb.SelectedIndex >= 0)
                selectedTrigger = trigger_cbb.SelectedItem.ToString();
            Close();
        }
    }
}
