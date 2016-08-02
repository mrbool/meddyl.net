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
	public class Base_Facebook_Data_Location
	{
		[DataMember(EmitDefaultValue=false)]
		public int id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public long fb_profile_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public long fb_location_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

	}
}

