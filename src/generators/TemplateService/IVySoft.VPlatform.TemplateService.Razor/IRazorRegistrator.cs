namespace IVySoft.VPlatform.TemplateService.Razor
{
    internal interface IRazorRegistrator
    {
        void add_component(string name, RazorRegistrator.Component component);
        RazorRegistrator.Component get_component(string name);
    }
}