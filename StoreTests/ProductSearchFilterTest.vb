Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StoreAPI
Imports PagedList

<TestClass()> Public Class ProductSearchFilterTest

#Region "Search criterias"

    <TestMethod()> Public Sub FilterByString()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, "2", Nothing, Nothing, Nothing, Nothing, Nothing)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(product2, result.FirstOrDefault)

    End Sub

    <TestMethod()> Public Sub FilterByMinAmount()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, "15", Nothing, Nothing, Nothing, Nothing)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(product2, result.FirstOrDefault)

    End Sub

    <TestMethod()> Public Sub FilterByMaxAmount()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, "15", Nothing, Nothing, Nothing)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(product1, result.FirstOrDefault)

    End Sub

    <TestMethod()> Public Sub SortByNameAscending()
        Dim productList As New List(Of Product)

        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, ProductSortOrder.NameAsc.ToString, Nothing, Nothing)
        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(product1, result.FirstOrDefault)

    End Sub

    <TestMethod()> Public Sub SortByNameDesc()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, ProductSortOrder.NameDesc.ToString, Nothing, Nothing)
        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(product2, result.FirstOrDefault)

    End Sub

    <TestMethod()> Public Sub SortByPriceAscending()
        Dim productList As New List(Of Product)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, ProductSortOrder.PriceAsc.ToString, Nothing, Nothing)
        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(product1, result.FirstOrDefault)
    End Sub

    <TestMethod()> Public Sub SortByPriceDesc()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, ProductSortOrder.PriceDesc.ToString, Nothing, Nothing)
        Assert.AreEqual(2, result.Count)
        Assert.AreEqual(product2, result.FirstOrDefault)
    End Sub

    <TestMethod()> Public Sub GetFirstPage()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, Nothing, 1, 1)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(product1, result.FirstOrDefault)
        Assert.AreEqual(1, result.PageNumber)
        Assert.AreEqual(2, result.PageCount)
    End Sub

    <TestMethod()> Public Sub GetSecondPage()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(productList, Nothing, Nothing, Nothing, Nothing, 1, 2)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(product2, result.FirstOrDefault)
        Assert.AreEqual(2, result.PageNumber)
        Assert.AreEqual(2, result.PageCount)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub SearchNoList()

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.FilterAndPageProducts(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub

#End Region

#Region "Grouping"

    <TestMethod()>
    Public Sub GroupOneCriteria()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product3 = TestUtils.CreateProduct("Product3", 30, 35, 3)
        productList.Add(product3)

        Dim groupingConditions As New List(Of GroupingCondition)
        Dim condition1 = New GroupingCondition(15, 25)
        groupingConditions.Add(condition1)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, groupingConditions)
        Assert.AreEqual(1, result.Keys.Count)
        Assert.IsTrue(result.ContainsKey(condition1))
        Assert.AreEqual(product2, result(condition1).First)
        Assert.AreEqual(1, result(condition1).Count)
    End Sub

    <TestMethod()>
    Public Sub GroupMultipleCriteriaNoUpperLimitIncluded()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product3 = TestUtils.CreateProduct("Product3", 30, 35, 3)
        productList.Add(product3)
        Dim product4 = TestUtils.CreateProduct("Product4", 5, 35, 4)
        productList.Add(product4)

        Dim groupingConditions As New List(Of GroupingCondition)
        Dim condition1 = New GroupingCondition(10, 20)
        groupingConditions.Add(condition1)
        Dim condition2 = New GroupingCondition(20, Nothing)
        groupingConditions.Add(condition2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, groupingConditions)
        Assert.AreEqual(2, result.Keys.Count)
        Assert.AreEqual(2, result(condition1).Count)
        Assert.AreEqual(2, result(condition2).Count)
        Assert.IsTrue(result(condition1).Contains(product1) And result(condition1).Contains(product2))
        Dim list = result(condition2)
        Assert.IsTrue(list.Contains(product2) And list.Contains(product3))
    End Sub

    <TestMethod()>
    Public Sub GroupMultipleCriteriaNoLowerLimitNotIncluded()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product3 = TestUtils.CreateProduct("Product3", 30, 35, 3)
        productList.Add(product3)
        Dim product4 = TestUtils.CreateProduct("Product4", 5, 35, 4)
        productList.Add(product4)

        Dim groupingConditions As New List(Of GroupingCondition)
        Dim condition1 = New GroupingCondition(10, 30, False, False)
        groupingConditions.Add(condition1)
        Dim condition2 = New GroupingCondition(Nothing, 10, False, False)
        groupingConditions.Add(condition2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, groupingConditions)
        Assert.AreEqual(2, result.Keys.Count)
        Assert.AreEqual(1, result(condition1).Count)
        Assert.AreEqual(1, result(condition2).Count)
        Assert.AreEqual(product4, result(condition2).First)
        Assert.AreEqual(product2, result(condition1).First)
    End Sub

    <TestMethod()>
    Public Sub GroupMultipleCriteriaNoLowerNoUpperLimitIncludedAndNotIncluede()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product3 = TestUtils.CreateProduct("Product3", 30, 35, 3)
        productList.Add(product3)
        Dim product4 = TestUtils.CreateProduct("Product4", 5, 35, 4)
        productList.Add(product4)

        Dim groupingConditions As New List(Of GroupingCondition)
        Dim condition1 = New GroupingCondition(Nothing, 10, False, False)
        groupingConditions.Add(condition1)
        Dim condition2 = New GroupingCondition(10, 20, True, True)
        groupingConditions.Add(condition2)
        Dim condition3 = New GroupingCondition(20, Nothing, False, False)
        groupingConditions.Add(condition3)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, groupingConditions)
        Assert.AreEqual(3, result.Keys.Count)
        Assert.AreEqual(1, result(condition1).Count)
        Assert.AreEqual(2, result(condition2).Count)
        Assert.AreEqual(1, result(condition3).Count)
        Assert.AreEqual(product4, result(condition1).First)
        Assert.AreEqual(product3, result(condition3).First)
        Dim list = result(condition2)
        Assert.IsTrue(list.Contains(product1) And list.Contains(product2))
    End Sub

    <TestMethod()>
    Public Sub GroupMultipleCriteriaNoMatchingCondition()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)
        Dim product3 = TestUtils.CreateProduct("Product3", 30, 35, 3)
        productList.Add(product3)
        Dim product4 = TestUtils.CreateProduct("Product4", 5, 35, 4)
        productList.Add(product4)

        Dim groupingConditions As New List(Of GroupingCondition)
        Dim condition1 = New GroupingCondition(10, 20, False, False)
        groupingConditions.Add(condition1)
        Dim condition2 = New GroupingCondition(30, Nothing, False, False)
        groupingConditions.Add(condition2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, groupingConditions)
        Assert.AreEqual(2, result.Keys.Count)
        Assert.AreEqual(0, result(condition1).Count)
        Assert.AreEqual(0, result(condition2).Count)
    End Sub

    <TestMethod()>
    Public Sub GroupEmptyCriteria()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, New List(Of GroupingCondition))
        Assert.AreEqual(0, result.Keys.Count)
    End Sub

    <TestMethod()>
    Public Sub GroupNoCriteria()
        Dim productList As New List(Of Product)
        Dim product1 = TestUtils.CreateProduct("Product1", 10, 15, 1)
        productList.Add(product1)
        Dim product2 = TestUtils.CreateProduct("Product2", 20, 25, 2)
        productList.Add(product2)

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(productList, Nothing)
        Assert.AreEqual(0, result.Keys.Count)
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentNullException))>
    Public Sub GroupNoList()

        Dim productSearchFilter As New ProductSearchFilter
        Dim result = productSearchFilter.GetGroupedProducts(Nothing, New List(Of GroupingCondition))

    End Sub

#End Region

End Class
