using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


namespace GTSoft.Meddyl.Admin.pages.certificate_search
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

                if (report == "certificate_search")
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

                DAL.Certificate certificate_dal = new DAL.Certificate();
                certificate_dal.search = search;

                BLL.Deal deal_bll = new BLL.Deal(certificate_dal);
                deal_bll.Certificate_Search();

                if (deal_bll.successful)
                {
                    Load_Grid(deal_bll.certificate_dal_array, "Search");
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

        private void Load_Grid(List<DAL.Certificate> certificate_dal_array, string report)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("certificate_id", Type.GetType("System.String"));
                dt.Columns.Add("company_name", Type.GetType("System.String"));
                dt.Columns.Add("status", Type.GetType("System.String"));
                dt.Columns.Add("email", Type.GetType("System.String"));

                foreach (DAL.Certificate certificate_row in certificate_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["certificate_id"] = certificate_row.certificate_id.ToString();
                    dt.Rows[dt.Rows.Count - 1]["company_name"] = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                    dt.Rows[dt.Rows.Count - 1]["status"] = certificate_row.certificate_status_dal.status.ToString();
                    if (certificate_row.customer_dal != null)
                        dt.Rows[dt.Rows.Count - 1]["email"] = certificate_row.customer_dal.contact_dal.email.ToString();
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