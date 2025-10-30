using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB.admin
{
    public partial class kitapdzn : System.Web.UI.Page
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
                string sql = "SELECT kitap_adi, yazar, isbn, yil FROM kitaplar WHERE ID=@id";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtKitapAdi.Text = dr["kitap_adi"].ToString();
                            txtYazar.Text = dr["yazar"].ToString();
                            txtIsbn.Text = dr["isbn"].ToString();
                            txtYil.Text = dr["yil"].ToString();
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
                        string insertSql = "INSERT INTO kitaplar(kitap_adi, yazar, isbn, yil) VALUES(@kitap_adi, @yazar, @isbn, @yil)";
                        using (var cmd = new MySqlCommand(insertSql, con))
                        {
                            cmd.Parameters.AddWithValue("@kitap_adi", txtKitapAdi.Text);
                            cmd.Parameters.AddWithValue("@yazar", txtYazar.Text);
                            cmd.Parameters.AddWithValue("@isbn", txtIsbn.Text);
                            cmd.Parameters.AddWithValue("@yil", string.IsNullOrWhiteSpace(txtYil.Text) ? (object)DBNull.Value : Convert.ToInt32(txtYil.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string updateSql = "UPDATE kitaplar SET kitap_adi=@kitap_adi, yazar=@yazar, isbn=@isbn, yil=@yil WHERE ID=@id";
                        using (var cmd = new MySqlCommand(updateSql, con))
                        {
                            cmd.Parameters.AddWithValue("@kitap_adi", txtKitapAdi.Text);
                            cmd.Parameters.AddWithValue("@yazar", txtYazar.Text);
                            cmd.Parameters.AddWithValue("@isbn", txtIsbn.Text);
                            cmd.Parameters.AddWithValue("@yil", string.IsNullOrWhiteSpace(txtYil.Text) ? (object)DBNull.Value : Convert.ToInt32(txtYil.Text));
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("kitaplar.aspx");
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }
    }
}


