Imports PagedList

Public Interface IProductAPI

    Sub Edit(editedProduct As Product)

    Sub Add(productToAdd As Product)

    Sub Delete(productId As Integer)

    Function GetById(productId As Integer)

    ''' <summary>
    ''' Get all products matching to given conditions. List is paged with given conditions. 
    ''' If no conditions are given all the products are returned
    ''' </summary>
    Function GetProducts(searchString As String, minPrice As String, maxPrice As String, sortOrder As String, rowsPerPage As Integer?, page As Integer?) As PagedList(Of Product)

    ''' <summary>
    ''' Products are returned grouped by given conditions. Depending on the conditions products can exists in multiple gorups.
    ''' If conditions are not given empty result is returned.
    ''' </summary>
    Function GetGroupedProducts(groupingCriterias As List(Of GroupingCondition)) As Dictionary(Of GroupingCondition, List(Of Product))

End Interface
