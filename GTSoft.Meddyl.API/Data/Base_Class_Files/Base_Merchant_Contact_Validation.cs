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
	public class Base_Merchant_Contact_Validation
	{
		[DataMember(EmitDefaultValue=false)]
		public int validation_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string validation_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string user_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string phone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string ip_address { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_validated { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

	}
}

