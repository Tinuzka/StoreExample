Imports StoreAPI
Public Class TestUtils

    Public Shared Function CreateProduct(name As String, price As Decimal, amount As Integer, id As Integer) As Product
        Dim product As New Product
        product.Name = name
        product.Price = price
        product.Amount = amount
        product.ID = id
        Return product
    End Function

    Public Shared Function CreateShoppingBasketItem(product As Product, amount As Integer, id As Integer) As ShoppingBasketItem
        Dim item As New ShoppingBasketItem(product, amount)
        item.ID = id
        Return item
    End Function

End Class
