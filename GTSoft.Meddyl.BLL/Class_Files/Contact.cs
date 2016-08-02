using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GTSoft.Meddyl.DAL;
using System.Data.Sql;


namespace GTSoft.Meddyl.BLL
{
    public class Contact
    {
        #region constructors

        public Contact()
        {
            if (contact_dal == null)
                contact_dal = new DAL.Contact();

            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();

            system_bll = new System();
        }

        public Contact(DAL.Contact _contact_dal)
        {
            contact_dal = _contact_dal;

            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();

            system_bll = new System();
        }

        #endregion


        #region public Methods

        public void Get_Credit_Card()
        {
            try
            {
                DataTable dt = credit_card_dal.SelectPK_credit_card_id();
                foreach (DataRow dr in dt.Rows)
                {
                    DAL.Credit_Card_Type credit_card_type_dal = new DAL.Credit_Card_Type();
                    credit_card_type_dal.type = dr["Credit_Card_Type_type"].ToString();
                    credit_card_type_dal.image = dr["Credit_Card_Type_image"].ToString();

                    credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = int.Parse(dr["Credit_Card_credit_card_id"].ToString());
                    credit_card_dal.card_number = dr["Credit_Card_card_number"].ToString();
                    credit_card_dal.expiration_date = dr["Credit_Card_expiration_date"].ToString();
                    credit_card_dal.credit_card_type_dal = credit_card_type_dal;
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reset_Password(string password, string reset_id)
        {
            try
            {
                DAL.Password_Reset_Status password_reset_status_dal = new Password_Reset_Status();
                DAL.Password_Reset password_reset_dal = new Password_Reset();

                password_reset_dal.reset_id = new Guid(reset_id);
                DataTable dt = password_reset_dal.SelectPK_reset_id();
                foreach(DataRow dr in dt.Rows)
                {
                    contact_dal.contact_id = int.Parse(dr["Contact_contact_id"].ToString());
                    password_reset_dal.expiration_date = DateTime.Parse(dr["Password_Reset_expiration_date"].ToString());

                    password_reset_status_dal.status_id = int.Parse(dr["Password_Reset_Status_status_id"].ToString());
                }

                if(dt.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1022, "");
                }
                else if (password_reset_status_dal.status_id == 3)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1023, "");
                }
                else if (password_reset_status_dal.status_id != 1)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1016, "");
                }
                else
                {
                    contact_dal.password = password;
                    contact_dal.usp_Contact_UpdatePK_contact_id_password();

                    password_reset_dal.usp_Password_Reset_UpdatePK();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1004, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region protected methods

        protected DataTable Get_Contact_By_User_Name()
        {
            try
            {
                return contact_dal.SelectUnique_user_name();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Load_Contact_Properties()
        {
            try
            {
                DataTable dt = contact_dal.SelectPK_contact_id();

                DataColumnCollection dc = dt.Columns;

                if ((dc.Contains("Contact_contact_id")) && (dt.Rows[0]["Contact_contact_id"] != DBNull.Value))
                    contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());

                if ((dc.Contains("Contact_first_name")) && (dt.Rows[0]["Contact_first_name"] != DBNull.Value))
                    contact_dal.first_name = dt.Rows[0]["Contact_first_name"].ToString();

                if ((dc.Contains("Contact_last_name")) && (dt.Rows[0]["Contact_last_name"] != DBNull.Value))
                    contact_dal.last_name = dt.Rows[0]["Contact_last_name"].ToString();

                if ((dc.Contains("Contact_email")) && (dt.Rows[0]["Contact_email"] != DBNull.Value))
                    contact_dal.email = dt.Rows[0]["Contact_email"].ToString();

                if ((dc.Contains("Contact_user_name")) && (dt.Rows[0]["Contact_user_name"] != DBNull.Value))
                    contact_dal.user_name = dt.Rows[0]["Contact_user_name"].ToString();

                if ((dc.Contains("Contact_password")) && (dt.Rows[0]["Contact_password"] != DBNull.Value))
                    contact_dal.password = dt.Rows[0]["Contact_password"].ToString();

                if ((dc.Contains("Contact_phone")) && (dt.Rows[0]["Contact_phone"] != DBNull.Value))
                    contact_dal.phone = dt.Rows[0]["Contact_phone"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Add_Password_Reset()
        {
            try
            {
                password_reset_dal = new Password_Reset();
                password_reset_dal.status_id = 1;
                password_reset_dal.contact_id = contact_dal.contact_id;
                password_reset_dal.email = contact_dal.email.ToString();
                password_reset_dal.usp_Password_Reset_Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected int Validate_Credit_Card()
        {
            try
            {
                int type_id = 0;
                string card_type;

                GTSoft.CoreDotNet.Credit_Card_Processing cc_processing = new GTSoft.CoreDotNet.Credit_Card_Processing();
                cc_processing.customer_first_name = contact_dal.first_name.ToString();
                cc_processing.customer_last_name = contact_dal.last_name.ToString();
                cc_processing.customer_email = contact_dal.email.ToString();
                cc_processing.card_number = credit_card_dal.card_number.ToString();
                cc_processing.card_date = credit_card_dal.expiration_date.ToString().Substring(0, 2) + "/" + credit_card_dal.expiration_date.ToString().Substring(2, 2);
                cc_processing.security_code = credit_card_dal.security_code.ToString();
                cc_processing.customer_postal_code = credit_card_dal.billing_zip_code.ToString();
                cc_processing.description = "";

                if (system_bll.system_settings_dal.credit_card_system.ToString() == "authorize.net")
                {
                    cc_processing.Get_Credit_Card_Type();
                    cc_processing.IsValid_Credit_Card();
                }
                else if (system_bll.system_settings_dal.credit_card_system == "braintree")
                {
                    cc_processing.Braintree_Card_Verification(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                }
                else if (system_bll.system_settings_dal.credit_card_system == "braintree_sandbox")
                {
                    cc_processing.Braintree_Sandbox_Card_Verification(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                }
                successful = cc_processing.successful;
                card_type = cc_processing.card_type;

                if (successful)
                {
                    credit_card_dal.credit_card_type_dal = new Credit_Card_Type();
                    credit_card_dal.credit_card_type_dal.type = card_type;
                    DataTable dt = credit_card_dal.credit_card_type_dal.usp_Credit_Card_Type_Select_By_type();
                    if (dt.Rows.Count == 0)
                    {
                        type_id = 0;
                        error_text = "Card type not accepted";
                        successful = false;
                    }
                    else
                    {
                        type_id = int.Parse(dt.Rows[0]["Credit_Card_Type_type_id"].ToString());
                    }
                }
                else
                {
                    error_text = cc_processing.error_text;
                }

                return type_id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void Email_Password(int template_id)
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(template_id);

                string[] to_emails = new string[1];
                to_emails[0] = contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", contact_dal.first_name.ToString());
                body = body.Replace("%reset_id%", password_reset_dal.reset_id.ToString().ToUpper());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region properties

        public bool successful { get; set; }
        public string error_text { get; set; }

        public DAL.Contact contact_dal { get; set; }
        public DAL.Credit_Card credit_card_dal;
        public DAL.Login_Log login_log_dal { get; set; }
        public DAL.Password_Reset password_reset_dal { get; set; }
        public DAL.Promotion promotion_dal { get; set; }
        public DAL.Promotion_Activity promotion_activity_dal { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Error system_error_dal_email { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        public List<DAL.Credit_Card> credit_card_dal_array { get; set; }
        public List<DAL.Promotion_Activity> promotion_activity_dal_array { get; set; }

        public System system_bll { get; set; }

        #endregion
    }
}
