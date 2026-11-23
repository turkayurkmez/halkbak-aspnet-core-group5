# ASP.NET Core Eğitim Projesi

Bu depo, ASP.NET Core eğitiminde geliştirilen ECommerce API demo projesini içerir. Proje, işlenen konuları küçük ve incelenebilir örneklerle göstermek amacıyla hazırlanmıştır.

## Proje yapısı

- [ECommerce.slnx](ECommerce/ECommerce.slnx): Çözüm dosyası.
- [ECommerce.API](ECommerce/ECommerce.API): ASP.NET Core web uygulaması.
- [Program.cs](ECommerce/ECommerce.API/Program.cs): Uygulamanın oluşturulması, HTTP işlem hattı ve `/ping` uç noktası.
- [Middlewares](ECommerce/ECommerce.API/Middlewares): İstekleri günlüğe kaydeden ve sürelerini ölçen özel middleware sınıfları.
- [ProductsController.cs](ECommerce/ECommerce.API/Controllers/ProductsController.cs): Ürün ve mağaza bilgisi için örnek controller uç noktaları.
- [ShopOptions.cs](ECommerce/ECommerce.API/Options/ShopOptions.cs): `Shop` yapılandırma bölümünün tipli karşılığı.

## İşlenen konular

### 20 Kasım 2026

İncelenen commit'ler: `491a76d`–`58e1dad`.

1. **ASP.NET Core proje yapısı ve yapılandırma:** Web uygulaması ve çözüm dosyasının oluşturulması; `appsettings.json`, geliştirme ortamı ayarları ve çalıştırma profilleri.
2. **Uygulamanın başlatılması:** `WebApplication.CreateBuilder`, servislerin yapılandırılması ve `builder.Build()` ile uygulamanın oluşturulması.
3. **HTTP istek işlem hattı (pipeline):** Middleware'lerin `app.Use` ile tanımlanması, sıralanması ve isteğin `next` üzerinden sonraki adıma aktarılması.
4. **İstek ve yanıt günlüğü:** `HttpContext` ile istek metodu, yol ve yanıt durum kodunun okunması; `ILogger` ile yapılandırılmış günlükleme.
5. **İstek süresinin ölçülmesi:** `Stopwatch` kullanılarak sonraki işlem hattının tamamlanma süresinin milisaniye cinsinden ölçülmesi.
6. **Özel middleware sınıfları:** Satır içi middleware örneklerinin `RequestLoggingMiddleware` ve `RequestTimingMiddleware` sınıflarına taşınması; `UseMiddleware` ile işlem hattına eklenmesi.
7. **Minimal API uç noktası:** `MapGet` ve `Results.Ok` ile JSON yanıtı döndüren `/ping` uç noktası.

### 23 Kasım 2026

İncelenen commit'ler: `9145efd`–`2af4d58`.

1. **Merkezi hata yakalama:** `ExceptionHandlingMiddleware`, `await _next(context)` ile kendisinden sonraki middleware ve uç noktaları `try` içinde çalıştırır. İşlenmeyen bir hata oluştuğunda istek metodu ve yolu günlüğe yazılır; yanıt henüz başlamamışsa HTTP 500 ve `{"error":"Beklenmeyen bir hata meydana geldi"}` döndürülür. Yanıt başlamışsa middleware hatayı yeniden fırlatır; yeni bir hata yanıtı yazamaz. Mevcut günlükleme, exception nesnesini veya stack trace'i kaydetmez.
2. **Middleware sırası ve hata akışı:** İşlem hattında önce hata yakalama, sonra istek günlüğü ve süre ölçümü bulunur. Böylece dıştaki hata yakalayıcı sonraki adımlardaki hataları yakalayabilir. Hata fırlatıldığında günlükleme ve süre ölçümünün `await _next` sonrasındaki kodları çalışmaz; dolayısıyla bu istek için normal yanıt ve süre günlükleri oluşmaz.
3. **Controller tabanlı API:** `AddControllers()` controller servislerini ekler, `MapControllers()` uç noktaları eşler. `[Route("api/[controller]")]`, `ProductsController` için `api/products` yolunu oluşturur; `[HttpGet]` ve `[HttpGet("info")]` GET eylemlerini belirtir. `Ok(...)` HTTP 200 yanıtı üretir. Ürün uç noktası henüz gerçek ürün listesi değil, örnek bir mesaj döndürür.
4. **Options pattern ve dependency injection:** `Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SectionName))`, `Shop` bölümünü `ShopOptions` özelliklerine bağlar. Controller, `IOptions<ShopOptions>` bağımlılığını constructor üzerinden alır ve `options.Value` ile değerlere erişir. `Program.cs` ayrıca `builder.Configuration["Shop:Currency"]` ile tek bir ayarı doğrudan okuyup günlüğe yazar.
5. **Ortama özel yapılandırma ve çalıştırma profilleri:** Genel değerler `appsettings.json` dosyasındadır; `appsettings.Development.json` veya `appsettings.Production.json` içindeki aynı anahtarlar, seçilen ortama göre bunları geçersiz kılar. `http` profili Development ortamında 5001, `http-pro` profili Production ortamında 5002 portunu kullanır. Ortam değişkenleri gibi daha yüksek öncelikli yapılandırma kaynakları bu değerleri de değiştirebilir.
6. **Dev container:** [.devcontainer/devcontainer.json](.devcontainer/devcontainer.json), .NET 10 geliştirme imajını, NuGet paketleri için volume'u, çözümün restore edilmesini ve VS Code eklentilerini tanımlar. 5001 ve 7242 portları yapılandırmada yönlendirilir; `http-pro` için 5002 gerektiğinde ayrıca yönlendirilmelidir.

## Çalıştırma

Gereksinim: **.NET 10 SDK**.

Depo kökünden:

```powershell
dotnet run --project .\ECommerce\ECommerce.API\ECommerce.API.csproj --launch-profile http
```

Uygulama çalışırken başka bir terminalde:

```powershell
curl.exe http://localhost:5001/ping
```

Beklenen yanıt:

```json
{"durum":"Her şey yolunda"}
```

İstek ve süre günlükleri uygulamanın çalıştığı terminalde görülebilir. HTTP ve HTTPS adresleri [launchSettings.json](ECommerce/ECommerce.API/Properties/launchSettings.json) dosyasında tanımlıdır. [ECommerce.API.http](ECommerce/ECommerce.API/ECommerce.API.http) dosyası başlangıç şablonundan kalan farklı bir adres ve uç nokta içerir; güncel örnek için yukarıdaki `/ping` isteğini kullanın.

### Controller ve hata işleme örnekleri

`http` profili çalışırken:

```powershell
curl.exe http://localhost:5001/api/products
curl.exe http://localhost:5001/api/products/info
curl.exe -i http://localhost:5001/error
```

İlk istek `{"message":"Burada ürünler döndürülecek!"}` döndürür. İkinci isteğin varsayılan Development ayarlarıyla yanıtı:

```json
{"currency":"TRY","maxOrderItems":20,"storeName":"ECommerce [Development]"}
```

`/error` bilerek `NotImplementedException` fırlatır; hata middleware'i HTTP 500 ve yukarıdaki genel hata mesajını döndürür. Terminalde hata günlüğünü ve başarılı isteklerdeki süre günlüklerini karşılaştırın.

Ortamın etkisini görmek için uygulamayı durdurup Production profiliyle yeniden başlatın:

```powershell
dotnet run --project .\ECommerce\ECommerce.API\ECommerce.API.csproj --launch-profile http-pro
```

Başka bir terminalde `curl.exe http://localhost:5002/api/products/info` çalıştırın. Varsayılan ayarlarda `storeName` değeri `ECommerce [Production]` olur. Bu demo uç noktası yapılandırmayı istemciye açar; gerçek uygulamalarda hassas yapılandırma değerleri yanıtlarda paylaşılmamalıdır.

## Günlük güncelleme

Her eğitim gününden sonra son incelenen commit'ten itibaren yeni commit'ler ve ilgili kod değişiklikleri gözden geçirilerek **İşlenen konular** bölümüne tarihli bir başlık eklenir. İlgili gün için incelenen commit aralığı da belirtilir. Mevcut günlerin kayıtları korunur; bu işlem otomatik olarak zamanlanmamıştır.
