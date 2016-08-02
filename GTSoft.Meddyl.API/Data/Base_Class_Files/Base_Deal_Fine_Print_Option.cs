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
	public class Base_Deal_Fine_Print_Option
	{
		[DataMember(EmitDefaultValue=false)]
		public int id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int option_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Deal deal_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Fine_Print_Option fine_print_option_obj { get; set; }

	}
}

