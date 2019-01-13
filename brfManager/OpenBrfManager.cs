using importantLib;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using System.Drawing;

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
        public extern static bool AddMeshToXViewModel(string meshName, int boneIndex, int skeletonIndex, int carryPostionIndex/*, bool isAtOrigin*/, bool mirror, string material, uint vertColor);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static bool RemoveMeshFromXViewModel(string meshName);

        [DllImport(OPEN_BRF_DLL_PATH)]
        public extern static void ShowTroop3DPreview(bool forceOverride = false);

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

        private static readonly List<string> ExcludedMeshes = new List<string>() { "flying_missile" };

        #endregion

        public OpenBrfManager(string mabPath, string modName = "Native", string[] args = null)
        {
            MabPath = mabPath;
            ModName = modName;

            ModulesPath = mabPath + "\\Modules";

            if (IsShown)
                Close();//is this even doing something?

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="meshName"></param>
        /// <param name="bone"></param>
        /// <param name="skeleton"></param>
        /// <param name="carryPosition"></param>
        /// <param name="mirror"></param>
        /// <param name="material"></param>
        /// <param name="vertColor"></param>
        /// <returns></returns>
        public bool AddMeshToTroop3DPreview(string meshName, int bone = 0, int skeleton = 0, int carryPosition = -1/*, bool isAtOrigin*/, params object[] additionalOptions)
        {
            bool success = false;
            if (!ExcludedMeshes.Contains(meshName))
            {
                bool mirror = false;
                if (additionalOptions.Length >= 1)
                {
                    mirror = (bool)additionalOptions[0];
                }

                string material = "";
                if (additionalOptions.Length > 1)
                {
                    material = (string)additionalOptions[1];
                }

                uint vertColor = 0;
                if (additionalOptions.Length > 2)
                {
                    vertColor = (uint)additionalOptions[2];
                }

                success = AddMeshToXViewModel(meshName, bone, skeleton, carryPosition/*, isAtOrigin*/, mirror, material, vertColor);
            }
            else
            {
                Console.WriteLine("Excluded mesh: " + meshName);
            }
            return success;
        }

        public bool RemoveMeshFromTroop3DPreview(string meshName)
        {
            return RemoveMeshFromXViewModel(meshName);
        }

        public void Troop3DPreviewClearData()
        {
            ClearTempMeshesTroop3DPreview();
        }

        public void Troop3DPreviewShow(Troop troop = null, bool forceOverride = false)
        {
            #region Set Body Parts

            if (troop != null)
            {
                bool success = true;
                var troopType = troop.GetTroopType();
                var face = Face.MergeTroopFaces(troop);

                Console.Write("Merged FaceCode: ");
                Console.Write("0x | " + face.FaceCode.Substring(0, 7));
                Console.Write(" | " + face.FaceCode.Substring(7, 9));
                Console.WriteLine(" | " + face.FaceCode.Substring(16));

                FaceTexture faceTexture;
                if ((troop.FlagsGZ >> 12 & 0xF) >= 0x8) // tf_randomize_face
                {
                    // random face gen here
                    Console.WriteLine("Randomize face flag set!");
                }
                //else
                    faceTexture = troopType.FaceTextures[face.Skin];

                Console.WriteLine("Selected FaceTexture: " + faceTexture.Name);

                int mergeFrame = 20;
                double mergeWeight = 0.5;

                success &= AddMeshToTroop3DPreview(troopType.HeadMesh, 9, 0, -1, true, faceTexture.Name, faceTexture.Color, mergeFrame, mergeWeight);
                success &= AddMeshToTroop3DPreview(troopType.BodyMesh, 0);
                success &= AddMeshToTroop3DPreview(troopType.HandMesh, 13);
                success &= AddMeshToTroop3DPreview(troopType.HandMesh.TrimEnd('L') + "R", 18);
                success &= AddMeshToTroop3DPreview(troopType.CalfMesh, 2);
                success &= AddMeshToTroop3DPreview(troopType.CalfMesh.TrimEnd('l') + "l", 5);

                // remove beard, hair, head, body, legs depending on item properties later and check skin color

                success &= Troop3DPreviewAddFacialHairs(troopType.HairMeshes, face.Hair, face.HairColor);
                success &= Troop3DPreviewAddFacialHairs(troopType.BeardMeshes, face.Beard, face.HairColor, true);

                Console.WriteLine("Troop skin body parts: " + success);
            }

            #endregion

            ShowTroop3DPreview(forceOverride);
        }

        private bool Troop3DPreviewAddFacialHairs(string[] facialHairTypes, uint hairIndex, int hairColor, bool mirror = false)
        {
            bool success = true;
            if (facialHairTypes.Length != 0 && hairIndex > 0 && hairIndex <= facialHairTypes.Length)
            {
                string texture = string.Empty; // change if needed

                // use hairPerc for color intesity
                // morph color index + 1 and find color position in between

                //Console.WriteLine("HairColorVal: " + hairColorVal + " | HairPerc: " + hairPerc + " | HairIndex: " + hairIdx + " | HairCount: " + hairColors.Count);
                //Console.WriteLine(" Base Hair" + Color.FromArgb(hairColors[hairIdx]));
                Console.WriteLine(" Final Hair" + Color.FromArgb(hairColor));

                string beardMesh = facialHairTypes[hairIndex - 1];
                Console.WriteLine("Add hairMesh: " + beardMesh); // wrong mesh?

                // add hair color perc to mesh
                success &= AddMeshToTroop3DPreview(beardMesh, 9, 0, -1, mirror, texture, (uint)hairColor);
            }
            return success;
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
