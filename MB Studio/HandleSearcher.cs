using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MB_Studio
{
    class HandleSearcher
    {
        public static IntPtr SearchWindow(string processName, string searchText)
        {
            IntPtr returnValue = IntPtr.Zero;
            //List<string> lines = new List<string>();
            foreach (Process procesInfo in Process.GetProcesses())
            {
                if (procesInfo.ProcessName == processName)
                {
                    Console.WriteLine("process {0} 0x{1:x}", procesInfo.ProcessName, procesInfo.Id);

                    foreach (ProcessThread threadInfo in procesInfo.Threads)
                    {
                        IntPtr[] windows = GetWindowHandlesForThread(threadInfo.Id);

                        if (windows != null && windows.Length > 0)
                        {
                            foreach (IntPtr hWnd in windows)
                            {
                                string text = GetText(hWnd);
                                if (text.Length >= searchText.Length)
                                {
                                    if (text.ToUpper().Contains(searchText))
                                    {
                                        returnValue = hWnd;
                                        //lines.Add("\twindow " + hWnd.ToInt32() + " text:" + text + " caption:" + GetEditText(hWnd).ToString());
                                        //Console.WriteLine("\twindow {0:x} text:{1} caption:{2}",
                                        //    hWnd.ToInt32(), GetText(hWnd), GetEditText(hWnd));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //File.WriteAllLines("testLog.txt", lines);
            return returnValue;
        }

        private static IntPtr[] GetWindowHandlesForThread(int threadHandle)
        {
            _results.Clear();
            EnumWindows(WindowEnum, threadHandle);
            return _results.ToArray();
        }

        // enum windows

        private delegate int EnumWindowsProc(IntPtr hwnd, int lParam);

        [DllImport("user32.Dll")]
        private static extern int EnumWindows(EnumWindowsProc x, int y);
        [DllImport("user32")]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, int lParam);
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        private static List<IntPtr> _results = new List<IntPtr>();

        private static int WindowEnum(IntPtr hWnd, int lParam)
        {
            int threadID = GetWindowThreadProcessId(hWnd, out int processID);
            if (threadID == lParam)
            {
                _results.Add(hWnd);
                EnumChildWindows(hWnd, WindowEnum, threadID);
            }
            return 1;
        }

        // get window text

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        private static string GetText(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        // get richedit text 

        public const int GWL_ID = -12;
        public const int WM_GETTEXT = 0x000D;

        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int index);
        [DllImport("User32.dll")]
        public static extern IntPtr SendDlgItemMessage(IntPtr hWnd, int IDDlgItem, int uMsg, int nMaxCount, StringBuilder lpString);
        [DllImport("User32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        private static StringBuilder GetEditText(IntPtr hWnd)
        {
            Int32 dwID = GetWindowLong(hWnd, GWL_ID);
            IntPtr hWndParent = GetParent(hWnd);
            StringBuilder title = new StringBuilder(128);
            SendDlgItemMessage(hWndParent, dwID, WM_GETTEXT, 128, title);
            return title;
        }
    }
}
