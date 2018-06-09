Imports System.IO
Imports System.Globalization

Public Class Item
    Inherits Skriptum

#Region "Consts AND Attributes"

    Public Const ZERO_15_CHARS As String = "000000000000000"

    Private ReadOnly my_names As String() = New String(1) {}
    Private my_triggers As List(Of String)
    Private my_factions As List(Of Integer)
    Private my_meshes As List(Of String)
    Private my_special_values As String() = New String(2) {}
    Private my_item_stats As Integer() = New Integer(11) {}
    Private ReadOnly my_itemProperties As String = String.Empty
    Private my_meshcount As Integer = 1

    Private Shared m_header_imodbits As HeaderVariable() = {}
    Private Shared m_header_imods As HeaderVariable() = {}
    Private Shared m_header_itemProperties As HeaderVariable() = {}
    Private Shared m_header_itemCapabilitiyFlags As HeaderVariable() = {}

#End Region

    Public Sub New(Optional values As String() = Nothing)
        MyBase.New(values(0).TrimStart().Split()(0).Substring(4), ObjectType.ITEM)
        ResetItem()
        If Not IsNothing(values) Then
            SetFirstLine(values(0))
            SetFactionAndTriggerValues(values)
        End If
    End Sub

    Public ReadOnly Property ModBits As String
        Get
            Return GetItemModifiers_IMODBITS(SkillHunter.Dec2Hex_16CHARS(my_special_values(2)), True).TrimStart("|")
        End Get
    End Property

    Public ReadOnly Property Properties As String
        Get
            Return GetItemPropertiesFromValue(SkillHunter.Dec2Hex_16CHARS(my_special_values(0)))
        End Get
    End Property

    Public ReadOnly Property CapabilityFlags As String
        Get
            Return GetItemCapabilityFlagsFromValue(SkillHunter.Dec2Hex_16CHARS(my_special_values(1))) ', ID)
        End Get
    End Property

    Private Sub SetFirstLine(line As String)
        Dim xvalues As String() = line.Split(SkillHunter.SPACE)
        'ID = xvalues(0)
        Name = xvalues(1).Replace("_"c, " "c)
        PluralName = xvalues(2).Replace("_"c, " "c)
        my_meshcount = StrToInt(xvalues(3))

        For i = 0 To my_meshcount - 1
            my_meshes.Add(xvalues(4 + (i * 2)) + " " + xvalues(5 + (i * 2)))
        Next

        my_meshcount = my_meshcount * 2 'from here my_meshcount is used as local variable to get the correct new index below

        my_special_values(0) = xvalues(my_meshcount + 4) 'xvalues(0)
        my_special_values(1) = xvalues(my_meshcount + 5) 'xvalues(1)
        Price = StrToInt(xvalues(my_meshcount + 6)) 'StrToInt(xvalues(2))
        my_special_values(2) = xvalues(my_meshcount + 7) 'xvalues(3)
        Weight = Double.Parse(xvalues(my_meshcount + 8), CultureInfo.InvariantCulture) 'Double.Parse(xvalues(4), CultureInfo.InvariantCulture)    'weightvalue = Convert.ToDouble(xvalues(4))

        For i = 0 To 11
            my_item_stats(i) = StrToInt(xvalues(i + my_meshcount + 9))
        Next

    End Sub

    Private Sub SetFactionAndTriggerValues(tmpvalues() As String)
        Dim tmpS As String()
        Dim tmp As String = tmpvalues(1).Trim()
        Dim x As Integer
        'If Not tmp.Equals("0") And tmp.Length > 0 Then
        x = Integer.Parse(tmp)
        If tmpvalues.Length = 5 Then
            If tmpvalues(2).Equals(String.Empty) Then
                tmpvalues = New String() {tmpvalues(0), tmpvalues(1), tmpvalues(3), tmpvalues(4)}
            End If
        End If
        Try
            If x > 0 Then
                tmpS = tmpvalues(2).Split(SkillHunter.SPACE)
                For i = 0 To tmpS.Length - 1
                    my_factions.Add(Convert.ToInt32(tmpS(i)))
                Next
                If HasTriggers(StrToInt(tmpvalues(3).Trim())) Then
                    For i = 4 To (StrToInt(tmpvalues(3).Trim()) + 3)
                        my_triggers.Add(tmpvalues(i))
                    Next
                End If
            ElseIf x = 0 And HasTriggers(StrToInt(tmpvalues(2).Trim())) Then
                For i = 3 To (StrToInt(tmpvalues(2).Trim()) + 2)
                    my_triggers.Add(tmpvalues(i))
                Next
            ElseIf HasTriggers(HasTriggers(StrToInt(tmpvalues(2).Trim()))) Then
                ErrorMsg(x, 1)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString() + Environment.NewLine + tmpvalues(0) + "; " + tmp + Environment.NewLine + tmpvalues.Length.ToString())
        End Try
        'End If
    End Sub

    Private Function HasTriggers(x As Integer) As Boolean
        Dim b As Boolean = False
        If x > 0 Then
            b = True
        ElseIf Not x = 0 Then
            ErrorMsg(x, 2)
        End If
        Return b
    End Function

    Private Function StrToInt(s As String) As Integer
        Return Convert.ToInt32(s)
    End Function

    Private Sub ResetItem()
        If IsNothing(my_triggers) Then
            my_triggers = New List(Of String)
        Else
            my_triggers.Clear()
        End If
        If IsNothing(my_factions) Then
            my_factions = New List(Of Integer)
        Else
            my_factions.Clear()
        End If
        For index = 0 To 1
            my_names(index) = String.Empty
            my_special_values(index) = String.Empty
        Next
        my_special_values(2) = String.Empty
        If IsNothing(my_meshes) Then
            my_meshes = New List(Of String)
        Else
            my_meshes.Clear()
        End If
        Price = 0
        Weight = 0.000000
        For i = 0 To my_item_stats.Length - 1
            my_item_stats(i) = 0
        Next
    End Sub

    Private Shared Sub ErrorMsg(x As Integer, errorNumber As Integer)
        MsgBox("There was an error somewhere in the file! --> x = " + x.ToString() + " : 0x" + errorNumber.ToString())
    End Sub

    Private Shared Sub InitializeHeaderItemProperties()
        Dim itemProperties As New List(Of HeaderVariable)
        Dim masks As New List(Of Integer)
        Using sr As New StreamReader(SkillHunter.FilesPath + "header_items.py")
            Dim s As String
            Dim sp As String()
            While Not sr.EndOfStream
                s = sr.ReadLine().Split("#")(0)
                If s.Split("_")(0).Equals("itp") Then
                    sp = s.Replace(" ", String.Empty).Split("=")
                    If sp(1).Contains("0x") Then
                        itemProperties.Add(New HeaderVariable(sp(1).Substring(2), sp(0)))
                    End If
                End If
            End While
        End Using
        For i = 0 To itemProperties.Count - 1
            If itemProperties(i).VariableName.EndsWith("mask") Then
                masks.Add(i)
            End If
        Next
        masks.Reverse()
        For Each i As Integer In masks
            itemProperties.RemoveAt(i)
        Next
        m_header_itemProperties = itemProperties.ToArray()
    End Sub

    Private Shared Sub InitializeHeaderItemCapabilityFlags()
        Dim itemCapabilityFlags As New List(Of HeaderVariable)
        Dim masks As New List(Of Integer)
        Using sr As New StreamReader(SkillHunter.FilesPath + "header_items.py")
            Dim s As String
            Dim sp As String()
            While Not sr.EndOfStream
                s = sr.ReadLine().Split("#")(0)
                If s.Split("_")(0).Equals("itcf") Then
                    sp = s.Replace(" ", String.Empty).Split("=")
                    If sp(1).Contains("0x") Then
                        itemCapabilityFlags.Add(New HeaderVariable(sp(1).Substring(2), sp(0)))
                    End If
                End If
            End While
        End Using
        For i = 0 To itemCapabilityFlags.Count - 1
            If itemCapabilityFlags(i).VariableName.EndsWith("mask") Then
                masks.Add(i)
            End If
        Next
        masks.Reverse()
        For Each i As Integer In masks
            itemCapabilityFlags.RemoveAt(i)
        Next
        m_header_itemCapabilitiyFlags = itemCapabilityFlags.ToArray()
    End Sub

    Private Shared Sub InitializeHeaderIModBits()
        Dim listIModBits As New List(Of HeaderVariable)
        Using sr As New StreamReader(SkillHunter.FilesPath + "header_item_modifiers.py")
            Dim s As String
            Dim sp As String()
            While Not sr.EndOfStream
                s = sr.ReadLine().Split("#")(0)
                If s.Split("_")(0).Equals("imodbit") Then
                    sp = s.Replace(" ", String.Empty).Split("=")
                    listIModBits.Add(New HeaderVariable(SkillHunter.Dec2Hex_16CHARS(sp(1)), sp(0)))
                End If
            End While
        End Using
        m_header_imodbits = listIModBits.ToArray()
    End Sub

    Private Shared Sub InitializeHeaderIMods()
        Dim listIMods As New List(Of HeaderVariable)
        Using sr As New StreamReader(SkillHunter.FilesPath + "header_item_modifiers.py")
            Dim s As String
            Dim sp As String()
            While Not sr.EndOfStream
                s = sr.ReadLine().Split("#")(0)
                If s.Split("_")(0).Equals("imod") Then
                    sp = s.Replace(" ", String.Empty).Split("=")
                    listIMods.Add(New HeaderVariable(sp(1), sp(0)))
                End If
            End While
        End Using
        m_header_imods = listIMods.ToArray()
    End Sub

    Public Shared Function GetItemPropertiesFromValue(value As String) As String
        Dim retur As String = String.Empty, tmp As String = String.Empty

        If m_header_itemProperties.Length = 0 Then
            InitializeHeaderItemProperties()
        End If

        Dim bbbb As Boolean

        For i = 0 To m_header_itemProperties.Length - 1
            tmp = m_header_itemProperties(i).VariableValue.TrimStart("0")
            bbbb = False
            If value.Length >= tmp.Length Then
                'Try
                If tmp.Chars(0) = value.Chars(value.Length - tmp.Length) And tmp.Length > 2 Then
                    retur += "|" + m_header_itemProperties(i).VariableName
                Else
                    bbbb = True
                End If
                'Catch ex As Exception
                '   MessageBox.Show("Q.E.D." + Environment.NewLine + ex.ToString())
                'End Try

                If Not value.Chars(value.Length - tmp.Length) = "0"c And bbbb Then
                    Dim list As New List(Of HeaderVariable)
                    Dim x_tmp As Integer = SkillHunter.Hex2Dec(value.Chars(value.Length - tmp.Length)), x_counter As Integer = 0

                    If tmp.Length > 2 Then
                        For j = 9 To m_header_itemProperties.Length - 1
                            If m_header_itemProperties(j).VariableValue.TrimStart("0").Length = value.Substring(value.Length - tmp.Length).Length Then
                                list.Add(m_header_itemProperties(j))
                            End If
                        Next
                        'MsgBox("START")
                        list.Reverse()
                        For Each variable As HeaderVariable In list
                            'MsgBox(variable.VariableName + ":" + variable.VariableValue)
                            If x_counter < x_tmp Then
                                Dim xtert As Integer = SkillHunter.Hex2Dec(variable.VariableValue.Trim("0"))
                                If xtert <= x_tmp And xtert + x_counter <= x_tmp Then ' NOCHMAL ÜBERPRÜFEN
                                    x_counter += xtert
                                    'MsgBox(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert))
                                    If Not retur.Contains(variable.VariableName) Then
                                        retur += "|" + variable.VariableName
                                    End If
                                End If
                            End If
                        Next
                        'Else
                        '
                        'For j = 0 To 10
                        'CODE
                        'Next
                    End If
                    'i = m_header_itemProperties.Length
                End If
            End If
        Next
        'MsgBox(tmp + Environment.NewLine + value + " - " + value.Length.ToString() + " - " + (value.Length - 2).ToString())
        If value.Length > 2 Then
            tmp = value.Substring(value.Length - 2)
        End If
        tmp = tmp.TrimStart("0")
        For Each itype As HeaderVariable In m_header_itemProperties
            Dim tmp2 As String = itype.VariableValue.TrimStart("0").ToLower()
            If tmp2.Length < 3 Then
                If tmp.ToLower().Equals(tmp2) Then
                    retur += "|" + itype.VariableName
                End If
            End If
        Next
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

    Public Shared Function GetItemCapabilityFlagsFromValue(value As String, Optional itemID As String = "") As String  ' NOT WORKING FOR ALL ITEMS --> SEE "itm_great_sword"!!!  'IS THIS STILL UP TO DATE?
        Dim retur As String = String.Empty, tmp As String
        If m_header_itemCapabilitiyFlags.Length = 0 Then
            InitializeHeaderItemCapabilityFlags()
        End If
        'Using writer As New StreamWriter("varBug_Test.txt", True)
        'writer.Write(Environment.NewLine + "STARTING NEW ITEM NOW! --> Value = 0x" + value + Environment.NewLine)
        'If Not itemID.Equals(String.Empty) Then
        'writer.WriteLine(" --> ItemID: " + itemID + Environment.NewLine)
        'End If
        For i = 0 To m_header_itemCapabilitiyFlags.Length - 1
            tmp = m_header_itemCapabilitiyFlags(i).VariableValue.TrimStart("0")
            'If tmp.Chars(0) = value.Chars(value.Length - tmp.Length) And tmp.Trim("0").Length = 1 And Not tmp.Length = 9 And Not tmp.Length = 8 Then
            'retur += "|" + m_header_itemCapabilitiyFlags(i).VariableName
            'writer.WriteLine(tmp.Chars(0) + " - USED!")
            If Not value.Chars(value.Length - tmp.Length) = "0"c Then 'ElseIf Not value.Chars(value.Length - tmp.Length) = "0"c Then
                Dim list As New List(Of HeaderVariable)
                Dim x_tmp As Integer = SkillHunter.Hex2Dec(value.Chars(value.Length - tmp.Length)), x_counter As Integer = 0
                If tmp.Length = 9 Then
                    x_tmp = SkillHunter.Hex2Dec(value.Substring(value.Length - tmp.Length, 2))
                    'If x_tmp > 100 Then
                    'MsgBox("NUCLEAR! - " + x_tmp.ToString() + " --> " + value.Substring(value.Length - tmp.Length, 2) + " --> " + value)
                    'End If
                End If
                Dim varValue As String
                For j = 0 To m_header_itemCapabilitiyFlags.Length - 1
                    varValue = m_header_itemCapabilitiyFlags(j).VariableValue.TrimStart("0")
                    If varValue.Length = value.Substring(value.Length - tmp.Length).Length Then
                        list.Add(m_header_itemCapabilitiyFlags(j))
                    End If
                Next
                'MsgBox("START2 --> list.Count = " + list.Count.ToString() + "; Column = " + (value.Length - tmp.Length + 1).ToString())
                list.Reverse()
                For Each variable As HeaderVariable In list
                    'MsgBox(variable.VariableName + ":" + variable.VariableValue)
                    If x_counter < x_tmp Then
                        Dim varStart As String = variable.VariableValue.TrimStart("0")
                        'Dim b As Boolean = False, c As Boolean = False
                        'If varStart.Length = 9 And Not variable.VariableValue.Equals("0000000800000000") Then
                        'b = True
                        'End If
                        If varStart.Length = 9 And (varStart.TrimEnd("0").Equals("1") Or varStart.TrimEnd("0").Equals("8")) Then
                            varStart += "."
                        ElseIf varStart.Length = 8 Then
                            varStart = "." + varStart
                        End If
                        varStart = varStart.Replace("0", String.Empty)
                        If varStart.Contains(".") Then
                            varStart = varStart.Replace(".", "0")
                        End If
                        While varStart.Length < 8
                            varStart = "0" + varStart
                        End While
                        Dim xtert As Integer = SkillHunter.Hex2Dec(varStart)
                        varStart = varStart.TrimStart("0")
                        'If x_tmp > 100 Then
                        'MsgBox("OVER 9000! - " + varStart + "; " + Str(xtert) + "; " + x_tmp.ToString())
                        'c = True
                        'End If
                        Dim ttttt As Integer = xtert + x_counter
                        'If b Then
                        'MsgBox(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + "varStart = " + varStart + "; x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert) + "; ttttt = " + Str(ttttt))
                        'End If
                        If xtert <= x_tmp And ttttt <= x_tmp Then ' NOCHMAL ÜBERPRÜFEN
                            'If b Then
                            'MsgBox(Str(varStart.Length) + " - " + x_tmp)
                            'End If
                            If varStart.Length = 1 Then
                                x_counter += xtert
                                'MsgBox(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert) + "; ttttt = " + Str(ttttt))
                                If Not IsValueInValueString(retur, variable.VariableName) Then 'If Not retur.Contains(variable.VariableName) Then
                                    retur += "|" + variable.VariableName
                                    'writer.WriteLine(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert) + "; ttttt = " + Str(ttttt) + " - USED!")
                                End If
                            Else
                                'If c Then
                                'MsgBox(varStart + "; " + varStart.Substring(1, 1) + "; " + value.Chars(8))
                                'End If
                                'writer.WriteLine(varStart + "; " + varStart.Substring(1, 1) + "; " + value.Chars(8))
                                If varStart.Substring(1, 1).Equals(value.Chars(8)) And (varStart.Substring(0, 1).Equals(value.Chars(7)) Or SkillHunter.Hex2Dec(varStart.Substring(0, 1)) = (SkillHunter.Hex2Dec(value.Chars(7)) - 8)) Then
                                    x_counter += xtert
                                    'If c Then
                                    'MsgBox(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + varStart.Substring(1, 1) + ".Equals(""" + value.Chars(8) + """)" + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert) + "; ttttt = " + Str(ttttt))
                                    'End If
                                    'writer.Write((variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + varStart.Substring(1, 1) + ".Equals(""" + value.Chars(8) + """)" + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert) + "; ttttt = " + Str(ttttt)))
                                    If Not IsValueInValueString(retur, variable.VariableName) Then 'If Not retur.Contains(variable.VariableName) Then
                                        retur += "|" + variable.VariableName
                                        'writer.WriteLine(" - USED!")
                                        'Else
                                        'writer.WriteLine(" - NOT USED! (0x1)")
                                    End If
                                    'Else
                                    'writer.WriteLine(" - NOT USED! (0x2)")
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next
        If value.Length >= 8 Then
            If SkillHunter.Hex2Dec(value.Chars(7)) >= 8 Then 'Or Not value.Chars(7) = "0" Or Not value.Chars(7) = "1" Or Not value.Chars(7) = "2" Or Not value.Chars(7) = "3" Then
                Dim holsterdVar As String = String.Empty
                For bbb = 0 To m_header_itemCapabilitiyFlags.Length - 1
                    holsterdVar = m_header_itemCapabilitiyFlags(bbb).VariableValue.TrimStart("0")
                    If SkillHunter.Hex2Dec(holsterdVar.TrimEnd("0")) = 8 And holsterdVar.Length = 9 Then
                        holsterdVar = m_header_itemCapabilitiyFlags(bbb).VariableName
                        bbb = m_header_itemCapabilitiyFlags.Length
                    End If
                Next
                If Not holsterdVar.Equals(String.Empty) Then
                    retur += "|" + holsterdVar
                End If
            End If
        End If
        If Not retur.Equals(String.Empty) Then
            retur = retur.Substring(1)
        End If
        'writer.WriteLine(Environment.NewLine + "All Listed:")
        Dim tmpS As String() = SkillHunter.RemoveItemDoublesFromArray(retur.Split("|"))
        retur = String.Empty
        For i = 0 To tmpS.Length - 1
            retur += tmpS(i)
            'writer.WriteLine(" - " + tmpS(i))
            If i < tmpS.Length - 1 Then
                retur += "|"
            End If
        Next
        If retur.Equals(String.Empty) Then
            retur = "0"
        End If
        'writer.WriteLine(Environment.NewLine + "RESULT: " + retur)
        'End Using
        Return retur
    End Function

    Private Shared Function IsValueInValueString(valueString As String, value As String) As Boolean
        Dim yes As Boolean = False
        Dim sp As String() = valueString.Trim().Split("|")
        For Each s As String In sp
            If s.Equals(value) Then
                yes = True
            End If
        Next
        Return yes
    End Function

    Public Shared Function GetMeshKindFromValue(value As String) As String
        Dim retur As String = String.Empty
        If SkillHunter.Hex2Dec_16CHARS(value) = 0 Then
            retur = "0"
        Else
            If value.Chars(0) = "1"c Then
                retur = "ixmesh_inventory"
            ElseIf value.Chars(0) = "2"c Then
                retur = "ixmesh_flying_ammo"
            ElseIf value.Chars(0) = "3"c Then
                retur = "ixmesh_carry"
            End If
            If Not value.Substring(1).Equals(ZERO_15_CHARS) Then
                retur += "|"c + GetItemModifiers_IMODBITS(value, True)
                retur = retur.Trim("|")
            End If
        End If
        Return retur
    End Function

    Public Shared Function GetHeaderPropertyFlags() As HeaderVariable()
        Return m_header_itemProperties
    End Function

    Public Shared Function GetHeaderCapabilityFlags() As HeaderVariable()
        Return m_header_itemCapabilitiyFlags
    End Function

    Public Shared Function GetHeaderIMODBITS() As HeaderVariable()
        Return m_header_imodbits
    End Function

    Public Shared Function GetHeaderIMODS() As HeaderVariable()
        Return m_header_imods
    End Function

    Public Shared Function GetItemModifiers_IMODBITS(value As String, Optional imodbits As Boolean = False) As String
        Dim retur As String = String.Empty, tmp As String
        If m_header_imodbits.Length = 0 Then
            InitializeHeaderIModBits()
        End If
        For i = 0 To m_header_imodbits.Length - 1
            tmp = m_header_imodbits(i).VariableValue.TrimStart("0")
            If tmp.Chars(0) = value.Chars(value.Length - tmp.Length) Then
                retur += "|" + m_header_imodbits(i).VariableName
                If Not imodbits Then
                    i = m_header_imodbits.Length
                End If
            ElseIf Not value.Chars(value.Length - tmp.Length) = "0"c Then
                Dim list As New List(Of HeaderVariable)
                Dim x_tmp As Integer = SkillHunter.Hex2Dec(value.Chars(value.Length - tmp.Length)), x_counter As Integer = 0
                For j = 0 To m_header_imodbits.Length - 1
                    If m_header_imodbits(j).VariableValue.TrimStart("0").Length = value.Substring(value.Length - tmp.Length).Length Then
                        list.Add(m_header_imodbits(j))
                    End If
                Next
                'MsgBox("START")
                list.Reverse()
                For Each variable As HeaderVariable In list
                    'MsgBox(variable.VariableName + ":" + variable.VariableValue)
                    If x_counter < x_tmp Then
                        Dim xtert As Integer = SkillHunter.Hex2Dec(variable.VariableValue.Trim("0"))
                        If xtert <= x_tmp And xtert + x_counter <= x_tmp Then ' NOCHMAL ÜBERPRÜFEN
                            x_counter += xtert
                            'MsgBox(variable.VariableName + ":" + variable.VariableValue + Environment.NewLine + " x_tmp = " + Str(x_tmp) + "; x_counter = " + Str(x_counter) + "; xtert = " + Str(xtert))
                            If Not retur.Contains(variable.VariableName) Then
                                retur += "|" + variable.VariableName
                            End If
                        End If
                    End If
                Next
            End If
        Next
        If Not retur.Equals(String.Empty) Then
            retur = retur.Substring(1)
            Dim tmpS As String() = SkillHunter.RemoveItemDoublesFromArray(retur.Split("|"))
            retur = String.Empty
            For i = 0 To tmpS.Length - 1
                retur += tmpS(i)
                If i < tmpS.Length - 1 Then
                    retur += "|"
                End If
            Next
            If Not imodbits Then
                retur = "|" + retur
            End If
        Else
            retur = "imodbits_none"
        End If
        Return retur
    End Function

    Public Shared Function GetItemModifiers_IMODS(value As String) As String ' USE THIS METHOD FOR COMBINING OF MODBITS !!!
        Dim retur As String = String.Empty
        If m_header_imods.Length = 0 Then
            InitializeHeaderIMods()
        End If
        'For i = 0 To m_header_imods.Length - 1
        retur = value ' CODE
        'Next
        Return retur
    End Function

#Region "Properties"

    Public Shared ReadOnly Property ItemStatsNames As String() = {"weigth", "abundance", "head_armor", "body_armor", "leg_armor", "difficulty", "hit_points", "spd_rtng", "shoot_speed", "weapon_length",
                                                               "max_ammo", "thrust_damage", "swing_damage"}

    Public ReadOnly Property Triggers As List(Of String)
        Get
            Return my_triggers
        End Get
    End Property

    Public ReadOnly Property Factions As List(Of Integer)
        Get
            Return my_factions
        End Get
    End Property

    Public Property Name As String
        Set(value As String)
            my_names(0) = value
        End Set
        Get
            Return my_names(0)
        End Get
    End Property

    Public Property PluralName As String
        Set(value As String)
            my_names(1) = value
        End Set
        Get
            Return my_names(1)
        End Get
    End Property

    Public Property Price As Integer = 0

    Public Property Weight As Double = 0.000000

    Public Property Abundance As Integer
        Set(value As Integer)
            my_item_stats(0) = value
        End Set
        Get
            Return my_item_stats(0)
        End Get
    End Property

    Public Property HeadArmor As Integer
        Set(value As Integer)
            my_item_stats(1) = value
        End Set
        Get
            Return my_item_stats(1)
        End Get
    End Property

    Public Property BodyArmor As Integer
        Set(value As Integer)
            my_item_stats(2) = value
        End Set
        Get
            Return my_item_stats(2)
        End Get
    End Property

    Public Property LegArmor As Integer
        Set(value As Integer)
            my_item_stats(3) = value
        End Set
        Get
            Return my_item_stats(3)
        End Get
    End Property

    Public Property Difficulty As Integer
        Set(value As Integer)
            my_item_stats(4) = value
        End Set
        Get
            Return my_item_stats(4)
        End Get
    End Property

    Public Property HitPoints As Integer
        Set(value As Integer)
            my_item_stats(5) = value
        End Set
        Get
            Return my_item_stats(5)
        End Get
    End Property

    Public Property SpeedRating As Integer
        Set(value As Integer)
            my_item_stats(6) = value
        End Set
        Get
            Return my_item_stats(6)
        End Get
    End Property

    Public Property MissileSpeed As Integer
        Set(value As Integer)
            my_item_stats(7) = value
        End Set
        Get
            Return my_item_stats(7)
        End Get
    End Property

    Public Property WeaponLength As Integer
        Set(value As Integer)
            my_item_stats(8) = value
        End Set
        Get
            Return my_item_stats(8)
        End Get
    End Property

    Public Property MaxAmmo As Integer
        Set(value As Integer)
            my_item_stats(9) = value
        End Set
        Get
            Return my_item_stats(9)
        End Get
    End Property

    Public Property ThrustDamage As Integer
        Set(value As Integer)
            my_item_stats(10) = value
        End Set
        Get
            Return my_item_stats(10)
        End Get
    End Property

    Public Property SwingDamage As Integer
        Set(value As Integer)
            my_item_stats(11) = value
        End Set
        Get
            Return my_item_stats(11)
        End Get
    End Property

    Public ReadOnly Property ItemStats As Integer()
        Get
            Return my_item_stats
        End Get
    End Property

    Public ReadOnly Property Meshes As List(Of String)
        Get
            Return my_meshes
        End Get
    End Property

    Public Sub SetMeshes(list As List(Of String))
        my_meshes = list
    End Sub

    Public ReadOnly Property SpecialValues As String()
        Get
            Return my_special_values
        End Get
    End Property

    Public Sub SetSpecialValues(list As String())
        my_special_values = list
    End Sub

    Public Sub SetItemStats_12(list As Integer())
        my_item_stats = list
    End Sub

    Public Sub SetFactions(list As List(Of Integer))
        my_factions = list
    End Sub

    Public Sub SetTriggers(list As List(Of String))
        my_triggers = list
    End Sub

#End Region

End Class
