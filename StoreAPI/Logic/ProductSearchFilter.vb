Imports PagedList
Imports System.Linq
Imports System.Data.Entity.Infrastructure


Public Class ProductSearchFilter

    ''' <summary>
    ''' Returns given list grouped with given criterias. If criterias are not given returns empty result.
    ''' </summary>
    Public Function GetGroupedProducts(products As IEnumerable(Of Product), groupingCriterias As List(Of GroupingCondition)) As Dictionary(Of GroupingCondition, List(Of Product))

        If products Is Nothing Then Throw New ArgumentNullException("No products")

        Dim groupedProducts As New Dictionary(Of GroupingCondition, List(Of Product))

        If groupingCriterias Is Nothing Then Return groupedProducts

        For Each criteria In groupingCriterias
            Dim criteriaResult = products
            If criteria.minValue IsNot Nothing Then
                If criteria.minValueIsInclude Then
                    criteriaResult = (From p In criteriaResult Where p.Price >= criteria.minValue)
                Else
                    criteriaResult = (From p In criteriaResult Where p.Price > criteria.minValue)
                End If
            End If

            If criteria.maxValue IsNot Nothing Then
                If criteria.maxValueIsInclude Then
                    criteriaResult = (From p In criteriaResult Where p.Price <= criteria.maxValue)
                Else
                    criteriaResult = (From p In criteriaResult Where p.Price < criteria.maxValue)
                End If

            End If
            groupedProducts.Add(criteria, (From p In criteriaResult).ToList())
        Next

        Return groupedProducts

    End Function

    Public Function FilterAndPageProducts(products As IEnumerable(Of Product), searchString As String, minPrice As String, maxPrice As String, sortOrder As String, rowsPerPage As Integer?, page As Integer?) As PagedList(Of Product)

        If products Is Nothing Then Throw New ArgumentNullException("No products")

        If Not String.IsNullOrEmpty(searchString) Then
            products = (From p In products Where p.Name.ToLower().Contains(searchString.ToLower))
        End If

        Dim searchedMinPrice = 0
        If Not String.IsNullOrEmpty(minPrice) Then
            Integer.TryParse(minPrice, searchedMinPrice)
            products = (From p In products Where p.Price >= searchedMinPrice)
        End If

        If Not String.IsNullOrEmpty(maxPrice) Then
            Dim searchedMaxPrice = Integer.MaxValue
            Integer.TryParse(maxPrice, searchedMaxPrice)
            If searchedMaxPrice > searchedMinPrice Then
                products = (From p In products Where p.Price <= searchedMaxPrice)
            End If
        End If

        Select Case sortOrder
            Case ProductSortOrder.NameDesc.ToString
                products = (From p In products).OrderByDescending(Function(p) p.Name)
            Case ProductSortOrder.PriceAsc.ToString
                products = (From p In products).OrderBy(Function(p) p.Price)
            Case ProductSortOrder.PriceDesc.ToString
                products = (From p In products).OrderByDescending(Function(p) p.Price)
            Case Else
                products = (From p In products).OrderBy(Function(p) p.Name)
        End Select

        If page Is Nothing Then
            page = 1
        End If
        Dim finalResultList = (From p In products).ToList()

        If rowsPerPage Is Nothing Then
            rowsPerPage = finalResultList.Count
        End If

        Return finalResultList.ToPagedList(pageNumber:=page, pageSize:=rowsPerPage)
    End Function

End Class
