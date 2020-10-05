using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class PhotoToTag : Entity
    {
        [Key]
        public int PhotoId { get; set; }

        [Key]
        public int TagId { get; set; }
    }
}