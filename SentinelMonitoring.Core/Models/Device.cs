using System;

namespace SentinelMonitoring.Core.Models
{
    public class Device
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime LastSeen { get; set; }
    }
}
