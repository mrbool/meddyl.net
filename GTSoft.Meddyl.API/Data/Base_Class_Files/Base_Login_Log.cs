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
	public class Base_Login_Log
	{
		[DataMember(EmitDefaultValue=false)]
		public int log_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int application_type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool registered { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool auto_login { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string ip_address { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime login_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string auth_token { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Application_Type application_type_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Contact contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer customer_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Contact merchant_contact_obj { get; set; }

	}
}

