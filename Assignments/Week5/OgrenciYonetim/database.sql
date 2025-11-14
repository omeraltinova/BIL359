-- Öğrenci Yönetim Sistemi Veritabanı
-- MySQL Database Script

-- Mevcut webkutup veritabanını kullan
USE webkutup;

-- Öğrenciler tablosu
CREATE TABLE IF NOT EXISTS ogrenciler (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Numara VARCHAR(20) NOT NULL UNIQUE,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    Bolum VARCHAR(100) NOT NULL,
    Kayit_Tarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_numara (Numara),
    INDEX idx_bolum (Bolum)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Örnek öğrenciler ekle
INSERT INTO ogrenciler (Numara, Ad, Soyad, Bolum) VALUES 
('20210001', 'Ahmet', 'Yılmaz', 'Bilgisayar Mühendisliği'),
('20210002', 'Ayşe', 'Demir', 'Bilgisayar Mühendisliği'),
('20210003', 'Mehmet', 'Kaya', 'Elektrik-Elektronik Mühendisliği'),
('20210004', 'Zeynep', 'Şahin', 'Endüstri Mühendisliği'),
('20210005', 'Fatma', 'Çelik', 'Bilgisayar Mühendisliği'),
('20210006', 'Ali', 'Öztürk', 'Makine Mühendisliği'),
('20210007', 'Elif', 'Aydın', 'Yazılım Mühendisliği'),
('20210008', 'Mustafa', 'Arslan', 'Bilgisayar Mühendisliği');

-- Tabloyu kontrol et
SELECT * FROM ogrenciler ORDER BY ID;

-- Tablo bilgilerini göster
DESCRIBE ogrenciler;

