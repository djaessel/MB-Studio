using System;
using System.Collections.Generic;
using importantLib;
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
        private string menuIDString, partyName, partyTemplate, faction, flagsString;
        private int menuID, partyTemplateID, factionID, aiBehavior, aiTargetParty, personality;

        private static readonly string[] ai_behaviors = {
            "ai_bhvr_hold",
            "ai_bhvr_travel_to_party",
            "ai_bhvr_patrol_location",
            "ai_bhvr_patrol_party",
            "ai_bhvr_attack_party",//"ai_bhvr_track_party" #deprecated, use the alias ai_bhvr_attack_party instead.
            "ai_bhvr_avoid_party",
            "ai_bhvr_travel_to_point",
            "ai_bhvr_negotiate_party",
            "ai_bhvr_in_town",
            "ai_bhvr_travel_to_ship",
            "ai_bhvr_escort_party",
            "ai_bhvr_driven_by_party"
        };

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

        public Party(string[] source_data, bool hasDegree) : base(source_data[0], ObjectType.PARTY)
        {
            partyName = source_data[1];

            flagsString = source_data[2];
            SetFlagsGZ();

            menuIDString = source_data[3];
            if (menuIDString.Equals("no_menu"))
                menuID = 0;
            else
                for (int i = 0; i < CodeReader.GameMenus.Length; i++)
                {
                    if (CodeReader.GameMenus[i].Equals(menuIDString))
                    {
                        menuID = i;
                        i = CodeReader.GameMenus.Length;
                    }
                }

            partyTemplate = source_data[4];
            for (int i = 0; i < CodeReader.PartyTemplates.Length; i++)
            {
                if (CodeReader.PartyTemplates[i].Equals(partyTemplate))
                {
                    partyTemplateID = i;
                    i = CodeReader.PartyTemplates.Length;
                }
            }

            faction = source_data[5];
            if (faction.Equals("no_faction"))
                factionID = -1;
            else
                for (int i = 0; i < CodeReader.Factions.Length; i++)
                {
                    if (CodeReader.Factions[i].Equals(faction))
                    {
                        factionID = i;
                        i = CodeReader.Factions.Length;
                    }
                }

            int x = 0;
            string[] tmp = source_data[6].Split('|');
            string[] tmp2;
            foreach (string s in tmp)
            {
                tmp2 = s.Split('_');
                if (tmp2[0].Equals("courage"))
                    x |= int.Parse(tmp2[1]);
                else if (tmp2[0].Equals("aggressiveness"))
                    x |= int.Parse(tmp2[1]) << 4;
                else if (tmp2[0].Equals("banditness"))
                    x |= 0x0100;
            }
            personality = x;

            x = 0;
            tmp = source_data[7].Split('|');
            foreach (string s in tmp)
            {
                for (int i = 0; i < ai_behaviors.Length; i++)
                {
                    if (ai_behaviors[i].Equals(s))
                    {
                        x |= i;
                        i = ai_behaviors.Length;
                    }
                }
            }
            aiBehavior = x;

            x = 0;
            tmp = new string[] { source_data[8] };
            for (int i = 0; i < CodeReader.Parties.Length; i++)
            {
                if (CodeReader.Parties[i].Equals(tmp[0]))
                {
                    x = i;
                    i = CodeReader.Parties.Length;
                }
            }
            aiTargetParty = x;

            initialCoordinates[0] = Math.Round(double.Parse(CodeReader.Repl_DotWComma(source_data[9].TrimStart('(', ' '))), 6);
            initialCoordinates[1] = Math.Round(double.Parse(CodeReader.Repl_DotWComma(source_data[10].TrimEnd(')', ' '))), 6);

            x = 0;
            if (hasDegree)
                x++;
            x = (source_data.Length - (11 + x)) / 3;

            members = new PMember[x];
            for (int i = 0; i < x; i++)
            {
                if (!ImportantMethods.IsNumericGZ(source_data[11 + i * 3]))
                {
                    for (int j = 0; j < CodeReader.Troops.Length; j++)
                    {
                        if (CodeReader.Troops[j].Equals(source_data[11 + i * 3]))
                        {
                            source_data[11 + i * 3] = j.ToString();
                            j = CodeReader.Troops.Length;
                        }
                    }
                }
                if (source_data[13 + i * 3].Equals("pmf_is_prisoner"))
                    source_data[13 + i * 3] = "1";
                if (!ImportantMethods.IsNumericGZ(source_data[13 + i * 3]))
                    DisplayErrorMessage();
                members[i] = new PMember(new string[] { source_data[11 + i * 3], source_data[12 + i * 3], source_data[13 + i * 3] });
            }

            if (hasDegree)
                degrees = Math.Round(double.Parse(CodeReader.Repl_DotWComma(source_data[source_data.Length - 1].TrimEnd(')', ' '))), 6);
            else
                degrees = 0d;
        }

        private static void DisplayErrorMessage()
        {
            System.Windows.Forms.MessageBox.Show("ERROR - 0x9930 - Party", "ERROR");
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

        private void SetFlagsGZ()
        {
            int x;
            ulong flags = 0;

            List<string> flagsList = new List<string>(flagsString.Split('|'));

            if (flagsList.Contains("pf_disabled"))
                flags |= 0x00000100;
            if (flagsList.Contains("pf_is_ship"))
                flags |= 0x00000200;
            if (flagsList.Contains("pf_is_static"))
                flags |= 0x00000400;

            if (flagsList.Contains("pf_label_medium"))
                flags |= 0x00001000;
            if (flagsList.Contains("pf_label_large"))
                flags |= 0x00002000;

            if (flagsList.Contains("pf_always_visible"))
                flags |= 0x00004000;
            if (flagsList.Contains("pf_default_behavior"))
                flags |= 0x00010000;
            if (flagsList.Contains("pf_auto_remove_in_town"))
                flags |= 0x00020000;
            if (flagsList.Contains("pf_quest_party"))
                flags |= 0x00040000;
            if (flagsList.Contains("pf_no_label"))
                flags |= 0x00080000;
            if (flagsList.Contains("pf_limit_members"))
                flags |= 0x00100000;
            if (flagsList.Contains("pf_hide_defenders"))
                flags |= 0x00200000;
            if (flagsList.Contains("pf_show_faction"))
                flags |= 0x00400000;
            //if (flagsList.Contains("pf_is_hidden"))
            //    flags |= 0x01000000; //#used in the engine, do not overwrite this flag
            if (flagsList.Contains("pf_dont_attack_civilians"))
                flags |= 0x02000000;
            if (flagsList.Contains("pf_civilian"))
                flags |= 0x04000000;

            foreach (string flagS in flagsList)
            {
                if (flagS.Contains("carries_"))
                {
                    x = int.Parse(flagS.Substring(flagS.IndexOf('(') + 1).Split(')')[0].Trim());
                    if (flagS.Contains("goods"))
                        flags |= (ulong)(x << 48) & 0x00ff000000000000;
                    else
                        flags |= (ulong)((x / 20) << 56) & 0xff00000000000000;
                }
                else if (flagS.StartsWith("icon_"))
                {
                    for (int i = 0; i < CodeReader.MapIcons.Length; i++)
                    {
                        if (CodeReader.MapIcons[i].Equals(flags))
                        {
                            flags |= (uint)i;
                            i = CodeReader.MapIcons.Length;
                        }
                    }
                }
            }

            this.flags = flags;
        }

        public string Menu { get { return menuIDString; } }

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