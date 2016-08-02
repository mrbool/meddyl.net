using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace GTSoft.Meddyl.Admin.pages.certificate
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

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Status();
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
                int deal_id = int.Parse(Request.QueryString["certificate_id"].ToString());

                Load_Certificate_Status();

                Set_Properties(deal_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Certificate_Status()
        {
            try
            {
                BLL.Deal deal_bll = new BLL.Deal();
                deal_bll.Get_Certificate_Status();

                List<DAL.Certificate_Status> certificate_status_dal_array = deal_bll.certificate_status_dal_array;
                this.ddlCertificateStatus.DataTextField = "status";
                this.ddlCertificateStatus.DataValueField = "status_id";
                this.ddlCertificateStatus.DataSource = certificate_status_dal_array;
                this.ddlCertificateStatus.DataBind(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Status()
        {
            try
            {
                int certificate_id = int.Parse(this.lblCertificateId.Text);
                int status_id = int.Parse(this.ddlCertificateStatus.SelectedValue);

                DAL.Certificate certificate_dal = new DAL.Certificate();
                certificate_dal.certificate_id = certificate_id;

                BLL.Deal deal_bll = new BLL.Deal(certificate_dal);
                deal_bll.Set_Certificate_Status(status_id);

                if (deal_bll.successful)
                {
                    Set_Properties(certificate_id);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "err_msg",
                                "alert('ERROR');",
                                true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void Set_Properties(int certificate_id)
        {
            try
            {
                DAL.Certificate certificate_dal = new DAL.Certificate();
                certificate_dal.certificate_id = certificate_id;

                BLL.Deal deal_bll = new BLL.Deal(certificate_dal);

                if (deal_bll.successful)
                {

                    this.lblCertificateId.Text = certificate_id.ToString();
                    this.lblCertificateCode.Text = deal_bll.certificate_dal.certificate_code.ToString(); 
                    this.hplCompanyName.Text = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();

                    if (deal_bll.certificate_dal.customer_dal != null)
                    {
                        this.lblCustomerName.Text = deal_bll.certificate_dal.customer_dal.contact_dal.first_name.ToString() + " " + deal_bll.certificate_dal.customer_dal.contact_dal.last_name.ToString();
                        this.hplCustomerEmail.Text = deal_bll.certificate_dal.customer_dal.contact_dal.email.ToString();
                        this.hplCustomerEmail.NavigateUrl = "../customer/?customer_id=" + deal_bll.certificate_dal.customer_dal.customer_id.ToString();
                    }
                    else
                    {
                        this.lblCustomerName.Text = "N/A";
                        this.hplCustomerEmail.Text = "N/A";
                    }
                    this.lblDealId.Text = deal_bll.deal_dal.deal_id.ToString();
                    this.hplCompanyName.NavigateUrl = "../merchant/?merchant_contact_id=" + deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_contact_id.ToString();
                    this.hplCompanyName.Text = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                    this.lblDeal.Text = deal_bll.deal_dal.deal.ToString();
                    this.ddlCertificateStatus.SelectedIndex = this.ddlCertificateStatus.Items.IndexOf(this.ddlCertificateStatus.Items.FindByText(deal_bll.certificate_dal.certificate_status_dal.status.ToString()));
                }
            }
            catch(Exception ex)
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