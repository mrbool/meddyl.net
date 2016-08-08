using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using GTSoft.Meddyl;

namespace GTSoft.Meddyl.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MerchantService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MerchantService.svc or MerchantService.svc.cs at the Solution Explorer and start debugging.

    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)] 
    
    public class MerchantService : IMerchantService
    {
        #region constructors

        public MerchantService()
        {
            system_bll = new BLL.System();
            json_to_dal = new JSON_To_Data();
            dal_to_json = new DAL_To_JSON();
            system_error_dal = new DAL.System_Error();
            system_successful = new DAL.System_Successful();
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
                    system_settings_obj.certificate_quantity_info = system_bll.system_settings_dal.certificate_quantity_info.ToString();
                    system_settings_obj.certificate_quantity_min = int.Parse(system_bll.system_settings_dal.certificate_quantity_min.ToString());
                    system_settings_obj.certificate_quantity_max = int.Parse(system_bll.system_settings_dal.certificate_quantity_max.ToString());
                    system_settings_obj.customer_app_android_id = system_bll.system_settings_dal.customer_app_android_id.ToString();
                    system_settings_obj.customer_app_ios_id = system_bll.system_settings_dal.customer_app_ios_id.ToString();
                    system_settings_obj.merchant_description_characters = int.Parse(system_bll.system_settings_dal.merchant_description_characters.ToString());
                    system_settings_obj.merchant_description_default = system_bll.system_settings_dal.merchant_description_default.ToString();
                    system_settings_obj.deal_new_customer_only = bool.Parse(system_bll.system_settings_dal.deal_new_customer_only.ToString());
                    system_settings_obj.deal_new_customer_only_info = system_bll.system_settings_dal.deal_new_customer_only_info.ToString();
                    system_settings_obj.deal_use_immediately = bool.Parse(system_bll.system_settings_dal.deal_use_immediately.ToString());
                    system_settings_obj.deal_use_immediately_info = system_bll.system_settings_dal.deal_use_immediately_info.ToString();
                    system_settings_obj.dollar_value_info = system_bll.system_settings_dal.dollar_value_info.ToString();
                    system_settings_obj.dollar_value_min = decimal.Parse(system_bll.system_settings_dal.dollar_value_min.ToString());
                    system_settings_obj.dollar_value_max = decimal.Parse(system_bll.system_settings_dal.dollar_value_max.ToString());
                    system_settings_obj.expiration_days_info = system_bll.system_settings_dal.expiration_days_info.ToString();
                    system_settings_obj.expiration_days_min = int.Parse(system_bll.system_settings_dal.expiration_days_min.ToString());
                    system_settings_obj.expiration_days_max = int.Parse(system_bll.system_settings_dal.expiration_days_max.ToString());
                    system_settings_obj.fine_print_more_characters = int.Parse(system_bll.system_settings_dal.fine_print_more_characters.ToString());
                    system_settings_obj.fine_print_more_default = system_bll.system_settings_dal.fine_print_more_default.ToString();
                    system_settings_obj.merchant_app_android_id = system_bll.system_settings_dal.merchant_app_android_id.ToString();
                    system_settings_obj.merchant_app_ios_id = system_bll.system_settings_dal.merchant_app_ios_id.ToString();
                    system_settings_obj.merchant_app_terms = system_bll.system_settings_dal.merchant_app_terms.ToString();
                    system_settings_obj.percent_off_default = int.Parse(system_bll.system_settings_dal.percent_off_default.ToString());
                    system_settings_obj.percent_off_max = int.Parse(system_bll.system_settings_dal.percent_off_max.ToString());
                    system_settings_obj.percent_off_min = int.Parse(system_bll.system_settings_dal.percent_off_min.ToString());

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

        public JSONResponse Get_Neighborhood_By_Zip(Zip_Code zip_code_obj)
        {
            try
            {
                DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                zip_code_dal.zip_code = zip_code_obj.zip_code;

                BLL.Location location_bll = new BLL.Location(zip_code_dal);
                location_bll.Get_Neighborhood_By_Zip();

                if (location_bll.successful)
                {
                    List<Neighborhood> neighborhood_obj_array = new List<Neighborhood>();
                    foreach (DAL.Neighborhood neighborhood_row in location_bll.neighborhood_dal_array)
                    {
                        Neighborhood neighborhood_obj = new Neighborhood();
                        neighborhood_obj.neighborhood_id = int.Parse(neighborhood_row.neighborhood_id.ToString());
                        neighborhood_obj.neighborhood = neighborhood_row.neighborhood.ToString();

                        neighborhood_obj_array.Add(neighborhood_obj);
                    }

                    State state_obj = new State();
                    state_obj.abbreviation = location_bll.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                    City city_obj = new City();
                    city_obj.city = location_bll.zip_code_dal.city_dal.city.ToString();
                    city_obj.state_obj = state_obj;

                    zip_code_obj.city_obj = city_obj;
                    zip_code_obj.neighborhood_obj_array = neighborhood_obj_array;

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(location_bll.system_successful_dal);
                    successful_response.data_obj = zip_code_obj;

                    return successful_response;
                }
                else
                {
                    error_response.system_error_obj = dal_to_json.Convert_System_Error(location_bll.system_error_dal);

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

        public JSONResponse Get_Fine_Print_Options(Login_Log login_log_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(login_log_obj), true);

                if (system_bll.successful)
                {
                    BLL.Deal deal_bll = new BLL.Deal();
                    deal_bll.Get_Fine_Print_Options();

                    if (deal_bll.successful)
                    {
                        List<Fine_Print_Option> fine_print_option_obj_array = new List<Fine_Print_Option>();

                        foreach (DAL.Fine_Print_Option fine_print_option_row in deal_bll.fine_print_option_dal_array)
                        {
                            Fine_Print_Option fine_print_option_obj = new Fine_Print_Option();
                            fine_print_option_obj.option_id = int.Parse(fine_print_option_row.option_id.ToString());
                            fine_print_option_obj.display = fine_print_option_row.display.ToString();
                            fine_print_option_obj.value = fine_print_option_row.value.ToString();
                            fine_print_option_obj.is_selected = bool.Parse(fine_print_option_row.is_selected.ToString());
                            fine_print_option_obj_array.Add(fine_print_option_obj);
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = fine_print_option_obj_array;

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


        #region merchant

        public JSONResponse Create_Validation(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact_Validation merchant_contact_validation_dal = new DAL.Merchant_Contact_Validation();
                    merchant_contact_validation_dal.ip_address = merchant_contact_obj.login_log_obj.ip_address;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.contact_id = merchant_contact_obj.contact_obj.contact_id;
                    contact_dal.first_name = merchant_contact_obj.contact_obj.first_name.Trim();
                    contact_dal.last_name = merchant_contact_obj.contact_obj.last_name.Trim();
                    contact_dal.user_name = merchant_contact_obj.contact_obj.email.Trim();
                    contact_dal.email = merchant_contact_obj.contact_obj.email.Trim();
                    contact_dal.phone = merchant_contact_obj.contact_obj.phone.Trim();

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.contact_dal = contact_dal;
                    merchant_contact_dal.merchant_contact_validation_dal = merchant_contact_validation_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.merchant_contact_dal = merchant_contact_dal;
                    merchant_bll.Create_Validation();

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Validate(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact_Validation merchant_contact_validation_dal = new DAL.Merchant_Contact_Validation();
                    merchant_contact_validation_dal.validation_code = merchant_contact_obj.merchant_contact_validation_obj.validation_code;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = merchant_contact_obj.contact_obj.email.Trim();
                    contact_dal.phone = merchant_contact_obj.contact_obj.phone.Trim();

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.contact_dal = contact_dal;
                    merchant_contact_dal.merchant_contact_validation_dal = merchant_contact_validation_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.merchant_contact_dal = merchant_contact_dal;
                    merchant_bll.Validate();

                    if (merchant_bll.successful)
                    {
                        Merchant_Contact_Validation merchant_contact_validation_obj = new Merchant_Contact_Validation();
                        merchant_contact_validation_obj.validation_id = int.Parse(merchant_bll.merchant_contact_dal.merchant_contact_validation_dal.validation_id.ToString());

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = merchant_contact_validation_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Register(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = merchant_contact_obj.merchant_obj.zip_code_obj.zip_code.Trim();

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.contact_id = merchant_contact_obj.contact_obj.contact_id;
                    contact_dal.first_name = merchant_contact_obj.contact_obj.first_name.Trim();
                    contact_dal.last_name = merchant_contact_obj.contact_obj.last_name.Trim();
                    contact_dal.user_name = merchant_contact_obj.contact_obj.email.Trim();
                    contact_dal.email = merchant_contact_obj.contact_obj.email.Trim();
                    contact_dal.phone = merchant_contact_obj.contact_obj.phone.Trim();
                    contact_dal.password = merchant_contact_obj.contact_obj.password.Trim();
                    contact_dal.zip_code_dal = zip_code_dal;

                    DAL.Neighborhood neighborhood_dal = new DAL.Neighborhood();
                    neighborhood_dal.neighborhood_id = merchant_contact_obj.merchant_obj.neighborhood_obj.neighborhood_id;

                    DAL.Industry industry_dal = new DAL.Industry();
                    industry_dal.industry_id = merchant_contact_obj.merchant_obj.industry_obj.industry_id;

                    DAL.Merchant merchant_dal = new DAL.Merchant();
                    merchant_dal.company_name = merchant_contact_obj.merchant_obj.company_name.Trim();
                    merchant_dal.address_1 = merchant_contact_obj.merchant_obj.address_1.Trim();
                    merchant_dal.address_2 = merchant_contact_obj.merchant_obj.address_2.Trim();
                    merchant_dal.phone = merchant_contact_obj.merchant_obj.phone.Trim();
                    merchant_dal.website = merchant_contact_obj.merchant_obj.website.Trim();
                    merchant_dal.description = merchant_contact_obj.merchant_obj.description.Trim();
                    merchant_dal.image = merchant_contact_obj.merchant_obj.image.Trim();
                    merchant_dal.image_base64 = merchant_contact_obj.merchant_obj.image_base64;
                    merchant_dal.zip_code_dal = zip_code_dal;
                    merchant_dal.neighborhood_dal = neighborhood_dal;
                    merchant_dal.industry_dal = industry_dal;

                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = merchant_contact_obj.login_log_obj.application_type_obj.application_type_id;

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.ip_address = merchant_contact_obj.login_log_obj.ip_address;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Merchant_Contact_Validation merchant_contact_validation_dal = new DAL.Merchant_Contact_Validation();
                    merchant_contact_validation_dal.validation_id = merchant_contact_obj.merchant_contact_validation_obj.validation_id;

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.title = merchant_contact_obj.title;
                    merchant_contact_dal.contact_dal = contact_dal;
                    merchant_contact_dal.merchant_dal = merchant_dal;
                    merchant_contact_dal.login_log_dal = login_log_dal;
                    merchant_contact_dal.merchant_contact_validation_dal = merchant_contact_validation_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.merchant_contact_dal = merchant_contact_dal;
                    merchant_bll.Register();

                    if (merchant_bll.successful)
                    {
                        merchant_contact_obj = new Merchant_Contact();
                        merchant_contact_obj.merchant_contact_id = int.Parse(merchant_bll.merchant_contact_dal.merchant_contact_id.ToString());

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = merchant_contact_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Login(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Application_Type application_type_dal = new DAL.Application_Type();
                    application_type_dal.application_type_id = merchant_contact_obj.login_log_obj.application_type_obj.application_type_id;

                    DAL.Login_Log login_log_dal = new DAL.Login_Log();
                    login_log_dal.auto_login = merchant_contact_obj.login_log_obj.auto_login;
                    login_log_dal.ip_address = merchant_contact_obj.login_log_obj.ip_address;
                    login_log_dal.application_type_dal = application_type_dal;

                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = merchant_contact_obj.contact_obj.user_name.Trim();
                    contact_dal.password = merchant_contact_obj.contact_obj.password.Trim();

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.contact_dal = contact_dal;
                    merchant_contact_dal.login_log_dal = login_log_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Login();

                    if (merchant_bll.successful)
                    {
                        Contact contact_obj = new Contact();
                        contact_obj.contact_id = int.Parse(merchant_bll.contact_dal.contact_id.ToString());
                        contact_obj.first_name = merchant_bll.contact_dal.first_name.ToString();
                        contact_obj.last_name = merchant_bll.contact_dal.last_name.ToString();
                        contact_obj.email = merchant_bll.contact_dal.email.ToString();
                        contact_obj.phone = merchant_bll.contact_dal.phone.ToString();

                        Merchant_Contact_Status merchant_contact_status_obj = new Merchant_Contact_Status();
                        merchant_contact_status_obj.status = merchant_bll.merchant_contact_dal.merchant_contact_status_dal.status.ToString();

                        merchant_contact_obj = new Merchant_Contact();
                        merchant_contact_obj.title = merchant_bll.merchant_contact_dal.title.ToString();
                        if (merchant_bll.merchant_contact_dal.merchant_contact_id.IsNull)
                        {
                            merchant_contact_obj.merchant_contact_id = 0;
                        }
                        else
                        {
                            merchant_contact_obj.merchant_contact_id = int.Parse(merchant_bll.merchant_contact_dal.merchant_contact_id.ToString());
                        }
                        merchant_contact_obj.contact_obj = contact_obj;
                        merchant_contact_obj.merchant_contact_status_obj = merchant_contact_status_obj;

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = merchant_contact_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Forgot_Password(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), false);

                if (system_bll.successful)
                {
                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.user_name = merchant_contact_obj.contact_obj.user_name.Trim();

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.contact_dal = contact_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Forgot_Password();

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Get_Merchant_Contact(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    // all you need to do is instatiate the object 
                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);

                    State contact_state_obj = new State();
                    contact_state_obj.abbreviation = merchant_bll.contact_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                    City contact_city_obj = new City();
                    contact_city_obj.city = merchant_bll.contact_dal.zip_code_dal.city_dal.city.ToString();
                    contact_city_obj.state_obj = contact_state_obj;

                    Zip_Code contact_zip_code_obj = new Zip_Code();
                    contact_zip_code_obj.zip_code = merchant_bll.contact_dal.zip_code_dal.zip_code.ToString();
                    contact_zip_code_obj.city_obj = contact_city_obj;

                    Contact contact_obj = new Contact();
                    contact_obj.contact_id = int.Parse(merchant_bll.contact_dal.contact_id.ToString());
                    contact_obj.first_name = merchant_bll.contact_dal.first_name.ToString();
                    contact_obj.last_name = merchant_bll.contact_dal.last_name.ToString();
                    contact_obj.phone = merchant_bll.contact_dal.phone.ToString();
                    contact_obj.email = merchant_bll.contact_dal.email.ToString();
                    contact_obj.password = merchant_bll.contact_dal.password.ToString();
                    contact_obj.zip_code_obj = contact_zip_code_obj;

                    State merchant_state_obj = new State();
                    merchant_state_obj.abbreviation = merchant_bll.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                    City merchant_city_obj = new City();
                    merchant_city_obj.city = merchant_bll.merchant_dal.zip_code_dal.city_dal.city.ToString();
                    merchant_city_obj.state_obj = merchant_state_obj;

                    Time_Zone merchant_time_zone_obj = new Time_Zone();
                    merchant_time_zone_obj.abbreviation = merchant_bll.merchant_dal.zip_code_dal.time_zone_dal.time_zone.ToString();

                    Zip_Code merchant_zip_code_obj = new Zip_Code();
                    merchant_zip_code_obj.zip_code = merchant_bll.merchant_dal.zip_code_dal.zip_code.ToString();
                    merchant_zip_code_obj.time_zone_obj = merchant_time_zone_obj;
                    merchant_zip_code_obj.city_obj = merchant_city_obj;

                    Merchant_Status merchant_status_obj = new Merchant_Status();
                    merchant_status_obj.status = merchant_bll.merchant_dal.merchant_status_dal.status.ToString();

                    Industry industry_obj = new Industry();
                    industry_obj.industry_id = int.Parse(merchant_bll.merchant_dal.industry_dal.industry_id.ToString());
                    industry_obj.industry = merchant_bll.merchant_dal.industry_dal.industry.ToString();

                    Neighborhood neighborhood_obj = new Neighborhood();
                    if (merchant_bll.merchant_dal.neighborhood_dal != null && !merchant_bll.merchant_dal.neighborhood_dal.neighborhood.IsNull)
                    {
                        neighborhood_obj.neighborhood_id = int.Parse(merchant_bll.merchant_dal.neighborhood_dal.neighborhood_id.ToString());
                        neighborhood_obj.neighborhood = merchant_bll.merchant_dal.neighborhood_dal.neighborhood.ToString();
                    }

                    Merchant merchant_obj = new Merchant();
                    merchant_obj.merchant_id = int.Parse(merchant_bll.merchant_dal.merchant_id.ToString());
                    merchant_obj.company_name = merchant_bll.merchant_dal.company_name.ToString();
                    merchant_obj.address_1 = merchant_bll.merchant_dal.address_1.ToString();
                    merchant_obj.address_2 = merchant_bll.merchant_dal.address_2.ToString();
                    merchant_obj.phone = merchant_bll.merchant_dal.phone.ToString();
                    merchant_obj.website = merchant_bll.merchant_dal.website.ToString();
                    merchant_obj.description = merchant_bll.merchant_dal.description.ToString();
                    merchant_obj.image = merchant_bll.merchant_dal.image.ToString();
                    merchant_obj.merchant_status_obj = merchant_status_obj;
                    merchant_obj.zip_code_obj = merchant_zip_code_obj;
                    merchant_obj.industry_obj = industry_obj;
                    merchant_obj.neighborhood_obj = neighborhood_obj;

                    Merchant_Contact_Validation merchant_contact_validation_obj = new Merchant_Contact_Validation();
                    merchant_contact_validation_obj.validation_code = merchant_bll.merchant_contact_dal.merchant_contact_validation_dal.validation_code.ToString();

                    merchant_contact_obj = new Merchant_Contact();
                    merchant_contact_obj.merchant_contact_id = int.Parse(merchant_bll.merchant_contact_dal.merchant_contact_id.ToString());
                    merchant_contact_obj.title = merchant_bll.merchant_contact_dal.title.ToString();
                    merchant_contact_obj.contact_obj = contact_obj;
                    merchant_contact_obj.merchant_obj = merchant_obj;
                    merchant_contact_obj.merchant_contact_validation_obj = merchant_contact_validation_obj;

                    successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                    successful_response.data_obj = merchant_contact_obj;

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

        public JSONResponse Update_Merchant_Contact(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Contact contact_dal = new DAL.Contact();
                    contact_dal.first_name = merchant_contact_obj.contact_obj.first_name;
                    contact_dal.last_name = merchant_contact_obj.contact_obj.last_name;
                    contact_dal.user_name = merchant_contact_obj.contact_obj.email;
                    contact_dal.email = merchant_contact_obj.contact_obj.email;

                    DAL.Merchant_Contact merchant_contact_dal_update = new DAL.Merchant_Contact();
                    merchant_contact_dal_update.merchant_contact_id = merchant_contact_obj.merchant_contact_id;
                    merchant_contact_dal_update.title = merchant_contact_obj.title;
                    merchant_contact_dal_update.contact_dal = contact_dal;

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Update_Merchant_Contact(merchant_contact_dal_update);

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Update_Merchant(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Zip_Code zip_code_dal = new DAL.Zip_Code();
                    zip_code_dal.zip_code = merchant_contact_obj.merchant_obj.zip_code_obj.zip_code;

                    DAL.Industry industry_dal = new DAL.Industry();
                    industry_dal.industry_id = merchant_contact_obj.merchant_obj.industry_obj.industry_id;

                    DAL.Neighborhood neighborhood_dal = new DAL.Neighborhood();
                    neighborhood_dal.neighborhood_id = merchant_contact_obj.merchant_obj.neighborhood_obj.neighborhood_id;

                    DAL.Merchant merchant_dal = new DAL.Merchant();
                    merchant_dal.company_name = merchant_contact_obj.merchant_obj.company_name;
                    merchant_dal.description = merchant_contact_obj.merchant_obj.description;
                    merchant_dal.address_1 = merchant_contact_obj.merchant_obj.address_1;
                    merchant_dal.address_2 = merchant_contact_obj.merchant_obj.address_2;
                    merchant_dal.phone = merchant_contact_obj.merchant_obj.phone;
                    merchant_dal.website = merchant_contact_obj.merchant_obj.website;
                    merchant_dal.image_base64 = merchant_contact_obj.merchant_obj.image_base64;
                    merchant_dal.zip_code_dal = zip_code_dal;
                    merchant_dal.industry_dal = industry_dal;
                    merchant_dal.neighborhood_dal = neighborhood_dal;

                    DAL.Merchant_Contact merchant_contact_dal_update = new DAL.Merchant_Contact();
                    merchant_contact_dal_update.merchant_contact_id = merchant_contact_obj.merchant_contact_id;
                    merchant_contact_dal_update.merchant_dal = merchant_dal;

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Update_Merchant(merchant_contact_dal_update);

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = credit_card_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.card_holder_name = credit_card_obj.card_holder_name.Trim();
                    credit_card_dal.card_number = credit_card_obj.card_number.Trim();
                    credit_card_dal.expiration_date = credit_card_obj.expiration_date.Trim();
                    credit_card_dal.billing_zip_code = credit_card_obj.billing_zip_code.Trim();
                    credit_card_dal.security_code = credit_card_obj.security_code.Trim();
                    credit_card_dal.merchant_contact_dal = merchant_contact_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(credit_card_dal);
                    merchant_bll.Add_Credit_Card();

                    if (merchant_bll.successful)
                    {
                        credit_card_obj = new Credit_Card();
                        credit_card_obj.credit_card_id = int.Parse(merchant_bll.credit_card_dal.credit_card_id.ToString());
                        credit_card_obj.card_number = merchant_bll.credit_card_dal.card_number.ToString();
                        credit_card_obj.expiration_date = merchant_bll.credit_card_dal.expiration_date.ToString();

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = credit_card_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = credit_card_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = credit_card_obj.credit_card_id;
                    credit_card_dal.merchant_contact_dal = merchant_contact_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(credit_card_dal);
                    merchant_bll.Delete_Credit_Card();

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Get_Credit_Cards(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Get_Credit_Cards();

                    if (merchant_bll.successful)
                    {
                        List<Credit_Card> credit_card_obj_array = new List<Credit_Card>();

                        foreach (DAL.Credit_Card credit_card_dal in merchant_bll.credit_card_dal_array)
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

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = credit_card_obj_array;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Get_Default_Credit_Card(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    BLL.Merchant merchant_bll = new BLL.Merchant(merchant_contact_dal);
                    merchant_bll.Get_Default_Credit_Card();

                    if (merchant_bll.successful)
                    {
                        Credit_Card credit_card_obj = new Credit_Card();

                        if (!merchant_bll.credit_card_dal.credit_card_id.IsNull)
                        {
                            Credit_Card_Type credit_card_type_obj = new Credit_Card_Type();
                            credit_card_type_obj.type = merchant_bll.credit_card_dal.credit_card_type_dal.type.ToString();
                            credit_card_type_obj.image = merchant_bll.credit_card_dal.credit_card_type_dal.image.ToString();

                            credit_card_obj = new Credit_Card();
                            credit_card_obj.credit_card_id = int.Parse(merchant_bll.credit_card_dal.credit_card_id.ToString());
                            credit_card_obj.card_number = merchant_bll.credit_card_dal.card_number.ToString();
                            credit_card_obj.expiration_date = merchant_bll.credit_card_dal.expiration_date.ToString();
                            credit_card_obj.default_flag = bool.Parse(merchant_bll.credit_card_dal.default_flag.ToString());
                            credit_card_obj.credit_card_type_obj = credit_card_type_obj;
                        }

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = credit_card_obj;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = credit_card_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Credit_Card credit_card_dal = new DAL.Credit_Card();
                    credit_card_dal.credit_card_id = credit_card_obj.credit_card_id;
                    credit_card_dal.merchant_contact_dal = merchant_contact_dal;

                    BLL.Merchant merchant_bll = new BLL.Merchant(credit_card_dal);
                    merchant_bll.Set_Default_Credit_Card();

                    if (merchant_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(merchant_bll.system_successful_dal);
                        successful_response.data_obj = null;

                        return successful_response;
                    }
                    else
                    {
                        error_response.system_error_obj = dal_to_json.Convert_System_Error(merchant_bll.system_error_dal);

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

        public JSONResponse Verify_Deal(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.percent_off = deal_obj.percent_off;
                    deal_dal.max_dollar_amount = deal_obj.max_dollar_amount;
                    deal_dal.certificate_quantity = deal_obj.certificate_quantity;
                    deal_dal.expiration_date = deal_obj.expiration_date;
                    deal_dal.use_deal_immediately = deal_obj.use_deal_immediately;
                    deal_dal.is_valid_new_customer_only = deal_obj.is_valid_new_customer_only;
                    deal_dal.fine_print = deal_obj.fine_print.Trim();
                    if(deal_obj.fine_print_option_obj_array != null)
                        deal_dal.fine_print_option_dal_array = json_to_dal.Fine_Print_Option_Array(deal_obj.fine_print_option_obj_array);

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, false);
                    deal_bll.Verify_Deal();

                    if (deal_bll.successful)
                    {
                        deal_obj = new Deal();
                        deal_obj.deal = deal_bll.deal_dal.deal.ToString();
                        deal_obj.fine_print_ext = deal_bll.deal_dal.fine_print_ext.ToString();

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = deal_obj;

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

        public JSONResponse Add_Deal(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Promotion promotion_dal = new DAL.Promotion();
                    promotion_dal.promotion_code = deal_obj.promotion_activity_obj.promotion_obj.promotion_code;

                    DAL.Promotion_Activity promotion_activity_dal = new DAL.Promotion_Activity();
                    promotion_activity_dal.promotion_dal = promotion_dal;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_obj.deal_id;
                    deal_dal.deal = deal_obj.deal.Trim();
                    deal_dal.fine_print = deal_obj.fine_print.Trim();
                    deal_dal.percent_off = deal_obj.percent_off;
                    deal_dal.max_dollar_amount = deal_obj.max_dollar_amount;
                    deal_dal.certificate_quantity = deal_obj.certificate_quantity;
                    deal_dal.expiration_date = deal_obj.expiration_date;
                    deal_dal.image = deal_obj.image.Trim();
                    deal_dal.image_base64 = deal_obj.image_base64;
                    deal_dal.use_deal_immediately = deal_obj.use_deal_immediately;
                    deal_dal.is_valid_new_customer_only = deal_obj.is_valid_new_customer_only;
                    deal_dal.promotion_activity_dal = promotion_activity_dal;
                    if (deal_obj.fine_print_option_obj_array != null)
                        deal_dal.fine_print_option_dal_array = json_to_dal.Fine_Print_Option_Array(deal_obj.fine_print_option_obj_array);

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, false);
                    deal_bll.Add();

                    if (deal_bll.successful)
                    {
                        deal_obj = new Deal();
                        deal_obj.deal_id = int.Parse(deal_bll.deal_dal.deal_id.ToString());

                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = deal_obj;

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

        public JSONResponse Send_Deal_Validation(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_obj.deal_id;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);
                    deal_bll.Send_Validation_Code();

                    if (deal_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = null;

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

        public JSONResponse Validate_Deal(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Deal_Validation deal_validation_obj = new DAL.Deal_Validation();
                    deal_validation_obj.validation_code = deal_obj.deal_validation_obj.validation_code;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_obj.deal_id;
                    deal_dal.deal_validation_dal = deal_validation_obj;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);
                    deal_bll.Validate();

                    if (deal_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = null;

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

        public JSONResponse Get_Deals(Merchant_Contact merchant_contact_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(merchant_contact_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Deal deal_dal = null;

                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = merchant_contact_obj.merchant_contact_id;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);
                    deal_bll.Get_Merchant_Deals();

                    if (deal_bll.successful)
                    {
                        List<Deal> deal_obj_array = new List<Deal>();

                        foreach(DAL.Deal deal_row in deal_bll.deal_dal_array)
                        {
                            List<Fine_Print_Option> fine_print_option_obj_array = new List<Fine_Print_Option>();
                            foreach (DAL.Fine_Print_Option fine_print_option_dal in deal_row.fine_print_option_dal_array)
                            {
                                Fine_Print_Option fine_print_option_obj = new Fine_Print_Option();
                                fine_print_option_obj.option_id = int.Parse(fine_print_option_dal.option_id.ToString());
                                fine_print_option_obj.display = fine_print_option_dal.display.ToString();
                                fine_print_option_obj.value = fine_print_option_dal.value.ToString();
                                fine_print_option_obj.is_selected = bool.Parse(fine_print_option_dal.is_selected.ToString());
                                fine_print_option_obj_array.Add(fine_print_option_obj);
                            }

                            State state_obj = new State();
                            state_obj.abbreviation = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                            City city_obj = new City();
                            city_obj.city = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.city_dal.city.ToString();
                            city_obj.state_obj = state_obj;

                            Time_Zone time_zone_obj_merchant = new Time_Zone();
                            time_zone_obj_merchant.time_zone_id = int.Parse(deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.time_zone_id.ToString());
                            time_zone_obj_merchant.abbreviation = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();
                            time_zone_obj_merchant.time_zone = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.time_zone.ToString();
                            time_zone_obj_merchant.offset = int.Parse(deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.time_zone_dal.offset.ToString());

                            Zip_Code zip_code_obj = new Zip_Code();
                            zip_code_obj.zip_code = deal_row.merchant_contact_dal.merchant_dal.zip_code_dal.zip_code.ToString();
                            zip_code_obj.city_obj = city_obj;
                            zip_code_obj.time_zone_obj = time_zone_obj_merchant;

                            Merchant merchant_obj = new Merchant();
                            merchant_obj.company_name = deal_row.merchant_contact_dal.merchant_dal.company_name.ToString();
                            merchant_obj.description = deal_row.merchant_contact_dal.merchant_dal.description.ToString();
                            merchant_obj.address_1 = deal_row.merchant_contact_dal.merchant_dal.address_1.ToString();
                            merchant_obj.address_2 = deal_row.merchant_contact_dal.merchant_dal.address_2.ToString();
                            merchant_obj.latitude = double.Parse(deal_row.merchant_contact_dal.merchant_dal.latitude.ToString());
                            merchant_obj.longitude = double.Parse(deal_row.merchant_contact_dal.merchant_dal.longitude.ToString());
                            merchant_obj.phone = deal_row.merchant_contact_dal.merchant_dal.phone.ToString();
                            merchant_obj.website = deal_bll.deal_dal.merchant_contact_dal.merchant_dal.website.ToString();
                            merchant_obj.zip_code_obj = zip_code_obj;

                            merchant_contact_obj = new Merchant_Contact();
                            merchant_contact_obj.merchant_obj = merchant_obj;

                            Deal_Status deal_status_obj = new Deal_Status();
                            deal_status_obj.status_id = int.Parse(deal_row.deal_status_dal.status_id.ToString());
                            deal_status_obj.status = deal_row.deal_status_dal.status.ToString();

                            Time_Zone time_zone_obj = new Time_Zone();
                            time_zone_obj.time_zone_id = int.Parse(deal_row.time_zone_dal.time_zone_id.ToString());
                            time_zone_obj.abbreviation = deal_row.time_zone_dal.abbreviation.ToString();
                            time_zone_obj.time_zone = deal_row.time_zone_dal.time_zone.ToString();
                            time_zone_obj.offset = int.Parse(deal_row.time_zone_dal.offset.ToString());

                            Deal deal_obj = new Deal();
                            deal_obj.deal_id = int.Parse(deal_row.deal_id.ToString());
                            deal_obj.deal = deal_row.deal.ToString();
                            deal_obj.fine_print = deal_row.fine_print.ToString();
                            deal_obj.fine_print_ext = deal_row.fine_print_ext.ToString();
                            deal_obj.percent_off = int.Parse(deal_row.percent_off.ToString());
                            deal_obj.max_dollar_amount = decimal.Parse(deal_row.max_dollar_amount.ToString());
                            deal_obj.certificate_quantity = int.Parse(deal_row.certificate_quantity.ToString());
                            deal_obj.expiration_date = DateTime.Parse(deal_row.expiration_date.ToString());
                            deal_obj.entry_date_utc_stamp = DateTime.Parse(deal_row.entry_date_utc_stamp.ToString());
                            deal_obj.image = deal_row.image.ToString();
                            deal_obj.certificate_amount = decimal.Parse(deal_row.certificate_amount.ToString());
                            deal_obj.certificates_remaining = int.Parse(deal_row.certificates_remaining.ToString());
                            deal_obj.certificates_redeemed = int.Parse(deal_row.certificates_redeemed.ToString());
                            deal_obj.certificates_sold = int.Parse(deal_row.certificates_sold.ToString());
                            deal_obj.certificates_unused = int.Parse(deal_row.certificates_unused.ToString());
                            deal_obj.certificates_expired = int.Parse(deal_row.certificates_expired.ToString());
                            deal_obj.use_deal_immediately = bool.Parse(deal_row.use_deal_immediately.ToString());
                            deal_obj.is_valid_new_customer_only = bool.Parse(deal_row.is_valid_new_customer_only.ToString());
                            deal_obj.last_assigned_date = DateTime.Parse(deal_row.last_assigned_date.ToString());
                            deal_obj.last_redeemed_date = DateTime.Parse(deal_row.last_redeemed_date.ToString());
                            deal_obj.deal_status_obj = deal_status_obj;
                            deal_obj.time_zone_obj = time_zone_obj;
                            deal_obj.merchant_contact_obj = merchant_contact_obj;
                            deal_obj.fine_print_option_obj_array = fine_print_option_obj_array;

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

        public JSONResponse Get_Deal_Details(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_obj.deal_id;
                    //deal_dal.merchant_contact_dal = merchant_contact_dal;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);

                    List<Fine_Print_Option> fine_print_option_obj_array = new List<Fine_Print_Option>();
                    foreach(DAL.Fine_Print_Option fine_print_option_dal in deal_dal.fine_print_option_dal_array)
                    {
                        Fine_Print_Option fine_print_option_obj = new Fine_Print_Option();
                        fine_print_option_obj.option_id = int.Parse(fine_print_option_dal.option_id.ToString());
                        fine_print_option_obj.display = fine_print_option_dal.display.ToString();
                        fine_print_option_obj.value = fine_print_option_dal.value.ToString();
                        fine_print_option_obj.is_selected = bool.Parse(fine_print_option_dal.is_selected.ToString());
                        fine_print_option_obj_array.Add(fine_print_option_obj);
                    }
                    
                    State state_obj = new State();
                    state_obj.abbreviation = deal_bll.merchant_bll.merchant_dal.zip_code_dal.city_dal.state_dal.abbreviation.ToString();

                    City city_obj = new City();
                    city_obj.city = deal_bll.merchant_bll.merchant_dal.zip_code_dal.city_dal.city.ToString();
                    city_obj.state_obj = state_obj;

                    Time_Zone time_zone_obj_merchant = new Time_Zone();
                    time_zone_obj_merchant.time_zone_id = int.Parse(deal_bll.merchant_bll.merchant_dal.zip_code_dal.time_zone_dal.time_zone_id.ToString());
                    time_zone_obj_merchant.abbreviation = deal_bll.merchant_bll.merchant_dal.zip_code_dal.time_zone_dal.abbreviation.ToString();
                    time_zone_obj_merchant.time_zone = deal_bll.merchant_bll.merchant_dal.zip_code_dal.time_zone_dal.time_zone.ToString();
                    time_zone_obj_merchant.offset = int.Parse(deal_bll.merchant_bll.merchant_dal.zip_code_dal.time_zone_dal.offset.ToString());

                    Zip_Code zip_code_obj = new Zip_Code();
                    zip_code_obj.zip_code = deal_bll.merchant_bll.merchant_dal.zip_code_dal.zip_code.ToString();
                    zip_code_obj.city_obj = city_obj;
                    zip_code_obj.time_zone_obj = time_zone_obj_merchant;

                    Merchant merchant_obj = new Merchant();
                    merchant_obj.company_name = deal_bll.merchant_bll.merchant_dal.company_name.ToString();
                    merchant_obj.address_1 = deal_bll.merchant_bll.merchant_dal.address_1.ToString();
                    merchant_obj.address_2 = deal_bll.merchant_bll.merchant_dal.address_2.ToString();
                    merchant_obj.latitude = double.Parse(deal_bll.merchant_bll.merchant_dal.latitude.ToString());
                    merchant_obj.longitude = double.Parse(deal_bll.merchant_bll.merchant_dal.longitude.ToString());
                    merchant_obj.phone = deal_bll.merchant_bll.merchant_dal.phone.ToString();
                    merchant_obj.website = deal_bll.merchant_bll.merchant_dal.website.ToString();
                    merchant_obj.zip_code_obj = zip_code_obj;

                    Merchant_Contact merchant_contact_obj = new Merchant_Contact();
                    merchant_contact_obj.merchant_obj = merchant_obj;

                    Deal_Status deal_status_obj = new Deal_Status();
                    deal_status_obj.status_id = int.Parse(deal_bll.deal_dal.deal_status_dal.status_id.ToString());
                    deal_status_obj.status = deal_bll.deal_dal.deal_status_dal.status.ToString();

                    Time_Zone time_zone_obj = new Time_Zone();
                    time_zone_obj.time_zone_id = int.Parse(deal_bll.deal_dal.time_zone_dal.time_zone_id.ToString());
                    time_zone_obj.abbreviation = deal_bll.deal_dal.time_zone_dal.abbreviation.ToString();
                    time_zone_obj.time_zone = deal_bll.deal_dal.time_zone_dal.time_zone.ToString();
                    time_zone_obj.offset = int.Parse(deal_bll.deal_dal.time_zone_dal.offset.ToString());

                    deal_obj = new Deal();
                    deal_obj.deal_id = int.Parse(deal_bll.deal_dal.deal_id.ToString());
                    deal_obj.deal = deal_bll.deal_dal.deal.ToString();
                    deal_obj.fine_print = deal_bll.deal_dal.fine_print.ToString();
                    deal_obj.fine_print_ext = deal_bll.deal_dal.fine_print_ext.ToString();
                    deal_obj.percent_off = int.Parse(deal_bll.deal_dal.percent_off.ToString());
                    deal_obj.max_dollar_amount = decimal.Parse(deal_bll.deal_dal.max_dollar_amount.ToString());
                    deal_obj.certificate_quantity = int.Parse(deal_bll.deal_dal.certificate_quantity.ToString());
                    deal_obj.expiration_date = DateTime.Parse(deal_bll.deal_dal.expiration_date.ToString());
                    deal_obj.entry_date_utc_stamp = DateTime.Parse(deal_bll.deal_dal.entry_date_utc_stamp.ToString());
                    deal_obj.image = deal_bll.deal_dal.image.ToString();
                    deal_obj.certificate_amount = decimal.Parse(deal_bll.deal_dal.certificate_amount.ToString());
                    deal_obj.certificates_remaining = int.Parse(deal_bll.deal_dal.certificates_remaining.ToString());
                    deal_obj.certificates_redeemed = int.Parse(deal_bll.deal_dal.certificates_redeemed.ToString());
                    deal_obj.certificates_sold = int.Parse(deal_bll.deal_dal.certificates_sold.ToString());
                    deal_obj.certificates_unused = int.Parse(deal_bll.deal_dal.certificates_unused.ToString());
                    deal_obj.certificates_expired = int.Parse(deal_bll.deal_dal.certificates_expired.ToString());
                    deal_obj.use_deal_immediately = bool.Parse(deal_bll.deal_dal.use_deal_immediately.ToString());
                    deal_obj.is_valid_new_customer_only = bool.Parse(deal_bll.deal_dal.is_valid_new_customer_only.ToString());
                    deal_obj.last_assigned_date = DateTime.Parse(deal_bll.deal_dal.last_assigned_date.ToString());
                    deal_obj.last_redeemed_date = DateTime.Parse(deal_bll.deal_dal.last_redeemed_date.ToString());
                    deal_obj.deal_status_obj = deal_status_obj;
                    deal_obj.time_zone_obj = time_zone_obj;
                    deal_obj.merchant_contact_obj = merchant_contact_obj;
                    deal_obj.fine_print_option_obj_array = fine_print_option_obj_array;

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

        public JSONResponse Cancel_Deal(Deal deal_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(deal_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = deal_obj.merchant_contact_obj.merchant_contact_id;

                    DAL.Deal deal_dal = new DAL.Deal();
                    deal_dal.deal_id = deal_obj.deal_id;

                    BLL.Deal deal_bll = new BLL.Deal(deal_dal, merchant_contact_dal, true);
                    deal_bll.Cancel_Deal();

                    if (deal_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = null;

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

        public JSONResponse Lookup_Certificate(Certificate certificate_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = certificate_obj.deal_obj.merchant_contact_obj.merchant_contact_id;
                    
                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_code = certificate_obj.certificate_code;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, merchant_contact_dal);
                    deal_bll.Lookup_Certificate();

                    if (deal_bll.successful)
                    {
                        Contact contact_obj = new Contact();
                        contact_obj.contact_id = int.Parse(deal_bll.certificate_dal.customer_dal.contact_dal.contact_id.ToString());
                        contact_obj.first_name = deal_bll.certificate_dal.customer_dal.contact_dal.first_name.ToString();
                        contact_obj.last_name = deal_bll.certificate_dal.customer_dal.contact_dal.last_name.ToString();
                        contact_obj.email = deal_bll.certificate_dal.customer_dal.contact_dal.email.ToString();

                        Customer customer_obj = new Customer();
                        customer_obj.customer_id = int.Parse(deal_bll.certificate_dal.customer_dal.customer_id.ToString());
                        customer_obj.contact_obj = contact_obj;

                        Certificate_Status certificate_status_obj = new Certificate_Status();
                        certificate_status_obj.status_id = int.Parse(deal_bll.certificate_dal.certificate_status_dal.status_id.ToString());
                        certificate_status_obj.status = deal_bll.certificate_dal.certificate_status_dal.status.ToString();

                        Time_Zone time_zone_obj = new Time_Zone();
                        time_zone_obj.time_zone_id = int.Parse(deal_bll.certificate_dal.deal_dal.time_zone_dal.time_zone_id.ToString());
                        time_zone_obj.abbreviation = deal_bll.certificate_dal.deal_dal.time_zone_dal.abbreviation.ToString();

                        Deal deal_obj = new Deal();
                        deal_obj.deal = deal_bll.deal_dal.deal.ToString();
                        deal_obj.fine_print_ext = deal_bll.deal_dal.fine_print_ext.ToString();
                        deal_obj.time_zone_obj = time_zone_obj;

                        certificate_obj.status_text_1 = deal_bll.certificate_dal.status_text_1.ToString();
                        certificate_obj.status_text_2 = deal_bll.certificate_dal.status_text_2.ToString();
                        certificate_obj.certificate_id = int.Parse(deal_bll.certificate_dal.certificate_id.ToString());
                        certificate_obj.assigned_date = DateTime.Parse(deal_bll.certificate_dal.assigned_date.ToString());
                        certificate_obj.customer_obj = customer_obj;
                        certificate_obj.certificate_status_obj = certificate_status_obj;
                        certificate_obj.deal_obj = deal_obj;

                        Certificate_Payment certificate_payment_obj = new Certificate_Payment();
                        certificate_payment_obj.payment_id = int.Parse(deal_bll.certificate_payment_dal.payment_id.ToString());
                        certificate_payment_obj.card_number = deal_bll.certificate_payment_dal.card_number.ToString();
                        certificate_payment_obj.certificate_obj = certificate_obj;

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

        public JSONResponse Redeem_Certificate(Certificate certificate_obj)
        {
            try
            {
                system_bll.Check_Version_Maintenance_And_Login(json_to_dal.Convert_Login_Log(certificate_obj.login_log_obj), true);

                if (system_bll.successful)
                {
                    DAL.Merchant_Contact merchant_contact_dal = new DAL.Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = certificate_obj.deal_obj.merchant_contact_obj.merchant_contact_id;
                    
                    DAL.Certificate certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_id = certificate_obj.certificate_id;

                    BLL.Deal deal_bll = new BLL.Deal(certificate_dal, merchant_contact_dal);
                    deal_bll.Redeem_Certificate();

                    if (deal_bll.successful)
                    {
                        successful_response.system_successful_obj = dal_to_json.Convert_System_Successful(deal_bll.system_successful_dal);
                        successful_response.data_obj = null;

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
        public DAL.System_Successful system_successful { get; set; }

        JSONErrorResponse error_response { get; set; }
        JSONSuccessfulResponse successful_response { get; set; }

        #endregion

    }
}
