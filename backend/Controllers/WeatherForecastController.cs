using Microsoft.AspNetCore.Mvc;
using LoadoutApi.Models;
using System.Linq;

namespace LoadoutApi.Controllers;


[ApiController]
[Route("[controller]")]

public class GamesController : ControllerBase
{
    private static List<Game> games = new List<Game>
    {
            new Game
            {
                Id = 1,
                Name = "Gece Yolcusu",
                Status = Status.Playing,
                Rating = 8.5
            },
            new Game
            {
                Id = 2,
                Name = "Kül Krallığı",
                Status = Status.Completed,
                Rating = 9.0
            },
            new Game
            {
                Id = 3,
                Name = "Taş Bahçe",
                Status = Status.PlanToPlay
            }

    };

    [HttpGet]
    public List<Game> Get()
    {
        return games;
    }

    [HttpPost]
    public Game Post(Game game)
    {
        game.Id = games.Max(g => g.Id) + 1;
        games.Add(game);
        return game;
    }

}


public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
