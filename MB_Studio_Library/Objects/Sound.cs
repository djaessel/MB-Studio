using System.IO;
using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects.Support;

namespace MB_Studio_Library.Objects
{
    public class Sound : Skriptum
    {
        private static string[] allSoundFiles = new string[0];

        public Sound(string[] raw_data) : base(raw_data[0], ObjectType.Sound)
        {
            if (allSoundFiles.Length == 0)
                InitializeAllSounds();
            if (ImportantMethods.IsNumericGZ(raw_data[1]))
            {
                FlagsGZ = ulong.Parse(raw_data[1]);
                SetFlags();
            }
            else
            {
                Flags = raw_data[1];
                SetFlagsGZ();
            }
            SoundFiles = new Variable[int.Parse(raw_data[2])];
            for (int i = 0; i < SoundFiles.Length; i++)
            {
                string soundFileId = allSoundFiles[int.Parse(raw_data[i * 2 + 3])];
                ulong soundFileFlags = ulong.Parse(raw_data[i * 2 + 4]);
                SoundFiles[i] = new Variable(soundFileId, soundFileFlags);
            }
        }

        public Sound(string[] source_data, bool sortSounds) : base(source_data[0], ObjectType.Sound)
        {
            Flags = source_data[1];
            SetFlagsGZ();
            string[] soundFileIds = source_data[2].Split();
            SoundFiles = new Variable[soundFileIds.Length];
            ulong soundFileFlags = 0;//should be checked before and replaced with real flags if there are some
            for (int i = 0; i < SoundFiles.Length; i++)
                SoundFiles[i] = new Variable(soundFileIds[i], soundFileFlags);
        }

        public ulong FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public Variable[] SoundFiles { get; }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((FlagsGZ & 0x1) == 1)
                flags += "sf_2d|";
            if ((FlagsGZ & 0x2) == 2)
                flags += "sf_looping|";
            if ((FlagsGZ & 0x4) == 4)
                flags += "sf_start_at_random_pos|";
            if ((FlagsGZ & 0x8) == 8)
                flags += "sf_stream_from_hd|";
            if ((FlagsGZ & 0x100000) == 0x100000)
                flags += "sf_always_send_via_network|";
            if ((FlagsGZ & 0xf00) != 0)
                flags += "sf_vol_" + ((FlagsGZ & 0xf00) >> 8) + "|";
            if ((FlagsGZ & 0xf0) != 0)
                flags += "sf_priority_" + ((FlagsGZ & 0xf0) >> 4) + "|";

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = FlagsGZ.ToString();

            Flags = flags;
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0ul;
            string[] flagsS = Flags.Trim().Split('|');
            foreach (string flag in flagsS)
            {
                if (flag.Equals("sf_2d"))
                    flagsGZ |= 0x00000001;
                else if (flag.Equals("sf_looping"))
                    flagsGZ |= 0x00000002;
                else if (flag.Equals("sf_start_at_random_pos"))
                    flagsGZ |= 0x00000004;
                else if (flag.Equals("sf_stream_from_hd"))
                    flagsGZ |= 0x00000008;
                else if (flag.Equals("sf_always_send_via_network"))
                    flagsGZ |= 0x00100000;
                else if (flag.StartsWith("sf_priority_"))
                    flagsGZ |= ulong.Parse(flag.Split('_')[1]) << 4;
                else if (flag.StartsWith("sf_vol_"))
                    flagsGZ |= ulong.Parse(flag.Split('_')[1]) << 8;
            }
            FlagsGZ = flagsGZ;
        }

        private static void InitializeAllSounds()
        {
            using (StreamReader sr = new StreamReader(CodeReader.ModPath + "sounds.txt"))
            {
                sr.ReadLine();
                allSoundFiles = new string[int.Parse(sr.ReadLine())];
                for (int i = 0; i < allSoundFiles.Length && !sr.EndOfStream; i++)
                    allSoundFiles[i] = sr.ReadLine().Substring(1).Split()[0];
            }
        }

    }
}