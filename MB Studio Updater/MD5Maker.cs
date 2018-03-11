using System;
using System.IO;
using System.Security.Cryptography;

namespace MB_Studio_Updater
{
    public class MD5Maker
    {
        public static byte[] CreateMD5Checksum(string filePath)
        {
            byte[] md5Check;
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                        md5Check = md5.ComputeHash(stream);
                }
            }
            catch (Exception)
            {
                md5Check = new byte[0];
            }
            return md5Check;
        }

        public static string ConvertMD5ChecksumToString(byte[] md5Checksum)
        {
            return BitConverter.ToString(md5Checksum).Replace("-", string.Empty).ToLower();
        }

        public static string CreateMD5ChecksumString(string filePath)
        {
            return ConvertMD5ChecksumToString(CreateMD5Checksum(filePath));
        }
    }
}
