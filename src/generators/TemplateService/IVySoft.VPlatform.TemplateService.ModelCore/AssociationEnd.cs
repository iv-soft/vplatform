using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVySoft.VPlatform.TemplateService.ModelCore
{
    [Owned]
    public class AssociationEnd
    {
	        public string Type { get; set; }
	        public string Property { get; set; }
	        public string Multiplicity { get; set; }
    }
}
