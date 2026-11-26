# ASP.NET Core Eğitim Projesi

Bu depo, ASP.NET Core eğitiminde geliştirilen ECommerce API projesini ve SOLID ilkelerini gösteren bir konsol demosunu içerir. Projeler, işlenen konuları küçük ve incelenebilir örneklerle göstermek amacıyla hazırlanmıştır.

## Proje yapısı

- [ECommerce.slnx](ECommerce/ECommerce.slnx): Çözüm dosyası.
- [ECommerce.API](ECommerce/ECommerce.API): ASP.NET Core web uygulaması.
- [ECommerce.Application](ECommerce/ECommerce.Application): Bellek içi ürün işlemlerini sağlayan uygulama katmanı.
- [ECommerce.Domain](ECommerce/ECommerce.Domain): Ürün varlığını içeren domain katmanı.
- [Demo.SOLID](Demo/Demo.SOLID): SRP, OCP, LSP ve ISP örneklerini içeren konsol uygulaması.
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
3. **Controller tabanlı API:** `AddControllers()` controller servislerini ekler, `MapControllers()` uç noktaları eşler. `[Route("api/[controller]")]`, `ProductsController` için `api/products` yolunu oluşturur; `[HttpGet]` ve `[HttpGet("info")]` GET eylemlerini belirtir. O tarihte ürün uç noktası örnek bir mesaj döndürüyordu; güncel davranış aşağıda açıklanmıştır.
4. **Options pattern ve dependency injection:** `Configure<ShopOptions>(builder.Configuration.GetSection(ShopOptions.SectionName))`, `Shop` bölümünü `ShopOptions` özelliklerine bağlar. Controller, `IOptions<ShopOptions>` bağımlılığını constructor üzerinden alır ve `options.Value` ile değerlere erişir. `Program.cs` ayrıca `builder.Configuration["Shop:Currency"]` ile tek bir ayarı doğrudan okuyup günlüğe yazar.
5. **Ortama özel yapılandırma ve çalıştırma profilleri:** Genel değerler `appsettings.json` dosyasındadır; `appsettings.Development.json` veya `appsettings.Production.json` içindeki aynı anahtarlar, seçilen ortama göre bunları geçersiz kılar. `http` profili Development ortamında 5001, `http-pro` profili Production ortamında 5002 portunu kullanır. Ortam değişkenleri gibi daha yüksek öncelikli yapılandırma kaynakları bu değerleri de değiştirebilir.
6. **Dev container:** [.devcontainer/devcontainer.json](.devcontainer/devcontainer.json), .NET 10 geliştirme imajını, NuGet paketleri için volume'u, çözümün restore edilmesini ve VS Code eklentilerini tanımlar. 5001 ve 7242 portları yapılandırmada yönlendirilir; `http-pro` için 5002 gerektiğinde ayrıca yönlendirilmelidir.

### 24 Kasım 2026

İncelenen commit'ler: `6c9f047`–`c3adc68`.

1. **Katmanlı yapı ve ürün modeli:** API, Application katmanına; Application da Domain katmanına referans verir. `Product`, kimlik, ad, SKU, fiyat, stok ve kategori kimliği alanlarını içerir.
2. **Ürün servisi ve dependency injection:** `ProductService`, başlangıçta üç ürün içeren bellek içi bir liste kullanır. `AddSingleton<ProductService>()` ile uygulama boyunca aynı servis örneği kullanılır; veriler kalıcı değildir ve uygulama yeniden başladığında sıfırlanır. Servisteki `Create` metodu listeye ürün ekler, ancak bunun için henüz bir HTTP uç noktası yoktur.
3. **Ürün uç noktaları:** `GET /api/products` ürün listesini, `GET /api/products/{id}` ise eşleşen ürünü döndürür. Eşleşme yoksa servis `null` döndürür; controller özel bir 404 yanıtı oluşturmamaktadır. Metotlar asenkron imzalıdır ancak örnekte gerçek bir asenkron veri erişimi yapılmaz.
4. **OpenAPI ve Scalar:** Development ortamında OpenAPI belgesi `/openapi/v1.json` adresinde, Scalar arayüzü `/scalar/v1` adresinde sunulur. Bu uç noktalar Production ortamında eşlenmez.
5. **SOLID — Tek Sorumluluk İlkesi (SRP):** [SRP.cs](Demo/Demo.SOLID/SRP.cs), ürün oluşturma, e-posta ve ekran sorumluluklarının tek sınıfta toplanmasını, bu görevlerin ayrı sınıflara bölündüğü örnekle karşılaştırır. İşlemler açıklayıcı taslaklardır; gerçek veritabanı veya e-posta işlemi yapılmaz.
6. **SOLID — Açık/Kapalı İlkesi (OCP):** [OCP.cs](Demo/Demo.SOLID/OCP.cs) içinde `PaymentSystemAfter`, indirim hesabını müşterinin `CardType` nesnesine devreder. `Standard`, `Silver`, `Gold` ve `Premium` sırasıyla %5, %10, %15 ve %20 indirim uygular. Yeni bir kart türü, ödeme sistemindeki hesaplama kodu değiştirilmeden `CardTypes` sınıfından türetilebilir.

### 25 Kasım 2026

İncelenen commit'ler: `39988cc`–`eb4e409`.

1. **Sipariş oluşturma ve sorumlulukların ayrılması (SRP):** `POST /api/orders`, `OrdersController` üzerinden `OrderService.CreateOrder` metodunu çağırır. Servis siparişin oluşturulduğunu varsayarak `NotificationService`'i çağırır; `NotificationService` kanallara dağıtım yapar, her kanal kendi gönderim davranışını ve günlüğünü yönetir. Veritabanına gerçek kayıt yapılmaz.
2. **Bildirim kanalları (OCP ve DIP):** `NotificationService`, somut e-posta/SMS/WhatsApp sınıfları yerine `INotificationChannel` soyutlamasına bağımlıdır ve DI ile verilen kanalların tümüne sırayla bildirim gönderir. Yeni bir kanal bu sözleşmeyi uygulayıp DI kaydına eklenerek dağıtım servisi değiştirilmeden eklenebilir. Mevcut kanallar gerçek bir sağlayıcıya gönderim yapmaz; tamamlanmış görev günlüğü yazar.
3. **SOLID — Liskov'un Yerine Geçme İlkesi (LSP):** [LSP.cs](Demo/Demo.SOLID/LSP.cs), `Rectangle` ve `Square` türlerini ortak `IArea` arayüzüyle kullanır; `Square`, `Rectangle` alt sınıfı değildir. Böylece kare kenarını değiştirirken genişlik/yükseklik davranışını beklenmedik biçimde değiştiren klasik kalıtım sorunu önlenir. `Geometry.GetRectangle`, ölçü argümanlarına göre bu iki uygulamadan birini `IArea` olarak döndürür.
4. **SOLID — Arayüz Ayırma İlkesi (ISP):** [ISP.cs](Demo/Demo.SOLID/ISP.cs), temel işlemleri (`IMath`) ileri işlemlerden (`IAdvancedMath`) ayırır. `SimpleMath` yalnızca temel işlemleri uygulayarak kullanmadığı metotlara bağımlı olmaz. `ComplexMath` her iki arayüzü uygular ancak metotları şu an `NotImplementedException` fırlatır; tamamlanmış bir matematik uygulaması değildir.
5. **Fax kanalı taslağı:** `c97a73c` commit mesajındaki fax kanalı uygulaması mevcut kaynakta etkin değildir; fax kodu yorum satırına alınmıştır. Bu nedenle çalışan bildirim kanalları yalnızca e-posta, SMS ve WhatsApp'tır.

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
curl.exe http://localhost:5001/api/products/1
curl.exe http://localhost:5001/api/products/info
curl.exe -i http://localhost:5001/error
```

İlk istek başlangıçtaki üç ürünün listesini döndürür. İkinci isteğin yanıtı:

```json
{"id":1,"name":"Ürün A","sku":"Test-001","price":100,"stock":100,"categoryId":1}
```

Üçüncü isteğin varsayılan Development ayarlarıyla yanıtı:

```json
{"currency":"TRY","maxOrderItems":20,"storeName":"ECommerce [Development]"}
```

`/error` bilerek `NotImplementedException` fırlatır; hata middleware'i HTTP 500 ve yukarıdaki genel hata mesajını döndürür. Terminalde hata günlüğünü ve başarılı isteklerdeki süre günlüklerini karşılaştırın.

Ortamın etkisini görmek için uygulamayı durdurup Production profiliyle yeniden başlatın:

```powershell
dotnet run --project .\ECommerce\ECommerce.API\ECommerce.API.csproj --launch-profile http-pro
```

Başka bir terminalde `curl.exe http://localhost:5002/api/products/info` çalıştırın. Varsayılan ayarlarda `storeName` değeri `ECommerce [Production]` olur. Bu demo uç noktası yapılandırmayı istemciye açar; gerçek uygulamalarda hassas yapılandırma değerleri yanıtlarda paylaşılmamalıdır.

### SOLID demosu

```powershell
dotnet run --project .\Demo\Demo.SOLID\Demo.SOLID.csproj
```

Konsol uygulaması `Hello, World!` yazdıktan sonra Premium kartla 100 üzerinden hesaplanan indirimli fiyatı (`80`) gösterir.

## Günlük güncelleme

Her eğitim gününden sonra son incelenen commit'ten itibaren yeni commit'ler ve ilgili kod değişiklikleri gözden geçirilerek **İşlenen konular** bölümüne tarihli bir başlık eklenir. İlgili gün için incelenen commit aralığı da belirtilir. Mevcut günlerin kayıtları korunur; bu işlem otomatik olarak zamanlanmamıştır.
