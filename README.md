# 📚 Kütüphane Yönetim Sistemi (Library Management System)

## 🎯 Proje Hakkında

Bu proje, kütüphanelerin günlük operasyonlarını yönetmek için geliştirilmiş kapsamlı bir **Kütüphane Yönetim Sistemi**'dir. Modern yazılım geliştirme prensipleri kullanılarak **katmanlı mimari (Layered Architecture)** ile tasarlanmıştır.

## 🏗️ Mimari Yapı

Proje, **3 katmanlı mimari** yaklaşımı ile yapılandırılmıştır:

```
KutuphaneYonetimSistemi/
├── 🌐 KutuphaneYonetimSistemi.API/          # Presentation Layer (Sunum Katmanı)
├── ⚙️ KutuphaneYonetimSistemi.Service/       # Business Logic Layer (İş Mantığı Katmanı)
└── 🗄️ KutuphaneYonetimSistemi.DataAccess/   # Data Access Layer (Veri Erişim Katmanı)
```

### Katman Detayları

#### 🌐 API Katmanı (Presentation Layer)

- **Controllers**: RESTful API endpoints
- **JWT Authentication**: Güvenli kimlik doğrulama
- **Swagger Documentation**: API dokümantasyonu
- **CORS**: Cross-origin istekleri destekler

#### ⚙️ Service Katmanı (Business Logic Layer)

- **Business Rules**: İş kuralları ve validasyonlar
- **Service Classes**: Her entity için ayrı servis sınıfları
- **Interface Implementations**: Loose coupling sağlar

#### 🗄️ Data Access Katmanı (Data Access Layer)

- **Entities**: Veritabanı işlemleri için entity sınıfları
- **DTOs**: Veri transfer objeleri
- **Interfaces**: Contract tanımlamaları
- **Stored Procedures**: SQL Server stored procedure kullanımı

## 💻 Teknoloji Stack

- **Framework**: .NET 8.0
- **API Type**: ASP.NET Core Web API
- **Database**: SQL Server
- **Authentication**: JWT Bearer Token
- **Documentation**: Swagger/OpenAPI
- **Dependency Injection**: Built-in DI Container
- **ORM/Data Access**: ADO.NET (Stored Procedures)

## 📦 Kullanılan NuGet Paketleri

```xml
<!-- API Katmanı -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.7" />
<PackageReference Include="Microsoft.IdentityModel.Tokens" Version="8.12.1" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.12.1" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
<PackageReference Include="Autofac.Extensions.DependencyInjection" Version="8.0.0" />

<!-- Data Access Katmanı -->
<PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.2" />
```

## 🔧 Özellikler

### 📚 Kitap Yönetimi

- ✅ Kitap ekleme, düzenleme, silme
- ✅ Kitap listeleme ve arama
- ✅ ISBN, yazar, yayınevi bilgileri
- ✅ Stok takibi ve raf konumu
- ✅ Kategori bazlı organizasyon

### 👥 Üye Yönetimi

- ✅ Üye kaydı ve profil yönetimi
- ✅ TC kimlik, iletişim bilgileri
- ✅ Üyelik tarihi takibi
- ✅ Üye bilgilerini güncelleme

### 📖 Ödünç Alma İşlemleri

- ✅ Kitap ödünç verme ve iade
- ✅ Ödünç alma tarihi takibi
- ✅ İade tarihi kontrolü
- ✅ Geçikme hesaplama

### 💰 Ceza Yönetimi

- ✅ Geç iade ceza hesaplama
- ✅ Ceza miktarı takibi
- ✅ Ödeme durumu kontrolü
- ✅ Ceza geçmişi

### 👨‍💼 Personel Yönetimi

- ✅ Personel kaydı ve yetkilendirme
- ✅ Rol bazlı erişim kontrolü
- ✅ Personel bilgileri yönetimi

### 🔐 Kimlik Doğrulama ve Yetkilendirme

- ✅ JWT tabanlı authentication
- ✅ Güvenli token yönetimi
- ✅ Rol bazlı authorization
- ✅ Session yönetimi

## 🗃️ Veritabanı Yapısı

### Ana Tablolar

#### 📚 Kitap (Kitap)

```sql
- KitapID (PK)
- KitapAdi
- YazarAdi
- YayinEvi
- ISBN
- SayfaSayisi
- BasimYili
- KategoriID (FK)
- StokAdedi
- RafNo
- Durum
- EklenmeTarihi
```

#### 👥 Üye (Uye)

```sql
- UyeID (PK)
- TCKimlik
- Ad
- Soyad
- Email
- KullaniciAdi
- DogumTarihi
- Cinsiyet
- Telefon
- UyelikTarihi
```

#### 📖 Ödünç İşlem (OduncIslem)

```sql
- IslemID (PK)
- UyeID (FK)
- KitapID (FK)
- OduncTarihi
- PlanlananIadeTarihi
- GercekIadeTarihi
- Durum
```

#### 💰 Ceza Bilgi (CezaBilgi)

```sql
- CezaID (PK)
- UyeID (FK)
- IslemID (FK)
- CezaMiktari
- CezaTarihi
- OdemeDurumu
```

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler

- .NET 8.0 SDK
- SQL Server (Express/LocalDB da çalışır)
- Visual Studio 2022 veya JetBrains Rider

### Adım 1: Repository'yi Klonlayın

```bash
git clone https://github.com/kullanici-adi/KutuphaneYonetimSistemi.git
cd KutuphaneYonetimSistemi
```

### Adım 2: Veritabanı Bağlantısını Ayarlayın

`KutuphaneYonetimSistemi.DataAccess/Context/DatabaseConnection.cs` dosyasında connection string'i güncelleyin:

```csharp
string connectionString = @"Server=YOUR_SERVER;Database=KutuphaneDB;Integrated Security=True;TrustServerCertificate=True;";
```

### Adım 3: Veritabanını Oluşturun

- SQL Server'da `KutuphaneDB` adında veritabanı oluşturun
- Gerekli tabloları ve stored procedure'ları çalıştırın

### Adım 4: JWT Ayarlarını Yapılandırın

`appsettings.json` dosyasında JWT ayarlarını güncelleyin:

```json
{
  "Jwt": {
    "Key": "your-secret-key-here-minimum-32-characters",
    "Issuer": "KutuphaneYonetimSistemi",
    "Audience": "KutuphaneUsers"
  }
}
```

### Adım 5: Projeyi Çalıştırın

```bash
dotnet restore
dotnet build
dotnet run --project KutuphaneYonetimSistemi
```

## 📋 API Endpoints

### 🔐 Authentication

```
POST /api/ControllerAuth/login          # Giriş yap
POST /api/ControllerAuth/register       # Kayıt ol
```

### 📚 Kitap İşlemleri

```
GET    /api/ControllerKitap             # Tüm kitapları getir
GET    /api/ControllerKitap/{id}        # ID'ye göre kitap getir
POST   /api/ControllerKitap             # Yeni kitap ekle
PUT    /api/ControllerKitap/{id}        # Kitap güncelle
DELETE /api/ControllerKitap/{id}        # Kitap sil
GET    /api/ControllerKitap/ara?query   # Kitap ara
```

### 👥 Üye İşlemleri

```
GET    /api/ControllerUye               # Tüm üyeleri getir
GET    /api/ControllerUye/{id}          # ID'ye göre üye getir
POST   /api/ControllerUye               # Yeni üye ekle
PUT    /api/ControllerUye/{id}          # Üye güncelle
DELETE /api/ControllerUye/{id}          # Üye sil
```

### 📖 Ödünç İşlemleri

```
GET    /api/ControllerOduncIslem        # Tüm ödünç işlemleri
POST   /api/ControllerOduncIslem        # Kitap ödünç ver
PUT    /api/ControllerOduncIslem/iade   # Kitap iade al
GET    /api/ControllerOduncIslem/geciken # Geciken kitaplar
```

### 💰 Ceza İşlemleri

```
GET    /api/ControllerCezaBilgi         # Tüm cezalar
GET    /api/ControllerCezaBilgi/uye/{id} # Üyeye ait cezalar
POST   /api/ControllerCezaBilgi/odemeal # Ceza ödemesi
```

### 🏷️ Kategori İşlemleri

```
GET    /api/ControllerKategori          # Tüm kategoriler
POST   /api/ControllerKategori          # Yeni kategori
PUT    /api/ControllerKategori/{id}     # Kategori güncelle
DELETE /api/ControllerKategori/{id}     # Kategori sil
```

### 👨‍💼 Personel İşlemleri

```
GET    /api/ControllerPersonel          # Tüm personel
POST   /api/ControllerPersonel          # Yeni personel
PUT    /api/ControllerPersonel/{id}     # Personel güncelle
DELETE /api/ControllerPersonel/{id}     # Personel sil
```

## 📝 Kullanım Örnekleri

### Kitap Ekleme

```json
POST /api/ControllerKitap
{
  "kitapAdi": "1984",
  "yazarAdi": "George Orwell",
  "yayinEvi": "Can Yayınları",
  "isbn": "9789750718533",
  "sayfaSayisi": 352,
  "basimYili": 2021,
  "kategoriID": 1,
  "stokAdedi": 5,
  "rafNo": "A-01-15"
}
```

### Üye Kaydı

```json
POST /api/ControllerUye
{
  "tcKimlik": "12345678901",
  "ad": "Ahmet",
  "soyad": "Yılmaz",
  "email": "ahmet@email.com",
  "kullaniciAdi": "ahmetyilmaz",
  "dogumTarihi": "1990-05-15",
  "cinsiyet": "E",
  "telefon": "05551234567"
}
```

### Kitap Ödünç Verme

```json
POST /api/ControllerOduncIslem
{
  "uyeID": 1,
  "kitapID": 1,
  "planlananIadeTarihi": "2024-02-15"
}
```

## 🔒 Güvenlik

- **JWT Authentication**: Tüm hassas endpoints JWT token ile korunur
- **HTTPS**: Production'da HTTPS kullanımı zorunlu
- **SQL Injection**: Stored procedure kullanımı ile korunur
- **Input Validation**: DTO'lar ile giriş validasyonu
- **Error Handling**: Detaylı hata mesajları gizlenir

## 📊 Swagger Dokümantasyonu

Proje çalıştırıldıktan sonra Swagger UI'a şu adresten erişebilirsiniz:

```
https://localhost:7xyz/swagger
```

## 🛠️ Geliştirici Notları

### Kod Yapısı

- **SOLID Principles**: Kod SOLID prensiplerine uygun yazılmıştır
- **Interface Segregation**: Her entity için ayrı interface'ler
- **Dependency Injection**: Gevşek bağlı tasarım
- **Repository Pattern**: Veri erişim katmanı soyutlandırılmıştır

### Best Practices

- Stored procedure kullanımı
- DTO pattern ile veri transferi
- Exception handling
- Logging (geliştirilebilir)
- Configuration management

## 

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için `LICENSE` dosyasına bakınız.

# 

---