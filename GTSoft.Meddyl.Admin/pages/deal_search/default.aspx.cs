using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


namespace GTSoft.Meddyl.Admin.pages.deal_search
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        #endregion


        #region private methods

        private void Load_Form_Data()
        {
            try
            {
                BLL.Deal deal_bll = new BLL.Deal();
                string report = Request.QueryString["report"].ToString();

                if (report == "deal_search")
                {
                    this.lblSearch.Visible = true;
                    this.txtSearch.Visible = true;
                    this.btnSearch.Visible = true;
                }
                else
                {
                    this.lblSearch.Visible = false;
                    this.txtSearch.Visible = false;
                    this.btnSearch.Visible = false;

                    if (report == "Deals_Pending_Approval")
                    {
                        deal_bll.Get_Deals_Pending_Approval();
                    }
                    else if (report == "Deals_Pending_Payment")
                    {
                        deal_bll.Get_Deals_Pending_Payment();
                    }

                    Load_Grid(deal_bll.deal_dal_array, report);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Search()
        {
            try
            {
                string search = this.txtSearch.Text;

                DAL.Deal deal_dal = new DAL.Deal();
                deal_dal.search =  search;

                BLL.Deal deal_bll = new BLL.Deal(deal_dal);
                deal_bll.Get_Deals_Search();

                if (deal_bll.successful)
                {
                    Load_Grid(deal_bll.deal_dal_array, "Search");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + deal_bll.system_error_dal.message.ToString() + "');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Grid(List<DAL.Deal> deal_dal_array, string report)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Merchant", Type.GetType("System.String"));
                dt.Columns.Add("Deal Id", Type.GetType("System.String"));
                dt.Columns.Add("Deal", Type.GetType("System.String"));

                foreach (DAL.Deal deal_row in deal_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["Merchant"] = deal_row.merchant_contact_dal.merchant_dal.company_name.ToString();
                    dt.Rows[dt.Rows.Count - 1]["Deal Id"] = deal_row.deal_id.ToString();
                    dt.Rows[dt.Rows.Count - 1]["Deal"] = deal_row.deal;
                }
                this.gdvResults.DataSource = dt;
                this.gdvResults.DataBind();

                this.gdvResults.Caption = report.Replace("_", " ");
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