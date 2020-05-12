using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace MB_Studio
{
    static class Program
    {
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main(string[] args)
        {
            HandleArguments(args);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(MB_Studio.Form_UIThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event.
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // Runs the application.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MB_Studio());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "An application error occurred. Please contact the adminstrator with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog
                {
                    Source = "ThreadException"
                };
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Put this in a handler class later - so no code is in Program
        private static void HandleArguments(string[] args)
        {
            if (args.Length == 0) return;

            if (args[0].Equals("-au"))
            {
                UpdateRegistryData();
            }
            else
            {
                Console.WriteLine("Unkown parameter: " + args[0]);
                Environment.Exit(1); // maybe change error code later
            }
        }

        // Put this in a handler class later - so no code is in Program
        private static void UpdateRegistryData()
        {
            string productName = Application.ProductName;
            productName = productName.Remove(productName.IndexOf('.'));

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                string regSubKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + productName;
                using (RegistryKey key = baseKey.OpenSubKey(regSubKey, true))
                {
                    UpdateRegKey(key, "DisplayVerion", Application.ProductVersion);
                    UpdateRegKey(key, "DisplayName", productName);
                    UpdateRegKey(key, "Publisher", Application.CompanyName);

                    key.Flush();
                }
            }
        }

        // Put this in a handler class later - so no code is in Program
        private static bool UpdateRegKey(
            RegistryKey key,
            string valueName,
            object newValue,
            RegistryValueKind keyKind = RegistryValueKind.String
        )
        {
            object oldValue = key.GetValue(valueName);
            bool needUpdate = !oldValue.Equals(newValue);
            if (needUpdate)
                key.SetValue(valueName, newValue, keyKind);
            return needUpdate;
        }
    }
}
