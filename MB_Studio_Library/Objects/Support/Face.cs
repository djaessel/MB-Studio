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

        private Skin troopType;

        #endregion

        #region Properties

        public string OriginalFaceCode { get; private set; }

        public string FaceCode { get { return GenerateNewFaceCode(); } }

        // - - - 

        public uint Age { get; private set; }

        public uint HairColorCode { get; private set; }
        public int HairColor { get; private set; }

        public uint Reserved1 { get; private set; } // check again

        public uint Skin { get; private set; }

        public uint Beard { get; private set; }

        public uint Hair { get; private set; }      // check again

        public uint Reserved2 { get; private set; } // check again

        // - - - 

        public int MaxSkin { get { return troopType.FaceTextures.Length - 1; } }

        public int MaxBeard { get { return troopType.BeardMeshes.Length; } } // in OpenBrf index = index - 1

        public int MaxHair { get { return troopType.HairMeshes.Length; } } // in OpenBrf index = index - 1

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
            string tmp = faceCode.Substring(7, 3);
            uint ageHairColor = uint.Parse(tmp, NumberStyles.HexNumber) & 0xFFF;
            Age = (ageHairColor & 0xFC0) >> 6; // check again
            HairColorCode = ageHairColor & 0x03F; // check again

            tmp = faceCode.Substring(10, 3);
            uint reserved1Skin = uint.Parse(tmp, NumberStyles.HexNumber) & 0xFFF;
            Reserved1 = (reserved1Skin & 0xFC0) >> 6; // check again
            Skin = reserved1Skin & 0x03F; // check again

            tmp = faceCode.Substring(13, 3);
            uint beardHair = uint.Parse(tmp, NumberStyles.HexNumber) & 0xFFF;
            Beard = (beardHair & 0xFC0) >> 6; // check again
            Hair = beardHair & 0x03F; // check again

            Reserved2 = uint.Parse(faceCode.Substring(16, 2), NumberStyles.HexNumber) & 0x08; // check again

            InitializeHairColor();
        }

        private void InitializeHairColor()
        {
            List<int> hairColorList = new List<int>();
            foreach (var color in troopType.FaceTextures[Skin].HairColors)
                hairColorList.Add((int)color);

            double hairPerc = 0x3F;
            hairPerc /= hairColorList.Count;

            int hairColorIdx = (int)(HairColorCode / hairPerc);
            if (hairColorIdx >= hairColorList.Count)
                hairColorIdx = hairColorList.Count - 1;

            int mergedColor = Color.FromArgb(byte.MaxValue, default(Color)).ToArgb();
            if (hairColorIdx >= 0)
                mergedColor = (int)((MergeColorsInList(hairColorList, hairColorIdx, HairColorCode, hairPerc) & 0x00FFFFFF) | 0xFF000000);

            // Later make mesh merge here with percentage
            //mergedColor = AddAgeColor(mergedColor, hairPerc);

            //mergedColor = Blend(Color.WhiteSmoke, Color.FromArgb(mergedColor), hairPerc).ToArgb();

            HairColor = mergedColor;
        }

        private static Color Blend(Color color, Color backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        private int AddAgeColor(int color, double hairPerc)
        {
            double percentage = Math.Round((int)Age / 63d, 4); // 63 max age

            Console.WriteLine("Add Age Color Percentage: " + (percentage * 100d) + " %");

            Color mac = Color.FromArgb(color);
            Color mic = Color.FromArgb(88, 88, 88);
            //Color mic = Color.FromArgb(byte.MaxValue, Color.White);

            /// TODO: age later merge diffuseA and diffuseB for color value

            int a = CalcValDifference(mac.A, mic.A, percentage);
            int r = CalcValDifference(mac.R, mic.R, percentage);
            int g = CalcValDifference(mac.G, mic.G, percentage);
            int b = CalcValDifference(mac.B, mic.B, percentage);

            int agedColor = Color.FromArgb(a, r, g, b).ToArgb();
            return agedColor;
        }

        private int MergeColorsInList(List<int> hairColors, int hairIdx, uint hairColorVal, double hairPerc)
        {
            int mergedColor;
            int mainColor = hairColors[hairIdx] & int.MaxValue;
            double percentage = Math.Round(hairColorVal % hairPerc / hairPerc, 4);

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

                mergedColor = Color.FromArgb(a, r, g, b).ToArgb(); // minor color check
            }
            else
            {
                mergedColor = mainColor; // main color
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

            uint ageM = Age << 6;
            uint reserved1M = Reserved1 << 6;
            uint beardM = Beard << 6;

            uint ageHairColor  = ageM | (HairColorCode & 0x3F); // 0xFFF -> FC0 + 03F
            uint reserved1Skin = reserved1M | (Skin & 0x3F);    // 0xFFF -> FC0 + 03F
            uint beardHair     = beardM | (Hair & 0x3F);        // 0xFFF -> FC0 + 03F
            //uint reserved2 = Reserved2 & 0x08;

            string ageHairHex = ageHairColor.ToString("X3");
            string reserved1SkinHex = reserved1Skin.ToString("X3");
            string beardHairHex = beardHair.ToString("X3");
            //string reserved2Hex = reserved2.ToString("X3");

            newFaceCode = newFaceCode.Substring(0, 7)
                 + ageHairHex
                 + reserved1SkinHex
                 + beardHairHex
               //+ reserved2Hex
                 + newFaceCode.Substring(16);
            
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
            Skin = skinIndex;
        }

        public void SetSkin(int skinIndex)
        {
            SetSkin((uint)skinIndex);
        }

        public void SetHair(uint hairIndex)
        {
            Hair = hairIndex;
        }

        public void SetHair(int hairIndex)
        {
            SetHair((uint)hairIndex);
        }

        public void SetBeard(uint beardIndex)
        {
            Beard = beardIndex;
        }

        public void SetBeard(int beardIndex)
        {
            SetBeard((uint)beardIndex);
        }

        public void SetHairColorP(uint hairColorCode)
        {
            HairColorCode = hairColorCode;
        }

        public void SetHairColorP(int hairColorCode)
        {
            SetHairColorP((uint)hairColorCode);
        }

        public void SetAgeP(uint agePercentage)
        {
            Age = agePercentage;
        }

        public void SetAgeP(int agePercentage)
        {
            SetAgeP((uint)agePercentage);
        }

        #endregion
    }
}
