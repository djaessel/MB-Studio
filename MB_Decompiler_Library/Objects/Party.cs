using System;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Party : Skriptum
    {
        private ulong flags;
        private double degrees;
        private PMember[] members;
        private double[] initialCoordinates = new double[2];
        private string partyName, partyTemplate, faction, flagsString;
        private int menuID, partyTemplateID, factionID, aiBehavior, aiTargetParty, personality;

        public Party(string[] raw_data) : base(raw_data[3].Substring(2), ObjectType.PARTY)
        {
            partyName = raw_data[4];
            flags = ulong.Parse(raw_data[5]);
            SetFlagsString();
            menuID = int.Parse(raw_data[6]);
            partyTemplateID = int.Parse(raw_data[7]);
            partyTemplate = CodeReader.PartyTemplates[partyTemplateID];
            factionID = int.Parse(raw_data[8]);
            faction = CodeReader.Factions[factionID];
            personality = int.Parse(raw_data[9]); //was ulong before
            aiBehavior = int.Parse(raw_data[11]); //was ulong before
            aiTargetParty = int.Parse(raw_data[12]); //was ulong before
            initialCoordinates[0] = Math.Round(double.Parse(CodeReader.Repl_DotWComma(raw_data[14])), 6);
            initialCoordinates[1] = Math.Round(double.Parse(CodeReader.Repl_DotWComma(raw_data[15])), 6);
            members = new PMember[int.Parse(raw_data[21])];
            for (int i = 0; i < members.Length; i++)
                members[i] = new PMember(new string[] { raw_data[(i * 4) + 22], raw_data[(i * 4) + 23], raw_data[(i * 4) + 24], raw_data[(i * 4) + 25] });
            degrees = Math.Round(double.Parse(CodeReader.Repl_DotWComma(raw_data[raw_data.Length - 1])), 6); //this.degrees = degrees;
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

        public string PartyName { get { return partyName; } }

        public string PartyTemplate { get { return partyTemplate; } }

        public int PartyTemplateID { get { return partyTemplateID; } }

        public string Faction { get { return faction; } }

        public int FactionID { get { return factionID; } }

        public ulong FlagsGZ { get { return flags; } }

        public string Flags { get { return flagsString; } }

        public int MenuID { get { return menuID; } }

        public int Personality { get { return personality; } } //was ulong before

        public int AIBehavior { get { return aiBehavior; } } //was ulong before

        public int AITargetParty { get { return aiTargetParty; } } //was ulong before

        public double[] InitialCoordinates { get { return initialCoordinates; } }

        public double PartyDirectionInDegrees { get { return degrees; } }

        public PMember[] Members { get { return members; } }

    }
}