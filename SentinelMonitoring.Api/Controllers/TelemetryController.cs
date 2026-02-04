using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SentinelMonitoring.Api.Hubs;
using SentinelMonitoring.Core.Models;
using System.Threading.Tasks;

namespace SentinelMonitoring.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly IHubContext<TelemetryHub> _hubContext;
        private readonly SentinelMonitoring.Infrastructure.Data.SentinelDbContext _context;

        public TelemetryController(IHubContext<TelemetryHub> hubContext, SentinelMonitoring.Infrastructure.Data.SentinelDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostTelemetry([FromBody] SensorData data)
        {
            // Simulate processing
            if (data.Temperature > 80)
            {
                data.Status = "Critical";
            }
            else if (data.Temperature > 60)
            {
                data.Status = "Warning";
            }

            // Persistence
            _context.Telemetries.Add(data);
            await _context.SaveChangesAsync();

            // Broadcast to real-time dashboard
            await _hubContext.Clients.All.SendAsync("ReceiveTelemetry", data);

            return Ok(new { Message = "Telemetry received and saved", Data = data });
        }
    }
}
