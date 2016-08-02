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
	public class Base_System_Users
	{
		[DataMember(EmitDefaultValue=false)]
		public int user_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string user_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string password { get; set; }

	}
}

