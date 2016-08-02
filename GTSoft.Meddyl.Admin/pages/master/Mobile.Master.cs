using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTSoft.Meddyl.Admin.pages.master
{
    public partial class Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbnMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("/pages/menu/");
        }
    }
}