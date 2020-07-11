using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IVySoft.VPlatform.TemplateService.ModelCore.Xml.Serialization
{
    [XmlRoot(Namespace = "IVySoft.VPlatform.TemplateService.ModelCore")]
    public class AssociationEnd
    {
        public string Type { get; set; }
        public string Property { get; set; }
        public string Multiplicity { get; set; }
		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.TemplateService.ModelCore.AssociationEnd result)
		{

			result.Type = this.Type;
			result.Property = this.Property;
			result.Multiplicity = this.Multiplicity;
		}
	}
}
