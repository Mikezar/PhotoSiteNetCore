using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Photo
{
    public class WatermarkDto
    {
        [JsonPropertyName("photo_id")]
        public int PhotoId { get; set; }

        [JsonPropertyName("is_watermark_applied")]
        public bool IsWatermarkApplied { get; set; }

        [JsonPropertyName("is_watermark_black")]
        public bool IsWatermarkBlack { get; set; }

        [JsonPropertyName("is_signature_applied")]
        public bool IsSignatureApplied { get; set; }

        [JsonPropertyName("is_signature_black")]
        public bool IsSignatureBlack { get; set; }

        [JsonPropertyName("is_web_site_title_applied")]
        public bool IsWebSiteTitleApplied { get; set; }

        [JsonPropertyName("is_web_site_title_black")]
        public bool IsWebSiteTitleBlack { get; set; }

        [JsonPropertyName("is_right_side")]
        public bool IsRightSide { get; set; }
    }
}