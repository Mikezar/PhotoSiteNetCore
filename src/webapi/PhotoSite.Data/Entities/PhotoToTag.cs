﻿namespace PhotoSite.Data.Entities
{
    public class PhotoToTag
    {
        public int PhotoId { get; set; }
        public int TagId { get; set; }

        public Photo? Photo { get; set; }
        public Tag? Tag { get; set; }
    }
}