using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Session kontrolü
            if (Session["yon_id"] == null)
            {
                Response.Redirect("../giris.aspx");
            }
        }
    }
}

