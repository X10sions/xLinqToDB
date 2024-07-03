using Common.Features.DummyFakeExamples.WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace X10sions.Playground.WebApi.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase {

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger) {
      _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get() {
      return await WeatherForecast.GetRandomListAsync(5);
    }
  }
}