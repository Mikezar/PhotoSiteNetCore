using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Common
{
    public class IdResultDto : ResultDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
