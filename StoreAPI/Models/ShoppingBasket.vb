

Public Class ShoppingBasket

    Public Property ID As Integer
    Public Property basketItems As List(Of ShoppingBasketItem)

    Public Function GetTotal() As Decimal
        If basketItems Is Nothing Then Return 0.0
        Return basketItems.Sum(Function(item) item.Product.Price * item.Amount)
    End Function

    Public Function GetItemByProductId(productId As Integer) As ShoppingBasketItem
        If basketItems Is Nothing Then Return Nothing
        Return basketItems.FirstOrDefault(Function(item) item.Product.ID = productId)
    End Function

    Public Function GetItemByItemId(itemId As Integer) As ShoppingBasketItem
        If basketItems Is Nothing Then Return Nothing
        Return basketItems.FirstOrDefault(Function(item) item.ID = itemId)
    End Function

    Public Sub AddItem(itemToAdd As ShoppingBasketItem)
        If basketItems Is Nothing Then
            basketItems = New List(Of ShoppingBasketItem)
        End If
        basketItems.Add(itemToAdd)
    End Sub

    Public Sub Remove(itemId As Integer)
        If basketItems Is Nothing Then Return
        Dim foundItem = basketItems.FirstOrDefault(Function(item) item.ID = itemId)
        If foundItem IsNot Nothing Then
            basketItems.Remove(foundItem)
        Else
            Throw New DatabaseObjectNotFoundException("Product was not found from shopping basket")
        End If

    End Sub

    Public Function GetAllItems() As List(Of ShoppingBasketItem)
        If basketItems Is Nothing Then Return New List(Of ShoppingBasketItem)
        Return basketItems
    End Function

End Class
