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
	public class Customer : GTSoft.Meddyl.API.Base_Customer
    {
        [DataMember(EmitDefaultValue = false)]
        public Application_Type application_type_obj { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Login_Log login_log_obj { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Promotion promotion_obj { get; set; }
	}
}

