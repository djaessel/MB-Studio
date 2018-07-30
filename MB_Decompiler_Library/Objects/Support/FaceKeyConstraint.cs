using MB_Studio_Library.IO;
using System.Windows.Forms;

namespace MB_Studio_Library.Objects.Support
{
    public class FaceKeyConstraint
    {
        private int compMode;
        private double number;
        private int[] valuesINT;
        private double[] valuesDOUBLE;

        public FaceKeyConstraint(string[] raw_data)
        {
            number = double.Parse(CodeReader.Repl_DotWComma(raw_data[0]));
            compMode = int.Parse(raw_data[1]);

            valuesINT = new int[int.Parse(raw_data[2])];
            valuesDOUBLE = new double[valuesINT.Length];

            for (int i = 0; i < valuesINT.Length; i++)
            {
                valuesDOUBLE[i] = double.Parse(CodeReader.Repl_DotWComma(raw_data[i * 2 + 4]));
                valuesINT[i] = int.Parse(raw_data[i * 2 + 5]);
            }
        }

        public int CompMode { get { return compMode; } }

        public double Number { get { return number; } }

        public int[] ValuesINT { get { return valuesINT; } }

        public double[] ValuesDOUBLE { get { return valuesDOUBLE; } }

    }
}