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
	public class Merchant_Contact : GTSoft.Meddyl.API.Base_Merchant_Contact
	{
        [DataMember(EmitDefaultValue = false)]
        public string search { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Login_Log login_log_obj { get; set; }
	}
}

