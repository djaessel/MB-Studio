using System;
using System.Globalization;
using System.Windows.Forms;

namespace importantLib
{
    public class HexConverter
    {
        #region Consts

        private const byte MAX_NUM_OF_BITS = 96;

        private const ulong TWO_TO_THE_49TH_POWER = 562949953421312;

        private const string BIN_VALUES = "0000"+"0001"+"0010"+"0011"+ 
                                          "0100"+"0101"+"0110"+"0111"+ 
                                          "1000"+"1001"+"1010"+"1011"+ 
                                          "1100"+"1101"+"1110"+"1111";

        private const string HEX_VALUES = "0123456789ABCDEF";

        private const string ZERO_8 = "00000000";

        private const string ZERO_16 = ZERO_8 + ZERO_8;

        #endregion

        #region Hex Methods

        public static string Dec2Hex(ulong decimalIn, bool use16Chars = false)
        {
            string retur = string.Empty;
            try
            {
                string binaryString = string.Empty;
                while (decimalIn != 0)
                {
                    ulong tmpX = decimalIn / 2ul;//Conversion.Int(input / 2M);
                    ulong zeroOrOne = decimalIn - 2 * tmpX;
                    binaryString = zeroOrOne + binaryString;
                    decimalIn = tmpX;
                }

                binaryString = new string('0', (4 - binaryString.Length % 4) % 4) + binaryString;

                for (int i = 0; i <= binaryString.Length - 4; i += 4)
                {
                    string tmp = binaryString.Substring(i, 4);
                    int binIndex = IndexOfBinValues(tmp);
                    retur += HEX_VALUES[binIndex];
                }

                string placeHolder = (use16Chars) ? ZERO_16 : ZERO_8;
                retur = RightS(placeHolder + retur, placeHolder.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERROR #111222 -> HEX_CONVERTER_WRONG_FORMAT:" + Environment.NewLine + ex.ToString(),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            return retur;
        }

        public static ulong Hex2Dec(string hexString, bool use16Char = false)
        {
            ulong retur = 0;
            try
            {
                if (hexString.StartsWith("0x"))
                    hexString = hexString.Substring(2);

                if (hexString.Length <= 23)
                {
                    string binaryString = string.Empty;

                    for (int i = 0; i < hexString.Length; i++)
                    {
                        int singleHex = int.Parse(hexString[i].ToString(), NumberStyles.HexNumber);
                        singleHex *= 4;
                        binaryString += BIN_VALUES.Substring(singleHex, 4);
                    }

                    uint constX = 49;
                    for (int i = 0; i < binaryString.Length; i++)
                    {
                        ulong power = (ulong)i;
                        int binIndex = binaryString.Length - i;
                        binIndex--;//VB to C# Index from 1-based to 0-based
                        ulong adder = ulong.Parse(binaryString[binIndex].ToString());

                        if (i > constX)
                        {
                            power -= constX;
                            adder *= TWO_TO_THE_49TH_POWER;
                        }

                        adder *= (ulong)Math.Pow(2d, power);
                        retur += adder;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERROR #333444 -> HEX_CONVERTER_WRONG_FORMAT:" + Environment.NewLine + ex.ToString(),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            return retur;
        }

        public static void ConvertSingleHexCodeToIntArray(string hexCode, out int[] array)
        {
            array = new int[hexCode.Length];
            char[] cc = hexCode.ToCharArray();
            for (int i = 0; i < hexCode.Length; i++)
                array[i] = ReplaceHexToInt(cc[i]);
        }

        public static int ReplaceHexToInt(char hexChar)
        {
            bool isNum = int.TryParse(hexChar.ToString(), out int retur);
            if (isNum) return retur;
            switch (hexChar)
            {
                case 'a':
                case 'A':
                    retur = 10;
                    break;
                case 'b':
                case 'B':
                    retur = 11;
                    break;
                case 'c':
                case 'C':
                    retur = 12;
                    break;
                case 'd':
                case 'D':
                    retur = 13;
                    break;
                case 'e':
                case 'E':
                    retur = 14;
                    break;
                case 'f':
                case 'F':
                    retur = 15;
                    break;
            }
            return retur;
        }

        public static string ReplaceHexToIntString(char hexChar)
        {
            return ReplaceHexToInt(hexChar).ToString();
        }

        #endregion

        #region Helper Methods

        public static string Dec2Hex(string decimalIn, bool use16Chars = false)
        {
            return Dec2Hex(ulong.Parse(decimalIn), use16Chars);
        }

        public static string Dec2Hex(int decimalIn, bool use16Chars = false)
        {
            return Dec2Hex((ulong)decimalIn, use16Chars);
        }

        public static string Dec2Hex_16CHARS(ulong decimalIn)
        {
            return Dec2Hex(decimalIn, true);
        }

        public static string Dec2Hex_16CHARS(string decimalIn)
        {
            return Dec2Hex_16CHARS(ulong.Parse(decimalIn));
        }

        public static ulong Hex2Dec_16CHARS(string hexString)
        {
            return Hex2Dec(hexString, true);
        }

        private static int IndexOfBinValues(string binText)
        {
            int ret = -1;
            for (int i = 0; i < BIN_VALUES.Length; i++)
            {
                if (BIN_VALUES.Substring(i * 4, 4).Equals(binText))
                {
                    ret = i;
                    i = BIN_VALUES.Length;
                }
            }
            return ret;
        }

        private static string RightS(string sText, int nLen) // Method Right() in VB.NET nachgebaut
        {
            if (nLen > sText.Length)
                nLen = sText.Length;
            return (sText.Substring(sText.Length - nLen));
        }

        #endregion
    }
}
