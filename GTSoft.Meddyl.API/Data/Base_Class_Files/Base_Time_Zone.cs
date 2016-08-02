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
	public class Base_Time_Zone
	{
		[DataMember(EmitDefaultValue=false)]
		public int time_zone_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string time_zone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string abbreviation { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int offset { get; set; }

	}
}

