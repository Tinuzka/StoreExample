@Code
    ViewData("Title") = "Home Page"
End Code

<div class="jumbotron">
    <h1>Wonder Store</h1>
</div>
<div class="row">
    <div class="col-md-4">
        <h2>Welcome to Wonder Store</h2>
        <p>
            Wonder Store is a sample application, which was made by Tiina Mononen, to demonstrate
            ASP.NET MVC web application and Entity Framework.
        </p>
        <p>More info about Tiina Mononen can be found in her <a href="https://www.linkedin.com/in/tiina-mononen-84773748/">LinkedIn profile</a> and in <a href="https://github.com/Tinuzka/">GitHub</a></p>
    </div>
    <div class="col-md-4">
        <h2>Go shopping</h2>
        <p>Search through all the wonders we have</p>
        <p><a class="btn btn-default" href="/Catalog/">See catalog &raquo;</a></p>
    </div>
    <div class="col-md-4">
        <h2>Add wonders</h2>
        <p>Didn't find what you were looking for? Add a wonder of your own.</p>
        <p><a class="btn btn-default" href="/Products/">Edit products &raquo;</a></p>
    </div>
</div>
