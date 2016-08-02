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
	public class Base_Zip_Code
	{
		[DataMember(EmitDefaultValue=false)]
		public string zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int city_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int time_zone_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public double latitude { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public double longitude { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public City city_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Time_Zone time_zone_obj { get; set; }

	}
}

