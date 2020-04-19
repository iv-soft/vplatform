using System;
using System.Collections.Generic;
using System.Text;

namespace IVySoft.VPlatform.TemplateService.Runtime.IndexScript
{
    public interface IBuildContextDependent
    {
        void SetBuildContext(BuildContext context);
        void ContextCompleted();
    }
}
