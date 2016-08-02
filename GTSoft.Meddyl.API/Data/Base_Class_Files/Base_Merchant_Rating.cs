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
	public class Base_Merchant_Rating
	{
		[DataMember(EmitDefaultValue=false)]
		public int rating_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Decimal rating { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image { get; set; }

	}
}

