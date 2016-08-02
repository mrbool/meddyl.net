using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;


namespace GTSoft.Meddyl.Admin.pages.customer
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

        protected void btnCustomerStatus_Click(object sender, EventArgs e)
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

        protected void btnPromotionAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Add_Promotion();
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
                int customer_id = int.Parse(Request.QueryString["customer_id"].ToString());

                Load_Customer_Status();

                Set_Properties(customer_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Customer_Status()
        {
            try
            {
                BLL.Customer customer_bll = new BLL.Customer();
                customer_bll.Get_Customer_Status();

                this.ddlCustomerStatus.DataTextField = "status";
                this.ddlCustomerStatus.DataValueField = "status_id";
                this.ddlCustomerStatus.DataSource = customer_bll.customer_status_dal_array;
                this.ddlCustomerStatus.DataBind(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Certificates(DAL.Customer customer_dal)
        {
            try
            {
                DAL.Certificate cerificate_dal = null;

                BLL.Deal deal_bll = new BLL.Deal(cerificate_dal, customer_dal);
                deal_bll.Get_Customer_Certificates();

                if (deal_bll.successful)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("certificate_id", Type.GetType("System.String"));
                    dt.Columns.Add("company_name", Type.GetType("System.String"));
                    dt.Columns.Add("deal", Type.GetType("System.String"));

                    foreach (DAL.Certificate certificate_row in deal_bll.certificate_dal_array)
                    {
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1]["certificate_id"] = certificate_row.certificate_id.ToString();
                        dt.Rows[dt.Rows.Count - 1]["company_name"] = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                        dt.Rows[dt.Rows.Count - 1]["deal"] = certificate_row.deal_dal.deal.ToString();
                    }
                    this.gdvCertificates.DataSource = dt;
                    this.gdvCertificates.DataBind();
                    this.lblCertificatesLabel.Text = dt.Rows.Count.ToString() + " Certificates";
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

        private void Load_Credit_Cards(DAL.Customer customer_dal)
        {
            try
            {
                BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                customer_bll.Get_Credit_Cards();

                if (customer_bll.successful)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("card_number", Type.GetType("System.String"));

                    foreach (DAL.Credit_Card credit_card_row in customer_bll.credit_card_dal_array)
                    {
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1]["card_number"] = credit_card_row.card_number.ToString();
                    }
                    this.gdvCreditCards.DataSource = dt;
                    this.gdvCreditCards.DataBind();
                    this.lblCreditCardsLabel.Text = dt.Rows.Count.ToString() + " Credit Cards";
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

        private void Load_Promotions(DAL.Customer customer_dal)
        {
            try
            {
                BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                customer_bll.Get_All_Promotions();

                if (customer_bll.successful)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("promotion_code", Type.GetType("System.String"));
                    dt.Columns.Add("description", Type.GetType("System.String"));
                    dt.Columns.Add("expiration_date", Type.GetType("System.String"));
                    dt.Columns.Add("redeemed_date", Type.GetType("System.String"));

                    foreach (DAL.Promotion_Activity promotion_activity_row in customer_bll.promotion_activity_dal_array)
                    {
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1]["promotion_code"] = promotion_activity_row.promotion_dal.promotion_code.ToString();
                        dt.Rows[dt.Rows.Count - 1]["description"] = promotion_activity_row.promotion_dal.description.ToString();
                        dt.Rows[dt.Rows.Count - 1]["expiration_date"] = promotion_activity_row.expiration_date.ToString();
                        if (promotion_activity_row.redeemed_date.IsNull)
                            dt.Rows[dt.Rows.Count - 1]["redeemed_date"] = "";
                        else
                            dt.Rows[dt.Rows.Count - 1]["redeemed_date"] = promotion_activity_row.redeemed_date.ToString();
                    }
                    this.gdvPromotions.DataSource = dt;
                    this.gdvPromotions.DataBind();
                    this.lblPromotionLabel.Text = dt.Rows.Count.ToString() + " Promotions";
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

        private void Update_Status()
        {
            try
            {
                int customer_id = int.Parse(this.lblCustomerId.Text);
                int status_id = int.Parse(this.ddlCustomerStatus.SelectedValue);

                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.customer_id = customer_id;

                BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                customer_bll.Set_Customer_Status(status_id);

                if (customer_bll.successful)
                {
                    Set_Properties(customer_id);
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

        private void Add_Promotion()
        {
            try
            {
                int customer_id = int.Parse(this.lblCustomerId.Text);
                string promotion_code = this.txtPromotionCode.Text;

                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.customer_id = customer_id;

                BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                customer_bll.Add_Promotion(promotion_code);

                if (customer_bll.successful)
                {
                    Set_Properties(customer_id);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "err_msg",
                                "alert('" + customer_bll.system_error_dal.message.ToString() + "');",
                                true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Set_Properties(int customer_id)
        {
            try
            {
                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.customer_id = customer_id;

                BLL.Customer customer_bll = new BLL.Customer(customer_dal);

                this.lblCustomerId.Text = customer_bll.customer_dal.customer_id.ToString();
                this.lblCustomerName.Text = customer_bll.contact_dal.first_name.ToString() + " " + customer_bll.contact_dal.last_name.ToString();
                this.lblEmail.Text = customer_bll.contact_dal.email.ToString();
                this.ddlCustomerStatus.SelectedIndex = this.ddlCustomerStatus.Items.IndexOf(this.ddlCustomerStatus.Items.FindByText(customer_bll.customer_dal.customer_status_dal.status.ToString()));

                Load_Certificates(customer_dal);
                Load_Credit_Cards(customer_dal);
                Load_Promotions(customer_dal);
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