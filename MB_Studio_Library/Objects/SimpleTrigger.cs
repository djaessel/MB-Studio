using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects.Support;
using System.IO;
using System.Collections.Generic;

namespace MB_Studio_Library.Objects
{
    public class SimpleTrigger : Skriptum
    {
        protected static List<IntervalCode> intervalCodes = new List<IntervalCode>();

        public SimpleTrigger(string checkInterval, ObjectType objectType = ObjectType.SimpleTrigger) : base("SIMPLE_TRIGGER", objectType)
        {
            if (intervalCodes.Count == 0)
                InitializeIntervalCodes();
            if (ImportantMethods.IsNumeric(checkInterval, true))
                CheckInterval = ReplaceIntervalWithCode(double.Parse(CodeReader.Repl_DotWComma(checkInterval)));
            else
                CheckInterval = checkInterval;
        }

        public string CheckInterval { get; }

        public string[] ConsequencesBlock { set; get; }

        protected static string ReplaceIntervalWithCode(double interval)
        {
            string ret = string.Empty;
            int tmp = (int)interval;
            for (int i = 0; i < intervalCodes.Count; i++)
            {
                if (tmp == intervalCodes[i].Value)
                {
                    ret = intervalCodes[i].Code;
                    i = intervalCodes.Count;
                }
            }
            if (ret.Length == 0/* && tmp >= 0 && tmp < 100000000*/)
                ret = interval.ToString().Replace(',', '.');
            return ret;
        }

        private static void InitializeIntervalCodes()
        {
            string line;
            string[] split;
            using (StreamReader sr = new StreamReader(CodeReader.FILES_PATH + "header_triggers.py"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split('#')[0].Replace(" ", string.Empty).Replace("\t", string.Empty);
                    if (line.Length >= 8 && line.Contains("=") && line.Contains("ti_"))
                    {
                        split = line.Split('=');
                        if (ImportantMethods.IsNumeric(split[1].Split('.')[0].Replace("-",string.Empty), true))
                            intervalCodes.Add(new IntervalCode(split[0], (int)double.Parse(split[1].Replace(".",","))));
                    }
                }
            }
        }

    }
}
