Imports StoreAPI
Imports System.Data
Imports System.Data.Entity
Imports PagedList

Public Class ProductAPI
    Implements IProductAPI

    Public Sub Add(productToAdd As Product) Implements IProductAPI.Add
        Using db As New StoreModel
            db.Products.Add(productToAdd)
            db.SaveChanges()
        End Using

    End Sub

    Public Sub Delete(productId As Integer) Implements IProductAPI.Delete
        Using db As New StoreModel
            Dim foundProduct = db.Products.Find(productId)
            If foundProduct Is Nothing Then
                Throw New DatabaseObjectNotFoundException("Product does not exist")
            End If
            db.Products.Remove(foundProduct)
            db.SaveChanges()
        End Using
    End Sub

    Public Sub Edit(editedProduct As Product) Implements IProductAPI.Edit
        Using db As New StoreModel
            Dim foundProduct = db.Products.Find(editedProduct.ID)
            If foundProduct Is Nothing Then Throw New DatabaseObjectNotFoundException("Product does not exist")
            foundProduct.CopyValues(editedProduct)
            db.SaveChanges()
        End Using
    End Sub

    Public Function GetById(productId As Integer) As Object Implements IProductAPI.GetById
        Using db As New StoreModel
            Dim foundProduct = db.Products.Find(productId)
            If foundProduct Is Nothing Then Throw New DatabaseObjectNotFoundException("Product does not exist")
            Return foundProduct
        End Using
    End Function

    Public Function GetGroupedProducts(groupingCriterias As List(Of GroupingCondition)) As Dictionary(Of GroupingCondition, List(Of Product)) Implements IProductAPI.GetGroupedProducts
        Using db As New StoreModel
            Dim searchFilterer As New ProductSearchFilter()
            Return searchFilterer.GetGroupedProducts(db.Products, groupingCriterias)
        End Using
    End Function

    Public Function GetProducts(searchString As String, minPrice As String, maxPrice As String, sortOrder As String, rowsPerPage As Integer?, page As Integer?) As PagedList(Of Product) Implements IProductAPI.GetProducts
        Using db As New StoreModel
            Dim searchFilterer = New ProductSearchFilter()
            Return searchFilterer.FilterAndPageProducts(db.Products, searchString, minPrice, maxPrice, sortOrder, rowsPerPage, page)
        End Using
    End Function
End Class
