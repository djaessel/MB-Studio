using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects
{
    public class Scene : Skriptum
    {
        public Scene(string[] raw_data, string[] otherScenes, string[] chestTroops, string terrainBase) : base(raw_data[1], ObjectType.Scene)
        {
            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                FlagsGZ = ulong.Parse(raw_data[2]);
                SetFlags();
            }
            else
            {
                Flags = raw_data[2];
                SetFlagsGZ();
            }
            MeshName = raw_data[3];
            BodyName = raw_data[4];
            MinPosition = new double[] { double.Parse(CodeReader.Repl_DotWComma(raw_data[5])), double.Parse(CodeReader.Repl_DotWComma(raw_data[6])) };
            MaxPosition = new double[] { double.Parse(CodeReader.Repl_DotWComma(raw_data[7])), double.Parse(CodeReader.Repl_DotWComma(raw_data[8])) };
            WaterLevel = double.Parse(CodeReader.Repl_DotWComma(raw_data[9]));
            TerrainCode = raw_data[10];
            OtherScenes = otherScenes;
            ChestTroops = chestTroops;
            TerrainBase = terrainBase;
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = Flags.Split('|');

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

            this.FlagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((FlagsGZ & 0x0001) == 0x0001)
                flags += "sf_indoors|";
            if ((FlagsGZ & 0x0002) == 0x0002)
                flags += "sf_force_skybox|";
            if ((FlagsGZ & 0x0100) == 0x0100)
                flags += "sf_generate|";
            if ((FlagsGZ & 0x0200) == 0x0200)
                flags += "sf_randomize|";
            if ((FlagsGZ & 0x0400) == 0x0400)
                flags += "sf_auto_entry_points|";
            if ((FlagsGZ & 0x0800) == 0x0800)
                flags += "sf_no_horses|";
            if ((FlagsGZ & 0x1000) == 0x1000)
                flags += "sf_muddy_water|";

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = FlagsGZ.ToString();

            this.Flags = flags;
        }

        public ulong FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public string MeshName { get; }

        public string BodyName { get; }

        public string TerrainCode { get; }

        public string TerrainBase { get; }

        public double WaterLevel { get; }

        public double[] MinPosition { get; }

        public double[] MaxPosition { get; }

        public string[] OtherScenes { get; }

        public string[] ChestTroops { get; }

    }
}