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
	public class Base_Contact
	{
		[DataMember(EmitDefaultValue=false)]
		public int contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public long facebook_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string first_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string last_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string email { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string phone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string user_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string password { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Zip_Code zip_code_obj { get; set; }

	}
}

