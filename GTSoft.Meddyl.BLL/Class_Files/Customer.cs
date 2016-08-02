using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GTSoft.Meddyl.DAL;

/* test github */

namespace GTSoft.Meddyl.BLL
{
    public class Customer : Contact
    {

        #region constructors

        public Customer()
        {
        }

        public Customer(DAL.Customer _customer_dal)
        {
            try
            {
                customer_dal = _customer_dal;

                if (customer_dal.contact_dal == null)
                    contact_dal = new DAL.Contact();
                else
                    contact_dal = customer_dal.contact_dal;

                if (customer_dal.login_log_dal == null)
                    login_log_dal = new DAL.Login_Log();
                else
                    login_log_dal = customer_dal.login_log_dal;

                promotion_dal = new Promotion();

                promotion_activity_dal = new Promotion_Activity();
                
                if ((!customer_dal.customer_id.IsNull) && (customer_dal.customer_id != 0))
                    Load_Customer_Properties();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Customer(DAL.Promotion _promotion_dal)
        {
            try
            {
                promotion_dal = _promotion_dal;

                if (promotion_dal.customer_dal == null)
                    customer_dal = new DAL.Customer();
                else
                    customer_dal = promotion_dal.customer_dal;

                if (customer_dal.contact_dal == null)
                    contact_dal = new DAL.Contact();
                else
                    contact_dal = customer_dal.contact_dal;

                if (customer_dal.login_log_dal == null)
                    login_log_dal = new DAL.Login_Log();
                else
                    login_log_dal = customer_dal.login_log_dal;

                promotion_activity_dal = new Promotion_Activity();

                if ((!customer_dal.customer_id.IsNull) && (customer_dal.customer_id != 0))
                    Load_Customer_Properties();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Customer(DAL.Credit_Card _credit_card_dal)
        {
            try
            {
                credit_card_dal = _credit_card_dal;

                if (!credit_card_dal.credit_card_id.IsNull)
                {
                    DataTable dt_card = credit_card_dal.SelectPK_credit_card_id();
                    Load_Credit_Card_Properties(dt_card.Rows[0]);
                }

                if (_credit_card_dal.customer_dal == null)
                    customer_dal = new DAL.Customer();
                else
                    customer_dal = _credit_card_dal.customer_dal;

                if (customer_dal.contact_dal == null)
                    contact_dal = new DAL.Contact();
                else
                    contact_dal = customer_dal.contact_dal;

                if ((!customer_dal.customer_id.IsNull) && (customer_dal.customer_id != 0))
                    Load_Customer_Properties();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region public methods

        public void Get_Customer_Status()
        {
            try
            {
                customer_status_dal_array = new List<DAL.Customer_Status>();

                customer_status_dal = new Customer_Status();
                DataTable dt = customer_status_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    customer_status_dal = new Customer_Status();

                    Load_Customer_Status_Properties(dr);

                    customer_status_dal_array.Add(customer_status_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Login()
        {
            try
            {
                DataTable dt, dt_contact;

                dt_contact = Get_Contact_By_User_Name();

                if (dt_contact.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1001, "");
                }
                else
                {
                    dt = contact_dal.usp_Customer_Password_Verify();

                    if (dt.Rows.Count > 0)
                    {
                        contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());
                        customer_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());

                        if (dt.Rows[0]["Customer_customer_id"] == DBNull.Value)
                        {
                            if (contact_dal.zip_code_dal.latitude == 0)
                                customer_dal.search_location_type_id = 2;
                            else
                                customer_dal.search_location_type_id = 1;

                            customer_dal.usp_Customer_Insert();
                        }
                        else
                        {
                            customer_dal.customer_id = int.Parse(dt.Rows[0]["Customer_customer_id"].ToString());
                        }

                        Load_Customer_Properties();

                        if (customer_dal.customer_status_dal.status_id != 1)
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(1011, "");
                        }
                        else
                        {
                            Log_Login(false, "");

                            successful = true;
                        }
                    }
                    else
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(1000, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Login_With_Facebook(string code, string auth_token)
        {
            try
            {
                GTSoft.Meddyl.BLL.Facebook facebook = new Facebook();
                facebook.Load_Facebook_Data(auth_token);
                contact_dal.facebook_id = facebook.facebook_id;
                successful = facebook.successful;

                if (successful)
                {
                    DataTable dt_promotion = null;
                    bool valid_coordinates = false;
                    bool valid_zip_code = false;
                    string zip_code = "";
                    int search_location_type_id = 0;

                    /* check location coordinates */
                    BLL.Location location_bll = new Location();
                    location_bll.latitude = double.Parse(contact_dal.zip_code_dal.latitude.ToString());
                    location_bll.longitude = double.Parse(contact_dal.zip_code_dal.longitude.ToString());
                    location_bll.Get_Zip_Code_From_Coordinates();
                    if (!location_bll.zip_code_dal.zip_code.IsNull)
                    {
                        zip_code = location_bll.zip_code_dal.zip_code.ToString();
                        valid_coordinates = true;
                    }

                    /* check location zip code */
                    location_bll.zip_code_dal = contact_dal.zip_code_dal;
                    DataTable dt_zip_code = location_bll.Get_Zip_Code();
                    if (dt_zip_code.Rows.Count > 0)
                    {
                        zip_code = contact_dal.zip_code_dal.zip_code.ToString();
                        valid_zip_code = true;
                    }

                    if (contact_dal.zip_code_dal.latitude == 0)
                        search_location_type_id = 2;
                    else
                        search_location_type_id = 1;

                    /* check user exists */
                    DataTable dt_contact_exists = contact_dal.usp_Contact_Select_facebook_id();

                    /* check promotion */
                    if (!promotion_dal.promotion_code.IsNull && promotion_dal.promotion_code != "")
                    {
                        dt_promotion = promotion_dal.usp_Promotion_SelectUnique_Customer_Register_promotion_code();
                    }

                    if (dt_promotion != null && dt_promotion.Rows.Count == 0)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(4003, "");
                    }
                    else if (((contact_dal.zip_code_dal.latitude != 0) && (!valid_coordinates)) && (dt_contact_exists.Rows.Count == 0))
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(1004, "");
                    }
                    else if (((contact_dal.zip_code_dal.latitude == 0) && (!valid_zip_code)) && (dt_contact_exists.Rows.Count == 0))
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(1003, "");
                    }
                    else
                    {
                        if (!valid_zip_code && !valid_coordinates && dt_contact_exists.Rows.Count != 0)
                        {
                            contact_dal.zip_code_dal.zip_code = dt_contact_exists.Rows[0]["Contact_zip_code"].ToString();
                        }
                        else
                        {
                            contact_dal.zip_code_dal.zip_code = zip_code;
                        }

                        customer_dal.search_location_type_id = search_location_type_id;
                        customer_dal.zip_code = zip_code;
                        customer_dal.usp_Customer_Facebook_Login();
                        bool registered = bool.Parse(customer_dal.registered.ToString());
                        contact_dal.contact_id = int.Parse(customer_dal.contact_id.ToString());

                        Log_Login(registered, auth_token);

                        // if promo good add to activity for free use, must be a new customer
                        if (dt_contact_exists.Rows.Count == 0)
                        {
                            if (!promotion_dal.promotion_code.IsNull && promotion_dal.promotion_code != "")
                            {
                                promotion_activity_dal.customer_id = customer_dal.customer_id;
                                promotion_activity_dal.promotion_code = promotion_dal.promotion_code;
                                promotion_activity_dal.usp_Promotion_Activity_Insert_By_Referral();
                            }
                        }

                        Load_Customer_Properties();

                        Email_Register();

                        successful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Register()
        {
            try
            {
                DataTable dt_promotion = null;
                bool valid_coordinates = false;
                bool valid_zip_code = false;
                string zip_code = "";

                /* check location coordinates */
                BLL.Location location_bll = new Location();
                location_bll.latitude = double.Parse(contact_dal.zip_code_dal.latitude.ToString());
                location_bll.longitude = double.Parse(contact_dal.zip_code_dal.longitude.ToString());
                location_bll.Get_Zip_Code_From_Coordinates();
                if (!location_bll.zip_code_dal.zip_code.IsNull)
                {
                    zip_code = location_bll.zip_code_dal.zip_code.ToString();
                    valid_coordinates = true;
                }

                /* check location zip code */
                location_bll.zip_code_dal = contact_dal.zip_code_dal;
                DataTable dt_zip_code = location_bll.Get_Zip_Code();
                if (dt_zip_code.Rows.Count > 0)
                {
                    zip_code = contact_dal.zip_code_dal.zip_code.ToString();
                    valid_zip_code = true;
                }

                if(contact_dal.zip_code_dal.latitude == 0)
                    customer_dal.search_location_type_id = 2;
                else
                    customer_dal.search_location_type_id = 1;

                /* check user exists */
                DataTable dt_user_name = Get_Contact_By_User_Name();

                /* check email */
                bool valid_email = system_bll.Validate_Email(customer_dal.contact_dal.email.ToString());

                /* check promotion */
                if (!promotion_dal.promotion_code.IsNull && promotion_dal.promotion_code != "")
                {
                    dt_promotion = promotion_dal.usp_Promotion_SelectUnique_Customer_Register_promotion_code();
                }

                // check if promo is valid for contact
                if ((!promotion_dal.promotion_code.IsNull) && (promotion_dal.promotion_code != "") && (dt_promotion.Rows.Count == 0))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4003, "");
                }
                else if ((contact_dal.zip_code_dal.latitude != 0) && (!valid_coordinates))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else if ((contact_dal.zip_code_dal.latitude == 0) && (!valid_zip_code))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else if (dt_user_name.Rows.Count > 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1005, "");
                }
                else if(!valid_email)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1015, "");
                }
                else
                {
                    if (contact_dal.contact_id.IsNull)
                    {
                        contact_dal.zip_code = zip_code;
                        contact_dal.Insert();
                    }

                    customer_dal.usp_Customer_Insert();

                    Log_Login(true, "");

                    // if promo good add to activity for free use   
                    if (!promotion_dal.promotion_code.IsNull && promotion_dal.promotion_code != "")
                    {
                        promotion_activity_dal.customer_id = customer_dal.customer_id;
                        promotion_activity_dal.promotion_code = promotion_dal.promotion_code;
                        promotion_activity_dal.usp_Promotion_Activity_Insert_By_Referral();
                    }

                    Load_Customer_Properties();

                    Email_Register();

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Forgot_Password()
        {
            try
            {
                DataTable dt_exists = contact_dal.SelectUnique_user_name();

                if (dt_exists.Rows.Count > 0)
                {
                    contact_dal.contact_id = int.Parse(dt_exists.Rows[0]["Contact_contact_id"].ToString());

                    Load_Contact_Properties();

                    Add_Password_Reset();

                    Email_Password(6);

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1008, "");
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1001, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Log_Login(bool registered, string auth_token)
        {
            try
            {
                login_log_dal.contact_id = contact_dal.contact_id;
                login_log_dal.customer_id = customer_dal.customer_id;
                login_log_dal.application_type_id = login_log_dal.application_type_dal.application_type_id;
                login_log_dal.registered = registered;
                login_log_dal.auth_token = auth_token;
                login_log_dal.Insert();

                Contact_GPS_Log contact_gps_log_obj = new Contact_GPS_Log();
                contact_gps_log_obj.contact_id = contact_dal.contact_id;
                contact_gps_log_obj.latitude = contact_dal.zip_code_dal.latitude;
                contact_gps_log_obj.longitude = contact_dal.zip_code_dal.longitude;
                contact_gps_log_obj.Insert();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Customer(DAL.Customer customer_dal_update)
        {
            try
            {
                bool valid_user = false;
                bool valid_email = false;
                bool valid_zip_code = false;

                /* validate user */
                if (!contact_dal.facebook_id.IsNull)
                {
                    valid_user = true;
                }
                else
                {
                    if (contact_dal.email == customer_dal_update.contact_dal.email)
                    {
                        valid_user = true;
                    }
                    else
                    {
                        customer_dal_update.contact_dal.user_name = customer_dal_update.contact_dal.email;
                        DataTable dt_user = customer_dal_update.contact_dal.usp_Merchant_Contact_Select_by_user_name();
                        if (dt_user.Rows.Count == 0)
                        {
                            valid_user = true;
                        }
                    }
                }

                /* validate email */
                valid_email = system_bll.Validate_Email(customer_dal_update.contact_dal.email.ToString());

                BLL.Location location = new BLL.Location();
                location.zip_code_dal = customer_dal_update.contact_dal.zip_code_dal;
                DataTable dt_zip_code = location.Get_Zip_Code();
                if (dt_zip_code.Rows.Count > 0)
                    valid_zip_code = true;

                if (!valid_user)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1006, "");
                }
                else if (!valid_email)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1015, "");
                }
                else if (!valid_zip_code)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else
                {
                    customer_dal_update.usp_Customer_UpdatePK_customer_id();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1001, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Customer_Settings(DAL.Customer customer_dal_update)
        {
            try
            {
                BLL.Location location = new BLL.Location();
                location.zip_code_dal = customer_dal_update.zip_code_dal;
                DataTable dt_zip_code = location.Get_Zip_Code();

                if (dt_zip_code.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else
                {
                    customer_dal_update.usp_Customer_UpdatePK_Search_Settings();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1009, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Customer_Status(int status_id)
        {
            try
            {
                customer_dal.status_id = status_id;
                customer_dal.usp_Customer_UpdatePK_status_id();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add_Promotion(string promotion_code)
        {
            try
            {
                int sytem_error_code;

                Promotion promotion_dal = new Promotion();
                promotion_dal.customer_id = customer_dal.customer_id;
                promotion_dal.promotion_code = promotion_code;
                promotion_dal.usp_Promotion_Validate_Customer();
                sytem_error_code = int.Parse(promotion_dal.system_error_code.ToString());
                promotion_dal.promotion_id = int.Parse(promotion_dal.promotion_id.ToString());

                if (sytem_error_code != 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(sytem_error_code, "");
                }
                else
                {
                    DAL.Promotion_Activity promotion_activity_dal = new Promotion_Activity();
                    promotion_activity_dal.customer_id = customer_dal.customer_id;
                    promotion_activity_dal.promotion_code = promotion_code;
                    promotion_activity_dal.usp_Promotion_Activity_Insert();

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Customer_Search()
        {
            try
            {
                customer_dal_array = new List<DAL.Customer>();

                DataTable dt = customer_dal.usp_Customer_Search();
                foreach (DataRow dr in dt.Rows)
                {
                    customer_dal = new DAL.Customer();
                    customer_dal.customer_id = int.Parse(dr["Customer_customer_id"].ToString());

                    Load_Customer_Properties();

                    customer_dal_array.Add(customer_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region payment
        
        public void Add_Credit_Card()
        {
            try
            {
                int type_id = Validate_Credit_Card();

                if (type_id != 0)
                {
                    credit_card_dal.customer_id = customer_dal.customer_id;
                    credit_card_dal.type_id = type_id;
                    credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                    credit_card_dal.usp_Credit_Card_Insert();

                    Get_Credit_Card();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1015, "");
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1500, error_text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_Credit_Card()
        {
            try
            {
                credit_card_dal.usp_Credit_Card_Delete_Customer();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Credit_Cards()
        {
            try
            {
                credit_card_dal_array = new List<DAL.Credit_Card>();

                credit_card_dal.customer_id = customer_dal.customer_id;
                DataTable dt = credit_card_dal.usp_Credit_Card_SelectFK_Customer_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    Load_Credit_Card_Properties(dr);

                    credit_card_dal_array.Add(credit_card_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Default_Credit_Card()
        {
            try
            {
                credit_card_dal.usp_Credit_Card_Customer_Set_Default();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Valid_Promotions()
        {
            try
            {
                promotion_activity_dal_array = new List<Promotion_Activity>();

                promotion_activity_dal.customer_id = customer_dal.customer_id;
                DataTable dt = promotion_activity_dal.usp_Promotion_Activity_Valid_SelectFK_Customer_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    Load_Promotion_Activity_Properties(dr);

                    promotion_activity_dal_array.Add(promotion_activity_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_All_Promotions()
        {
            try
            {
                promotion_activity_dal_array = new List<Promotion_Activity>();

                promotion_activity_dal.customer_id = customer_dal.customer_id;
                DataTable dt = promotion_activity_dal.SelectFK_Customer_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    Load_Promotion_Activity_Properties(dr);

                    promotion_activity_dal_array.Add(promotion_activity_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion


        #region private methods

        private void Email_Register()
        {
            try
            {
                string[] to_emails = new string[1];
                to_emails[0] = contact_dal.email.ToString();

                BLL.System system_bll = new System();

                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(1);

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", contact_dal.first_name.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal_email = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Load_Customer_Properties()
        {
            try
            {
                DataTable dt = customer_dal.usp_Customer_SelectPK_customer_id();
                DataColumnCollection dc = dt.Columns;

                // instantiate objects
                if (customer_dal.customer_status_dal == null)
                    customer_dal.customer_status_dal = new DAL.Customer_Status();

                if (customer_dal.customer_search_location_type_dal == null)
                    customer_dal.customer_search_location_type_dal = new Customer_Search_Location_Type();

                if (customer_dal.industry_dal == null)
                    customer_dal.industry_dal = new Industry();

                if (customer_dal.zip_code_dal == null)
                    customer_dal.zip_code_dal = new DAL.Zip_Code();

                if (customer_dal.zip_code_dal.time_zone_dal == null)
                    customer_dal.zip_code_dal.time_zone_dal = new DAL.Time_Zone();

                if (customer_dal.zip_code_dal.city_dal == null)
                    customer_dal.zip_code_dal.city_dal = new DAL.City();

                if (customer_dal.zip_code_dal.city_dal.state_dal == null)
                    customer_dal.zip_code_dal.city_dal.state_dal = new DAL.State();

                if (customer_dal.promotion_dal == null)
                    customer_dal.promotion_dal = new Promotion();

                if (customer_dal.contact_dal == null)
                    customer_dal.contact_dal = new DAL.Contact();

                if (contact_dal == null)
                {
                    contact_dal = new DAL.Contact();
                }

                if (contact_dal.zip_code_dal == null)
                {
                    contact_dal.zip_code_dal = new DAL.Zip_Code();
                }

                if (contact_dal.zip_code_dal.city_dal == null)
                {
                    contact_dal.zip_code_dal.city_dal = new DAL.City();
                }

                if (contact_dal.zip_code_dal.time_zone_dal == null)
                {
                    contact_dal.zip_code_dal.time_zone_dal = new DAL.Time_Zone();
                }

                if (contact_dal.zip_code_dal.city_dal.state_dal == null)
                {
                    contact_dal.zip_code_dal.city_dal.state_dal = new DAL.State();
                }

                if (credit_card_dal == null)
                    credit_card_dal = new DAL.Credit_Card();

                /* Contact info */
                if ((dc.Contains("Contact_contact_id")) && (dt.Rows[0]["Contact_contact_id"] != DBNull.Value))
                {
                    contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());
                    customer_dal.contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());
                }

                if ((dc.Contains("Contact_facebook_id")) && (dt.Rows[0]["Contact_facebook_id"] != DBNull.Value))
                {
                    contact_dal.facebook_id = long.Parse(dt.Rows[0]["Contact_facebook_id"].ToString());
                    customer_dal.contact_dal.facebook_id = long.Parse(dt.Rows[0]["Contact_facebook_id"].ToString());
                }

                if ((dc.Contains("Contact_first_name")) && (dt.Rows[0]["Contact_first_name"] != DBNull.Value))
                {
                    contact_dal.first_name = dt.Rows[0]["Contact_first_name"].ToString();
                    customer_dal.contact_dal.first_name = dt.Rows[0]["Contact_first_name"].ToString();
                }

                if ((dc.Contains("Contact_last_name")) && (dt.Rows[0]["Contact_last_name"] != DBNull.Value))
                {
                    contact_dal.last_name = dt.Rows[0]["Contact_last_name"].ToString();
                    customer_dal.contact_dal.last_name = dt.Rows[0]["Contact_last_name"].ToString();
                }

                if ((dc.Contains("Contact_email")) && (dt.Rows[0]["Contact_email"] != DBNull.Value))
                {
                    contact_dal.email = dt.Rows[0]["Contact_email"].ToString();
                    customer_dal.contact_dal.email = dt.Rows[0]["Contact_email"].ToString();
                }

                if ((dc.Contains("Contact_user_name")) && (dt.Rows[0]["Contact_user_name"] != DBNull.Value))
                {
                    contact_dal.user_name = dt.Rows[0]["Contact_user_name"].ToString();
                    customer_dal.contact_dal.user_name = dt.Rows[0]["Contact_user_name"].ToString();
                }

                if ((dc.Contains("Contact_password")) && (dt.Rows[0]["Contact_password"] != DBNull.Value))
                {
                    contact_dal.password = dt.Rows[0]["Contact_password"].ToString();
                    customer_dal.contact_dal.password = dt.Rows[0]["Contact_password"].ToString();
                }

                /* Location info */
                if ((dc.Contains("Contact_City_city")) && (dt.Rows[0]["Contact_City_city"] != DBNull.Value))
                    contact_dal.zip_code_dal.city_dal.city = dt.Rows[0]["Contact_City_city"].ToString();

                if ((dc.Contains("Contact_State_abbreviation")) && (dt.Rows[0]["Contact_State_abbreviation"] != DBNull.Value))
                    contact_dal.zip_code_dal.city_dal.state_dal.state = dt.Rows[0]["Contact_State_abbreviation"].ToString();

                if ((dc.Contains("Contact_Zip_Code_zip_code")) && (dt.Rows[0]["Contact_Zip_Code_zip_code"] != DBNull.Value))
                    contact_dal.zip_code_dal.zip_code = dt.Rows[0]["Contact_Zip_Code_zip_code"].ToString();

                /* Customer info */
                if ((dc.Contains("Customer_customer_id")) && (dt.Rows[0]["Customer_customer_id"] != DBNull.Value))
                    customer_dal.customer_id = int.Parse(dt.Rows[0]["Customer_customer_id"].ToString());

                if ((dc.Contains("Customer_deal_range")) && (dt.Rows[0]["Customer_deal_range"] != DBNull.Value))
                    customer_dal.deal_range = int.Parse(dt.Rows[0]["Customer_deal_range"].ToString());

                if ((dc.Contains("Customer_search_industry_id")) && (dt.Rows[0]["Customer_search_industry_id"] != DBNull.Value))
                    customer_dal.search_industry_id = int.Parse(dt.Rows[0]["Customer_search_industry_id"].ToString());

                if ((dc.Contains("Customer_search_location_type_id")) && (dt.Rows[0]["Customer_search_location_type_id"] != DBNull.Value))
                    customer_dal.search_location_type_id = int.Parse(dt.Rows[0]["Customer_search_location_type_id"].ToString());

                if ((dc.Contains("Customer_search_zip_code")) && (dt.Rows[0]["Customer_search_zip_code"] != DBNull.Value))
                    customer_dal.search_zip_code = dt.Rows[0]["Customer_search_zip_code"].ToString();

                /* customer status info */
                if ((dc.Contains("Customer_Status_status_id")) && (dt.Rows[0]["Customer_Status_status_id"] != DBNull.Value))
                    customer_dal.customer_status_dal.status_id = int.Parse(dt.Rows[0]["Customer_Status_status_id"].ToString());

                if ((dc.Contains("Customer_Status_status")) && (dt.Rows[0]["Customer_Status_status"] != DBNull.Value))
                    customer_dal.customer_status_dal.status = dt.Rows[0]["Customer_Status_status"].ToString();

                /* city info */
                if ((dc.Contains("Customer_City_city")) && (dt.Rows[0]["Customer_City_city"] != DBNull.Value))
                    customer_dal.zip_code_dal.city_dal.city = dt.Rows[0]["Customer_City_city"].ToString();

                if ((dc.Contains("Customer_State_abbreviation")) && (dt.Rows[0]["Customer_State_abbreviation"] != DBNull.Value))
                    customer_dal.zip_code_dal.city_dal.state_dal.state = dt.Rows[0]["Customer_State_abbreviation"].ToString();

                if ((dc.Contains("Customer_Zip_Code_zip_code")) && (dt.Rows[0]["Customer_Zip_Code_zip_code"] != DBNull.Value))
                    customer_dal.zip_code_dal.zip_code = dt.Rows[0]["Customer_Zip_Code_zip_code"].ToString();

                /* Search location type */
                if ((dc.Contains("Customer_Search_Location_Type_search_location_type_id")) && (dt.Rows[0]["Customer_Search_Location_Type_search_location_type_id"] != DBNull.Value))
                    customer_dal.customer_search_location_type_dal.search_location_type_id = int.Parse(dt.Rows[0]["Customer_Search_Location_Type_search_location_type_id"].ToString());

                if ((dc.Contains("Customer_Search_Location_Type_location_type")) && (dt.Rows[0]["Customer_Search_Location_Type_location_type"] != DBNull.Value))
                    customer_dal.customer_search_location_type_dal.location_type = dt.Rows[0]["Customer_Search_Location_Type_location_type"].ToString();

                /* Industry info */
                if ((dc.Contains("Industry_industry_id")) && (dt.Rows[0]["Industry_industry_id"] != DBNull.Value))
                    customer_dal.industry_dal.industry_id = int.Parse(dt.Rows[0]["Industry_industry_id"].ToString());

                if ((dc.Contains("Industry_industry")) && (dt.Rows[0]["Industry_industry"] != DBNull.Value))
                    customer_dal.industry_dal.industry = dt.Rows[0]["Industry_industry"].ToString();

                /* Promotion info */
                if ((dc.Contains("Promotion_promotion_id")) && (dt.Rows[0]["Promotion_promotion_id"] != DBNull.Value))
                    customer_dal.promotion_dal.promotion_id = int.Parse(dt.Rows[0]["Promotion_promotion_id"].ToString());

                if ((dc.Contains("Promotion_promotion_code")) && (dt.Rows[0]["Promotion_promotion_code"] != DBNull.Value))
                    customer_dal.promotion_dal.promotion_code = dt.Rows[0]["Promotion_promotion_code"].ToString();

                if ((dc.Contains("Promotion_link")) && (dt.Rows[0]["Promotion_link"] != DBNull.Value))
                    customer_dal.promotion_dal.link = dt.Rows[0]["Promotion_link"].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Credit_Card_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                credit_card_dal = new Credit_Card();
                credit_card_dal.credit_card_type_dal = new DAL.Credit_Card_Type();

                if ((dc.Contains("Credit_Card_credit_card_id")) && (dr["Credit_Card_credit_card_id"] != DBNull.Value))
                    credit_card_dal.credit_card_id = int.Parse(dr["Credit_Card_credit_card_id"].ToString());

                if ((dc.Contains("Credit_Card_customer_id")) && (dr["Credit_Card_customer_id"] != DBNull.Value))
                    credit_card_dal.customer_id = int.Parse(dr["Credit_Card_customer_id"].ToString());

                if ((dc.Contains("Credit_Card_card_holder_name")) && (dr["Credit_Card_card_holder_name"] != DBNull.Value))
                    credit_card_dal.card_holder_name = dr["Credit_Card_card_holder_name"].ToString();

                if ((dc.Contains("Credit_Card_card_number")) && (dr["Credit_Card_card_number"] != DBNull.Value))
                    credit_card_dal.card_number = dr["Credit_Card_card_number"].ToString();

                if ((dc.Contains("Credit_Card_expiration_date")) && (dr["Credit_Card_expiration_date"] != DBNull.Value))
                    credit_card_dal.expiration_date = dr["Credit_Card_expiration_date"].ToString();

                if ((dc.Contains("Credit_Card_default_flag")) && (dr["Credit_Card_default_flag"] != DBNull.Value))
                    credit_card_dal.default_flag = bool.Parse(dr["Credit_Card_default_flag"].ToString());

                /* load cc types */
                if ((dc.Contains("Credit_Card_Type_type")) && (dr["Credit_Card_Type_type"] != DBNull.Value))
                    credit_card_dal.credit_card_type_dal.type = dr["Credit_Card_Type_type"].ToString();

                if ((dc.Contains("Credit_Card_Type_image")) && (dr["Credit_Card_Type_image"] != DBNull.Value))
                    credit_card_dal.credit_card_type_dal.image = dr["Credit_Card_Type_image"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Customer_Status_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Customer_Status_status_id")) && (dr["Customer_Status_status_id"] != DBNull.Value))
                    customer_status_dal.status_id = int.Parse(dr["Customer_Status_status_id"].ToString());

                if ((dc.Contains("Customer_Status_status")) && (dr["Customer_Status_status"] != DBNull.Value))
                    customer_status_dal.status = dr["Customer_Status_status"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Promotion_Activity_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                promotion_activity_dal = new DAL.Promotion_Activity();
                promotion_activity_dal.promotion_dal = new Promotion();

                /* set promotion activity */
                if ((dc.Contains("Promotion_Activity_promotion_activity_id")) && (dr["Promotion_Activity_promotion_activity_id"] != DBNull.Value))
                    promotion_activity_dal.promotion_activity_id = int.Parse(dr["Promotion_Activity_promotion_activity_id"].ToString());

                if ((dc.Contains("Promotion_Activity_expiration_date")) && (dr["Promotion_Activity_expiration_date"] != DBNull.Value))
                    promotion_activity_dal.expiration_date = DateTime.Parse(dr["Promotion_Activity_expiration_date"].ToString());

                if ((dc.Contains("v")) && (dr["Promotion_Activity_redeemed_date"] != DBNull.Value))
                    promotion_activity_dal.redeemed_date = DateTime.Parse(dr["Promotion_Activity_redeemed_date"].ToString());

                /* set promotion */
                if ((dc.Contains("Promotion_promotion_id")) && (dr["Promotion_promotion_id"] != DBNull.Value))
                    promotion_activity_dal.promotion_dal.promotion_id = int.Parse(dr["Promotion_promotion_id"].ToString());

                if ((dc.Contains("Promotion_promotion_code")) && (dr["Promotion_promotion_code"] != DBNull.Value))
                    promotion_activity_dal.promotion_dal.promotion_code = dr["Promotion_promotion_code"].ToString();

                if ((dc.Contains("Promotion_description")) && (dr["Promotion_description"] != DBNull.Value))
                    promotion_activity_dal.promotion_dal.description = dr["Promotion_description"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region properties

        public DAL.Customer customer_dal { get; set; }
        public DAL.Customer_Status customer_status_dal;

        public List<DAL.Customer> customer_dal_array;
        public List<DAL.Customer_Status> customer_status_dal_array;

        #endregion
    }
}
