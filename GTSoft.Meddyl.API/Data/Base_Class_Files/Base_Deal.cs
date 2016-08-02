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
	public class Base_Deal
	{
		[DataMember(EmitDefaultValue=false)]
		public int deal_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int promotion_activity_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int validation_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int time_zone_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string deal { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fine_print { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fine_print_ext { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int percent_off { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal max_dollar_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_quantity { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal deal_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal certificate_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_days_valid { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_delay_hours { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool use_deal_immediately { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_valid_new_customer_only { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string instructions { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int ranking { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal_Status deal_status_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal_Validation deal_validation_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact merchant_contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Promotion_Activity promotion_activity_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Time_Zone time_zone_obj { get; set; }

	}
}

