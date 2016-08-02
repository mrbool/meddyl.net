using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTSoft.Meddyl.Web.merchant.error
{
    public partial class _default : System.Web.UI.Page
    {
        #region form methods

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
                MasterPageFile = "~/pages/merchant/master/Mobile.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Form_Data();
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\r\n", " ").Replace("\n", " ");
                Response.Redirect("../error/?error_text=" + message, false);
            }
        }

        #endregion


        #region private methods

        private void Load_Form_Data()
        {
            try
            {
                string error_text = "";

                if (Request.QueryString["message"] != null)
                    error_text = Request.QueryString["message"].ToString();
                else
                    error_text = "";

                this.lblErrorText.Text = error_text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}