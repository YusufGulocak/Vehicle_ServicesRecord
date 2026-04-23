# Vehicle Services Record

Araç bakım ve servis geçmişini yönetmek için geliştirilmiş full-stack web uygulaması.

## Teknolojiler

**Backend**
- ASP.NET Core 8 Web API
- Entity Framework Core 9 (SQL Server / LocalDB)
- JWT Authentication
- AutoMapper, FluentValidation
- Clean Architecture (Repository Pattern, Unit of Work)

**Frontend**
- React 19 + Vite
- React Router DOM
- Axios

## Özellikler

- Kullanıcı kayıt ve giriş (JWT token)
- Araç yönetimi (ekle, düzenle, sil, ara)
- Araç başına servis geçmişi
- Maliyet özeti (toplam harcama, ortalama maliyet, son servis tarihi)
- Anlık arama/filtreleme

## Kurulum

### Gereksinimler
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org)
- SQL Server veya LocalDB

### Backend

```bash
# Bağımlılıkları yükle
cd App.API
dotnet restore

# User Secrets ayarla
dotnet user-secrets set "AppSettings:Token" "YOUR_SECRET_KEY_MIN_32_CHARS"
dotnet user-secrets set "AppSettings:Issuer" "YourApp"
dotnet user-secrets set "AppSettings:Audience" "YourAudience"

# appsettings.Development.json içinde connection string'i ayarla
# "ConnectionStrings": { "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=vehcserv;..." }

# Veritabanını oluştur
dotnet ef database update --project ../Repositories --startup-project .

# Çalıştır
dotnet run
```

API: `https://localhost:7027`  
Swagger: `https://localhost:7027/swagger`  
Scalar: `https://localhost:7027/scalar`

### Frontend

```bash
cd vehicle-frontend
npm install
npm run dev
```

Uygulama: `http://localhost:5173`

## Proje Yapısı

```
├── App.API/           # Controller'lar, Program.cs
├── Services/          # İş mantığı, DTO'lar, Validasyon
├── Repositories/      # EF Core, Entity'ler, Migration'lar
└── vehicle-frontend/  # React uygulaması
```
