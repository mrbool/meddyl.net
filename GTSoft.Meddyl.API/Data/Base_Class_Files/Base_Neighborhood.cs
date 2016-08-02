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
	public class Base_Neighborhood
	{
		[DataMember(EmitDefaultValue=false)]
		public int neighborhood_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string neighborhood { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Zip_Code zip_code_obj { get; set; }

	}
}

