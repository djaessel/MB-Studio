using importantLib;
using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Scene : Skriptum
    {
        private ulong flagsGZ;
        private string meshName, flags, bodyName, terrainCode, terrainBase;
        private double[] minPos, maxPos;
        private double waterLevel;
        private string[] otherScenes, chestTroops;

        public Scene(string[] raw_data, string[] otherScenes, string[] chestTroops, string terrainBase) : base(raw_data[1], ObjectType.SCENE)
        {
            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                flagsGZ = ulong.Parse(raw_data[2]);
                SetFlags();
            }
            else
            {
                flags = raw_data[2];
                SetFlagsGZ();
            }
            meshName = raw_data[3];
            bodyName = raw_data[4];
            minPos = new double[] { double.Parse(CodeReader.Repl_DotWComma(raw_data[5])), double.Parse(CodeReader.Repl_DotWComma(raw_data[6])) };
            maxPos = new double[] { double.Parse(CodeReader.Repl_DotWComma(raw_data[7])), double.Parse(CodeReader.Repl_DotWComma(raw_data[8])) };
            waterLevel = double.Parse(CodeReader.Repl_DotWComma(raw_data[9]));
            terrainCode = raw_data[10];
            this.otherScenes = otherScenes;
            this.chestTroops = chestTroops;
            this.terrainBase = terrainBase;
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = flags.Split('|');

            foreach (string flag in sp)
            {
                if (flag.Equals("sf_indoors"))
                    flagsGZ |= 0x00000001; //The scene shouldn't have a skybox and lighting by sun
                else if (flag.Equals("sf_force_skybox"))
                    flagsGZ |= 0x00000002; //Force adding a skybox even if indoors flag is set
                else if (flag.Equals("sf_generate"))
                    flagsGZ |= 0x00000100; //Generate terrain by terran-generator
                else if (flag.Equals("sf_randomize"))
                    flagsGZ |= 0x00000200; //Randomize terrain generator key
                else if (flag.Equals("sf_auto_entry_points"))
                    flagsGZ |= 0x00000400; //Automatically create entry points
                else if (flag.Equals("sf_no_horses"))
                    flagsGZ |= 0x00000800; //Horses are not avaible
                else if (flag.Equals("sf_muddy_water"))
                    flagsGZ |= 0x00001000; //Changes the shader of the river mesh
            }

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((flagsGZ & 0x0001) == 0x0001)
                flags += "sf_indoors|";
            if ((flagsGZ & 0x0002) == 0x0002)
                flags += "sf_force_skybox|";
            if ((flagsGZ & 0x0100) == 0x0100)
                flags += "sf_generate|";
            if ((flagsGZ & 0x0200) == 0x0200)
                flags += "sf_randomize|";
            if ((flagsGZ & 0x0400) == 0x0400)
                flags += "sf_auto_entry_points|";
            if ((flagsGZ & 0x0800) == 0x0800)
                flags += "sf_no_horses|";
            if ((flagsGZ & 0x1000) == 0x1000)
                flags += "sf_muddy_water|";

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = flagsGZ.ToString();

            this.flags = flags;
        }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public string MeshName { get { return meshName; } }

        public string BodyName { get { return bodyName; } }

        public string TerrainCode { get { return terrainCode; } }

        public string TerrainBase { get { return terrainBase; } }

        public double WaterLevel { get { return waterLevel; } }

        public double[] MinPosition { get { return minPos; } }

        public double[] MaxPosition { get { return maxPos; } }

        public string[] OtherScenes { get { return otherScenes; } }

        public string[] ChestTroops { get { return chestTroops; } }

    }
}