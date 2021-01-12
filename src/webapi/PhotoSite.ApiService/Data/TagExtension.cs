using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Data
{
    public class TagExtension : Tag
    {
        public TagExtension()
        {

        }

        public TagExtension(Tag tag)
        {
            Id = tag.Id;
            Title = tag.Title;
        }

        public int PhotoCount { get; set; }
    }
}