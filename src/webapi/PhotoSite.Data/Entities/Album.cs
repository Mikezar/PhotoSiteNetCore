using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identification of parent's album
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }

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
