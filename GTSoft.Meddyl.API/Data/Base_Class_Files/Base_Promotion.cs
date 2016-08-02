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
	public class Base_Promotion
	{
		[DataMember(EmitDefaultValue=false)]
		public int promotion_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int promotion_type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string promotion_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string description { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Promotion_Type promotion_type_obj { get; set; }

	}
}

