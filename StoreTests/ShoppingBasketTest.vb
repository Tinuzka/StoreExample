Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StoreAPI

<TestClass()> Public Class ShoppingBasketTest

    <TestMethod()> Public Sub GetTotalNoItemsTest()
        Dim testBasket As New ShoppingBasket
        Dim total = testBasket.GetTotal
        Assert.AreEqual(CDec(0), total)
    End Sub

    <TestMethod()> Public Sub GetTotalSeveralItemsTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim product2 = TestUtils.CreateProduct("Test", 50, 12, 2)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 2)
        testBasket.AddItem(item2)

        Dim total = testBasket.GetTotal
        Assert.AreEqual(CDec(50 + 10 + 10), total)
    End Sub

#Region "GetItemByProductId"

    <TestMethod()> Public Sub GetItemByProductIdNoItemsTest()
        Dim testBasket As New ShoppingBasket
        Dim result = testBasket.GetItemByProductId(1)
        Assert.AreEqual(Nothing, result)
    End Sub

    <TestMethod()> Public Sub GetItemByProductIdNotExistsTest()
        Dim testBasket As New ShoppingBasket
        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim result = testBasket.GetItemByProductId(10)
        Assert.AreEqual(Nothing, result)
    End Sub

    <TestMethod()> Public Sub GetItemByProductIdSeveralItemsTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim item2 = TestUtils.CreateShoppingBasketItem(product1, 1, 2)
        testBasket.AddItem(item2)

        Dim result = testBasket.GetItemByProductId(product1.ID)
        Assert.AreEqual(item1, result)
    End Sub

    <TestMethod()> Public Sub GetItemByProductIdOneResultTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim product2 = TestUtils.CreateProduct("Test2", 5, 5, 5)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 2)
        testBasket.AddItem(item2)

        Dim result = testBasket.GetItemByProductId(product2.ID)
        Assert.AreEqual(item2, result)

    End Sub

#End Region



#Region "GetItemByItemId"

    <TestMethod()> Public Sub GetItemByItemIdNoItemsTest()
        Dim testBasket As New ShoppingBasket
        Dim result = testBasket.GetItemByItemId(1)
        Assert.AreEqual(Nothing, result)
    End Sub

    <TestMethod()> Public Sub GetItemByItemdNotExistsTest()
        Dim testBasket As New ShoppingBasket
        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim result = testBasket.GetItemByItemId(10)
        Assert.AreEqual(Nothing, result)
    End Sub

    <TestMethod()> Public Sub GetItemByItemIdSeveralItemsTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)


        Dim product2 = TestUtils.CreateProduct("Test2", 5, 5, 5)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 1)
        testBasket.AddItem(item2)

        Dim result = testBasket.GetItemByProductId(1)
        Assert.AreEqual(item1, result)
    End Sub

    <TestMethod()> Public Sub GetItemByItemIdOneResultTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim product2 = TestUtils.CreateProduct("Test2", 5, 5, 5)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 2)
        testBasket.AddItem(item2)

        Dim result = testBasket.GetItemByProductId(product2.ID)
        Assert.AreEqual(item2, result)

    End Sub


#End Region

#Region "Delete item"

    <TestMethod()> Public Sub RemoveItemNoItemsTest()
        Dim testBasket As New ShoppingBasket
        testBasket.Remove(1)
        Assert.AreEqual(0, testBasket.GetAllItems.Count)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(DatabaseObjectNotFoundException))>
    Public Sub RemoveItemNotExistsTest()
        Dim testBasket As New ShoppingBasket
        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        testBasket.Remove(10)

    End Sub

    <TestMethod()> Public Sub RemoveItemSeveralItemsTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)


        Dim product2 = TestUtils.CreateProduct("Test2", 5, 5, 5)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 1)
        testBasket.AddItem(item2)

        testBasket.Remove(1)
        Assert.AreEqual(1, testBasket.GetAllItems.Count)
    End Sub

    <TestMethod()> Public Sub RemoveItemOneResultTest()
        Dim testBasket As New ShoppingBasket

        Dim product1 = TestUtils.CreateProduct("Test", 10, 12, 1)
        Dim item1 = TestUtils.CreateShoppingBasketItem(product1, 2, 1)
        testBasket.AddItem(item1)

        Dim product2 = TestUtils.CreateProduct("Test2", 5, 5, 5)
        Dim item2 = TestUtils.CreateShoppingBasketItem(product2, 1, 2)
        testBasket.AddItem(item2)

        testBasket.Remove(item2.ID)
        Assert.AreEqual(1, testBasket.GetAllItems.Count)
        Assert.IsNotNull(testBasket.GetItemByItemId(1))

    End Sub

#End Region

End Class
