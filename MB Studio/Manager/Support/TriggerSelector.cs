using importantLib;
using System;
using static MB_Decompiler_Library.Objects.Skriptum;

namespace MB_Studio
{
    public partial class TriggerSelector : SpecialFormBlack
    {
        private ObjectType type;
        private string selectedTrigger;
        private static readonly string[] item_trigger = new string[] { "ti_on_init_item", "ti_on_weapon_attack", "ti_on_missile_hit", "ti_on_shield_hit" };
        private static readonly double[] item_check_interval = new double[] { -50d, -51d, -52d, -80d };

        public TriggerSelector()
        {
            type = ObjectType.Script;
            Init();
        }

        public TriggerSelector(ObjectType type)
        {
            this.type = type;
            Init();
        }

        public string[] TriggerNames { get; private set; }
        public double[] TriggerCheckIntervals { get; private set; }

        private void Init()
        {
            if (type == ObjectType.Item)
            {
                TriggerNames = item_trigger;
                TriggerCheckIntervals = item_check_interval;
            }
            else
            {
                TriggerNames = new string[0];
                TriggerCheckIntervals = new double[0];
            }
            InitializeComponent();
        }

        private void TriggerSelector_Load(object sender, EventArgs e)
        {
            if (TriggerNames.Length != 0)
            {
                foreach (string t in TriggerNames)
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
