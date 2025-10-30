# WEB - İMÜ Kütüphane Sistemi

Bu proje, Medeniyet Üniversitesi Kütüphane Sistemi için geliştirilmiş bir ASP.NET Web Forms uygulamasıdır.

## Özellikler

- **Kullanıcı Girişi**: Admin ve normal kullanıcı girişi
- **Admin Paneli**: Kitap yönetimi (listeleme, ekleme, düzenleme, silme)
- **Modern Tasarım**: Week3 derslerinde geliştirilen CSS grid/flexbox yapısı
- **MySQL Veritabanı**: MySQL veritabanı entegrasyonu

## Teknolojiler

- ASP.NET Web Forms (.NET Framework 4.7.2)
- C#
- MySQL
- HTML5/CSS3
- Grid ve Flexbox Layout

## Kurulum

### Gereksinimler

- Visual Studio 2019 veya üzeri
- .NET Framework 4.7.2
- MySQL Server 8.0 veya üzeri
- IIS Express

### Veritabanı Kurulumu

1. MySQL sunucunuzda `webkutup` adında bir veritabanı oluşturun:
```sql
CREATE DATABASE webkutup CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

2. `database.sql` dosyasını çalıştırarak tabloları oluşturun:
```bash
mysql -u root -p webkutup < database.sql
```

### Proje Kurulumu

1. Projeyi Visual Studio'da açın
2. `Web.config` dosyasındaki connection string'i düzenleyin:
```xml
<add name="MySqlConnection" 
     connectionString="Server=localhost;Database=webkutup;Uid=root;Pwd=your_password;" 
     providerName="MySql.Data.MySqlClient" />
```

3. NuGet paketlerini geri yükleyin (Visual Studio otomatik yapacaktır)
4. Projeyi derleyin ve çalıştırın (F5)

## Veritabanı Yapısı

### kullanicilar Tablosu
- ID (int, primary key, auto_increment)
- kulad (varchar(50)) - Kullanıcı adı
- sifre (varchar(50)) - Şifre
- yetki (varchar(20)) - Yetki seviyesi (admin/uye)

### kitaplar Tablosu
- ID (int, primary key, auto_increment)
- kitap_adi (varchar(200)) - Kitap adı
- yazar (varchar(100)) - Yazar adı
- isbn (varchar(20)) - ISBN numarası
- yil (int) - Yayın yılı

## Kullanım

### Varsayılan Admin Girişi
- Kullanıcı adı: `admin`
- Şifre: `admin123`

### Sayfalar

- **index.aspx**: Ana sayfa
- **giris.aspx**: Giriş sayfası
- **admin/index_admin.aspx**: Admin ana sayfası
- **admin/kitaplar.aspx**: Kitap yönetimi sayfası

## Proje Yapısı

```
WEB/
├── admin/                      # Admin paneli dosyaları
│   ├── admin.Master           # Admin master page
│   ├── index_admin.aspx       # Admin ana sayfa
│   └── kitaplar.aspx          # Kitap yönetimi
├── css/                       # CSS dosyaları
│   ├── genel.css             # Genel stiller
│   └── kitap.css             # Kitap sayfası stilleri
├── images/                    # Resim dosyaları
│   └── logo.png              # Logo
├── Properties/                # Proje özellikleri
├── anasayfa.Master           # Ana master page
├── index.aspx                # Ana sayfa
├── giris.aspx                # Giriş sayfası
├── Web.config                # Yapılandırma dosyası
└── Global.asax               # Uygulama global dosyası
```

## Güvenlik Notu

⚠️ **ÖNEMLİ**: Bu proje eğitim amaçlıdır. Üretim ortamında kullanmadan önce:
- Şifreleri hash'leyin (bcrypt, SHA256 vb.)
- SQL Injection koruması ekleyin (bu projede parametreli sorgular kullanılıyor)
- HTTPS kullanın
- Session yönetimini güçlendirin

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

