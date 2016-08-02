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
	public class Base_Credit_Card_Type
	{
		[DataMember(EmitDefaultValue=false)]
		public int type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string type { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image { get; set; }

	}
}

