using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


namespace GTSoft.Meddyl.Admin.pages.merchant_search
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
            try
            {
                Search();
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
                BLL.Merchant merchant_bll = new BLL.Merchant();
                string report = Request.QueryString["report"].ToString();

                if (report == "merchant_search")
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

                    if (report == "Merchants_Pending_Approval")
                    {
                        this.lblSearch.Visible = false;
                        this.txtSearch.Visible = false;
                        this.btnSearch.Visible = false;

                        merchant_bll.Get_Merchant_Contacts_Pending_Approval();

                        Load_Grid(merchant_bll.merchant_contact_dal_array, report);
                    }
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

                DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                merchant_contact_dal.search = search;

                BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                merchant_bll.Get_Merchant_Contact_Search();

                Load_Grid(merchant_bll.merchant_contact_dal_array, "Search");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Grid(List<DAL.Merchant_Contact> merchant_contact_dal_array, string report)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Contact Id", Type.GetType("System.String"));
                dt.Columns.Add("Merchant", Type.GetType("System.String"));

                foreach (DAL.Merchant_Contact merchant_contact_row in merchant_contact_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["Contact Id"] = merchant_contact_row.merchant_contact_id.ToString();
                    dt.Rows[dt.Rows.Count - 1]["Merchant"] = merchant_contact_row.merchant_dal.company_name.ToString();
                }
                this.gdvResults.DataSource = dt;
                this.gdvResults.DataBind();
                
                this.gdvResults.Caption = report.Replace("_", " ");
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