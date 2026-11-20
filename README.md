# ASP.NET Core Eğitim Projesi

Bu depo, ASP.NET Core eğitiminde geliştirilen ECommerce API demo projesini içerir. Proje, işlenen konuları küçük ve incelenebilir örneklerle göstermek amacıyla hazırlanmıştır.

## Proje yapısı

- [ECommerce.slnx](ECommerce/ECommerce.slnx): Çözüm dosyası.
- [ECommerce.API](ECommerce/ECommerce.API): ASP.NET Core web uygulaması.
- [Program.cs](ECommerce/ECommerce.API/Program.cs): Uygulamanın oluşturulması, HTTP işlem hattı ve `/ping` uç noktası.
- [Middlewares](ECommerce/ECommerce.API/Middlewares): İstekleri günlüğe kaydeden ve sürelerini ölçen özel middleware sınıfları.

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

## Günlük güncelleme

Her eğitim gününden sonra son incelenen commit'ten itibaren yeni commit'ler ve ilgili kod değişiklikleri gözden geçirilerek **İşlenen konular** bölümüne tarihli bir başlık eklenir. İlgili gün için incelenen commit aralığı da belirtilir. Mevcut günlerin kayıtları korunur; bu işlem otomatik olarak zamanlanmamıştır.
