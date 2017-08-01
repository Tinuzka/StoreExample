Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports Store
Imports PagedList
Imports StoreAPI

Namespace Controllers
    Public Class CatalogController
        Inherits System.Web.Mvc.Controller
        Private ReadOnly controllerName = "Catalog"
        Private currentPage As Integer?

        Dim shoppingBasketAPI As New ShoppingBasketApi

        Function Index(searchString As String, minPrice As String, maxPrice As String, sortOrder As String, rowsPerPage As Integer?, page As Integer?) As ActionResult

            SetSearchParameters(searchString, minPrice, maxPrice, sortOrder)
            SetPaging(rowsPerPage, page)
            SetSorting(sortOrder)

            Dim productService As New ProductAPI()
            Dim selectedProducts = productService.GetProducts(searchString, minPrice, maxPrice, sortOrder, rowsPerPage, page)

            Return View(selectedProducts)
        End Function

        Private Sub SetSearchParameters(ByRef searchString As String, ByRef minPrice As String, ByRef maxPrice As String, ByRef sortOrder As String)
            SetUsedValue(searchString, "Search")
            SetUsedValue(minPrice, "MinPrice")
            SetUsedValue(maxPrice, "MaxPrice")
            SetUsedValue(sortOrder, "Sort")
        End Sub

        Private Sub SetPaging(ByRef rowsPerPage As Integer?, ByRef page As Integer?)
            If currentPage IsNot Nothing Then
                'SetUsedValues set the currentPage to 1 to start from first page after 
                'search parameter changes
                page = currentPage
            End If

            SetUsedValue(page, "Page", 1)
            SetUsedValue(rowsPerPage, "RowAmount", 3)
        End Sub

        Private Sub SetSorting(sortOrder As String)
            'Set viewpag so that each column knows the direction it goes if the sort is changed
            ViewBag.NameSortParm = If(sortOrder = ProductSortOrder.NameAsc.ToString Or String.IsNullOrEmpty(sortOrder), ProductSortOrder.NameDesc, ProductSortOrder.NameAsc)
            ViewBag.PriceSortParm = If(sortOrder = ProductSortOrder.PriceAsc.ToString, ProductSortOrder.PriceDesc, ProductSortOrder.PriceAsc)
        End Sub

        ''' <summary>
        ''' Set value, which will be used into given object and to Session, that it is remember even if page is changed
        ''' </summary>
        ''' <param name="value">Value or Nothing</param>
        ''' <param name="parameterName">name which will be used for session key</param>
        Private Sub SetUsedValue(ByRef value As Object, parameterName As String)
            If value IsNot Nothing Then
                ' When new search value is given, start again from page 1
                currentPage = 1
                System.Web.HttpContext.Current.Session(controllerName + parameterName) = value
            Else
                value = System.Web.HttpContext.Current.Session(controllerName + parameterName)
            End If

        End Sub

        ''' <summary>
        ''' Set value, which will be used, into given object and to Session, that it is remember even if page is changed. If value is not found use given default.
        ''' </summary>
        ''' <param name="value">Value or Nothing</param>
        ''' <param name="parameterName">name which will be used for session key</param>
        ''' <param name="defaultValue">Value is used, if value and session does not have value</param>
        Private Sub SetUsedValue(ByRef value As Object, parameterName As String, defaultValue As Object)
            If value IsNot Nothing Then
                System.Web.HttpContext.Current.Session(controllerName + parameterName) = value
            ElseIf (Session(controllerName + parameterName) Is Nothing) Then
                System.Web.HttpContext.Current.Session(controllerName + parameterName) = defaultValue
            End If
            value = System.Web.HttpContext.Current.Session(controllerName + parameterName)
        End Sub

        Function AddItem(productId As Integer?)

            If productId Is Nothing Then
                Return HttpNotFound()
            End If

            Dim shoppingBasketId = GetShoppingBasketId()
            Try
                shoppingBasketAPI.AddProduct(productId, shoppingBasketId)
                TempData("AddedToBasket") = True
            Catch ex As Exception
                'Temp data informs the view was the adding succesful
                TempData("AddedToBasket") = False
            End Try

            Return RedirectToAction("Index")

        End Function

        Private Function GetShoppingBasketId() As Integer
            Dim id = System.Web.HttpContext.Current.Session("ShoppingBasketId")
            If id Is Nothing Then
                Dim shoppingBasketService As New ShoppingBasketApi
                id = shoppingBasketService.AddShoppingBasket
                System.Web.HttpContext.Current.Session("ShoppingBasketId") = id
            End If
            Return id
        End Function

    End Class
End Namespace
