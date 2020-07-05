using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Linq;

namespace IVySoft.VPlatform.Target.ModelCode.Xml.Serialization
{
    [XmlRoot(Namespace = XmlConfig.Namespace)]
    public class Association
    {
		[XmlElement()]
		public string Name { get; set; }
		[XmlElement()]
		public AssociationEnd Left { get; set; }
		[XmlElement()]
		public AssociationEnd Right { get; set; }



		public virtual object ToModel()
		{
			var result = new IVySoft.VPlatform.Target.ModelCode.Association();
			this.InitModel(result);
			return result;
		}

		protected void InitModel(IVySoft.VPlatform.Target.ModelCode.Association result)
		{
				result.Name = this.Name;
				result.Left = (IVySoft.VPlatform.Target.ModelCode.AssociationEnd)this.Left?.ToModel();
				result.Right = (IVySoft.VPlatform.Target.ModelCode.AssociationEnd)this.Right?.ToModel();
				        }
    }
}