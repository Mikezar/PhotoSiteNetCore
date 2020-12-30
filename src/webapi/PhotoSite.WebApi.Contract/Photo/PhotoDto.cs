using System;
using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class PhotoDto
    {
        [JsonPropertyName("photo_id")]
        public int PhotoId { get; set; }

        [JsonPropertyName("album_id")]
        public int AlbumId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("photo_path")]
        public string? PhotoPath { get; set; }

        [JsonPropertyName("thumbnail_path")]
        public string? ThumbnailPath { get; set; }

        [JsonPropertyName("file_name")]
        public string? FileName { get; set; }

        [JsonPropertyName("creation_date")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonPropertyName("show_random")]
        public bool ShowRandom { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("is_for_background")]
        public bool IsForBackground { get; set; }

        //[JsonPropertyName("id")]
        //public ICollection<PhotoToTag>? Tags { get; set; }
    }
}