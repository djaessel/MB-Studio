using System.Globalization;

namespace MB_Studio_Library.Objects.Support
{
    public class Face
    {
        #region Attributes

        private uint
            age,
            hairColorCode,
            reserved1,
            skin,
            beard,
            hair,
            reserved2
            ;

        #endregion

        #region Properties

        public string OriginalFaceCode { get; private set; }

        public string FaceCode { get { return GenerateNewFaceCode(); } }

        // - - - 

        public uint Age { get { return age; } }

        public uint HairColorCode { get { return hairColorCode; } }

        public uint Reserved1 { get { return reserved1; } } // check again

        public uint Skin { get { return skin; } }

        public uint Beard { get { return beard; } }

        public uint Hair { get { return hair; } }           // check again

        public uint Reserved2 { get { return reserved2; } } // check again

        #endregion

        public Face(string faceCode)
        {
            if (faceCode.StartsWith("0x"))
                faceCode = faceCode.Substring(2);

            OriginalFaceCode = faceCode;

            age = ((uint.Parse(faceCode.Substring(7, 2), NumberStyles.HexNumber) & 0xFC) >> 3) / 4;
            
            hairColorCode = uint.Parse(faceCode.Substring(8, 2), NumberStyles.HexNumber) & 0x3F;

            reserved1 = (uint.Parse(faceCode.Substring(10, 2), NumberStyles.HexNumber) & 0xFC) >> 3;    // check again

            skin = uint.Parse(faceCode.Substring(11, 2), NumberStyles.HexNumber) & 0x3F;

            beard = ((uint.Parse(faceCode.Substring(13, 2), NumberStyles.HexNumber) & 0xFC) >> 3) / 4;

            hair = uint.Parse(faceCode.Substring(14, 2), NumberStyles.HexNumber) & 0x3F;                // check again

            reserved2 = uint.Parse(faceCode.Substring(16, 2), NumberStyles.HexNumber) & 0x08;           // check again
        }

        private string GenerateNewFaceCode()
        {
            string ret = string.Empty;

            // generate new code here
            ret = OriginalFaceCode; // just for testing - delete after you developed algorithm!

            return ret; 
        }

        public static Face MergeTroopFaces(Troop troop)
        {
            var face1 = new Face(troop.Face1);
            var face2 = new Face(troop.Face2);

            return MergeFaces(face1, face2);
        }

        public static Face MergeFaces(Face face1, Face face2)
        {
            Face newFace;
            if (face2.OriginalFaceCode.Trim('0').Length != 0)
            {
                newFace = face1;
                // merge faces instead of the above later !!!
            }
            else
                newFace = face1;
            return newFace;
        }
    }
}
