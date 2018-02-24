using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WarbandTranslator
{
    public class Reader
    {
        private string file;
        private string path;
        private string mode;
        public const char SPLIT1 = '|', SPLIT2 = ' ';

        public const string ITEMS = "items", GAME_MENUS = "menus", STRINGS = "strings", QUICK_STRINGS = "quick_string", TROOPS = "troops", CONVERSATION = "dialogs", FACTIONS = "factions", 
            INFO_PAGES = "infopages", PARTIES = "parties", PARTYTEMPLATES = "partytemplates", QUESTS = "quests", SKILLS = "skills", SKINS = "skins_", DEFAULT = "?";

        public Reader(string path, string file)
        {
            this.path = path;
            this.file = file;
            checkFileExists();
            readFirstLine();
        }

        private void checkFileExists()
        {
            string realPath = path + file, tmpPATH = string.Empty;
            if (!Directory.Exists(path.Substring(0, path.Length - 1)))
                Directory.CreateDirectory(path.Substring(0, path.Length - 1));
            if (!File.Exists(realPath))
            {
                string[] spl = realPath.Split('\\');
                for (int i = 0; i < (spl.Length - 1); i++)
                {
                    tmpPATH += spl[i];
                    if (i < (spl.Length - 2))
                        tmpPATH += "\\";
                }
                Directory.CreateDirectory(tmpPATH);
                using (StreamWriter wr = new StreamWriter(realPath))
                    wr.WriteLine("lockitver|DO NOT DELETE THIS LINE! " + FileSaver.LockItVersion);
            }
        }

        public List<string> GetOriginalFileProperties(List<string> listx)
        {
            Main.Saved = false;
            if (mode.Equals(ITEMS))
                readItems(listx);
            else if (mode.Equals(GAME_MENUS))
                readGameMenus(listx);
            else if (mode.Equals(STRINGS) || mode.Equals(QUICK_STRINGS))
                readStrings(listx);
            else if (mode.Equals(TROOPS))
                readTroops(listx);
            else if (mode.Equals(CONVERSATION))
                readConversations(listx);
            else if (mode.Equals(FACTIONS))
                readFactions(listx);
            else if (mode.Equals(INFO_PAGES))
                readInfoPages(listx);
            else if (mode.Equals(PARTIES))
                readParties(listx);
            else if (mode.Equals(PARTYTEMPLATES))
                readPartyTemplates(listx);
            else if (mode.Equals(QUESTS))
                readQuests(listx);
            else if (mode.Equals(SKILLS))
                readSkills(listx);
            else if (mode.Equals(SKINS))
                readSkins(listx);
            else
            {
                readDefault(listx);
                MessageBox.Show("MODE: " + mode);
            }

            return listx;
        }

        public List<string> GetLanguageFileProperties(List<string> listx)
        {
            Main.Saved = false;
            return readDefault(listx);
        }

        private string searchNextValue(string[] sx, int index)
        {
            string s = string.Empty;
            for (int i = index; i < sx.Length; i++)
            {
                if (!IsNumeric(sx[i]) && !sx[i].Equals(string.Empty))
                {
                    s = sx[i];
                    i = sx.Length;
                }
            }
            return s;
        }

        private void readItems(List<string> listx)
        {
            string line; string[] split;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains(" itm_"))
                    {
                        line = line.Substring(1);
                        split = line.Split(SPLIT2);
                        listx.Add(split[0] + SPLIT1 + split[1]);
                        listx.Add(split[0] + "_pl" + SPLIT1 + split[2]);
                    }
                }
            }
        }

        private void readGameMenus(List<string> listx)
        {
            string line; string[] split;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("menu_") || line.Contains(" mno_"))
                    {
                        split = line.Split(SPLIT2);
                        for (int i = 0; i < split.Length; i++)
                            if (!split[i].Equals(string.Empty) && !IsNumeric(split[i]))
                                if (split[i].Contains("mno_") || split[i].Contains("menu_"))
                                    listx.Add(split[i] + SPLIT1 + searchNextValue(split, i + 1));
                    }
                }
            }
        }

        private void readStrings(List<string> listx)
        {
            using (StreamReader sr = new StreamReader(path + file))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                    listx.Add(sr.ReadLine().Replace(SPLIT2, SPLIT1));
            }
        }

        private void readTroops(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains("trp_"))
                    {
                        spl = s.Split(SPLIT2);
                        listx.Add(spl[0] + SPLIT1 + spl[1]);
                        listx.Add(spl[0] + "_pl" + SPLIT1 + spl[2]);
                    }
                }
            }
        }

        private void readConversations(List<string> listx)
        {
            string s, NO_VOICE = "NO_VOICEOVER";
            string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                s = sr.ReadLine();
                s = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    spl = s.Split(SPLIT2);
                    s = string.Empty;
                    for (int i = 1; i < spl.Length; i++)
                    {
                        if (!IsNumeric(spl[i]) && !spl[i].Equals(string.Empty) && !spl[i].Equals(NO_VOICE))
                        {
                            s = spl[i];
                            i = spl.Length;
                        }
                    }
                    listx.Add(spl[0] + SPLIT1 + s);
                }
            }
        }

        private void readSkins(List<string> listx)
        {
            string s, txt = "skinkey_"; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains(txt))
                    {
                        spl = s.Split(SPLIT2);
                        for (int i = 0; i < spl.Length; i++)
                        {
                            if (spl[i].Contains(txt))
                            {
                                for (int j = i + 1; j < spl.Length; j++)
                                {
                                    if (!IsNumeric(spl[j]))
                                    {
                                        s = spl[i] + SPLIT1 + spl[j];
                                        i = j;
                                        j = spl.Length;
                                        if (!listx.Contains(s))
                                            listx.Add(s);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void readFactions(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains(" fac_"))
                    {
                        spl = s.Split(SPLIT2);
                        listx.Add(spl[1] + SPLIT1 + spl[2]);
                    }
                }
            }
        }

        private void readInfoPages(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains("ip_"))
                    {
                        spl = s.Split(SPLIT2);
                        listx.Add(spl[0] + SPLIT1 + spl[1]);
                        listx.Add(spl[0] + "_text" + SPLIT1 + spl[2]);
                    }
                }
            }
        }

        private void readParties(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains("p_"))
                    {
                        spl = s.Split(SPLIT2);
                        listx.Add(spl[4] + SPLIT1 + spl[5]);
                    }
                }
            }
        }

        private void readPartyTemplates(List<string> listx)
        {
            string[] split;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    split = sr.ReadLine().Split(SPLIT2);
                    if (!split[0].Equals(string.Empty) && !IsNumeric(split[0]))
                            listx.Add(split[0] + SPLIT1 + split[1]);
                }
            }
        }

        private void readQuests(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (s.Contains("qst_"))
                    {
                        spl = s.Split(SPLIT2);
                        listx.Add(spl[0] + SPLIT1 + spl[1]);
                    }
                }
            }
        }

        private void readSkills(List<string> listx)
        {
            string s; string[] spl;
            using (StreamReader sr = new StreamReader(path + file))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    spl = s.Split(SPLIT2);
                    listx.Add(spl[0] + SPLIT1 + spl[1]);
                    listx.Add(spl[0] + "_desc" + SPLIT1 + spl[4]);
                }
            }
        }

        private List<string> readDefault(List<string> list)
        {
            string s;
            using (StreamReader sr = new StreamReader(path + file))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (!list.Contains(s))
                        list.Add(s);
                }
            }
            return list;
        }

        private void readFirstLine()
        {
            string[] sx;
            using (StreamReader sr = new StreamReader(path + file))
            {
                sx = sr.ReadLine().Split(SPLIT2, SPLIT1);
                if (!IsNumeric(sx[0]) && sx[0].Length >= 5 && !sx[0].Equals("lockitver"))
                    mode = sx[0].Replace("file", "");
                else if (file.Contains(SKILLS))
                    mode = SKILLS;
                else if (file.Contains(QUICK_STRINGS))
                    mode = QUICK_STRINGS;
                else if (file.Contains(GAME_MENUS))
                    mode = GAME_MENUS;
            }
        }

        public static bool IsNumeric(string s)
        {
            double d;
            return double.TryParse(s, out d);
        }
    }
}
