using System;
using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Scene : Skriptum
    {
        private ulong flags;
        private string meshName, bodyName, terrainCode, terrainBase;
        private double[] minPos, maxPos;
        private double waterLevel;
        private string[] otherScenes, chestTroops;

        public Scene(string[] raw_data, string[] otherScenes, string[] chestTroops, string terrainBase) : base(raw_data[1], ObjectType.SCENE)
        {
            flags = ulong.Parse(raw_data[2]);
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

        public ulong Flags { get { return flags; } }

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