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
	public class Base_City
	{
		[DataMember(EmitDefaultValue=false)]
		public int city_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int state_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string city { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public State state_obj { get; set; }

	}
}

