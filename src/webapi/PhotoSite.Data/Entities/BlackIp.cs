
namespace PhotoSite.Data.Entities
{
    /// <summary>
    /// Black IP
    /// </summary>
    public class BlackIp : EntityBase
    {
        public string? MaskAddress { get; set; }

        public int SubnetMask { get; set; }

        public bool IsInterNetworkV6 { get; set; }
    }
}