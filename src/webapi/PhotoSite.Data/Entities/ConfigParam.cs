using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class ConfigParam
    {
        [Key]
        public string? Name { get; set; }

        public string? Value { get; set; }
    }
}