using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}