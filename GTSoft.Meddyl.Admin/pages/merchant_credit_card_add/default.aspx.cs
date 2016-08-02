using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;


namespace GTSoft.Meddyl.Admin.pages.merchant_credit_card_add
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

        protected void btnAddCard_Click(object sender, EventArgs e)
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

                this.lblMerchantContactId.Text = merchant_contact_id.ToString();


                this.txtCardHolderName.Text = "George Triarhos";
                this.txtCardNumber.Text = "4147202229308208";
                this.txtExpDate.Text = "0918";
                this.txtZipCode.Text = "92014";
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
                if (this.txtCardHolderName.Text.Trim() != "")
                {
                    int merchant_contact_id = int.Parse(this.lblMerchantContactId.Text);
                    string card_holder_name = this.txtCardHolderName.Text;
                    string card_number = this.txtCardNumber.Text;
                    string expiration_date = this.txtExpDate.Text;
                    string billing_zip_code = this.txtZipCode.Text;

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.card_holder_name = card_holder_name.Trim();
                    credit_card_dal.card_number = card_number.Trim();
                    credit_card_dal.expiration_date = expiration_date.Trim();
                    credit_card_dal.billing_zip_code = billing_zip_code.Trim();
                    credit_card_dal.merchant_contact_dal = merchant_contact_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(credit_card_dal);
                    merchant_bll.Add_Credit_Card();

                    if (merchant_bll.successful)
                    {
                        Response.Redirect("../merchant/?merchant_contact_id=" + merchant_contact_id.ToString(), false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                  "err_msg",
                                  "alert('Error!)');",
                                  true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "err_msg", "alert('Missing data entry');", true);
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