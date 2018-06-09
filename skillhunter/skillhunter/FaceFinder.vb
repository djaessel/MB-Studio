Public Class FaceFinder

    Public ReadOnly Property FaceCodes As String() = New String(1) {}

    Public ReadOnly Property Face1 As String
        Get
            Return FaceCodes(0)
        End Get
    End Property

    Public ReadOnly Property Face2 As String
        Get
            Return FaceCodes(1)
        End Get
    End Property

    Public Sub ReadFaceCode(faceCodeLine As String)
        Dim tempArray As String() = Split(faceCodeLine.Trim())

        FaceCodes(0) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(0)) & SkillHunter.Dec2Hex_16CHARS(tempArray(1)) &
                                SkillHunter.Dec2Hex_16CHARS(tempArray(2)) & SkillHunter.Dec2Hex_16CHARS(tempArray(3)))
        FaceCodes(1) = LCase("0x" & SkillHunter.Dec2Hex_16CHARS(tempArray(4)) & SkillHunter.Dec2Hex_16CHARS(tempArray(5)) &
                                SkillHunter.Dec2Hex_16CHARS(tempArray(6)) & SkillHunter.Dec2Hex_16CHARS(tempArray(7)))
    End Sub

    Public Shared Function GetFaceCodestring(face1 As String, face2 As String) As String
        Dim face2_sec_obj As Object = (SkillHunter.Hex2Dec(Mid(face2, 19, 16)))
        If CLng(face2_sec_obj) > 0 Then
            face2_sec_obj = (SkillHunter.Hex2Dec(Mid(face2, 19, 16)) - 1)
        End If
        Return SkillHunter.DOUBLESPACE & SkillHunter.Hex2Dec(Mid(face1, 3, 16)) & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(face1, 19, 16)) &
            SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(face1, 35, 16)) & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(face1, 51, 16)) & SkillHunter.SPACE &
            SkillHunter.Hex2Dec(Mid(face2, 3, 16)) & SkillHunter.SPACE & face2_sec_obj & SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(face2, 35, 16)) &
            SkillHunter.SPACE & SkillHunter.Hex2Dec(Mid(face2, 51, 16)) & SkillHunter.SPACE
    End Function

End Class
