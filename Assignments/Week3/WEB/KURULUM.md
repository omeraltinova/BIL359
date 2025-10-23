# WEB Projesi Kurulum ve Çalıştırma Rehberi

## Adım 1: MySQL Veritabanını Oluşturun

### Yöntem 1: Terminal/CMD ile

1. MySQL'e bağlanın:
```bash
mysql -u root -p
```

2. Veritabanını oluşturun ve tabloları ekleyin:
```sql
CREATE DATABASE webkutup CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE webkutup;

-- Kullanıcılar tablosu
CREATE TABLE kullanicilar (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    kulad VARCHAR(50) NOT NULL UNIQUE,
    sifre VARCHAR(50) NOT NULL,
    yetki VARCHAR(20) NOT NULL DEFAULT 'uye',
    kayit_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Kitaplar tablosu
CREATE TABLE kitaplar (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    kitap_adi VARCHAR(200) NOT NULL,
    yazar VARCHAR(100) NOT NULL,
    isbn VARCHAR(20),
    yil INT,
    kayit_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Örnek veriler
INSERT INTO kullanicilar (kulad, sifre, yetki) VALUES 
('admin', 'admin123', 'admin'),
('kullanici', '123456', 'uye');

INSERT INTO kitaplar (kitap_adi, yazar, isbn, yil) VALUES 
('Yapay Zeka', 'Ethem Alpaydin', '9789750227561', 2020),
('Makine Öğrenmesi', 'Ethem Alpaydin', '9789750228261', 2021),
('Derin Öğrenme', 'Ian Goodfellow', '9780262035613', 2016);
```

### Yöntem 2: SQL Dosyası ile

Terminal'de proje klasöründeyken:
```bash
cd Assignments/Week3/WEB
mysql -u root -p < database.sql
```

## Adım 2: Web.config Dosyasını Düzenleyin

`Web.config` dosyasını açın ve MySQL şifrenizi girin:

```xml
<connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=webkutup;Uid=root;Pwd=BURAYA_ŞİFRENİZİ_YAZIN;" 
         providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

**Önemli**: `Pwd=` kısmına kendi MySQL root şifrenizi yazın!
- Şifre yoksa: `Pwd=;`
- Şifre varsa: `Pwd=123456;` (kendi şifrenizi yazın)

## Adım 3: Visual Studio'da Projeyi Açın

### Windows'ta:

1. `WEB.sln` dosyasına çift tıklayın
   - Dosya yolu: `Assignments/Week3/WEB/WEB.sln`

2. Visual Studio açılacak

### Mac'te (Visual Studio for Mac):

1. Visual Studio for Mac'i açın
2. `File > Open`
3. `WEB.sln` dosyasını seçin

## Adım 4: NuGet Paketlerini Geri Yükleyin

Visual Studio açıldığında otomatik olarak NuGet paketleri yüklenecek. Eğer yüklenmediyse:

1. Solution Explorer'da projeye sağ tık
2. "Restore NuGet Packages" seçin

Veya Package Manager Console'da:
```
Update-Package -Reinstall
```

## Adım 5: Projeyi Derleyin ve Çalıştırın

1. **Derleme**: 
   - Menü: `Build > Build Solution` (veya `Ctrl+Shift+B`)

2. **Çalıştırma**:
   - Menü: `Debug > Start Debugging` (veya `F5`)
   - Veya üstteki yeşil ▶ butonuna tıklayın

3. Tarayıcı otomatik açılacak ve proje çalışacak

## Adım 6: Giriş Yapın

Tarayıcıda açılan sayfada:

1. `giris.aspx` sayfasına gidin
2. Admin olarak giriş yapın:
   - **Kullanıcı adı**: `admin`
   - **Şifre**: `admin123`

3. Giriş yaptıktan sonra admin paneline yönlendirileceksiniz

## 🎯 Sayfalar

- `https://localhost:44371/index.aspx` - Ana sayfa
- `https://localhost:44371/giris.aspx` - Giriş sayfası
- `https://localhost:44371/admin/index_admin.aspx` - Admin ana sayfa
- `https://localhost:44371/admin/kitaplar.aspx` - Kitap yönetimi

## ⚠️ Yaygın Sorunlar ve Çözümleri

### Hata: "Could not find file 'MySql.Data'"

**Çözüm**: NuGet paketlerini geri yükleyin
```
Install-Package MySql.Data -Version 8.0.33
```

### Hata: "Unable to connect to any of the specified MySQL hosts"

**Çözüm**: 
1. MySQL servisinin çalıştığından emin olun
2. `Web.config` dosyasındaki connection string'i kontrol edin
3. MySQL kullanıcı adı/şifreyi kontrol edin

### Hata: "Table 'webkutup.kullanicilar' doesn't exist"

**Çözüm**: Veritabanı tablolarını oluşturmadınız. Adım 1'e dönün.

### Port Çakışması

Eğer 44371 portu kullanımdaysa, `WEB.csproj` dosyasında portu değiştirebilirsiniz:
```xml
<IISExpressSSLPort>44372</IISExpressSSLPort>
```

## 🔐 Test Kullanıcıları

| Kullanıcı Adı | Şifre | Yetki |
|---------------|-------|-------|
| admin | admin123 | admin |
| kullanici | 123456 | uye |

## 📝 Not

Bu proje eğitim amaçlıdır. Üretim ortamında kullanmadan önce:
- Şifreleri hash'leyin
- HTTPS kullanın
- Güvenlik önlemlerini artırın

