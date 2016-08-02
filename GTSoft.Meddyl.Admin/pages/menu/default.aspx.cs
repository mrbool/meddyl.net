using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace GTSoft.Meddyl.Admin.pages.menu
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
                    if (Session["user_id"] != null)
                    {
                        int user_id = int.Parse(Session["user_id"].ToString());

                        Load_Form_Data();
                    }
                    else
                    {
                        throw new System.InvalidOperationException("You must login");
                    }
                }
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        #endregion


        #region private methods

        private void Load_Form_Data()
        {
            try
            {
                List<DAL.System_Settings> system_settings_dal_array = new List<DAL.System_Settings>();

                BLL.System system_bll = new BLL.System();
                system_bll.Get_System_Pending();
                system_settings_dal_array = system_bll.system_settings_dal_array;

                foreach (DAL.System_Settings system_settings in system_settings_dal_array)
                {
                    if (system_settings.report == "Merchants Pending Approval")
                    {
                        this.hplMerchantPendingApproval.Text = "There are " + system_settings.quantity.ToString() + " Merchants Pending Approval";
                        this.hplMerchantPendingApproval.NavigateUrl = "../merchant_search/default.aspx?report=" + system_settings.report.ToString().Replace(" ","_");
                    }
                    else if (system_settings.report == "Deals Pending Approval")
                    {
                        this.hplDealPendingApproval.Text = "There are " + system_settings.quantity.ToString() + " Deals Pending Approval";
                        this.hplDealPendingApproval.NavigateUrl = "../deal_search/default.aspx?report=" + system_settings.report.ToString().Replace(" ", "_");
                    }
                    else if (system_settings.report == "Deals Pending Payment")
                    {
                        this.hplDealPendingPayment.Text = "There are " + system_settings.quantity.ToString() + " Deals Pending Payment";
                        this.hplDealPendingPayment.NavigateUrl = "../deal_search/default.aspx?report=" + system_settings.report.ToString().Replace(" ", "_");
                    }
                }

                this.hplMerchantSearch.Text = "Merchant Search";
                this.hplMerchantSearch.NavigateUrl = "../merchant_search/default.aspx?report=merchant_search";

                this.hplDealSearch.Text = "Deal Search";
                this.hplDealSearch.NavigateUrl = "../deal_search/default.aspx?report=deal_search";

                this.hplCustomerSearch.Text = "Customer Search";
                this.hplCustomerSearch.NavigateUrl = "../customer_search/default.aspx?report=customer_search";

                this.hplCertificateSearch.Text = "Certificate Search";
                this.hplCertificateSearch.NavigateUrl = "../certificate_search/default.aspx?report=certificate_search";

                this.hplPaymentBatch.Text = "Payment Batch";
                this.hplPaymentBatch.NavigateUrl = "../payment_batch/default.aspx?report=deal_search";
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