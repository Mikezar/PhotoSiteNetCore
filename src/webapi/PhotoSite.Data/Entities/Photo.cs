using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoSite.Data.Entities
{
    public class Photo : Entity
    {
        [Key]
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? PhotoPath { get; set; }

        public string? ThumbnailPath { get; set; }

        public string? FileName { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public bool ShowRandom { get; set; }

        public int Order { get; set; }

        public bool IsForBackground { get; set; }
    }
}