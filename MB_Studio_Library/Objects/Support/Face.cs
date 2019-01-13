using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace MB_Studio_Library.Objects.Support
{
    public class Face
    {
        #region Consts

        public const int MAX_AGE_P = 0x3F; // 0 - 63 percentage
        public const int MAX_HAIR_COLOR_P = 0x3F; // 0 - 63 percentage

        #endregion

        #region Attributes

        private uint age;
        private uint hairColorCode;
        private int hairColor;
        private uint reserved1;
        private uint skin;
        private uint beard;
        private uint hair;
        private uint reserved2;

        private Skin troopType;

        #endregion

        #region Properties

        public string OriginalFaceCode { get; private set; }

        public string FaceCode { get { return GenerateNewFaceCode(); } }

        // - - - 

        public uint Age { get { return age; } }

        public uint HairColorCode { get { return hairColorCode; } }
        public int HairColor { get { return hairColor; } }

        public uint Reserved1 { get { return reserved1; } } // check again

        public uint Skin { get { return skin; } }

        public uint Beard { get { return beard; } }

        public uint Hair { get { return hair; } }           // check again

        public uint Reserved2 { get { return reserved2; } } // check again

        // - - - 

        public int MaxSkin { get { return troopType.FaceTextures.Length - 1; } }

        public int MaxBeard { get { return troopType.BeardMeshes.Length - 1; } }

        public int MaxHair { get { return troopType.HairMeshes.Length - 1; } }

        #endregion

        public Face(string faceCode, Skin troopType)
        {
            this.troopType = troopType;

            if (faceCode.StartsWith("0x"))
                faceCode = faceCode.Substring(2);

            OriginalFaceCode = faceCode;

            InitializeValues(faceCode);
        }

        private void InitializeValues(string faceCode)
        {
            age = ((uint.Parse(faceCode.Substring(7, 2), NumberStyles.HexNumber) & 0xFC) >> 3) / 4;
            hairColorCode = uint.Parse(faceCode.Substring(8, 2), NumberStyles.HexNumber) & 0x3F;
            reserved1 = (uint.Parse(faceCode.Substring(10, 2), NumberStyles.HexNumber) & 0xFC) >> 3;    // check again
            skin = uint.Parse(faceCode.Substring(11, 2), NumberStyles.HexNumber) & 0x3F;
            beard = ((uint.Parse(faceCode.Substring(13, 2), NumberStyles.HexNumber) & 0xFC) >> 3) / 4;
            hair = uint.Parse(faceCode.Substring(14, 2), NumberStyles.HexNumber) & 0x3F;                // check again
            reserved2 = uint.Parse(faceCode.Substring(16, 2), NumberStyles.HexNumber) & 0x08;           // check again

            InitializeHairColor();
        }

        private void InitializeHairColor()
        {
            List<int> hairColorList = new List<int>();
            foreach (var color in troopType.FaceTextures[skin].HairColors)
                hairColorList.Add((int)color);

            double hairPerc = 0x3F;
            hairPerc /= hairColorList.Count;

            int hairColorIdx = (int)(hairColorCode / hairPerc);
            if (hairColorIdx >= hairColorList.Count)
                hairColorIdx = hairColorList.Count - 1;

            int mergedColor = Color.FromArgb(byte.MaxValue, default(Color)).ToArgb();
            if (hairColorIdx >= 0)
                mergedColor = (int)((MergeColorsInList(hairColorList, hairColorIdx, hairColorCode, hairPerc) & 0x00FFFFFF) | 0xFF000000);

            hairColor = mergedColor;
        }

        private int MergeColorsInList(List<int> hairColors, int hairIdx, uint hairColorVal, double hairPerc)
        {
            int mergedColor;
            int mainColor = hairColors[hairIdx] & int.MaxValue;
            double percentage = Math.Round((hairColorVal % hairPerc) / hairPerc, 4);

            Console.WriteLine("Percentage: " + percentage);

            if (hairColors.Count > 1 && percentage > 0d && percentage != hairPerc)
            {
                if (hairIdx < hairColors.Count - 1)
                    hairIdx++; // upper color
                else
                    hairIdx--; // lower color

                int minorColor = hairColors[hairIdx];

                Color mac = Color.FromArgb(mainColor);
                Color mic = Color.FromArgb(minorColor);

                int a = CalcValDifference(mac.A, mic.A, percentage);
                int r = CalcValDifference(mac.R, mic.R, percentage);
                int g = CalcValDifference(mac.G, mic.G, percentage);
                int b = CalcValDifference(mac.B, mic.B, percentage);

                mergedColor = Color.FromArgb(a, r, g, b).ToArgb();

                Console.WriteLine("Minor Color: " + Color.FromArgb(minorColor));
            }
            else
            {
                mergedColor = mainColor;
                Console.WriteLine("Used main color!");
            }

            return mergedColor;
        }

        private int CalcValDifference(int mac, int mic, double percentage)
        {
            int differ;
            if (mac > mic)
            {
                differ = mac - mic;
                mac -= (int)(differ * percentage);
            }
            else
            {
                differ = mic - mac;
                mac += (int)(differ * percentage);
            }
            return mac;
        }

        private string GenerateNewFaceCode()
        {
            string newFaceCode = OriginalFaceCode;

            // generate left settings for face here

            newFaceCode = newFaceCode.Substring(0, 7) +
                (((age * 4) << 19) | hairColorCode).ToString("X3") +
                ((reserved1 << 19) | skin).ToString("X3") +
                (((beard * 4) << 19) | hair).ToString("X3") +
                //reserved2.ToString("X1") +
                newFaceCode.Substring(16);
            
            return newFaceCode; 
        }

        public static Face MergeTroopFaces(Troop troop)
        {
            var troopType = troop.GetTroopType();

            var face1 = new Face(troop.Face1, troopType);
            var face2 = new Face(troop.Face2, troopType);

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

        #region Set Methods

        public void SetSkin(uint skinIndex)
        {
            skin = skinIndex;
        }

        public void SetSkin(int skinIndex)
        {
            SetSkin((uint)skinIndex);
        }

        public void SetHair(uint hairIndex)
        {
            hair = hairIndex;
        }

        public void SetHair(int hairIndex)
        {
            SetHair((uint)hairIndex);
        }

        public void SetBeard(uint beardIndex)
        {
            skin = beardIndex;
        }

        public void SetBeard(int beardIndex)
        {
            SetBeard((uint)beardIndex);
        }

        public void SetHairColorP(uint hairColorCode)
        {
            skin = hairColorCode;
        }

        public void SetHairColorP(int hairColorCode)
        {
            SetHairColorP((uint)hairColorCode);
        }

        public void SetAgeP(uint agePercentage)
        {
            skin = agePercentage;
        }

        public void SetAgeP(int agePercentage)
        {
            SetAgeP((uint)agePercentage);
        }

        #endregion
    }
}
