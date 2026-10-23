List<Game> games = new List<Game>();


games.Add(new Game { Name = "Gece Yolcusu", Status = Status.Playing });

games.Add(new Game { Name = "Batman", Status = Status.Planned, Rating = 5.5 });

games.Add(new Game { Name = "Last of Us", Status = Status.Dropped, Rating = 10 });



while (true)

{

    Console.WriteLine("1) Listele");

    Console.WriteLine("2) Ekle");

    Console.WriteLine("3) Oynadıklarımı listele");

    Console.WriteLine("4) Puana göre sırala");

    Console.WriteLine("5) Ortalama puan");

    Console.WriteLine("6) RAWG simülasyonu");

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

    else if (secim == "3")
    {
        var oynananlar = games.Where(g => g.Status == Status.Playing);
        foreach (Game g in oynananlar)
        {
            Console.WriteLine($"{g.Name} - {g.Status}");
        }

    }

    else if (secim == "4")
    {
        var sirali = games.OrderByDescending(g => g.Rating);
        foreach (Game g in sirali)
        {
            Console.WriteLine($"{g.Name} - {g.Rating}");
        }
    }

    else if (secim == "5")
    {
        var avg = games.Average(g => g.Rating);
        double sonuc = avg ?? 0;
        if (sonuc == 0)
        {
            Console.WriteLine("Henüz bir oyun puanlanmamıştır.");

        }
        else
        {
            Console.Write("Ortalama puan: ");
            Console.WriteLine(sonuc);
        }
    }

    else if (secim == "6")
    {
        Console.WriteLine("RAWG'a soruluyor...");
        await Task.Delay(2000);
        Console.WriteLine("Cevap geldi.");
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