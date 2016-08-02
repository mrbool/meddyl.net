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
	public class Base_Twilio_Phone_Number
	{
		[DataMember(EmitDefaultValue=false)]
		public int phone_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string phone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_active { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

	}
}

