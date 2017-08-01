''' <summary>
''' Object with searched id did not exist in the database
''' </summary>
Public Class DatabaseObjectNotFoundException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class
