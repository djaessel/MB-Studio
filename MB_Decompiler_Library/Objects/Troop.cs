using skillhunter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MB_Decompiler_Library.Objects
{
    public class Troop : Skriptum
    {
        public const int GuaranteeAll = 133169152;

        private static List<HeaderVariable> headerFlags = null;

        private string[] ValuesX;
        private string[] names = new string[2];
        private string[] faceCodes = new string[2];
        private string[] upgradePathError = new string[2];
        private string dialogImage, sceneCode, reserved, flags, proficiencies_SC;

        private int[] upgradePath = new int[2];
        private int[] proficiencies = new int[7];
        private int reservedGZ, flagsGZ;//, dialogImageGZ;

        private List<ulong> itemFlags = new List<ulong>();
        private ulong sceneCodeGZ;

        public Troop(string[] values) : base(values[0].TrimStart().Split()[0], ObjectType.TROOP)
        {
            ValuesX = values;
            Init();
        }

        private void Init()
        {
            Reset();
            if (ValuesX.Length > 5 && ValuesX.Length < 8)
            {
                SetFirstLine(ValuesX[0]);
                SetItems(ValuesX[1]);
                SetAttributes(ValuesX[2]);
                SetProficiencies(ValuesX[3]);
                SetSkills(ValuesX[4]);
                SetFaceCodes(ValuesX[5]);
                return;
            }
            //else if (ValuesX.Length == 0)
                SendErrorMessage();
        }

        private void SendErrorMessage(byte id = 0)
        {
            string show = (id == 0) ? 
                "ERROR! You probably have too many lines found in troop init! Check your files!" : 
                "ERROR! Too few lines found in troop init! Check your files!";
            MessageBox.Show(show);
        }

        private void SetFaceCodes(string v)
        {
            throw new NotImplementedException();
        }

        private void SetSkills(string v)
        {
            throw new NotImplementedException();
        }

        private void SetProficiencies(string v)
        {
            throw new NotImplementedException();
        }

        private void SetAttributes(string v)
        {
            throw new NotImplementedException();
        }

        private void SetItems(string v)
        {
            throw new NotImplementedException();
        }

        private void SetFirstLine(string v)
        {
            throw new NotImplementedException();
        }

        private void Reset()
        {
            string tmp = "0";
            names = new string[] { string.Empty, string.Empty };
            upgradePath = new int[] { 0, 0 };
            faceCodes = new string[] { "0x0", "0x0" };
            //Attributes = new int[] { 0, 0, 0, 0 };
            proficiencies = new int[] { 0, 0, 0, 0, 0, 0 };
            //FacionID = 0;
            dialogImage = tmp;
            sceneCode = tmp;
            reserved = tmp;
            //Items.Clear();
            itemFlags.Clear();
            //for (int i = 0; i < Skills.Length; i++)
            //    Skills[i] = 0;
        }

        /*

#Region "Properties"

    Public Shared Property ShortProficies As Boolean = False

    Public ReadOnly Property Flags As String
        Get
            Return my_flags
        End Get
    End Property

    Public ReadOnly Property FlagsGZ As Integer
        Get
            Return my_flags_GZ
        End Get
    End Property

    Public ReadOnly Property DialogImage As String
        Get
            Return my_dialogImage
        End Get
    End Property

    Public ReadOnly Property SceneCode As String
        Get
            Return my_sceneCode
        End Get
    End Property

    Public ReadOnly Property SceneCodeGZ As ULong
        Get
            Return my_sceneCode_GZ
        End Get
    End Property

    Public ReadOnly Property Reserved As String
        Get
            Return my_reserved
        End Get
    End Property

    Public ReadOnly Property ReservedGZ As Integer
        Get
            Return my_reserved_GZ
        End Get
    End Property

    Public ReadOnly Property Attributes As Integer() = New Integer(4) {}

    Public ReadOnly Property Proficiencies As Integer()
        Get
            Return my_proficiencies
        End Get
    End Property

    Public ReadOnly Property ProficienciesSC As String
        Get
            Return my_proficiencies_SC
        End Get
    End Property

    Public Property Name As String
        Set(value As String)
            names(1) = value
        End Set
        Get
            Return names(1)
        End Get
    End Property

    Public Property PluralName As String
        Set(value As String)
            names(2) = value
        End Set
        Get
            Return names(2)
        End Get
    End Property

    Public Property FactionID As Integer

    Public Property UpgradeTroop1 As Integer
        Set(value As Integer)
            upgradePath(0) = value
        End Set
        Get
            Return upgradePath(0)
        End Get
    End Property

    Public Property UpgradeTroop1ErrorCode As String
        Set(value As String)
            upgradePathError(0) = value
        End Set
        Get
            If Not IsNothing(upgradePathError(0)) Then
                Return upgradePathError(0)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property UpgradeTroop2 As Integer
        Set(value As Integer)
            upgradePath(1) = value
        End Set
        Get
            Return upgradePath(1)
        End Get
    End Property

    Public Property UpgradeTroop2ErrorCode As String
        Set(value As String)
            upgradePathError(1) = value
        End Set
        Get
            If Not IsNothing(upgradePathError(1)) Then
                Return upgradePathError(1)
            Else
                Return String.Empty
            End If
        End Get
    End Property

    Public Property Face1 As String
        Set(value As String)
            face_codes(0) = value
        End Set
        Get
            Return face_codes(0)
        End Get
    End Property

    Public Property Face2 As String
        Set(value As String)
            face_codes(1) = value
        End Set
        Get
            Return face_codes(1)
        End Get
    End Property

    Public Property Items As List(Of Integer)

    Public ReadOnly Property ItemFlags As List(Of ULong)
        Get
            Return my_itemFlags
        End Get
    End Property

    Public Property Skills As Integer() = New Integer(41) {}

    Public Property Persuasion As Integer
        Set(value As Integer)
            Skills(0) = value
        End Set
        Get
            Return Skills(0)
        End Get
    End Property

    Public Property PrisonerManagement As Integer
        Set(value As Integer)
            Skills(1) = value
        End Set
        Get
            Return Skills(1)
        End Get
    End Property

    Public Property Leadership As Integer
        Set(value As Integer)
            Skills(2) = value
        End Set
        Get
            Return Skills(2)
        End Get
    End Property

    Public Property Trade As Integer
        Set(value As Integer)
            Skills(3) = value
        End Set
        Get
            Return Skills(3)
        End Get
    End Property

    Public Property Tactics As Integer
        Set(value As Integer)
            Skills(4) = value
        End Set
        Get
            Return Skills(4)
        End Get
    End Property

    Public Property Pathfinding As Integer
        Set(value As Integer)
            Skills(5) = value
        End Set
        Get
            Return Skills(5)
        End Get
    End Property

    Public Property Spotting As Integer
        Set(value As Integer)
            Skills(6) = value
        End Set
        Get
            Return Skills(6)
        End Get
    End Property

    Public Property InventoryManagement As Integer
        Set(value As Integer)
            Skills(7) = value
        End Set
        Get
            Return Skills(7)
        End Get
    End Property

    Public Property WoundTreatment As Integer
        Set(value As Integer)
            Skills(8) = value
        End Set
        Get
            Return Skills(8)
        End Get
    End Property

    Public Property Surgery As Integer
        Set(value As Integer)
            Skills(9) = value
        End Set
        Get
            Return Skills(9)
        End Get
    End Property

    Public Property FirstAid As Integer
        Set(value As Integer)
            Skills(10) = value
        End Set
        Get
            Return Skills(10)
        End Get
    End Property

    Public Property Engineer As Integer
        Set(value As Integer)
            Skills(11) = value
        End Set
        Get
            Return Skills(11)
        End Get
    End Property

    Public Property HorseArchery As Integer
        Set(value As Integer)
            Skills(12) = value
        End Set
        Get
            Return Skills(12)
        End Get
    End Property

    Public Property Looting As Integer
        Set(value As Integer)
            Skills(13) = value
        End Set
        Get
            Return Skills(13)
        End Get
    End Property

    Public Property Training As Integer
        Set(value As Integer)
            Skills(14) = value
        End Set
        Get
            Return Skills(14)
        End Get
    End Property

    Public Property Tracking As Integer
        Set(value As Integer)
            Skills(15) = value
        End Set
        Get
            Return Skills(15)
        End Get
    End Property

    Public Property WeaponMaster As Integer
        Set(value As Integer)
            Skills(16) = value
        End Set
        Get
            Return Skills(16)
        End Get
    End Property

    Public Property Shield As Integer
        Set(value As Integer)
            Skills(17) = value
        End Set
        Get
            Return Skills(17)
        End Get
    End Property

    Public Property Athletics As Integer
        Set(value As Integer)
            Skills(18) = value
        End Set
        Get
            Return Skills(18)
        End Get
    End Property

    Public Property Riding As Integer
        Set(value As Integer)
            Skills(19) = value
        End Set
        Get
            Return Skills(19)
        End Get
    End Property

    Public Property Ironflesh As Integer
        Set(value As Integer)
            Skills(20) = value
        End Set
        Get
            Return Skills(20)
        End Get
    End Property

    Public Property PowerStrike As Integer
        Set(value As Integer)
            Skills(21) = value
        End Set
        Get
            Return Skills(21)
        End Get
    End Property

    Public Property PowerThrow As Integer
        Set(value As Integer)
            Skills(22) = value
        End Set
        Get
            Return Skills(22)
        End Get
    End Property

    Public Property PowerDraw As Integer
        Set(value As Integer)
            Skills(23) = value
        End Set
        Get
            Return Skills(23)
        End Get
    End Property

    Public Property Strength As Integer
        Set(value As Integer)
            Attributes(0) = value
        End Set
        Get
            Return Attributes(0)
        End Get
    End Property

    Public Property Agility As Integer
        Set(value As Integer)
            Attributes(1) = value
        End Set
        Get
            Return Attributes(1)
        End Get
    End Property

    Public Property Intelligence As Integer
        Set(value As Integer)
            Attributes(2) = value
        End Set
        Get
            Return Attributes(2)
        End Get
    End Property

    Public Property Charisma As Integer
        Set(value As Integer)
            Attributes(3) = value
        End Set
        Get
            Return Attributes(3)
        End Get
    End Property

    Public Property Level As Integer
        Set(value As Integer)
            Attributes(4) = value
        End Set
        Get
            Return Attributes(4)
        End Get
    End Property

    Public Property OneHanded As Integer
        Set(value As Integer)
            my_proficiencies(0) = value
        End Set
        Get
            Return my_proficiencies(0)
        End Get
    End Property

    Public Property TwoHanded As Integer
        Set(value As Integer)
            my_proficiencies(1) = value
        End Set
        Get
            Return my_proficiencies(1)
        End Get
    End Property

    Public Property Polearm As Integer
        Set(value As Integer)
            my_proficiencies(2) = value
        End Set
        Get
            Return my_proficiencies(2)
        End Get
    End Property

    Public Property Archery As Integer
        Set(value As Integer)
            my_proficiencies(3) = value
        End Set
        Get
            Return my_proficiencies(3)
        End Get
    End Property

    Public Property Crossbow As Integer
        Set(value As Integer)
            my_proficiencies(4) = value
        End Set
        Get
            Return my_proficiencies(4)
        End Get
    End Property

    Public Property Throwing As Integer
        Set(value As Integer)
            my_proficiencies(5) = value
        End Set
        Get
            Return my_proficiencies(5)
        End Get
    End Property

    Public Property Firearm As Integer
        Set(value As Integer)
            my_proficiencies(6) = value
        End Set
        Get
            Return my_proficiencies(6)
        End Get
    End Property

#End Region
        */
    }
}
