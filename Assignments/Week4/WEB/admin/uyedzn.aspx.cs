using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB.admin
{
    public partial class uyedzn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                litBaslik.Text = string.IsNullOrEmpty(id) ? "Ekle" : "Düzenle";
                if (!string.IsNullOrEmpty(id))
                {
                    KaydiYukle(id);
                }
            }
        }

        private void KaydiYukle(string id)
        {
            string cs = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var con = new MySqlConnection(cs))
            {
                con.Open();
                string sql = "SELECT kulad, yetki, IFNULL(durum,'aktif') AS durum FROM kullanicilar WHERE ID=@id";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtKulad.Text = dr["kulad"].ToString();
                            ddlYetki.SelectedValue = dr["yetki"].ToString();
                            ddlDurum.SelectedValue = dr["durum"].ToString();
                        }
                        else
                        {
                            lblMesaj.Text = "Kayıt bulunamadı.";
                        }
                    }
                }
            }
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string cs = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var con = new MySqlConnection(cs))
            {
                try
                {
                    con.Open();
                    if (string.IsNullOrEmpty(id))
                    {
                        if (string.IsNullOrWhiteSpace(txtSifre.Text))
                        {
                            lblMesaj.Text = "Yeni üye için şifre zorunludur.";
                            return;
                        }
                        string insertSql = "INSERT INTO kullanicilar(kulad, sifre, yetki, durum) VALUES(@kulad, @sifre, @yetki, @durum)";
                        using (var cmd = new MySqlCommand(insertSql, con))
                        {
                            cmd.Parameters.AddWithValue("@kulad", txtKulad.Text);
                            cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                            cmd.Parameters.AddWithValue("@yetki", ddlYetki.SelectedValue);
                            cmd.Parameters.AddWithValue("@durum", ddlDurum.SelectedValue);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Şifre boşsa değiştirme
                        string updateSql = string.IsNullOrWhiteSpace(txtSifre.Text)
                            ? "UPDATE kullanicilar SET kulad=@kulad, yetki=@yetki, durum=@durum WHERE ID=@id"
                            : "UPDATE kullanicilar SET kulad=@kulad, sifre=@sifre, yetki=@yetki, durum=@durum WHERE ID=@id";

                        using (var cmd = new MySqlCommand(updateSql, con))
                        {
                            cmd.Parameters.AddWithValue("@kulad", txtKulad.Text);
                            cmd.Parameters.AddWithValue("@yetki", ddlYetki.SelectedValue);
                            cmd.Parameters.AddWithValue("@durum", ddlDurum.SelectedValue);
                            cmd.Parameters.AddWithValue("@id", id);
                            if (!string.IsNullOrWhiteSpace(txtSifre.Text))
                            {
                                cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("uyeler.aspx");
                }
                catch (MySqlException ex)
                {
                    // durum sütunu yoksa uyarı göster
                    if (ex.Message.IndexOf("Unknown column 'durum'", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        lblMesaj.Text = "Veritabanında 'durum' sütunu yok. Lütfen schema'yı güncelleyin.";
                    }
                    else
                    {
                        lblMesaj.Text = "Hata: " + ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }
    }
}


