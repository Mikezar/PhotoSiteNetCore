using System.ComponentModel;

namespace PhotoSite.ApiService.Data
{
    public class Settings
    {
        [DefaultValue("Bell MT")]
        public string? WatermarkFont { get; set; }

        [DefaultValue(60)]
        public int WatermarkFontSize { get; set; }

        [DefaultValue("AlexSilver.Photo@gmail.com")]
        public string? WatermarkText { get; set; }

        [DefaultValue("Edwardian Script ITC")]
        public string? SignatureFont { get; set; }

        [DefaultValue(43)]
        public int SignatureFontSize { get; set; }

        [DefaultValue("© Aleksandr Serebryakov")]
        public string? SignatureText { get; set; }

        [DefaultValue("Bell MT")]
        public string? StampFont { get; set; }

        [DefaultValue(45)]
        public int StampFontSize { get; set; }

        [DefaultValue("www.askanio.ru")]
        public string? StampText { get; set; }

        [DefaultValue(80)]
        public int Alpha { get; set; }
    }
}