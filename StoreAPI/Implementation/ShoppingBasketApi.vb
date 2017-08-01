

Public Class ShoppingBasketApi
    Implements IShoppingBasketAPI

    Public Function AddShoppingBasket() As Integer Implements IShoppingBasketAPI.AddShoppingBasket

        Using db As New StoreModel
            Dim newBasket As New ShoppingBasket()
            db.ShoppingBaskets.Add(newBasket)
            db.SaveChanges()
            Return newBasket.ID
        End Using

    End Function

    Public Sub AddProduct(productId As Integer, shoppingBasketId As Integer) Implements IShoppingBasketAPI.AddProduct

        Using db As New StoreModel
            Dim foundBasket = GetShoppingBasketById(shoppingBasketId)
            db.ShoppingBaskets.Attach(foundBasket)
            Dim selectedProduct = db.Products.Find(productId)
            If selectedProduct Is Nothing Then Throw New DatabaseObjectNotFoundException("Product does not exist")

            Dim shoppingBasketAdder As New ShoppingBasketOperations
            shoppingBasketAdder.AddProductToBasket(foundBasket, selectedProduct)
            db.SaveChanges()
            Dim d = foundBasket
        End Using

    End Sub

    Public Sub DeleteProduct(productId As Integer, shoppingBasketId As Integer) Implements IShoppingBasketAPI.DeleteProduct

        Using db As New StoreModel
            Dim currentBasket = GetShoppingBasketById(shoppingBasketId)
            db.ShoppingBaskets.Attach(currentBasket)

            Dim shoppingBasketDeleter As New ShoppingBasketOperations
            shoppingBasketDeleter.DeleteProductFromBasket(currentBasket, productId)

            db.SaveChanges()
        End Using

    End Sub

    Public Sub EditShoppingBasketItem(editedItem As ShoppingBasketItem, shoppingBasketId As Integer) Implements IShoppingBasketAPI.EditShoppingBasketItem
        Using db As New StoreModel
            Dim foundBasket = GetShoppingBasketById(shoppingBasketId)
            db.ShoppingBaskets.Attach(foundBasket)
            Dim shoppingBasketEditor As New ShoppingBasketOperations
            shoppingBasketEditor.EditProductInBasket(foundBasket, editedItem)

            db.SaveChanges()
        End Using
    End Sub

    Public Function GetShoppingBasketById(shoppingBasketId As Integer) As ShoppingBasket Implements IShoppingBasketAPI.GetShoppingBasketById
        Using db As New StoreModel
            Dim foundBasket = (From b In db.ShoppingBaskets.Include("basketItems.Product") Where b.ID = shoppingBasketId).FirstOrDefault
            If foundBasket Is Nothing Then Throw New DatabaseObjectNotFoundException("Shopping basket does not exist")
            Return foundBasket
        End Using
    End Function

End Class
