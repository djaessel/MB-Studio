using importantLib;
using MB_Studio_Library.Objects.Support;
using System.Collections.Generic;
using System.IO;

namespace MB_Studio_Library.Objects
{
    public class MissionTemplate : Skriptum
    {
        private static List<HeaderVariable> missionTypes = null;
        private static List<HeaderVariable> headerVariables = null;
        private List<Entrypoint> entryPoints = new List<Entrypoint>();
        private List<Trigger> triggers = new List<Trigger>();

        public MissionTemplate(string[] headerInfo) : base(headerInfo[0], ObjectType.MissionTemplate) // base(name, type)
        {
            if (headerVariables == null)
                headerVariables = GetHeaderVariableList();

            if (missionTypes == null)
                missionTypes = GetMissionTypes();

            if (ImportantMethods.IsNumericGZ(headerInfo[1]))
            {
                FlagsGZ = ulong.Parse(headerInfo[1]);
                Flags = SetFlags(FlagsGZ, headerVariables);
            }
            else
            {
                Flags = headerInfo[1];
                FlagsGZ = SetFlagsGZ(Flags, headerVariables);
            }

            if (ImportantMethods.IsNumericGZ(headerInfo[2]))
            {
                MissionTypeGZ = int.Parse(headerInfo[2]);
                MissionType = SetFlags(FlagsGZ, headerVariables);
            }
            else
            {
                MissionType = headerInfo[2];
                MissionTypeGZ = (int)SetFlagsGZ(Flags, headerVariables);
            }

            MissionType = headerInfo[2];
            Description = headerInfo[3];
        }

        public void AddEntryPoint(Entrypoint entryPoint) { entryPoints.Add(entryPoint); }

        public void AddTrigger(Trigger trigger) { triggers.Add(trigger); }

        public string[] HeaderInfo { get { return new string[] { ID, Flags, MissionType, Description }; } }

        public string Flags { get; private set; }

        public ulong FlagsGZ { get; private set; }

        public string MissionType { get; }

        public int MissionTypeGZ { get; }

        public string Description { get; }

        public Entrypoint[] EntryPoints { get { return entryPoints.ToArray(); } }

        public Trigger[] Triggers { get { return triggers.ToArray(); } }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private string SetFlags(ulong gz, List<HeaderVariable> variables)
        {
            string text = string.Empty;
            foreach (HeaderVariable variable in variables)
            {
                ulong x = ulong.Parse(variable.VariableValue);
                if ((x & gz) == x)
                    text += variable.VariableName + '|';
            }

            if (text.Length != 0)
                text = text.TrimEnd('|');
            else
                text = gz.ToString();

            return text;
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private ulong SetFlagsGZ(string text, List<HeaderVariable> variables)
        {
            ulong gz = 0;
            string[] sp = text.Split('|');
            foreach (HeaderVariable variable in variables)
                foreach (string flag in sp)
                    if (variable.VariableName.Equals(flag))
                        gz |= ulong.Parse(variable.VariableValue);
            return gz;
        }

        //place normalized version in Skriptum ? for every Skriptum Object and public
        private List<HeaderVariable> GetHeaderVariableList(string file = "header_mission_templates.py")
        {
            string[] tmp;
            string s = string.Empty;
            List<HeaderVariable> list = new List<HeaderVariable>();
            using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + "moduleSystem\\" + file))
            {
                while (!sr.EndOfStream && !s.Equals(string.Empty))
                {
                    s = sr.ReadLine().Split('#')[0].Trim();
                    if (s.Length != 0 && s.Contains("="))
                    {
                        tmp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');

                        s = HexConverter.Hex2Dec(tmp[1].Substring(2).TrimStart('0')).ToString();

                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            if (list[i].VariableValue.Equals(s))
                            {
                                list.RemoveAt(i);//if no conflict remove comment
                                i = list.Count;
                            }
                        }

                        list.Add(new HeaderVariable(s, tmp[0]));
                    }
                }
            }
            return list;
        }

        private List<HeaderVariable> GetMissionTypes(string file = "header_mission_types.py")
        {
            string[] tmp;
            string s = string.Empty;
            List<HeaderVariable> list = new List<HeaderVariable>();
            using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + "moduleSystem\\" + file))
            {
                while (!sr.EndOfStream && !s.Equals(string.Empty))
                {
                    s = sr.ReadLine().Split('#')[0].Trim();
                    if (s.Length != 0 && s.Contains("="))
                    {
                        tmp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');

                        s = HexConverter.Hex2Dec(tmp[1].Substring(2).TrimStart('0')).ToString();

                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            if (list[i].VariableValue.Equals(s))
                            {
                                list.RemoveAt(i);//if no conflict remove comment
                                i = list.Count;
                            }
                        }

                        list.Add(new HeaderVariable(s, tmp[0]));
                    }
                }
            }
            return list;
        }
    }
}
