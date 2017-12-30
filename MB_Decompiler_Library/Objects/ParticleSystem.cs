using MB_Decompiler_Library.IO;
using skillhunter;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class ParticleSystem : Skriptum
    {
        private int particlesPerSecond;
        private ulong flags;
        private string meshName;
        private double particleLife, damping, gravityStrength, turbulanceSize, turbulanceStrength, emitDirRandomness, particleRotationSpeed, particleRotationDamping;
        private double[] alphaKeys, redKeys, greenKeys, blueKeys, scaleKeys, emitBoxScale, emitVelocity;

        public ParticleSystem(List<string[]> raw_data) : base(raw_data[0][0].Split()[0], ObjectType.PARTICLE_SYSTEM)
        {
            string[] sp = raw_data[0][0].Split();

            flags = ulong.Parse(sp[1]);
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

        private static double[] ConvertStringArrayIntoDouble(string[] s_array)
        {
            double[] dd = new double[s_array.Length];
            for (int i = 0; i < s_array.Length; i++)
                dd[i] = double.Parse(CodeReader.Repl_DotWComma(s_array[i]));
            return dd;
        }

        public ulong Flags { get { return flags; } }

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