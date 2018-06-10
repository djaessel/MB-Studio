using System.IO;
using System.Collections.Generic;
using importantLib;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;

namespace MB_Decompiler_Library.Objects
{
    public class ParticleSystem : Skriptum
    {
        private static List<HeaderVariable> headerVariables = null;

        public ParticleSystem(List<string[]> raw_data) : base(raw_data[0][0].Split()[0], ObjectType.PARTICLE_SYSTEM)
        {
            if (headerVariables == null)
                headerVariables = GetHeaderVariableList();

            string[] sp = raw_data[0][0].Split();

            if (ImportantMethods.IsNumericGZ(sp[1]))
            {
                FlagsGZ = ulong.Parse(sp[1]);
                SetFlags();
            }
            else
            {
                Flags = sp[1];
                SetFlagsGZ();
            }

            MeshName = sp[2];
            sp = raw_data[0][1].Split();

            ParticlesPerSecond = int.Parse(sp[0]);
            ParticleLifeTime = double.Parse(CodeReader.Repl_DotWComma(sp[1]));
            Damping = double.Parse(CodeReader.Repl_DotWComma(sp[2]));
            double d = double.Parse(CodeReader.Repl_DotWComma(sp[3]));
            if (sp[3].Substring(0, 1).Equals("-") && d == 0d)
                GravityStrength = -0.00000000001337; // for -0.0
            else
                GravityStrength = d;
            TurbulanceSize = double.Parse(CodeReader.Repl_DotWComma(sp[4]));
            TurbulanceStrength = double.Parse(CodeReader.Repl_DotWComma(sp[5]));

            AlphaKeys = ConvertStringArrayIntoDouble(raw_data[1]);
            RedKeys = ConvertStringArrayIntoDouble(raw_data[2]);
            GreenKeys = ConvertStringArrayIntoDouble(raw_data[3]);
            BlueKeys = ConvertStringArrayIntoDouble(raw_data[4]);
            ScaleKeys = ConvertStringArrayIntoDouble(raw_data[5]);

            sp = raw_data[6][0].Split();
            EmitBoxScale = ConvertStringArrayIntoDouble(sp);
            sp = raw_data[6][1].Split();
            EmitVelocity = ConvertStringArrayIntoDouble(sp);
            EmitDirectionRandomness = double.Parse(CodeReader.Repl_DotWComma(raw_data[6][2]));

            if (!raw_data[7][0].Trim().Equals("0.0 0.0"))
            {
                sp = raw_data[7][0].Split();
                ParticleRotationSpeed = double.Parse(CodeReader.Repl_DotWComma(sp[0]));
                ParticleRotationDamping = double.Parse(CodeReader.Repl_DotWComma(sp[1]));
            }
            else
            {
                ParticleRotationSpeed = double.NaN;
                ParticleRotationDamping = double.NaN;
            }
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private void SetFlagsGZ()
        {
            string flags = string.Empty;
            foreach (HeaderVariable headerVar in headerVariables)
            {
                ulong x = ulong.Parse(headerVar.VariableValue);
                if ((x & FlagsGZ) == x)
                    flags += headerVar.VariableName + '|';
            }

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = FlagsGZ.ToString();

            this.Flags = flags;
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private void SetFlags()
        {
            ulong flagsGZ = 0;
            string[] sp = Flags.Split('|');
            foreach (HeaderVariable headerVar in headerVariables)
                foreach (string flag in sp)
                    if (headerVar.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(headerVar.VariableValue);
            this.FlagsGZ = flagsGZ;
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private List<HeaderVariable> GetHeaderVariableList(string file = "header_particle_systems.py")
        {
            string[] tmp;
            string s = string.Empty;
            List<HeaderVariable> list = new List<HeaderVariable>();
            using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + "moduleSystem\\" + file))
            {
                while (!sr.EndOfStream && !s.Equals(string.Empty))
                {
                    s = sr.ReadLine().Split('#')[0].Trim();
                    if (s.Length != 0 && s.Contains("="))
                    {
                        tmp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');

                        s = HexConverter.Hex2Dec(tmp[1].Substring(2).TrimStart('0')).ToString();

                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].VariableValue.Equals(s))
                            {
                                list.RemoveAt(i);//if no conflict remove comment
                                i = list.Count;
                            }
                        }

                        list.Add(new HeaderVariable(s, tmp[0]));
                    }
                }
            }
            return list;
        }

        private static double[] ConvertStringArrayIntoDouble(string[] s_array)
        {
            double[] dd = new double[s_array.Length];
            for (int i = 0; i < s_array.Length; i++)
                dd[i] = double.Parse(CodeReader.Repl_DotWComma(s_array[i]));
            return dd;
        }

        public ulong FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public string MeshName { get; }

        public int ParticlesPerSecond { get; }

        public double ParticleLifeTime { get; }

        public double Damping { get; }

        public double GravityStrength { get; }

        public double TurbulanceSize { get; }

        public double TurbulanceStrength { get; }

        public double EmitDirectionRandomness { get; }

        public double ParticleRotationSpeed { get; }

        public double ParticleRotationDamping { get; }

        public double[] AlphaKeys { get; }

        public double[] RedKeys { get; }

        public double[] GreenKeys { get; }

        public double[] BlueKeys { get; }

        public double[] ScaleKeys { get; }

        public double[] EmitBoxScale { get; }

        public double[] EmitVelocity { get; }

    }
}