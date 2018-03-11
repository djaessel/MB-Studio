using System.Collections.Generic;

namespace MB_Studio_Updater
{
    internal class FileVersionCode
    {
        public FileVersionCode(string dataString)
        {
            Init(dataString.Split('|'));
        }

        public FileVersionCode(string[] data)
        {
            Init(data);
        }

        private void Init(string[] data)
        {
            MD5Hash = data[0];
            FileLastWriteTime = ulong.Parse(data[1]);
            FilePath = data[2];
            DownloadToken = (data.Length < 4) ? string.Empty : data[3];
            Code = MD5Hash + "|" + FileLastWriteTime + "|" + FilePath;
            if (DownloadToken.Length != 0)
                Code += "|" + DownloadToken;
        }

        public static FileVersionCode CombineFileVersionCodes(FileVersionCode downloadedFVC, FileVersionCode generatedFVC)
        {
            string[] codePaths = new string[4];
            codePaths[0] = generatedFVC.MD5Hash;
            codePaths[1] = generatedFVC.FileLastWriteTime.ToString();
            codePaths[2] = generatedFVC.FilePath;
            codePaths[3] = downloadedFVC.DownloadToken;
            return new FileVersionCode(codePaths);
        }

        public static List<FileVersionCode> ConvertToFileVersions(string[] codes)
        {
            return ConvertToFileVersions(new List<string>(codes));
        }

        public static List<FileVersionCode> ConvertToFileVersions(List<string> codes)
        {
            List<FileVersionCode> list = new List<FileVersionCode>();
            foreach (string code in codes)
                list.Add(new FileVersionCode(code));
            return list;
        }

        public ulong FileLastWriteTime { get; private set; }

        public string MD5Hash { get; private set; }

        public string FilePath { get; private set; }

        public string DownloadToken { get; private set; }

        public string Code { get; private set; }

    }
}
