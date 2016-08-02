using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace GTSoft.Meddyl.Admin.pages.login
{
    public partial class _default : System.Web.UI.Page
    {
        #region events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
                lnkCSS.Attributes["href"] = "~/css/mobile_style.css";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //if (Request.Browser.IsMobileDevice)
                HtmlMeta meta_content = new HtmlMeta();
                meta_content.Name = "viewport";
                meta_content.Content = "width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0";
                this.Page.Header.Controls.Add(meta_content);

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

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            Login_User();
        }

        #endregion


        #region private methods

        private void Load_Form_Data()
        {
            try
            {
                this.lblError.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Login_User()
        {
            try
            {
                string user_name = this.txtUserName.Text;
                string password = this.txtPassword.Text;

                BLL.System system_bll = new BLL.System();
                system_bll.Login(user_name, password);

                if (!system_bll.system_error_dal.code.IsNull)
                {
                    this.lblError.Visible = true;
                    this.lblError.Text = system_bll.system_error_dal.message.ToString();
                }
                else
                {
                    this.lblError.Visible = false;
                    this.lblError.Text = "";
                    Session["user_id"] = system_bll.system_users_dal.user_id;
                    Response.Redirect("../menu/default.aspx");
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