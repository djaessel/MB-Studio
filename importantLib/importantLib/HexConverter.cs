using System;
using System.Globalization;
using System.Windows.Forms;

namespace importantLib
{
    public class HexConverter
    {
        private const byte MAX_NUM_OF_BITS = 96;
        private const string BIN_VALUES = "*0000*0001*0010*0011" + 
                                          "*0100*0101*0110*0111" + 
                                          "*1000*1001*1010*1011" + 
                                          "*1100*1101*1110*1111*";
        private const string BIN_VALUES_2 = "0000000100100011" + 
                                            "0100010101100111" + 
                                            "1000100110101011" + 
                                            "1100110111101111";

        private const string TWO_TO_THE_49TH_POWER = "562949953421312";
        private const string HEX_VALUES = "0123456789ABCDEF";
        private const string ZERO_8 = "00000000";

        private static string RightS(string sText, int nLen) // Method Right() in VB.NET nachgebaut
        {
            if (nLen > sText.Length)
                nLen = sText.Length;
            return (sText.Substring(sText.Length - nLen));
        }


        public static string Dec2Hex(object decimalIn, bool use16Chars = false)
        {
            string retur = string.Empty;

            try
            {
                string binaryString = string.Empty;

                decimalIn = long.Parse(decimalIn.ToString());
                while ((long)decimalIn != 0)
                {
                    binaryString = ((long)decimalIn - 2 * (long)Math.Round((long)decimalIn / 2d, 0)).ToString().Trim() + binaryString;
                    decimalIn = (long)Math.Round((long)decimalIn / 2d, 0);
                }

                binaryString = new string('0', (4 - binaryString.Length % 4) % 4) + binaryString;

                for (var x = 1; x <= binaryString.Length - 3; x += 4)
                    retur += HEX_VALUES.Substring(4 + (BIN_VALUES.IndexOf("*" + binaryString.Substring(x, 4) + "*") + 1) / 5, 1);//+ 1 because original was 1 base not 0 based!

                string placeHolder = ZERO_8;
                if (use16Chars)
                    placeHolder += ZERO_8;

                retur = RightS(placeHolder + retur, placeHolder.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERROR #111222 -> HEX_CONVERTER_WRONG_FORMAT:" + 
                        Environment.NewLine + ex.ToString(),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return retur;
        }

        public static string Dec2Hex_16CHARS(object decimalIn)
        {
            return Dec2Hex(decimalIn, true);
        }

        public static object Hex2Dec(string hexString, bool use16Char = false)
        {
            object retur = 0;

            try
            {
                string binaryString = string.Empty;

                if (hexString.Substring(0, 2).Equals("0x"))
                    hexString = hexString.Substring(2);

                if (hexString.Length <= 23)
                {
                    for (int i = 0; i < hexString.Length; i++)
                        binaryString += BIN_VALUES_2.Substring(4 * int.Parse(hexString.Substring(i, 1), NumberStyles.HexNumber) + 1, 4);
                    for (int i = 0; i < binaryString.Length; i++)
                    {
                        if (i < 50)
                            retur = (long)retur + long.Parse(binaryString.Substring(binaryString.Length - i - 1, 1)) * 2 ^ i;
                        else
                            retur = (long)retur + long.Parse(TWO_TO_THE_49TH_POWER) * long.Parse(binaryString.Substring(binaryString.Length - i - 1, 1)) * 2 ^ (i - 49);
                    }
                    retur = long.Parse(retur.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ERROR #333444 -> HEX_CONVERTER_WRONG_FORMAT:" +
                        Environment.NewLine + ex.ToString(),
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            return retur;
        }

        public static object Hex2Dec_16CHARS(string hexString)
        {
            return Hex2Dec(hexString, true);
        }
    }
}
