using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class PartyTemplate : Skriptum
    {
        private PMember[] pt_members = new PMember[6];
        private string partyTemplateName, faction, flagsString;
        private int menuID, personality, factionID;
        private ulong flags;

        public PartyTemplate(string[] raw_data) : base(raw_data[0], ObjectType.PARTY_TEMPLATE)
        {
            partyTemplateName = raw_data[1];
            flags = ulong.Parse(raw_data[2]);
            SetFlagsString();
            menuID = int.Parse(raw_data[3]);
            factionID = int.Parse(raw_data[4]);
            faction = CodeReader.Factions[factionID];
            personality = int.Parse(raw_data[5]);
            for (int i = 0; i < pt_members.Length; i++)
                pt_members[i] = PMember.DEFAULT_MEMBER;
            for (int i = 0; i < pt_members.Length; i++)
                if (!raw_data[(i * 4) + 6].Equals("-1"))
                    pt_members[i] = new PMember(CodeReader.GetStringArrayStartFromIndex(raw_data, (i * 4) + 6));
                else
                    i = pt_members.Length;
        }

        private void SetFlagsString()
        {
            char[] cc = SkillHunter.Dec2Hex_16CHARS(flags).ToCharArray();
            string tmp = SkillHunter.Hex2Dec("000000" + cc[cc.Length - 2] + cc[cc.Length - 1]).ToString();
            if (CodeReader.MapIcons.Length > 0)
                flagsString = CodeReader.MapIcons[int.Parse(tmp)];

            byte b = byte.Parse(cc[13].ToString());
            if (b >= 4)
            {
                b -= 4;
                flagsString += "|pf_is_static";
            }
            if (b >= 2)
            {
                b -= 2;
                flagsString += "|pf_is_ship";
            }
            if (b == 1)
                flagsString += "|pf_disabled";

            b = byte.Parse(cc[12].ToString());
            if (b >= 4)
            {
                b -= 4;
                flagsString += "|pf_always_visible";
            }
            if (b == 2)
                flagsString += "|pf_label_large";
            else if (b == 1)
                flagsString += "|pf_label_medium";
            else
                flagsString += "|pf_label_small";

            b = byte.Parse(cc[11].ToString());
            if (b == 8)
            {
                b -= 8;
                flagsString += "|pf_no_label";
            }
            if (b >= 4)
            {
                b -= 4;
                flagsString += "|pf_quest_party";
            }
            if (b >= 2)
            {
                b -= 2;
                flagsString += "|pf_auto_remove_in_town";
            }
            if (b == 1)
                flagsString += "|pf_default_behavior";

            b = byte.Parse(cc[10].ToString());
            if (b >= 4)
            {
                b -= 4;
                flagsString += "|pf_show_faction";
            }
            if (b >= 2)
            {
                b -= 2;
                flagsString += "|pf_hide_defenders";
            }
            if (b == 1)
                flagsString += "|pf_limit_members";

            b = byte.Parse(cc[9].ToString());
            if (b >= 4)
            {
                b -= 4;
                flagsString += "|pf_civilian";
            }
            if (b == 2) /*//if (b >= 2)
            {
                b -= 2;*/
                flagsString += "|pf_dont_attack_civilians";
            /*}
            if (b == 1)
                flagsString += "|pf_is_hidden";*/

            b = byte.Parse(SkillHunter.Hex2Dec("000000" + cc[0] + cc[1]).ToString());
            if (b > 0)
                flagsString += "|carries_gold(" + b + ")";

            b = byte.Parse(SkillHunter.Hex2Dec("000000" + cc[2] + cc[3]).ToString());
            if (b > 0)
                flagsString += "|carries_goods(" + b + ")";

            flagsString = flagsString.Replace(" ", string.Empty).Trim('|');
        }

        public PMember[] Members { get { return pt_members; } }

        public string PartyTemplateName { get { return partyTemplateName; } }

        public int FactionID { get { return factionID; } }

        public string Faction { get { return faction; } }

        public ulong FlagsGZ { get { return flags; } }

        public string Flags { get { return flagsString; } }

        public int MenuID { get { return menuID; } }

        public int Personality { get { return personality; } }

    }
}