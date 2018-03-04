using importantLib;
using skillhunter;
using MB_Decompiler_Library.IO;
using System.IO;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class ParticleSystem : Skriptum
    {
        private static List<HeaderVariable> headerVariables = null;

        private int particlesPerSecond;
        private ulong flagsGZ;
        private string meshName, flags;
        private double particleLife, damping, gravityStrength, turbulanceSize, turbulanceStrength, emitDirRandomness, particleRotationSpeed, particleRotationDamping;
        private double[] alphaKeys, redKeys, greenKeys, blueKeys, scaleKeys, emitBoxScale, emitVelocity;

        public ParticleSystem(List<string[]> raw_data) : base(raw_data[0][0].Split()[0], ObjectType.PARTICLE_SYSTEM)
        {
            if (headerVariables == null)
                headerVariables = GetHeaderVariableList();

            string[] sp = raw_data[0][0].Split();

            if (ImportantMethods.IsNumericGZ(sp[1]))
            {
                flagsGZ = ulong.Parse(sp[1]);
                SetFlags();
            }
            else
            {
                flags = sp[1];
                SetFlagsGZ();
            }

            meshName = sp[2];
            sp = raw_data[0][1].Split();

            particlesPerSecond = int.Parse(sp[0]);
            particleLife = double.Parse(CodeReader.Repl_DotWComma(sp[1]));
            damping = double.Parse(CodeReader.Repl_DotWComma(sp[2]));
            double d = double.Parse(CodeReader.Repl_DotWComma(sp[3]));
            if (sp[3].Substring(0, 1).Equals("-") && d == 0d)
                gravityStrength = -0.00000000001337; // for -0.0
            else
                gravityStrength = d;
            turbulanceSize = double.Parse(CodeReader.Repl_DotWComma(sp[4]));
            turbulanceStrength = double.Parse(CodeReader.Repl_DotWComma(sp[5]));

            alphaKeys = ConvertStringArrayIntoDouble(raw_data[1]);
            redKeys = ConvertStringArrayIntoDouble(raw_data[2]);
            greenKeys = ConvertStringArrayIntoDouble(raw_data[3]);
            blueKeys = ConvertStringArrayIntoDouble(raw_data[4]);
            scaleKeys = ConvertStringArrayIntoDouble(raw_data[5]);

            sp = raw_data[6][0].Split();
            emitBoxScale = ConvertStringArrayIntoDouble(sp);
            sp = raw_data[6][1].Split();
            emitVelocity = ConvertStringArrayIntoDouble(sp);
            emitDirRandomness = double.Parse(CodeReader.Repl_DotWComma(raw_data[6][2]));

            if (!raw_data[7][0].Trim().Equals("0.0 0.0"))
            {
                sp = raw_data[7][0].Split();
                particleRotationSpeed = double.Parse(CodeReader.Repl_DotWComma(sp[0]));
                particleRotationDamping = double.Parse(CodeReader.Repl_DotWComma(sp[1]));
            }
            else
            {
                particleRotationSpeed = double.NaN;
                particleRotationDamping = double.NaN;
            }
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private void SetFlagsGZ()
        {
            string flags = string.Empty;
            foreach (HeaderVariable headerVar in headerVariables)
            {
                ulong x = ulong.Parse(headerVar.VariableValue);
                if ((x & flagsGZ) == x)
                    flags += headerVar.VariableName + '|';
            }

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = flagsGZ.ToString();

            this.flags = flags;
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private void SetFlags()
        {
            ulong flagsGZ = 0;
            string[] sp = flags.Split('|');
            foreach (HeaderVariable headerVar in headerVariables)
                foreach (string flag in sp)
                    if (headerVar.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(headerVar.VariableValue);
            this.flagsGZ = flagsGZ;
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

                        s = SkillHunter.Hex2Dec(tmp[1].Substring(2).TrimStart('0')).ToString();

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

        public ulong Flags { get { return flagsGZ; } }

        public string MeshName { get { return meshName; } }

        public int ParticlesPerSecond { get { return particlesPerSecond; } }

        public double ParticleLifeTime { get { return particleLife; } }

        public double Damping { get { return damping; } }

        public double GravityStrength { get { return gravityStrength; } }

        public double TurbulanceSize { get { return turbulanceSize; } }

        public double TurbulanceStrength { get { return turbulanceStrength; } }

        public double EmitDirectionRandomness { get { return emitDirRandomness; } }

        public double ParticleRotationSpeed { get { return particleRotationSpeed; } }

        public double ParticleRotationDamping { get { return particleRotationDamping; } }

        public double[] AlphaKeys { get { return alphaKeys; } }

        public double[] RedKeys { get { return redKeys; } }

        public double[] GreenKeys { get { return greenKeys; } }

        public double[] BlueKeys { get { return blueKeys; } }

        public double[] ScaleKeys { get { return scaleKeys; } }

        public double[] EmitBoxScale { get { return emitBoxScale; } }

        public double[] EmitVelocity { get { return emitVelocity; } }

    }
}