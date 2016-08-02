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
	public class Base_Promotion_Activity
	{
		[DataMember(EmitDefaultValue=false)]
		public int promotion_activity_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int promotion_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int parent_activity_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime redeemed_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact merchant_contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Promotion promotion_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Promotion_Activity promotion_activity_obj { get; set; }

	}
}

