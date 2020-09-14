﻿using System;

namespace PhotoSite.Data.Entities
{
    public class Photo : Entity
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string? TitleRu { get; set; }

        public string? TitleEng { get; set; }

        public string? DescriptionRu { get; set; }

        public string? DescriptionEng { get; set; }

        public string? PhotoPath { get; set; }

        public string? ThumbnailPath { get; set; }

        public string? FileName { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public bool ShowRandom { get; set; }

        public int Order { get; set; }

        public bool IsForBackground { get; set; }
    }
}