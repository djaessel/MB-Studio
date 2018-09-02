using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace importantLib
{
    public class ImportantMethods
    {
        #region Imports And Constants

        #region User32

        #region Constants

        public const uint WS_OVERLAPPED = 0x00000000;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_DISABLED = 0x08000000;
        public const uint WS_CLIPSIBLINGS = 0x04000000;
        public const uint WS_CLIPCHILDREN = 0x02000000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_DLGFRAME = 0x00400000;
        public const uint WS_VSCROLL = 0x00200000;
        public const uint WS_HSCROLL = 0x00100000;
        public const uint WS_SYSMENU = 0x00080000;
        public const uint WS_THICKFRAME = 0x00040000;
        public const uint WS_GROUP = 0x00020000;
        public const uint WS_TABSTOP = 0x00010000;

        public const uint WS_MINIMIZEBOX = 0x00020000;
        public const uint WS_MAXIMIZEBOX = 0x00010000;

        public const uint WS_TILED = WS_OVERLAPPED;
        public const uint WS_ICONIC = WS_MINIMIZE;
        public const uint WS_SIZEBOX = WS_THICKFRAME;
        public const uint WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        // Common Window Styles

        public const uint WS_OVERLAPPEDWINDOW =
            (WS_OVERLAPPED |
              WS_CAPTION |
              WS_SYSMENU |
              WS_THICKFRAME |
              WS_MINIMIZEBOX |
              WS_MAXIMIZEBOX);

        public const uint WS_POPUPWINDOW =
            (WS_POPUP |
              WS_BORDER |
              WS_SYSMENU);

        public const uint WS_CHILDWINDOW = WS_CHILD;

        private const int GWL_STYLE = -16;
        private const int WM_PARENTNOTIFY = 0x0210;
        private const int WM_SIZE = 0x0005;

        #endregion

        internal class NativeMethods
        {
            #region User32

            [DllImport("user32.dll")]
            public static extern uint GetWindowLong(IntPtr hwnd, Int32 test);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

            #endregion

            #region Kernel32

            [return: MarshalAs(UnmanagedType.LPWStr)]
            [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
            public static extern IntPtr GetModuleHandle(string moduleName);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetCurrentProcess();

            [SecurityCritical]
            public static bool DoesWin32MethodExist(string moduleName, string methodName)
            {
                IntPtr moduleHandle = GetModuleHandle(moduleName);
                if (moduleHandle == IntPtr.Zero) return false;
                return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
            }

            [return: MarshalAs(UnmanagedType.Bool)]
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern bool IsWow64Process([In] IntPtr hSourceProcessHandle, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);

            #endregion
        }

        [SecuritySafeCritical]
        public static bool Is64BitOperatingSystem()
        {
            return (IntPtr.Size == 8) ||
            ((NativeMethods.DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
            NativeMethods.IsWow64Process(NativeMethods.GetCurrentProcess(), out bool flag)) && flag);
        }

        #endregion

        #endregion

        #region Window Management

        private IntPtr _childHandle = IntPtr.Zero;

        public static void AddWindowHandleToControl(IntPtr hWnd, Control parent, int height, int left, int top, int addWidth = 0, bool resize = false)
        {
            Label l = new Label()
            {
                AutoSize = false,
                Size = new Size(parent.Width - left, height - (top)),
                Left = left,
                Top = top,
                Text = "PLACE HOLDER",
                ForeColor = Color.White,
                BackColor = Color.Green,
                Visible = true
            };
            l.Parent = parent;
            l.BringToFront();

            new ImportantMethods().AddNativeChildWindow(hWnd, parent.Handle, l, resize, addWidth);

            parent.Refresh();
        }

        public static void RemoveWindowHandleFromParent(IntPtr hWndChild)
        {
            IntPtr desktopHwnd = NativeMethods.GetDesktopWindow();
            NativeMethods.SetParent(hWndChild, desktopHwnd);
        }

        private void Parent_SizeChanged(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            NativeMethods.MoveWindow(_childHandle, c.Location.X, c.Location.Y, c.Size.Width, c.Size.Height, true);
        }

        public void AddNativeChildWindow(IntPtr hWndChild, IntPtr hWndParent, Control parentControl, bool resize = false, int addWidth = 0)
        {
            //uint style = NativeMethods.GetWindowLong(hWndChild, GWL_STYLE);
            //MessageBox.Show("BEFORE: " + skillhunter.HexConverter.Dec2Hex(style));
            //style = (style & ~(WS_OVERLAPPEDWINDOW | WS_POPUP)) | WS_CHILD;
            //MessageBox.Show("AFTER: " + skillhunter.HexConverter.Dec2Hex(style));
            NativeMethods.SetWindowLong(hWndChild, GWL_STYLE, WS_CHILDWINDOW | WS_VISIBLE);// | WS_CAPTION);//style);

            //let the .NET control  be the parent of the native window
            NativeMethods.SetParent(hWndChild, hWndParent);

            if (resize)
            {
                _childHandle = hWndChild;

                // just for fun, send an appropriate message to the .NET control 
                //NativeMethods.SendMessage(hWndParent, WM_PARENTNOTIFY, (IntPtr)1, hWndChild);

                parentControl.SizeChanged += Parent_SizeChanged;
            }

            NativeMethods.MoveWindow(hWndChild, parentControl.Location.X, parentControl.Location.Y, parentControl.Width + addWidth, parentControl.Height, false);

            parentControl.Width = parentControl.Width - 20;
            parentControl.Height = parentControl.Height - 32;
        }

        #endregion

        public static bool ArrayContainsString(string[] array, string s)
        {
            bool found = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(s))
                {
                    found = true;
                    i = array.Length;
                }
            }
            return found;
        }

        public static int ArrayIndexOfString(string[] array, string s)
        {
            int idx = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(s))
                {
                    idx = i;
                    i = array.Length;
                }
            }
            return idx;
        }

        public static int CountStringAccurancyInList(string s, List<string> list, bool checkCounter = true, bool contains = true)
        {
            int x = 0;
            foreach (string sx in list)
            {
                string sss = sx.Substring(sx.LastIndexOf('_') + 1);
                if (sx.Equals(s)
                    || (contains && sx.Contains(s) && !checkCounter)
                    || (contains && sx.Contains(s) && checkCounter && IsNumericFKZ128(sss))
                    || (!contains && checkCounter && IsNumericFKZ128(sss)) && s.Equals(sx.Substring(0, sx.Length - (sss.Length + 1))))
                    x++;
            }
            return x;
        }

        public static string GetDirectoryNameOnly(string path)
        {
            string newPath = Path.GetFullPath(path.Trim('\\'));
            newPath = newPath.Substring(newPath.LastIndexOf('\\') + 1);
            return newPath;
        }

        public static string GetDirectoryPathOnly(string path)
        {
            string newPath = Path.GetFullPath(path.Trim('\\'));
            newPath = newPath.Remove(newPath.LastIndexOf('\\'));
            return newPath;
        }

        public static string ReverseString(string s)
        {
            char[] c = s.ToCharArray();
            s = string.Empty;
            for (int i = c.Length - 1; i >= 0; i--)
                s += c[i];
            return s;
        }

        public static string[] SplitIntoBlocks(string str, int blockSize)
        {
            int size = str.Length / blockSize;
            if (str.Length % blockSize > 0)
                size++;
            string[] blocks = new string[size];
            for (int i = 0; i < blocks.Length; i++)
                if (str.Substring(i * blockSize).Length <= blockSize)
                    blocks[i] = str.Substring(i * blockSize, blockSize);
                else
                    blocks[i] = str.Substring(i * blockSize);
            return blocks;
        }

        public static string RemoveFirstMinus(string s)
        {
            if (s.StartsWith("-"))
                s = s.Substring(1);
            return s;
        }

        public static bool IsNumericGZ(string s)
        {
            return ulong.TryParse(RemoveFirstMinus(s), out ulong u);
        }

        public static bool IsNumericFKZ128(string s)
        {
            return decimal.TryParse(RemoveFirstMinus(s), out decimal d);
        }

        public static bool IsNumericFKZ(string s)
        {
            return double.TryParse(RemoveFirstMinus(s), out double d);
        }

        public static bool IsNumeric(string s, bool all)
        {
            bool b = IsNumericGZ(s);
            if (!b && all)
                b = IsNumericFKZ(s);
            if (!b && all)
                b = IsNumericFKZ128(s);
            return b;
        }

        public static List<string> StringArrayToList(string[] s, int startIndex = 0)
        {
            List<string> list = new List<string>();
            for (int i = startIndex; i < s.Length; i++)
                list.Add(s[i]);
            return list;
        }

        public static List<int> IntArrayToList(int[] s, int startIndex = 0)
        {
            List<int> list = new List<int>();
            for (int i = startIndex; i < s.Length; i++)
                list.Add(s[i]);
            return list;
        }

        public static string ToUpperAfterBlank(string s)
        {
            string[] tmp = s.Split();
            s = string.Empty;
            foreach (string sx in tmp)
            {
                s += sx.Substring(0, 1).ToUpper();
                if (sx.Length > 1)
                    s += sx.Substring(1);
                s += ' ';
            }
            return s.TrimEnd();
        }

        /// <summary>
        /// Executes a list of shell commands either synchronously or asynchronously
        /// </summary>
        /// <param name="commands">List of shell commands. One command per index.</param>
        /// <param name="sync">Decides whether it is executed synchronously or asynchronously</param>
        public static void ExecuteCommands(string[] commands, bool sync = true)
        {
            string commandsParam = string.Empty;
            foreach (string command in commands)
                commandsParam += ' ' + command + " &";
            commandsParam = commandsParam.Trim().TrimEnd('&').TrimEnd();
            if (sync)
                ExecuteCommandSync(commandsParam);
            else
                ExecuteCommandAsync(commandsParam);
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Executes a shell command synchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command</param></span>
        /// <span class="code-SummaryComment"><returns>string, as output of the command.</returns></span>
        public static void ExecuteCommandSync(object command)
        {
            ExecuteCommandSync(command, null);
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Executes a shell command synchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command</param></span>
        /// <span class="code-SummaryComment"><param name="workingDir">string workingDir</param></span>
        /// <span class="code-SummaryComment"><returns>string, as output of the command.</returns></span>
        public static void ExecuteCommandSync(object command, string workingDir)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                ProcessStartInfo procStartInfo =
                    new ProcessStartInfo("cmd", "/C " + command)
                    {
                        // The following commands are needed to redirect the standard output.
                        // This means that it will be redirected to the Process.StandardOutput StreamReader.
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        // Do not create the black window.
                        CreateNoWindow = true
                    };

                if (workingDir != null)
                    procStartInfo.WorkingDirectory = workingDir;

                // Now we create a process, assign its ProcessStartInfo and start it
                Process proc = new Process() { StartInfo = procStartInfo };
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Trim result if needed
                result = result.Trim(' ', '\r', '\n');
                // Display the command output.

                // TEST

                // Get the output into a string
                string error_result = proc.StandardError.ReadToEnd();
                // Trim result if needed
                error_result = error_result.Trim(' ', '\r', '\n');
                // Display the command output.

                // TEST

                if (result.Length != 0)
                    Console.WriteLine(result);
                if (error_result.Length != 0)// TEST
                    Console.WriteLine(error_result); // TEST
            }
            catch (Exception objException)
            {
                // Log the exception
                Console.WriteLine(objException.ToString());
            }
        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Execute the command Asynchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command.</param></span>
        public static void ExecuteCommandAsync(string command)
        {
            string errorMsg = string.Empty;

            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync))
                {
                    //Make the thread as background thread.
                    IsBackground = true,
                    //Set the Priority of the thread.
                    //Priority = ThreadPriority.AboveNormal
                };
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
                errorMsg += objException.ToString() + Environment.NewLine;
            }
            catch (ThreadAbortException objException)
            {
                // Log the exception
                errorMsg += objException.ToString() + Environment.NewLine;
            }
            catch (Exception objException)
            {
                // Log the exception
                errorMsg += objException.ToString() + Environment.NewLine;
            }

            if (errorMsg.Length != 0)
                Console.WriteLine(errorMsg);
        }

    }
}
