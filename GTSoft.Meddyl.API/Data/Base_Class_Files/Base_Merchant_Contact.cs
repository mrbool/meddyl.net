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
	public class Base_Merchant_Contact
	{
		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int validation_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string title { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime accept_terms_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Contact contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant merchant_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact_Status merchant_contact_status_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact_Validation merchant_contact_validation_obj { get; set; }

	}
}

