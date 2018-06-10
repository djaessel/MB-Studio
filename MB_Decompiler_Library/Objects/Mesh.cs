using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Mesh : Skriptum
    {
        private double[] manipulations = new double[9];

        public Mesh(string[] raw_data) : base(raw_data[0], ObjectType.MESH)
        {
            Flags = raw_data[1];
            ResourceName = raw_data[2];
            for (int i = 0; i < manipulations.Length; i++)
                manipulations[i] = double.Parse(CodeReader.Repl_DotWComma(raw_data[i + 3]));
        }

        public string Flags { get; }

        public string ResourceName { get; }//change to Name if good idea later

        public double[] AxisTranslation { get { return new double[] { manipulations[0], manipulations[1], manipulations[2] }; } }

        public double[] RotationAngle { get { return new double[] { manipulations[3], manipulations[4], manipulations[5] }; } }

        public double[] Scale { get { return new double[] { manipulations[6], manipulations[7], manipulations[8] }; } }

    }
}