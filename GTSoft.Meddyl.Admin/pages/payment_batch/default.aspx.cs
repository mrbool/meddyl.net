using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace GTSoft.Meddyl.Admin.pages.payment_batch
{
    public partial class _default : System.Web.UI.Page
    {
        #region events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
                MasterPageFile = "~/pages/master/Mobile.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Load_Form_Data();
                }
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Process();
        }

        #endregion


        #region private methods

        private void Load_Form_Data()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Process()
        {
            try
            {
                BLL.Deal deal_bll = new BLL.Deal();
                deal_bll.Deal_Payment_Batch();

                if (deal_bll.successful)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                              "successful",
                              "alert('Processing complete');",
                              true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                              "err_msg",
                              "alert('" + deal_bll.system_error_dal.message.ToString() + "');",
                              true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Error_Page(Exception ex)
        {
            string message = ex.Message.Replace("\r\n", " ").Replace("\n", " ");
            Response.Redirect("../error/?message=" + message, false);
        }

        #endregion

    }
}