using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Album
{
    /// <summary>
    /// Album
    /// </summary>
    public class AlbumDto
    {
        /// <summary>
        /// Identification
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identification of parent's album
        /// </summary>
        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Path to cover
        /// </summary>
        [JsonPropertyName("cover_path")]
        public string? CoverPath { get; set; }

        /// <summary>
        /// View of Pattern
        /// </summary>
        [JsonPropertyName("view_pattern")]
        public ushort ViewPattern { get; set; }

    }
}
