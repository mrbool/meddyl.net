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
	public class Base_Industry
	{
		[DataMember(EmitDefaultValue=false)]
		public int industry_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int parent_industry_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string industry { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int order_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Industry industry_obj { get; set; }

	}
}

