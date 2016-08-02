using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using GTSoft.Meddyl.DAL;


namespace GTSoft.Meddyl.BLL
{
    public class Merchant : Contact
    {
        #region constructors

        public Merchant()
        {
            if (merchant_contact_dal == null)
                merchant_contact_dal = new DAL.Merchant_Contact();
        }

        public Merchant(DAL.Merchant_Contact _merchant_contact_dal)
        {
            merchant_contact_dal = _merchant_contact_dal;

            if (merchant_contact_dal.merchant_dal == null)
                merchant_dal = new DAL.Merchant();
            else
                merchant_dal = merchant_contact_dal.merchant_dal;

            if (merchant_contact_dal.contact_dal == null)
                contact_dal = new DAL.Contact();
            else
                contact_dal = merchant_contact_dal.contact_dal;

            if (merchant_contact_dal.merchant_contact_status_dal == null)
                merchant_contact_dal.merchant_contact_status_dal = new DAL.Merchant_Contact_Status();
            else
                merchant_contact_dal.merchant_contact_status_dal = merchant_contact_dal.merchant_contact_status_dal;

            if (merchant_contact_dal.merchant_contact_validation_dal == null)
                merchant_contact_dal.merchant_contact_validation_dal = new DAL.Merchant_Contact_Validation();
            else
                merchant_contact_dal.merchant_contact_validation_dal = merchant_contact_dal.merchant_contact_validation_dal;

            if (merchant_contact_dal.login_log_dal == null)
                login_log_dal = new DAL.Login_Log();
            else
                login_log_dal = merchant_contact_dal.login_log_dal;
            
            if (!merchant_contact_dal.merchant_contact_id.IsNull)
                Load_Merchant_Contact_Properties();
        }

        public Merchant(DAL.Merchant _merchant_dal)
        {
            merchant_dal = _merchant_dal;
        }

        public Merchant(DAL.Credit_Card _credit_card_dal)
        {
            credit_card_dal = _credit_card_dal;

            if (!credit_card_dal.credit_card_id.IsNull)
            {
                credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                DataTable dt_card = credit_card_dal.usp_Credit_Card_SelectPK_credit_card_id();
                Load_Credit_Card_Properties(dt_card.Rows[0]);
            }

            if(contact_dal == null)
                contact_dal = new DAL.Contact();

            if (credit_card_dal.merchant_contact_dal == null)
            {
                merchant_contact_dal = new Merchant_Contact();
                merchant_contact_dal.merchant_contact_id = credit_card_dal.merchant_contact_id;
            }
            else
            {
                merchant_contact_dal = credit_card_dal.merchant_contact_dal;
            }

            Load_Merchant_Contact_Properties();
        }

        #endregion


        #region public methods

        public void Get_Merchant_Status()
        {
            try
            {
                merchant_status_dal_array = new List<DAL.Merchant_Status>();

                merchant_status_dal = new Merchant_Status();
                DataTable dt = merchant_status_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    merchant_status_dal = new Merchant_Status();

                    Load_Merchant_Status_Properties(dr);

                    merchant_status_dal_array.Add(merchant_status_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Merchant_Rating()
        {
            try
            {
                merchant_rating_dal_array = new List<DAL.Merchant_Rating>();

                merchant_rating_dal = new DAL.Merchant_Rating();
                DataTable dt = merchant_rating_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    merchant_rating_dal = new Merchant_Rating();

                    Load_Merchant_Rating_Properties(dr);

                    merchant_rating_dal_array.Add(merchant_rating_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Merchant_Contact_Status()
        {
            try
            {
                merchant_contact_status_dal_array = new List<DAL.Merchant_Contact_Status>();

                merchant_contact_status_dal = new Merchant_Contact_Status();
                DataTable dt = merchant_contact_status_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    merchant_contact_status_dal = new Merchant_Contact_Status();

                    Load_Merchant_Contact_Status_Properties(dr);

                    merchant_contact_status_dal_array.Add(merchant_contact_status_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Merchant_Contacts_Pending_Approval()
        {
            try
            {
                merchant_contact_dal_array = new List<Merchant_Contact>();

                merchant_contact_dal.status_id = 4;
                DataTable dt = merchant_contact_dal.SelectFK_Merchant_Contact_Status_status_id();
                foreach (DataRow dr in dt.Rows)
                {
                    merchant_contact_dal = new Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = int.Parse(dr["Merchant_Contact_merchant_contact_id"].ToString());
                    
                    Load_Merchant_Contact_Properties();

                    merchant_contact_dal.merchant_dal = merchant_dal;

                    merchant_contact_dal_array.Add(merchant_contact_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Get_Merchant_Contact_Search()
        {
            try
            {
                merchant_contact_dal_array = new List<Merchant_Contact>();

                DataTable dt = merchant_contact_dal.usp_Merchant_Contact_Search();
                foreach (DataRow dr in dt.Rows)
                {
                    merchant_contact_dal = new Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = int.Parse(dr["Merchant_Contact_merchant_contact_id"].ToString());

                    Load_Merchant_Contact_Properties();

                    merchant_contact_dal.merchant_dal = merchant_dal;

                    merchant_contact_dal_array.Add(merchant_contact_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Create_Validation()
        {
            try
            {
                DataTable dt_user = merchant_contact_dal.contact_dal.usp_Merchant_Contact_Select_by_user_name();
                
                bool valid_email = system_bll.Validate_Email(merchant_contact_dal.contact_dal.email.ToString());

                if ((merchant_contact_dal.contact_dal.contact_id == 0) && (dt_user.Rows.Count == 1) && (dt_user.Rows[0]["Contact_contact_id"] != DBNull.Value))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1005, "");
                }
                else if ((dt_user.Rows.Count == 1) && (dt_user.Rows[0]["Merchant_Contact_merchant_contact_id"] != DBNull.Value))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1005, "");
                }
                else if (!valid_email)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1015, "");
                }
                else
                {
                    // create validation code
                    DAL.Merchant_Contact_Validation merchant_contact_validation_dal = new Merchant_Contact_Validation();
                    merchant_contact_validation_dal.user_name = contact_dal.user_name;
                    merchant_contact_validation_dal.phone = contact_dal.phone;
                    merchant_contact_validation_dal.ip_address = merchant_contact_dal.merchant_contact_validation_dal.ip_address;
                    merchant_contact_validation_dal.usp_Merchant_Contact_Validation_Insert();

                    SMS_Validation(merchant_contact_validation_dal.validation_code.ToString());

                    if (!successful)
                    {
                        system_error_dal = system_bll.Get_System_Error(1017, "");
                    }
                    else
                    {
                        system_successful_dal = system_bll.Get_System_Successful(1000, "");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Validate()
        {
            try
            {
                merchant_contact_dal.merchant_contact_validation_dal.user_name = merchant_contact_dal.contact_dal.user_name;
                merchant_contact_dal.merchant_contact_validation_dal.phone = merchant_contact_dal.contact_dal.phone;
                merchant_contact_dal.merchant_contact_validation_dal.usp_Merchant_Contact_Validate();

                if (merchant_contact_dal.merchant_contact_validation_dal.validation_id == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1018, "");
                }
                else
                {
                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1006, "");
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
                bool is_validated = false;
                DataTable dt_user = merchant_contact_dal.contact_dal.usp_Merchant_Contact_Select_by_user_name();

                // this is executed only if it's a sign-in and need to complete merchant registration
                if (contact_dal.contact_id != 0)
                {
                    merchant_contact_dal.contact_id = merchant_contact_dal.contact_dal.contact_id;
                    DataTable dt_merchant_contact = merchant_contact_dal.SelectFK_Contact_contact_id();
                    if (dt_merchant_contact.Rows.Count == 0)
                        merchant_contact_dal.merchant_contact_id = 0;
                    else
                        merchant_contact_dal.merchant_contact_id = int.Parse(dt_merchant_contact.Rows[0]["Merchant_Contact_merchant_contact_id"].ToString());
                }

                DataTable dt_validation = merchant_contact_dal.merchant_contact_validation_dal.SelectPK_validation_id();
                foreach(DataRow dr in dt_validation.Rows)
                {
                    is_validated = bool.Parse(dr["Merchant_Contact_Validation_is_validated"].ToString());
                }

                GTSoft.Meddyl.BLL.Location location = new BLL.Location();
                location.zip_code_dal = merchant_contact_dal.merchant_dal.zip_code_dal;
                DataTable dt_zip_code = location.Get_Zip_Code();

                bool valid_email = system_bll.Validate_Email(merchant_contact_dal.contact_dal.email.ToString());

                if(!is_validated)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1024, "");
                }
                else if ((dt_user.Rows.Count == 1) && (contact_dal.contact_id == 0))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1005, "");
                }
                else if (merchant_contact_dal.merchant_contact_id != 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1012, "");
                }
                else if (dt_zip_code.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else if (!valid_email)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1015, "");
                }
                else
                {
                    location.address_1 = merchant_contact_dal.merchant_dal.address_1.ToString();
                    location.address_2 = merchant_contact_dal.merchant_dal.address_2.ToString();
                    location.zip_code_dal.zip_code = merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                    location.Get_Coordinates_For_Address();

                    if (contact_dal.contact_id != 0)
                        merchant_contact_dal.merchant_dal.contact_id = contact_dal.contact_id;
                    merchant_contact_dal.merchant_dal.first_name = contact_dal.first_name;
                    merchant_contact_dal.merchant_dal.last_name = contact_dal.last_name;
                    merchant_contact_dal.merchant_dal.email = contact_dal.email;
                    merchant_contact_dal.merchant_dal.contact_phone = contact_dal.phone;
                    merchant_contact_dal.merchant_dal.user_name = contact_dal.user_name;
                    merchant_contact_dal.merchant_dal.password = contact_dal.password;
                    merchant_contact_dal.merchant_dal.title = merchant_contact_dal.title;
                    merchant_contact_dal.merchant_dal.application_type_id = merchant_contact_dal.login_log_dal.application_type_dal.application_type_id;
                    merchant_contact_dal.merchant_dal.validation_id = merchant_contact_dal.merchant_contact_validation_dal.validation_id;
                    merchant_contact_dal.merchant_dal.latitude = location.latitude;
                    merchant_contact_dal.merchant_dal.longitude = location.longitude;
                    merchant_contact_dal.merchant_dal.zip_code = merchant_contact_dal.merchant_dal.zip_code_dal.zip_code;
                    merchant_contact_dal.merchant_dal.ip_address = merchant_contact_dal.login_log_dal.ip_address;
                    merchant_contact_dal.merchant_dal.industry_id = merchant_contact_dal.merchant_dal.industry_dal.industry_id;
                    if (merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id != 0)
                        merchant_contact_dal.merchant_dal.neighborhood_id = merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id;
                    merchant_contact_dal.merchant_dal.usp_Merchant_Insert();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_dal.merchant_dal.merchant_contact_id;
                    merchant_contact_dal.merchant_id = merchant_contact_dal.merchant_dal.merchant_id;

                    if(merchant_contact_dal.merchant_dal.image!="")
                        Update_Image(merchant_contact_dal.merchant_dal.image_base64.ToString());

                    if (system_bll.system_settings_dal.merchant_contact_approve)
                        Email_Admin_Approval();
                    else
                        Email_Register();

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Approve_Merchant_Contact()
        {
            try
            {
                if (merchant_contact_dal.merchant_contact_status_dal.status_id != 4)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1014, "Status is " + merchant_contact_dal.merchant_contact_status_dal.status.ToString());
                }
                else
                {
                    merchant_contact_dal.usp_Merchant_Contact_Approve();

                    Load_Merchant_Contact_Properties();

                    if (merchant_contact_dal.merchant_contact_status_dal.status_id != 1)
                    {
                        system_error_dal = new System_Error();
                        system_error_dal.code = 1010;
                        DataTable dt_system_error = system_error_dal.SelectPK_code();
                        system_error_dal.message = dt_system_error.Rows[0]["System_Error_message"].ToString();

                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(1010, "");
                    }
                    else
                    {
                        Email_Register();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(1007, "");
                    }
                }
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
                    dt = null;

                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1001, "");
                }
                else
                {
                    dt = contact_dal.usp_Contact_Merchant_Password_Verify();

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Merchant_Contact_merchant_contact_id"] == DBNull.Value)
                        {
                            contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());
                            merchant_contact_dal.merchant_contact_id = 0;

                            Load_Contact_Properties();

                            Log_Login(false);

                            successful = true;
                        }
                        else
                        {
                            merchant_contact_dal.merchant_contact_status_dal.status_id = int.Parse(dt.Rows[0]["Merchant_Contact_Status_status_id"].ToString());

                            if (merchant_contact_dal.merchant_contact_status_dal.status_id == 2)
                            {
                                successful = false;
                                system_error_dal = system_bll.Get_System_Error(1011, "");
                            }
                            else
                            {
                                merchant_contact_dal.merchant_contact_id = int.Parse(dt.Rows[0]["Merchant_Contact_merchant_contact_id"].ToString());

                                Load_Merchant_Contact_Properties();

                                Log_Login(false);

                                successful = true;
                            }
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

        public void Forgot_Password()
        {
            try
            {
                DataTable dt_exists = merchant_contact_dal.contact_dal.usp_Merchant_Contact_Select_by_user_name();

                if (dt_exists.Rows.Count > 0)
                {
                    if (dt_exists.Rows[0]["Merchant_Contact_merchant_contact_id"] == DBNull.Value)
                        merchant_contact_dal.merchant_contact_id = 0;
                    else
                    {
                        merchant_contact_dal.merchant_contact_id = int.Parse(dt_exists.Rows[0]["Merchant_Contact_merchant_contact_id"].ToString());

                        Load_Merchant_Contact_Properties();
                    }

                    Add_Password_Reset();

                    Email_Password(5);

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

        public void Update_Merchant_Contact(DAL.Merchant_Contact merchant_contact_dal_update)
        {
            try
            {
                bool valid_user = false;
                bool valid_email = false;

                /* validate user */
                DataTable dt_user = merchant_contact_dal_update.contact_dal.usp_Merchant_Contact_Select_by_user_name();
                if (dt_user.Rows.Count == 0)
                {
                    valid_user = true;
                }
                else
                {
                    if (int.Parse(dt_user.Rows[0]["Merchant_Contact_merchant_contact_id"].ToString()) == merchant_contact_dal_update.merchant_contact_id)
                        valid_user = true;
                    else
                        valid_user = false;
                }
                
                /* validate email */
                valid_email = system_bll.Validate_Email(merchant_contact_dal_update.contact_dal.email.ToString());

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
                else
                {
                    merchant_contact_dal.first_name = merchant_contact_dal_update.contact_dal.first_name;
                    merchant_contact_dal.last_name = merchant_contact_dal_update.contact_dal.last_name;
                    merchant_contact_dal.email = merchant_contact_dal_update.contact_dal.email;
                    merchant_contact_dal.title = merchant_contact_dal_update.title;
                    merchant_contact_dal.usp_Merchant_Contact_UpdatePK();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1013, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Merchant(DAL.Merchant_Contact merchant_contact_dal_update)
        {
            try
            {
                /* validate zip code */
                GTSoft.Meddyl.BLL.Location location = new BLL.Location();
                location.zip_code_dal = merchant_contact_dal_update.merchant_dal.zip_code_dal;
                DataTable dt_zip_code = location.Get_Zip_Code();

                if (dt_zip_code.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
                else
                {
                    location.address_1 = merchant_contact_dal_update.merchant_dal.address_1.ToString();
                    location.address_2 = merchant_contact_dal_update.merchant_dal.address_2.ToString();
                    location.zip_code_dal.zip_code = merchant_contact_dal_update.merchant_dal.zip_code_dal.zip_code.ToString();
                    location.Get_Coordinates_For_Address();

                    merchant_dal.merchant_contact_id = merchant_contact_dal_update.merchant_contact_id;
                    merchant_dal.company_name = merchant_contact_dal_update.merchant_dal.company_name;
                    merchant_dal.industry_id = merchant_contact_dal_update.merchant_dal.industry_dal.industry_id;
                    if (merchant_contact_dal_update.merchant_dal.neighborhood_dal.neighborhood_id != 0)
                        merchant_dal.neighborhood_id = merchant_contact_dal_update.merchant_dal.neighborhood_dal.neighborhood_id;
                    merchant_dal.description = merchant_contact_dal_update.merchant_dal.description;
                    merchant_dal.address_1 = merchant_contact_dal_update.merchant_dal.address_1;
                    merchant_dal.address_2 = merchant_contact_dal_update.merchant_dal.address_2;
                    merchant_dal.zip_code = merchant_contact_dal_update.merchant_dal.zip_code_dal.zip_code;
                    merchant_dal.latitude = location.latitude;
                    merchant_dal.longitude = location.longitude;
                    merchant_dal.phone = merchant_contact_dal_update.merchant_dal.phone;
                    merchant_dal.website = merchant_contact_dal_update.merchant_dal.website;
                    merchant_dal.usp_Merchant_UpdatePK();

                    merchant_dal.image_base64 = merchant_contact_dal_update.merchant_dal.image_base64;
                    if (merchant_dal.image_base64 != "")
                        Update_Image(merchant_dal.image_base64.ToString());

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1014, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Merchant_By_Admin(DAL.Merchant merchant_dal_update)
        {
            try
            {
                merchant_dal_update.usp_Merchant_UpdatePK_By_Admin();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1014, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Company_name(string company_name)
        {
            try
            {
                merchant_dal.company_name = company_name;
                merchant_dal.usp_Merchant_UpdatePK_company_name();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1500, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Address(string address_1, string address_2, string zip_code, int neighborhood_id)
        {
            try
            {
                GTSoft.Meddyl.BLL.Location location = new Location();
                location.zip_code_dal = contact_dal.zip_code_dal;
                DataTable dt_zip_codes = location.Get_Zip_Code();

                if (dt_zip_codes.Rows.Count == 1)
                {
                    location.address_1 = address_1;
                    location.address_2 = address_2;
                    location.zip_code_dal.zip_code = zip_code;
                    location.Get_Coordinates_For_Address();

                    merchant_dal.address_1 = address_1;
                    merchant_dal.address_2 = address_2;
                    merchant_dal.zip_code = zip_code;
                    merchant_dal.neighborhood_id = neighborhood_id;
                    merchant_dal.latitude = location.latitude;
                    merchant_dal.longitude = location.longitude;
                    merchant_dal.usp_Merchant_UpdatePK_address();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1501, "");
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Phone(string phone)
        {
            try
            {
                merchant_dal.phone = phone;
                merchant_dal.usp_Merchant_UpdatePK_phone();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1502, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Website(string website)
        {
            try
            {
                merchant_dal.website = website;
                merchant_dal.usp_Merchant_UpdatePK_website();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1503, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Industry(int industry_id)
        {
            try
            {
                merchant_dal.industry_id = industry_id;
                merchant_dal.usp_Merchant_UpdatePK_industry_id();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1504, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update_Image(string image_base64)
        {
            try
            {
                string image = system_bll.Save_Image(image_base64, int.Parse(merchant_dal.merchant_id.ToString()), "merchant");

                merchant_contact_dal.merchant_dal.image = image;
                merchant_contact_dal.merchant_dal.usp_Merchant_UpdatePK_image();

                successful = true;
                system_successful_dal = system_bll.Get_System_Successful(1505, "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Merchant_Contact_Status(int status_id)
        {
            try
            {
                merchant_contact_dal.status_id = status_id;
                merchant_contact_dal.usp_Merchant_Contact_UpdatePK_status_id();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Merchant_Status(int status_id)
        {
            try
            {
                merchant_dal.status_id = status_id;
                merchant_dal.usp_Merchant_UpdatePK_status_id();

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
                    credit_card_dal.merchant_contact_id = merchant_contact_dal.merchant_contact_id;
                    credit_card_dal.type_id = type_id;
                    credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                    credit_card_dal.usp_Credit_Card_Insert();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(1015, "");
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1500, "");
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
                credit_card_dal.usp_Credit_Card_Delete_Merchant_Contact();

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

                credit_card_dal = new Credit_Card();
                credit_card_dal.merchant_contact_id = merchant_contact_dal.merchant_contact_id;
                credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                DataTable dt = credit_card_dal.usp_Credit_Card_SelectFK_Merchant_Contact_merchant_contact_id();
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

        public void Get_Default_Credit_Card()
        {
            try
            {
                credit_card_dal = new DAL.Credit_Card();

                credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                credit_card_dal.merchant_contact_id = merchant_contact_dal.merchant_contact_id;
                DataTable dt = credit_card_dal.usp_Credit_Card_Merchant_Contact_Select_default();
                foreach (DataRow dr in dt.Rows)
                {
                    Load_Credit_Card_Properties(dr);
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
                credit_card_dal.usp_Credit_Card_Merchant_Contact_Set_Default();

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

        private void Log_Login(bool registered)
        {
            try
            {
                login_log_dal.contact_id = contact_dal.contact_id;
                if (merchant_contact_dal.merchant_contact_id != 0)
                    login_log_dal.merchant_contact_id = merchant_contact_dal.merchant_contact_id;
                login_log_dal.application_type_id = login_log_dal.application_type_dal.application_type_id;
                login_log_dal.auth_token = "";
                login_log_dal.registered = registered;
                login_log_dal.Insert();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void Email_Admin_Approval()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(4);

                string[] to_emails = new string[1];
                to_emails[0] = system_bll.system_settings_dal.email_admin.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%merchant_contact_id%", merchant_contact_dal.merchant_contact_id.ToString());
                body = body.Replace("%first_name%", merchant_contact_dal.contact_dal.first_name.ToString());
                body = body.Replace("%last_name%", merchant_contact_dal.contact_dal.last_name.ToString());
                body = body.Replace("%email%", merchant_contact_dal.contact_dal.email.ToString());
                body = body.Replace("%merchant_id%", merchant_contact_dal.merchant_dal.merchant_id.ToString());
                body = body.Replace("%company_name%", merchant_contact_dal.merchant_dal.company_name.ToString());

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

        private void Email_Register()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(2);

                string[] to_emails = new string[1];
                to_emails[0] = contact_dal.email.ToString();

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

        private void SMS_Validation(string validation_code)
        {
            try
            {
                DAL.SMS_Template sms_template_dal = system_bll.Get_SMS_Template(1);

                string body = sms_template_dal.body.ToString();
                body = body.Replace("%validation_code%", validation_code);

                system_bll.Send_Text(contact_dal.phone.ToString(), body);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1019, ex.Message.ToString());
            }
        }

        private void Load_Merchant_Contact_Properties()
        {
            try
            {
                DataTable dt = merchant_contact_dal.usp_Merchant_Contact_SelectPK_merchant_contact_id();
                DataColumnCollection dc = dt.Columns;

                /* check objects */
                if (merchant_contact_dal.merchant_contact_status_dal == null)
                    merchant_contact_dal.merchant_contact_status_dal = new DAL.Merchant_Contact_Status();

                if (merchant_contact_dal.merchant_contact_validation_dal == null)
                    merchant_contact_dal.merchant_contact_validation_dal = new DAL.Merchant_Contact_Validation();

                if (merchant_contact_dal.merchant_dal == null)
                    merchant_contact_dal.merchant_dal = new DAL.Merchant();

                merchant_dal = new DAL.Merchant();

                if (merchant_dal.merchant_status_dal == null)
                    merchant_dal.merchant_status_dal = new DAL.Merchant_Status();

                if (merchant_dal.neighborhood_dal == null)
                    merchant_dal.neighborhood_dal = new DAL.Neighborhood();

                if (merchant_dal.merchant_rating_dal == null)
                    merchant_dal.merchant_rating_dal = new DAL.Merchant_Rating();

                if (merchant_dal.industry_dal == null)
                    merchant_dal.industry_dal = new DAL.Industry();

                if (merchant_dal.zip_code_dal == null)
                    merchant_dal.zip_code_dal = new DAL.Zip_Code();

                if (merchant_dal.zip_code_dal.time_zone_dal == null)
                    merchant_dal.zip_code_dal.time_zone_dal = new DAL.Time_Zone();

                if (merchant_dal.zip_code_dal.city_dal == null)
                    merchant_dal.zip_code_dal.city_dal = new DAL.City();

                if (merchant_dal.zip_code_dal.city_dal.state_dal == null)
                    merchant_dal.zip_code_dal.city_dal.state_dal = new DAL.State();

                contact_dal = new DAL.Contact();

                if (contact_dal.zip_code_dal == null)
                    contact_dal.zip_code_dal = new DAL.Zip_Code();

                if (contact_dal.zip_code_dal.city_dal == null)
                    contact_dal.zip_code_dal.city_dal = new DAL.City();

                if (contact_dal.zip_code_dal.time_zone_dal == null)
                    contact_dal.zip_code_dal.time_zone_dal = new DAL.Time_Zone();

                if (contact_dal.zip_code_dal.city_dal.state_dal == null)
                    contact_dal.zip_code_dal.city_dal.state_dal = new DAL.State();


                /* Contact */
                if ((dc.Contains("Contact_contact_id")) && (dt.Rows[0]["Contact_contact_id"] != DBNull.Value))
                    contact_dal.contact_id = int.Parse(dt.Rows[0]["Contact_contact_id"].ToString());

                if ((dc.Contains("Contact_first_name")) && (dt.Rows[0]["Contact_first_name"] != DBNull.Value))
                    contact_dal.first_name = dt.Rows[0]["Contact_first_name"].ToString();

                if ((dc.Contains("Contact_last_name")) && (dt.Rows[0]["Contact_last_name"] != DBNull.Value))
                    contact_dal.last_name = dt.Rows[0]["Contact_last_name"].ToString();

                if ((dc.Contains("Contact_email")) && (dt.Rows[0]["Contact_email"] != DBNull.Value))
                    contact_dal.email = dt.Rows[0]["Contact_email"].ToString();

                if ((dc.Contains("Contact_phone")) && (dt.Rows[0]["Contact_phone"] != DBNull.Value))
                    contact_dal.phone = dt.Rows[0]["Contact_phone"].ToString();

                if ((dc.Contains("Contact_user_name")) && (dt.Rows[0]["Contact_user_name"] != DBNull.Value))
                    contact_dal.user_name = dt.Rows[0]["Contact_user_name"].ToString();

                if ((dc.Contains("Contact_password")) && (dt.Rows[0]["Contact_password"] != DBNull.Value))
                    contact_dal.password = dt.Rows[0]["Contact_password"].ToString();

                if ((dc.Contains("Contact_City_city")) && (dt.Rows[0]["Contact_City_city"] != DBNull.Value))
                    contact_dal.zip_code_dal.city_dal.city = dt.Rows[0]["Contact_City_city"].ToString();

                if ((dc.Contains("Contact_State_abbreviation")) && (dt.Rows[0]["Contact_State_abbreviation"] != DBNull.Value))
                    contact_dal.zip_code_dal.city_dal.state_dal.state = dt.Rows[0]["Contact_State_abbreviation"].ToString();

                if ((dc.Contains("Contact_Zip_Code_zip_code")) && (dt.Rows[0]["Contact_Zip_Code_zip_code"] != DBNull.Value))
                    contact_dal.zip_code_dal.zip_code = dt.Rows[0]["Contact_Zip_Code_zip_code"].ToString();

                /* Merchant Contact */
                if ((dc.Contains("Merchant_Contact_merchant_contact_id")) && (dt.Rows[0]["Merchant_Contact_merchant_contact_id"] != DBNull.Value))
                    merchant_contact_dal.merchant_contact_id = int.Parse(dt.Rows[0]["Merchant_Contact_merchant_contact_id"].ToString());

                if ((dc.Contains("Merchant_Contact_title")) && (dt.Rows[0]["Merchant_Contact_title"] != DBNull.Value))
                    merchant_contact_dal.title = dt.Rows[0]["Merchant_Contact_title"].ToString();

                if ((dc.Contains("Merchant_Contact_Validation_validation_code")) && (dt.Rows[0]["Merchant_Contact_Validation_validation_code"] != DBNull.Value))
                    merchant_contact_dal.merchant_contact_validation_dal.validation_code = dt.Rows[0]["Merchant_Contact_Validation_validation_code"].ToString();

                if ((dc.Contains("Merchant_Contact_Status_status_id")) && (dt.Rows[0]["Merchant_Contact_Status_status_id"] != DBNull.Value))
                    merchant_contact_dal.merchant_contact_status_dal.status_id = int.Parse(dt.Rows[0]["Merchant_Contact_status_id"].ToString());

                if ((dc.Contains("Merchant_Contact_Status_status")) && (dt.Rows[0]["Merchant_Contact_Status_status"] != DBNull.Value))
                    merchant_contact_dal.merchant_contact_status_dal.status = dt.Rows[0]["Merchant_Contact_Status_status"].ToString();


                /* Merchant */
                if ((dc.Contains("Merchant_merchant_id")) && (dt.Rows[0]["Merchant_merchant_id"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.merchant_id = int.Parse(dt.Rows[0]["Merchant_merchant_id"].ToString());
                    merchant_dal.merchant_id = int.Parse(dt.Rows[0]["Merchant_merchant_id"].ToString());
                }

                if ((dc.Contains("Merchant_company_name")) && (dt.Rows[0]["Merchant_company_name"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.company_name = dt.Rows[0]["Merchant_company_name"].ToString();
                    merchant_dal.company_name = dt.Rows[0]["Merchant_company_name"].ToString();
                }

                if ((dc.Contains("Merchant_address_1")) && (dt.Rows[0]["Merchant_address_1"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.address_1 = dt.Rows[0]["Merchant_address_1"].ToString();
                    merchant_dal.address_1 = dt.Rows[0]["Merchant_address_1"].ToString();
                }

                if ((dc.Contains("Merchant_address_2")) && (dt.Rows[0]["Merchant_address_2"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.address_2 = dt.Rows[0]["Merchant_address_2"].ToString();
                    merchant_dal.address_2 = dt.Rows[0]["Merchant_address_2"].ToString();
                }

                if ((dc.Contains("Merchant_latitude")) && (dt.Rows[0]["Merchant_latitude"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.latitude = double.Parse(dt.Rows[0]["Merchant_latitude"].ToString());
                    merchant_dal.latitude = double.Parse(dt.Rows[0]["Merchant_latitude"].ToString());
                }

                if ((dc.Contains("Merchant_longitude")) && (dt.Rows[0]["Merchant_longitude"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.longitude = double.Parse(dt.Rows[0]["Merchant_longitude"].ToString());
                    merchant_dal.longitude = double.Parse(dt.Rows[0]["Merchant_longitude"].ToString());
                }

                if ((dc.Contains("Merchant_phone")) && (dt.Rows[0]["Merchant_phone"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.phone = dt.Rows[0]["Merchant_phone"].ToString();
                    merchant_dal.phone = dt.Rows[0]["Merchant_phone"].ToString();
                }

                if ((dc.Contains("Merchant_website")) && (dt.Rows[0]["Merchant_website"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.website = dt.Rows[0]["Merchant_website"].ToString();
                    merchant_dal.website = dt.Rows[0]["Merchant_website"].ToString();
                }

                if ((dc.Contains("Merchant_description")) && (dt.Rows[0]["Merchant_description"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.description = dt.Rows[0]["Merchant_description"].ToString();
                    merchant_dal.description = dt.Rows[0]["Merchant_description"].ToString();
                }

                if ((dc.Contains("Merchant_image")) && (dt.Rows[0]["Merchant_image"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.image = dt.Rows[0]["Merchant_image"].ToString();
                    merchant_dal.image = dt.Rows[0]["Merchant_image"].ToString();
                }

                if ((dc.Contains("Merchant_max_active_deals")) && (dt.Rows[0]["Merchant_max_active_deals"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.max_active_deals = int.Parse(dt.Rows[0]["Merchant_max_active_deals"].ToString());
                    merchant_dal.max_active_deals = int.Parse(dt.Rows[0]["Merchant_max_active_deals"].ToString());
                }

                if ((dc.Contains("Merchant_current_active_deals")) && (dt.Rows[0]["Merchant_current_active_deals"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.current_active_deals = int.Parse(dt.Rows[0]["Merchant_current_active_deals"].ToString());
                    merchant_dal.current_active_deals = int.Parse(dt.Rows[0]["Merchant_current_active_deals"].ToString());
                }

                if ((dc.Contains("Merchant_yelp_business_id")) && (dt.Rows[0]["Merchant_yelp_business_id"] != DBNull.Value))
                {
                    merchant_contact_dal.merchant_dal.yelp_business_id = dt.Rows[0]["Merchant_yelp_business_id"].ToString();
                    merchant_dal.yelp_business_id = dt.Rows[0]["Merchant_yelp_business_id"].ToString();
                }

                /* Merchant Status */
                if ((dc.Contains("Merchant_Status_status")) && (dt.Rows[0]["Merchant_Status_status"] != DBNull.Value))
                    merchant_dal.merchant_status_dal.status = dt.Rows[0]["Merchant_Status_status"].ToString();


                /* Merchant Rating */
                if ((dc.Contains("Merchant_Rating_rating_id")) && (dt.Rows[0]["Merchant_Rating_rating_id"] != DBNull.Value))
                    merchant_dal.merchant_rating_dal.rating_id = int.Parse(dt.Rows[0]["Merchant_Rating_rating_id"].ToString());

                if ((dc.Contains("Merchant_Rating_rating")) && (dt.Rows[0]["Merchant_Rating_rating"] != DBNull.Value))
                    merchant_dal.merchant_rating_dal.rating = Decimal.Parse(dt.Rows[0]["Merchant_Rating_rating"].ToString());

                if ((dc.Contains("Merchant_Rating_image")) && (dt.Rows[0]["Merchant_Rating_image"] != DBNull.Value))
                    merchant_dal.merchant_rating_dal.image = dt.Rows[0]["Merchant_Rating_image"].ToString();


                /* City */
                if ((dc.Contains("Merchant_City_city")) && (dt.Rows[0]["Merchant_City_city"] != DBNull.Value))
                    merchant_dal.zip_code_dal.city_dal.city = dt.Rows[0]["Merchant_City_city"].ToString();


                /* State */
                if ((dc.Contains("Merchant_State_abbreviation")) && (dt.Rows[0]["Merchant_State_abbreviation"] != DBNull.Value))
                    merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation = dt.Rows[0]["Merchant_State_abbreviation"].ToString();


                /* Zip Code */
                if ((dc.Contains("Merchant_zip_code")) && (dt.Rows[0]["Merchant_zip_code"] != DBNull.Value))
                    merchant_dal.zip_code_dal.zip_code = dt.Rows[0]["Merchant_zip_code"].ToString();


                /* Time Zone */
                if ((dc.Contains("Merchant_Time_Zone_time_zone_id")) && (dt.Rows[0]["Merchant_Time_Zone_time_zone_id"] != DBNull.Value))
                    merchant_dal.zip_code_dal.time_zone_dal.time_zone_id = int.Parse(dt.Rows[0]["Merchant_Time_Zone_time_zone_id"].ToString());

                if ((dc.Contains("Merchant_Time_Zone_abbreviation")) && (dt.Rows[0]["Merchant_Time_Zone_abbreviation"] != DBNull.Value))
                    merchant_dal.zip_code_dal.time_zone_dal.abbreviation = dt.Rows[0]["Merchant_Time_Zone_abbreviation"].ToString();

                if ((dc.Contains("Merchant_Time_Zone_time_zone")) && (dt.Rows[0]["Merchant_Time_Zone_time_zone"] != DBNull.Value))
                    merchant_dal.zip_code_dal.time_zone_dal.time_zone = dt.Rows[0]["Merchant_Time_Zone_time_zone"].ToString();

                if ((dc.Contains("Merchant_Time_Zone_offset")) && (dt.Rows[0]["Merchant_Time_Zone_offset"] != DBNull.Value))
                    merchant_dal.zip_code_dal.time_zone_dal.offset = int.Parse(dt.Rows[0]["Merchant_Time_Zone_offset"].ToString());


                /* Industry */
                if ((dc.Contains("Industry_industry_id")) && (dt.Rows[0]["Industry_industry_id"] != DBNull.Value))
                    merchant_dal.industry_dal.industry_id = int.Parse(dt.Rows[0]["Industry_industry_id"].ToString());

                if ((dc.Contains("Industry_industry")) && (dt.Rows[0]["Industry_industry"] != DBNull.Value))
                    merchant_dal.industry_dal.industry = dt.Rows[0]["Industry_industry"].ToString();


                /* Neighborhood */
                if ((dc.Contains("Neighborhood_neighborhood_id")) && (dt.Rows[0]["Neighborhood_neighborhood_id"] != DBNull.Value))
                    merchant_dal.neighborhood_dal.neighborhood_id = int.Parse(dt.Rows[0]["Neighborhood_neighborhood_id"].ToString());

                if ((dc.Contains("Neighborhood_neighborhood")) && (dt.Rows[0]["Neighborhood_neighborhood"] != DBNull.Value))
                    merchant_dal.neighborhood_dal.neighborhood = dt.Rows[0]["Neighborhood_neighborhood"].ToString();
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

                if ((dc.Contains("Credit_Card_merchant_contact_id")) && (dr["Credit_Card_merchant_contact_id"] != DBNull.Value))
                    credit_card_dal.merchant_contact_id = int.Parse(dr["Credit_Card_merchant_contact_id"].ToString());

                if ((dc.Contains("Credit_Card_card_holder_name")) && (dr["Credit_Card_card_holder_name"] != DBNull.Value))
                    credit_card_dal.card_holder_name = dr["Credit_Card_card_holder_name"].ToString();

                if ((dc.Contains("Credit_Card_card_number")) && (dr["Credit_Card_card_number"] != DBNull.Value))
                    credit_card_dal.card_number = dr["Credit_Card_card_number"].ToString();

                if ((dc.Contains("Credit_Card_card_number_decrypted")) && (dr["Credit_Card_card_number_decrypted"] != DBNull.Value))
                    credit_card_dal.card_number_decrypted = dr["Credit_Card_card_number_decrypted"].ToString();

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

        private void Load_Merchant_Status_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Merchant_Status_status_id")) && (dr["Merchant_Status_status_id"] != DBNull.Value))
                    merchant_status_dal.status_id = int.Parse(dr["Merchant_Status_status_id"].ToString());

                if ((dc.Contains("Merchant_Status_status")) && (dr["Merchant_Status_status"] != DBNull.Value))
                    merchant_status_dal.status = dr["Merchant_Status_status"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Merchant_Rating_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Merchant_Rating_rating_id")) && (dr["Merchant_Rating_rating_id"] != DBNull.Value))
                    merchant_rating_dal.rating_id = int.Parse(dr["Merchant_Rating_rating_id"].ToString());

                if ((dc.Contains("Merchant_Rating_rating")) && (dr["Merchant_Rating_rating"] != DBNull.Value))
                    merchant_rating_dal.rating = decimal.Parse(dr["Merchant_Rating_rating"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Merchant_Contact_Status_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Merchant_Contact_Status_status_id")) && (dr["Merchant_Contact_Status_status_id"] != DBNull.Value))
                    merchant_contact_status_dal.status_id = int.Parse(dr["Merchant_Contact_Status_status_id"].ToString());

                if ((dc.Contains("Merchant_Contact_Status_status")) && (dr["Merchant_Contact_Status_status"] != DBNull.Value))
                    merchant_contact_status_dal.status = dr["Merchant_Contact_Status_status"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region properties

        public DAL.Merchant merchant_dal;
        public DAL.Merchant_Contact merchant_contact_dal;
        public DAL.Merchant_Contact_Status merchant_contact_status_dal;
        public DAL.Merchant_Rating merchant_rating_dal { get; set; }
        public DAL.Merchant_Status merchant_status_dal { get; set; }

        public List<DAL.Merchant_Contact> merchant_contact_dal_array;
        public List<DAL.Merchant_Contact_Status> merchant_contact_status_dal_array;
        public List<DAL.Merchant_Rating> merchant_rating_dal_array;
        public List<DAL.Merchant_Status> merchant_status_dal_array;

        #endregion
    }
}
