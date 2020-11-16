namespace PhotoSite.Data.Entities
{
    public class PhotoToTag : EntityBase
    {
        public int PhotoId { get; set; }
        public int TagId { get; set; }

        public Photo? Photo { get; set; }
        public Tag? Tag { get; set; }
    }
}