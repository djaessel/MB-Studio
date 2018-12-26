using importantLib;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MB_Studio_Library.Objects;
using MB_Studio_Library.IO;
using System.Globalization;

namespace brfManager
{
    public class OpenBrfManager
    {
        #region Imports And Constants

        public const string OPEN_BRF_DLL_PATH = "openBrf.dll";

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static int StartExternal(int argc, string[] argv);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void SetModPath(string modPath);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void SelectIndexOfKind(int kind, int index);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool SelectItemByNameAndKind(string meshName, int kind = 0);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool SelectItemByNameAndKindFromCurFile(string meshName, int kind = 0);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void AddCurSelectedMeshsAllDataToMod(string modName);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void SetSkinBodyParts(byte skinType);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool AddMeshToXViewModel(string meshName, int boneIndex, int skeletonIndex, int carryPostionIndex/*, bool isAtOrigin*/, string material);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool SetMaterialLastSel(string material);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool RemoveMeshFromXViewModel(string meshName);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void ShowTroop3DPreviewFace(byte skin, string face1, string face2);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void ShowTroop3DPreview();

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void ClearTempMeshesTroop3DPreview();

        [DllImport(OPEN_BRF_DLL_PATH)]
        public static extern void GenerateStringsAndStoreInSafeArray(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] ManagedStringArray,
            byte onlyCurrentModule,
            byte commonRes = 0
        );

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static byte IsCurHWndShown();

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static IntPtr GetCurWindowPtr();

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void CloseApp();

        #endregion

        #region Attributes / Properties

        public bool IsShown { get { return (IsCurHWndShown() != 0) ? true : false; } }

        public static bool KillModeActive { get; private set; } = false;

        public bool HasParent { get; private set; } = false;

        private string[] SArray = new string[0];

        public string MabPath { get; private set; }

        public string ModName { get; private set; }

        public string ModPath { get; private set; }

        public string ResourcePath { get; private set; }

        public string ModulesPath { get; private set; }

        public IntPtr Handle { get { return GetCurWindowPtr(); } }

        #endregion

        public OpenBrfManager(string mabPath, string modName = "Native", string[] args = null)
        {
            MabPath = mabPath;
            ModName = modName;

            ModulesPath = mabPath + "\\Modules";

            if (IsShown) Close();//is this even doing something?

            if (args != null)
                SArray = args;

            ChangeModule(modName);
        }

        public int Show(bool debugMode = false, string[] args = null)
        {
            string useDebug = "--debug";

            if (args == null)
            {
                if (!debugMode)
                    args = SArray;
                else
                    args = new string[] { useDebug };
            }
            else if (debugMode)
            {
                string[] tmp = new string[args.Length + 1];
                tmp[0] = useDebug;
                for (int i = 0; i < args.Length; i++)
                    tmp[i + 1] = args[i];
                args = tmp;
            }

            return StartExternal(args.Length, args);
        }

        /// <summary>
        /// EXPERIMENTAL
        /// TODO: Create C++ method to clear scene (maybe use this for Troop3DPreviewClearData as well later!
        /// </summary>
        public void Clear()
        {
            Troop3DPreviewClearData();
            Troop3DPreviewShow();
        }

        public void Close()
        {
            CloseApp();
        }

        public void SetSkinBody(byte skinType)
        {
            SetSkinBodyParts(skinType);
        }

        public bool AddMeshToTroop3DPreview(string meshName, int bone = 0, int skeleton = 0, int carryPosition = -1/*, bool isAtOrigin*/, string material = "")
        {
            return AddMeshToXViewModel(meshName, bone, skeleton, carryPosition/*, isAtOrigin*/, material);
        }

        public bool RemoveMeshFromTroop3DPreview(string meshName)
        {
            return RemoveMeshFromXViewModel(meshName);
        }

        public void Troop3DPreviewClearData()
        {
            ClearTempMeshesTroop3DPreview();
        }

        public void Troop3DPreviewShow(Troop troop = null)
        {
            if (troop != null)
            {
                byte skinType = (byte)(troop.FlagsGZ & 0x0F);
                string fileName = CodeReader.Files[(int)Skriptum.ObjectType.Skin];

                var cr = new CodeReader(CodeReader.ModPath + fileName);
                List<Skin> skins = cr.ReadSkin();
                Skin skin = skins[skinType];

                string faceCode = TroopCombinedFaceCode(troop);

                ulong age         = ((ulong.Parse(faceCode.Substring(10, 2), NumberStyles.HexNumber) & 0xFF000000) >> 24 & 0x0FF) / 2; // check again
                ulong face        = ((ulong.Parse(faceCode.Substring(12, 1), NumberStyles.HexNumber) & 0x00F00000) >> 16 & 0x00F) / 1; // check again
                ulong beard       = ((ulong.Parse(faceCode.Substring(13, 2), NumberStyles.HexNumber) & 0x000FF000) >> 12 & 0x0FF) / 4; // check again
                ulong hair        = ((ulong.Parse(faceCode.Substring(14, 3), NumberStyles.HexNumber) & 0x00001FF0) >>  8 & 0x1FF) / 1; // check again

                var faceTexture = skin.FaceTextures[face];

                bool success = true;
                success &= AddMeshToTroop3DPreview(skin.BodyMesh, 0, 0, -1, faceTexture.Name);//check later for color and real material
                success &= AddMeshToTroop3DPreview(skin.HandMesh, 13, 0, -1, faceTexture.Name);//check later for color and real material
                success &= AddMeshToTroop3DPreview(skin.HandMesh.TrimEnd('l') + "r", 18, 0, -1, faceTexture.Name);//check later for color and real material
                success &= AddMeshToTroop3DPreview(skin.CalfMesh, 2, 0, -1, faceTexture.Name);//check later for color and real material
                success &= AddMeshToTroop3DPreview(skin.CalfMesh.TrimEnd('L') + "R", 5, 0, -1, faceTexture.Name);//check later for color and real material

                if (skin.HairMeshes.Length != 0)
                {
                    string hairMesh = skin.HairMeshes[hair];
                    string hairTexture = skin.HairTextures[face];
                    success &= AddMeshToTroop3DPreview(hairMesh, 9, 0, -1, hairTexture);
                }

                if (skin.BeardMeshes.Length != 0)
                {
                    string beardMesh = skin.BeardMeshes[beard];
                    string beardTexture = skin.BeardTextures[face];
                    success &= AddMeshToTroop3DPreview(beardMesh, 9, 0, -1, beardTexture);
                }

                success &= AddMeshToTroop3DPreview(skin.HeadMesh, 9, 0, -1, faceTexture.Name);
                // add mirror here if needed

                Console.WriteLine("FaceTexture: " + faceTexture.Name);

                Console.Write("Troop base body set ");
                if (!success)
                    Console.WriteLine("successful");
                else
                    Console.WriteLine("failed");
            }
            //else
            ShowTroop3DPreview();
        }

        public bool SetMaterialLastSelected(string material)
        {
            return SetMaterialLastSel(material);
        }

        private string TroopCombinedFaceCode(Troop troop)
        {
            //char[] face1X = troop.Face1.ToCharArray();
            //char[] face2X = troop.Face2.ToCharArray();
            //string result = string.Empty;
            //for (int i = 0; i < face1X.Length; i++)
            //    result += (char)(face1X[i] + face2X[i] / 2); // theoretical function - double/triple numbers not included
            //return result;
            return troop.Face1.Substring(2);
        }

        public void ChangeModule(string moduleName)
        {
            string tempPath = ModulesPath + "\\" + moduleName;
            if (Directory.Exists(tempPath))
            {
                ModName = moduleName;
                ModPath = tempPath;
                SetResourcePath(ModPath + "\\Resource");

                if (IsShown && Directory.Exists(ModPath))
                    SetModPath(ModPath);
            }
            else
                Console.WriteLine(tempPath + " -> Path is invaild!");
        }

        private void SetResourcePath(string resourcePath, bool changeMod = true, bool useModFirst = false)
        {
            if (Directory.Exists(resourcePath))
            {
                ResourcePath = resourcePath;
                if (changeMod)
                    SArray = new string[] { string.Empty, string.Empty, "-mod", ModPath };
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
                SArray[1] = MabPath + "\\CommonRes\\barrier_primitives.brf";
        }

        public void SelectItembyKindAndIndex(int kind, int index)
        {
            if (IsShown)
                SelectIndexOfKind(kind, index);
        }

        public bool SelectItemNameByKind(string name, int kind = 0)
        {
            if (!IsShown) return false;

            Console.Write("TRY: OPEN_BRF_MANAGER SELECT " + name + " - (KIND[" + kind + "]) :");
            bool b = SelectItemByNameAndKind(name, kind);
            Console.WriteLine(" " + b);

            if (!b)
                Console.WriteLine("OPEN_BRF_MANAGER: MESH_NOT_FOUND!");

            return b;
        }

        public void AddSelectedMeshsToMod(string modName)
        {
            AddCurSelectedMeshsAllDataToMod(modName);
        }

        public static void ActivateKillMode()
        {
            KillModeActive = true;
        }

        public void AddWindowHandleToControlsParent(Control childX, bool addOnRightSide = true)
        {
            if (KillModeActive) return;

            int left;
            if (addOnRightSide)
                left = childX.Width;
            else
                left = childX.Left;
            ImportantMethods.AddWindowHandleToControl(Handle, childX.Parent, childX.Height, left, childX.Top, 32);
            HasParent = true;
        }

        public void RemoveWindowHandleFromControlsParent()
        {
            if (!HasParent || KillModeActive) return;
            ImportantMethods.RemoveWindowHandleFromParent(Handle);
            HasParent = false;
        }

        /// <summary>
        /// Generates an array including all modulenames
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllModuleNames()
        {
            GenerateStringsAndStoreInSafeArray(out string[] managedStringArray, 2);
            managedStringArray = managedStringArray[0].TrimEnd(';').Split(';');
            List<string> list = new List<string>();
            foreach (string s in managedStringArray)
                if (!list.Contains(s))
                    list.Add(s);
            return list;
        }

        /// <summary>
        /// Generates an array including all mesh-/resourcenames from current module
        /// </summary>
        /// <returns></returns>
        public List<string> GetCurrentModuleAllMeshResourceNames(bool commonRes = false)
        {
            byte comRes = (byte)((commonRes) ? 1 : 0);
            GenerateStringsAndStoreInSafeArray(out string[] managedStringArray, 1, comRes);
            List<string> list = GetRealNamesArray(ref managedStringArray, out List<string> moduleNames)[0];//only one possible
            return list;
        }

        /// <summary>
        /// Generates an array including all mesh-/resourcenames
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetAllMeshResourceNames(out List<string> moduleNames, bool commonRes = false)
        {
            byte comRes = (byte)((commonRes) ? 1 : 0);
            GenerateStringsAndStoreInSafeArray(out string[] managedStringArray, 0, comRes);
            return GetRealNamesArray(ref managedStringArray, out moduleNames);
        }

        private static List<List<string>> GetRealNamesArray(ref string[] managedStringArray, out List<string> modNames, bool filterDots = true)
        {
            List<List<string>> allNames = new List<List<string>>();
            modNames = new List<string>();
            foreach (string block in managedStringArray)
            {
                List<string> listX = new List<string>(block.TrimEnd(';').Split(';'));
                int lastIdx = listX.Count - 1;
                if (!modNames.Contains(listX[lastIdx]))
                {
                    modNames.Add(listX[lastIdx]);//last index is modName!
                    listX.RemoveAt(lastIdx);//last index is modName!
                    lastIdx--;
                    for (int i = lastIdx; i != 0; i--)
                    {
                        string tmp = listX[i].Split('.')[0];
                        if (listX.Contains(tmp)) listX.RemoveAt(i);
                        else listX[i] = tmp;
                    }
                    allNames.Add(listX);
                }
            }
            return allNames;
        }
    }
}
