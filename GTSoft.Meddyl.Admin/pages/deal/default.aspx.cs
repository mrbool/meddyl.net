using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace GTSoft.Meddyl.Admin.pages.deal
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Approve_Deal();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Reject_Deal();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnPromotion_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Promotion();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnRanking_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Ranking();
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

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                Payment();
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
                int deal_id = int.Parse(Request.QueryString["deal_id"].ToString());

                Load_Deal_Status();

                Set_Properties(deal_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Deal_Status()
        {
            try
            {
                BLL.Deal deal_bll = new BLL.Deal();
                deal_bll.Get_Deal_Status();

                List<DAL.Deal_Status> deal_status_dal_array = deal_bll.deal_status_dal_array;
                this.ddlDealStatus.DataTextField = "status";
                this.ddlDealStatus.DataValueField = "status_id";
                this.ddlDealStatus.DataSource = deal_status_dal_array;
                this.ddlDealStatus.DataBind(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Credit_Cards(int merchant_contact_id)
        {
            try
            {
                DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                merchant_contact_dal.merchant_contact_id = merchant_contact_id;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                merchant_bll.Get_Credit_Cards();

                List<DAL.Credit_Card> credit_card_dal_array = merchant_bll.credit_card_dal_array;
                this.ddlCreditCards.DataTextField = "card_number";
                this.ddlCreditCards.DataValueField = "credit_card_id";
                this.ddlCreditCards.DataSource = credit_card_dal_array;
                this.ddlCreditCards.DataBind();
                this.ddlCreditCards.Items.Insert(0, new ListItem("Select a card", "0"));
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
                int deal_id = int.Parse(this.lblDealId.Text);
                int status_id = int.Parse(this.ddlDealStatus.SelectedValue);

                DAL.Deal deal_dal = new DAL.Deal();
                deal_dal.deal_id = deal_id;

                BLL.Deal deal_bll = new BLL.Deal(deal_dal);
                deal_bll.Set_Deal_Status(status_id);

                if (deal_bll.successful)
                {
                    Set_Properties(deal_id);
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

        private void Update_Ranking()
        {
            try
            {
                if (this.txtRanking.Text.Trim() != "")
                {
                    int deal_id = int.Parse(this.lblDealId.Text);
                    int ranking = int.Parse(this.txtRanking.Text);

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_id;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal);
                    deal_bll.Set_Deal_Ranking(ranking);

                    if (deal_bll.successful)
                    {
                        Set_Properties(deal_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                  "err_msg",
                                  "alert('" + deal_bll.system_error_dal.message.ToString() + "');",
                                  true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "err_msg", "alert('Please enter a ranking');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Promotion()
        {
            try
            {
                if (this.txtPromotion.Text.Trim() != "")
                {
                    int deal_id = int.Parse(this.lblDealId.Text);
                    string promotion_code = this.txtPromotion.Text;

                    DAL.Promotion promotion_dal = new DAL.Promotion();
                    promotion_dal.promotion_code = promotion_code;

                    DAL.Promotion_Activity promotion_activity_dal = new DAL.Promotion_Activity();
                    promotion_activity_dal.promotion_dal = promotion_dal;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_id;
                    deal_dal.promotion_activity_dal = promotion_activity_dal;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal);
                    deal_bll.Set_Deal_Promotion();

                    if (deal_bll.successful)
                    {
                        decimal deal_amount = decimal.Parse(deal_bll.deal_dal.deal_amount.ToString());

                        Set_Properties(deal_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                  "err_msg",
                                  "alert('" + deal_bll.system_error_dal.message.ToString() + "');",
                                  true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "err_msg", "alert('Please enter a promotion');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Approve_Deal()
        {
            try
            {
                if (!CoreDotNet.Utilities.IsNumeric(this.txtApproveRanking.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                              "err_msg",
                              "alert('You must enter a deal ranking');",
                              true);
                }
                else
                {
                    int deal_id = int.Parse(this.lblDealId.Text);
                    int ranking = int.Parse(this.txtApproveRanking.Text);

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_id;
                    deal_dal.ranking = ranking;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal);
                    deal_bll.Approve_Deal();

                    if (deal_bll.successful)
                    {
                        Set_Properties(deal_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                  "err_msg",
                                  "alert('ERROR');",
                                  true);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Reject_Deal()
        {
            try
            {
                if (this.txtRejectDesc.Text.Trim() != "")
                {
                    int deal_id = int.Parse(this.lblDealId.Text);
                    string reject_desc = this.txtRejectDesc.Text;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_id;

                    DAL.Deal_Log deal_log_dal = new DAL.Deal_Log();
                    deal_log_dal.notes = reject_desc;
                    deal_log_dal.deal_dal = deal_dal;

                    BLL.Deal deal_bll = new BLL.Deal(deal_log_dal);
                    deal_bll.Reject_Deal();

                    if (deal_bll.successful)
                    {
                        Set_Properties(deal_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                  "err_msg",
                                  "alert('Dispatch assignment saved, but you forgot to click Confirm or Cancel!)');",
                                  true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                        "err_msg", "alert('Please enter a rejection description');", true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Payment()
        {
            try
            {
                if (int.Parse(this.ddlCreditCards.SelectedValue) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Please select a credit card');",
                                    true);
                }
                else
                {
                    int deal_id = int.Parse(this.lblDealId.Text);

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = int.Parse(this.ddlCreditCards.SelectedValue.ToString());

                    DAL.Deal_Payment deal_payment_dal = new DAL.Deal_Payment();
                    deal_payment_dal.deal_dal = deal_dal;
                    deal_payment_dal.credit_card_dal = credit_card_dal;

                    BLL.Deal deal_bll = new BLL.Deal(deal_payment_dal);
                    deal_bll.Deal_Payment("single");

                    if (deal_bll.successful)
                    {
                        Set_Properties(deal_id);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                                    "err_msg",
                                    "alert('" + deal_bll.system_error_dal.message.ToString() + "');",
                                    true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Set_Properties(int deal_id)
        {
            try
            {
                DAL.Deal deal_dal = new DAL.Deal();
                deal_dal.deal_id = deal_id;

                BLL.Deal deal_bll = new BLL.Deal(deal_dal);

                this.lblDealId.Text = deal_id.ToString();
                this.hplCompanyName.Text = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                this.hplCompanyName.NavigateUrl = "../merchant/?merchant_contact_id=" + deal_bll.deal_dal.merchant_contact_dal.merchant_contact_id.ToString();
                this.lblDeal.Text = deal_bll.deal_dal.deal.ToString();
                this.lblFinePrint.Text = deal_bll.deal_dal.fine_print_ext.ToString();
                this.lblExpirationDate.Text = deal_bll.deal_dal.expiration_date.ToString();
                this.lblCertificatesIssued.Text = deal_bll.deal_dal.certificate_quantity.ToString();
                this.imgDeal.ImageUrl = deal_bll.deal_dal.image.ToString();
                this.txtRanking.Text = deal_bll.deal_dal.ranking.ToString();
                if (deal_bll.deal_dal.promotion_activity_dal.promotion_dal != null)
                    this.txtPromotion.Text = deal_bll.deal_dal.promotion_activity_dal.promotion_dal.promotion_code.ToString();

                this.ddlDealStatus.SelectedIndex = this.ddlDealStatus.Items.IndexOf(this.ddlDealStatus.Items.FindByText(deal_bll.deal_dal.deal_status_dal.status.ToString()));

                if (deal_bll.deal_dal.deal_status_dal.status_id == 4)
                    this.btnApprove.Enabled = true;
                else
                    this.btnApprove.Enabled = false;


                Load_Credit_Cards(int.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_contact_id.ToString()));
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