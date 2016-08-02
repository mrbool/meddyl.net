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
	public class Deal_Payment : GTSoft.Meddyl.API.Base_Deal_Payment
	{
        [DataMember(EmitDefaultValue = false)]
        public Application_Type application_type_obj { get; set; }
	}
}

