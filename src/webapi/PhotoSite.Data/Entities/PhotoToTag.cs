using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class PhotoToTag : Entity
    {
        [Key]
        public int Id { get; set; }

        public int PhotoId { get; set; }

        public int TagId { get; set; }
    }
}