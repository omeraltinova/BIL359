using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB.admin
{
    public partial class uyesil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    lblMesaj.Text = "Geçersiz istek.";
                    return;
                }

                string cs = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (var con = new MySqlConnection(cs))
                {
                    try
                    {
                        con.Open();
                        string sql = "DELETE FROM kullanicilar WHERE ID=@id";
                        using (var cmd = new MySqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int res = cmd.ExecuteNonQuery();
                            lblMesaj.Text = res > 0 ? "Üye silindi." : "Üye bulunamadı.";
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
}


