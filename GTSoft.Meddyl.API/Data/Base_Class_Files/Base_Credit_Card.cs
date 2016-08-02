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
	public class Base_Credit_Card
	{
		[DataMember(EmitDefaultValue=false)]
		public int credit_card_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string card_holder_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string card_number { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Byte[] card_number_encrypted { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string billing_zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool default_flag { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool deleted { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Credit_Card_Type credit_card_type_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact merchant_contact_obj { get; set; }

	}
}

