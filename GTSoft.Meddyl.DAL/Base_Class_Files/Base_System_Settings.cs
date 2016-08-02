using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Base_System_Settings
	{
		#region variables

		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;
		public SqlConnection mainConnection;
		public int connection_timeout;
		public SqlInt32 errorCode;

		#endregion


		#region constructor

		public Base_System_Settings()
		{
			mainConnect = new GTSoft.CoreDotNet.Database.SQLServer_Connector("Meddyl");
			mainConnection = mainConnect.DBConnection;
			connection_timeout = mainConnect.connection_timeout;
		}

		#endregion


		#region stored procedures

		public bool Insert()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_System_Settings_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_auth_net_api_login_id", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, auth_net_api_login_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_auth_net_transaction_key", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, auth_net_transaction_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_braintree_public_key", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, braintree_public_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_braintree_private_key", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, braintree_private_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_braintree_merchant_id", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, braintree_merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_cost_mode", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_cost_mode));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_customer_amount", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, certificate_customer_amount));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_days_valid_default", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_days_valid_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_delay_hours", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_delay_hours));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_merchant_amount", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, certificate_merchant_amount));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_merchant_percentage", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_merchant_percentage));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_quantity_info", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certificate_quantity_info));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_quantity_min", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_quantity_min));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_quantity_max", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_quantity_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_system", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, credit_card_system));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_app_android_id", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, customer_app_android_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_app_ios_id", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, customer_app_ios_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_app_terms", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, customer_app_terms));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_deal_range_default", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_deal_range_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_deal_range_max", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_deal_range_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_deal_range_min", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_deal_range_min));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_fine_print", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_fine_print));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_instructions_default", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_instructions_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_min_ranking", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_min_ranking));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_max_ranking", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_max_ranking));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_needs_credit_card", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_needs_credit_card));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_new_customer_only_info", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_new_customer_only_info));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_new_customer_only", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_new_customer_only));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_use_immediately", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_use_immediately));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_use_immediately_info", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_use_immediately_info));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_validate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal_validate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_dollar_value_info", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, dollar_value_info));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_dollar_value_min", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, dollar_value_min));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_dollar_value_max", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, dollar_value_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_email_admin", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email_admin));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_email_on", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email_on));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_email_system", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email_system));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_email_validation", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email_validation));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_days_info", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, expiration_days_info));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_days_max", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, expiration_days_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_days_min", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, expiration_days_min));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fb_app_id", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fb_app_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fb_app_secret", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fb_app_secret));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fb_scope", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fb_scope));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fb_redirect_url", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fb_redirect_url));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print_more_characters", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fine_print_more_characters));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print_more_default", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fine_print_more_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_google_api_key", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, google_api_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_gps_accuracy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, gps_accuracy));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_gps_timeout", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, gps_timeout));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image_url", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image_url));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image_folder", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image_folder));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_mailgun_domain", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mailgun_domain));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_mailgun_api_private_key", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mailgun_api_private_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_mailgun_api_public_key", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mailgun_api_public_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_mailgun_url", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mailgun_url));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_active_deals_max", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_active_deals_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_app_android_id", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_app_android_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_app_ios_id", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_app_ios_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_app_terms", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_app_terms));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_approve", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_contact_approve));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_validate", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_contact_validate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_description_characters", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_description_characters));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_description_default", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, merchant_description_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_password_reset_days", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, password_reset_days));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_pci_key", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pci_key));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off_default", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off_default));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off_max", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off_max));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off_min", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off_min));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_plivo_auth_id", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, plivo_auth_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_plivo_auth_token", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, plivo_auth_token));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_referral_days", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, promotion_referral_days));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_smtp_from_email", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, smtp_from_email));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_sms_on", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sms_on));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_sms_system", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, sms_system));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_smtp_port", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, smtp_port));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_smtp_server", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, smtp_server));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_twilio_account_sid", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, twilio_account_sid));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_twilio_auth_token", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, twilio_auth_token));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_twilio_test_account_sid", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, twilio_test_account_sid));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_twilio_test_auth_token", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, twilio_test_auth_token));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_System_Settings_Insert' reported the ErrorCode: " + errorCode);
				}

				return true;
			}
			catch(Exception ex)
			{
				/* some error occured. Bubble it to caller and encapsulate Exception object */
				throw ex;
			}
			finally
			{
				mainConnection.Close();
				scmCmdToExecute.Dispose();
			}
		}

		public DataTable SelectAll()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_System_Settings_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("System_Settings");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_System_Settings_SelectAll' reported the ErrorCode: " + errorCode);
				}

				return toReturn;
			}
			catch(Exception ex)
			{
				/* some error occured. Bubble it to caller and encapsulate Exception object */
				throw ex;
			}
			finally
			{
				mainConnection.Close();
				scmCmdToExecute.Dispose();
				adapter.Dispose();
			}
		}

		#endregion


		#region property declarations

		public SqlString auth_net_api_login_id { get; set; }
		public SqlString auth_net_transaction_key { get; set; }
		public SqlString braintree_public_key { get; set; }
		public SqlString braintree_private_key { get; set; }
		public SqlString braintree_merchant_id { get; set; }
		public SqlInt32 certificate_cost_mode { get; set; }
		public SqlMoney certificate_customer_amount { get; set; }
		public SqlInt32 certificate_days_valid_default { get; set; }
		public SqlInt32 certificate_delay_hours { get; set; }
		public SqlMoney certificate_merchant_amount { get; set; }
		public SqlInt32 certificate_merchant_percentage { get; set; }
		public SqlString certificate_quantity_info { get; set; }
		public SqlInt32 certificate_quantity_min { get; set; }
		public SqlInt32 certificate_quantity_max { get; set; }
		public SqlString credit_card_system { get; set; }
		public SqlString customer_app_android_id { get; set; }
		public SqlString customer_app_ios_id { get; set; }
		public SqlString customer_app_terms { get; set; }
		public SqlInt32 customer_deal_range_default { get; set; }
		public SqlInt32 customer_deal_range_max { get; set; }
		public SqlInt32 customer_deal_range_min { get; set; }
		public SqlString deal_fine_print { get; set; }
		public SqlString deal_instructions_default { get; set; }
		public SqlInt32 deal_min_ranking { get; set; }
		public SqlInt32 deal_max_ranking { get; set; }
		public SqlBoolean deal_needs_credit_card { get; set; }
		public SqlString deal_new_customer_only_info { get; set; }
		public SqlBoolean deal_new_customer_only { get; set; }
		public SqlBoolean deal_use_immediately { get; set; }
		public SqlString deal_use_immediately_info { get; set; }
		public SqlBoolean deal_validate { get; set; }
		public SqlString dollar_value_info { get; set; }
		public SqlMoney dollar_value_min { get; set; }
		public SqlMoney dollar_value_max { get; set; }
		public SqlString email_admin { get; set; }
		public SqlBoolean email_on { get; set; }
		public SqlString email_system { get; set; }
		public SqlBoolean email_validation { get; set; }
		public SqlString expiration_days_info { get; set; }
		public SqlInt32 expiration_days_max { get; set; }
		public SqlInt32 expiration_days_min { get; set; }
		public SqlString fb_app_id { get; set; }
		public SqlString fb_app_secret { get; set; }
		public SqlString fb_scope { get; set; }
		public SqlString fb_redirect_url { get; set; }
		public SqlInt32 fine_print_more_characters { get; set; }
		public SqlString fine_print_more_default { get; set; }
		public SqlString google_api_key { get; set; }
		public SqlString gps_accuracy { get; set; }
		public SqlInt32 gps_timeout { get; set; }
		public SqlString image_url { get; set; }
		public SqlString image_folder { get; set; }
		public SqlString mailgun_domain { get; set; }
		public SqlString mailgun_api_private_key { get; set; }
		public SqlString mailgun_api_public_key { get; set; }
		public SqlString mailgun_url { get; set; }
		public SqlInt32 merchant_active_deals_max { get; set; }
		public SqlString merchant_app_android_id { get; set; }
		public SqlString merchant_app_ios_id { get; set; }
		public SqlString merchant_app_terms { get; set; }
		public SqlBoolean merchant_contact_approve { get; set; }
		public SqlBoolean merchant_contact_validate { get; set; }
		public SqlInt32 merchant_description_characters { get; set; }
		public SqlString merchant_description_default { get; set; }
		public SqlInt32 password_reset_days { get; set; }
		public SqlString pci_key { get; set; }
		public SqlInt32 percent_off_default { get; set; }
		public SqlInt32 percent_off_max { get; set; }
		public SqlInt32 percent_off_min { get; set; }
		public SqlString plivo_auth_id { get; set; }
		public SqlString plivo_auth_token { get; set; }
		public SqlInt32 promotion_referral_days { get; set; }
		public SqlString smtp_from_email { get; set; }
		public SqlBoolean sms_on { get; set; }
		public SqlString sms_system { get; set; }
		public SqlInt32 smtp_port { get; set; }
		public SqlString smtp_server { get; set; }
		public SqlString twilio_account_sid { get; set; }
		public SqlString twilio_auth_token { get; set; }
		public SqlString twilio_test_account_sid { get; set; }
		public SqlString twilio_test_auth_token { get; set; }

		#endregion
	}
}

