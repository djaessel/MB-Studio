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

        private const string BIN_VALUES = "*0000*0001*0010*0011" + 
                                          "*0100*0101*0110*0111" + 
                                          "*1000*1001*1010*1011" + 
                                          "*1100*1101*1110*1111*";

        private const string BIN_VALUES_2 = "0000000100100011" + 
                                            "0100010101100111" + 
                                            "1000100110101011" + 
                                            "1100110111101111";

        private const string HEX_VALUES = "0123456789ABCDEF";

        private const string ZERO_8 = "00000000";

        private const string ZERO_16 = ZERO_8 + ZERO_8;

        #endregion

        #region Hex Methods

        public static string Dec2Hex(object decimalIn, bool use16Chars = false)
        {
            string retur = string.Empty;

            try
            {
                string binaryString = string.Empty;

                ulong input = ulong.Parse(decimalIn.ToString());
                while (input != 0)
                {
                    binaryString = (input - 2 * (ulong)Math.Round(input / 2d, 0)).ToString().Trim() + binaryString;
                    input = (ulong)Math.Round(input / 2d, 0);
                }

                binaryString = new string('0', (4 - binaryString.Length % 4) % 4) + binaryString;

                for (int i = 0; i <= binaryString.Length - 4; i += 4)
                    retur += HEX_VALUES.Substring(4 + (BIN_VALUES.IndexOf("*" + binaryString.Substring(i, 4) + "*")) / 4, 1);

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

        public static object Hex2Dec(string hexString, bool use16Char = false)
        {
            ulong retur = 0;

            try
            {
                string binaryString = string.Empty;

                if (hexString.Length > 2)
                    if (hexString.Substring(0, 2).Equals("0x"))
                        hexString = hexString.Substring(2);

                if (hexString.Length <= 23)
                {
                    for (int i = 0; i < hexString.Length; i++)
                        binaryString += BIN_VALUES_2.Substring(4 * int.Parse(hexString.Substring(i, 1), NumberStyles.HexNumber), 4);

                    for (int i = 0; i < binaryString.Length; i++)
                    {
                        if (i < 50)
                            retur += ulong.Parse(binaryString.Substring(binaryString.Length - i - 1, 1)) * (ulong)(2 ^ i);
                        else
                            retur += TWO_TO_THE_49TH_POWER * ulong.Parse(binaryString.Substring(binaryString.Length - i - 1, 1)) * (ulong)(2 ^ (i - 49));
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

        #endregion

        #region Helper Methods

        public static string Dec2Hex_16CHARS(object decimalIn)
        {
            return Dec2Hex(decimalIn, true);
        }

        public static object Hex2Dec_16CHARS(string hexString)
        {
            return Hex2Dec(hexString, true);
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
