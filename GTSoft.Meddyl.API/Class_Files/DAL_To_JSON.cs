using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GTSoft.Meddyl.DAL;

namespace GTSoft.Meddyl.API
{
    public class DAL_To_JSON
    {
        
        #region constructor

        public DAL_To_JSON()
        {
        }

        #endregion


        #region public methods

        public API.System_Error Convert_System_Error(DAL.System_Error system_error_dal)
        {
            API.System_Error system_error_obj = new API.System_Error();
            if (system_error_dal.code != 0)
                system_error_obj.code = int.Parse(system_error_dal.code.ToString());

            if (!system_error_dal.message.IsNull)
                system_error_obj.message = system_error_dal.message.ToString();

            return system_error_obj;
        }

        public API.System_Successful Convert_System_Successful(DAL.System_Successful system_successful_dal)
        {
            API.System_Successful system_successful_obj = new API.System_Successful();
            if (system_successful_dal.code != 0)
                system_successful_obj.code = int.Parse(system_successful_dal.code.ToString());

            if (!system_successful_dal.message.IsNull)
                system_successful_obj.message = system_successful_dal.message.ToString();

            return system_successful_obj;
        }
        
        #endregion



    }
}