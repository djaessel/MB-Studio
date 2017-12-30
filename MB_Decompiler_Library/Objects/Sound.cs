using MB_Decompiler_Library.IO;
using skillhunter;
using System.IO;
using System;

namespace MB_Decompiler_Library.Objects
{
    public class Sound : Skriptum
    {
        private ulong flags;
        private string[] soundFiles;
        private static string[] allSoundFiles = new string[0];

        public Sound(string[] raw_data) : base(raw_data[0], ObjectType.SOUND)
        {
            if (allSoundFiles.Length == 0)
                initializeAllSounds();
            flags = ulong.Parse(raw_data[1]);
            soundFiles = new string[int.Parse(raw_data[2])];
            for (int i = 0; i < soundFiles.Length; i++)
                soundFiles[i] = allSoundFiles[int.Parse(raw_data[i * 2 + 3])];
        }

        public ulong Flags { get { return flags; } }

        public string[] SoundFiles { get { return soundFiles; } }

        public static void setSoundFiles(string[] allSoundFiles) { Sound.allSoundFiles = allSoundFiles; }

        private static void initializeAllSounds()
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