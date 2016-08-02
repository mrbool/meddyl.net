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
	public class Base_System_Settings
	{
		[DataMember(EmitDefaultValue=false)]
		public string auth_net_api_login_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string auth_net_transaction_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string braintree_public_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string braintree_private_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string braintree_merchant_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_cost_mode { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal certificate_customer_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_days_valid_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_delay_hours { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal certificate_merchant_amount { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_merchant_percentage { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string certificate_quantity_info { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_quantity_min { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int certificate_quantity_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string credit_card_system { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string customer_app_android_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string customer_app_ios_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string customer_app_terms { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_deal_range_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_deal_range_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int customer_deal_range_min { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string deal_fine_print { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string deal_instructions_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_min_ranking { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int deal_max_ranking { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool deal_needs_credit_card { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string deal_new_customer_only_info { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool deal_new_customer_only { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool deal_use_immediately { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string deal_use_immediately_info { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool deal_validate { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string dollar_value_info { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal dollar_value_min { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public decimal dollar_value_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string email_admin { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool email_on { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string email_system { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool email_validation { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string expiration_days_info { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int expiration_days_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int expiration_days_min { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fb_app_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fb_app_secret { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fb_scope { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fb_redirect_url { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int fine_print_more_characters { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string fine_print_more_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string google_api_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string gps_accuracy { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int gps_timeout { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image_url { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string image_folder { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string mailgun_domain { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string mailgun_api_private_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string mailgun_api_public_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string mailgun_url { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_active_deals_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string merchant_app_android_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string merchant_app_ios_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string merchant_app_terms { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool merchant_contact_approve { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool merchant_contact_validate { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int merchant_description_characters { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string merchant_description_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int password_reset_days { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string pci_key { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int percent_off_default { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int percent_off_max { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int percent_off_min { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string plivo_auth_id { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string plivo_auth_token { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int promotion_referral_days { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string smtp_from_email { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public bool sms_on { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string sms_system { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public int smtp_port { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string smtp_server { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string twilio_account_sid { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string twilio_auth_token { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string twilio_test_account_sid { get; set; }

		[DataMember(EmitDefaultValue=false)]
		public string twilio_test_auth_token { get; set; }

	}
}

