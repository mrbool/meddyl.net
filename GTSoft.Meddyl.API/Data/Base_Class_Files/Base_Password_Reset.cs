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
	public class Base_Password_Reset
	{
		[DataMember(EmitDefaultValue=false)]
		public string reset_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string email { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime expiration_date { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Contact contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Password_Reset_Status password_reset_status_obj { get; set; }

	}
}

