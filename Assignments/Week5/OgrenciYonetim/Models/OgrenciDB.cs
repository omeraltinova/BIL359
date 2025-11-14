using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Öğrenci veritabanı işlemleri sınıfı
    /// </summary>
    public class OgrenciDB
    {
        private readonly string connectionString;

        public OgrenciDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Tüm öğrencileri getirir
        /// </summary>
        public List<Ogrenci> GetAll()
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT ID, Numara, Ad, Soyad, Bolum, Kayit_Tarihi FROM ogrenciler ORDER BY ID DESC";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ogrenciler.Add(new Ogrenci
                            {
                                ID = reader.GetInt32("ID"),
                                Numara = reader.GetString("Numara"),
                                Ad = reader.GetString("Ad"),
                                Soyad = reader.GetString("Soyad"),
                                Bolum = reader.GetString("Bolum"),
                                Kayit_Tarihi = reader.GetDateTime("Kayit_Tarihi")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Öğrenciler listelenirken hata oluştu: " + ex.Message);
                }
            }

            return ogrenciler;
        }

        /// <summary>
        /// ID'ye göre öğrenci getirir
        /// </summary>
        public Ogrenci GetById(int id)
        {
            Ogrenci ogrenci = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT ID, Numara, Ad, Soyad, Bolum, Kayit_Tarihi FROM ogrenciler WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ogrenci = new Ogrenci
                                {
                                    ID = reader.GetInt32("ID"),
                                    Numara = reader.GetString("Numara"),
                                    Ad = reader.GetString("Ad"),
                                    Soyad = reader.GetString("Soyad"),
                                    Bolum = reader.GetString("Bolum"),
                                    Kayit_Tarihi = reader.GetDateTime("Kayit_Tarihi")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Öğrenci getirilirken hata oluştu: " + ex.Message);
                }
            }

            return ogrenci;
        }

        /// <summary>
        /// Yeni öğrenci ekler
        /// </summary>
        public bool Insert(Ogrenci ogrenci)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO ogrenciler (Numara, Ad, Soyad, Bolum) VALUES (@numara, @ad, @soyad, @bolum)";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@numara", ogrenci.Numara);
                        cmd.Parameters.AddWithValue("@ad", ogrenci.Ad);
                        cmd.Parameters.AddWithValue("@soyad", ogrenci.Soyad);
                        cmd.Parameters.AddWithValue("@bolum", ogrenci.Bolum);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) // Duplicate entry error
                    {
                        throw new Exception("Bu öğrenci numarası zaten kayıtlı!");
                    }
                    throw new Exception("Öğrenci eklenirken hata oluştu: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Öğrenci eklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Öğrenci bilgilerini günceller
        /// </summary>
        public bool Update(Ogrenci ogrenci)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE ogrenciler SET Numara = @numara, Ad = @ad, Soyad = @soyad, Bolum = @bolum WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", ogrenci.ID);
                        cmd.Parameters.AddWithValue("@numara", ogrenci.Numara);
                        cmd.Parameters.AddWithValue("@ad", ogrenci.Ad);
                        cmd.Parameters.AddWithValue("@soyad", ogrenci.Soyad);
                        cmd.Parameters.AddWithValue("@bolum", ogrenci.Bolum);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) // Duplicate entry error
                    {
                        throw new Exception("Bu öğrenci numarası başka bir öğrenciye ait!");
                    }
                    throw new Exception("Öğrenci güncellenirken hata oluştu: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Öğrenci güncellenirken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Öğrenci siler
        /// </summary>
        public bool Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM ogrenciler WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Öğrenci silinirken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Öğrenci numarasının mevcut olup olmadığını kontrol eder
        /// </summary>
        public bool NumaraExists(string numara, int? excludeId = null)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT COUNT(*) FROM ogrenciler WHERE Numara = @numara";
                    
                    if (excludeId.HasValue)
                    {
                        sql += " AND ID != @id";
                    }
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@numara", numara);
                        
                        if (excludeId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@id", excludeId.Value);
                        }
                        
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Numara kontrolü yapılırken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Bölüme göre öğrenci sayısını getirir
        /// </summary>
        public Dictionary<string, int> GetStatsByBolum()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT Bolum, COUNT(*) as Sayi FROM ogrenciler GROUP BY Bolum ORDER BY Sayi DESC";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stats.Add(reader.GetString("Bolum"), reader.GetInt32("Sayi"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("İstatistikler getirilirken hata oluştu: " + ex.Message);
                }
            }

            return stats;
        }
    }
}

