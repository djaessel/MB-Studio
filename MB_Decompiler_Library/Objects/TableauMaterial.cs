using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class TableauMaterial : Skriptum
    {
        private ulong flagsGZ;
        private int width, height, minX, minY, maxX, maxY;
        private string sampleMaterialName;
        private string[] operationBlock;

        public TableauMaterial(string[] raw_data, bool source = false) : base(raw_data[0], ObjectType.TABLEAU_MATERIAL)
        {
            flagsGZ = ulong.Parse(raw_data[1]);//change if flags available
            sampleMaterialName = raw_data[2];
            width = int.Parse(raw_data[3]);
            height = int.Parse(raw_data[4]);
            minX = int.Parse(raw_data[5]);
            minY = int.Parse(raw_data[6]);
            maxX = int.Parse(raw_data[7]);
            maxY = int.Parse(raw_data[8]);

            if (!source)
            {
                operationBlock = new string[int.Parse(raw_data[9]) + 1];
                operationBlock[0] = ID;
                operationBlock = CodeReader.GetStringArrayStartFromIndex(CodeReader.DecompileScriptCode(operationBlock, CodeReader.GetStringArrayStartFromIndex(raw_data, 9)), 1);
            }
            else
            {
                operationBlock = new string[raw_data.Length - 9];
                for (int i = 0; i < operationBlock.Length; i++)
                    operationBlock[i] = raw_data[i + 9];
            }
        }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        public int MinX { get { return minX; } }

        public int MinY { get { return minY; } }

        public int MaxX { get { return maxX; } }

        public int MaxY { get { return maxY; } }

        public string SampleMaterialName { get { return sampleMaterialName; } }

        public string[] OperationBlock { get { return operationBlock; } }

    }
}