using Microsoft.AspNetCore.SignalR;
using SentinelMonitoring.Core.Models;
using System.Threading.Tasks;

namespace SentinelMonitoring.Api.Hubs
{
    public class TelemetryHub : Hub
    {
        public async Task SendTelemetry(SensorData data)
        {
            await Clients.All.SendAsync("ReceiveTelemetry", data);
        }

        public async Task BroadcastStatus(string deviceId, string status)
        {
            await Clients.All.SendAsync("DeviceStatusUpdate", deviceId, status);
        }
    }
}
