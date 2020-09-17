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
        public int Id { get; }

        /// <summary>
        /// Identification of parent's album
        /// </summary>
        public int? ParentId { get; }

        /// <summary>
        /// Title by Russian
        /// </summary>
        public string? TitleRu { get; }

        /// <summary>
        /// Title by English
        /// </summary>
        public string? TitleEng { get; }

        /// <summary>
        /// Description by Russian
        /// </summary>
        public string? DescriptionRu { get; }

        /// <summary>
        /// Description by English
        /// </summary>
        public string? DescriptionEng { get; }

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
        public AlbumDto(
            int id, 
            int? parentId, 
            string? titleRu, 
            string? titleEng, 
            string? descriptionRu, 
            string? descriptionEng, 
            string? coverPath, 
            ushort viewPattern)
        {
            Id = id;
            ParentId = parentId;
            TitleRu = titleRu;
            TitleEng = titleEng;
            DescriptionRu = descriptionRu;
            DescriptionEng = descriptionEng;
            CoverPath = coverPath;
            ViewPattern = viewPattern;
        }
    }
}
