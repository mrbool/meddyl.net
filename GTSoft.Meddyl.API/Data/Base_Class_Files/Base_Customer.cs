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
	public class Base_Customer
	{
		[DataMember(EmitDefaultValue=false)]
		public int customer_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int contact_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int status_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int search_location_type_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string search_zip_code { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int search_industry_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_range { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime accept_terms_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public DateTime entry_date_utc_stamp { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Contact contact_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer_Search_Location_Type customer_search_location_type_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Customer_Status customer_status_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Industry industry_obj { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public Zip_Code zip_code_obj { get; set; }

	}
}

