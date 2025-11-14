using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Kitap veritabanı işlemleri sınıfı
    /// </summary>
    public class KitapDB
    {
        private readonly string connectionString;

        public KitapDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Tüm kitapları getirir
        /// </summary>
        public List<Kitap> GetAll()
        {
            List<Kitap> kitaplar = new List<Kitap>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT ID, kitap_adi, yazar, isbn, yil, kayit_tarihi FROM kitaplar ORDER BY ID DESC";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kitaplar.Add(new Kitap
                            {
                                ID = reader.GetInt32("ID"),
                                kitap_adi = reader.GetString("kitap_adi"),
                                yazar = reader.GetString("yazar"),
                                isbn = reader.IsDBNull(reader.GetOrdinal("isbn")) ? "" : reader.GetString("isbn"),
                                yil = reader.IsDBNull(reader.GetOrdinal("yil")) ? (int?)null : reader.GetInt32("yil"),
                                kayit_tarihi = reader.GetDateTime("kayit_tarihi")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kitaplar listelenirken hata oluştu: " + ex.Message);
                }
            }

            return kitaplar;
        }

        /// <summary>
        /// ID'ye göre kitap getirir
        /// </summary>
        public Kitap GetById(int id)
        {
            Kitap kitap = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT ID, kitap_adi, yazar, isbn, yil, kayit_tarihi FROM kitaplar WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kitap = new Kitap
                                {
                                    ID = reader.GetInt32("ID"),
                                    kitap_adi = reader.GetString("kitap_adi"),
                                    yazar = reader.GetString("yazar"),
                                    isbn = reader.IsDBNull(reader.GetOrdinal("isbn")) ? "" : reader.GetString("isbn"),
                                    yil = reader.IsDBNull(reader.GetOrdinal("yil")) ? (int?)null : reader.GetInt32("yil"),
                                    kayit_tarihi = reader.GetDateTime("kayit_tarihi")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kitap getirilirken hata oluştu: " + ex.Message);
                }
            }

            return kitap;
        }

        /// <summary>
        /// Yeni kitap ekler
        /// </summary>
        public bool Insert(Kitap kitap)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO kitaplar (kitap_adi, yazar, isbn, yil) VALUES (@kitap_adi, @yazar, @isbn, @yil)";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@kitap_adi", kitap.kitap_adi);
                        cmd.Parameters.AddWithValue("@yazar", kitap.yazar);
                        cmd.Parameters.AddWithValue("@isbn", kitap.isbn ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@yil", kitap.yil ?? (object)DBNull.Value);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kitap eklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Kitap bilgilerini günceller
        /// </summary>
        public bool Update(Kitap kitap)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE kitaplar SET kitap_adi = @kitap_adi, yazar = @yazar, isbn = @isbn, yil = @yil WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", kitap.ID);
                        cmd.Parameters.AddWithValue("@kitap_adi", kitap.kitap_adi);
                        cmd.Parameters.AddWithValue("@yazar", kitap.yazar);
                        cmd.Parameters.AddWithValue("@isbn", kitap.isbn ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@yil", kitap.yil ?? (object)DBNull.Value);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kitap güncellenirken hata oluştu: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Kitap siler
        /// </summary>
        public bool Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM kitaplar WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kitap silinirken hata oluştu: " + ex.Message);
                }
            }
        }
    }
}

