using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using System.Drawing;

namespace MB_Decompiler_Library.Objects
{
    public class GameMenu : Skriptum
    {
        private GameMenuOption[] menuOptions;
        private string text, flags, meshName;
        private ulong flagsGZ;
        private string[] operationBlock = new string[0];
        private static ImportsManager impManager = new ImportsManager(CodeReader.FILES_PATH);
        private Color textColor = Color.Black;

        public GameMenu(string[] raw_data) : base(raw_data[0].Substring(raw_data[0].IndexOf('_') + 1).Split()[0], ObjectType.GAME_MENU)
        {
            string[] tmpS = raw_data[0].Split();
            InitializeGameMeu(tmpS);
            menuOptions = DecompileGameMenuOptions(raw_data[1].Split(), int.Parse(tmpS[tmpS.Length - 1]));
        }

        public string Text { get { return text; } }

        public string Flags { get { return flags; } }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string MeshName { get { return meshName; } }

        public Color TextColor { get { return textColor; } }

        public string[] OperationBlock { get { return operationBlock; } }

        public GameMenuOption[] MenuOptions { get { return menuOptions; } }

        private void InitializeGameMeu(string[] raw_data)
        {
            string[] tmpSX;
            int tmp = 1, tmp2;
            SetFlags(raw_data[tmp]);
            tmp++;
            text = raw_data[tmp];
            tmp++;
            meshName = raw_data[tmp];
            tmp++;
            tmp2 = int.Parse(raw_data[tmp]);
            if (tmp2 != 0)
            {
                operationBlock = new string[tmp2 + 1];
                tmpSX = new string[raw_data.Length - tmp - 1];
                tmp++;
                for (int i = tmp - 1; i < raw_data.Length - 1; i++)
                    tmpSX[i - (tmp - 1)] = raw_data[i];
                operationBlock[0] = ID + 1;
                tmpSX = CodeReader.DecompileScriptCode(operationBlock, tmpSX);
                operationBlock = CodeReader.GetStringArrayStartFromIndex(tmpSX, 1);
            }
        }

        private void SetFlags(string _flags)
        {
            flags = string.Empty;
            flagsGZ = ulong.Parse(_flags);
            ulong tmpU = flagsGZ;
            char[] cc = SkillHunter.Dec2Hex_16CHARS(flagsGZ).ToCharArray();
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
                string tmp = SkillHunter.Dec2Hex(tmpU >> 32);
                textColor = Color.FromArgb(byte.Parse(SkillHunter.Hex2Dec(tmp.Substring(0, 2)).ToString()),
                                           byte.Parse(SkillHunter.Hex2Dec(tmp.Substring(2, 2)).ToString()),
                                           byte.Parse(SkillHunter.Hex2Dec(tmp.Substring(4, 2)).ToString()),
                                           byte.Parse(SkillHunter.Hex2Dec(tmp.Substring(6, 2)).ToString()));
                flags += "|menu_text_color(0x" + tmp +')';
            }

            if (flags.Equals(string.Empty))
                flags = _flags;
            else
                flags = flags.Substring(1);
        }

        public void SetText(string text) { this.text = text; }

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