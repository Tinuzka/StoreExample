@Imports StoreAPI
@ModelType IEnumerable(Of StoreAPI.ShoppingBasketItem)

@Code
    ViewData("Title") = "Index"
End Code

@If TempData("OperationFailed") IsNot Nothing Then
    @<br />
    @Html.Label(TempData("OperationFailed"))
End If

<h2>Shopping basket</h2>

<table class="table">
    <tr>
        <th>
            @Html.Label("Name")
        </th>
        <th>
            @Html.Label("Quantity")
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.product.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Amount)
        </td>
        <td>
            @Html.ActionLink("Delete", "Delete", New With {.id = item.product.ID}) | @Html.ActionLink("Edit", "Edit", New With {.id = item.ID})
        </td>
    </tr>
Next

</table>
<br/>
Total price: @ViewBag.TotalPrice