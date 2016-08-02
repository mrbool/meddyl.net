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
	public class Zip_Code : GTSoft.Meddyl.API.Base_Zip_Code
	{
        [DataMember(EmitDefaultValue = false)]
        public List<Neighborhood> neighborhood_obj_array { get; set; }

	}
}

