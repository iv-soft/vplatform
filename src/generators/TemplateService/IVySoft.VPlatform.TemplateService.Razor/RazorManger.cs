using IVySoft.VPlatform.TemplateService.Runtime.IndexScript;

namespace IVySoft.VPlatform.TemplateService.Razor
{
    internal class RazorManger : IRazorManger, IBuildContextDependent
    {
        private BuildContext context_;

        public RazorTemplateInstance load(string file_path)
        {
            return new RazorTemplateInstance(this.context_, file_path);
        }

        public void SetBuildContext(BuildContext context)
        {
            this.context_ = context;
        }
    }
}