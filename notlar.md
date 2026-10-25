# Loadout – Öğrenme Notlarım

Bu dosya, Loadout projesi boyunca öğrendiklerimin kaydı. Aşama 0: C# ısınması (konsol projesi).

---

## Gün 1: Kurulum ve çalışma düzeni

**.NET SDK:** C# geliştirme kiti. İçinde derleyici, çalışma ortamı (runtime), `dotnet` komutu ve proje şablonları var. SDK, runtime'ı da kapsar. Sunucuya genelde sadece runtime kurulur, geliştirici makinesine SDK.

**C# derlenir:** Python kodu çalışırken yorumlanır, C# kodu önce derlenir sonra çalışır. Yazım ve tip hatalarını derleyici program başlamadan yakalar. Mantık hatalarını (yanlış formül gibi) yakalamaz.

**Git ve GitHub farkı:** Git, bilgisayarımdaki sürüm kontrol aracı. GitHub, git depolarını internette tutan servis.

**Git'in üç adımı:**
```
git add .      → değişiklikleri sepete koy (bilgisayarımda)
git commit -m  → sepetin kaydını al, mesaj yaz (bilgisayarımda)
git push       → kayıtları GitHub'a gönder (internete)
```
`commit` GitHub'a göndermez, sadece yerel geçmişe kayıt ekler. Göndermek için `push` lazım.

**Kullandığım komutlar:**
- `git clone <adres>`: repoyu bilgisayara indirir
- `cd klasör` / `cd ..`: klasöre gir / bir üste çık. Komutlar bulunduğum klasöre göre çalışır.
- `dotnet new gitignore`: .NET için hazır `.gitignore` üretir
- `dotnet new console -n LoadoutKonsol`: konsol projesi açar
- `dotnet run`: derler ve çalıştırır
- `git push -u origin HEAD`: ilk gönderimde dalı bağlar, sonra düz `git push` yeter

**.gitignore:** GitHub'a gitmeyecek dosyaların listesi. `bin/` ve `obj/` derleme çıktısı, kodum değil, her bilgisayarda yeniden oluşuyor. Kural: GitHub'a sadece benim yazdıklarım gider, üretilenler gitmez.

**Proje yapısı:** `loadout/` klasörü bütün projenin çatısı. Her .NET projesi kendi klasöründe ve kendi `.csproj` dosyasıyla durur. İleride `backend/`, `frontend/` gibi klasörler aynı repoda olacak.

**Program.cs ve .csproj:** `Program.cs` programın giriş noktası. `.csproj` projenin ayar dosyası (hangi .NET sürümü, hangi paketler).

---

## Gün 2: Enum, sınıf, liste

**Enum:** Önceden belirlenmiş sabit seçenekler listesi. Metin yerine kullanılır, çünkü metinde `"Bitirdim"` ile `"bitirdim"` farklı sayılır ve hata fark edilmez. Enum'da listede olmayan değer yazarsam derleyici hemen itiraz eder. Formdaki açılır liste (`<select>`) mantığının kod içindeki hali.

```csharp
enum Status { Planned, Playing, Completed, Dropped }
Status durum = Status.Playing;
```

Arkada her değer sırayla bir sayıdır (Planned = 0, Playing = 1...). Veritabanına kaydederken önemli olacak.

**C#'ta tip önce yazılır:** `Status durum = ...`. Yanlış tipte değer atarsam derleyici izin vermez.

**Sınıf (class):** Bir şeyin kalıbı. Kalıptan istediğim kadar nesne üretirim.

```csharp
class Game
{
    public string Name { get; set; } = "";
    public Status Status { get; set; }
}
```

- `public`: dışarıdan erişilebilir
- `{ get; set; }`: değeri okuyabilirim ve değiştirebilirim
- `= ""`: başlangıç değeri, null uyarısını önler
- `Status Status`: özelliğin adı ve tipi aynı olabilir, normal

**Liste:** `List<Game>` sadece Game tutabilen liste. Python'daki `[]` gibi.

```csharp
List<Game> games = new List<Game>();
games.Add(new Game { Name = "Gece Yolcusu", Status = Status.Playing });
foreach (Game game in games) { ... }
```

- `.Add(...)` = Python'daki `.append(...)`
- `new Game { Name = ..., Status = ... }`: nesneyi oluşturup özelliklerini tek seferde doldurur
- `foreach (Game g in games)` = Python'daki `for g in games`

---

## Gün 3: Konsol menüsü

**Menü döngüsü:** `while (true)` ile program ben "çıkış" diyene kadar açık kalır. `break` döngüden çıkarır.

**ReadLine:** `Console.ReadLine()` = Python'daki `input()`. `Console.Write` satır sonuna geçmez, `WriteLine` geçer.

**`string?`:** ReadLine bazen "hiçbir şey" (null) dönebilir, bu yüzden tip `string?`.

**if / else if / else:** Python'daki `if / elif / else`. Koşul parantez içinde, gövde süslü parantez içinde.

**String interpolation:** `$"{game.Name} - {game.Status}"` Python'daki f-string. Süslü parantez içindeki şey hesaplanıp yerine konur.

**Yaptığım hatalar ve dersler:**
1. `{"game.Name"}` yazdım, tırnak içindeki şey metin sayılır, ekrana harfi harfine `game.Name` çıktı. Doğrusu `{game.Name}`.
2. Son `else`'e `break` koymuştum, yanlış seçimde program kapanıyordu. `0` için ayrı `else if` açıp `break`'i oraya taşıdım, `else`'te sadece mesaj yazdırdım.
3. Oyun listesini döngünün dışında oluşturmalıyım, içinde olsaydı her turda sıfırlanırdı.

---

## Gün 4: LINQ ve Where

**LINQ:** Listelerde filtreleme, sıralama, hesaplama için hazır metotlar.

**Where:** Koşula uyanları seçer. Python'daki liste kapsamının (list comprehension) karşılığı.

```csharp
var oynananlar = games.Where(g => g.Status == Status.Playing);
```

- `g => ...` bir **lambda**: kısa, isimsiz fonksiyon. `g` her oyun için kullandığım geçici ad, istediğim adı verebilirim.
- `var`: "tipi derleyici bulsun" demek.

---

## Gün 5: Puan, sıralama, ortalama

**`double?`:** Ondalıklı sayı ve boş olabilir. Puanı henüz verilmemiş oyunun puanı 0 değil, "yoktur". Aksi halde "puanlanmamış" ile "0 puan" ayırt edilemez.

```csharp
public double? Rating { get; set; }
```

**OrderByDescending:** Büyükten küçüğe sıralar. Python'daki `sorted(..., reverse=True)`. Puanı boş olanlar en sona düşer.

```csharp
var sirali = games.OrderByDescending(g => g.Rating);
```

**Average:** Ortalamayı hesaplar, boş olanları atlar. Hiçbir oyun puanlanmamışsa sonuç da boştur (null).

```csharp
var ortalama = games.Average(g => g.Rating);
```

---

## Gün 6: Null ve async/await

**Null:** "Değer yok" demek. Boş olabilen bir değerle çalışırken boş durumunu ele almak gerekir.

```csharp
if (ortalama == null) { Console.WriteLine("Henüz puanlanmış oyun yok."); }
```

**`??` operatörü:** "Boşsa şunu kullan". `double sonuc = ortalama ?? 0;` Python'daki `x if x is not None else 0` gibi.

**async/await:** İnternetten veri çekmek, veritabanına sormak gibi yavaş işler sırf beklemekten ibarettir. Normal kodda program bekleme boyunca durur. `await`, "bu işi başlat, cevap gelene kadar iş parçacığını başkasına bırak, cevap gelince devam et" der. Web API'de çok kullanıcılı çalışmak için şart. Python'daki `await asyncio.sleep()` gibi.

```csharp
await Task.Delay(2000);   // 2 saniyelik sahte yavaş iş
```

`Program.cs` içinde `await` doğrudan kullanılabilir. Başka bir metotta kullanmak için metoda `async` yazılır ve dönüş tipi `Task` olur (Web API'de görülecek).

---

## Python → C# eşleşmeleri

| Python | C# |
|---|---|
| `print()` | `Console.WriteLine()` |
| `input()` | `Console.ReadLine()` |
| `f"{x}"` | `$"{x}"` |
| `[]`, `.append()` | `List<T>`, `.Add()` |
| `for x in liste:` | `foreach (T x in liste)` |
| `elif` | `else if` |
| `enum.Enum` | `enum` |
| `None` | `null` |
| `x if x else y` | `x ?? y` |
| liste kapsamı `[g for g in ...]` | `.Where(g => ...)` |
| `sorted(..., reverse=True)` | `.OrderByDescending(...)` |
| `await asyncio.sleep()` | `await Task.Delay()` |

---

---

## Gün 7: Docker

**Docker:** Bir programı kendi izole kutusunda (konteyner) çalıştırır. Kurulum ve ayarlar kutunun içinde kalır, bilgisayarımı kirletmez, kutuyu silip sıfırdan başlayabilirim. Aynı kutu başka bir bilgisayarda da aynen çalışır, bu yüzden ilanlarda sık geçiyor. Python'daki `venv` fikrinin çok daha kapsamlısı: sadece paketleri değil, programın bütün ortamını izole eder.

```
docker --version          → Docker kurulu mu
docker run hello-world    → test kutusunu indirip çalıştırır
```

`hello-world` çıktısını görmek Docker'ın çalıştığını kanıtladı. Kurulumda Docker Desktop açık olmalı, yoksa komutlar hata verir.

---

## Gün 8: Gizli bilgiler (.env)

**.env dosyası:** Şifre gibi gizli bilgileri tutar. Her satır `ANAHTAR=değer` biçiminde, boşluk ve tırnak yok. Kod ve `docker-compose.yml` bu değerleri `.env`'den okur, böylece şifre dosyaların içine yazılmaz.

**.gitignore'a `.env` eklemek:** Git'e "bu dosyayı GitHub'a gönderme" demek. GitHub'da `.env`'nin görünmemesi hata değil, doğru çalıştığının kanıtı. Kontrol: `.env` bilgisayarımda var ama `git status` temiz diyorsa git onu yok sayıyor.

**Commit ile push farkı (tekrar):** `git status` içindeki "ahead of origin/main by 1 commit" uyarısı, commit'in yerelde olduğunu ama GitHub'a gönderilmediğini söyler. Çözüm `git push`.

---

**Image ve container:** Image (imaj), hazır paketlenmiş kalıp, bir sınıf (class) gibi. Container (konteyner), o imajdan başlatılmış çalışan kutu, bir nesne gibi. `postgres:17` PostgreSQL'in 17 sürümünün imajı. Bir kez indirilir, ondan istediğim kadar kutu başlatılabilir.

**Docker Compose:** Kutuların tarifini tek bir dosyada (`docker-compose.yml`) yazıp tek komutla ayağa kaldırmak.

**docker-compose.yml satırları:**
- `image`: hangi imajdan üretileceği
- `environment`: kutuya verilen ayarlar. `${...}` yazmak değeri `.env`'den okur, şifre dosyaya yazılmaz
- `ports: "5432:5432"`: soldaki bilgisayarımın kapısı, sağdaki kutunun içindeki kapı. Uygulamam `localhost:5432` ile kutudaki PostgreSQL'e ulaşır
- `volumes`: veri kutunun dışında saklanır. Yazmazsam kutuyu silince veritabanı da silinir

**YAML'da girinti anlam taşır, tab kullanılmaz.**

**Komutlar:**
```
docker compose up -d     → kutuyu arka planda başlat
docker ps                → çalışan kutuları listele (STATUS: Up olmalı)
docker compose down      → kutuyu durdur (veri volume'da kalır)
```
`docker compose down -v` yazmamalıyım, `-v` volume'u yani bütün veritabanını siler.

---

## Gün 9: DBeaver ile PostgreSQL'e bağlanmak

**DBeaver:** Veritabanının içine gözle bakmamı sağlayan arayüz. Flask projelerindeki DB Browser'ın PostgreSQL karşılığı.

**Bağlantı bilgileri nereden geliyor:**
- Host `localhost`: kutu benim bilgisayarımda çalışıyor
- Port `5432`: `docker-compose.yml` içindeki `ports` satırı
- Database, Username, Password: `.env` dosyasındaki değerler

**SQL komutları (PostgreSQL):**
```sql
CREATE TABLE deneme (id SERIAL PRIMARY KEY, ad TEXT);   -- tablo oluştur
INSERT INTO deneme (ad) VALUES ('Gece Yolcusu');        -- satır ekle
SELECT * FROM deneme;                                   -- tabloyu getir
DROP TABLE deneme;                                      -- tabloyu sil
```

`SERIAL`, PostgreSQL'de kendiliğinden artan numara demek (SQLite'taki `AUTOINCREMENT`'in karşılığı). İleride bu tabloları elle yazmayacağım, EF Core benim yerime üretecek.

**Çalıştırma:** komut satırında imleç varken `Ctrl+Enter`. Liste eskiyse sol panelde `F5` ile yenilenir.

---

## Gün 10: İlk Web API

**Web API:** Flask'taki `@app.route` ile aynı fikir: adrese istek gelir, fonksiyon çalışır, cevap döner. Fark, cevabın HTML değil **JSON** olması. HTML'i ileride React çizecek, API sadece veri verecek.

**Flask ve C# eşleşmesi:**

| Flask | C# |
|---|---|
| `@app.route("/games")` | `[Route("[controller]")]` |
| `methods=["GET"]` | `[HttpGet]` |
| `def get():` | `public List<string> Get()` |
| `return jsonify(...)` | `return ...` (JSON'a çevirmeyi ASP.NET yapar) |

**Köşeli parantezler `[...]` (attribute):** Python'daki decorator'ların karşılığı. Altındaki şeye özellik ekler. `[HttpGet]` = "bu metot GET isteğine cevap verir", `[ApiController]` = "bu sınıf bir API controller'ı".

**Controller:** İlgili uç noktaların toplandığı sınıf. `GamesController` içinde oyunla ilgili adresler olacak (listele, ekle, sil).

**`[controller]` kısayolu:** Sınıfın adından "Controller" atılınca kalan kısım adres olur. `GamesController` → `/games`.

**Komutlar:**
```
dotnet new webapi --use-controllers -n LoadoutApi -o backend   → API projesi aç
dotnet run                                                      → sunucuyu başlat
Ctrl+C                                                          → sunucuyu durdur
```

**Sunucu çalışırken terminal meşgul kalır:** `Now listening on: http://localhost:5099` yazısı sunucunun çalıştığını gösterir (Flask'taki `app.run()` gibi). Port numarası kendi ekranımdaki yazıdan alınır.

**Swagger konusu:** .NET 9'dan beri Web API şablonunda Swagger arayüzü hazır gelmiyor, sadece OpenAPI belgesi (`/openapi/v1.json`) üretiliyor. Test arayüzü için Scalar kullanacağım.

---

## Gün 11: Scalar ve HTTP istek-cevap döngüsü

**Scalar:** API'nin uç noktalarını bir arayüzde listeleyen, tıklayıp deneme yapmamı sağlayan araç. POST gibi adres çubuğundan deneyemeyeceğim istekleri test etmek için lazım.

**OpenAPI ve Scalar farkı:**
- `app.MapOpenApi()`: API'nin menüsünü (hangi adresler var) belge olarak yayınlar, `/openapi/v1.json`
- `app.MapScalarApiReference()`: o belgeyi okuyup arayüz çizer, `/scalar/v1`

**Bir isteğin yolculuğu (Send'e basınca):**
1. Scalar API'ye HTTP isteği yollar (`GET /games`). Adres çubuğuna yazmakla aynı şey.
2. İstek benim bilgisayarımda kalır (`localhost` = bu bilgisayar, `5099` = API'nin portu).
3. ASP.NET yola ve metoda bakıp `[Route]` ve `[HttpGet]` ile doğru metodu bulur.
4. Metot çalışır, liste döner.
5. ASP.NET listeyi JSON'a çevirir.
6. Cevap durum koduyla (200) ve JSON gövdesiyle döner.
7. Scalar cevabı düzenli gösterir.

**JSON:** Sadece metin. Python liste/sözlük yazımına benzer: çift tırnak, `true/false/null` küçük harf, sonda fazladan virgül yok. Tarayıcı ve React gibi programlar bu metni kendi veri yapılarına çevirir.

**HTTP durum kodları:** 200 başarılı, 400 hatalı istek, 401 giriş yapılmamış, 404 bulunamadı, 500 sunucu hatası.

**Kod değişince API'yi yeniden başlatmam gerekir:** C# derlenir, değişiklik yeni derlemede görünür (`Ctrl+C`, sonra `dotnet run`).

**Paket ekleme:** `dotnet add package Scalar.AspNetCore` (Python'daki `pip install`'ın karşılığı, paket `.csproj` dosyasına yazılır).

---

## Gün 12: Model sınıfları ve nesne döndürmek

**Model:** Veriyi temsil eden sınıf (oyun, kullanıcı). `Models/` klasöründe tutulur, controller'lar sadece istekleri karşılar.

**Namespace (`namespace LoadoutApi.Models;`):** Sınıfın hangi gruba ait olduğunu söyler. Başka bir namespace'teki sınıfı kullanmak için `using LoadoutApi.Models;` yazılır.

**`public`:** Controller'ın döndürdüğü tip `public` olmak zorunda. Eksikse "Inconsistent accessibility" hatası gelir, çünkü `public` olmayan sınıf sadece kendi projesinde görünür.

**`Id`:** Veritabanında her satırın kendine ait numarası. Oyunu bulmak, güncellemek ve silmek için gerekli, konsolda buna ihtiyaç yoktu.

**Nesne döndürmek:** Metodun dönüş tipi `List<Game>` olunca ASP.NET her oyunu JSON nesnesine çevirir.

**JSON'a çevirirken olanlar:**
- Alan adları otomatik küçük harfle başlar (`Name` → `name`)
- Boş olabilen değer `null` olur (`"rating": null`)
- Enum varsayılan olarak **sayı** çıkar (`Playing` → `1`), çünkü arkada her enum değeri bir sayıdır. Bu, API'yi okuyan biri için anlamsız