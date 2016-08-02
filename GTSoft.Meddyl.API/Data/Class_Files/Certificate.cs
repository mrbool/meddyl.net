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
	public class Certificate : GTSoft.Meddyl.API.Base_Certificate
    {
        [DataMember(EmitDefaultValue = false)]
        public string status_text_1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string status_text_2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string search { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Application_Type application_type_obj { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Login_Log login_log_obj { get; set; }
	}
}

