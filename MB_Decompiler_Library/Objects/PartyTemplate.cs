using System;
using System.Collections.Generic;
using importantLib;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class PartyTemplate : Skriptum
    {
        private PMember[] ptMembers = new PMember[6];
        private string name, menuIDString, faction, flags;
        private int menuID, personality, factionID;
        private ulong flagsGZ;

        public PartyTemplate(string[] raw_data) : base(raw_data[0], ObjectType.PARTY_TEMPLATE)
        {
            name = raw_data[1];
            flagsGZ = ulong.Parse(raw_data[2]);
            SetFlagsString();
            menuID = int.Parse(raw_data[3]);
            factionID = int.Parse(raw_data[4]);
            faction = CodeReader.Factions[factionID];
            personality = int.Parse(raw_data[5]);
            for (int i = 0; i < ptMembers.Length; i++)
                ptMembers[i] = PMember.DEFAULT_MEMBER;
            for (int i = 0; i < ptMembers.Length; i++)
                if (!raw_data[(i * 4) + 6].Equals("-1"))
                    ptMembers[i] = new PMember(CodeReader.GetStringArrayStartFromIndex(raw_data, (i * 4) + 6));
                else
                    i = ptMembers.Length;
        }

        public PartyTemplate(string[] source_data, bool second) : base(source_data[0], ObjectType.PARTY)
        {
            int curIdx = 0;

            name = source_data[curIdx++];

            flags = source_data[curIdx++];
            SetFlagsGZ();

            menuIDString = source_data[curIdx++];
            if (menuIDString.Equals("no_menu"))
                menuID = 0;
            else
                menuID = CodeReader.GameMenus.IndexOf(menuIDString);

            //faction = source_data[curIdx++];
            //if (faction.Equals("no_faction"))
            //    factionID = -1;
            //else
            factionID = CodeReader.Factions.IndexOf(source_data[curIdx++]);

            string tmpS = source_data[curIdx++].Replace("soldier_personality", "aggressiveness_8|courage_9");
            tmpS = tmpS.Replace("merchant_personality", "aggressiveness_0|courage_7");
            tmpS = tmpS.Replace("escorted_merchant_personality", "aggressiveness_0|courage_11");
            tmpS = tmpS.Replace("bandit_personality", "aggressiveness_3|courage_8|banditness");

            int x = 0;
            string[] tmp = tmpS.Split('|');
            string[] tmp2;
            foreach (string s in tmp)
            {
                tmp2 = s.Trim().Split('_');
                if (tmp2[0].Equals("courage"))
                    x |= int.Parse(tmp2[1]);
                else if (tmp2[0].Equals("aggressiveness"))
                    x |= int.Parse(tmp2[1]) << 4;
                else if (tmp2[0].Equals("banditness"))
                    x |= 0x0100;
            }
            personality = x;

            x = (source_data.Length - 11) / 3;

            ptMembers = new PMember[x];
            for (int i = 0; i < x; i++)
            {
                curIdx = 11 + i * 3;
                if (!ImportantMethods.IsNumericGZ(source_data[curIdx]))
                    source_data[curIdx] = CodeReader.Troops.IndexOf(source_data[curIdx]).ToString();
                curIdx += 2;
                if (source_data[curIdx].Equals("pmf_is_prisoner"))
                    source_data[curIdx] = "1";
                if (!ImportantMethods.IsNumericGZ(source_data[curIdx]))
                    DisplayErrorMessage();
                curIdx -= 2;
                ptMembers[i] = new PMember(new string[] { source_data[curIdx], source_data[curIdx++], source_data[curIdx] });
            }
        }

        private void SetFlagsGZ()
        {
            int x;
            ulong flagsGZ = 0;
            List<string> flagsList = new List<string>(flags.Split('|'));

            if (flagsList.Contains("pf_disabled"))
                flagsGZ |= 0x00000100;
            if (flagsList.Contains("pf_is_ship"))
                flagsGZ |= 0x00000200;
            if (flagsList.Contains("pf_is_static"))
                flagsGZ |= 0x00000400;

            if (flagsList.Contains("pf_label_medium"))
                flagsGZ |= 0x00001000;
            if (flagsList.Contains("pf_label_large"))
                flagsGZ |= 0x00002000;

            if (flagsList.Contains("pf_always_visible"))
                flagsGZ |= 0x00004000;
            if (flagsList.Contains("pf_default_behavior"))
                flagsGZ |= 0x00010000;
            if (flagsList.Contains("pf_auto_remove_in_town"))
                flagsGZ |= 0x00020000;
            if (flagsList.Contains("pf_quest_party"))
                flagsGZ |= 0x00040000;
            if (flagsList.Contains("pf_no_label"))
                flagsGZ |= 0x00080000;
            if (flagsList.Contains("pf_limit_members"))
                flagsGZ |= 0x00100000;
            if (flagsList.Contains("pf_hide_defenders"))
                flagsGZ |= 0x00200000;
            if (flagsList.Contains("pf_show_faction"))
                flagsGZ |= 0x00400000;
            //if (flagsList.Contains("pf_is_hidden"))
            //    flagsGZ |= 0x01000000; //used in the engine, do not overwrite this flag
            if (flagsList.Contains("pf_dont_attack_civilians"))
                flagsGZ |= 0x02000000;
            if (flagsList.Contains("pf_civilian"))
                flagsGZ |= 0x04000000;

            foreach (string flagS in flagsList)
            {
                if (flagS.Contains("carries_"))
                {
                    x = int.Parse(flagS.Substring(flagS.IndexOf('(') + 1).Split(')')[0].Trim());
                    if (flagS.Contains("goods"))
                        flagsGZ |= (ulong)(x << 48) & 0x00ff000000000000;
                    else
                        flagsGZ |= (ulong)((x / 20) << 56) & 0xff00000000000000;
                }
                else if (flagS.StartsWith("icon_"))
                    flagsGZ |= (uint)CodeReader.MapIcons.IndexOf(flagS);
            }

            this.flagsGZ = flagsGZ;
        }

        private static void DisplayErrorMessage()
        {
            System.Windows.Forms.MessageBox.Show("ERROR - 0x9930 - PartyTemplate", "ERROR");
        }

        private void SetFlagsString()
        {
            char[] cc = SkillHunter.Dec2Hex_16CHARS(flagsGZ).ToCharArray();
            string tmp = SkillHunter.Hex2Dec("000000" + cc[cc.Length - 2] + cc[cc.Length - 1]).ToString();
            if (CodeReader.MapIcons.Count > 0)
                flags = CodeReader.MapIcons[int.Parse(tmp)];

            byte b = byte.Parse(cc[13].ToString());
            if (b >= 4)
            {
                b -= 4;
                flags += "|pf_is_static";
            }
            if (b >= 2)
            {
                b -= 2;
                flags += "|pf_is_ship";
            }
            if (b == 1)
                flags += "|pf_disabled";

            b = byte.Parse(cc[12].ToString());
            if (b >= 4)
            {
                b -= 4;
                flags += "|pf_always_visible";
            }
            if (b == 2)
                flags += "|pf_label_large";
            else if (b == 1)
                flags += "|pf_label_medium";
            else
                flags += "|pf_label_small";

            b = byte.Parse(cc[11].ToString());
            if (b == 8)
            {
                b -= 8;
                flags += "|pf_no_label";
            }
            if (b >= 4)
            {
                b -= 4;
                flags += "|pf_quest_party";
            }
            if (b >= 2)
            {
                b -= 2;
                flags += "|pf_auto_remove_in_town";
            }
            if (b == 1)
                flags += "|pf_default_behavior";

            b = byte.Parse(cc[10].ToString());
            if (b >= 4)
            {
                b -= 4;
                flags += "|pf_show_faction";
            }
            if (b >= 2)
            {
                b -= 2;
                flags += "|pf_hide_defenders";
            }
            if (b == 1)
                flags += "|pf_limit_members";

            b = byte.Parse(cc[9].ToString());
            if (b >= 4)
            {
                b -= 4;
                flags += "|pf_civilian";
            }
            if (b == 2) /*//if (b >= 2)
            {
                b -= 2;*/
                flags += "|pf_dont_attack_civilians";
            /*}
            if (b == 1)
                flagsString += "|pf_is_hidden";*/

            b = byte.Parse(SkillHunter.Hex2Dec("000000" + cc[0] + cc[1]).ToString());
            if (b > 0)
                flags += "|carries_gold(" + b + ")";

            b = byte.Parse(SkillHunter.Hex2Dec("000000" + cc[2] + cc[3]).ToString());
            if (b > 0)
                flags += "|carries_goods(" + b + ")";

            flags = flags.Replace(" ", string.Empty).Trim('|');
        }

        public PMember[] Members { get { return ptMembers; } }

        public string Name { get { return name; } }

        public int FactionID { get { return factionID; } }

        public string Faction { get { return faction; } }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public int MenuID { get { return menuID; } }

        public int Personality { get { return personality; } }

    }
}