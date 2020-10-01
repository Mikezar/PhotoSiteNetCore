using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class Tag : Entity
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

    }
}