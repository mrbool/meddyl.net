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
	public class Base_Certificate
	{
		[DataMember(EmitDefaultValue=false)]
		public int certificate_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string certificate_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime assigned_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime start_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime redeemed_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Certificate_Status certificate_status_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal deal_obj { get; set; }

	}
}

