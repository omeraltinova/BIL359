using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WEB.admin
{
    public partial class kitaplar : System.Web.UI.Page
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

            using (MySqlConnection bag = new MySqlConnection(connectionString))
            {
                try
                {
                    bag.Open();

                    string sql = "SELECT * FROM kitaplar ORDER BY ID";
                    MySqlCommand cmd = new MySqlCommand(sql, bag);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    Repeater1.DataSource = dr;
                    Repeater1.DataBind();

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

