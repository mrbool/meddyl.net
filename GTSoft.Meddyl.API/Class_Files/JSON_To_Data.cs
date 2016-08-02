using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTSoft.Meddyl.DAL;

namespace GTSoft.Meddyl.API
{
    public class JSON_To_Data
    {
        
        #region constructor

        public JSON_To_Data()
        {
        }

        #endregion


        #region public methods

        public DAL.Application_Type Convert_Application_Type(API.Application_Type application_type_obj)
        {
            DAL.Application_Type application_type_dal = new DAL.Application_Type();
            if (application_type_obj.application_type_id != 0)
                application_type_dal.application_type_id = application_type_obj.application_type_id;

            if (application_type_obj.application_type != null)
                application_type_dal.application_type = application_type_obj.application_type;

            if (application_type_obj.version != null)
                application_type_dal.version = application_type_obj.version;

            application_type_dal.is_down = application_type_obj.is_down;

            if (application_type_obj.down_message != null)
                application_type_dal.down_message = application_type_obj.down_message;

            return application_type_dal;
        }

        public DAL.Fine_Print_Option Fine_Print_Option(API.Fine_Print_Option fine_print_option_obj)
        {
            DAL.Fine_Print_Option fine_print_option_data = new DAL.Fine_Print_Option();
            fine_print_option_data.option_id = fine_print_option_obj.option_id;

            if (fine_print_option_obj.display == null)
                fine_print_option_data.display = "";
            else
                fine_print_option_data.display = fine_print_option_obj.display;

            if (fine_print_option_obj.value == null)
                fine_print_option_data.value = "";
            else
                fine_print_option_data.value = fine_print_option_obj.value;

            fine_print_option_data.is_selected = fine_print_option_obj.is_selected;
            fine_print_option_data.is_active = fine_print_option_obj.is_active;

            return fine_print_option_data;
        }

        public DAL.Login_Log Convert_Login_Log(API.Login_Log login_log_obj)
        {
            DAL.Login_Log login_log_dal = new DAL.Login_Log();
            if (login_log_obj.log_id != 0)
                login_log_dal.log_id = login_log_obj.log_id;

            if (login_log_obj.contact_id != 0)
                login_log_dal.contact_id = login_log_obj.contact_id;

            if (login_log_obj.customer_id != 0)
                login_log_dal.customer_id = login_log_obj.customer_id;

            if (login_log_obj.merchant_contact_id != 0)
                login_log_dal.merchant_contact_id = login_log_obj.merchant_contact_id;

            if (login_log_obj.application_type_id != 0)
                login_log_dal.application_type_id = login_log_obj.application_type_id;

                login_log_dal.registered = login_log_obj.registered;

            if (login_log_obj.login_date_utc_stamp != DateTime.Parse("1/1/0001 12:00:00 AM"))
                login_log_dal.login_date_utc_stamp = login_log_obj.login_date_utc_stamp;

            if (login_log_obj.auth_token != null)
                login_log_dal.auth_token = login_log_obj.auth_token;

            if (login_log_obj.application_type_obj != null)
                login_log_dal.application_type_dal = Convert_Application_Type(login_log_obj.application_type_obj);

            return login_log_dal;
        }


        public List<DAL.Fine_Print_Option> Fine_Print_Option_Array(List<API.Fine_Print_Option> fine_print_option_obj_array)
        {
            List<DAL.Fine_Print_Option> fine_print_option_array_data = new List<DAL.Fine_Print_Option>();

            foreach (API.Fine_Print_Option fine_print_option_obj in fine_print_option_obj_array) // Loop through List with foreach.
            {
                fine_print_option_array_data.Add(Fine_Print_Option(fine_print_option_obj));
            }

            return fine_print_option_array_data;
        }

        #endregion



    }
}