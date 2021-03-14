using System.ComponentModel.DataAnnotations;

namespace TemplateRenderSample21.Templates
{
    public enum ExampleTemplateType
    {
        [Display(Name = "Simple text")]
        Simple,

        [Display(Name = "Complex example with Razor logic")]
        Complex,
    }
}
