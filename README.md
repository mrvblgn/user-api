# Senswise User Service API

Kullanıcı yönetimi için Clean Architecture prensiplerine uygun olarak geliştirilmiş RESTful API servisi.

## 🎯 Genel Bakış

Bu proje kullanıcı ekleme, güncelleme, silme ve listeleme işlemlerini gerçekleştiren bir Web API'dir. CQRS pattern, Entity Framework Core ve PostgreSQL veritabanı kullanılarak geliştirilmiştir.

## 🏗️ Teknolojiler

- **.NET Core 6.0**
- **PostgreSQL** - Veritabanı
- **Entity Framework Core 6.0** - ORM (Code-First)
- **MediatR** - CQRS implementasyonu
- **FluentValidation** - Doğrulama
- **Swagger/OpenAPI** - API dokümantasyonu

## 📋 Özellikler

### API Endpoint'leri

| Method | Endpoint | Açıklama |
|--------|----------|----------|
| `GET` | `/api/users` | Tüm kullanıcıları listeler |
| `GET` | `/api/users/{id}` | ID'ye göre kullanıcı getirir |
| `POST` | `/api/users` | Yeni kullanıcı oluşturur |
| `PUT` | `/api/users/{id}` | Kullanıcı bilgilerini günceller |
| `DELETE` | `/api/users/{id}` | Kullanıcıyı siler |

### Kullanıcı Alanları

- **Ad** (FirstName) - Zorunlu, Max 100 karakter
- **Soyad** (LastName) - Zorunlu, Max 100 karakter
- **E-posta** (Email) - Zorunlu, Geçerli email formatı, Max 255 karakter
- **Adres** (Address) - Opsiyonel, Max 500 karakter

### Mimari

- **Clean Architecture** - Katmanlı mimari
- **CQRS Pattern** - Command/Query ayrımı
- **Repository Pattern** - Veri erişim soyutlaması
- **Validation Pipeline** - Merkezi doğrulama
- **Global Exception Handling** - Merkezi hata yönetimi
- **Standardized API Response** - Tutarlı yanıt formatı

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/downloads)

### 1. Projeyi İndirin

```bash
git clone https://github.com/mrvblgn/user-api.git
cd user-api
```

### 2. Veritabanı Kurulumu

PostgreSQL'in çalıştığından emin olun ve bir veritabanı oluşturun:

```sql
CREATE DATABASE SenswiseUserDb;
```

### 3. Bağlantı Ayarları

`appsettings.json` dosyasındaki veritabanı bağlantı bilgilerini güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SenswiseUserDb;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

### 4. Veritabanı Migration

```bash
dotnet ef database update
```

### 5. Uygulamayı Başlatın

```bash
dotnet run
```

Uygulama başarıyla başladığında:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5266
```

## 📖 Swagger/API Dokümantasyonu

Uygulama çalıştıktan sonra Swagger UI'a aşağıdaki adresten erişebilirsiniz:

**🌐 http://localhost:5266/swagger**

Swagger UI üzerinden:
- Tüm endpoint'leri görebilir
- API'yi interaktif olarak test edebilir
- Request/Response şemalarını inceleyebilirsiniz

### Swagger Üzerinden Test Etme

1. Tarayıcıda `http://localhost:5266/swagger` adresine gidin
2. Test etmek istediğiniz endpoint'e tıklayın
3. "Try it out" butonuna basın
4. Gerekli parametreleri doldurun
5. "Execute" butonuna tıklayın
6. Response'u görüntüleyin

## 📝 API Kullanım Örnekleri

### 1. Kullanıcı Oluşturma

```bash
curl -X POST "http://localhost:5266/api/Users" \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "Ahmet",
    "lastName": "Yılmaz",
    "email": "ahmet@example.com",
    "address": "Ankara"
  }'
```

**Response:**
```json
{
  "success": true,
  "message": "Kullanıcı başarıyla oluşturuldu.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "Ahmet",
    "lastName": "Yılmaz",
    "email": "ahmet@example.com",
    "address": "Ankara",
    "createdAt": "2026-02-23T19:00:00Z"
  },
  "errors": null
}
```

### 2. Tüm Kullanıcıları Listeleme

```bash
curl -X GET "http://localhost:5266/api/Users"
```

### 3. Kullanıcı Güncelleme

```bash
curl -X PUT "http://localhost:5266/api/Users/{id}" \
  -H "Content-Type: application/json" \
  -d '{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "Mehmet",
    "lastName": "Yılmaz",
    "email": "mehmet@example.com",
    "address": "İstanbul"
  }'
```

### 4. Kullanıcı Silme

```bash
curl -X DELETE "http://localhost:5266/api/Users/{id}"
```

## 🔒 Validasyon Kuralları

- **Ad**: Zorunlu, boş bırakılamaz, maksimum 100 karakter
- **Soyad**: Zorunlu, boş bırakılamaz, maksimum 100 karakter
- **E-posta**: Zorunlu, geçerli email formatı, maksimum 255 karakter
- **Adres**: Opsiyonel, maksimum 500 karakter

### Hata Response Örneği

```json
{
  "success": false,
  "message": "Doğrulama başarısız",
  "data": null,
  "errors": [
    "Ad alanı zorunludur.",
    "Geçerli bir e-posta adresi giriniz."
  ]
}
```

## 📂 Proje Yapısı

```
Senswise.UserService/
├── Application/              # CQRS - Commands & Queries
│   ├── Behaviors/           # MediatR Behaviors
│   └── Features/
│       └── Users/
│           ├── Commands/    # Create, Update, Delete
│           └── Queries/     # GetAll, GetById
├── Core/                    # Domain Layer
│   ├── Common/             # Base Entity, ApiResponse
│   ├── Entities/           # User Entity
│   └── Interfaces/         # Abstractions
├── Infrastructure/          # Data Access Layer
│   ├── Configurations/     # EF Core Configurations
│   ├── Middleware/         # Global Exception Handler
│   └── Persistence/        # DbContext
├── Controllers/            # API Controllers
└── Program.cs             # Application Entry Point
```

## 🛠️ Geliştirme

### Migration Oluşturma

```bash
dotnet ef migrations add MigrationName
```

### Migration Uygulama

```bash
dotnet ef database update
```

### Veritabanını Sıfırlama

```bash
dotnet ef database drop -f
dotnet ef database update
```

### Projeyi Build Etme

```bash
dotnet build
```

### Testler

```bash
dotnet test
```

## 🎨 Response Formatı

Tüm API yanıtları standart bir format kullanır:

```json
{
  "success": true/false,
  "message": "İşlem mesajı",
  "data": { ... },
  "errors": ["hata1", "hata2"]
}
```

## 🔧 Yapılandırma

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SenswiseUserDb;Username=postgres;Password=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📦 NuGet Paketleri

- Microsoft.EntityFrameworkCore (6.0.25)
- Microsoft.EntityFrameworkCore.Design (6.0.25)
- Npgsql.EntityFrameworkCore.PostgreSQL (6.0.22)
- MediatR (12.2.0)
- FluentValidation (11.9.0)
- FluentValidation.DependencyInjectionExtensions (11.9.0)
- Swashbuckle.AspNetCore (6.5.0)

## 🐛 Sorun Giderme

### Port Zaten Kullanımda

```bash
# macOS/Linux
lsof -ti:5266 | xargs kill -9

# Windows
netstat -ano | findstr :5266
taskkill /PID <PID> /F
```

### Migration Hataları

```bash
dotnet ef migrations remove
dotnet ef database drop -f
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📞 İletişim

Sorularınız için:
- GitHub: [https://github.com/mrvblgn/user-api](https://github.com/mrvblgn/user-api)
- Email: merve@senswise.com

## 📄 Lisans

Bu proje Senswise için geliştirilmiştir.

---

**Son Güncelleme:** 23 Şubat 2026
