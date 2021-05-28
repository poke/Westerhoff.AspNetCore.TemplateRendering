using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using VerifyXunit;
using Westerhoff.AspNetCore.TemplateRendering.Test.Templates;
using Xunit;

namespace Westerhoff.AspNetCore.TemplateRendering.Test
{
    [UsesVerify]
    public class RenderTests
    {
        [Fact]
        public async Task Static()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Static.cshtml", new object());

            Assert.Null(rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task StaticTitle()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/StaticTitle.cshtml", new object());

            Assert.Equal("Static title", rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task Simple()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Simple.cshtml", new SimpleTemplateModel
            {
                Value = "example",
            });

            Assert.Null(rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task SimpleTitle()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/SimpleTitle.cshtml", new SimpleTemplateModel
            {
                Value = "example",
            });

            Assert.Equal("example title", rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task Loops()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Loops.cshtml", new LoopsTemplateModel
            {
                Header = "Loops example",
                Items = new[]
                {
                    new LoopsItemModel { Name = "Item 1", Url = "https://example.com/items/1" },
                    new LoopsItemModel { Name = "Item 2", Url = "https://example.com/items/2" },
                    new LoopsItemModel { Name = "Item 3", Url = "https://example.com/items/3" },
                },
            });

            Assert.Null(rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task Mixed()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Mixed.cshtml", new MixedTemplateModel
            {
                UserName = "user@example.com",
                ShowDetails = false,
            });

            Assert.Equal("Title for user@example.com", rendered.Title);
            await Verifier.Verify(rendered.Body);
        }

        [Fact]
        public async Task Mixed_WithDetails()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Mixed.cshtml", new MixedTemplateModel
            {
                UserName = "user@example.com",
                ShowDetails = true,
            });

            Assert.Equal("Title for user@example.com", rendered.Title);
            await Verifier.Verify(rendered.Body);
        }
    }
}
