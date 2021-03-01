using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TemplateRenderSample31.Templates;
using Westerhoff.AspNetCore.TemplateRendering;

namespace TemplateRenderSample31.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRazorTemplateRenderer _razorTemplateRenderer;

        public string ExampleTemplateTitle
        { get; set; }

        public string ExampleTemplateBody
        { get; set; }

        public IndexModel(IRazorTemplateRenderer razorTemplateRenderer)
        {
            _razorTemplateRenderer = razorTemplateRenderer;
        }

        public async Task OnGet(string userId = null)
        {
            var model = new ExampleModel
            {
                UserId = userId ?? "user@example.com",
            };
            var result = await _razorTemplateRenderer.RenderAsync("/Templates/Example.cshtml", model);

            ExampleTemplateTitle = result.Title;
            ExampleTemplateBody = result.Body;
        }
    }
}
