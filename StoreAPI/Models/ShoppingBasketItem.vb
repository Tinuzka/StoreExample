Public Class ShoppingBasketItem

    Public Property ID As Integer
    Public Property Product As Product
    Public Property Amount As Integer

    Public Sub New()

    End Sub

    Public Sub New(product As Product, amount As Integer)
        Me.Product = product
        Me.Amount = amount
    End Sub

End Class
