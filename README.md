# WebHelpers.Mvc5

![Build Status](https://coshea.visualstudio.com/_apis/public/build/definitions/c4f53986-29b5-45cd-b130-b074bbc802b0/4/badge)
![NuGet](https://img.shields.io/nuget/v/WebHelpers.Mvc5.svg)

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

## IsTreeviewActive

Similar to `IsLinkActive`, this makes it easy to determine whether the treeview is the active link.

```
@{
    var treeviewActions = new Dictionary<string, string>
    {
        { "Action", "Controller" }
    };
}

<li class="treeview @Url.IsTreeviewActive(treeviewActions)">
    <a href="#"><i class="fa fa-cogs"></i> <span>Action</span> <i class="fa fa-angle-left pull-right"></i></a>
    <ul class="treeview-menu">
        <li class="@Url.IsLinkActive("Action", "Controller")">
            <a href="@Url.Action("Action", "Controller")"><i class="fa fa-trash"></i> <span>Action</span></a>
        </li>
    </ul>
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

Add to your _Layout.cshtml:

```
<script src="@EnumHandler.HandlerUrl"></script>
```

Then decorate your enum with the `ExposeInJavaScript` attribute:
```
[ExposeInJavaScript]
public enum MyEnum
{
    Test
}
```

Alternatively, you can specify the enums to include and exclude via configuration.
This is helpful if you choose to keep your enums clean or if they reside in other
libraries that can't take on this dependency. To do this, you can register them in
your `Global.asax`.

```
protected void Application_Start()
{
    EnumHandler.EnumsToExpose.Include(typeof(MyEnum));
}
```

## ClientIP

Gets the IP address of the client sending the request. This method will return the originating
IP if specified by a proxy but makes no guarantee that this is the client's true IP address.
Since these headers can be spoofed, you are encouraged to perform additional validation if
you are using the IP in a sensitive context.

```
var ip = HttpContext.Current.Request.ClientIP();
```
