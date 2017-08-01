Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StoreAPI

<TestClass()> Public Class ShoppingBasketOperationsTest

#Region "Adding product"
    <TestMethod()> Public Sub TestAddNewProductEmptyBasket()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        Dim testProduct = CreateProduct("Test", 100, 100, 1)

        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct)

        Assert.AreEqual(1, testBasket.GetAllItems.Count)
        Assert.AreEqual(testProduct, testBasket.GetItemByProductId(1).Product)
        Assert.AreEqual(99, testProduct.Amount)
    End Sub

    <TestMethod()> Public Sub TestAddTwoDifferentProducts()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket

        Dim testProduct = CreateProduct("Test", 100, 100, 1)
        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct)

        Dim testProduct2 = CreateProduct("Test2", 100, 100, 2)
        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct2)

        Assert.AreEqual(2, testBasket.GetAllItems.Count)
        Assert.AreEqual(1, testBasket.GetItemByProductId(1).Amount)
        Assert.AreEqual(99, testProduct.Amount)
        Assert.AreEqual(1, testBasket.GetItemByProductId(2).Amount)
        Assert.AreEqual(99, testProduct.Amount)
    End Sub

    <TestMethod()> Public Sub TestAddSameProductTwice()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        Dim testProduct = CreateProduct("Test", 100, 100, 1)

        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct)
        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct)

        Assert.AreEqual(1, testBasket.GetAllItems.Count)
        Assert.AreEqual(2, testBasket.GetItemByProductId(1).Amount)
        Assert.AreEqual(98, testProduct.Amount)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(OutOfStockException))>
    Public Sub TestAddNoProductInStock()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        Dim testProduct = CreateProduct("Test", 100, 0, 1)
        shoppingBasketOperations.AddProductToBasket(testBasket, testProduct)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub TestAddShoppingBasketNothing()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testProduct = CreateProduct("Test", 100, 100, 1)
        shoppingBasketOperations.AddProductToBasket(Nothing, testProduct)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub TestAddProductNothing()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        shoppingBasketOperations.AddProductToBasket(testBasket, Nothing)
    End Sub
#End Region

#Region "Delete product"

    <TestMethod()> Public Sub TestDeleteProductSeveralLeft()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket

        Dim testProduct = CreateProduct("Test", 100, 100, 2)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem.ID = 1
        testBasket.AddItem(shoppingBasketItem)

        Dim testProduct2 = CreateProduct("Test2", 100, 100, 2)
        Dim shoppingBasketItem2 = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem2.ID = 5
        testBasket.AddItem(shoppingBasketItem2)

        shoppingBasketOperations.DeleteProductFromBasket(testBasket, testProduct.ID)

        Assert.AreEqual(2, testBasket.GetAllItems.Count)
        Assert.AreEqual(4, testBasket.GetItemByItemId(1).Amount)
        Assert.AreEqual(5, testBasket.GetItemByItemId(5).Amount)
        Assert.AreEqual(101, testProduct.Amount)
    End Sub

    <TestMethod()> Public Sub TestDeleteOneLeft()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket

        Dim testProduct = CreateProduct("Test", 100, 100, 1)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 1)
        shoppingBasketItem.ID = 2
        testBasket.AddItem(shoppingBasketItem)

        shoppingBasketOperations.DeleteProductFromBasket(testBasket, testProduct.ID)

        Assert.AreEqual(0, testBasket.GetAllItems.Count)
        Assert.AreEqual(101, testProduct.Amount)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub TestDeleteShoppingBasketNothing()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        shoppingBasketOperations.DeleteProductFromBasket(Nothing, 1)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(DatabaseObjectNotFoundException))>
    Public Sub TestDeleteProductNotFound()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        shoppingBasketOperations.DeleteProductFromBasket(testBasket, 1)
    End Sub

#End Region

#Region "Edit shopping basket item"

    <TestMethod()> Public Sub TestEditItem()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        Dim testProduct = CreateProduct("Test", 100, 100, 10)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem.ID = 5
        testBasket.AddItem(shoppingBasketItem)

        Dim editedItem = New ShoppingBasketItem(testProduct, 10)
        editedItem.ID = 5

        Dim otherProduct = CreateProduct("Test123", 100, 100, 1)
        Dim otherItem = New ShoppingBasketItem(otherProduct, 7)
        otherItem.ID = 101
        testBasket.AddItem(otherItem)

        shoppingBasketOperations.EditProductInBasket(testBasket, editedItem)
        Assert.AreEqual(2, testBasket.GetAllItems.Count)
        Assert.AreEqual(10, testBasket.GetItemByProductId(testProduct.ID).Amount)
        Assert.AreEqual(95, testProduct.Amount)
        Assert.AreEqual(7, testBasket.GetItemByProductId(otherProduct.ID).Amount)
        Assert.AreEqual(100, otherProduct.Amount)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(OutOfStockException))>
    Public Sub TestEditNoProductInStockEnough()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket

        Dim testProduct = CreateProduct("Test", 100, 1, 1)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem.ID = 5
        testBasket.AddItem(shoppingBasketItem)

        Dim editedItem = New ShoppingBasketItem(testProduct, 10)
        editedItem.ID = 5

        shoppingBasketOperations.EditProductInBasket(testBasket, editedItem)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(DatabaseObjectNotFoundException))>
    Public Sub TestEditItemNotFound()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket

        Dim testProduct = CreateProduct("Test", 100, 100, 2)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem.ID = 1
        testBasket.AddItem(shoppingBasketItem)

        'Not added to the basket
        Dim notInBasketProduct = CreateProduct("Test2", 100, 100, 2)
        Dim notInBasketItem = New ShoppingBasketItem(notInBasketProduct, 5)
        notInBasketItem.ID = 5

        shoppingBasketOperations.EditProductInBasket(testBasket, notInBasketItem)

    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub TestEditShoppingBasketNothing()
        Dim shoppingBasketOperations As New ShoppingBasketOperations

        Dim testProduct = CreateProduct("Test", 100, 0, 1)
        Dim shoppingBasketItem = New ShoppingBasketItem(testProduct, 5)
        shoppingBasketItem.ID = 2

        shoppingBasketOperations.EditProductInBasket(Nothing, shoppingBasketItem)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub TestEditShoppingBasketItemNothing()
        Dim shoppingBasketOperations As New ShoppingBasketOperations
        Dim testBasket As New ShoppingBasket
        shoppingBasketOperations.EditProductInBasket(testBasket, Nothing)
    End Sub
#End Region

    Private Function CreateProduct(name As String, price As Decimal, amount As Integer, id As Integer) As Product
        Dim product As New Product
        product.Name = name
        product.Price = price
        product.Amount = amount
        product.ID = id
        Return product
    End Function

End Class