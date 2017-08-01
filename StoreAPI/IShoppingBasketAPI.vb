Public Interface IShoppingBasketAPI

    Function AddShoppingBasket() As Integer

    Sub AddProduct(productId As Integer, shoppingBasketId As Integer)

    Sub DeleteProduct(productId As Integer, shoppingBasketId As Integer)

    Sub EditShoppingBasketItem(editedItem As ShoppingBasketItem, shoppingBasketId As Integer)

    Function GetShoppingBasketById(shoppingBasketId As Integer) As ShoppingBasket

End Interface
