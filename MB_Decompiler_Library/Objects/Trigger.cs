using importantLib;
using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Trigger : SimpleTrigger
    {
        private string delayInterval, rearmInterval;
        private string[] conditionBlock;

        public Trigger(string checkInterval, string delayInterval, string rearmInterval) : base(checkInterval, ObjectType.TRIGGER)
        {
            if (ImportantMethods.IsNumeric(delayInterval, true))
                this.delayInterval = ReplaceIntervalWithCode(double.Parse(CodeReader.Repl_DotWComma(delayInterval)));
            else
                this.delayInterval = delayInterval;
            if (ImportantMethods.IsNumeric(rearmInterval, true))
                this.rearmInterval = ReplaceIntervalWithCode(double.Parse(CodeReader.Repl_DotWComma(rearmInterval)));
            else
                this.rearmInterval = rearmInterval;
        }

        public string DelayInterval { get { return delayInterval; } }

        public string ReArmInterval { get { return rearmInterval; } }

        public string[] ConditionBlock
        {
            set { conditionBlock = value; }
            get { return conditionBlock; }
        }

    }
}
