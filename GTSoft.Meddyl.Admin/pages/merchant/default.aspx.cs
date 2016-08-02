using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;


namespace GTSoft.Meddyl.Admin.pages.merchant
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

        protected void btnMerchantStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Merchant_Status();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnMerchantUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Merchant();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Approve();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnMerchantContactStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Merchant_Contact_Status();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnCreditCardAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Add_Credit_Card();
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
                int merchant_contact_id = int.Parse(Request.QueryString["merchant_contact_id"].ToString());

                Load_Merchant_Status();
                Load_Merchant_Contact_Status();
                Load_Merchant_Rating();

                Set_Properties(merchant_contact_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Merchant_Status()
        {
            try
            {
                BLL.Merchant merchant_bll = new BLL.Merchant();
                merchant_bll.Get_Merchant_Status();

                this.ddlMerchantStatus.DataTextField = "status";
                this.ddlMerchantStatus.DataValueField = "status_id";
                this.ddlMerchantStatus.DataSource = merchant_bll.merchant_status_dal_array;
                this.ddlMerchantStatus.DataBind(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Merchant_Rating()
        {
            try
            {
                BLL.Merchant merchant_bll = new BLL.Merchant();
                merchant_bll.Get_Merchant_Rating();

                this.ddlRating.DataTextField = "rating";
                this.ddlRating.DataValueField = "rating_id";
                this.ddlRating.DataSource = merchant_bll.merchant_rating_dal_array;
                this.ddlRating.DataBind();
                this.ddlRating.Items.Insert(0, new ListItem("", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Merchant_Contact_Status()
        {
            try
            {
                BLL.Merchant merchant_bll = new BLL.Merchant();
                merchant_bll.Get_Merchant_Contact_Status();

                this.ddlMerchantContactStatus.DataTextField = "status";
                this.ddlMerchantContactStatus.DataValueField = "status_id";
                this.ddlMerchantContactStatus.DataSource = merchant_bll.merchant_contact_status_dal_array;
                this.ddlMerchantContactStatus.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Deals(DAL.Merchant_Contact merchant_contact_dal)
        {
            try
            {
                DAL.Deal deal_dal = null;

                BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);
                deal_bll.Get_Merchant_Deals();

                DataTable dt = new DataTable();
                dt.Columns.Add("Deal Id", Type.GetType("System.String"));
                dt.Columns.Add("Deal", Type.GetType("System.String"));

                foreach (DAL.Deal deal_row in deal_bll.deal_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["Deal Id"] = deal_row.deal_id.ToString();
                    dt.Rows[dt.Rows.Count - 1]["Deal"] = deal_row.deal;
                }
                this.gdvDeals.DataSource = dt;
                this.gdvDeals.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Credit_Cards(DAL.Merchant_Contact merchant_contact_dal)
        {
            try
            {
                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                merchant_bll.Get_Credit_Cards();

                DataTable dt = new DataTable();
                dt.Columns.Add("card_number", Type.GetType("System.String"));

                foreach (DAL.Credit_Card credit_card_row in merchant_bll.credit_card_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["card_number"] = credit_card_row.card_number.ToString();
                }
                this.gdvCreditCards.DataSource = dt;
                this.gdvCreditCards.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Merchant_Status()
        {
            try
            {
                int merchant_id = int.Parse(this.lblMerchantId.Text);
                int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);
                int status_id = int.Parse(this.ddlMerchantStatus.SelectedValue);

                DAL.Merchant merchant_dal = new DAL.Merchant();
                merchant_dal.merchant_id = merchant_id;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_dal);
                merchant_bll.Set_Merchant_Status(status_id);

                if (merchant_bll.successful)
                {
                    Set_Properties(merchant_contact_id);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "err_msg",
                                "alert('" + merchant_bll.system_error_dal.message.ToString() + "');",
                                true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Merchant()
        {
            try
            {
                int merchant_id = int.Parse(this.lblMerchantId.Text);
                int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);
                int max_active_deals = int.Parse(this.txtMaxActiveDeals.Text);
                string yelp_business_id = this.txtYelpId.Text;
                int rating_id = int.Parse(this.ddlRating.SelectedValue);

                DAL.Merchant_Rating merchant_rating_dal = new DAL.Merchant_Rating();
                if (rating_id == 0)
                    merchant_rating_dal.rating_id = System.Data.SqlTypes.SqlInt32.Null;
                else
                    merchant_rating_dal.rating_id = rating_id;

                DAL.Merchant merchant_dal = new DAL.Merchant();
                merchant_dal.merchant_id = merchant_id;
                merchant_dal.max_active_deals = max_active_deals;
                merchant_dal.yelp_business_id = yelp_business_id;
                merchant_dal.merchant_rating_dal = merchant_rating_dal;

                BLL.Merchant merchant_bll = new BLL.Merchant();
                merchant_bll.Update_Merchant_By_Admin(merchant_dal);

                if (merchant_bll.successful)
                {
                    Set_Properties(merchant_contact_id);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "err_msg",
                                "alert('" + merchant_bll.system_error_dal.message.ToString() + "');",
                                true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Approve()
        {
            try
            {
                int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);

                DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                merchant_contact_dal.merchant_contact_id = merchant_contact_id;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                merchant_bll.Approve_Merchant_Contact();

                if (merchant_bll.successful)
                {
                    Set_Properties(merchant_contact_id);
                }
                else
                {
                    string error = merchant_bll.system_error_dal.message.ToString();
                    error = "** Error **";

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please enter a " + error + "description');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Merchant_Contact_Status()
        {
            try
            {
                int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);
                int status_id = int.Parse(this.ddlMerchantContactStatus.SelectedValue);

                DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                merchant_contact_dal.merchant_contact_id = merchant_contact_id;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                merchant_bll.Set_Merchant_Contact_Status(status_id);

                if (merchant_bll.successful)
                {
                    Set_Properties(merchant_contact_id);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                "err_msg",
                                "alert('" + merchant_bll.system_error_dal.message.ToString() + "');",
                                true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Add_Credit_Card()
        {
            try
            {
                int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);

                Response.Redirect("../merchant_credit_card_add/?merchant_contact_id=" + merchant_contact_id.ToString(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Set_Properties(int merchant_contact_id)
        {
            try
            {
                DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                merchant_contact_dal.merchant_contact_id = merchant_contact_id;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);

                DAL.Contact contact_dal = merchant_bll.contact_dal;

                this.lblMerchantId.Text = merchant_bll.merchant_dal.merchant_id.ToString();
                this.lblCompanyName.Text = merchant_bll.merchant_dal.company_name.ToString();
                this.lblIndustry.Text = merchant_bll.merchant_dal.industry_dal.industry.ToString();
                this.lblDescription.Text = merchant_bll.merchant_dal.description.ToString();
                this.lblAddress1.Text = merchant_bll.merchant_dal.address_1.ToString() + " " + merchant_bll.merchant_dal.address_2.ToString();
                this.lblAddress2.Text = merchant_bll.merchant_dal.zip_code_dal.city_dal.city.ToString() + ", " + merchant_bll.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString() + "  " + merchant_bll.merchant_dal.zip_code_dal.zip_code.ToString();
                this.lblPhone.Text = merchant_bll.merchant_dal.phone.ToString();
                this.lblWebsite.Text = merchant_bll.merchant_dal.website.ToString();
                this.imgLogo.ImageUrl = merchant_bll.merchant_dal.image.ToString();
                this.ddlMerchantStatus.SelectedIndex = this.ddlMerchantStatus.Items.IndexOf(this.ddlMerchantStatus.Items.FindByText(merchant_bll.merchant_dal.merchant_status_dal.status.ToString()));
                this.txtMaxActiveDeals.Text = merchant_bll.merchant_dal.max_active_deals.ToString();
                this.txtYelpId.Text = merchant_bll.merchant_dal.yelp_business_id.ToString();
                this.ddlRating.SelectedIndex = this.ddlRating.Items.IndexOf(this.ddlRating.Items.FindByText(merchant_bll.merchant_dal.merchant_rating_dal.rating.ToString()));
                this.lblMerchantContactId.Text = merchant_bll.merchant_contact_dal.merchant_contact_id.ToString();
                this.lblContactName.Text = contact_dal.first_name.ToString() + " " + contact_dal.last_name.ToString();
                this.lblEmail.Text = contact_dal.email.ToString();
                this.lblPhone.Text = contact_dal.phone.ToString();
                this.lblTitle.Text = merchant_bll.merchant_contact_dal.title.ToString();
                this.ddlMerchantContactStatus.SelectedIndex = this.ddlMerchantContactStatus.Items.IndexOf(this.ddlMerchantContactStatus.Items.FindByText(merchant_bll.merchant_contact_dal.merchant_contact_status_dal.status.ToString()));


                Load_Deals(merchant_contact_dal);
                Load_Credit_Cards(merchant_contact_dal);

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