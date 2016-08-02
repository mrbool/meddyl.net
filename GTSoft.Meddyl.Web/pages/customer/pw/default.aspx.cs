using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTSoft.Meddyl.BLL;

namespace GTSoft.Meddyl.Web.customer.pw
{
    public partial class _default : System.Web.UI.Page
    {
        #region constructors

        public _default()
        {
            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();
        }

        #endregion


        #region events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
                MasterPageFile = "~/pages/customer/master/Mobile.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Load_Form_Data();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Password();
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
                if (Request.QueryString["reset_id"] != null)
                    reset_id = Request.QueryString["reset_id"].ToString();
                else
                    reset_id = "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Update_Password()
        {
            try
            {
                string password = this.txtPassword.Text.Trim();
                string password_confirm = this.txtPasswordConfirm.Text.Trim();

                if (password != password_confirm)
                {
                    this.lblError.Text = "Passwords do not match";
                }
                else if(password.Length < 5)
                {
                    this.lblError.Text = "Password length must be at least 5 characters";
                }
                else
                {
                    BLL.Contact contact_bll = new Contact();
                    contact_bll.Reset_Password(password, reset_id);

                    successful = contact_bll.successful;
                    system_successful_dal = contact_bll.system_successful_dal;
                    system_error_dal = contact_bll.system_error_dal;

                    if (successful)
                        Response.Redirect("../successful/?message=" + system_successful_dal.message.ToString(), false);
                    else
                        this.lblError.Text = "* " + system_error_dal.message.ToString();
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


        #region properties

        public bool successful { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        public string reset_id { get; set; }

        #endregion
    }
}