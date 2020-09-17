namespace PhotoSite.WebApi.Album
{
    /// <summary>
    /// Simple album
    /// </summary>
    public class AlbumSimpleDto
    {
        /// <summary>
        /// Identification
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Identification of parent's album
        /// </summary>
        public int? ParentId { get; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Path to cover
        /// </summary>
        public string? CoverPath { get; }

        /// <summary>
        /// View of Pattern
        /// </summary>
        public ushort ViewPattern { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public AlbumSimpleDto(
            int id,
            int? parentId,
            string? title,
            string? description,
            string? coverPath,
            ushort viewPattern)
        {
            Id = id;
            ParentId = parentId;
            Title = title;
            Description = description;
            CoverPath = coverPath;
            ViewPattern = viewPattern;
        }
    }
}
