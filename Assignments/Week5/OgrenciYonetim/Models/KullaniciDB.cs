using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace OgrenciYonetim.Models
{
    /// <summary>
    /// Kullanıcı veritabanı işlemleri sınıfı
    /// </summary>
    public class KullaniciDB
    {
        private readonly string connectionString;

        public KullaniciDB()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        /// <summary>
        /// Kullanıcı girişi kontrolü
        /// </summary>
        public Kullanici Login(string kulad, string sifre)
        {
            Kullanici kullanici = null;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM kullanicilar WHERE kulad=@kulad AND sifre=@sifre";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@kulad", kulad);
                        cmd.Parameters.AddWithValue("@sifre", sifre);
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kullanici = new Kullanici
                                {
                                    ID = reader.GetInt32("ID"),
                                    kulad = reader.GetString("kulad"),
                                    yetki = reader.GetString("yetki"),
                                    durum = reader.GetString("durum"),
                                    kayit_tarihi = reader.GetDateTime("kayit_tarihi")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Giriş kontrolü sırasında hata: " + ex.Message);
                }
            }

            return kullanici;
        }

        /// <summary>
        /// Yeni kullanıcı kaydı
        /// </summary>
        public bool Register(string kulad, string sifre)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kullanıcı var mı kontrol
                    string kontrolSql = "SELECT COUNT(1) FROM kullanicilar WHERE kulad=@kulad";
                    using (MySqlCommand kontrolCmd = new MySqlCommand(kontrolSql, conn))
                    {
                        kontrolCmd.Parameters.AddWithValue("@kulad", kulad);
                        int count = Convert.ToInt32(kontrolCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            throw new Exception("Bu kullanıcı adı zaten alınmış.");
                        }
                    }

                    // Yeni kullanıcı ekle
                    string ekleSql = "INSERT INTO kullanicilar(kulad, sifre, yetki) VALUES(@kulad, @sifre, 'uye')";
                    using (MySqlCommand ekleCmd = new MySqlCommand(ekleSql, conn))
                    {
                        ekleCmd.Parameters.AddWithValue("@kulad", kulad);
                        ekleCmd.Parameters.AddWithValue("@sifre", sifre);
                        int res = ekleCmd.ExecuteNonQuery();
                        return res > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kayıt sırasında hata: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Tüm kullanıcıları getirir (Admin için)
        /// </summary>
        public List<Kullanici> GetAll()
        {
            List<Kullanici> kullanicilar = new List<Kullanici>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT ID, kulad, yetki, durum, kayit_tarihi FROM kullanicilar ORDER BY ID DESC";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kullanicilar.Add(new Kullanici
                            {
                                ID = reader.GetInt32("ID"),
                                kulad = reader.GetString("kulad"),
                                yetki = reader.GetString("yetki"),
                                durum = reader.GetString("durum"),
                                kayit_tarihi = reader.GetDateTime("kayit_tarihi")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kullanıcılar listelenirken hata: " + ex.Message);
                }
            }

            return kullanicilar;
        }

        /// <summary>
        /// Kullanıcı siler
        /// </summary>
        public bool Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM kullanicilar WHERE ID = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Kullanıcı silinirken hata: " + ex.Message);
                }
            }
        }
    }
}

