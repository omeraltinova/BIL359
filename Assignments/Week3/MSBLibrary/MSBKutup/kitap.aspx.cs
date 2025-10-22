using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSBKutup
{
	public partial class kitap : System.Web.UI.Page
	{

        SqlConnection bag = new SqlConnection
                    (@"Data Source=DESKTOP-C3Q1IB4\SQLEXPRESS;Initial Catalog=msbkutup;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
		{
			bag.Open();

			SqlCommand cmd = new SqlCommand("select * from kitap",bag);
			SqlDataReader dr=cmd.ExecuteReader();
			Repeater1.DataSource = dr;
			Repeater1.DataBind();


            bag.Close();


		}
	}
}