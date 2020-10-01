using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class SiteSettings : Entity
    {
        [Key]
        public string? Name { get; set; }

        public string? Value { get; set; }
    }
}