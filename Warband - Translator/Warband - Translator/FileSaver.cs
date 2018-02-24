using System.Collections.Generic;
using System.IO;

namespace WarbandTranslator
{
    public class FileSaver
    {
        private string path;
        private string file;
        private static int lockitver = 2896;
        private const string BAK = ".bak";
        private const string BAK2 = ".bak2";
        private const string BACKUPFOLDER = "backups";

        public static int LockItVersion
        {
            get { return lockitver; }
            set { lockitver = value; }
        }

        public string FilePath { get { return path.TrimEnd('\\') + '\\' + file; } }

        public FileSaver(string path, string file)
        {
            this.path = path;
            this.file = file;
        }

        public void SaveFile(List<string> list, List<string> originalList)
        {
            //string[] spl;
            /*if (File.Exists(FilePath))
            {*/
            MakeBackup();
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            string[] spl;
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                wr.WriteLine("lockitver|DO NOT DELETE THIS LINE! " + lockitver);
                foreach (string s in originalList)
                {
                    spl = s.Split(Reader.SPLIT1);
                    wr.WriteLine(spl[0] + Reader.SPLIT1 + spl[1].Replace('_', ' '));
                }
                if (list.Count > 0)
                {
                    foreach (string s in list)
                    {
                        spl = s.Split(Reader.SPLIT1);
                        wr.WriteLine(spl[0] + Reader.SPLIT1 + spl[1].Replace('_', ' '));
                    }
                }
            }
            Main.Saved = true;
            /*}
            else
                MessageBox.Show("The File doesn't exists or the path is incorrect!" + Environment.NewLine + "The file couldn't be saved!");*/
        }

        public void MakeBackup()
        {
            if (File.Exists(FilePath))
            {
                string backupFilePath = path.TrimEnd('\\') + '\\' + BACKUPFOLDER;
                if (!Directory.Exists(backupFilePath))
                    Directory.CreateDirectory(backupFilePath);
                backupFilePath += '\\' + file.Split('.')[0];
                if (File.Exists(backupFilePath + BAK))
                    File.Copy(backupFilePath + BAK, backupFilePath + BAK2, true);
                File.Copy(FilePath, backupFilePath + BAK, true);
            }
        }
    }
}
