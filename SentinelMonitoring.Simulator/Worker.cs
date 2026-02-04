using SentinelMonitoring.Core.Models;
using System.Net.Http.Json;

namespace SentinelMonitoring.Simulator;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _deviceId = "DEVICE-001";
    private readonly Random _random = new Random();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Simulator started for Device: {DeviceId}", _deviceId);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var data = new SensorData
                {
                    DeviceId = _deviceId,
                    Temperature = 20 + _random.NextDouble() * 70, // 20-90
                    Pressure = 100 + _random.NextDouble() * 50,    // 100-150
                    Humidity = 30 + _random.NextDouble() * 40      // 30-70
                };

                var response = await _httpClient.PostAsJsonAsync("api/telemetry", data, stoppingToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Telemetry sent: Temp={Temp:F2}°C, Status={Status}", data.Temperature, data.Status);
                }
                else
                {
                    _logger.LogWarning("Failed to send telemetry: {Reason}", response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending telemetry");
            }

            await Task.Delay(2000, stoppingToken);
        }
    }
}
