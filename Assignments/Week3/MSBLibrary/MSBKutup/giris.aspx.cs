using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSBKutup
{
	public partial class giris : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}


		SqlConnection bag = new SqlConnection
			(@"Data Source=DESKTOP-C3Q1IB4\SQLEXPRESS;Initial Catalog=msbkutup;Integrated Security=True");
        protected void Button1_Click(object sender, EventArgs e)
        {
			//db bağlan

			bag.Open();

			SqlCommand cmd = new SqlCommand("select * from kullanicilar where " +
				" kulad='"+TextBox1.Text+"' and sifre='"+TextBox2.Text+"'",bag);
			SqlDataReader dr=cmd.ExecuteReader();

			if (dr.Read())
			{
				if (dr["yetki"].ToString()=="admin")
                {
                    Session.Timeout = 30;
                    Session["yon_id"] = dr[0].ToString();

                    Response.Redirect("admin/index_admin.aspx");
				}
				else
				{

				}
			}

			bag.Close();


        }
    }
}