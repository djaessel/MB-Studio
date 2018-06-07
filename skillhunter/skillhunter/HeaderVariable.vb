Public Class HeaderVariable

    Public Sub New(value As String, name As String)
        VariableValue = value
        VariableName = name
    End Sub

    Public ReadOnly Property VariableName As String

    Public ReadOnly Property VariableValue As String

End Class
