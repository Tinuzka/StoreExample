''' <summary>
''' Searched product's amount was too small to make purchase
''' </summary>
Public Class OutOfStockException
    Inherits Exception
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub
End Class
