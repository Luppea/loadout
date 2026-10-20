List<Game> games = new List<Game>();


games.Add(new Game { Name = "Gece Yolcusu", Status = Status.Playing });

games.Add(new Game { Name = "Batman", Status = Status.Planned });

games.Add(new Game { Name = "Last of Us", Status = Status.Dropped });



while (true)

{

    Console.WriteLine("1) Listele");

    Console.WriteLine("2) Ekle");

    Console.WriteLine("0) Çıkış");

    Console.Write("Seçim:");



    string? secim = Console.ReadLine();



    if (secim == "1")

    {

        foreach (Game game in games)

        {
            Console.WriteLine($"{game.Name} - {game.Status}");
        }

    }

    else if (secim == "2")

    {

        Console.Write("Oyun adı: ");
        string? game_name = Console.ReadLine();
        games.Add(new Game { Name = $"{game_name}", Status = Status.Planned });

    }

    else if (secim == "0")

    {
        break;
    }

    else
    {
        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
    }

}