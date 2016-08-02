using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;


namespace GTSoft.Meddyl.Admin.pages.customer_search
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

                if (report == "customer_search")
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

                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.search = search;

                BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                customer_bll.Customer_Search();

                if (customer_bll.successful)
                {
                    Load_Grid(customer_bll.customer_dal_array, "Search");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('" + customer_bll.system_error_dal.message.ToString() + "');", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Grid(List<DAL.Customer> customer_dal_array, string report)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("customer_id", Type.GetType("System.String"));
                dt.Columns.Add("name", Type.GetType("System.String"));
                dt.Columns.Add("email", Type.GetType("System.String"));
                dt.Columns.Add("status", Type.GetType("System.String"));

                foreach (DAL.Customer customer_row in customer_dal_array)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["customer_id"] = customer_row.customer_id.ToString();
                    dt.Rows[dt.Rows.Count - 1]["name"] = customer_row.contact_dal.first_name.ToString() + " " + customer_row.contact_dal.last_name.ToString();
                    dt.Rows[dt.Rows.Count - 1]["email"] = customer_row.contact_dal.email.ToString();
                    dt.Rows[dt.Rows.Count - 1]["status"] = customer_row.customer_status_dal.status.ToString();
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