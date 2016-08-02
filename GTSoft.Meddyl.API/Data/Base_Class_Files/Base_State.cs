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
	public class Base_State
	{
		[DataMember(EmitDefaultValue=false)]
		public int state_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string state { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string abbreviation { get; set; }

	}
}

