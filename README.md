# WebHelpers.Mvc5

![Build Status](https://coshea.visualstudio.com/_apis/public/build/definitions/c4f53986-29b5-45cd-b130-b074bbc802b0/4/badge)

A collection of helpers for ASP.NET MVC5.

## CssRewriteUrlTransformAbsolute

Converts any URLs in the input to absolute using the application's base directory.
The standard `CssRewriteUrlTransform` class doesn't use the application's absolute path required
by many assets.

For example, `url(../fonts/glyphicons.woff)` is rewritten as
`url(Contoso/Content/fonts/glyphicons.woff)` for an application whose base directory is Contoso.

```cs
.Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
```

## IsLinkActive

When building static navigation, there are two approaches to highlight the link of the current page
as active. Either you can run some JavaScript to sniff out the URLs or you can build out an `if` statement
for every link to determine whether or not to apply the class.

To make the second option easier, you can turn this:

```cshtml
<li class="@(ViewContext.RouteData.Values["Action"].ToString() == nameof(HomeController.Index) ? "active" : "")">
    <a href="@Url.Action("Index", "Home")"><i class="fa fa-link"></i> <span>Home</span></a>
</li>
```

into this:

```cshtml
<li class="@Url.IsLinkActive("Index", "Home")">
    <a href="@Url.Action("Index", "Home")"><i class="fa fa-link"></i> <span>Home</span></a>
</li>
```

## AddVersion

Adds a cache-busting version number, which is the number of ticks since the last write time of the file,
as a query parameter to the URL of the asset.

```cshtml
<img src="@Url.Content("~/Content/img/user.png").AddVersion()" />
```

outputs

```
/Content/img/user.png?v=636296810982047488
```

## EnumHandler

Renders Enums as a frozen object in JavaScript to promote re-usability between the server and client.

Add to your Web.config:

```
<system.webServer>
    <handlers>
        <remove name="WebHelpers" />
        <add name="WebHelpers" verb="GET" path="WebHelpers.axd" type="WebHelpers.Mvc5.Enum.EnumHandler" preCondition="integratedMode" />
    </handlers>
</system.webServer>
```
