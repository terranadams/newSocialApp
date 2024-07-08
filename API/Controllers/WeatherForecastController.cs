using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; // Define the namespace that this controller is a part of.

// Attributes that indicate this class is an API controller and sets up routing.
[ApiController] // This attribute indicates that this class is an API Controller and enables MVC to infer certain behaviors.
[Route("[controller]")] // This attribute sets the route for the API to match the controller's name (e.g. 'WeatherForecast').
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger; // A logger service provided by dependency injection to log information.

    public WeatherForecastController(ILogger<WeatherForecastController> logger) // Constructor for the WeatherForecastController that takes an ILogger instance.
    {
        _logger = logger; // Assigns the passed-in logger to the private field _logger.
    }

    [HttpGet(Name = "GetWeatherForecast")] // This attribute indicates that this action responds to an HTTP GET request.
    public IEnumerable<WeatherForecast> Get() // The action method that handles the GET request and returns an enumerable of WeatherForecast.
    {
        // Generates a sequence of 5 WeatherForecast objects with random data.
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)), // Sets the Date to a day in the future based on the index.
            TemperatureC = Random.Shared.Next(-20, 55), // Generates a random temperature between -20 and 55 degrees Celsius.
            Summary = Summaries[Random.Shared.Next(Summaries.Length)] // Picks a random summary from the Summaries array. 
        })
        .ToArray(); // Converts the resulting sequence to an array before returning it.
    }
}
