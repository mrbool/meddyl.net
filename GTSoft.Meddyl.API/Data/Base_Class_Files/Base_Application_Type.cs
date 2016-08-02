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
	public class Base_Application_Type
	{
		[DataMember(EmitDefaultValue=false)]
		public int application_type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string application_type { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string version { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_down { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string down_message { get; set; }

	}
}

