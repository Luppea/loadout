namespace LoadoutApi.Models;

public class Game
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public string Name { get; set; } = "";
    public double? Rating { get; set; }

}

