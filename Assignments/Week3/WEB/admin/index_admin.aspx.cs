using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.admin
{
    public partial class index_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["kullanici_adi"] != null)
                {
                    lblKullaniciAdi.Text = Session["kullanici_adi"].ToString();
                }
            }
        }
    }
}

