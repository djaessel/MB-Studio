using importantLib;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using System.Drawing;

namespace MB_Decompiler_Library.Objects
{
    public class GameMenu : Skriptum
    {
        private static readonly ImportsManager impManager = new ImportsManager(CodeReader.FILES_PATH);

        public GameMenu(string[] raw_data) : base(raw_data[0].Substring(raw_data[0].IndexOf('_') + 1).Split()[0], ObjectType.GameMenu)
        {
            string[] tmpS = raw_data[0].Split();
            InitializeGameMeu(tmpS);
            MenuOptions = DecompileGameMenuOptions(raw_data[1].Split(), int.Parse(tmpS[tmpS.Length - 1]));
        }

        public string Text { get; private set; }//change to Name?

        public string Flags { get; private set; }

        public ulong FlagsGZ { get; private set; }

        public string MeshName { get; private set; }

        public Color TextColor { get; private set; } = Color.Black;
        public string[] OperationBlock { get; private set; } = new string[0];
        public GameMenuOption[] MenuOptions { get; }

        private void InitializeGameMeu(string[] raw_data)
        {
            string[] tmpSX;
            int tmp = 1, tmp2;
            if (ImportantMethods.IsNumericGZ(raw_data[tmp]))
            {
                FlagsGZ = ulong.Parse(raw_data[tmp]);
                SetFlags();
            }
            else
            {
                Flags = raw_data[tmp].Trim();
                SetFlagsGZ();
            }
            tmp++;
            Text = raw_data[tmp];
            tmp++;
            MeshName = raw_data[tmp];
            tmp++;
            tmp2 = int.Parse(raw_data[tmp]);
            if (tmp2 != 0)
            {
                OperationBlock = new string[tmp2 + 1];
                tmpSX = new string[raw_data.Length - tmp - 1];
                tmp++;
                for (int i = tmp - 1; i < raw_data.Length - 1; i++)
                    tmpSX[i - (tmp - 1)] = raw_data[i];
                OperationBlock[0] = ID + 1;
                tmpSX = CodeReader.DecompileScriptCode(OperationBlock, tmpSX);
                OperationBlock = CodeReader.GetStringArrayStartFromIndex(tmpSX, 1);
            }
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0ul;
            string tmp;
            string[] sp = Flags.Split('|');

            foreach (string s in sp)
            {
                if (s.Equals("mnf_join_battle"))
                    flagsGZ |= 0x00000001; //Consider this menu when the player joins a battle
                else if (s.Equals("mnf_auto_enter"))
                    flagsGZ |= 0x00000010; //Automatically enter the town with the first menu option.
                else if (s.Equals("mnf_enable_hot_keys"))
                    flagsGZ |= 0x00000100; //Enables P,I,C keys
                else if (s.Equals("mnf_disable_all_keys"))
                    flagsGZ |= 0x00000200; //Disables all keys
                else if (s.Equals("mnf_scale_picture"))
                    flagsGZ |= 0x00001000; //Scale menu picture to offest screen aspect ratio
                else if (s.StartsWith("menu_text_color("))
                {
                    tmp = s.Split('(')[1].Split(')')[0].Trim('\t', ' ');
                    if (!ImportantMethods.IsNumericGZ(tmp))
                    {
                        if (tmp.StartsWith("0x"))
                            tmp = tmp.Substring(2);
                        tmp = HexConverter.Hex2Dec(tmp).ToString();
                    }
                    flagsGZ |= (ulong.Parse(tmp) << 32); //color
                }
            }

            FlagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;
            ulong tmpU = FlagsGZ;

            char[] cc = HexConverter.Dec2Hex_16CHARS(FlagsGZ).ToCharArray();

            if (cc.Length > 7)
            {
                if (cc[cc.Length - 1] == '1')
                {
                    flags += "|mnf_join_battle";
                    tmpU -= 0x1;
                }

                if (cc[cc.Length - 2] == '1')
                {
                    flags += "|mnf_auto_enter";
                    tmpU -= 0x10;
                }

                if (cc[cc.Length - 3] == '1')
                {
                    flags += "|mnf_enable_hot_keys";
                    tmpU -= 0x100;
                }
                else if (cc[cc.Length - 3] == '2')
                {
                    flags += "|mnf_disable_all_keys";
                    tmpU -= 0x200;
                }
                else if (cc[cc.Length - 3] == '3')
                {
                    flags += "|mnf_enable_hot_keys|mnf_disable_all_keys";
                    tmpU -= 0x300;
                }

                if (cc[cc.Length - 4] == '1')
                {
                    flags += "|mnf_scale_picture";
                    tmpU -= 0x1000;
                }
            }

            if (tmpU > 0)
            {
                string tmp = HexConverter.Dec2Hex(tmpU >> 32);
                TextColor = Color.FromArgb(byte.Parse(HexConverter.Hex2Dec(tmp.Substring(0, 2)).ToString()),
                                           byte.Parse(HexConverter.Hex2Dec(tmp.Substring(2, 2)).ToString()),
                                           byte.Parse(HexConverter.Hex2Dec(tmp.Substring(4, 2)).ToString()),
                                           byte.Parse(HexConverter.Hex2Dec(tmp.Substring(6, 2)).ToString()));
                flags += "|menu_text_color(0x" + tmp +')';
            }

            if (flags.Equals(string.Empty))
                flags = FlagsGZ.ToString();
            else
                flags = flags.TrimStart('|');

            Flags = flags;
        }

        public void SetText(string text) { Text = text; }

        public static GameMenuOption[] DecompileGameMenuOptions(string[] optionsCode, int count)
        {
            GameMenuOption[] gameMenuOptions = new GameMenuOption[count];
            //string[] codes = CodeReader.ReInitializeArrayAndRemoveEmpty(optionsCode, count, 6);
            string[] codes = new string[count];
            int lastStartIndex = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = lastStartIndex; j < optionsCode.Length; j++)
                {
                    if (optionsCode[j].Split('_')[0].Equals("mno"))
                    {
                        codes[i] = string.Empty;
                        bool end = false;
                        do
                        {
                            codes[i] += optionsCode[j] + ' ';
                            if (j == optionsCode.Length - 1)
                                end = true;
                            else
                                j++;
                        } while (!optionsCode[j].Split('_')[0].Equals("mno") && !end);
                        codes[i] = codes[i].TrimEnd();
                        lastStartIndex = j;
                        j = optionsCode.Length;
                    }
                }
            }
            for (int i = 0; i < gameMenuOptions.Length; i++)
                gameMenuOptions[i] = new GameMenuOption(codes[i].Split());
            return gameMenuOptions;
        }
    }
}