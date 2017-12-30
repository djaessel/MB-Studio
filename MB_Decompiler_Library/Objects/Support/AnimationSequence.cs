using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects.Support
{
    public class AnimationSequence
    {
        private double duration;
        private double[] lastNumbersDOUBLE = new double[4];
        private int beginFrame, endFrame;
        private ulong flags, lastNumberINT;
        private string resourceName;

        public AnimationSequence(string[] raw_data)
        {
            duration = double.Parse(CodeReader.Repl_DotWComma(raw_data[0]));
            resourceName = raw_data[1];
            beginFrame = int.Parse(raw_data[2]);
            endFrame = int.Parse(raw_data[3]);
            flags = ulong.Parse(raw_data[4]);
            lastNumberINT = ulong.Parse(raw_data[5]);
            string tmp;
            for (int i = 0; i < lastNumbersDOUBLE.Length; i++)
            {
                tmp = CodeReader.Repl_DotWComma(raw_data[i + 6]);
                if (tmp.Length > 3)
                {
                    lastNumbersDOUBLE[i] = double.Parse(tmp);
                    if (tmp.Contains("-") && lastNumbersDOUBLE[i] == 0d)
                        lastNumbersDOUBLE[i] = -0.000001;
                }
                else
                    lastNumbersDOUBLE[i] = double.NaN;
            }
        }

        public double Duration { get { return duration; } }

        public int BeginFrame { get { return beginFrame; } }

        public int EndFrame { get { return endFrame; } }

        public ulong Flags { get { return flags; } }

        public string ResourceName { get { return resourceName; } }

        public ulong LastNumberGZ { get { return lastNumberINT; } }
        
        public double[] LastNumbersFKZ { get { return lastNumbersDOUBLE; } }

    }
}