Public Class HeaderVariable

    Private value As String
    Private name As String

    Public Sub New(value As String, name As String)
        Me.value = value
        Me.name = name
    End Sub

    Public ReadOnly Property VariableName As String
        Get
            Return name
        End Get
    End Property

    Public ReadOnly Property VariableValue As String
        Get
            Return value
        End Get
    End Property

End Class
