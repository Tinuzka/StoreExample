Public Class ShoppingBasketOperations

    Public Function AddProductToBasket(ByRef foundBasket As ShoppingBasket, ByRef selectedProduct As Product) As ShoppingBasketItem

        If foundBasket Is Nothing Then Throw New ArgumentNullException("Shopping basket is not defined")

        If selectedProduct Is Nothing Then Throw New ArgumentNullException("Product is not defined")

        If selectedProduct.Amount = 0 Then
            'Can not add the product into basket if there is not any in the stock
            Throw New OutOfStockException("No products in stock")
        End If

        Dim basketItem = foundBasket.GetItemByProductId(selectedProduct.ID)

        If basketItem IsNot Nothing Then
            basketItem.Amount += 1
        Else
            basketItem = New ShoppingBasketItem(selectedProduct, 1)
            foundBasket.AddItem(basketItem)
        End If

        'update stock
        selectedProduct.Amount -= 1
        Return basketItem

    End Function

    Public Sub DeleteProductFromBasket(ByRef shoppingBasket As ShoppingBasket, productId As Integer)

        If shoppingBasket Is Nothing Then Throw New ArgumentNullException("Shopping basket is not defined")

        Dim itemInBasket = shoppingBasket.GetItemByProductId(productId)

        If itemInBasket Is Nothing Then
            Throw New DatabaseObjectNotFoundException("Product was not in the shopping basket")
        End If

        If itemInBasket.Amount > 1 Then
            itemInBasket.Amount -= 1
        Else
            shoppingBasket.Remove(itemInBasket.ID)
        End If

        'Update database stock value
        itemInBasket.Product.Amount += 1

    End Sub

    Public Sub EditProductInBasket(ByRef shoppingBasket As ShoppingBasket, ByRef editedBasketItem As ShoppingBasketItem)

        If shoppingBasket Is Nothing Then Throw New ArgumentNullException("Shopping basket is not defined")

        If editedBasketItem Is Nothing Then Throw New ArgumentNullException("Shopping basket item is not defined")

        Dim oldBasketItem = shoppingBasket.GetItemByItemId(editedBasketItem.ID)

        If oldBasketItem Is Nothing Then
            Throw New DatabaseObjectNotFoundException("Product was not in the shopping basket")
        End If

        Dim amountDifference = oldBasketItem.Amount - editedBasketItem.Amount

        If oldBasketItem.Product.Amount + amountDifference > 0 Then
            oldBasketItem.Product.Amount += amountDifference
            oldBasketItem.Amount = editedBasketItem.Amount
        Else
            Dim productsLeft = oldBasketItem.Product.Amount + oldBasketItem.Amount
            Throw New OutOfStockException("Purchasing " & editedBasketItem.Amount & " products failed. Only " & productsLeft & " left to buy")
        End If

    End Sub


End Class
