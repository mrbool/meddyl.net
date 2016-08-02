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
	public class Base_Merchant
	{
		[DataMember(EmitDefaultValue=false)]
		public int merchant_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int industry_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int neighborhood_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int rating_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string company_name { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string address_1 { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string address_2 { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public double latitude { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public double longitude { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string phone { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string website { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string description { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int max_active_deals { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string yelp_business_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Industry industry_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Rating merchant_rating_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Merchant_Status merchant_status_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Neighborhood neighborhood_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Zip_Code zip_code_obj { get; set; }

	}
}

