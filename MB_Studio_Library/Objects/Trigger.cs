using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects
{
    public class Trigger : SimpleTrigger
    {
        public Trigger(string checkInterval, string delayInterval, string rearmInterval) : base(checkInterval, ObjectType.Trigger)
        {
            if (ImportantMethods.IsNumeric(delayInterval, true))
                DelayInterval = ReplaceIntervalWithCode(double.Parse(CodeReader.Repl_DotWComma(delayInterval)));
            else
                DelayInterval = delayInterval;
            if (ImportantMethods.IsNumeric(rearmInterval, true))
                ReArmInterval = ReplaceIntervalWithCode(double.Parse(CodeReader.Repl_DotWComma(rearmInterval)));
            else
                ReArmInterval = rearmInterval;
        }

        public string DelayInterval { get; }

        public string ReArmInterval { get; }

        public string[] ConditionBlock { set; get; }

    }
}
