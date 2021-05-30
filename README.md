# Westerhoff.AspNetCore.TemplateRendering

[![See this package on NuGet](https://img.shields.io/nuget/v/Westerhoff.AspNetCore.TemplateRendering.svg?style=flat-square)](https://www.nuget.org/packages/Westerhoff.AspNetCore.TemplateRendering)

A small library for rendering Razor views directly, e.g. for using Razor as email templates. This library is built on top of the ASP.NET Core Razor template rendering infrastructure and merely provides a means to render any Razor view as a string.

Since this library is built on top of the Razor view engine services in ASP.NET Core, this library is best suited for being used in existing web applications. Just like other Razor views, the templates will be compiled and build time and embedded into the assembly. If you want to use Razor templating in non-web contexts, or when the templates should be compiled at run-time and e.g. stored in a database, consider using a different library instead, for example [RazorLight](https://www.nuget.org/packages/RazorLight/) or [Razor.Templating.Core](https://www.nuget.org/packages/Razor.Templating.Core/).

## Using the library

To enable the library, call `AddRazorTemplateRendering()` on the [`IServiceCollection`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-5.0) in the `ConfigureServices` method:

```csharp
services.AddRazorTemplateRenderer();
```

Note that this will not automatically register the Razor view engine on its own but instead assumes that you have already done that implicitly as part of your web application, when enabling support for Razor views. If you have a call to either [`AddRazorPages()`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addrazorpages?view=aspnetcore-5.0) or [`AddControllersWithViews()`](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollerswithviews?view=aspnetcore-5.0) then there is nothing else to do. Otherwise, you can also register the minimal requirements using the following calls:

```csharp
services.AddMvcCore().AddRazorViewEngine();
```

### Rendering templates as string

You can inject the `IRazorTemplateRenderer` and call its `RenderStringAsync` method to render any Razor view to a string:

```html
@model ExampleModel
<p>Hello @Model.Name!</p>
```

```csharp
var model = new ExampleModel { Name = "World" };
var body = await _razorTemplateRenderer.RenderStringAsync("/Templates/Example.cshtml", model);
Console.WriteLine(body); // <p>Hello World!</p>
```

### Support for the `Title` property

You can set the `ViewData["Title"]` property from within the view and then access its value using the `RenderAsync` method. This can be used for example for sending emails where you might want to give the Razor template control over the email subject, which can then be set as the `Title`:

```html
@{
    ViewData["Title"] = $"Hello {Model.Name}";
}
<p>Hello @Model.Name!</p>
```

```csharp
var model = new ExampleModel { Name = "World" };
var result = await _razorTemplateRenderer.RenderAsync("/Templates/Example.cshtml", model);

Console.WriteLine(result.Title); // Hello World
Console.WriteLine(result.Body) // <p>Hello world!</p>
```
