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
	public class Merchant : GTSoft.Meddyl.API.Base_Merchant
    {
        [DataMember(EmitDefaultValue = false)]
        public string image_base64 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Industry top_industry_obj { get; set; }
	}
}

