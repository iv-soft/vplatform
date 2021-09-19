using System.ComponentModel.DataAnnotations;

namespace IVySoft.VPlatform.Model
{
    public class InstalledFeature
    {
        [Key]
        [Required]
        public string Name { get; set; }
        public int Version { get; set; }
    }
}