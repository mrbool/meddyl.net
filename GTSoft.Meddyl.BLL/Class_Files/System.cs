using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using GTSoft.Meddyl.DAL;
using GTSoft.CoreDotNet;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace GTSoft.Meddyl.BLL
{
    public class System
    {

        #region constructors

        public System()
        {
            system_error_dal = new System_Error();
            system_successful_dal = new System_Successful();

            Load_System_Settings();
        }

        #endregion


        #region public methods

        public void Check_Version_Maintenance_And_Login(DAL.Login_Log _login_log_dal,  bool check_login)
        {
            String current_version, min_version = "", down_message = "";
            bool is_down = false, is_active_user = false;
            int status_id=0;
            int current_major, current_minor, current_build;
            int min_major, min_minor, min_build;

            if (!check_login)
            {
                is_active_user = true;
            }
            else
            {
                DataTable dt_user=null;

                if (_login_log_dal.customer_id != 0)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = _login_log_dal.customer_id;
                    dt_user = customer_dal.usp_Customer_SelectPK_customer_id();
                    foreach (DataRow dr_user in dt_user.Rows)
                    {
                        status_id = int.Parse(dr_user["Customer_status_id"].ToString());
                    }

                    if (status_id == 1)
                        is_active_user = true;
                }
                else if (_login_log_dal.merchant_contact_id != 0)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = _login_log_dal.merchant_contact_id;
                    dt_user = merchant_contact_dal.usp_Merchant_Contact_SelectPK_merchant_contact_id();
                    foreach (DataRow dr_user in dt_user.Rows)
                    {
                        status_id = int.Parse(dr_user["Merchant_Contact_status_id"].ToString());
                    }

                    if (status_id == 1 || status_id == 4 || status_id == 3)
                        is_active_user = true;
                }
                else
                {
                    is_active_user = false;
                }
            }

            current_version = _login_log_dal.application_type_dal.version.ToString();

            DAL.Application_Type application_type_dal = new DAL.Application_Type();
            application_type_dal.application_type_id = _login_log_dal.application_type_dal.application_type_id;
            DataTable dt = application_type_dal.SelectPK_application_type_id();
            foreach(DataRow dr in dt.Rows)
            {
                min_version = dr["Application_Type_version"].ToString();
                is_down = bool.Parse(dr["Application_Type_is_down"].ToString());
                down_message = dr["Application_Type_down_message"].ToString();
            }
            
            current_major = int.Parse(current_version.Substring(0, Utilities.NthIndexOf(current_version, ".", 1)));
            current_minor = int.Parse(current_version.Substring(Utilities.NthIndexOf(current_version, ".", 1) + 1, Utilities.NthIndexOf(current_version, ".", 2) - (Utilities.NthIndexOf(current_version, ".", 1) + 1)));
            current_build = int.Parse(current_version.Substring(Utilities.NthIndexOf(current_version, ".", 2) + 1, current_version.Length - (Utilities.NthIndexOf(current_version, ".", 2) + 1)));

            min_major = int.Parse(min_version.Substring(0, Utilities.NthIndexOf(min_version, ".", 1)));
            min_minor = int.Parse(min_version.Substring(Utilities.NthIndexOf(min_version, ".", 1) + 1, Utilities.NthIndexOf(min_version, ".", 2) - (Utilities.NthIndexOf(min_version, ".", 1) + 1)));
            min_build = int.Parse(min_version.Substring(Utilities.NthIndexOf(min_version, ".", 2) + 1, min_version.Length - (Utilities.NthIndexOf(min_version, ".", 2) + 1)));
            
            if(is_down)
            {
                successful = false;
                system_error_dal = Get_System_Error(5000, "");
            }
            else if ((current_major < min_major) ||
                (current_major == min_major) && (current_minor < min_minor) ||
                (current_major == min_major) && (current_major == min_major) && (current_build < min_build))
            {
                successful = false;
                system_error_dal = Get_System_Error(5001, "");
            }
            else if (!is_active_user)
            {
                successful = false;
                system_error_dal = Get_System_Error(5002, "");
            }
            else
            {
                successful = true;
            }
        }

        public void Get_Application_Settings(int application_type_id)
        {
            try
            {
                application_type_dal = new DAL.Application_Type();
                application_type_dal.application_type_id = application_type_id;
                DataTable dt = application_type_dal.SelectPK_application_type_id();
                foreach (DataRow dr in dt.Rows)
                {
                    application_type_dal.version = dr["Application_Type_version"].ToString();
                    application_type_dal.is_down = bool.Parse(dr["Application_Type_is_down"].ToString());
                    application_type_dal.down_message = dr["Application_Type_down_message"].ToString();
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Industry_Parent_Level(int industry_id)
        {
            try
            {
                industry_dal_array = new List<Industry>();

                DAL.Industry industry_dal = new Industry();
                if (industry_id != 0)
                    industry_dal.parent_industry_id = industry_id;
                
                DataTable dt = industry_dal.usp_Industry_SelectFK_Industry_industry_id();
                foreach (DataRow dr in dt.Rows)
                {
                    industry_dal = new DAL.Industry();
                    industry_dal.industry_id = int.Parse(dr["Industry_industry_id"].ToString());
                    industry_dal.industry = dr["Industry_industry"].ToString();
                    industry_dal.image = dr["Industry_image"].ToString();
                    
                    industry_dal_array.Add(industry_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save_Image(string image_base64, int id, string image_type)
        {
            try
            {               
                string image_folder = system_settings_dal.image_folder.ToString() + image_type + @"\";
                string saved_file_name = id.ToString() + ".jpg";
                string saved_full_file_name = image_folder + saved_file_name;

                if (!Directory.Exists(image_folder))
                    Directory.CreateDirectory(image_folder);

                if (File.Exists(saved_full_file_name))
                    File.Delete(saved_full_file_name);

                byte[] logo_bytes = Convert.FromBase64String(image_base64);
                File.WriteAllBytes(saved_full_file_name, logo_bytes);

                return saved_file_name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_System_Pending()
        {
            try
            {
                system_settings_dal_array = new List<DAL.System_Settings>();

                DataTable dt = system_settings_dal.usp_System_Pendings();
                foreach (DataRow dr in dt.Rows)
                {
                    system_settings_dal = new System_Settings();
                    system_settings_dal.report = dr["report"].ToString();
                    system_settings_dal.quantity = int.Parse(dr["quantity"].ToString());

                    system_settings_dal_array.Add(system_settings_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send_Email(string body, string[] to_emails)
        {            
            try
            {
                if (system_settings_dal.email_on)
                {
                    if (system_settings_dal.email_system == "mailgun")
                    {
                        string domain = system_settings_dal.mailgun_domain.ToString();
                        string api_private_key = system_settings_dal.mailgun_api_private_key.ToString();
                        string api_public_key = system_settings_dal.mailgun_api_public_key.ToString();
                        string url = system_settings_dal.mailgun_url.ToString();

                        CoreDotNet.Mailgun mailgun = new Mailgun(domain, api_private_key, api_public_key, url);
                        mailgun.to_emails = to_emails;
                        mailgun.from = email_template_dal.from_email.ToString();
                        mailgun.from_display = email_template_dal.from_display.ToString();
                        mailgun.subject = email_template_dal.subject.ToString();
                        mailgun.body = body;
                        mailgun.is_html = bool.Parse(email_template_dal.is_html.ToString());;
                        mailgun.Send_Mail();
                    }
                    else if (system_settings_dal.email_system == "smtp")
                    {
                        GTSoft.CoreDotNet.SMTP mail = new CoreDotNet.SMTP();
                        mail.server = system_settings_dal.smtp_server.ToString();
                        mail.port = int.Parse(system_settings_dal.smtp_port.ToString());
                        mail.from = email_template_dal.from_email.ToString();
                        mail.from_display = email_template_dal.from_display.ToString();
                        mail.subject = email_template_dal.subject.ToString(); ;
                        mail.body = body;
                        mail.to_emails = to_emails;
                        mail.Send_Mail();
                    }
                }

                successful = true;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = Get_System_Error(1021, ex.Message.ToString());
            }
        }

        public DAL.Email_Template Get_Email_Template(int template_id)
        {
            try
            {
                DAL.Email_Template email_template_dal = new Email_Template();
                email_template_dal.template_id = template_id;
                DataTable dt = email_template_dal.SelectPK_template_id();
                foreach (DataRow dr in dt.Rows)
                {
                    email_template_dal.from_email = dr["Email_Template_from_email"].ToString();
                    email_template_dal.from_display = dr["Email_Template_from_display"].ToString();
                    email_template_dal.subject = dr["Email_Template_subject"].ToString();
                    email_template_dal.body = dr["Email_Template_body"].ToString();
                    email_template_dal.is_html = bool.Parse(dr["Email_Template_is_html"].ToString());
                }

                successful = true;

                return email_template_dal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Validate_Email(string email)
        {
            try
            {
                if (system_settings_dal.email_validation)
                {
                    if (system_settings_dal.email_system == "mailgun")
                    {
                        string domain = system_settings_dal.mailgun_domain.ToString();
                        string api_private_key = system_settings_dal.mailgun_api_private_key.ToString();
                        string api_public_key = system_settings_dal.mailgun_api_public_key.ToString();
                        string url = system_settings_dal.mailgun_url.ToString();

                        CoreDotNet.Mailgun mailgun = new Mailgun(domain, api_private_key, api_public_key, url);
                        return mailgun.Validate_Email(email);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DAL.SMS_Template Get_SMS_Template(int template_id)
        {
            try
            {
                DAL.SMS_Template sms_template_dal = new SMS_Template();
                sms_template_dal.template_id = template_id;
                DataTable dt = sms_template_dal.SelectPK_template_id();
                foreach (DataRow dr in dt.Rows)
                {
                    sms_template_dal.body = dr["SMS_Template_body"].ToString(); ;
                }

                successful = true;

                return sms_template_dal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send_Text(string phone_to, string body)
        {
            try
            {
                string phone_from = "";

                if (system_settings_dal.sms_on)
                {
                    if (system_settings_dal.sms_system == "twilio")
                    {
                        DAL.Twilio_Phone_Number twilio_phone_number_dal = new DAL.Twilio_Phone_Number();
                        DataTable dt = twilio_phone_number_dal.usp_Twilio_Phone_Number_Current();
                        foreach(DataRow dr in dt.Rows)
                        {
                            phone_from = dr["Twilio_Phone_Number_phone"].ToString();
                        }

                        CoreDotNet.Twilio twilio = new Twilio(system_settings_dal.twilio_account_sid.ToString(), system_settings_dal.twilio_auth_token.ToString());
                        twilio.Send_SMS(phone_from, phone_to, body);

                        successful = twilio.successful;
                        system_error_dal = Get_System_Error(1020, twilio.message);
                    }
                    else if(system_settings_dal.sms_system == "plivo")
                    {
                        DAL.Plivo_Phone_Number plivo_phone_number_dal = new DAL.Plivo_Phone_Number();
                        DataTable dt = plivo_phone_number_dal.usp_Plivo_Phone_Number_Current();
                        foreach (DataRow dr in dt.Rows)
                        {
                            phone_from = dr["Plivo_Phone_Number_phone"].ToString();
                        }

                        CoreDotNet.Plivo plivo = new CoreDotNet.Plivo(system_settings_dal.plivo_auth_id.ToString(), system_settings_dal.plivo_auth_token.ToString());
                        plivo.Send_SMS(phone_from, phone_to, body);

                        successful = plivo.successful;
                        system_error_dal = Get_System_Error(1020, plivo.message);
                    }
                    else
                    {
                        successful = false;
                        system_error_dal = Get_System_Error(1020, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DAL.System_Successful Get_System_Successful(int code, string message_additional)
        {
            try
            {
                DAL.System_Successful system_successful_dal = new DAL.System_Successful();
                system_successful_dal.code = code;
                DataTable dt = system_successful_dal.SelectPK_code();
                foreach (DataRow dr in dt.Rows)
                {
                    system_successful_dal.message = dr["System_Successful_message"].ToString();
                }

                return system_successful_dal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DAL.System_Error Get_System_Error(int code, string additional_message)
        {
            try
            {
                DAL.System_Error system_error_dal = new DAL.System_Error();
                system_error_dal.code = code;
                DataTable dt = system_error_dal.SelectPK_code();

                if(additional_message != "")
                    additional_message = "\r\n\r\n" + additional_message;

                system_error_dal.message = dt.Rows[0]["System_Error_message"].ToString() + additional_message;

                return system_error_dal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Encrypt(string decrypted_value)
        {
            try
            {
                string encrypted_value = "";

                byte[] desKey = new ASCIIEncoding().GetBytes("k&hH%g4B");
                byte[] desIV = new ASCIIEncoding().GetBytes("k&hH%g4B");
                
                GTSoft.CoreDotNet.Security cryptor = new GTSoft.CoreDotNet.Security();

                return encrypted_value = cryptor.DESEncryptString(decrypted_value, desKey, desIV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Decrypt(string encrypted_value)
        {
            try
            {
                string decrypted_value = "";

                byte[] desKey = new ASCIIEncoding().GetBytes("k&hH%g4B");
                byte[] desIV = new ASCIIEncoding().GetBytes("k&hH%g4B");

                GTSoft.CoreDotNet.Security cryptor = new GTSoft.CoreDotNet.Security();
                decrypted_value = cryptor.DESDecryptString(encrypted_value, desKey, desIV);

                return decrypted_value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Login(string user_name, string password)
        {
            try
            {
                system_users_dal = new System_Users();
                system_users_dal.user_name = user_name;
                system_users_dal.password = password;

                DataTable dt = system_users_dal.usp_System_Users_Password_Verify();
                if(dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    Load_System_User(dr);

                    successful = true;
                }
                else
                {
                    successful = false;
                    system_error_dal = Get_System_Error(5006, "");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region private methods

        private void Load_System_Settings()
        {
            try
            {
                system_settings_dal = new DAL.System_Settings();
                DataTable dt = system_settings_dal.usp_System_Settings_SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    system_settings_dal.auth_net_api_login_id = dr["System_Settings_auth_net_api_login_id"].ToString();
                    system_settings_dal.auth_net_transaction_key = dr["System_Settings_auth_net_transaction_key"].ToString();
                    system_settings_dal.braintree_merchant_id = dr["System_Settings_braintree_merchant_id"].ToString();
                    system_settings_dal.braintree_public_key = dr["System_Settings_braintree_public_key"].ToString();
                    system_settings_dal.braintree_private_key = dr["System_Settings_braintree_private_key"].ToString();
                    system_settings_dal.certificate_quantity_info = dr["System_Settings_certificate_quantity_info"].ToString();
                    system_settings_dal.certificate_quantity_max = int.Parse(dr["System_Settings_certificate_quantity_max"].ToString());
                    system_settings_dal.certificate_quantity_min = int.Parse(dr["System_Settings_certificate_quantity_min"].ToString());
                    system_settings_dal.credit_card_system = dr["System_Settings_credit_card_system"].ToString();
                    system_settings_dal.customer_deal_range_max = int.Parse(dr["System_Settings_customer_deal_range_max"].ToString());
                    system_settings_dal.customer_deal_range_min = int.Parse(dr["System_Settings_customer_deal_range_min"].ToString());
                    system_settings_dal.merchant_description_characters = int.Parse(dr["System_Settings_merchant_description_characters"].ToString());
                    system_settings_dal.merchant_description_default = dr["System_Settings_merchant_description_default"].ToString();
                    system_settings_dal.customer_app_android_id = dr["System_Settings_customer_app_android_id"].ToString();
                    system_settings_dal.customer_app_ios_id = dr["System_Settings_customer_app_ios_id"].ToString();
                    system_settings_dal.customer_app_terms = dr["System_Settings_customer_app_terms"].ToString();
                    system_settings_dal.deal_max_ranking = int.Parse(dr["System_Settings_deal_max_ranking"].ToString());
                    system_settings_dal.deal_min_ranking = int.Parse(dr["System_Settings_deal_min_ranking"].ToString());
                    system_settings_dal.deal_needs_credit_card = bool.Parse(dr["System_Settings_deal_needs_credit_card"].ToString());
                    system_settings_dal.deal_new_customer_only = bool.Parse(dr["System_Settings_deal_new_customer_only"].ToString());
                    system_settings_dal.deal_new_customer_only_info = dr["System_Settings_deal_new_customer_only_info"].ToString();
                    system_settings_dal.deal_use_immediately = bool.Parse(dr["System_Settings_deal_use_immediately"].ToString());
                    system_settings_dal.deal_use_immediately_info = dr["System_Settings_deal_use_immediately_info"].ToString();
                    system_settings_dal.dollar_value_info = dr["System_Settings_dollar_value_info"].ToString();
                    system_settings_dal.dollar_value_max = decimal.Parse(dr["System_Settings_dollar_value_max"].ToString());
                    system_settings_dal.dollar_value_min = decimal.Parse(dr["System_Settings_dollar_value_min"].ToString());
                    system_settings_dal.deal_validate = bool.Parse(dr["System_Settings_deal_validate"].ToString());
                    system_settings_dal.email_admin = dr["System_Settings_email_admin"].ToString();
                    system_settings_dal.email_on = bool.Parse(dr["System_Settings_email_on"].ToString());
                    system_settings_dal.email_system = dr["System_Settings_email_system"].ToString();
                    system_settings_dal.email_validation = bool.Parse(dr["System_Settings_email_validation"].ToString());
                    system_settings_dal.expiration_days_info = dr["System_Settings_expiration_days_info"].ToString();
                    system_settings_dal.expiration_days_max = int.Parse(dr["System_Settings_expiration_days_max"].ToString());
                    system_settings_dal.expiration_days_min = int.Parse(dr["System_Settings_expiration_days_min"].ToString());
                    system_settings_dal.fb_app_id = dr["System_Settings_fb_app_id"].ToString();
                    system_settings_dal.fb_app_secret = dr["System_Settings_fb_app_secret"].ToString();
                    system_settings_dal.fb_redirect_url = dr["System_Settings_fb_redirect_url"].ToString();
                    system_settings_dal.fb_scope = dr["System_Settings_fb_scope"].ToString();
                    system_settings_dal.fine_print_more_characters = int.Parse(dr["System_Settings_fine_print_more_characters"].ToString());
                    system_settings_dal.fine_print_more_default = dr["System_Settings_fine_print_more_default"].ToString();
                    system_settings_dal.gps_accuracy = dr["System_Settings_gps_accuracy"].ToString();
                    system_settings_dal.gps_timeout = int.Parse(dr["System_Settings_gps_timeout"].ToString());
                    system_settings_dal.image_folder = dr["System_Settings_image_folder"].ToString();
                    system_settings_dal.mailgun_api_private_key = dr["System_Settings_mailgun_api_private_key"].ToString();
                    system_settings_dal.mailgun_api_public_key = dr["System_Settings_mailgun_api_public_key"].ToString();
                    system_settings_dal.mailgun_domain = dr["System_Settings_mailgun_domain"].ToString();
                    system_settings_dal.mailgun_url = dr["System_Settings_mailgun_url"].ToString();
                    system_settings_dal.merchant_app_android_id = dr["System_Settings_merchant_app_android_id"].ToString();
                    system_settings_dal.merchant_app_ios_id = dr["System_Settings_merchant_app_ios_id"].ToString();
                    system_settings_dal.merchant_app_terms = dr["System_Settings_merchant_app_terms"].ToString();
                    system_settings_dal.merchant_contact_approve = bool.Parse(dr["System_Settings_merchant_contact_approve"].ToString());
                    system_settings_dal.merchant_contact_validate = bool.Parse(dr["System_Settings_merchant_contact_validate"].ToString());
                    system_settings_dal.pci_key = dr["System_Settings_pci_key"].ToString();
                    system_settings_dal.percent_off_default = int.Parse(dr["System_Settings_percent_off_default"].ToString());
                    system_settings_dal.percent_off_max = int.Parse(dr["System_Settings_percent_off_max"].ToString());
                    system_settings_dal.percent_off_min = int.Parse(dr["System_Settings_percent_off_min"].ToString());
                    system_settings_dal.plivo_auth_id = dr["System_Settings_plivo_auth_id"].ToString();
                    system_settings_dal.plivo_auth_token = dr["System_Settings_plivo_auth_token"].ToString();
                    system_settings_dal.sms_on = bool.Parse(dr["System_Settings_sms_on"].ToString());
                    system_settings_dal.sms_system = dr["System_Settings_sms_system"].ToString();
                    system_settings_dal.smtp_port = int.Parse(dr["System_Settings_smtp_port"].ToString());
                    system_settings_dal.smtp_server = dr["System_Settings_smtp_server"].ToString();
                    system_settings_dal.twilio_account_sid = dr["System_Settings_twilio_account_sid"].ToString();
                    system_settings_dal.twilio_auth_token = dr["System_Settings_twilio_auth_token"].ToString();
                    system_settings_dal.twilio_test_account_sid = dr["System_Settings_twilio_test_account_sid"].ToString();
                    system_settings_dal.twilio_test_auth_token = dr["System_Settings_twilio_test_auth_token"].ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Load_System_User(DataRow dr)
        {
            try
            {
                system_users_dal.user_id = int.Parse(dr["System_User_user_id"].ToString());
                system_users_dal.user_name = dr["System_User_user_name"].ToString();
                system_users_dal.password = dr["System_User_password"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region properties

        public bool successful { get; set; }

        public DAL.Application_Type application_type_dal { get; set; }
        public DAL.Email_Template email_template_dal { get; set; }
        public DAL.SMS_Template sms_template_dal { get; set; }
        public DAL.System_Settings system_settings_dal { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }
        public DAL.System_Users system_users_dal { get; set; }

        public List<DAL.Industry> industry_dal_array { get; set; }
        public List<System_Settings> system_settings_dal_array { get; set; }

        #endregion
    }
}
   