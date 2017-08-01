Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports StoreAPI
Imports PagedList

Namespace Controllers
    Public Class ProductsController
        Inherits System.Web.Mvc.Controller

        Dim productAPI As New ProductAPI

        ''' <summary>
        ''' Show all the products
        ''' </summary>
        Function Index() As ActionResult

            Return View(productAPI.GetProducts(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
        End Function

        ''' <summary>
        ''' Show one of the products
        ''' </summary>
        ''' <param name="id"></param>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim product As Product = productAPI.GetById(id)
            If IsNothing(product) Then
                Return HttpNotFound()
            End If
            Return View(product)
        End Function

        ''' <summary>
        ''' Show view for adding
        ''' </summary>
        Function Create() As ActionResult
            Return View()
        End Function


        ''' <summary>
        ''' Add given product and show the added product after that
        ''' </summary>
        ''' <param name="product">product to be added</param>
        ''' <returns></returns>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Name,Price,Amount")> ByVal product As Product) As ActionResult
            If ModelState.IsValid Then
                productAPI.Add(product)
                Return RedirectToAction("Index")
            End If
            Return View(product)
        End Function

        ''' <summary>
        ''' Show editing view
        ''' </summary>
        ''' <param name="id">id of the product to be edited</param>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim product As Product = productAPI.GetById(id)
            If IsNothing(product) Then
                Return HttpNotFound()
            End If
            Return View(product)
        End Function


        ''' <summary>
        ''' Save the given edited product and show the result
        ''' </summary>
        ''' <param name="product">edited product</param>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID,Name,Price,Amount")> ByVal product As Product) As ActionResult
            If ModelState.IsValid Then
                productAPI.Edit(product)
                Return RedirectToAction("Index")
            End If
            Return View(product)
        End Function

        ''' <summary>
        ''' Show deleting view
        ''' </summary>
        ''' <param name="id">id of the deleted item</param>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim product As Product = productAPI.GetById(id)
            If IsNothing(product) Then
                Return HttpNotFound()
            End If
            Return View(product)
        End Function


        ''' <summary>
        ''' Delete product with given id and show the list of products after that
        ''' </summary>
        ''' <param name="id">id of the product to be deleted</param>
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            productAPI.Delete(id)
            Return RedirectToAction("Index")
        End Function

    End Class
End Namespace
