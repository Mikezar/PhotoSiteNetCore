﻿namespace PhotoSite.Data.Entities
{
    public class Watermark : Entity
    {
        public int PhotoId { get; set; }

        public bool IsWatermarkApplied { get; set; }

        public bool IsWatermarkBlack { get; set; }

        public bool IsSignatureApplied { get; set; }

        public bool IsSignatureBlack { get; set; }

        public bool IsWebSiteTitleApplied { get; set; }

        public bool IsWebSiteTitleBlack { get; set; }

        public bool IsRightSide { get; set; }
    }
}