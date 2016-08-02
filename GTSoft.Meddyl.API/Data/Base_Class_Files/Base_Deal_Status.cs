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
	public class Base_Deal_Status
	{
		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string status { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int order_id { get; set; }

	}
}

