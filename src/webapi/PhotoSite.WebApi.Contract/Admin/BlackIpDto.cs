using System.Text.Json.Serialization;

namespace PhotoSite.WebApi.Admin
{
    public class BlackIpDto
    {
        /// <summary>
        /// Identification
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("mask_address")]
        public string? MaskAddress { get; set; }

        [JsonPropertyName("subnet_mask")]
        public int SubnetMask { get; set; }

        [JsonPropertyName("is_v6")]
        public bool IsInterNetworkV6 { get; set; }
    }
}