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
    }
}

