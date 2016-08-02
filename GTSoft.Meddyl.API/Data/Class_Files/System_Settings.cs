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
	public class System_Settings : GTSoft.Meddyl.API.Base_System_Settings
    {
        [DataMember(EmitDefaultValue = false)]
        public string report { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int quantity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Login_Log login_log_obj { get; set; }
	}
}

