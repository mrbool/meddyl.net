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
	public class Base_Facebook_Data_Profile
	{
		[DataMember(EmitDefaultValue=false)]
		public int id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public long fb_profile_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string birthday { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string email { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string first_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string gender { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string last_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string link { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string locale { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string middle_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string timezone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string updated_time { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string username { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string verified { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

	}
}

