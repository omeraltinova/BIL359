using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB.admin
{
    public partial class uyeler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UyeleriGetir();
            }
        }

        private void UyeleriGetir()
        {
            string cs = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var con = new MySqlConnection(cs))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT ID, kulad, yetki, IFNULL(durum, 'aktif') AS durum FROM kullanicilar ORDER BY ID";
                    using (var cmd = new MySqlCommand(sql, con))
                    using (var dr = cmd.ExecuteReader())
                    {
                        RepeaterUyeler.DataSource = dr;
                        RepeaterUyeler.DataBind();
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


