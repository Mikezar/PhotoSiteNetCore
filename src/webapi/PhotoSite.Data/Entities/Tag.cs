using System.Collections.Generic;

namespace PhotoSite.Data.Entities
{
    public class Tag : EntityBase
    {
        public string? Title { get; set; }

        public ICollection<PhotoToTag>? Photos { get; set; }
    }
}