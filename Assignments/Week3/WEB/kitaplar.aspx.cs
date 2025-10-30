using System;
using System.Configuration;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace WEB
{
    public partial class kitaplar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KitaplariGetir();
            }
        }

        private void KitaplariGetir()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT ID, kitap_adi, yazar FROM kitaplar ORDER BY ID";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    RepeaterKitaplar.DataSource = reader;
                    RepeaterKitaplar.DataBind();

                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblMesaj.Text = "Hata: " + ex.Message;
                }
            }
        }
    }
}


