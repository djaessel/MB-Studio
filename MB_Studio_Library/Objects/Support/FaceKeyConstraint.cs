using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class FaceKeyConstraint
    {
        public FaceKeyConstraint(string[] raw_data)
        {
            Number = double.Parse(CodeReader.Repl_DotWComma(raw_data[0]));
            CompMode = int.Parse(raw_data[1]);

            ValuesINT = new int[int.Parse(raw_data[2])];
            ValuesDOUBLE = new double[ValuesINT.Length];

            for (int i = 0; i < ValuesINT.Length; i++)
            {
                ValuesDOUBLE[i] = double.Parse(CodeReader.Repl_DotWComma(raw_data[i * 2 + 4]));
                ValuesINT[i] = int.Parse(raw_data[i * 2 + 5]);
            }
        }

        public int CompMode { get; }

        public double Number { get; }

        public int[] ValuesINT { get; }

        public double[] ValuesDOUBLE { get; }

    }
}