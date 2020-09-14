namespace PhotoSite.Data.Entities
{
    /// <summary>
    /// ALbum
    /// </summary>
    public class Album : Entity
    {
        /// <summary>
        /// Identification
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identification of parent's album
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Title by Russian
        /// </summary>
        public string? TitleRu { get; set; }

        /// <summary>
        /// Title by English
        /// </summary>
        public string? TitleEng { get; set; }

        /// <summary>
        /// Description by Russian
        /// </summary>
        public string? DescriptionRu { get; set; }

        /// <summary>
        /// Description by English
        /// </summary>
        public string? DescriptionEng { get; set; }

        /// <summary>
        /// Path to cover
        /// </summary>
        public string? CoverPath { get; set; }

        /// <summary>
        /// View of Pattern
        /// </summary>
        public int ViewPattern { get; set; }
    }
}
