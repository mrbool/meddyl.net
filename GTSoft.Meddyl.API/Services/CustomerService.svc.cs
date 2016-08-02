using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using GTSoft.Meddyl;


namespace GTSoft.Meddyl.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.

    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)] 

    public class CustomerService : ICustomerService
    {
        #region constructors

        public CustomerService()
        {
            system_bll = new BLL.System();
            json_to_dal = new JSON_To_Data();
            dal_to_json = new DAL_To_JSON();
            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();
            error_response = new JSONErrorResponse();
            successful_response = new JSONSuccessfulResponse();
        }

        #endregion


        #region system

        public JSONResponse Get_Application_Settings(Login_Log login_log_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(login_log_obj), true);

                if (system_bll.successful)
                {
                    system_bll.Get_Application_Settings(login_log_obj.application_type_obj.application_type_id);

                    if (system_bll.successful)
                    {
                        Application_Type application_type_obj = new Application_Type();
                        application_type_obj.is_down = bool.Parse(system_bll.application_type_dal.is_down.ToString());
                        application_type_obj.down_message = system_bll.application_type_dal.down_message.ToString();

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(system_bll.system_successful_dal);
                        successful_response.data_obj = application_type_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_System_Settings(System_Settings system_settings_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(system_settings_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    system_settings_obj.customer_app_android_id = system_bll.system_settings_dal.customer_app_android_id.ToString();
                    system_settings_obj.customer_app_ios_id = system_bll.system_settings_dal.customer_app_ios_id.ToString();
                    system_settings_obj.customer_app_terms = system_bll.system_settings_dal.customer_app_terms.ToString();
                    system_settings_obj.customer_deal_range_min = int.Parse(system_bll.system_settings_dal.customer_deal_range_min.ToString());
                    system_settings_obj.customer_deal_range_max = int.Parse(system_bll.system_settings_dal.customer_deal_range_max.ToString());
                    system_settings_obj.gps_accuracy = system_bll.system_settings_dal.gps_accuracy.ToString();
                    system_settings_obj.gps_timeout = int.Parse(system_bll.system_settings_dal.gps_timeout.ToString());
                    system_settings_obj.merchant_app_android_id = system_bll.system_settings_dal.merchant_app_android_id.ToString();
                    system_settings_obj.merchant_app_ios_id = system_bll.system_settings_dal.merchant_app_ios_id.ToString();

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(system_bll.system_successful_dal);
                    successful_response.data_obj = system_settings_obj;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Industry_Parent_Level(Industry industry_obj)
        {
            try
            {
                system_bll.Get_Industry_Parent_Level(int.Parse(industry_obj.parent_industry_id.ToString()));

                if (system_bll.successful)
                {
                    List<Industry> industry_obj_array = new List<Industry>();

                    foreach (DAL.Industry industry_row in system_bll.industry_dal_array)
                    {
                        industry_obj = new Industry();
                        industry_obj.industry_id = int.Parse(industry_row.industry_id.ToString());
                        industry_obj.industry = industry_row.industry.ToString();

                        industry_obj_array.Add(industry_obj);
                    }

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(system_bll.system_successful_dal);
                    successful_response.data_obj = industry_obj_array;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        #endregion


        #region customer

        public JSONResponse Register(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = customer_obj.login_log_obj.application_type_obj.application_type_id;

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.auto_login = customer_obj.login_log_obj.auto_login;
                    login_log_dal.ip_address = customer_obj.login_log_obj.ip_address;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = customer_obj.contact_obj.zip_code_obj.zip_code;
                    zip_code_dal.latitude = customer_obj.contact_obj.zip_code_obj.latitude;
                    zip_code_dal.longitude = customer_obj.contact_obj.zip_code_obj.longitude;
                    
                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.first_name = customer_obj.contact_obj.first_name.Trim();
                    contact_dal.last_name = customer_obj.contact_obj.last_name.Trim();
                    contact_dal.email = customer_obj.contact_obj.email.Trim();
                    contact_dal.user_name = customer_obj.contact_obj.email.Trim();
                    contact_dal.password = customer_obj.contact_obj.password.Trim();
                    contact_dal.phone = "";
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;
                    customer_dal.login_log_dal = login_log_dal;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Register();

                    if (customer_bll.successful)
                    {
                        State state_obj = new State();
                        state_obj.abbreviation = customer_bll.contact_dal.zip_code_dal.city_dal.state_dal.state.ToString();

                        City city_obj = new City();
                        city_obj.city = customer_bll.contact_dal.zip_code_dal.city_dal.city.ToString();
                        city_obj.state_obj = state_obj;

                        Zip_Code zip_code_obj = new Zip_Code();
                        zip_code_obj.zip_code = customer_bll.contact_dal.zip_code_dal.zip_code.ToString();
                        zip_code_obj.city_obj = city_obj;

                        Contact contact_obj = new Contact();
                        contact_obj.contact_id = int.Parse(customer_bll.contact_dal.contact_id.ToString());
                        if (!customer_bll.contact_dal.facebook_id.IsNull)
                            contact_obj.facebook_id = long.Parse(customer_bll.contact_dal.facebook_id.ToString());
                        contact_obj.first_name = customer_bll.contact_dal.first_name.ToString();
                        contact_obj.last_name = customer_bll.contact_dal.last_name.ToString();
                        contact_obj.email = customer_bll.contact_dal.email.ToString();
                        contact_obj.password = customer_bll.contact_dal.password.ToString();
                        contact_obj.zip_code_obj = zip_code_obj;

                        Customer_Search_Location_Type customer_search_location_type_obj = new Customer_Search_Location_Type();
                        customer_search_location_type_obj.search_location_type_id = int.Parse(customer_bll.customer_dal.customer_search_location_type_dal.search_location_type_id.ToString());
                        customer_search_location_type_obj.location_type = customer_bll.customer_dal.customer_search_location_type_dal.location_type.ToString();

                        Industry industry_obj = new Industry();
                        industry_obj.industry = customer_bll.customer_dal.industry_dal.industry.ToString();

                        Zip_Code search_zip_code_obj = new Zip_Code();
                        search_zip_code_obj.zip_code = customer_bll.customer_dal.zip_code_dal.zip_code.ToString();

                        Promotion promotion_obj = new Promotion();
                        if (!customer_bll.customer_dal.promotion_dal.promotion_id.IsNull)
                        {
                            promotion_obj.promotion_id = int.Parse(customer_bll.customer_dal.promotion_dal.promotion_id.ToString());
                            promotion_obj.promotion_code = customer_bll.customer_dal.promotion_dal.promotion_code.ToString();
                            promotion_obj.link = customer_bll.customer_dal.promotion_dal.link.ToString();
                        }

                        customer_obj = new Customer();
                        customer_obj.customer_id = int.Parse(customer_bll.customer_dal.customer_id.ToString());
                        customer_obj.deal_range = int.Parse(customer_bll.customer_dal.deal_range.ToString());
                        customer_obj.industry_obj = industry_obj;
                        customer_obj.contact_obj = contact_obj;
                        customer_obj.customer_search_location_type_obj = customer_search_location_type_obj;
                        customer_obj.zip_code_obj = search_zip_code_obj;
                        customer_obj.promotion_obj = promotion_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = customer_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Login(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = customer_obj.login_log_obj.application_type_obj.application_type_id;

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.auto_login = customer_obj.login_log_obj.auto_login;
                    login_log_dal.ip_address = customer_obj.login_log_obj.ip_address;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.latitude = customer_obj.contact_obj.zip_code_obj.latitude;
                    zip_code_dal.longitude = customer_obj.contact_obj.zip_code_obj.longitude;
                    
                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = customer_obj.contact_obj.user_name.Trim();
                    contact_dal.password = customer_obj.contact_obj.password.Trim();
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;
                    customer_dal.login_log_dal = login_log_dal;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Login();

                    if (customer_bll.successful)
                    {
                        State state_obj = new State();
                        state_obj.abbreviation = customer_bll.contact_dal.zip_code_dal.city_dal.state_dal.state.ToString();

                        City city_obj = new City();
                        city_obj.city = customer_bll.contact_dal.zip_code_dal.city_dal.city.ToString();
                        city_obj.state_obj = state_obj;

                        Zip_Code zip_code_obj = new Zip_Code();
                        zip_code_obj.zip_code = customer_bll.contact_dal.zip_code_dal.zip_code.ToString();
                        zip_code_obj.city_obj = city_obj;

                        Contact contact_obj = new Contact();
                        contact_obj.contact_id = int.Parse(customer_bll.contact_dal.contact_id.ToString());
                        if (!customer_bll.contact_dal.facebook_id.IsNull)
                            contact_obj.facebook_id = long.Parse(customer_bll.contact_dal.facebook_id.ToString());
                        contact_obj.first_name = customer_bll.contact_dal.first_name.ToString();
                        contact_obj.last_name = customer_bll.contact_dal.last_name.ToString();
                        contact_obj.email = customer_bll.contact_dal.email.ToString();
                        contact_obj.password = customer_bll.contact_dal.password.ToString();
                        contact_obj.zip_code_obj = zip_code_obj;

                        Customer_Search_Location_Type customer_search_location_type_obj = new Customer_Search_Location_Type();
                        customer_search_location_type_obj.search_location_type_id = int.Parse(customer_bll.customer_dal.customer_search_location_type_dal.search_location_type_id.ToString());
                        customer_search_location_type_obj.location_type = customer_bll.customer_dal.customer_search_location_type_dal.location_type.ToString();

                        Industry industry_obj = new Industry();
                        industry_obj.industry = customer_bll.customer_dal.industry_dal.industry.ToString();

                        Zip_Code search_zip_code_obj = new Zip_Code();
                        search_zip_code_obj.zip_code = customer_bll.customer_dal.zip_code_dal.zip_code.ToString();

                        Promotion promotion_obj = new Promotion();
                        if (!customer_bll.customer_dal.promotion_dal.promotion_id.IsNull)
                        {
                            promotion_obj.promotion_id = int.Parse(customer_bll.customer_dal.promotion_dal.promotion_id.ToString());
                            promotion_obj.promotion_code = customer_bll.customer_dal.promotion_dal.promotion_code.ToString();
                            promotion_obj.link = customer_bll.customer_dal.promotion_dal.link.ToString();
                        }

                        customer_obj = new Customer();
                        customer_obj.customer_id = int.Parse(customer_bll.customer_dal.customer_id.ToString());
                        customer_obj.deal_range = int.Parse(customer_bll.customer_dal.deal_range.ToString());
                        customer_obj.industry_obj = industry_obj;
                        customer_obj.contact_obj = contact_obj;
                        customer_obj.customer_search_location_type_obj = customer_search_location_type_obj;
                        customer_obj.zip_code_obj = search_zip_code_obj;
                        customer_obj.promotion_obj = promotion_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = customer_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Login_With_Facebook(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = customer_obj.login_log_obj.application_type_obj.application_type_id;

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.auto_login = customer_obj.login_log_obj.auto_login;
                    login_log_dal.ip_address = customer_obj.login_log_obj.ip_address;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = customer_obj.contact_obj.zip_code_obj.zip_code;
                    zip_code_dal.latitude = customer_obj.contact_obj.zip_code_obj.latitude;
                    zip_code_dal.longitude = customer_obj.contact_obj.zip_code_obj.longitude;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = customer_obj.contact_obj.user_name;
                    contact_dal.password = customer_obj.contact_obj.password;
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;
                    customer_dal.login_log_dal = login_log_dal;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Login_With_Facebook("", customer_obj.login_log_obj.auth_token);

                    if (customer_bll.successful)
                    {
                        State state_obj = new State();
                        state_obj.abbreviation = customer_bll.contact_dal.zip_code_dal.city_dal.state_dal.state.ToString();

                        City city_obj = new City();
                        city_obj.city = customer_bll.contact_dal.zip_code_dal.city_dal.city.ToString();
                        city_obj.state_obj = state_obj;

                        Zip_Code zip_code_obj = new Zip_Code();
                        zip_code_obj.zip_code = customer_bll.contact_dal.zip_code_dal.zip_code.ToString();
                        zip_code_obj.city_obj = city_obj;

                        Contact contact_obj = new Contact();
                        contact_obj.contact_id = int.Parse(customer_bll.contact_dal.contact_id.ToString());
                        if (!customer_bll.contact_dal.facebook_id.IsNull)
                            contact_obj.facebook_id = long.Parse(customer_bll.contact_dal.facebook_id.ToString());
                        contact_obj.first_name = customer_bll.contact_dal.first_name.ToString();
                        contact_obj.last_name = customer_bll.contact_dal.last_name.ToString();
                        contact_obj.email = customer_bll.contact_dal.email.ToString();
                        contact_obj.password = customer_bll.contact_dal.password.ToString();
                        contact_obj.zip_code_obj = zip_code_obj;

                        Customer_Search_Location_Type customer_search_location_type_obj = new Customer_Search_Location_Type();
                        customer_search_location_type_obj.search_location_type_id = int.Parse(customer_bll.customer_dal.customer_search_location_type_dal.search_location_type_id.ToString());
                        customer_search_location_type_obj.location_type = customer_bll.customer_dal.customer_search_location_type_dal.location_type.ToString();

                        Industry industry_obj = new Industry();
                        industry_obj.industry = customer_bll.customer_dal.industry_dal.industry.ToString();

                        Zip_Code search_zip_code_obj = new Zip_Code();
                        search_zip_code_obj.zip_code = customer_bll.customer_dal.zip_code_dal.zip_code.ToString();

                        Promotion promotion_obj = new Promotion();
                        if (!customer_bll.customer_dal.promotion_dal.promotion_id.IsNull)
                        {
                            promotion_obj.promotion_id = int.Parse(customer_bll.customer_dal.promotion_dal.promotion_id.ToString());
                            promotion_obj.promotion_code = customer_bll.customer_dal.promotion_dal.promotion_code.ToString();
                            promotion_obj.link = customer_bll.customer_dal.promotion_dal.link.ToString();
                        }

                        customer_obj = new Customer();
                        customer_obj.customer_id = int.Parse(customer_bll.customer_dal.customer_id.ToString());
                        customer_obj.deal_range = int.Parse(customer_bll.customer_dal.deal_range.ToString());
                        customer_obj.industry_obj = industry_obj;
                        customer_obj.contact_obj = contact_obj;
                        customer_obj.customer_search_location_type_obj = customer_search_location_type_obj;
                        customer_obj.zip_code_obj = search_zip_code_obj;
                        customer_obj.promotion_obj = promotion_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = customer_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Forgot_Password(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = customer_obj.contact_obj.user_name.Trim();

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.contact_dal = contact_dal;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Forgot_Password();

                    if (customer_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Customer_Profile(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);

                    State state_obj = new State();
                    state_obj.abbreviation = customer_bll.contact_dal.zip_code_dal.city_dal.state_dal.state.ToString();

                    City city_obj = new City();
                    city_obj.city = customer_bll.contact_dal.zip_code_dal.city_dal.city.ToString();
                    city_obj.state_obj = state_obj;

                    Zip_Code zip_code_obj = new Zip_Code();
                    zip_code_obj.zip_code = customer_bll.contact_dal.zip_code_dal.zip_code.ToString();
                    zip_code_obj.city_obj = city_obj;

                    Contact contact_obj = new Contact();
                    contact_obj.contact_id = int.Parse(customer_bll.contact_dal.contact_id.ToString());
                    if (!customer_bll.contact_dal.facebook_id.IsNull)
                        contact_obj.facebook_id = long.Parse(customer_bll.contact_dal.facebook_id.ToString());
                    contact_obj.first_name = customer_bll.contact_dal.first_name.ToString();
                    contact_obj.last_name = customer_bll.contact_dal.last_name.ToString();
                    contact_obj.email = customer_bll.contact_dal.email.ToString();
                    contact_obj.password = customer_bll.contact_dal.password.ToString();
                    contact_obj.zip_code_obj = zip_code_obj;

                    Customer_Search_Location_Type customer_search_location_type_obj = new Customer_Search_Location_Type();
                    customer_search_location_type_obj.search_location_type_id = int.Parse(customer_bll.customer_dal.customer_search_location_type_dal.search_location_type_id.ToString());
                    customer_search_location_type_obj.location_type = customer_bll.customer_dal.customer_search_location_type_dal.location_type.ToString();

                    Industry industry_obj = new Industry();
                    industry_obj.industry = customer_bll.customer_dal.industry_dal.industry.ToString();

                    Zip_Code search_zip_code_obj = new Zip_Code();
                    search_zip_code_obj.zip_code = customer_bll.customer_dal.zip_code_dal.zip_code.ToString();

                    Promotion promotion_obj = new Promotion();
                    if (!customer_bll.customer_dal.promotion_dal.promotion_id.IsNull)
                    {
                        promotion_obj.promotion_id = int.Parse(customer_bll.customer_dal.promotion_dal.promotion_id.ToString());
                        promotion_obj.promotion_code = customer_bll.customer_dal.promotion_dal.promotion_code.ToString();
                        promotion_obj.link = customer_bll.customer_dal.promotion_dal.link.ToString();
                    }

                    customer_obj = new Customer();
                    customer_obj.customer_id = int.Parse(customer_bll.customer_dal.customer_id.ToString());
                    customer_obj.deal_range = int.Parse(customer_bll.customer_dal.deal_range.ToString());
                    customer_obj.industry_obj = industry_obj;
                    customer_obj.contact_obj = contact_obj;
                    customer_obj.customer_search_location_type_obj = customer_search_location_type_obj;
                    customer_obj.zip_code_obj = search_zip_code_obj;
                    customer_obj.promotion_obj = promotion_obj;

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                    successful_response.data_obj = customer_obj;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Update_Customer(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = customer_obj.contact_obj.zip_code_obj.zip_code;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.first_name = customer_obj.contact_obj.first_name;
                    contact_dal.last_name = customer_obj.contact_obj.last_name;
                    contact_dal.email = customer_obj.contact_obj.email;
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Customer customer_dal_update = new DAL.Customer();
                    customer_dal_update.customer_id = customer_obj.customer_id;
                    customer_dal_update.contact_dal = contact_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Update_Customer(customer_dal_update);

                    if (customer_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Update_Customer_Settings(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = customer_obj.zip_code_obj.zip_code;

                    DAL.Customer_Search_Location_Type customer_search_location_type_dal = new DAL.Customer_Search_Location_Type();
                    customer_search_location_type_dal.search_location_type_id = customer_obj.customer_search_location_type_obj.search_location_type_id;

                    DAL.Customer customer_dal_update = new DAL.Customer();
                    customer_dal_update.customer_id = customer_obj.customer_id;
                    customer_dal_update.deal_range = customer_obj.deal_range;
                    customer_dal_update.zip_code_dal = zip_code_dal;
                    customer_dal_update.customer_search_location_type_dal = customer_search_location_type_dal;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Update_Customer_Settings(customer_dal_update);

                    if (customer_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Add_Credit_Card(Credit_Card credit_card_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(credit_card_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = credit_card_obj.customer_obj.customer_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.card_holder_name = credit_card_obj.card_holder_name.Trim();
                    credit_card_dal.card_number = credit_card_obj.card_number.Trim();
                    credit_card_dal.expiration_date = credit_card_obj.expiration_date.Trim();
                    credit_card_dal.billing_zip_code = credit_card_obj.billing_zip_code.Trim();
                    credit_card_dal.security_code = credit_card_obj.security_code.Trim();
                    credit_card_dal.customer_dal = customer_dal;

                    BLL.Customer customer_bll = new BLL.Customer(credit_card_dal);
                    customer_bll.Add_Credit_Card();

                    if (customer_bll.successful)
                    {
                        Credit_Card_Type credit_card_type_obj = new Credit_Card_Type();
                        credit_card_type_obj.type = customer_bll.credit_card_dal.credit_card_type_dal.type.ToString();
                        credit_card_type_obj.image = customer_bll.credit_card_dal.credit_card_type_dal.image.ToString();

                        credit_card_obj = new Credit_Card();
                        credit_card_obj.credit_card_id = int.Parse(customer_bll.credit_card_dal.credit_card_id.ToString());
                        credit_card_obj.card_number = customer_bll.credit_card_dal.card_number.ToString();
                        credit_card_obj.credit_card_type_obj = credit_card_type_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = credit_card_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Delete_Credit_Card(Credit_Card credit_card_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(credit_card_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = credit_card_obj.customer_obj.customer_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = credit_card_obj.credit_card_id;
                    credit_card_dal.customer_dal = customer_dal;

                    BLL.Customer customer_bll = new BLL.Customer(credit_card_dal);
                    customer_bll.Delete_Credit_Card();

                    if (customer_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Credit_Cards(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Get_Credit_Cards();

                    if (customer_bll.successful)
                    {
                        List<Credit_Card> credit_card_obj_array = new List<Credit_Card>();

                        foreach (DAL.Credit_Card credit_card_dal in customer_bll.credit_card_dal_array)
                        {
                            Credit_Card_Type credit_card_type_obj = new Credit_Card_Type();
                            credit_card_type_obj.type = credit_card_dal.credit_card_type_dal.type.ToString();
                            credit_card_type_obj.image = credit_card_dal.credit_card_type_dal.image.ToString();

                            Credit_Card credit_card_obj = new Credit_Card();
                            credit_card_obj.credit_card_id = int.Parse(credit_card_dal.credit_card_id.ToString());
                            credit_card_obj.card_number = credit_card_dal.card_number.ToString();
                            credit_card_obj.expiration_date = credit_card_dal.expiration_date.ToString();
                            credit_card_obj.default_flag = bool.Parse(credit_card_dal.default_flag.ToString());
                            credit_card_obj.credit_card_type_obj = credit_card_type_obj;

                            credit_card_obj_array.Add(credit_card_obj);
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = credit_card_obj_array;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Set_Default_Credit_Card(Credit_Card credit_card_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(credit_card_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = credit_card_obj.customer_obj.customer_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = credit_card_obj.credit_card_id;
                    credit_card_dal.customer_dal = customer_dal;

                    BLL.Customer customer_bll = new BLL.Customer(credit_card_dal);
                    customer_bll.Set_Default_Credit_Card();

                    if (customer_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Valid_Promotions(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Customer customer_bll = new BLL.Customer(customer_dal);
                    customer_bll.Get_Valid_Promotions();

                    if (customer_bll.successful)
                    {
                        List<Promotion_Activity> promotion_activity_obj_array = new List<Promotion_Activity>();

                        foreach (DAL.Promotion_Activity promotion_activity_dal in customer_bll.promotion_activity_dal_array)
                        {
                            Promotion promotion_obj = new Promotion();
                            promotion_obj.promotion_id = int.Parse(promotion_activity_dal.promotion_dal.promotion_id.ToString());
                            promotion_obj.promotion_code = promotion_activity_dal.promotion_dal.promotion_code.ToString();
                            promotion_obj.description = promotion_activity_dal.promotion_dal.description.ToString();

                            Promotion_Activity promotion_activity_obj = new Promotion_Activity();
                            promotion_activity_dal.promotion_activity_id = int.Parse(promotion_activity_dal.promotion_activity_id.ToString());
                            promotion_activity_obj.promotion_obj = promotion_obj;

                            promotion_activity_obj_array.Add(promotion_activity_obj);
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(customer_bll.system_successful_dal);
                        successful_response.data_obj = promotion_activity_obj_array;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(customer_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        #endregion


        #region deal

        public JSONResponse Get_Deals(int customer_id, float latitude, float longitude)
        {
            try
            {
                DAL.Deal deal_dal= null;

                DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                zip_code_dal.latitude = latitude;
                zip_code_dal.longitude = longitude;

                DAL.Contact contact_dal = new DAL.Contact();
                contact_dal.zip_code_dal = zip_code_dal;

                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.customer_id = customer_id;
                customer_dal.contact_dal = contact_dal;

                BLL.Deal deal_bll = new BLL.Deal(deal_dal, customer_dal);
                deal_bll.Get_Customer_Deals();

                if (deal_bll.successful)
                {
                    List<Deal> deal_obj_array = new List<Deal>();

                    foreach (DAL.Deal deal_row in deal_bll.deal_dal_array)
                    {
                        State state_obj = new State();
                        state_obj.abbreviation = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                        City city_obj = new City();
                        city_obj.city = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                        city_obj.state_obj = state_obj;

                        Time_Zone time_zone_obj = new Time_Zone();
                        time_zone_obj.abbreviation = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();
                        time_zone_obj.time_zone = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.time_zone.ToString();
                        time_zone_obj.offset = int.Parse(deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.offset.ToString());

                        Zip_Code zip_code_obj = new Zip_Code();
                        zip_code_obj.zip_code = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                        zip_code_obj.city_obj = city_obj;
                        zip_code_obj.time_zone_obj = time_zone_obj;

                        Neighborhood neighborhood_obj = new Neighborhood();
                        if (!deal_row.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.IsNull)
                        {
                            neighborhood_obj.neighborhood_id = int.Parse(deal_row.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.ToString());
                            neighborhood_obj.neighborhood = deal_row.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood.ToString();
                        }

                        Merchant_Rating merchant_rating_obj = new Merchant_Rating();
                        if (!deal_row.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.IsNull)
                        {
                            merchant_rating_obj.rating_id = int.Parse(deal_row.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.ToString());
                            merchant_rating_obj.rating = Decimal.Parse(deal_row.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating.ToString());
                            merchant_rating_obj.image = deal_row.merchant_contact_dal.merchant_dal.merchant_rating_dal.image.ToString();
                        }

                        Merchant merchant_obj = new Merchant();
                        merchant_obj.company_name = deal_row.merchant_contact_dal.merchant_dal.company_name.ToString();
                        merchant_obj.description = deal_row.merchant_contact_dal.merchant_dal.description.ToString();
                        merchant_obj.address_1 = deal_row.merchant_contact_dal.merchant_dal.address_1.ToString();
                        merchant_obj.address_2 = deal_row.merchant_contact_dal.merchant_dal.address_2.ToString();
                        merchant_obj.latitude = double.Parse(deal_row.merchant_contact_dal.merchant_dal.latitude.ToString());
                        merchant_obj.longitude = double.Parse(deal_row.merchant_contact_dal.merchant_dal.longitude.ToString());
                        merchant_obj.phone = deal_row.merchant_contact_dal.merchant_dal.phone.ToString();
                        merchant_obj.website = deal_row.merchant_contact_dal.merchant_dal.website.ToString();
                        merchant_obj.image = deal_row.merchant_contact_dal.merchant_dal.image.ToString();
                        merchant_obj.zip_code_obj = zip_code_obj;
                        merchant_obj.neighborhood_obj = neighborhood_obj;
                        merchant_obj.merchant_rating_obj = merchant_rating_obj;

                        Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                        merchant_contact_obj.merchant_obj = merchant_obj;

                        Deal_Status deal_status_obj = new Deal_Status();
                        deal_status_obj.status = deal_row.deal_status_dal.status.ToString();

                        Time_Zone deal_time_zone_obj = new Time_Zone();
                        deal_time_zone_obj.abbreviation = deal_row.time_zone_dal.abbreviation.ToString();
                        deal_time_zone_obj.time_zone = deal_row.time_zone_dal.time_zone.ToString();
                        deal_time_zone_obj.offset = int.Parse(deal_row.time_zone_dal.offset.ToString());

                        Deal deal_obj = new Deal();
                        deal_obj.deal_id = int.Parse(deal_row.deal_id.ToString());
                        deal_obj.deal = deal_row.deal.ToString();
                        deal_obj.fine_print = deal_row.fine_print.ToString();
                        deal_obj.fine_print_ext = deal_row.fine_print_ext.ToString();
                        deal_obj.max_dollar_amount = decimal.Parse(deal_row.max_dollar_amount.ToString());
                        deal_obj.certificate_quantity = int.Parse(deal_row.certificate_quantity.ToString());
                        deal_obj.expiration_date = DateTime.Parse(deal_row.expiration_date.ToString());
                        deal_obj.entry_date_utc_stamp = DateTime.Parse(deal_row.entry_date_utc_stamp.ToString());
                        deal_obj.image = deal_row.image.ToString();
                        deal_obj.certificate_amount = decimal.Parse(deal_row.certificate_amount.ToString());
                        deal_obj.certificates_remaining = int.Parse(deal_row.certificates_remaining.ToString());
                        deal_obj.certificates_redeemed = int.Parse(deal_row.certificates_redeemed.ToString());
                        deal_obj.certificates_unused = int.Parse(deal_row.certificates_unused.ToString());
                        deal_obj.certificates_expired = int.Parse(deal_row.certificates_expired.ToString());
                        deal_obj.last_assigned_date = DateTime.Parse(deal_row.last_assigned_date.ToString());
                        deal_obj.last_redeemed_date = DateTime.Parse(deal_row.last_redeemed_date.ToString());
                        deal_obj.distance = Decimal.Parse(deal_row.distance.ToString());
                        deal_obj.deal_status_obj = deal_status_obj;
                        deal_obj.merchant_contact_obj = merchant_contact_obj;
                        deal_obj.time_zone_obj = deal_time_zone_obj;

                        deal_obj_array.Add(deal_obj);
                    }

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                    successful_response.data_obj = deal_obj_array;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Deal_Detail(Certificate certificate_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = certificate_obj.customer_obj.zip_code_obj.zip_code;
                    zip_code_dal.longitude = certificate_obj.customer_obj.zip_code_obj.longitude;
                    zip_code_dal.latitude = certificate_obj.customer_obj.zip_code_obj.latitude;

                    DAL.Customer_Search_Location_Type customer_search_location_type_dal = new DAL.Customer_Search_Location_Type();
                    customer_search_location_type_dal.search_location_type_id = certificate_obj.customer_obj.customer_search_location_type_obj.search_location_type_id;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = certificate_obj.customer_obj.customer_id;
                    customer_dal.search_location_type_id = certificate_obj.customer_obj.search_location_type_id;
                    customer_dal.customer_search_location_type_dal = customer_search_location_type_dal;
                    customer_dal.zip_code_dal = zip_code_dal;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = certificate_obj.deal_obj.deal_id;

                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.deal_dal = deal_dal;

                    // all you need to do is instantiate the object
                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, customer_dal);

                    State state_obj = new State();
                    state_obj.abbreviation = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                    City city_obj = new City();
                    city_obj.city = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                    city_obj.state_obj = state_obj;

                    Time_Zone time_zone_obj = new Time_Zone();
                    time_zone_obj.abbreviation = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();
                    time_zone_obj.time_zone = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.time_zone.ToString();
                    time_zone_obj.offset = int.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.offset.ToString());

                    Zip_Code zip_code_obj = new Zip_Code();
                    zip_code_obj.zip_code = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                    zip_code_obj.city_obj = city_obj;
                    zip_code_obj.time_zone_obj = time_zone_obj;

                    Neighborhood neighborhood_obj = new Neighborhood();
                    if (!deal_bll.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.IsNull)
                    {
                        neighborhood_obj.neighborhood_id = int.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.ToString());
                        neighborhood_obj.neighborhood = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood.ToString();
                    }

                    Merchant_Rating merchant_rating_obj = new Merchant_Rating();
                    if (!deal_bll.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.IsNull)
                    {
                        merchant_rating_obj.rating_id = int.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.ToString());
                        merchant_rating_obj.rating = Decimal.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating.ToString());
                        merchant_rating_obj.image = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.image.ToString();
                    }

                    Merchant merchant_obj = new Merchant();
                    merchant_obj.company_name = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                    merchant_obj.address_1 = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.address_1.ToString();
                    merchant_obj.address_2 = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.address_2.ToString();
                    merchant_obj.latitude = double.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.latitude.ToString());
                    merchant_obj.longitude = double.Parse(deal_bll.deal_dal.merchant_contact_dal.merchant_dal.longitude.ToString());
                    merchant_obj.phone = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.phone.ToString();
                    merchant_obj.website = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.website.ToString();
                    merchant_obj.description = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.description.ToString();
                    merchant_obj.image = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.image.ToString();
                    merchant_obj.zip_code_obj = zip_code_obj;
                    merchant_obj.neighborhood_obj = neighborhood_obj;
                    merchant_obj.merchant_rating_obj = merchant_rating_obj;

                    Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                    merchant_contact_obj.merchant_obj = merchant_obj;

                    Deal_Status deal_status_obj = new Deal_Status();
                    deal_status_obj.status = deal_bll.deal_dal.deal_status_dal.status.ToString();

                    Time_Zone deal_time_zone_obj = new Time_Zone();
                    deal_time_zone_obj.abbreviation = deal_bll.deal_dal.time_zone_dal.abbreviation.ToString();
                    deal_time_zone_obj.time_zone = deal_bll.deal_dal.time_zone_dal.time_zone.ToString();
                    deal_time_zone_obj.offset = int.Parse(deal_bll.deal_dal.time_zone_dal.offset.ToString());

                    Deal deal_obj = new Deal();
                    deal_obj.deal_id = int.Parse(deal_bll.deal_dal.deal_id.ToString());
                    deal_obj.deal = deal_bll.deal_dal.deal.ToString();
                    deal_obj.fine_print = deal_bll.deal_dal.fine_print.ToString();
                    deal_obj.fine_print_ext = deal_bll.deal_dal.fine_print_ext.ToString();
                    deal_obj.max_dollar_amount = decimal.Parse(deal_bll.deal_dal.max_dollar_amount.ToString());
                    deal_obj.certificate_quantity = int.Parse(deal_bll.deal_dal.certificate_quantity.ToString());
                    deal_obj.expiration_date = DateTime.Parse(deal_bll.deal_dal.expiration_date.ToString());
                    deal_obj.entry_date_utc_stamp = DateTime.Parse(deal_bll.deal_dal.entry_date_utc_stamp.ToString());
                    deal_obj.image = deal_bll.deal_dal.image.ToString();
                    deal_obj.certificate_amount = decimal.Parse(deal_bll.deal_dal.certificate_amount.ToString());
                    deal_obj.certificates_remaining = int.Parse(deal_bll.deal_dal.certificates_remaining.ToString());
                    deal_obj.certificates_redeemed = int.Parse(deal_bll.deal_dal.certificates_redeemed.ToString());
                    deal_obj.certificates_unused = int.Parse(deal_bll.deal_dal.certificates_unused.ToString());
                    deal_obj.certificates_expired = int.Parse(deal_bll.deal_dal.certificates_expired.ToString());
                    deal_obj.last_assigned_date = DateTime.Parse(deal_bll.deal_dal.last_assigned_date.ToString());
                    deal_obj.last_redeemed_date = DateTime.Parse(deal_bll.deal_dal.last_redeemed_date.ToString());
                    deal_obj.distance = Decimal.Parse(deal_bll.deal_dal.distance.ToString());
                    deal_obj.deal_status_obj = deal_status_obj;
                    deal_obj.merchant_contact_obj = merchant_contact_obj;
                    deal_obj.time_zone_obj = deal_time_zone_obj;

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                    successful_response.data_obj = deal_obj;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Payment(Certificate certificate_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = certificate_obj.customer_obj.customer_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = certificate_obj.deal_obj.deal_id;

                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.deal_dal = deal_dal;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, customer_dal);
                    deal_bll.Get_Customer_Payment_Info();

                    if (deal_bll.successful)
                    {
                        Promotion promotion_obj = new Promotion();
                        promotion_obj.promotion_id = int.Parse(deal_bll.promotion_activity_dal.promotion_dal.promotion_id.ToString());
                        promotion_obj.promotion_code = deal_bll.promotion_activity_dal.promotion_dal.promotion_code.ToString();

                        Promotion_Activity promotion_activity_obj = new Promotion_Activity();
                        promotion_activity_obj.promotion_activity_id = int.Parse(deal_bll.promotion_activity_dal.promotion_activity_id.ToString());
                        promotion_activity_obj.promotion_obj = promotion_obj;

                        Credit_Card_Type credit_card_type_obj = new Credit_Card_Type();
                        credit_card_type_obj.type = deal_bll.customer_bll.credit_card_dal.credit_card_type_dal.type.ToString();
                        credit_card_type_obj.image = deal_bll.customer_bll.credit_card_dal.credit_card_type_dal.image.ToString();

                        Credit_Card credit_card_obj = new Credit_Card();
                        credit_card_obj.credit_card_id = int.Parse(deal_bll.customer_bll.credit_card_dal.credit_card_id.ToString());
                        credit_card_obj.card_number = deal_bll.customer_bll.credit_card_dal.card_number.ToString();
                        credit_card_obj.credit_card_type_obj = credit_card_type_obj;
                        
                        Certificate_Payment certificate_payment_obj = new Certificate_Payment();
                        certificate_payment_obj.payment_amount = decimal.Parse(deal_bll.certificate_payment_dal.payment_amount.ToString());
                        certificate_payment_obj.promotion_activity_obj = promotion_activity_obj;
                        certificate_payment_obj.credit_card_obj = credit_card_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = certificate_payment_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Apply_Promotion(Certificate_Payment certificate_payment_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_payment_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = certificate_payment_obj.credit_card_obj.customer_obj.customer_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.customer_dal = customer_dal;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = certificate_payment_obj.certificate_obj.deal_obj.deal_id;

                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.deal_dal = deal_dal;

                    DAL.Promotion promotion_dal = new DAL.Promotion();
                    promotion_dal.promotion_code = certificate_payment_obj.promotion_activity_obj.promotion_obj.promotion_code.Trim();

                    DAL.Promotion_Activity promotion_activity_dal = new DAL.Promotion_Activity();
                    promotion_activity_dal.promotion_dal = promotion_dal;

                    DAL.Certificate_Payment certificate_payment_dal = new DAL.Certificate_Payment();
                    certificate_payment_dal.promotion_activity_dal = promotion_activity_dal;
                    certificate_payment_dal.credit_card_dal = credit_card_dal;
                    certificate_payment_dal.certificate_dal = certificate_dal;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_payment_dal);
                    deal_bll.Apply_Promotion_For_Customer();

                    if (deal_bll.successful)
                    {
                        Promotion promotion_obj = new Promotion();
                        promotion_obj.promotion_id = int.Parse(deal_bll.promotion_activity_dal.promotion_dal.promotion_id.ToString());
                        promotion_obj.promotion_code = deal_bll.promotion_activity_dal.promotion_dal.promotion_code.ToString();

                        Promotion_Activity promotion_activity_obj = new Promotion_Activity();
                        promotion_activity_obj.promotion_obj = promotion_obj;

                        Credit_Card_Type credit_card_type_obj = new Credit_Card_Type();
                        credit_card_type_obj.type = deal_bll.customer_bll.credit_card_dal.credit_card_type_dal.type.ToString();
                        credit_card_type_obj.image = deal_bll.customer_bll.credit_card_dal.credit_card_type_dal.image.ToString();

                        Credit_Card credit_card_obj = new Credit_Card();
                        credit_card_obj.credit_card_id = int.Parse(deal_bll.customer_bll.credit_card_dal.credit_card_id.ToString());
                        credit_card_obj.card_number = deal_bll.customer_bll.credit_card_dal.card_number.ToString();
                        credit_card_obj.credit_card_type_obj = credit_card_type_obj;

                        certificate_payment_obj = new Certificate_Payment();
                        certificate_payment_obj.payment_amount = decimal.Parse(deal_bll.certificate_payment_dal.payment_amount.ToString());
                        certificate_payment_obj.promotion_activity_obj = promotion_activity_obj;
                        certificate_payment_obj.credit_card_obj = credit_card_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = certificate_payment_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Buy_Certificate(Certificate_Payment certificate_payment_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_payment_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = certificate_payment_obj.certificate_obj.customer_obj.customer_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = certificate_payment_obj.certificate_obj.deal_obj.deal_id;

                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.deal_dal = deal_dal;

                    DAL.Promotion_Activity promotion_activity_dal = new DAL.Promotion_Activity();
                    promotion_activity_dal.promotion_activity_id = certificate_payment_obj.promotion_activity_obj.promotion_activity_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = certificate_payment_obj.credit_card_obj.credit_card_id;
                    credit_card_dal.customer_dal = customer_dal;

                    DAL.Certificate_Payment certificate_payment_dal = new DAL.Certificate_Payment();
                    certificate_payment_dal.payment_amount = certificate_payment_obj.payment_amount;
                    certificate_payment_dal.promotion_activity_dal = promotion_activity_dal;
                    certificate_payment_dal.credit_card_dal = credit_card_dal;
                    certificate_payment_dal.certificate_dal = certificate_dal;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_payment_dal);
                    deal_bll.Buy_Certificate();

                    if (deal_bll.successful)
                    {
                        Certificate certificate_obj = new Certificate();
                        certificate_obj.certificate_id = int.Parse(deal_bll.certificate_dal.certificate_id.ToString());

                        certificate_payment_obj = new Certificate_Payment();
                        certificate_payment_obj.payment_id = int.Parse(deal_bll.certificate_payment_dal.payment_id.ToString());
                        certificate_payment_obj.certificate_obj = certificate_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        //successful_response.message = "Certificate has been assigned to you\r\nYour Certificate # is " + deal_bll.certificate_dal.certificate_id.ToString();
                        successful_response.data_obj = certificate_payment_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Customer_Certificates(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Certificate cerificate_dal = null;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Deal deal_bll = new BLL.Deal(cerificate_dal, customer_dal);
                    deal_bll.Get_Customer_Certificates();

                    if (deal_bll.successful)
                    {
                        List<Certificate> certificate_obj_array = new List<Certificate>();

                        foreach (DAL.Certificate certificate_row in deal_bll.certificate_dal_array)
                        {
                            State state_obj = new State();
                            state_obj.abbreviation = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                            City city_obj = new City();
                            city_obj.city = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                            city_obj.state_obj = state_obj;

                            Time_Zone time_zone_obj_merchant = new Time_Zone();
                            time_zone_obj_merchant.abbreviation = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();
                            
                            Zip_Code zip_code_obj = new Zip_Code();
                            zip_code_obj.zip_code = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                            zip_code_obj.city_obj = city_obj;
                            zip_code_obj.time_zone_obj = time_zone_obj_merchant;

                            Merchant merchant_obj = new Merchant();
                            merchant_obj.company_name = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                            merchant_obj.image = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.image.ToString();
                            merchant_obj.zip_code_obj = zip_code_obj;

                            Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                            merchant_contact_obj.merchant_obj = merchant_obj;

                            Time_Zone time_zone_obj_deal = new Time_Zone();
                            time_zone_obj_deal.abbreviation = certificate_row.deal_dal.time_zone_dal.abbreviation.ToString();
                            
                            Deal deal_obj = new Deal();
                            deal_obj.deal_id = int.Parse(certificate_row.deal_dal.deal_id.ToString());
                            deal_obj.deal = certificate_row.deal_dal.deal.ToString();
                            deal_obj.fine_print = certificate_row.deal_dal.fine_print.ToString();
                            deal_obj.fine_print_ext = certificate_row.deal_dal.fine_print_ext.ToString();
                            deal_obj.max_dollar_amount = decimal.Parse(certificate_row.deal_dal.max_dollar_amount.ToString());
                            deal_obj.certificate_quantity = int.Parse(certificate_row.deal_dal.certificate_quantity.ToString());
                            deal_obj.expiration_date = DateTime.Parse(certificate_row.deal_dal.expiration_date.ToString());
                            deal_obj.image = certificate_row.deal_dal.image.ToString();
                            deal_obj.merchant_contact_obj = merchant_contact_obj;
                            deal_obj.time_zone_obj = time_zone_obj_deal;
                            
                            Certificate_Status certificate_status_obj = new Certificate_Status();
                            certificate_status_obj.status = certificate_row.certificate_status_dal.status.ToString();

                            Certificate certificate_obj = new Certificate();
                            certificate_obj.certificate_id = int.Parse(certificate_row.certificate_id.ToString());
                            certificate_obj.certificate_code = certificate_row.certificate_code.ToString();
                            certificate_obj.status_text_1 = certificate_row.status_text_1.ToString();
                            certificate_obj.status_text_2 = certificate_row.status_text_2.ToString();
                            if (!certificate_row.assigned_date.IsNull)
                                certificate_obj.assigned_date = DateTime.Parse(certificate_row.assigned_date.ToString());
                            if (!certificate_row.start_date.IsNull)
                                certificate_obj.start_date = DateTime.Parse(certificate_row.start_date.ToString());
                            if (!certificate_row.expiration_date.IsNull)
                                certificate_obj.expiration_date = DateTime.Parse(certificate_row.expiration_date.ToString());
                            if (!certificate_row.redeemed_date.IsNull)
                                certificate_obj.redeemed_date = DateTime.Parse(certificate_row.redeemed_date.ToString());
                            if (!certificate_row.entry_date_utc_stamp.IsNull)
                                certificate_obj.entry_date_utc_stamp = DateTime.Parse(certificate_row.entry_date_utc_stamp.ToString());
                            certificate_obj.deal_obj = deal_obj;
                            certificate_obj.certificate_status_obj = certificate_status_obj;

                            certificate_obj_array.Add(certificate_obj);
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = certificate_obj_array;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Customer_Active_Certificates(Customer customer_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(customer_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Certificate certificate_dal = null;

                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = customer_obj.customer_id;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, customer_dal);
                    deal_bll.Get_Customer_Active_Certificates();

                    if (deal_bll.successful)
                    {
                        List<Certificate> certificate_obj_array = new List<Certificate>();

                        foreach (DAL.Certificate certificate_row in deal_bll.certificate_dal_array)
                        {
                            State state_obj = new State();
                            state_obj.abbreviation = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                            City city_obj = new City();
                            city_obj.city = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                            city_obj.state_obj = state_obj;

                            Time_Zone time_zone_obj = new Time_Zone();
                            time_zone_obj.abbreviation = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();

                            Zip_Code zip_code_obj = new Zip_Code();
                            zip_code_obj.zip_code = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                            zip_code_obj.city_obj = city_obj;
                            zip_code_obj.time_zone_obj = time_zone_obj;

                            Merchant merchant_obj = new Merchant();
                            merchant_obj.company_name = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                            merchant_obj.image = certificate_row.deal_dal.merchant_contact_dal.merchant_dal.image.ToString();
                            merchant_obj.zip_code_obj = zip_code_obj;

                            Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                            merchant_contact_obj.merchant_obj = merchant_obj;

                            Deal deal_obj = new Deal();
                            deal_obj.deal_id = int.Parse(certificate_row.deal_dal.deal_id.ToString());
                            deal_obj.deal = certificate_row.deal_dal.deal.ToString();
                            deal_obj.fine_print = certificate_row.deal_dal.fine_print.ToString();
                            deal_obj.fine_print_ext = certificate_row.deal_dal.fine_print_ext.ToString();
                            deal_obj.max_dollar_amount = decimal.Parse(certificate_row.deal_dal.max_dollar_amount.ToString());
                            deal_obj.certificate_quantity = int.Parse(certificate_row.deal_dal.certificate_quantity.ToString());
                            deal_obj.expiration_date = DateTime.Parse(certificate_row.deal_dal.expiration_date.ToString());
                            deal_obj.image = certificate_row.deal_dal.image.ToString();
                            deal_obj.merchant_contact_obj = merchant_contact_obj;

                            Certificate_Status certificate_status_obj = new Certificate_Status();
                            certificate_status_obj.status = certificate_row.certificate_status_dal.status.ToString();

                            Certificate certificate_obj = new Certificate();
                            certificate_obj.certificate_id = int.Parse(certificate_row.certificate_id.ToString());
                            certificate_obj.certificate_code = certificate_row.certificate_code.ToString();
                            certificate_obj.status_text_1 = certificate_row.status_text_1.ToString();
                            certificate_obj.status_text_2 = certificate_row.status_text_2.ToString();
                            if (!certificate_row.assigned_date.IsNull)
                                certificate_obj.assigned_date = DateTime.Parse(certificate_row.assigned_date.ToString());
                            if (!certificate_row.start_date.IsNull)
                                certificate_obj.start_date = DateTime.Parse(certificate_row.start_date.ToString());
                            if (!certificate_row.expiration_date.IsNull)
                                certificate_obj.expiration_date = DateTime.Parse(certificate_row.expiration_date.ToString());
                            if (!certificate_row.redeemed_date.IsNull)
                                certificate_obj.redeemed_date = DateTime.Parse(certificate_row.redeemed_date.ToString());
                            if (!certificate_row.entry_date_utc_stamp.IsNull)
                                certificate_obj.entry_date_utc_stamp = DateTime.Parse(certificate_row.entry_date_utc_stamp.ToString());
                            certificate_obj.deal_obj = deal_obj;
                            certificate_obj.certificate_status_obj = certificate_status_obj;

                            certificate_obj_array.Add(certificate_obj);
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = certificate_obj_array;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        public JSONResponse Get_Certificate_Detail(Certificate certificate_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Customer customer_dal = new DAL.Customer();
                    customer_dal.customer_id = certificate_obj.customer_obj.customer_id;

                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_id = certificate_obj.certificate_id;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, customer_dal);

                    if (deal_bll.successful)
                    {
                        State state_obj = new State();
                        state_obj.abbreviation = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                        City city_obj = new City();
                        city_obj.city = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                        city_obj.state_obj = state_obj;

                        Time_Zone time_zone_obj = new Time_Zone();
                        time_zone_obj.abbreviation = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();

                        Zip_Code zip_code_obj = new Zip_Code();
                        zip_code_obj.zip_code = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                        zip_code_obj.city_obj = city_obj;
                        zip_code_obj.time_zone_obj = time_zone_obj;


                        Neighborhood neighborhood_obj = new Neighborhood();
                        if (!deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.IsNull)
                        {
                            neighborhood_obj.neighborhood_id = int.Parse(deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood_id.ToString());
                            neighborhood_obj.neighborhood = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.neighborhood_dal.neighborhood.ToString();
                        }

                        Merchant_Rating merchant_rating_obj = new Merchant_Rating();
                        if (!deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.IsNull)
                        {
                            merchant_rating_obj.rating_id = int.Parse(deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating_id.ToString());
                            merchant_rating_obj.rating = Decimal.Parse(deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.rating.ToString());
                            merchant_rating_obj.image = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.merchant_rating_dal.image.ToString();
                        }

                        Merchant merchant_obj = new Merchant();
                        merchant_obj.company_name = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString();
                        merchant_obj.address_1 = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.address_1.ToString();
                        merchant_obj.address_2 = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.address_2.ToString();
                        merchant_obj.description = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.description.ToString();
                        merchant_obj.phone = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.phone.ToString();
                        merchant_obj.website = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.website.ToString();
                        merchant_obj.image = deal_bll.certificate_dal.deal_dal.merchant_contact_dal.merchant_dal.image.ToString();
                        merchant_obj.zip_code_obj = zip_code_obj;
                        merchant_obj.neighborhood_obj = neighborhood_obj;
                        merchant_obj.merchant_rating_obj = merchant_rating_obj;

                        Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                        merchant_contact_obj.merchant_obj = merchant_obj;

                        Deal deal_obj = new Deal();
                        deal_obj.deal_id = int.Parse(deal_bll.certificate_dal.deal_dal.deal_id.ToString());
                        deal_obj.deal = deal_bll.certificate_dal.deal_dal.deal.ToString();
                        deal_obj.fine_print = deal_bll.certificate_dal.deal_dal.fine_print.ToString();
                        deal_obj.fine_print_ext = deal_bll.certificate_dal.deal_dal.fine_print_ext.ToString();
                        deal_obj.max_dollar_amount = decimal.Parse(deal_bll.certificate_dal.deal_dal.max_dollar_amount.ToString());
                        deal_obj.certificate_quantity = int.Parse(deal_bll.certificate_dal.deal_dal.certificate_quantity.ToString());
                        deal_obj.expiration_date = DateTime.Parse(deal_bll.certificate_dal.deal_dal.expiration_date.ToString());
                        deal_obj.instructions = deal_bll.certificate_dal.deal_dal.instructions.ToString();
                        deal_obj.image = deal_bll.certificate_dal.deal_dal.image.ToString();
                        deal_obj.merchant_contact_obj = merchant_contact_obj;

                        Contact contact_obj = new Contact();
                        contact_obj.first_name = deal_bll.certificate_dal.customer_dal.contact_dal.first_name.ToString();
                        contact_obj.last_name = deal_bll.certificate_dal.customer_dal.contact_dal.last_name.ToString();
                        contact_obj.email = deal_bll.certificate_dal.customer_dal.contact_dal.email.ToString();

                        Customer customer_obj = new Customer();
                        customer_obj.contact_obj = contact_obj;

                        Certificate_Status certificate_status_obj = new Certificate_Status();
                        certificate_status_obj.status = deal_bll.certificate_dal.certificate_status_dal.status.ToString();

                        certificate_obj = new Certificate();
                        certificate_obj.certificate_id = int.Parse(deal_bll.certificate_dal.certificate_id.ToString());
                        certificate_obj.certificate_code = deal_bll.certificate_dal.certificate_code.ToString();
                        certificate_obj.status_text_1 = deal_bll.certificate_dal.status_text_1.ToString();
                        certificate_obj.status_text_2 = deal_bll.certificate_dal.status_text_2.ToString();
                        if (!deal_bll.certificate_dal.assigned_date.IsNull)
                            certificate_obj.assigned_date = DateTime.Parse(deal_bll.certificate_dal.assigned_date.ToString());
                        if (!deal_bll.certificate_dal.start_date.IsNull)
                            certificate_obj.start_date = DateTime.Parse(deal_bll.certificate_dal.start_date.ToString());
                        if (!deal_bll.certificate_dal.expiration_date.IsNull)
                            certificate_obj.expiration_date = DateTime.Parse(deal_bll.certificate_dal.expiration_date.ToString());
                        if (!deal_bll.certificate_dal.redeemed_date.IsNull)
                            certificate_obj.redeemed_date = DateTime.Parse(deal_bll.certificate_dal.redeemed_date.ToString());
                        if (!deal_bll.certificate_dal.entry_date_utc_stamp.IsNull)
                            certificate_obj.entry_date_utc_stamp = DateTime.Parse(deal_bll.certificate_dal.entry_date_utc_stamp.ToString());
                        certificate_obj.deal_obj = deal_obj;
                        certificate_obj.certificate_status_obj = certificate_status_obj;
                        certificate_obj.customer_obj = customer_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = certificate_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(deal_bll.system_error_dal);

                        return error_response;
                    }
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(system_bll.system_error_dal);

                    return error_response;
                }
            }
            catch (Exception ex)
            {
                API.System_Error system_error_obj = new API.System_Error();
                system_error_obj.code = 400;
                system_error_obj.message = ex.Message.ToString();

                error_response.system_error_obj = system_error_obj;

                return error_response;
            }
        }

        #endregion


        #region properties

        public BLL.System system_bll { get; set; }
        public JSON_To_Data json_to_dal { get; set; }
        public DAL_To_JSON dal_to_json { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        JSONErrorResponse error_response { get; set; }
        JSONSuccessfulResponse successful_response { get; set; }

        #endregion
    }
}

