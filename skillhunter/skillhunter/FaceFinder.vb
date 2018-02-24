Public Class FaceFinder

    Private my_faceCodes As String() = New String(1) {}
    Private Shared xer As Integer = 0

    Public ReadOnly Property Face1() As String
        Get
            Return my_faceCodes(0)
        End Get
    End Property

    Public ReadOnly Property Face2() As String
        Get
            Return my_faceCodes(1)
        End Get
    End Property

    '    Public Shared Function Dec2Hex(ByVal DecimalIn As Object) As String
    '        On Error GoTo Dec2Hex_Error
    '        Dim X As Integer
    '        Dim BinaryString As String
    '        Const BinValues = "*0000*0001*0010*0011" &
    '                          "*0100*0101*0110*0111" &
    '                          "*1000*1001*1010*1011" &
    '                          "*1100*1101*1110*1111*"
    '        Const HexValues = "0123456789ABCDEF"
    '        Const MaxNumOfBits As Long = 96
    '        Dec2Hex = ""
    '        BinaryString = ""
    '        DecimalIn = Int(CDec(DecimalIn))
    '        Do While DecimalIn <> 0
    '            BinaryString = Trim$(Str$(DecimalIn - 2 * Int(DecimalIn / 2))) & BinaryString
    '            DecimalIn = Int(DecimalIn / 2)
    '        Loop
    '        BinaryString = New String("0"c, (4 - Len(BinaryString) Mod 4) Mod 4) & BinaryString
    '        For X = 1 To Len(BinaryString) - 3 Step 4
    '            Dec2Hex = Dec2Hex & Mid$(HexValues, (4 + InStr(BinValues, "*" & Mid$(BinaryString, X, 4) & "*")) \ 5, 1)
    '        Next
    '
    '        GoTo Dec2Hex_End
    '
    'Dec2Hex_Error:
    '        MsgBox("Error" + SkillHunter.SPACE + "#" & Err.Number & ":" & Environment.NewLine & Err.ToString())
    '
    'Dec2Hex_End:
    '
    '    End Function

    Public Sub ReadFaceCode(faceCodeLine As String)
        Dim tempArray As String()
        tempArray = Split(faceCodeLine.Trim())
        'If SkillHunter.DebugMode Then
        ' Console.WriteLine(SkillHunter.Dec2Hex_16CHARS(tempArray(5)) + SkillHunter.SPACE + "=" + SkillHunter.SPACE + tempArray(5) + SkillHunter.SPACE + "-" + SkillHunter.SPACE +
        'CStr(xer) + SkillHunter.SPACE + "-" + SkillHunter.SPACE + "LINE:" + SkillHunter.SPACE + CStr((xer * 7) + 3))
        'End If
        'If SkillHunter.DebugMode Then
        ' Console.WriteLine(SkillHunter.Dec2Hex_16CHARS(tempArray(3)) + SkillHunter.SPACE + "="c + SkillHunter.SPACE + tempArray(3))
        'End If
        'my_faceCodes(0) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(0)) & SkillHunter.Dec2Hex_16CHARS(tempArray(1)) &
        ' SkillHunter.Dec2Hex_16CHARS(tempArray(2)) & SkillHunter.Dec2Hex_16CHARS(tempArray(3)))
        'my_faceCodes(1) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(4)) & SkillHunter.Dec2Hex_16CHARS(tempArray(5)) &
        ' SkillHunter.Dec2Hex_16CHARS(tempArray(6)) & SkillHunter.Dec2Hex_16CHARS(tempArray(7)))

        my_faceCodes(0) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(0)) & SkillHunter.Dec2Hex_16CHARS(tempArray(1)) &
                                SkillHunter.Dec2Hex_16CHARS(tempArray(2)) & SkillHunter.Dec2Hex_16CHARS(tempArray(3)))
        my_faceCodes(1) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(4)) & SkillHunter.Dec2Hex_16CHARS(tempArray(5)) &
                                SkillHunter.Dec2Hex_16CHARS(tempArray(6)) & SkillHunter.Dec2Hex_16CHARS(tempArray(7)))

        xer += 1
    End Sub

    Public ReadOnly Property FaceCodes
        Get
            Return my_faceCodes
        End Get
    End Property

    Public Shared Function GetFaceCodestring(troop As Troop) As String
        Dim face2_sec_obj As Object = (SkillHunter.Hex2Dec(Mid(troop.Face2, 19, 16)))
        If CLng(face2_sec_obj) > 0 Then
            face2_sec_obj = (SkillHunter.Hex2Dec(Mid(troop.Face2, 19, 16)) - 1)
        End If
        Return SkillHunter.DOUBLESPACE & SkillHunter.Hex2Dec(Mid(troop.Face1, 3, 16)) & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(troop.Face1, 19, 16)) &
            SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(troop.Face1, 35, 16)) & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(troop.Face1, 51, 16)) & SkillHunter.SPACE &
            SkillHunter.Hex2Dec(Mid(troop.Face2, 3, 16)) & SkillHunter.SPACE & face2_sec_obj & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(troop.Face2, 35, 16)) &
            SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(troop.Face2, 51, 16)) & SkillHunter.SPACE
    End Function

End Class
