using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects.Support;
using System.Collections.Generic;

namespace MB_Studio_Library.Objects
{
    public class Skin : Skriptum
    {
        public static string[] SoundKinds { get; } = { "voice_die", "voice_hit", "voice_grunt", "voice_grunt_long", "voice_yell", "voice_warcry", "voice_victory", "voice_stun" };
        public static string[] SkinFlags { get; } = { "0", "skf_use_morph_key_10", "skf_use_morph_key_20", "skf_use_morph_key_30", "skf_use_morph_key_40", "skf_use_morph_key_50", "skf_use_morph_key_60", "skf_use_morph_key_70" };

        public Skin(List<string[]> raw_data) : base(raw_data[0][0], ObjectType.Skin)
        {
            Flags = ulong.Parse(raw_data[0][1]);
            BodyMesh = raw_data[1][0];
            CalfMesh = raw_data[1][1];
            HandMesh = raw_data[1][2];
            HeadMesh = raw_data[2][0];
            SetFaceKeys(raw_data[2]);
            HairMeshes = raw_data[3];
            if (HairMeshes.Length == 1 && HairMeshes[0].Trim().Equals(string.Empty))
                HairMeshes = new string[0];
            BeardMeshes = raw_data[4];
            if (BeardMeshes.Length == 1 && BeardMeshes[0].Trim().Equals(string.Empty))
                BeardMeshes = new string[0];
            HairTextures = new string[int.Parse(raw_data[5][0])];
            for (int i = 0; i < HairTextures.Length; i++)
                HairTextures[i] = raw_data[5][i + 1];
            BeardTextures = new string[int.Parse(raw_data[6][0])];
            for (int i = 0; i < BeardTextures.Length; i++)
                BeardTextures[i] = raw_data[6][i + 1];
            SetFaceTextures(raw_data[7]);
            SetVoices(raw_data[8]);
            SkeletonName = raw_data[9][0];
            Scale = double.Parse(CodeReader.Repl_DotWComma(raw_data[9][1]));
            BloodParticle1GZ = int.Parse(raw_data[10][0]);
            BloodParticle2GZ = int.Parse(raw_data[10][1]);
            BloodParticle1 = CodeReader.ParticleSystems[BloodParticle1GZ];
            BloodParticle2 = CodeReader.ParticleSystems[BloodParticle2GZ];
            int fkcCount = 0;
            if (ImportantMethods.IsNumericFKZ2(raw_data[11][0]))
            {
                fkcCount = int.Parse(raw_data[11][0]);
                if (fkcCount > 0)
                {
                    FaceKeyConstraints = new FaceKeyConstraint[int.Parse(raw_data[11][0])];
                    for (int i = 0; i < FaceKeyConstraints.Length; i++)
                        FaceKeyConstraints[i] = new FaceKeyConstraint(raw_data[12 + i]); //raw_data[11 + i]
                }
            }
            if (fkcCount <= 0)
                FaceKeyConstraints = new FaceKeyConstraint[0];
        }

        #region Properties

        public ulong Flags { get; }

        public string BodyMesh { get; }

        public string CalfMesh { get; }

        public string HandMesh { get; }

        public string HeadMesh { get; }

        public string SkeletonName { get; }

        public int BloodParticle1GZ { get; }

        public int BloodParticle2GZ { get; }

        public string BloodParticle1 { get; }

        public string BloodParticle2 { get; }

        public FaceKey[] FaceKeys { get; private set; }

        public FaceKeyConstraint[] FaceKeyConstraints { get; }

        public FaceTexture[] FaceTextures { get; private set; }

        public Variable[] Voices { get; private set; }

        public string[] HairMeshes { get; }

        public string[] BeardMeshes { get; }

        public string[] HairTextures { get; }

        public string[] BeardTextures { get; }

        public double Scale { get; }

        #endregion

        #region Methods

        private void SetFaceKeys(string[] s)
        {
            string t1, t2;
            FaceKeys = new FaceKey[int.Parse(s[1])];
            for (int i = 0; i < FaceKeys.Length; i++)
            {
                t1 = CodeReader.Repl_DotWComma(s[i * 6 + 5]);
                t2 = CodeReader.Repl_DotWComma(s[i * 6 + 6]);
                double d1 = double.Parse(t1);
                double d2 = double.Parse(t2);
                if (t1.Substring(0, 1).Equals("-") && d1 == 0d)
                    t1 = CodeReader.MINUS_ZERO;
                if (t2.Substring(0, 1).Equals("-") && d2 == 0d)
                    t2 = CodeReader.MINUS_ZERO;
                FaceKeys[i] = new FaceKey(
                    s[i * 6 + 2].Substring(s[i * 6 + 2].IndexOf('_') + 1),
                    int.Parse(s[i * 6 + 3]),
                    int.Parse(s[i * 6 + 4]),
                    double.Parse(t1),
                    double.Parse(t2),
                    s[i * 6 + 7]);
            }
        }

        private void SetFaceTextures(string[] s)
        {
            int x, y = 0;
            FaceTextures = new FaceTexture[int.Parse(s[0])];
            s = CodeReader.GetStringArrayStartFromIndex(s, 1);
            for (int i = 0; i < FaceTextures.Length; i++)
            {
                string[] textures = new string[int.Parse(s[y + 2])];
                x = 0;
                for (int j = 0; j < textures.Length; j++)
                {
                    textures[j] = s[y + x + 4];
                    x++;
                }
                uint[] texHV = new uint[int.Parse(s[y + 3])];
                for (int j = 0; j < texHV.Length; j++)
                {
                    texHV[j] = uint.Parse(s[y + x + 4]);
                    x++;
                }
                FaceTextures[i] = new FaceTexture(s[y], uint.Parse(s[y + 1]), textures, texHV);
                y += x + 4;
            }
        }

        private void SetVoices(string[] s)
        {
            Voices = new Variable[int.Parse(s[0])];
            for (int i = 0; i < Voices.Length; i++)
            {
                string[] tmp = s[i + 1].Split();
                Voices[i] = new Variable(tmp[1], int.Parse(tmp[0]));
            }
        }

        public static string GetCompMode(int x)
        {
            string s;
            if (x == -1)
                s = "comp_less_than";
            else if (x == 1)
                s = "comp_greater_than";
            else
                s = x.ToString();
            return s;
        }

        #endregion
    }
}