Imports System.IO

Public Class Troop
    Inherits Skriptum

    Private ValuesX As String()
    Private ReadOnly names() As Object = New String(2) {}
    Private my_dialogImage As String
    'Private my_dialogImage_GZ As Integer
    Private my_sceneCode As String
    Private my_reserved As String
    Private my_sceneCode_GZ As ULong
    Private my_reserved_GZ As Integer
    Private my_flags As String
    Private my_flags_GZ As Integer
    Private ReadOnly upgradePath() As Integer = New Integer(1) {}
    Private ReadOnly upgradePathError() As String = New String(1) {}
    Private my_itemFlags As List(Of ULong)
    Private face_codes() As String = New String(1) {}
    Private my_proficiencies() As Integer = New Integer(6) {}
    Private my_proficiencies_SC As String
    Public Const Guarantee_All As Integer = 133169152
    Private Shared m_header_flags As HeaderVariable() = {}


    Public Sub New(values As String())
        MyBase.New(values(0).TrimStart().Split()(0), ObjectType.TROOP)
        ValuesX = values
        Init()
    End Sub

    Public Sub Reset()
        For index = 0 To 2
            names(index) = String.Empty
        Next
        FactionID = 0
        'For index = 0 To 2
        '    temp_values(index) = "0"
        'Next
        my_dialogImage = "0"
        my_sceneCode = "0"
        my_reserved = "0"
        For index = 0 To 1
            upgradePath(index) = 0
        Next
        If IsNothing(Items) Then
            Items = New List(Of Integer)
        Else
            Items.Clear()
        End If
        If IsNothing(my_itemFlags) Then
            my_itemFlags = New List(Of ULong)
        Else
            my_itemFlags.Clear()
        End If
        For index = 0 To Skills.Length - 1
            Skills(index) = 0
        Next
        For index = 0 To 1
            face_codes(0) = "0x0"
        Next
        For index = 0 To 4
            Attributes(index) = 0
        Next
        For index = 0 To 6
            my_proficiencies(index) = 0
        Next
    End Sub

    Private Sub SendErrorMessage1()
        MsgBox("ERROR! You probably have too many lines found in troop init! Check your files!")
    End Sub

    Private Sub SendErrorMessage2()
        MsgBox("ERROR! Too few lines found in troop init! Check your files!")
    End Sub

    Private Sub Init()
        Reset()
        'If IsNothing(ValuesX) Then
        '    my_flags_GZ = Guarantee_All 'SetFlags(Guarantee_All)
        If ValuesX.Length > 5 And ValuesX.Length < 8 Then 'ElseIf
            SetFirstLine(ValuesX(0))
            SetItems(ValuesX(1))
            SetAttributes(ValuesX(2))
            SetProficiencies(ValuesX(3))
            SetSkills(ValuesX(4)) 'NOT READY
            SetFaceCodes(ValuesX(5)) 'NOT READY
        ElseIf Not IsNothing(ValuesX) Then
            SendErrorMessage1()
        End If
    End Sub

    Private Sub SetFirstLine(lineOne As String)
        Dim line As String() = lineOne.Trim().Split()
        If line.Length >= 10 Then
            'ID = line(0)
            Name = line(1).Replace("_"c, " "c)
            PluralName = line(2).Replace("_"c, " "c)
            my_dialogImage = line(3) 'temp_values(0) = line(3)
            my_flags = line(4).Trim()
            If IsNumeric(my_flags) Then
                my_flags_GZ = line(4)
                my_flags = GetFlagsFromValue(SkillHunter.Dec2Hex(my_flags_GZ))
            Else 'If Not my_flags.Equals(String.Empty) Then
                my_flags_GZ = GetFlagsGZFromString(my_flags)
                'Else
                '    my_flags_GZ = 0
                '    my_flags = my_flags_GZ
                '    MsgBox("ERROR (0x12) - flags is empty!", MsgBoxStyle.Critical)
            End If
            SetSceneCode(line(5)) 'temp_values(1) = line(5)
            SetReserved(line(6)) 'temp_values(2) = line(6)
            FactionID = line(7)
            Try
                UpgradeTroop1 = line(8)
            Catch ex As Exception
                UpgradeTroop1ErrorCode = line(8)
                MsgBox("ERROR (0x4941) - " + ID + " - UPGRADE_TROOP1: " + UpgradeTroop1ErrorCode, MsgBoxStyle.Question)
            End Try
            Try
                UpgradeTroop2 = line(9)
            Catch ex As Exception
                UpgradeTroop2ErrorCode = line(9)
                MsgBox("ERROR (0x4942) - " + ID + " - UPGRADE_TROOP2: " + UpgradeTroop2ErrorCode, MsgBoxStyle.Question)
            End Try
        Else
            SendErrorMessage2()
        End If
    End Sub

    Public Sub SetItems(itemx As String)
        Dim tmpArray As String() = itemx.Trim().Split() 'Dim items As String() = itemx.Substring(2).Split("0"c)
        Dim items As String() = New String(tmpArray.Length / 2 - 1) {}
        Dim itemFlags As String() = New String(items.Length) {}
        For i = 0 To items.Length - 1
            items(i) = tmpArray(i * 2)
        Next
        For i = 0 To items.Length - 1
            itemFlags(i) = tmpArray(i * 2 + 1)
        Next
        Me.Items.Clear()
        my_itemFlags.Clear()
        For index = 0 To items.Length - 1
            items(index) = items(index).Trim()
            If Not items(index).Equals("-1") And Not items(index).Equals(String.Empty) Then
                Me.Items.Add(items(index))
                my_itemFlags.Add(itemFlags(index) >> 24)
            End If
        Next
    End Sub

    Private Shared Sub InitializeHeaderFlags(Optional file As String = "header_troops.py", Optional itemPointsX As List(Of HeaderVariable) = Nothing)
        Dim itemPoints As List(Of HeaderVariable)
        Dim file2 As String = "header_mb_decompiler.py"
        If IsNothing(itemPointsX) Then
            itemPoints = New List(Of HeaderVariable)
        Else
            itemPoints = itemPointsX
        End If
        Using sr As New StreamReader(SkillHunter.FilesPath + file)
            Dim s As String
            Dim sp As String()
            While Not sr.EndOfStream
                s = sr.ReadLine().Split("#")(0)
                If s.Split("_")(0).Equals("tf") Then
                    sp = s.Replace(" ", String.Empty).Replace("\t", String.Empty).Split("=")
                    If sp(1).Contains("0x") Then
                        itemPoints = RemoveHeaderVariableListEquals(itemPoints, sp(1).Substring(2))
                        itemPoints.Add(New HeaderVariable(sp(1).Substring(2), sp(0)))
                    ElseIf IsNumeric(sp(1)) Then
                        s = String.Empty
                        For index = 0 To (7 - sp(1).Length) ' because of 8 character hex -> 00000000
                            s += "0"c
                        Next
                        s += sp(1)
                        itemPoints = RemoveHeaderVariableListEquals(itemPoints, s)
                        itemPoints.Add(New HeaderVariable(s, sp(0)))
                        'Else
                        ' FIND tf_female|tf_hero or something like that in here
                    End If
                End If
            End While
        End Using
        If Not file.Equals(file2) Then
            InitializeHeaderFlags(file2, itemPoints)
        Else
            m_header_flags = itemPoints.ToArray()
        End If
    End Sub

    Private Shared Function RemoveHeaderVariableListEquals(list As List(Of HeaderVariable), hfValue As String) As List(Of HeaderVariable)
        Dim i As Integer = -1
        For index = 0 To list.Count - 1
            If list.Item(index).VariableValue.Equals(hfValue) Then
                i = index
                index = list.Count - 1
            End If
        Next
        If i >= 0 Then
            list.RemoveAt(i)
        End If
        Return list
    End Function

    Private Shared Function GetFlagsGZFromString(my_flags As String) As Integer
        Dim tmp As String
        Dim foundX As Boolean
        Dim flagsGZ As Integer = 0
        If m_header_flags.Length = 0 Then
            InitializeHeaderFlags()
        End If
        For Each flag As String In my_flags.Split("|"c)
            foundX = False
            For index = 0 To m_header_flags.Length - 1
                If m_header_flags(index).VariableName.Equals(flag) Then
                    tmp = m_header_flags(index).VariableValue
                    If tmp.Contains("0x") Then
                        tmp = SkillHunter.Hex2Dec(tmp.Replace("0x", String.Empty)).ToString()
                    End If
                    flagsGZ = flagsGZ Or Integer.Parse(tmp)
                    index = m_header_flags.Length
                End If
            Next
            If Not foundX And IsNumeric(flag) Then
                flagsGZ = flagsGZ Or Integer.Parse(flag)
            Else
                MsgBox("ERROR: 0x4943 - FLAG_NOT_FOUND " + flag)
            End If
        Next
        Return flagsGZ
    End Function

    Private Shared Function GetFlagsFromValue(value As String) As String
        Dim retur As String = String.Empty, tmp As String
        If m_header_flags.Length = 0 Then
            InitializeHeaderFlags()
        End If
        For i = 0 To m_header_flags.Length - 1
            tmp = m_header_flags(i).VariableValue.TrimStart("0")
            If tmp.Length = 0 Then
                tmp = "0"
            End If
            If tmp.Chars(0) = value.Chars(value.Length - tmp.Length) And tmp.Length > 1 Then
                retur += "|" + m_header_flags(i).VariableName
            ElseIf Not value.Chars(value.Length - tmp.Length) = "0"c Then
                Dim list As New List(Of HeaderVariable)
                Dim x_tmp As Integer = SkillHunter.Hex2Dec(value.Chars(value.Length - tmp.Length)), x_counter As Integer = 0
                If tmp.Length > 1 Then
                    For j = 0 To m_header_flags.Length - 1
                        If m_header_flags(j).VariableValue.TrimStart("0").Length = value.Substring(value.Length - tmp.Length).Length Then
                            list.Add(m_header_flags(j))
                        End If
                    Next
                    list.Reverse()
                    For Each variable As HeaderVariable In list
                        If x_counter < x_tmp Then
                            Dim xtert As Integer = SkillHunter.Hex2Dec(variable.VariableValue.Trim("0"))
                            If xtert <= x_tmp And xtert + x_counter <= x_tmp Then ' NOCHMAL ÜBERPRÜFEN
                                x_counter += xtert
                                If Not retur.Contains(variable.VariableName) Then
                                    retur += "|" + variable.VariableName
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next
        tmp = value.Substring(value.Length - 1) '.TrimStart("0")
        If Not tmp = "0" Then
            For Each troopFlag As HeaderVariable In m_header_flags
                Dim tmp2 As String = troopFlag.VariableValue.TrimStart("0").ToLower()
                'MsgBox("1: " + tmp + " : " + tmp2 + " : " + troopFlag.VariableName)
                If tmp2.Length < 2 Then
                    If tmp.ToLower().Equals(tmp2) Then
                        'MsgBox("2: " + tmp + " : " + tmp2 + " : " + troopFlag.VariableName)
                        retur += "|" + troopFlag.VariableName
                    End If
                End If
            Next
        End If
        If Not retur.Equals(String.Empty) Then
            retur = retur.Substring(1)
        End If
        Dim tmpS As String() = SkillHunter.RemoveItemDoublesFromArray(retur.Split("|"))
        retur = String.Empty
        For i = 0 To tmpS.Length - 1
            retur += tmpS(i)
            If i < tmpS.Length - 1 Then
                retur += "|"
            End If
        Next
        If retur.Equals(String.Empty) Then
            retur = "0"
        End If
        Return retur
    End Function

    Public Sub SetSceneCode(code As ULong)
        Dim entryPoint As Byte = 0
        my_sceneCode_GZ = code
        'my_sceneCode = code
        If code = 0 Then
            my_sceneCode = "no_scene"
        Else
            entryPoint = (code >> 16) And Byte.MaxValue
            code = code And UShort.MaxValue
            'code / CULng(Math.Pow(2, 16))
            'code -= entryPoint * CULng(Math.Pow(2, 16))
            my_sceneCode = Str(code) + "|"c + Str(entryPoint)
        End If
    End Sub

    'Public Sub SetSceneCodeGZ(code As String)
    '    Dim entryPoint As Byte = 0
    '    my_sceneCode = code
    '    If code.Contains("|"c) Then
    '        Dim sss As String() = code.Split("|"c)
    '        entryPoint = Byte.Parse(sss(1).TrimStart("(").TrimEnd(")").Trim())
    '        my_sceneCode_GZ
    '    End If
    'End Sub

    Public Sub SetReserved(reserved As String)
        Dim resV As String = "reserved"
        If reserved.Equals(resV) Then
            my_reserved_GZ = 0
        Else
            my_reserved_GZ = reserved
        End If
        If reserved = "0" Then
            my_reserved = resV
        Else
            my_reserved = reserved
        End If
    End Sub

    Public Sub SetAttributes(attributesX As String)
        Dim sp As String() = attributesX.TrimStart().Split()
        If sp.Length >= 5 Then
            Strength = CInt(sp(0))
            Agility = CInt(sp(1))
            Intelligence = CInt(sp(2))
            Charisma = CInt(sp(3))
            Level = CInt(sp(4))
        ElseIf sp.Length = 1 Then
            If IsNumeric(sp(1)) Then
                Dim attrib As ULong = ULong.Parse(sp(0))
                Strength = attrib And Byte.MaxValue
                Agility = (attrib >> 8) And Byte.MaxValue
                Intelligence = (attrib >> 16) And Byte.MaxValue
                Charisma = (attrib >> 24) And Byte.MaxValue
                Level = (attrib >> 32) And Byte.MaxValue
            Else
                SendErrorMessage2()
            End If
        Else
            SendErrorMessage2()
        End If
    End Sub

    Public Sub SetProficiencies(proficienciesX As String)
        Dim profS As String() = proficienciesX.Substring(1).Split()
        If profS.Length >= 7 Then
            For index = 0 To profS.Length - 1
                my_proficiencies(index) = CInt(profS(index))
            Next
            SetProficiesSC()
            Return
        ElseIf profS.Length = 1 Then
            If Not IsNumeric(profS(1)) Then
                my_proficiencies = GetProficiesFromSC(profS(0))
                Return
            End If
        End If
        SendErrorMessage2()
    End Sub

    Public Sub SetProficiesSC()
        Dim tmp As String = String.Empty

        If my_proficiencies(0) = my_proficiencies(1) = my_proficiencies(2) = my_proficiencies(3) = my_proficiencies(4) = my_proficiencies(5) Then
            tmp = "wp(" + my_proficiencies(5).ToString()
        ElseIf my_proficiencies(0) = my_proficiencies(1) = my_proficiencies(2) Then
            tmp = "wpe(" + my_proficiencies(2).ToString() + ", " + my_proficiencies(3).ToString() + ", " + my_proficiencies(4).ToString() + ", " + my_proficiencies(5).ToString()
        ElseIf (my_proficiencies(0) + 20) = my_proficiencies(1) = (my_proficiencies(2) + 10) Then
            tmp = "wp_melee(" + my_proficiencies(1).ToString()
        ElseIf Not ShortProficies Then
            If (OneHanded > 0) Then
                tmp += "wp_one_handed(" + my_proficiencies(0).ToString() 'OneHanded
            End If
            If (TwoHanded > 0) Then
                tmp += ")|wp_two_handed(" + my_proficiencies(1).ToString() 'TwoHanded
            End If
            If (Polearm > 0) Then
                tmp += ")|wp_polearm(" + my_proficiencies(2).ToString() 'Polearm
            End If
            If (Archery > 0) Then
                tmp += ")|wp_archery(" + my_proficiencies(3).ToString() 'Archery
            End If
            If (Crossbow > 0) Then
                tmp += ")|wp_crossbow(" + my_proficiencies(4).ToString() 'Crossbow
            End If
            If (Throwing > 0) Then
                tmp += ")|wp_throwing(" + my_proficiencies(5).ToString() 'Throwing
            End If
        Else
            tmp = "wpex("
            For i = 0 To 5
                tmp += my_proficiencies(i).ToString() + ","
            Next
            tmp = tmp.TrimEnd(","c)
        End If

        If (Firearm > 0) Then
            tmp += ")|wp_firearm(" + my_proficiencies(6).ToString()
        End If

        If tmp.Length = 0 Then
            tmp = "0"
        Else
            tmp = tmp.TrimStart(")|") + ")"c
        End If

        my_proficiencies_SC = tmp
    End Sub

    Public Shared Function GetProficiesFromSC(sc As String) As Integer()
        Dim profs(6) As Integer
        Dim sp As String() = sc.Split("|"c)
        Dim sp2 As String()
        Dim x As Integer
        For Each s As String In sp
            sp2 = s.Replace(" ", String.Empty).TrimEnd(")"c).Split("("c)
            If sp2(0).Equals("wp") Then
                x = CInt(sp2(1))
                profs(0) = profs(0) Or x
                profs(1) = profs(1) Or x
                profs(2) = profs(2) Or x
                profs(3) = profs(3) Or x
                profs(4) = profs(4) Or x
                profs(5) = profs(5) Or x
            ElseIf sp2(0).Equals("wpe") Then
                sp2 = sp2(1).Split(","c)
                x = CInt(sp2(0))
                profs(0) = profs(0) Or x
                profs(1) = profs(1) Or x
                profs(2) = profs(2) Or x
                profs(3) = profs(3) Or CInt(sp2(1))
                profs(4) = profs(4) Or CInt(sp2(2))
                profs(5) = profs(5) Or CInt(sp2(3))
            ElseIf sp2(0).Equals("wp_melee") Then
                x = CInt(sp2(1))
                profs(0) = profs(0) Or (x + 20)
                profs(1) = profs(1) Or x
                profs(2) = profs(2) Or (x + 10)
            ElseIf sp2(0).Equals("wpex") Then
                sp2 = sp2(1).Split(","c)
                profs(0) = profs(0) Or CInt(sp2(0))
                profs(1) = profs(1) Or CInt(sp2(1))
                profs(2) = profs(2) Or CInt(sp2(2))
                profs(3) = profs(3) Or CInt(sp2(3))
                profs(4) = profs(4) Or CInt(sp2(4))
                profs(5) = profs(5) Or CInt(sp2(5))
            ElseIf sp2(0).Equals("wp_one_handed") Then
                profs(0) = profs(0) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_two_handed") Then
                profs(1) = profs(1) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_polearm") Then
                profs(2) = profs(2) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_archery") Then
                profs(3) = profs(3) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_crossbow") Then
                profs(4) = profs(4) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_throwing") Then
                profs(5) = profs(5) Or CInt(sp2(1))
            ElseIf sp2(0).Equals("wp_firearms") Then
                profs(6) = profs(6) Or CInt(sp2(1))
            End If
        Next

        Return profs
    End Function

    Public Sub SetSkills(knowledge As String)
        Dim sk As New SkillHunter
        sk.ReadSkills(knowledge)
        Skills = sk.Skills
    End Sub

    Public Sub SetFaceCodes(facecodeX As String)
        Dim ff As New FaceFinder
        ff.ReadFaceCode(facecodeX)
        face_codes = ff.FaceCodes
    End Sub

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

    'Public ReadOnly Property TempValues() As String()
    '    Get
    '        Return temp_values
    '    End Get
    'End Property

    Public ReadOnly Property DialogImage As String
        Get
            Return my_dialogImage
        End Get
    End Property

    'Public ReadOnly Property DialogImageGZ As Integer
    '    Get
    '        Return my_dialogImage_GZ
    '    End Get
    'End Property

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

    'Public Property ID As String
    'Set(value As String)
    '        names(0) = value
    'End Set
    'Get
    'Return names(0)
    'End Get
    'End Property

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

End Class
