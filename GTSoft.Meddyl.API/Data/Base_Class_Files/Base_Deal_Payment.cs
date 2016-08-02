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
	public class Base_Deal_Payment
	{
		[DataMember(EmitDefaultValue=false)]
		public int payment_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int credit_card_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int promotion_activity_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string card_holder_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string card_number { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string card_expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal payment_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime payment_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Credit_Card credit_card_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal deal_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Promotion_Activity promotion_activity_obj { get; set; }

	}
}

