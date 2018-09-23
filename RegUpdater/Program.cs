using Microsoft.Win32;
using System.Diagnostics;
using System.Security.AccessControl;

namespace RegUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                if (args[0].EndsWith("useAdmin"))
                {
                    using (RegistryKey reg32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                    {
                        using (RegistryKey appReg = reg32.OpenSubKey(args[1], RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey))
                        {
                            string valName = "DisplayVersion";
                            appReg.SetValue(valName, args[2], RegistryValueKind.String);

                            valName = "DisplayName";
                            appReg.SetValue(valName, args[3], RegistryValueKind.String);
                        }
                    }
                }
                else
                {
                    var psi = new ProcessStartInfo
                    {
                        CreateNoWindow = true,
                        UseShellExecute = true,
                        Verb = "runas",
                        FileName = @"F:\WORKINGAREA\Visual Studio Projects\MB Studio\MB Studio\bin\x86\Release\RegUpdater.exe",
                        Arguments = "-useAdmin \"" + args[0] + "\" \"" + args[1] + "\" \"" + args[2] + "\""
                    };

                    var process = new Process {
                        StartInfo = psi
                    };
                    process.Start();
                }
            }
        }
    }
}
