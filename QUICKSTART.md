# 🚀 Hızlı Başlangıç Kılavuzu

Bu dokümanda projeyi 5 dakikada nasıl çalıştıracağınız anlatılmaktadır.

## ✅ Adım 1: Gerekli Yazılımlar

Aşağıdaki yazılımların yüklü olduğundan emin olun:

- ✅ .NET 6.0 SDK - https://dotnet.microsoft.com/download/dotnet/6.0
- ✅ PostgreSQL 12+ - https://www.postgresql.org/download/
- ✅ Git - https://git-scm.com/downloads

**Kontrol için:**
```bash
dotnet --version     # 6.0.x görmelisiniz
psql --version       # PostgreSQL 12+ görmelisiniz
```

## ✅ Adım 2: Projeyi İndirin

```bash
git clone https://github.com/mrvblgn/user-api.git
cd user-api
```

## ✅ Adım 3: Veritabanı Oluşturun

PostgreSQL'e bağlanın ve veritabanı oluşturun:

```bash
# PostgreSQL'e bağlanın
psql -U postgres

# Veritabanını oluşturun
CREATE DATABASE "SenswiseUserDb";

# Çıkış yapın
\q
```

## ✅ Adım 4: Bağlantı Ayarları

`appsettings.json` dosyasını açın ve PostgreSQL şifrenizi güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SenswiseUserDb;Username=postgres;Password=BURAYA_SİFRENİZİ_YAZIN"
  }
}
```

## ✅ Adım 5: Veritabanını Hazırlayın

```bash
dotnet ef database update
```

✅ Başarılı mesajı görmelisiniz: `"Done."`

## ✅ Adım 6: Uygulamayı Başlatın

```bash
dotnet run
```

✅ Şu mesajı göreceksiniz:
```
Now listening on: http://localhost:5266
```

## 🌐 Swagger'a Erişim

Tarayıcınızda şu adresi açın:

### **http://localhost:5266/swagger**

## 🎯 Swagger Üzerinden Test

### 1️⃣ Kullanıcı Oluşturma

1. **POST /api/Users** endpoint'ine tıklayın
2. **"Try it out"** butonuna basın
3. Şu örnek verileri girin:

```json
{
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "email": "ahmet@test.com",
  "address": "Ankara"
}
```

4. **"Execute"** butonuna basın
5. Response'da `201 Created` ve kullanıcı bilgilerini göreceksiniz

### 2️⃣ Kullanıcıları Listeleme

1. **GET /api/Users** endpoint'ine tıklayın
2. **"Try it out"** butonuna basın
3. **"Execute"** butonuna basın
4. Oluşturduğunuz kullanıcıları göreceksiniz

### 3️⃣ Diğer İşlemler

- **GET /api/Users/{id}** - ID ile kullanıcı getir
- **PUT /api/Users/{id}** - Kullanıcı güncelle
- **DELETE /api/Users/{id}** - Kullanıcı sil

## ⚠️ Sorun Giderme

### Port Zaten Kullanımda Hatası

**macOS/Linux:**
```bash
lsof -ti:5266 | xargs kill -9
dotnet run
```

**Windows:**
```cmd
netstat -ano | findstr :5266
taskkill /PID <PID_NUMARASI> /F
dotnet run
```

### PostgreSQL Bağlantı Hatası

1. PostgreSQL'in çalıştığından emin olun:
   ```bash
   # macOS
   brew services start postgresql
   
   # Linux
   sudo systemctl start postgresql
   
   # Windows - Services'den PostgreSQL'i başlatın
   ```

2. `appsettings.json` dosyasındaki şifreyi kontrol edin

3. Veritabanının oluşturulduğundan emin olun:
   ```bash
   psql -U postgres -c "\l" | grep SenswiseUserDb
   ```

### Migration Hatası

```bash
# Migration'ları sıfırlayın
dotnet ef database drop -f
dotnet ef database update
```

## 📸 Ekran Görüntüsü

Swagger açıldığında şu şekilde görünmelidir:

```
Senswise User Service API - v1

GET  /api/Users            Tüm kullanıcıları listeler
GET  /api/Users/{id}       ID'ye göre kullanıcı getirir
POST /api/Users            Yeni kullanıcı oluşturur
PUT  /api/Users/{id}       Kullanıcı bilgilerini günceller
DELETE /api/Users/{id}     Kullanıcıyı siler
```

## ✅ Başarılı Response Örneği

```json
{
  "success": true,
  "message": "Kullanıcı başarıyla oluşturuldu.",
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "firstName": "Ahmet",
    "lastName": "Yılmaz",
    "email": "ahmet@test.com",
    "address": "Ankara",
    "createdAt": "2026-02-23T19:00:00Z"
  },
  "errors": null
}
```

## 📞 Yardım

Sorun yaşarsanız:
- GitHub Issues: https://github.com/mrvblgn/user-api/issues
- README.md dosyasına bakın
- Detaylı bilgi için projedeki dokümantasyonu inceleyin

---

**Tebrikler! 🎉 Projeniz çalışıyor.**

Swagger üzerinden tüm API fonksiyonlarını test edebilirsiniz.
