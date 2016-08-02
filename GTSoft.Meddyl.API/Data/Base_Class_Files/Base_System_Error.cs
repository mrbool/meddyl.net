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
	public class Base_System_Error
	{
		[DataMember(EmitDefaultValue=false)]
		public int code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string message { get; set; }

	}
}

