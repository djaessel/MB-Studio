Public Class SkillHunter

#Region "Attributes"

    Public Const KNOWS As String = "knows_"
    Public Const EMPTY As String = "nothing"
    Public Const SPACE As String = " "
    Public Const DOUBLESPACE As String = SPACE + SPACE
    Public Const FilesPath As String = ".\files\"

    Private m_skills As Integer() = New Integer(47) {} ' was 41 before for 42 slots

    'Private Shared m_text_skills As String() = New String() {"persuasion", "prisoner_management", "leadership", "trade", "tactics", "pathfinding", "spotting",
    '                                                    "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery",
    '                                                    "looting", "trainer", "tracking", "weapon_master", "shield", "athletics", "riding", "ironflesh",
    '                                                    "power_strike", "power_throw", "power_draw"} 'V1

    'Private Shared m_text_skills As String() = New String() {"persuasion", "reserved_I", "reserved_II", "reserved_III", "reserved_IV", "prisoner_management", "leadership", "trade", "tactics",
    '                                                    "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting",
    '                                                    "reserved_V", "reserved_VI", "reserved_VII", "reserved_VIII", "trainer", "tracking", "reserved_IX", "reserved_X", "reserved_XI", "maintenance",
    '                                                    "weapon_master", "shield", "athletics", "riding", "reserved_XIII", "sea_king", "navigation", "ironflesh", "power_strike", "power_throw",
    '                                                    "power_draw", "reserved_XVI", "reserved_XVII", "reserved_XVIII", "reserved_XIX", "reserved_XX", "reserved_XXI", "reserved_XXII",
    '                                                    "reserved_XXIII", "reserved_XXIV"} 'V2

    'Private Shared m_text_skills As String() = New String() {"persuasion", "reserved_1", "reserved_2", "reserved_3", "reserved_4", "prisoner_management", "leadership", "trade", "tactics",
    '                                                    "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting",
    '                                                    "reserved_5", "reserved_6", "reserved_7", "reserved_8", "trainer", "tracking", "reserved_9", "reserved_10", "reserved_11", "reserved_12",
    '                                                    "weapon_master", "shield", "athletics", "riding", "reserved_13", "reserved_14", "reserved_15", "ironflesh", "power_strike", "power_throw",
    '                                                    "power_draw", "reserved_16", "reserved_17", "reserved_18", "reserved_19", "reserved_20", "reserved_21", "reserved_22",
    '                                                    "reserved_23", "reserved_24"} ' Change known later in header files if needed - V3

    'Private Shared m_text_skills As String() = New String() {"persuasion", "reserved_4", "reserved_3", "reserved_2", "reserved_1", "prisoner_management", "leadership", "trade", "tactics",
    '                                                    "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting",
    '                                                    "reserved_8", "reserved_7", "reserved_6", "reserved_5", "trainer", "tracking", "reserved_12", "reserved_11", "reserved_10", "reserved_9",
    '                                                    "weapon_master", "shield", "athletics", "riding", "reserved_16", "reserved_15", "reserved_14", "ironflesh", "power_strike", "power_throw",
    '                                                    "power_draw", "reserved_13", "reserved_24", "reserved_23", "reserved_22", "reserved_21", "reserved_20", "reserved_19",
    '                                                    "reserved_18", "reserved_17"} ' Change known later in header files if needed to real skills instead of reserved_X - V4

    Private Shared m_text_skills As String() = New String() {"persuasion", "reserved_4", "reserved_3", "reserved_2", "reserved_1", "prisoner_management", "leadership", "trade", "tactics",
                                                        "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting",
                                                        "reserved_8", "reserved_7", "reserved_6", "reserved_5", "trainer", "tracking", "reserved_12", "reserved_11", "reserved_10", "reserved_9",
                                                        "weapon_master", "shield", "athletics", "riding", "reserved_16", "reserved_15", "reserved_14", "ironflesh", "power_strike", "power_throw",
                                                        "power_draw", "reserved_13", "reserved_17", "reserved_18", "reserved_19", "reserved_20", "reserved_21", "reserved_22",
                                                        "reserved_23", "reserved_24"} ' Change known later in header files if needed to real skills instead of reserved_X - V5


    Private Shared _debugMode As Boolean = False

#End Region

    Public Sub New()
        InitialiseArrays()
    End Sub

#Region "Methods"

    Public Sub ReadSkills(skill_line As String)
        ResetSkillArray()
        'startUp(skill_line) 'only 24 skills
        StartUpAll(skill_line) 'all 48 Skills (are there more?)
    End Sub

    Private Sub InitialiseArrays()
        For index = 0 To m_text_skills.Length - 1
            If Not m_text_skills(index).Substring(m_text_skills(index).Length - 1).Equals("_") Then
                m_text_skills(index) += "_"
            End If
        Next
        ResetSkillArray()
    End Sub

    Private Sub ResetSkillArray()
        For index = 0 To m_skills.Length - 1
            m_skills(index) = -1
        Next
    End Sub

    Private Shared Function RightS(ByVal sText As String, ByVal nLen As Integer) As String 'Method Right() in VB.NET nachgebaut
        If nLen > sText.Length Then nLen = sText.Length
        Return (sText.Substring(sText.Length - nLen))
    End Function

    Private Sub StartUp(skill_line As String)
        Dim HexString As String
        Dim tempArray As String() '274 131072 0 1 0 0
        tempArray = Split(skill_line)

        'read first set of values (if value is A then set it to 10)
        'HexString = Right$("0000000" & Hex$(TempArray(0)), 8)
        HexString = Dec2Hex(tempArray(0))
        m_skills(0) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'persuasion
        m_skills(1) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'prisoner_management
        m_skills(2) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'leadership
        m_skills(3) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'trade

        'read second set of values
        'Hex$ method breaks on Stannis Bartheon (large number in TempArray(1))
        'HexString = Right$("0000000" & Hex$(TempArray(1)), 8)
        HexString = Dec2Hex(tempArray(1))
        m_skills(4) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'tactics
        m_skills(5) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'pathfinding
        m_skills(6) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'spotting
        m_skills(7) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'inventory_management
        m_skills(8) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'wound_treatment
        m_skills(9) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'surgery
        m_skills(10) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'first_aid
        m_skills(11) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'engineer

        'read third set of values
        'HexString = Right$("0000000" & Hex$(TempArray(2)), 8)
        HexString = Dec2Hex(tempArray(2))
        m_skills(12) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'horse_archery
        m_skills(13) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'looting
        m_skills(14) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'trainer
        m_skills(15) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'tracking

        'read fourth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(3)), 8)
        HexString = Dec2Hex(tempArray(3))
        m_skills(16) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'weapon_master
        m_skills(17) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'shield
        m_skills(18) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'athletics
        m_skills(19) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'riding

        'read fifth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(4)), 8)
        HexString = Dec2Hex(tempArray(4))
        m_skills(20) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'ironflesh
        m_skills(21) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'power_strike
        m_skills(22) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'power_throw
        m_skills(23) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'power_draw
    End Sub

    Private Sub StartUpAll(skill_line As String)
        Dim HexString As String
        Dim tempArray As String() '274 131072 0 1 0 0
        tempArray = Split(skill_line)

        'read first set of values (if value is A then set it to 10)
        'HexString = Right$("0000000" & Hex$(TempArray(0)), 8)
        HexString = Dec2Hex(tempArray(0))
        m_skills(0) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'persuasion | - - - X - - -
        m_skills(1) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_IV | - - - X - - -
        m_skills(2) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_III | - - - X - - -
        m_skills(3) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_II | - - - X - - -
        m_skills(4) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'reserved_I | - - - X - - -
        m_skills(5) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'prisoner_management | - - - X - - -
        m_skills(6) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'leadership | - - - X - - -
        m_skills(7) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'trade | - - - X - - -

        'read second set of values
        'Hex$ method breaks on Stannis Bartheon (large number in TempArray(1))
        'HexString = Right$("0000000" & Hex$(TempArray(1)), 8)
        HexString = Dec2Hex(tempArray(1))
        m_skills(8) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'tactics | - - - X - - -
        m_skills(9) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'pathfinding | - - - X - - -
        m_skills(10) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'spotting | - - - X - - -
        m_skills(11) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'inventory_management | - - - X - - -
        m_skills(12) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'wound_treatment | - - - X - - -
        m_skills(13) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'surgery | - - - X - - -
        m_skills(14) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'first_aid | - - - X - - -
        m_skills(15) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'engineer | - - - X - - -

        'read third set of values
        'HexString = Right$("0000000" & Hex$(TempArray(2)), 8)
        HexString = Dec2Hex(tempArray(2))
        m_skills(16) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'horse_archery | - - - X - - -
        m_skills(17) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'looting | - - - X - - -
        m_skills(18) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_VIII | - - - X - - -
        m_skills(19) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_VII | - - - X - - -
        m_skills(20) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'reserved_VI | - - - X - - -
        m_skills(21) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'reserved_V | - - - X - - -
        m_skills(22) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'trainer | - - - X - - -
        m_skills(23) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'tracking | - - - X - - -

        'read fourth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(3)), 8)
        HexString = Dec2Hex(tempArray(3))
        m_skills(24) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XII ?
        m_skills(25) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XI ? 
        m_skills(26) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_X ?
        m_skills(27) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_IV | - - - X - - -
        m_skills(28) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'weapon_master | - - - X - - -
        m_skills(29) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'shield | - - - X - - -
        m_skills(30) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'athletics | - - - X - - -
        m_skills(31) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'riding | - - - X - - -

        'read fifth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(4)), 8)
        HexString = Dec2Hex(tempArray(4))
        m_skills(32) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XVI ?
        m_skills(33) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XV | - - - X - - -
        m_skills(34) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_XIV | - - - X - - -
        m_skills(35) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'ironflesh | - - - X - - -
        m_skills(36) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'power_strike | - - - X - - -
        m_skills(37) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'power_throw | - - - X - - -
        m_skills(38) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'power_draw | - - - X - - -
        m_skills(39) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'reserved_XIII

        'read sixth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(5)), 8)
        HexString = Dec2Hex(tempArray(5))
        m_skills(40) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XVII
        m_skills(41) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XVIII
        'm_skills(42) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_XIX ???
        'm_skills(43) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_XX ???
        'm_skills(44) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'reserved_XXI ???
        'm_skills(45) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'reserved_XXII ???
        'm_skills(46) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'reserved_XXIII ???
        'm_skills(47) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'reserved_XXIV ???
    End Sub

    Public Shared Function RemoveItemDoublesFromArray(array As String()) As String()
        Dim retList As New List(Of String)
        For Each stext As String In array
            If Not retList.Contains(stext) Then
                retList.Add(stext)
            End If
        Next
        Return retList.ToArray()
    End Function

#Region "HEX AND DEC"

    Private Shared Function ReplaceHex(hextex As String) As String
        hextex = Replace(hextex, "A", "10")
        hextex = Replace(hextex, "B", "11")
        hextex = Replace(hextex, "C", "12")
        hextex = Replace(hextex, "D", "13")
        hextex = Replace(hextex, "E", "14")
        hextex = Replace(hextex, "F", "15")
        Return hextex
    End Function

    Private Shared Function ReplaceDec(dectex As String) As String
        dectex = Replace(dectex, "10", "A")
        dectex = Replace(dectex, "11", "B")
        dectex = Replace(dectex, "12", "C")
        dectex = Replace(dectex, "13", "D")
        dectex = Replace(dectex, "14", "E")
        dectex = Replace(dectex, "15", "F")
        Return dectex
    End Function

    Public Shared Function Dec2Hex(ByVal DecimalIn As Object) As String
        On Error GoTo Dec2Hex_Error
        Dim X As Integer
        Dim BinaryString As String
        Const BinValues = "*0000*0001*0010*0011" &
                          "*0100*0101*0110*0111" &
                          "*1000*1001*1010*1011" &
                          "*1100*1101*1110*1111*"
        Const HexValues = "0123456789ABCDEF"
        Const MaxNumOfBits As Long = 96
        Dec2Hex = String.Empty
        BinaryString = String.Empty
        DecimalIn = Int(CLng(DecimalIn))
        Do While DecimalIn <> 0
            BinaryString = Trim$(Str$(DecimalIn - 2 *
                   Int(DecimalIn / 2))) & BinaryString
            DecimalIn = Int(DecimalIn / 2)
        Loop
        BinaryString = New String("0"c, (4 - Len(BinaryString) Mod 4) Mod 4) & BinaryString
        For X = 1 To Len(BinaryString) - 3 Step 4
            Dec2Hex = Dec2Hex & Mid$(HexValues,
              (4 + InStr(BinValues, "*" &
              Mid$(BinaryString, X, 4) & "*")) \ 5, 1)
        Next
        'check if value is empty, is so default to zero
        If Dec2Hex.Equals(String.Empty) Then
            Dec2Hex = "0"
        End If
        'add leading zero's
        Dec2Hex = RightS("00000000" & Dec2Hex, 8)

        GoTo Dec2Hex_End

Dec2Hex_Error:
        MsgBox("Error" + SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())

Dec2Hex_End:

    End Function

    Public Shared Function Dec2Hex_16CHARS(ByVal DecimalIn As Object) As String
        On Error GoTo Dec2Hex_Error
        Dim X As Integer
        Dim BinaryString As String
        Const BinValues = "*0000*0001*0010*0011" &
                          "*0100*0101*0110*0111" &
                          "*1000*1001*1010*1011" &
                          "*1100*1101*1110*1111*"
        Const HexValues = "0123456789ABCDEF"
        Const MaxNumOfBits As Long = 96
        Dec2Hex_16CHARS = String.Empty
        BinaryString = String.Empty
        DecimalIn = Int(CULng(DecimalIn))
        Do While DecimalIn <> 0
            BinaryString = Trim$(Str$(DecimalIn - 2 *
                   Int(DecimalIn / 2))) & BinaryString
            DecimalIn = Int(DecimalIn / 2)
        Loop
        BinaryString = New String("0"c, (4 - Len(BinaryString) Mod 4) Mod 4) & BinaryString
        For X = 1 To Len(BinaryString) - 3 Step 4
            Dec2Hex_16CHARS = Dec2Hex_16CHARS & Mid$(HexValues,
              (4 + InStr(BinValues, "*" &
              Mid$(BinaryString, X, 4) & "*")) \ 5, 1)
        Next
        'check if value is empty, is so default to zero
        If Dec2Hex_16CHARS.Equals(String.Empty) Then
            Dec2Hex_16CHARS = "0"
        End If
        'add leading zero's
        Dec2Hex_16CHARS = RightS("0000000000000000" & Dec2Hex_16CHARS, 16)

        GoTo Dec2Hex_End

Dec2Hex_Error:
        MsgBox("Error" + SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())

Dec2Hex_End:

    End Function

    Public Shared Function Hex2Dec(ByVal HexString As String) As Object
        On Error GoTo Hex2Dec_Error
        Dim X As Integer
        Dim BinStr As String = String.Empty
        Const TwoToThe49thPower As String = "562949953421312"
        Const BinValues = "0000000100100011" &
                          "0100010101100111" &
                          "1000100110101011" &
                          "1100110111101111"
        If Left$(HexString, 2) Like "&[hH]" Then
            HexString = Mid$(HexString, 3)
        End If
        If Len(HexString) <= 23 Then
            For X = 1 To Len(HexString)
                BinStr = BinStr & Mid$(BinValues, 4 * Val("&h" & Mid$(HexString, X, 1)) + 1, 4)
            Next
            Hex2Dec = CLng(0)
            For X = 0 To Len(BinStr) - 1
                If X < 50 Then
                    Hex2Dec = Hex2Dec + Val(Mid(BinStr, Len(BinStr) - X, 1)) * 2 ^ X
                Else
                    Hex2Dec = Hex2Dec + CLng(TwoToThe49thPower) * Val(Mid(BinStr, Len(BinStr) - X, 1)) * 2 ^ (X - 49)
                End If
            Next
            Hex2Dec = CLng(Hex2Dec)
            'MsgBox("TEST:" + SPACE + Convert.ToString(Hex2Dec))
            'Else
            ' Number is too big, handle error here
        End If

        GoTo Hex2Dec_End

Hex2Dec_Error:
        MsgBox("Error" + SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())

Hex2Dec_End:
    End Function

    Public Shared Function Hex2Dec_16CHARS(ByVal HexString As String) As Object
        On Error GoTo Hex2Dec_Error
        Dim X As Integer
        Dim BinStr As String = String.Empty
        Const TwoToThe49thPower As String = "562949953421312"
        Const BinValues = "0000000100100011" &
                          "0100010101100111" &
                          "1000100110101011" &
                          "1100110111101111"
        If Left$(HexString, 2) Like "&[hH]" Then
            HexString = Mid$(HexString, 3)
        End If
        If Len(HexString) <= 23 Then
            For X = 1 To Len(HexString)
                BinStr = BinStr & Mid$(BinValues, 4 * Val("&h" & Mid$(HexString, X, 1)) + 1, 4)
            Next
            Hex2Dec_16CHARS = CULng(0)
            For X = 0 To Len(BinStr) - 1
                If X < 50 Then
                    Hex2Dec_16CHARS = Hex2Dec_16CHARS + Val(Mid(BinStr, Len(BinStr) - X, 1)) * 2 ^ X
                Else
                    Hex2Dec_16CHARS = Hex2Dec_16CHARS + CULng(TwoToThe49thPower) * Val(Mid(BinStr, Len(BinStr) - X, 1)) * 2 ^ (X - 49)
                End If
            Next
            Hex2Dec_16CHARS = CULng(Hex2Dec_16CHARS)
            'MsgBox("TEST:" + SPACE + Convert.ToString(Hex2Dec))
            'Else
            ' Number is too big, handle error here
        End If

        GoTo Hex2Dec_End

Hex2Dec_Error:
        MsgBox("Error" + SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())

Hex2Dec_End:
    End Function

#End Region

    Public Shared Function TroopUpdate(TroopX As Troop) As String()
        On Error GoTo Update_Error

        'check the number of items they have added

        Dim tempString As String
        Dim TroopCode() As String = New String(5) {}
        Dim CurrentFlag As String = Dec2Hex(TroopX.Flags)

#Region "UPDATE TROOP FLAG"

        'If (CmbRace.ListIndex = Race) Then
        'race hasn't changed, do nothing
        'Else
        'clear the old flag
        '   CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not RaceArray(Race))
        'set the new flag
        '    CurrentFlag = Dec2Hex("&H" & CurrentFlag Or RaceArray(CmbRace.ListIndex))
        '    Race = CmbRace.ListIndex
        'End If
        '
        '   'check tf_male = 0
        '   If optGender(0).Value = True Then
        '      'add flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 0)
        '   Else
        '      'clear flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 0)
        '   End If
        '
        '   'check tf_female = 1
        '   If optGender(1).Value = True Then
        '      'add flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 1)
        '   Else
        '      'clear flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 1)
        '   End If

        '   'check tf_undead = 2
        '   If optGender(2).Value = True Then
        '      'add flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 2)
        '   Else
        '      'clear flag
        '      CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 2)
        '   End If

        'check troop_type_mask = 0x0000000f (kingdom lord wife may use it??)
        'dont change the flag, ignore it....

        'check hero flag (tf_hero = 0x00000010)
        'If chkHero.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 10)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 10)
        'End If

        'tf_inactive = 0x00000020
        'If chkInactive.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 20)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 20)
        'End If

        'tf_unkillable = 0x00000040
        'If chkUnkillable.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 40)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 40)
        'End If

        'tf_allways_fall_dead = 0x00000080
        'If chkFallDead.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 80)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 80)
        'End If

        'tf_no_capture_alive = 0x00000100
        'If chkNoCapture.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 100)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 100)
        'End If

        'tf_mounted = 0x00000400 #Troop's movement speed on map is determined by riding skill.
        'If chkMounted.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 400)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 400)
        'End If

        'tf_is_merchant = 0x00001000 #When set, troop does not equip stuff he owns
        'If chkMerchant.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 1000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 1000)
        'End If

        'check boots flag (tf_guarantee_boots = 0x00100000)
        'If chkBoots.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 100000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 100000)
        'End If

        'check armor flag (tf_guarantee_armor = 0x00200000)
        'If chkArmor.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 200000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 200000)
        'End If

        'check helmet flag (tf_guarantee_helmet = 0x00400000)
        'If chkHelmet.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 400000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 400000)
        'End If

        'tf_guarantee_gloves = 0x00800000
        'If chkGloves.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 800000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 800000)
        'End If

        'tf_guarantee_horse = 0x01000000
        'If chkHorse.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 1000000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 1000000)
        'End If

        'tf_guarantee_shield = 0x02000000
        'If chkShield.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 2000000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 2000000)
        'End If

        'tf_guarantee_ranged = 0x04000000
        'If chkRanged.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 4000000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 4000000)
        'End If

        'tf_unmoveable_in_party_window = 0x10000000
        'If chkUnmoveable.Value = Checked Then
        'add flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag Or "&H" & 10000000)
        'Else
        'clear flag
        'CurrentFlag = Dec2Hex("&H" & CurrentFlag And Not "&H" & 10000000)
        'End If

#End Region

        'update name/flag/upgrade information (row 0)
        CurrentFlag = Hex2Dec(CurrentFlag)
        tempString = TroopX.ID & SPACE & TroopX.ID & SPACE & TroopX.PluralName & SPACE & TroopX.DialogImage & SPACE & CurrentFlag & SPACE & TroopX.SceneCodeGZ & SPACE &
            TroopX.ReservedGZ & SPACE & TroopX.FactionID & SPACE & TroopX.UpgradeTroop1 & SPACE & TroopX.UpgradeTroop2
        TroopCode(0) = tempString

        'update item information (row 1)
        Dim i As Integer
        Dim iArray() As String
        tempString = SPACE + SPACE
        For i = 0 To TroopX.Items.Count - 1
            If TroopX.Items.Item(i).Equals(String.Empty) Then
                tempString = tempString & "-1" & " 0 "
            Else
                iArray = Split(TroopX.Items.Item(i), " - ")
                tempString = tempString & iArray(0) & " 0 "
            End If
        Next
        Dim TempItem As String = "-1"
        For i = (TroopX.Items.Count * 2) To 127
            If TempItem.Equals("0") Then
                tempString = tempString & "0 "
                TempItem = "-1"
            ElseIf TempItem.Equals("-1") Then
                tempString = tempString & "-1 "
                TempItem = "0"
            End If
        Next
        TroopCode(1) = tempString

        'update attribute information (row 2), first 2 elements are white space
        tempString = DOUBLESPACE & TroopX.Strength & SPACE & TroopX.Agility & SPACE & TroopX.Intelligence & SPACE & TroopX.Charisma & SPACE & TroopX.Level
        TroopCode(2) = tempString

        'update proficiencies information (row 3), first 1 element is white space
        tempString = SPACE & TroopX.OneHanded & SPACE & TroopX.TwoHanded & SPACE & TroopX.Polearm & SPACE & TroopX.Archery & SPACE & TroopX.Crossbow & SPACE & TroopX.Throwing & SPACE & TroopX.Firearm
        TroopCode(3) = tempString

        'update skills information (row 4)
        Dim SkillLine As String
        Dim SkillHexTemp As String

        'update first set of values (if value is 10 then set it to A)
        'HexString = Right$("0000000" & Hex$(TempArray(0)), 8)

        'update first set of skills
        SkillHexTemp = ReplaceDec(TroopX.Persuasion) & "0000" & ReplaceDec(TroopX.PrisonerManagement) & ReplaceDec(TroopX.Leadership) & ReplaceDec(TroopX.Trade)
        SkillLine = Hex2Dec(SkillHexTemp)
        'update second set of skills
        SkillHexTemp = ReplaceDec(TroopX.Tactics) & ReplaceDec(TroopX.Pathfinding) & ReplaceDec(TroopX.Spotting) & ReplaceDec(TroopX.InventoryManagement) & ReplaceDec(TroopX.WoundTreatment) &
            ReplaceDec(TroopX.Surgery) & ReplaceDec(TroopX.FirstAid) & ReplaceDec(TroopX.Engineer)
        SkillLine = SkillLine & SPACE & Hex2Dec(SkillHexTemp)
        'update third set of skills
        SkillHexTemp = ReplaceDec(TroopX.HorseArchery) & ReplaceDec(TroopX.Looting) & "0000" & ReplaceDec(TroopX.Training) & ReplaceDec(TroopX.Tracking)
        SkillLine = SkillLine & SPACE & Hex2Dec(SkillHexTemp)
        'update fourth set of values
        SkillHexTemp = "0000" & ReplaceDec(TroopX.WeaponMaster) & ReplaceDec(TroopX.Shield) & ReplaceDec(TroopX.Athletics) & ReplaceDec(TroopX.Riding)
        SkillLine = SkillLine & SPACE & Hex2Dec(SkillHexTemp)
        'update fifth set of values
        SkillHexTemp = "000" & ReplaceDec(TroopX.Ironflesh) & ReplaceDec(TroopX.PowerStrike) & ReplaceDec(TroopX.PowerThrow) & ReplaceDec(TroopX.PowerDraw) & "0"
        SkillLine = SkillLine & SPACE & Hex2Dec(SkillHexTemp)
        'add sixth value (zero)
        SkillLine = SkillLine & " 0"

        TroopCode(4) = SkillLine

        'update face information (row 5)
        TroopCode(5) = FaceFinder.GetFaceCodestring(TroopX)

        'MsgBox(TroopX.Name & " (" & TroopX.ID & ") has been updated!")

        GoTo Update_End

Update_Error:
        MsgBox("Error" + SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())
        GoTo Update_Real_End

Update_End:
        Return TroopCode
Update_Real_End:
    End Function

#End Region

#Region "Properties"

    Public ReadOnly Property Skills() As Integer()
        Get
            Return m_skills
        End Get
    End Property

    Public Shared ReadOnly Property Skillnames As String()
        Get
            Return m_text_skills
        End Get
    End Property

#Region "UNUSED PROPERTIES"

    'Public ReadOnly Property AdvancedSkills() As Integer()
    '    Get
    '        Return mx_skills
    '    End Get
    'End Property

    ' - - - - - - - - - - - - - - - - - - - - - - - - - - -

    'Public ReadOnly Property Persuasion() As Integer
    '    Get
    '        Return m_skills(0)
    '    End Get
    'End Property

    'Public ReadOnly Property PrisonerManagement() As Integer
    '    Get
    '        Return m_skills(1)
    '    End Get
    'End Property

    'Public ReadOnly Property Leadership() As Integer
    '    Get
    '        Return m_skills(2)
    '    End Get
    'End Property

    'Public ReadOnly Property Trade() As Integer
    '    Get
    '        Return m_skills(3)
    '    End Get
    'End Property

    'Public ReadOnly Property Tactics() As Integer
    '    Get
    '        Return m_skills(4)
    '    End Get
    'End Property

    'Public ReadOnly Property Pathfinding() As Integer
    '    Get
    '        Return m_skills(5)
    '    End Get
    'End Property

    'Public ReadOnly Property Spotting() As Integer
    '    Get
    '        Return m_skills(6)
    '    End Get
    'End Property

    'Public ReadOnly Property InventoryManagement() As Integer
    '    Get
    '        Return m_skills(7)
    '    End Get
    'End Property

    'Public ReadOnly Property WoundTreatment() As Integer
    '    Get
    '        Return m_skills(8)
    '    End Get
    'End Property

    'Public ReadOnly Property Surgery() As Integer
    '    Get
    '        Return m_skills(9)
    '    End Get
    'End Property

    'Public ReadOnly Property FirstAid() As Integer
    '    Get
    '        Return m_skills(10)
    '    End Get
    'End Property

    'Public ReadOnly Property Engineer() As Integer
    '    Get
    '        Return m_skills(11)
    '    End Get
    'End Property

    'Public ReadOnly Property HorseArchery() As Integer
    '    Get
    '        Return m_skills(12)
    '    End Get
    'End Property

    'Public ReadOnly Property Looting() As Integer
    '    Get
    '        Return m_skills(13)
    '    End Get
    'End Property

    'Public ReadOnly Property Training() As Integer
    '    Get
    '        Return m_skills(14)
    '    End Get
    'End Property

    'Public ReadOnly Property Tracking() As Integer
    '    Get
    '        Return m_skills(15)
    '    End Get
    'End Property

    'Public ReadOnly Property WeaponMaster() As Integer
    '    Get
    '        Return m_skills(16)
    '    End Get
    'End Property

    'Public ReadOnly Property Shield() As Integer
    '    Get
    '        Return m_skills(17)
    '    End Get
    'End Property

    'Public ReadOnly Property Athletics() As Integer
    '    Get
    '        Return m_skills(18)
    '    End Get
    'End Property

    'Public ReadOnly Property Riding() As Integer
    '    Get
    '        Return m_skills(19)
    '    End Get
    'End Property

    'Public ReadOnly Property Ironflesh() As Integer
    '    Get
    '        Return m_skills(20)
    '    End Get
    'End Property

    'Public ReadOnly Property PowerStrike() As Integer
    '    Get
    '        Return m_skills(21)
    '    End Get
    'End Property

    'Public ReadOnly Property PowerThrow() As Integer
    '    Get
    '        Return m_skills(22)
    '    End Get
    'End Property

    'Public ReadOnly Property PowerDraw() As Integer
    '    Get
    '        Return m_skills(23)
    '    End Get
    'End Property

#End Region

    Public Shared Property DebugMode() As Boolean
        Get
            Return _debugMode
        End Get
        Set(value As Boolean)
            _debugMode = value
        End Set
    End Property

#End Region

End Class
