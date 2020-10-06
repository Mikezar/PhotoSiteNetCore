using System.Collections.Generic;

namespace PhotoSite.Data.Entities
{
    public class Tag : Entity
    {
        public string? Title { get; set; }

        public ICollection<PhotoToTag>? Photos { get; set; }
    }
}