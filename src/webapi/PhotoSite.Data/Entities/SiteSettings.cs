using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class SiteSettings : EntityBase
    {
        [Key]
        public string? Name { get; set; }

        public string? Value { get; set; }
    }
}