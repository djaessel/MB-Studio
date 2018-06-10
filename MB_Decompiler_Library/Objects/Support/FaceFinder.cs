using importantLib;

namespace MB_Decompiler_Library.Objects.Support
{
    public class FaceFinder
    {
        public string[] FaceCodes { get; private set; } = new string[2];

        public string Face1
        {
            get { return FaceCodes[0]; }
            set { FaceCodes[0] = value; }
        }

        public string Face2
        {
            get { return FaceCodes[1]; }
            set { FaceCodes[1] = value; }
        }

        public void ReadFaceCode(string faceCodeLine)
        {
            string[] sp = faceCodeLine.Trim().Split();
            Face1 = ("0x" + HexConverter.Dec2Hex_16CHARS(sp[0]) + HexConverter.Dec2Hex_16CHARS(sp[1]) +
                            HexConverter.Dec2Hex_16CHARS(sp[2]) + HexConverter.Dec2Hex_16CHARS(sp[3])).ToLower();
            Face2 = ("0x" + HexConverter.Dec2Hex_16CHARS(sp[4]) + HexConverter.Dec2Hex_16CHARS(sp[5]) +
                            HexConverter.Dec2Hex_16CHARS(sp[6]) + HexConverter.Dec2Hex_16CHARS(sp[7])).ToLower();
        }

        public static string GetFaceCode(string face1, string face2)
        {
            char space = ' ';
            string ff = HexConverter.Hex2Dec(face2.Substring(19, 16)).ToString();

            long fx2 = long.Parse(ff);
            if (fx2 > 0)
                ff = (fx2 - 1).ToString();

            ff = space + string.Empty + space + HexConverter.Hex2Dec(face1.Substring(3, 16)) + space + HexConverter.Hex2Dec(face1.Substring(19, 16)) + space +
                HexConverter.Hex2Dec(face1.Substring(35, 16)) + space + HexConverter.Hex2Dec(face1.Substring(51, 16)) + space +
                HexConverter.Hex2Dec(face2.Substring(3, 16)) + space + ff + space + HexConverter.Hex2Dec(face2.Substring(3, 16)) + space +
                HexConverter.Hex2Dec(face2.Substring(51, 16)) + space;

            return ff;
        }
    }
}
