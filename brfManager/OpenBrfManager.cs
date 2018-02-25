﻿using importantLib;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace brfManager
{
    public class OpenBrfManager
    {
        #region Imports And Constants

        public const string OPEN_BRF_DLL = "openBrf.dll";

        [DllImport(OPEN_BRF_DLL)]
        public extern static int StartExternal(int argc, string[] argv);

        [DllImport(OPEN_BRF_DLL)]
        public extern static void SetModPath(string modPath);

        [DllImport(OPEN_BRF_DLL)]
        public extern static void SelectIndexOfKind(int kind, int index);

        [DllImport(OPEN_BRF_DLL)]
        public extern static bool SelectItemByNameAndKind(string meshName, int kind = 0);

        [DllImport(OPEN_BRF_DLL)]
        public extern static bool SelectItemByNameAndKindFromCurFile(string meshName, int kind = 0);

        [DllImport(OPEN_BRF_DLL)]
        public extern static IntPtr GetCurWindowPtr();

        [DllImport(OPEN_BRF_DLL)]
        public extern static void CloseApp();

        [DllImport(OPEN_BRF_DLL)]
        public extern static bool IsCurHWndShown();

        [DllImport(OPEN_BRF_DLL)]
        public extern static void AddMeshToXViewModel(string meshName);

        [DllImport(OPEN_BRF_DLL)]
        public extern static bool RemoveMeshFromXViewModel(string meshName);

        #endregion

        #region Attributes / Properties

        private string modName, mabPath, modulesPath, modPath, resourcePath;
        private string[] SArray = new string[0];

        private ImportantMethods imp = new ImportantMethods();

        public string ModulesPath { get { return modulesPath; } }

        public bool IsShown { get { return IsCurHWndShown(); } }

        public IntPtr Handle { get { return GetCurWindowPtr(); } }

        #endregion

        public OpenBrfManager(string modName = "Native", string mabPath = @"F:\Program Files\Steam\steamapps\common\MountBlade Warband", string[] args = null)
        {
            if (IsShown)
                MessageBox.Show("CUR_WINDOW_STILL_OPEN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (args != null)
                SArray = args;

            modulesPath = mabPath + "\\Modules";
            this.mabPath = mabPath;
            this.modName = modName;

            ChangeModule(modName);
        }

        public int Show(bool debugMode = false, string[] args = null)
        {
            if (args == null)
            {
                if (!debugMode)
                    args = SArray;
                else
                    args = new string[] { "--debug" };
            }
            else if (debugMode)
            {
                string[] tmp = new string[args.Length + 1];
                tmp[0] = "--debug";
                for (int i = 0; i < args.Length; i++)
                    tmp[i + 1] = args[i];
                args = tmp;
            }

            return StartExternal(args.Length, args);
        }

        public void Close()
        {
            CloseApp();
        }

        public void AddMeshToTroopDummy(string meshName)
        {
            AddMeshToXViewModel(meshName);
        }

        public bool RemoveMeshFromTroopDummy(string meshName)
        {
            return RemoveMeshFromXViewModel(meshName);
        }

        public void ChangeModule(string moduleName)
        {
            string tempPath = modulesPath + "\\" + moduleName;
            if (Directory.Exists(tempPath))
            {
                modName = moduleName;
                modPath = tempPath;
                SetResourcePath(modPath + "\\Resource");

                if (IsShown && Directory.Exists(modPath))
                    SetModPath(modPath);
            }
            else
                Console.WriteLine(mabPath + " -> Path is not vaild!");
        }

        private void SetResourcePath(string resourcePath, bool changeMod = true, bool useModFirst = false)
        {
            if (Directory.Exists(resourcePath))
            {
                this.resourcePath = resourcePath;
                if (changeMod)
                    SArray = new string[] { string.Empty, string.Empty, "-mod", modPath };
                else
                    SArray = new string[] { string.Empty, string.Empty };
                if (useModFirst)
                {
                    string[] brfFiles = Directory.GetFiles(resourcePath);
                    for (int i = 0; i < brfFiles.Length; i++)
                    {
                        if (brfFiles[i].Substring(brfFiles[i].LastIndexOf('.') + 1).ToLower().Equals("brf"))
                        {
                            SArray[1] = brfFiles[i];
                            i = brfFiles.Length;
                        }
                    }
                }
            }
            if (SArray[1].Length == 0)
                SArray[1] = mabPath + "\\CommonRes\\barrier_primitives.brf";
        }

        public void SelectItembyKindAndIndex(int kind, int index)
        {
            if (IsShown)
                SelectIndexOfKind(kind, index);
        }

        public bool SelectItemNameByKind(string name, int kind = 0)
        {
            bool b = false;
            if (IsShown)
            {
                Console.WriteLine("TRY: OPEN_BRF_MANAGER SELECT " + name + " - (KIND[" + kind + "])");
                b = SelectItemByNameAndKind(name, kind);
                if (!b)
                    Console.WriteLine("OPEN_BRF_MANAGER: MESH_NOT_FOUND!");
            }
            return b;
        }

        public void AddWindowHandleToControlsParent(Control childX, bool addOnRightSide = true)
        {
            int left;
            if (addOnRightSide)
                left = childX.Width;
            else
                left = childX.Left;
            ImportantMethods.AddWindowHandleToControl(Handle, childX.Parent, childX.Height, left, childX.Top);
        }

        /*public void AddNativeChildWindow(IntPtr hWndParent, Control parentControl)
        {
            imp.AddNativeChildWindow(Handle, hWndParent, parentControl);
        }*/
    }
}
