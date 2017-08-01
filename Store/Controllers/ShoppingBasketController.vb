Imports System.Web.Mvc
Imports System.Net
Imports System.Data
Imports System.Data.Entity
Imports StoreAPI


Namespace Controllers
    Public Class ShoppingBasketController
        Inherits Controller

        Private shoppingBasketAPI As New ShoppingBasketApi

        Function Index() As ActionResult
            Dim id = System.Web.HttpContext.Current.Session("ShoppingBasketId")
            Dim currentBakset As ShoppingBasket
            Dim basketList = New List(Of ShoppingBasketItem)

            Try
                currentBakset = shoppingBasketAPI.GetShoppingBasketById(id)
                basketList = currentBakset.GetAllItems
                ViewBag.TotalPrice = currentBakset.GetTotal()
            Catch notFoundEx As DatabaseObjectNotFoundException
                'Basket is not yet created, no problem
                ViewBag.TotalPrice = 0
            End Try

            Return View(basketList)
        End Function


        Function Delete(id As Integer)
            Dim basketId = System.Web.HttpContext.Current.Session("ShoppingBasketId")
            Try
                shoppingBasketAPI.DeleteProduct(id, basketId)
                'Inform the view that item was succesfully added
                TempData("RemovedItem") = True
            Catch ex As Exception
                TempData("OperationFailed") = ex.Message
            End Try

            Return RedirectToAction("Index")

        End Function

        ''' <summary>
        ''' Show editing view
        ''' </summary>
        ''' <param name="id">id of the shopping basket item to be edited</param>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim basketId = System.Web.HttpContext.Current.Session("ShoppingBasketId")
            Dim currentBakset = shoppingBasketAPI.GetShoppingBasketById(basketId)
            Dim itemToBeEdited = currentBakset.GetItemByItemId(id)
            If itemToBeEdited Is Nothing Then
                Return HttpNotFound()
            End If
            Return View(itemToBeEdited)
        End Function


        ''' <summary>
        ''' Save the given edited shopping basket item and show the result
        ''' </summary>
        ''' <param name="basketItem">edited shopping basket item</param>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ID, Amount")> ByVal basketItem As ShoppingBasketItem) As ActionResult
            If ModelState.IsValid Then
                Dim shoppingBasketId = System.Web.HttpContext.Current.Session("ShoppingBasketId")
                Try
                    shoppingBasketAPI.EditShoppingBasketItem(basketItem, shoppingBasketId)
                Catch ex As Exception
                    TempData("EditFailed") = ex.Message
                End Try

                Return RedirectToAction("Index")

            End If
            Return View(basketItem)
        End Function

    End Class
End Namespace