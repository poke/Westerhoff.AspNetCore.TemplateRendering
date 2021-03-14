using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TemplateRenderSample31.Templates;
using Westerhoff.AspNetCore.TemplateRendering;

namespace TemplateRenderSample31.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRazorTemplateRenderer _razorTemplateRenderer;

        [BindProperty]
        public ExampleTemplateType SelectedTemplate
        { get; set; }

        [BindProperty]
        public string Name
        { get; set; }

        [BindProperty]
        public string Parameter
        { get; set; }

        [BindProperty]
        public bool IncludeDetails
        { get; set; }

        public string RenderedTitle
        { get; set; }

        public string RenderedBody
        { get; set; }

        public IndexModel(IRazorTemplateRenderer razorTemplateRenderer)
        {
            _razorTemplateRenderer = razorTemplateRenderer;
        }

        public async Task OnGet()
        {
            Name = "user@example.com";
            Parameter = "Example";
            IncludeDetails = false;
            await RenderTemplate();
        }

        public async Task OnPost()
        {
            await RenderTemplate();
        }

        private async Task RenderTemplate()
        {
            var model = new ExampleModel
            {
                Name = Name,
                Parameter = Parameter,
                IncludeDetails = IncludeDetails,
            };
            var templatePath = $"/Templates/{SelectedTemplate}.cshtml";
            var result = await _razorTemplateRenderer.RenderAsync(templatePath, model);

            RenderedTitle = result.Title;
            RenderedBody = result.Body;
        }
    }
}
