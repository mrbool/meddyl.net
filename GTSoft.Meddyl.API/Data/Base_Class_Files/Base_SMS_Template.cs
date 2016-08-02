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
	public class Base_SMS_Template
	{
		[DataMember(EmitDefaultValue=false)]
		public int template_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string description { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string body { get; set; }

	}
}

