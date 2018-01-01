using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Warband___Translator
{
    public class FileSaver
    {
        private string path;
        private string file;
        private static int lockitver = 2896;
        private const string BAK = ".bak";
        private const string BAK2 = ".bak2";
        private const string BACKUPFOLDER = "backups";
        private const char SPLIT = '|';

        public static int LockItVer
        {
            get { return lockitver; }
            set { lockitver = value; }
        }

        public string FullFilePath { get { return path + file; } }

        public FileSaver(string path, string file)
        {
            this.path = path;
            this.file = file;
        }

        public void saveFile(List<string> list, List<string> originalList)
        {
            string[] spl;
            if (File.Exists(FullFilePath))
            {
                makeBackup(FullFilePath);
                using (StreamWriter wr = new StreamWriter(FullFilePath))
                {
                    wr.WriteLine("lockitver|DO NOT DELETE THIS LINE! " + lockitver);
                    foreach (string s in originalList)
                    {
                        spl = s.Split(SPLIT);
                        wr.WriteLine(spl[0] + SPLIT + spl[1].Replace('_', ' '));
                    }
                    if (list.Count > 0)
                    {
                        foreach (string s in list)
                        {
                            spl = s.Split(SPLIT);
                            wr.WriteLine(spl[0] + SPLIT + spl[1].Replace('_', ' '));
                        }
                    }
                }
                //Main.Saved = true;
            }
            else
                MessageBox.Show("The File doesn't exists or the path is incorrect!" + Environment.NewLine + "The file couldn't be saved!");
        }

        public void makeBackup(string filePath)
        {
            string backupFilePath = string.Empty;
            string[] spl = filePath.Split('\\');

            for (int i = 0; i < spl.Length - 1; i++)
                backupFilePath += spl[i] + '\\';

            backupFilePath += BACKUPFOLDER;

            if (!Directory.Exists(backupFilePath))
                Directory.CreateDirectory(backupFilePath);

            backupFilePath += '\\' + spl[spl.Length - 1].Split('.')[0];

            if (File.Exists(backupFilePath + BAK))
                File.Copy(backupFilePath + BAK, backupFilePath + BAK2, true);

            File.Copy(path + file, backupFilePath + BAK, true);
        }
    }
}
