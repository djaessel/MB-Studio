using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects
{
    public class TableauMaterial : Skriptum
    {
        public TableauMaterial(string[] raw_data, bool source = false) : base(raw_data[0], ObjectType.TableauMaterial)
        {
            FlagsGZ = ulong.Parse(raw_data[1]);
            Flags = FlagsGZ.ToString();//change if flags available
            SampleMaterialName = raw_data[2];//change to name if good idea later
            Width = int.Parse(raw_data[3]);
            Height = int.Parse(raw_data[4]);
            MinX = int.Parse(raw_data[5]);
            MinY = int.Parse(raw_data[6]);
            MaxX = int.Parse(raw_data[7]);
            MaxY = int.Parse(raw_data[8]);

            if (!source)
            {
                OperationBlock = new string[int.Parse(raw_data[9]) + 1];
                OperationBlock[0] = ID;
                OperationBlock = CodeReader.GetStringArrayStartFromIndex(CodeReader.DecompileScriptCode(OperationBlock, CodeReader.GetStringArrayStartFromIndex(raw_data, 9)), 1);
            }
            else
            {
                OperationBlock = new string[raw_data.Length - 9];
                for (int i = 0; i < OperationBlock.Length; i++)
                    OperationBlock[i] = raw_data[i + 9];
            }
        }

        public ulong FlagsGZ { get; }

        public int Width { get; }

        public int Height { get; }

        public int MinX { get; }

        public int MinY { get; }

        public int MaxX { get; }

        public int MaxY { get; }

        public string SampleMaterialName { get; }

        public string Flags { get; }

        public string[] OperationBlock { get; }

    }
}