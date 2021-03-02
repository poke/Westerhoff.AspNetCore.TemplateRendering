using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Westerhoff.AspNetCore.TemplateRendering.Test.Templates;
using Xunit;

namespace Westerhoff.AspNetCore.TemplateRendering.Test
{
    public class RenderTests
    {
        [Fact]
        public async Task Static()
        {
            using var host = Utility.CreateHost();
            var templateRenderer = host.Services.GetService<IRazorTemplateRenderer>();
            var rendered = await templateRenderer.RenderAsync("/Templates/Static.cshtml", new object());

            Assert.Null(rendered.Title);
            Assert.Equal("<p>Foo</p>\r\n", rendered.Body);
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
            Assert.Equal("<p>example</p>\r\n", rendered.Body);
        }
    }
}
