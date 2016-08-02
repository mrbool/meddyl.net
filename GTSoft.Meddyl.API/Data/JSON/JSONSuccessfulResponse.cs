using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GTSoft.Meddyl.API
{
    [DataContract]
    public class JSONSuccessfulResponse : JSONResponse
    {
        [DataMember]
        public Object data_obj { get; set; }

        [DataMember]
        public System_Successful system_successful_obj { get; set; }

        public JSONSuccessfulResponse()
        {
            successful = true;

        }

    }
}

