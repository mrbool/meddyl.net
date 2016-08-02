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
	public class Base_Payment_Log
	{
		[DataMember(EmitDefaultValue=false)]
		public int log_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int payment_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int credit_card_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_successful { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string type { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string notes { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Certificate certificate_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal deal_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact merchant_contact_obj { get; set; }

	}
}

