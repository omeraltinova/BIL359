using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WEB
{
    public partial class giris : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMesaj.Text = "";
                lblKayitMesaj.Text = "";
            }
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            
            using (MySqlConnection bag = new MySqlConnection(connectionString))
            {
                try
                {
                    bag.Open();

                    string sql = "SELECT * FROM kullanicilar WHERE kulad=@kulad AND sifre=@sifre";
                    MySqlCommand cmd = new MySqlCommand(sql, bag);
                    cmd.Parameters.AddWithValue("@kulad", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        string yetki = dr["yetki"].ToString();
                        
                        if (yetki == "admin")
                        {
                            Session.Timeout = 30;
                            Session["yon_id"] = dr["ID"].ToString();
                            Session["kullanici_adi"] = dr["kulad"].ToString();
                            
                            Response.Redirect("admin/index_admin.aspx");
                        }
                        else
                        {
                            Session.Timeout = 30;
                            Session["uye_id"] = dr["ID"].ToString();
                            Session["kullanici_adi"] = dr["kulad"].ToString();
                            
                            Response.Redirect("index.aspx");
                        }
                    }
                    else
                    {
                        lblMesaj.Text = "Kullanıcı adı veya şifre hatalı!";
                    }

                    dr.Close();
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }

        protected void btnKayit_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection bag = new MySqlConnection(connectionString))
            {
                try
                {
                    bag.Open();

                    // Kullanıcı var mı kontrol
                    string kontrolSql = "SELECT COUNT(1) FROM kullanicilar WHERE kulad=@kulad";
                    using (MySqlCommand kontrolCmd = new MySqlCommand(kontrolSql, bag))
                    {
                        kontrolCmd.Parameters.AddWithValue("@kulad", txtYeniKullaniciAdi.Text);
                        int count = Convert.ToInt32(kontrolCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            lblKayitMesaj.Text = "Bu kullanıcı adı zaten alınmış.";
                            return;
                        }
                    }

                    // Ekleme
                    string ekleSql = "INSERT INTO kullanicilar(kulad, sifre, yetki) VALUES(@kulad, @sifre, 'uye')";
                    using (MySqlCommand ekleCmd = new MySqlCommand(ekleSql, bag))
                    {
                        ekleCmd.Parameters.AddWithValue("@kulad", txtYeniKullaniciAdi.Text);
                        ekleCmd.Parameters.AddWithValue("@sifre", txtYeniSifre.Text);
                        int res = ekleCmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            lblKayitMesaj.ForeColor = System.Drawing.Color.Green;
                            lblKayitMesaj.Text = "Kayıt başarıyla oluşturuldu. Giriş yapabilirsiniz.";
                        }
                        else
                        {
                            lblKayitMesaj.Text = "Kayıt oluşturulamadı.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblKayitMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }
    }
}

