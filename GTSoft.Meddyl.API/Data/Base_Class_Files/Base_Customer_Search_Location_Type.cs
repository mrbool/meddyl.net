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
	public class Base_Customer_Search_Location_Type
	{
		[DataMember(EmitDefaultValue=false)]
		public int search_location_type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string location_type { get; set; }

	}
}

