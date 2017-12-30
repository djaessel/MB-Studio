namespace MB_Decompiler_Library.Objects
{
    public class Trigger : SimpleTrigger
    {
        private string delay_Interval, re_arm_Interval;
        private string[] conditionBlock;

        public Trigger(double check_Interval, double delay_Interval, double re_arm_Interval) : base(check_Interval, ObjectType.TRIGGER)
        {
            this.delay_Interval = ReplaceIntervalWithCode(delay_Interval);
            this.re_arm_Interval = ReplaceIntervalWithCode(re_arm_Interval);
        }

        public string DelayInterval { get { return delay_Interval; } }

        public string ReArmInterval { get { return re_arm_Interval; } }

        public string[] ConditionBlock
        {
            set { conditionBlock = value; }
            get { return conditionBlock; }
        }

    }
}
