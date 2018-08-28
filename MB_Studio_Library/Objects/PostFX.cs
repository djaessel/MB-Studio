using importantLib;
using System.Collections.Generic;

namespace MB_Studio_Library.Objects
{
    public class PostFX : Skriptum
    {
        public PostFX(string raw_data) : base(raw_data.Split()[0], ObjectType.PostFX)
        {
            raw_data = raw_data.Trim();
            while (raw_data.Contains("  "))
                raw_data = raw_data.Replace("  ", " ");

            string[] tmpS = raw_data.Split();

            if (ImportantMethods.IsNumericGZ(tmpS[1]))
            {
                FlagsGZ = ulong.Parse(tmpS[1]);
                if ((FlagsGZ & 0x1) == 1)
                    Flags = "fxf_highhdr";//change if more than one flag
            }
            else
            {
                Flags = tmpS[1];
                FlagsGZ = 0;
                if (Flags.Equals("fxf_highhdr"))//change if more than one flag
                    FlagsGZ |= 0x1;
            }

            TonemapOperatorType = ulong.Parse(tmpS[2]);

            AddValueFromIndexToShaderParameters123(tmpS, 3, 6);
            AddValueFromIndexToShaderParameters123(tmpS, 7, 10);
            AddValueFromIndexToShaderParameters123(tmpS, 11);
        }

        private void AddValueFromIndexToShaderParameters123(string[] values, int index1 = 0, int index2 = -1)
        {
            List<float> valuesX = new List<float>();
            if (index2 < 0)
                index2 = values.Length;
            else
                index2++;
            for (int i = index1; i < index2; i++)
                valuesX.Add(float.Parse(values[i]));
            AllShaderParameters.Add(valuesX.ToArray());
        }

        public ulong FlagsGZ { get; }

        public string Flags { get; }

        public ulong TonemapOperatorType { get; }

        public float[] ShaderParameter1 { get { return AllShaderParameters[0]; } }

        public float[] ShaderParameter2 { get { return AllShaderParameters[1]; } }

        public float[] ShaderParameter3 { get { return AllShaderParameters[2]; } }

        public List<float[]> AllShaderParameters { get; } = new List<float[]>();

    }
}