using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using System.Collections.Generic;
using importantLib;

namespace MB_Decompiler_Library.Objects
{
    public class Skin : Skriptum
    {
        private ulong flags;
        private string bodyMesh, calfMesh, handMesh, headMesh, skeletonName, bloodParticles1, bloodParticles2;
        private FaceKey[] faceKeys;
        private FaceKeyConstraint[] faceKeyConstraints;
        private FaceTexture[] faceTextures;
        private Variable[] voices;
        private string[] hairMeshes, beardMeshes, hairTextures, beardTextures;
        private double scale;

        private static readonly string[] soundKinds = { "voice_die", "voice_hit", "voice_grunt", "voice_grunt_long", "voice_yell", "voice_warcry", "voice_victory", "voice_stun" };
        private static readonly string[] skf_flags = { "0", "skf_use_morph_key_10", "skf_use_morph_key_20", "skf_use_morph_key_30", "skf_use_morph_key_40", "skf_use_morph_key_50", "skf_use_morph_key_60", "skf_use_morph_key_70" };

        public static string[] SoundKinds { get { return soundKinds; } }
        public static string[] SkinFlags { get { return skf_flags; } }

        public Skin(List<string[]> raw_data) : base(raw_data[0][0], ObjectType.Skin)
        {
            flags = ulong.Parse(raw_data[0][1]);
            bodyMesh = raw_data[1][0];
            calfMesh = raw_data[1][1];
            handMesh = raw_data[1][2];
            headMesh = raw_data[2][0];
            SetFaceKeys(raw_data[2]);
            hairMeshes = raw_data[3];
            if (hairMeshes.Length == 1 && hairMeshes[0].Trim().Equals(string.Empty))
                hairMeshes = new string[0];
            beardMeshes = raw_data[4];
            if (beardMeshes.Length == 1 && beardMeshes[0].Trim().Equals(string.Empty))
                beardMeshes = new string[0];
            hairTextures = new string[int.Parse(raw_data[5][0])];
            for (int i = 0; i < hairTextures.Length; i++)
                hairTextures[i] = raw_data[5][i + 1];
            beardTextures = new string[int.Parse(raw_data[6][0])];
            for (int i = 0; i < beardTextures.Length; i++)
                beardTextures[i] = raw_data[6][i + 1];
            SetFaceTextures(raw_data[7]);
            SetVoices(raw_data[8]);
            skeletonName = raw_data[9][0];
            scale = double.Parse(CodeReader.Repl_DotWComma(raw_data[9][1]));
            bloodParticles1 = CodeReader.ParticleSystems[int.Parse(raw_data[10][0])];
            bloodParticles2 = CodeReader.ParticleSystems[int.Parse(raw_data[10][1])];
            int fkcCount = 0;
            if (ImportantMethods.IsNumericFKZ2(raw_data[11][0]))
            {
                fkcCount = int.Parse(raw_data[11][0]);
                if (fkcCount > 0)
                {
                    faceKeyConstraints = new FaceKeyConstraint[int.Parse(raw_data[11][0])];
                    for (int i = 0; i < faceKeyConstraints.Length; i++)
                        faceKeyConstraints[i] = new FaceKeyConstraint(raw_data[12 + i]); //raw_data[11 + i]
                }
            }
            if (fkcCount <= 0)
                faceKeyConstraints = new FaceKeyConstraint[0];
        }

        #region Properties

        public ulong Flags { get { return flags; } }

        public string BodyMesh { get { return bodyMesh; } }

        public string CalfMesh { get { return calfMesh; } }

        public string HandMesh { get { return handMesh; } }

        public string HeadMesh { get { return headMesh; } }

        public string SkeletonName { get { return skeletonName; } }

        public string BloodParticle1 { get { return bloodParticles1; } }

        public string BloodParticle2 { get { return bloodParticles2; } }

        public FaceKey[] FaceKeys { get { return faceKeys; } }

        public FaceKeyConstraint[] FaceKeyConstraints { get { return faceKeyConstraints; } }

        public FaceTexture[] FaceTextures { get { return faceTextures; } }

        public Variable[] Voices { get { return voices; } }

        public string[] HairMeshes { get { return hairMeshes; } }

        public string[] BeardMeshes { get { return beardMeshes; } }

        public string[] HairTextures { get { return hairTextures; } }

        public string[] BeardTextures { get { return beardTextures; } }

        public double Scale { get { return scale; } }

        #endregion

        #region Methods

        private void SetFaceKeys(string[] s)
        {
            string t1, t2;
            faceKeys = new FaceKey[int.Parse(s[1])];
            for (int i = 0; i < faceKeys.Length; i++)
            {
                t1 = CodeReader.Repl_DotWComma(s[i * 6 + 5]);
                t2 = CodeReader.Repl_DotWComma(s[i * 6 + 6]);
                double d1 = double.Parse(t1);
                double d2 = double.Parse(t2);
                if (t1.Substring(0, 1).Equals("-") && d1 == 0d)
                    t1 = CodeReader.MINUS_ZERO;
                if (t2.Substring(0, 1).Equals("-") && d2 == 0d)
                    t2 = CodeReader.MINUS_ZERO;
                faceKeys[i] = new FaceKey(
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
            faceTextures = new FaceTexture[int.Parse(s[0])];
            s = CodeReader.GetStringArrayStartFromIndex(s, 1);
            for (int i = 0; i < faceTextures.Length; i++)
            {
                string[] textures = new string[int.Parse(s[y + 2])];
                x = 0;
                for (int j = 0; j < textures.Length; j++)
                {
                    textures[j] = s[y + x + 4];
                    x++;
                }
                string[] texHV = new string[int.Parse(s[y + 3])];
                for (int j = 0; j < texHV.Length; j++)
                {
                    texHV[j] = s[y + x + 4];
                    x++;
                }
                faceTextures[i] = new FaceTexture(s[y], ulong.Parse(s[y + 1]), textures, texHV);
                y += x + 4;
            }
        }

        private void SetVoices(string[] s)
        {
            voices = new Variable[int.Parse(s[0])];
            for (int i = 0; i < voices.Length; i++)
            {
                string[] tmp = s[i + 1].Split();
                voices[i] = new Variable(tmp[1], int.Parse(tmp[0]));
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