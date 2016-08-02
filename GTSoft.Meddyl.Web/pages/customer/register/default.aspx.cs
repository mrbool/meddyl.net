using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GTSoft.Meddyl;
using System.Net;

namespace GTSoft.Meddyl.Web.customer.register
{
    public partial class _default : System.Web.UI.Page
    {
        #region constructors

        public _default()
        {
            try
            {
                system_bll = new BLL.System();
                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();
            }
            catch(Exception ex)
            {

                string message = ex.Message.Replace("\r\n", " ").Replace("\n", " ");
                HttpContext.Current.Response.Redirect("../error/?message=" + message, false);
            }
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
                if (!IsPostBack)
                {
                    //this.txtFirstName.Text = "George";
                    //this.txtLastName.Text = "Triarhos";
                    //this.txtEmail.Text = "gtriarhos@hotmail.com";
                    //this.txtPassword.Text = "test12";
                    //this.txtZipCode.Text = "92037";
                    
                    Load_Form_Data();
                }
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void imbFacebookSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Register_With_Facebook();
            }
            catch (Exception ex)
            {
                Error_Page(ex);
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                Register();
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
                if (Request.QueryString["code"] != null)
                {
                    string access_token = "";
                    string state = Request.QueryString["state"].ToString();
                    string code = Request.QueryString["code"];
                    string zip_code = state.Substring(0, state.IndexOf("."));
                    string promotion_code = state.Substring(state.IndexOf(".") + 1);

                    if (promotion_code.IndexOf(",") != -1)
                        promotion_code = promotion_code.Substring(0, promotion_code.IndexOf(","));

                    //access_token = "EAAMQxSneVU0BAAL6zgG827Bk18icGoBOER1tS0jZA7YARqWdL7y0FXePtj1OSCVFOiw4GblZBb0roFnsKowqJ2XuW0WqeI2uAd9eVW9KDXlyNkeWqrCzUS1X3GfH8RG9ZC22ZBVEvqK0uNbLjirbPEbIm4aicC3V4PkBdpc6WwZDZD";

                    if (access_token == "")
                    {
                        // Exchange the code for an access token
                        //Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + system_bll.system_settings_dal.fb_app_id.ToString() + "&client_secret=" + system_bll.system_settings_dal.fb_app_secret.ToString() + "&redirect_uri=http://web.meddyl.com/pages/customer/register/default.aspx&code=" + code);
                        Uri targetUri = new Uri("https://graph.facebook.com/oauth/access_token?client_id=" + system_bll.system_settings_dal.fb_app_id.ToString() + "&client_secret=" + system_bll.system_settings_dal.fb_app_secret.ToString() + "&redirect_uri=" + system_bll.system_settings_dal.fb_redirect_url.ToString() + "&code=" + code);
                        HttpWebRequest at = (HttpWebRequest)HttpWebRequest.Create(targetUri);

                        System.IO.StreamReader str = new System.IO.StreamReader(at.GetResponse().GetResponseStream());
                        string token = str.ReadToEnd().ToString().Replace("access_token=", "");

                        // Split the access token and expiration from the single string
                        string[] combined = token.Split('&');
                        access_token = combined[0];
                    }

                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = 7;
                    application_type_dal.version = "1.9.0";

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.ip_address = "0.0.0.0";
                    login_log_dal.auto_login = false;
                    login_log_dal.auth_token = access_token;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = zip_code;
                    zip_code_dal.latitude = 0;
                    zip_code_dal.longitude = 0;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;
                    customer_dal.login_log_dal = login_log_dal;

                    DAL.Promotion promotion_dal = new DAL.Promotion();
                    promotion_dal.promotion_code = promotion_code;
                    promotion_dal.customer_dal = customer_dal;

                    BLL.Customer customer_bll = new BLL.Customer(promotion_dal);
                    customer_bll.Login_With_Facebook("", access_token);

                    successful = customer_bll.successful;
                    system_successful_dal = customer_bll.system_successful_dal;
                    system_error_dal = customer_bll.system_error_dal;

                    if (successful)
                    {
                        Response.Redirect("../download/?message=" + system_successful_dal.message.ToString(), false);
                    }
                    else
                    {
                        Clear_Values();
                        this.lblFacebookError.Text = "* " + system_error_dal.message.ToString();
                    }
                }
                else
                {
                    if (Request.QueryString["promotion_code"] != null)
                    {
                        this.txtPromoCode.Text = Request.QueryString["promotion_code"].ToString();
                        this.txtPromoCode.Text = Request.QueryString["promotion_code"].ToString();
                    }

                    HyperLink1.Visible = false;
                    HyperLink1.NavigateUrl = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + system_bll.system_settings_dal.fb_app_id.ToString() + "&redirect_uri=" + system_bll.system_settings_dal.fb_redirect_url.ToString() + "&response_type=code&state=1";
                    //HyperLink1.NavigateUrl = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=862863927170381&redirect_uri=http://web.meddyl.com/pages/customer/register/default.aspx&response_type=code&state=1";
                    //HyperLink1.NavigateUrl = "http://localhost:56203/pages/customer/register/default.aspx?code=AQD8D8XeEKpjoHXIicHXsmBdC-rxEcELI2HI144USJTyrBXYdKQOocwtfygJKYQ8Yqo5q1l3etQH1c5UkOpq150ThqYYudKoYbc0bqYzAE8-0hvhSwMbpY9j0DmUyE6Q4a574MVD_JQ5VsfDrY1x0McmpwIKwMKuvOSkXMdiBcOrnPurfBvzXTWWa3LJsD5gDlodOQ5cqqSup1d02RCnqbvJEVKVBeviygJ0v41wtAkIbN2zUQkLpiOsOOqpjX1I4es1PvdeAfDJPLgtLT0zdsczwsaAJvc5LS7yP7lzzLPcgmhNKXKhfdWgntx9bgOYokYAwo4e-QovGr4zqnBwhjoO&state=1#_=_";
                    //HyperLink1.Text = "Login with Facebook";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Register_With_Facebook()
        {
            try
            {
                string url;
                string promotion_code = this.txtPromoCode.Text.Trim();
                string zip_code = this.txtFacebookZipCode.Text.Trim();

                Clear_Values();

                if (zip_code.Length != 5)
                {
                    this.lblFacebookError.Text = "* You must enter a valid zip code";
                }
                else
                {
                    url = "https://www.facebook.com/v2.4/dialog/oauth/?client_id=" + system_bll.system_settings_dal.fb_app_id.ToString() + "&redirect_uri=" + system_bll.system_settings_dal.fb_redirect_url.ToString() + "&scope=public_profile,email,user_friends&response_type=code&state=" + zip_code + "." + promotion_code;
                    //url = "http://localhost:56203/pages/customer/register/default.aspx?state=" + zip_code + "." + promotion_code + "&code=AQDUUqijrlj-eA_VQ2SsaEA2yYfdADRa_WxIesL1zC7BZpNx8vKNPZU1pepzvF05X0PX_HHoy2y_FWtEKfC6RYJxrBa_ti6dUNCAM_wx_xPkyiasunQYan2xWGyuZ9IsZMxjKADH4J0tQzIgTgyWtDu4IgQ7f5kDsdN85MIFrBeSVlFcVQ-pqKgyf7oiL6Fb7hNdZBiQTZlRWr073S9_eI6Ejja0NX8-Jl-jXGurOZW3Rlr6uYvNLeu-PVwrWFHo93CE4FQPcp4R9Yb0bEdCt8Ao0RZFWNYL_WLFX04rD1xdcq_egMJmXkgFs7o9VK7bvX2xoAsZRGVpiXJyDByA7tzz&state=1#_=_";

                    Response.Redirect(url, false);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Register()
        {
            try
            {
                string first_name = this.txtFirstName.Text.Trim();
                string last_name = this.txtLastName.Text.Trim();
                string email = this.txtEmail.Text.Trim();
                string password = this.txtPassword.Text.Trim();
                string zip_code = this.txtZipCode.Text.Trim();
                string promotion_code = this.txtPromoCode.Text.Trim();

                Clear_Values();

                if (first_name.Length == 0)
                {
                    this.lblError.Text = "* First name is required";
                } 
                else if (last_name.Length == 0)
                {
                    this.lblError.Text = "* Last name is required";
                } 
                else if (password.Length < 5)
                {
                    this.lblError.Text = "* Password length must be at least 5 characters";
                }
                else if (zip_code.Length != 5)
                {
                    this.lblError.Text = "* Zip code is required";
                } 
                else
                {
                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = 7;
                    application_type_dal.version = "1.9.0";

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.ip_address = "0.0.0.0";
                    login_log_dal.auto_login = false;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = zip_code;
                    zip_code_dal.latitude = 0;
                    zip_code_dal.longitude = 0;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.first_name = first_name;
                    contact_dal.last_name = last_name;
                    contact_dal.email = email;
                    contact_dal.user_name = email;
                    contact_dal.password = password;
                    contact_dal.phone = "";
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;
                    customer_dal.login_log_dal = login_log_dal;

                    DAL.Promotion promotion_dal = new DAL.Promotion();
                    promotion_dal.promotion_code = promotion_code;
                    promotion_dal.customer_dal = customer_dal;

                    BLL.Customer customer_bll = new BLL.Customer(promotion_dal);
                    customer_bll.Register();

                    successful = customer_bll.successful;
                    system_successful_dal = customer_bll.system_successful_dal;
                    system_error_dal = customer_bll.system_error_dal;

                    if (successful)
                    {
                        Response.Redirect("../download/?message=" + system_successful_dal.message.ToString(), false);
                    }
                    else
                    {
                        this.lblError.Text = "* " + system_error_dal.message.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear_Values()
        {
            try
            {
                this.lblFacebookError.Text = "";
                this.lblError.Text = "";
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


        #region properties

        public bool successful { get; set; }
        public string code { get; set; }
        public BLL.System system_bll { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        #endregion


    }
}