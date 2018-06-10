Public Class SkillHunter

#Region "Const"

    Public Const KNOWS As String = "knows_"
    Public Const EMPTY As String = "nothing"
    Public Const SPACE As String = " "
    Public Const DOUBLESPACE As String = SPACE + SPACE
    Public Const FilesPath As String = ".\files\"

#End Region

#Region "Properties"

    Public Shared ReadOnly Property DebugMode As Boolean = False

    Public ReadOnly Property Skills As Integer() = New Integer(47) {} 'was 41 before for 42 slots

    Public Shared ReadOnly Property Skillnames As String() = New String() {"persuasion", "reserved_4", "reserved_3", "reserved_2", "reserved_1", "prisoner_management", "leadership", "trade", "tactics",
                                                        "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting",
                                                        "reserved_8", "reserved_7", "reserved_6", "reserved_5", "trainer", "tracking", "reserved_12", "reserved_11", "reserved_10", "reserved_9",
                                                        "weapon_master", "shield", "athletics", "riding", "reserved_16", "reserved_15", "reserved_14", "ironflesh", "power_strike", "power_throw",
                                                        "power_draw", "reserved_13", "reserved_17", "reserved_18", "reserved_19", "reserved_20", "reserved_21", "reserved_22",
                                                        "reserved_23", "reserved_24"} 'Change known later in header files if needed to real skills instead of reserved_X - V5

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
        For index = 0 To Skillnames.Length - 1
            If Not Skillnames(index).Substring(Skillnames(index).Length - 1).Equals("_") Then
                Skillnames(index) += "_"
            End If
        Next
        ResetSkillArray()
    End Sub

    Private Sub ResetSkillArray()
        For index = 0 To Skills.Length - 1
            Skills(index) = -1
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
        Skills(0) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'persuasion
        Skills(1) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'prisoner_management
        Skills(2) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'leadership
        Skills(3) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'trade

        'read second set of values
        'Hex$ method breaks on Stannis Bartheon (large number in TempArray(1))
        'HexString = Right$("0000000" & Hex$(TempArray(1)), 8)
        HexString = Dec2Hex(tempArray(1))
        Skills(4) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'tactics
        Skills(5) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'pathfinding
        Skills(6) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'spotting
        Skills(7) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'inventory_management
        Skills(8) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'wound_treatment
        Skills(9) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'surgery
        Skills(10) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'first_aid
        Skills(11) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'engineer

        'read third set of values
        'HexString = Right$("0000000" & Hex$(TempArray(2)), 8)
        HexString = Dec2Hex(tempArray(2))
        Skills(12) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'horse_archery
        Skills(13) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'looting
        Skills(14) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'trainer
        Skills(15) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'tracking

        'read fourth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(3)), 8)
        HexString = Dec2Hex(tempArray(3))
        Skills(16) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'weapon_master
        Skills(17) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'shield
        Skills(18) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'athletics
        Skills(19) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'riding

        'read fifth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(4)), 8)
        HexString = Dec2Hex(tempArray(4))
        Skills(20) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'ironflesh
        Skills(21) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'power_strike
        Skills(22) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'power_throw
        Skills(23) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'power_draw
    End Sub

    Private Sub StartUpAll(skill_line As String)
        Dim HexString As String
        Dim tempArray As String() '274 131072 0 1 0 0
        tempArray = Split(skill_line)

        'read first set of values (if value is A then set it to 10)
        'HexString = Right$("0000000" & Hex$(TempArray(0)), 8)
        HexString = Dec2Hex(tempArray(0))
        Skills(0) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'persuasion | - - - X - - -
        Skills(1) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_IV | - - - X - - -
        Skills(2) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_III | - - - X - - -
        Skills(3) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_II | - - - X - - -
        Skills(4) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'reserved_I | - - - X - - -
        Skills(5) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'prisoner_management | - - - X - - -
        Skills(6) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'leadership | - - - X - - -
        Skills(7) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'trade | - - - X - - -

        'read second set of values
        'Hex$ method breaks on Stannis Bartheon (large number in TempArray(1))
        'HexString = Right$("0000000" & Hex$(TempArray(1)), 8)
        HexString = Dec2Hex(tempArray(1))
        Skills(8) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'tactics | - - - X - - -
        Skills(9) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'pathfinding | - - - X - - -
        Skills(10) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'spotting | - - - X - - -
        Skills(11) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'inventory_management | - - - X - - -
        Skills(12) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'wound_treatment | - - - X - - -
        Skills(13) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'surgery | - - - X - - -
        Skills(14) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'first_aid | - - - X - - -
        Skills(15) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'engineer | - - - X - - -

        'read third set of values
        'HexString = Right$("0000000" & Hex$(TempArray(2)), 8)
        HexString = Dec2Hex(tempArray(2))
        Skills(16) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'horse_archery | - - - X - - -
        Skills(17) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'looting | - - - X - - -
        Skills(18) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_VIII | - - - X - - -
        Skills(19) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_VII | - - - X - - -
        Skills(20) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'reserved_VI | - - - X - - -
        Skills(21) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'reserved_V | - - - X - - -
        Skills(22) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'trainer | - - - X - - -
        Skills(23) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'tracking | - - - X - - -

        'read fourth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(3)), 8)
        HexString = Dec2Hex(tempArray(3))
        Skills(24) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XII ?
        Skills(25) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XI ? 
        Skills(26) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_X ?
        Skills(27) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'reserved_IV | - - - X - - -
        Skills(28) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'weapon_master | - - - X - - -
        Skills(29) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'shield | - - - X - - -
        Skills(30) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'athletics | - - - X - - -
        Skills(31) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'riding | - - - X - - -

        'read fifth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(4)), 8)
        HexString = Dec2Hex(tempArray(4))
        Skills(32) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XVI ?
        Skills(33) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XV | - - - X - - -
        Skills(34) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'reserved_XIV | - - - X - - -
        Skills(35) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'ironflesh | - - - X - - -
        Skills(36) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'power_strike | - - - X - - -
        Skills(37) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'power_throw | - - - X - - -
        Skills(38) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'power_draw | - - - X - - -
        Skills(39) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'reserved_XIII

        'read sixth set of values
        'HexString = Right$("0000000" & Hex$(TempArray(5)), 8)
        HexString = Dec2Hex(tempArray(5))
        Skills(40) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'reserved_XVII
        Skills(41) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'reserved_XVIII
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
        MsgBox("Error" + SPACE + "#111" & Err.Number & ":" & Environment.NewLine & Err.Description)

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
        MsgBox("Error" + SPACE + "#222" & Err.Number & ":" & Environment.NewLine & Err.Description)

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
        MsgBox("Error" + SPACE + "#333" & Err.Number & ":" & Environment.NewLine & Err.Description)

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
        MsgBox("Error" + SPACE + "#444" & Err.Number & ":" & Environment.NewLine & Err.Description)

Hex2Dec_End:
    End Function

#End Region

#End Region

End Class
