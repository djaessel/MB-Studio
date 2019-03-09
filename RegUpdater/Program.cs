using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
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
                        RegistryKey appReg = null;
                        if (args.Length < 5)
                            appReg = reg32.OpenSubKey(args[1], RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.WriteKey);
                        else if (args[4].EndsWith("-createNew"))
                            appReg = reg32.CreateSubKey(args[1], RegistryKeyPermissionCheck.ReadWriteSubTree);

                        if (appReg != null)
                        {
                            using (appReg)
                            {
                                string valName = "DisplayVersion";
                                appReg.SetValue(valName, args[2], RegistryValueKind.String);

                                valName = "DisplayName";
                                appReg.SetValue(valName, args[3], RegistryValueKind.String);
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: The reg key " + args[1] + " couldn't be created/updated!");
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
                        FileName = Path.GetFullPath(@".\RegUpdater.exe"),
                        Arguments = "-useAdmin " + GetArgumentsString(args)
                    };

                    var process = new Process { StartInfo = psi };
                    process.Start();
                }
            }
        }

        private static string GetArgumentsString(string[] args)
        {
            string arguments = string.Empty;
            foreach (string arg in args)
            {
                string tmp = arg;
                if (tmp.Contains(" "))
                {
                    tmp = '"' + tmp + '"';
                }
                arguments += tmp + " ";
            }
            arguments = arguments.TrimEnd();
            return arguments;
        }
    }
}
