using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin
{
    public class ConfigParamDto
    {
        [JsonPropertyName("watermark_font")]
        public string? WatermarkFont { get; set; }

        [JsonPropertyName("watermark_font_size")]
        public int WatermarkFontSize { get; set; }

        [JsonPropertyName("watermark_text")]
        public string? WatermarkText { get; set; }

        [JsonPropertyName("signature_font")]
        public string? SignatureFont { get; set; }

        [JsonPropertyName("signature_font_size")]
        public int SignatureFontSize { get; set; }

        [JsonPropertyName("signature_text")]
        public string? SignatureText { get; set; }

        [JsonPropertyName("stamp_font")]
        public string? StampFont { get; set; }

        [JsonPropertyName("stamp_font_size")]
        public int StampFontSize { get; set; }

        [JsonPropertyName("stamp_text")]
        public string? StampText { get; set; }

        [JsonPropertyName("alpha")]
        public int Alpha { get; set; }
    }
}