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
	public class Base_Fine_Print_Option
	{
		[DataMember(EmitDefaultValue=false)]
		public int option_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string display { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string value { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_selected { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool is_active { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int order_id { get; set; }

	}
}

