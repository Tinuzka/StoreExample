@ModelType PagedList.IPagedList(Of Product)
@Imports PagedList.Mvc
@Imports StoreAPI
<link href = "~/Content/PagedList.css" rel="stylesheet" type="text/css" />

@If TempData("AddedToBasket") IsNot Nothing Then
    @<br />
    If TempData("AddedToBasket") = True Then
        @Html.Label("Succesfully added product to the basket")
    Else
        @Html.Label("Product could not be added to the basket")
    End If
End If


<h2>Index</h2>

        @Code
            ViewData("Title") = "Index"
            Using (Html.BeginForm("Index", "Catalog", FormMethod.Get))
                @<p>
                    Title: @Html.TextBox("SearchString", Session("CatalogSearch"))
                    Min price: @Html.TextBox("MinPrice", Session("CatalogMinPrice"))
                    Max price: @Html.TextBox("MaxPrice", Session("CatalogMaxPrice"))
                    <input type="submit" value="Filter" />
                </p>
            End Using
        End Code

            <Table Class="table">
    <tr>
                <tr>
                <th>
                @Html.ActionLink("Name", "Index", New With {.sortOrder = ViewBag.NameSortParm})
        </th>
        <th>
                @Html.ActionLink("Price", "Index", New With {.sortOrder = ViewBag.PriceSortParm})
        </th>
        <th>
                @Html.Label("Amount")
        </th>
        <th></th>
    </tr>

                @For Each item In Model
                @<tr>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Name)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Price)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Amount)
                    </td>
                    <td>
                        @Html.ActionLink("Buy", "AddItem", New With {.productId = item.ID})
                    </td>
                </tr>
                Next

</table>
<br />
Page @IIf(Model.PageCount < Model.PageNumber, 0, Model.PageNumber) of @Model.PageCount

@Html.PagedListPager(Model, Function(page) Url.Action("Index", New With {page}))
