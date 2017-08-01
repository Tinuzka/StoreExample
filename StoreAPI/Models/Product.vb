Public Class Product

    Public Property ID As Integer
    Public Property Name As String
    Public Property Price As Decimal
    Public Property Amount As Integer

    Public Sub CopyValues(newValues As Product)
        Name = newValues.Name
        Price = newValues.Price
        Amount = newValues.Amount
    End Sub

End Class

