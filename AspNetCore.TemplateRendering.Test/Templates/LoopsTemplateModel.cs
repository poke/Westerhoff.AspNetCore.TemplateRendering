namespace Westerhoff.AspNetCore.TemplateRendering.Test.Templates
{
    public class LoopsTemplateModel
    {
        public string Header
        { get; set; }

        public LoopsItemModel[] Items
        { get; set; }
    }

    public class LoopsItemModel
    {
        public string Name
        { get; set; }

        public string Url
        { get; set; }
    }
}
