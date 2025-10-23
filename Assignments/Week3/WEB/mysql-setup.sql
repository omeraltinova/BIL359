-- MySQL Kullanıcı ve Veritabanı Kurulumu
-- Mac için (root yerine webuser kullanıcısı)

-- 1. Veritabanını oluştur
CREATE DATABASE IF NOT EXISTS webkutup CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 2. Yeni kullanıcı oluştur (root yerine)
CREATE USER IF NOT EXISTS 'webuser'@'localhost' IDENTIFIED BY 'webpass123';

-- 3. Kullanıcıya webkutup veritabanı için tüm yetkileri ver
GRANT ALL PRIVILEGES ON webkutup.* TO 'webuser'@'localhost';

-- 4. Yetkileri uygula
FLUSH PRIVILEGES;

-- 5. webkutup veritabanını kullan
USE webkutup;

-- 6. Tabloları oluştur
CREATE TABLE IF NOT EXISTS kullanicilar (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    kulad VARCHAR(50) NOT NULL UNIQUE,
    sifre VARCHAR(50) NOT NULL,
    yetki VARCHAR(20) NOT NULL DEFAULT 'uye',
    kayit_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS kitaplar (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    kitap_adi VARCHAR(200) NOT NULL,
    yazar VARCHAR(100) NOT NULL,
    isbn VARCHAR(20),
    yil INT,
    kayit_tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 7. Örnek verileri ekle
INSERT INTO kullanicilar (kulad, sifre, yetki) VALUES 
('admin', 'admin123', 'admin'),
('kullanici', '123456', 'uye');

INSERT INTO kitaplar (kitap_adi, yazar, isbn, yil) VALUES 
('Yapay Zeka', 'Ethem Alpaydin', '9789750227561', 2020),
('Makine Öğrenmesi', 'Ethem Alpaydin', '9789750228261', 2021),
('Derin Öğrenme', 'Ian Goodfellow', '9780262035613', 2016),
('Python ile Veri Bilimi', 'Jake VanderPlas', '9781491912058', 2016),
('Algoritmalara Giriş', 'Thomas H. Cormen', '9780262033848', 2009),
('Temiz Kod', 'Robert C. Martin', '9780132350884', 2008);

-- 8. Kontrol et
SELECT 'Kullanıcılar:' as 'Tablo';
SELECT * FROM kullanicilar;

SELECT 'Kitaplar:' as 'Tablo';
SELECT * FROM kitaplar;

-- Kullanıcı bilgileri:
-- Kullanıcı adı: webuser
-- Şifre: webpass123
-- Veritabanı: webkutup

