using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Weather.Context;
using Weather.Domain;

namespace Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherContext _context;
        private readonly IConfiguration _config;
        private readonly HttpClient _http;
        public WeatherController(WeatherContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _http = new HttpClient();
        }

        [HttpGet("current/{location}")]
        public async Task<IActionResult> GetCurrent(string location)
        {
            var apiKey = _config["OpenWeather:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&units=metric&appid={apiKey}";
            try
            {
                var resp = await _http.GetAsync(url);
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
                var desc = doc.RootElement.GetProperty("weather")[0].GetProperty("description").GetString();
                var icon = doc.RootElement.GetProperty("weather")[0].GetProperty("icon").GetString();

                var record = new WeatherRecord
                {
                    Location = location,
                    FetchedAt = DateTime.UtcNow,
                    Temperature = temp,
                    Description = desc,
                    Icon = icon
                };
                _context.WeatherRecords.Add(record);
                await _context.SaveChangesAsync();

                return Ok(new { temp, desc, icon });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Unable to fetch data");
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] string location, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            if ((to - from).TotalDays > 30)
                return BadRequest("Date range cannot exceed 30 days.");

            var data = await _context.WeatherRecords
                .Where(w => w.Location == location && w.FetchedAt >= from && w.FetchedAt <= to)
                .OrderBy(w => w.FetchedAt)
                .ToListAsync();
            return Ok(data);
        }
    }

}
