using System;

namespace SentinelMonitoring.Core.Models
{
    public class SensorData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DeviceId { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Normal";
    }
}
