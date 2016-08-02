using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using GTSoft.CoreDotNet;

namespace GTSoft.Meddyl.DAL
{
	public class Deal : GTSoft.Meddyl.DAL.Base_Deal
    {
        #region constructors

		public Deal()
		{
			//
			// TODO:  Add constructor logic here
			//
		}

        #endregion


        #region public methods

        public bool usp_Deal_Text_Fine_Print_Text()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Text_Fine_Print_Text]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fine_print));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_max_dollar_amount", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, max_dollar_amount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_use_deal_immediately", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, use_deal_immediately));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_is_valid_new_customer_only", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, is_valid_new_customer_only));


                SqlParameter p = scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print_option_array", SqlDbType.Structured));
                if (fine_print_option_dal_array != null)
                {
                    DataTable dt = CoreDotNet.Utilities.Convert_List_To_DataTable(fine_print_option_dal_array);
                    p.Value = dt;
                }
                else
                {
                    p.Value = null;
                }
                p.TypeName = "dbo.fine_print_option_array";

                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_deal", SqlDbType.VarChar, 1000, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, deal));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_fine_print_ext", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, fine_print_ext));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                deal = (SqlString)scmCmdToExecute.Parameters["@o_deal"].Value;
                fine_print_ext = (SqlString)scmCmdToExecute.Parameters["@o_fine_print_ext"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Deal_Text_Fine_Print_Text' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fine_print));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_max_dollar_amount", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, max_dollar_amount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_quantity));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, expiration_date));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_use_deal_immediately", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, use_deal_immediately));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_is_valid_new_customer_only", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, is_valid_new_customer_only));

                SqlParameter p = scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print_option_array", SqlDbType.Structured));
                if (fine_print_option_dal_array != null)
                {
                    DataTable dt = CoreDotNet.Utilities.Convert_List_To_DataTable(fine_print_option_dal_array);
                    p.Value = dt;
                }
                else
                {
                    p.Value = null;
                }
                p.TypeName = "dbo.fine_print_option_array";

                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_deal_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                deal_id = (SqlInt32)scmCmdToExecute.Parameters["@o_deal_id"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Deal_Insert' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_Update()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Update]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id)); 
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, deal));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, fine_print));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_percent_off", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, percent_off));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_max_dollar_amount", SqlDbType.Money, 8, ParameterDirection.Input, false, 19, 4, "", DataRowVersion.Proposed, max_dollar_amount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_quantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_quantity));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_date", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, expiration_date));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_use_deal_immediately", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, use_deal_immediately));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_is_valid_new_customer_only", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, is_valid_new_customer_only));

                SqlParameter p = scmCmdToExecute.Parameters.Add(new SqlParameter("@p_fine_print_option_array", SqlDbType.Structured));
                if (fine_print_option_dal_array != null)
                {
                    DataTable dt = CoreDotNet.Utilities.Convert_List_To_DataTable(fine_print_option_dal_array);
                    p.Value = dt;
                }
                else
                {
                    p.Value = null;
                }
                p.TypeName = "dbo.fine_print_option_array";

                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Update' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public DataTable usp_Deal_Merchant_merchant_contact_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Merchant_merchant_contact_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Merchant_Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Merchant_merchant_contact_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Deal_SelectPK_deal_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_SelectPK_deal_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_SelectPK_deal_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public bool usp_Deal_Cancel_deal_id_merchant_contact_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Cancel_deal_id_merchant_contact_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_dal.merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '" + System.Reflection.MethodInfo.GetCurrentMethod() + "' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_UpdatePK_promotion_code()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_UpdatePK_promotion_code]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, promotion_activity_dal.promotion_dal.promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_UpdatePK_promotion_code' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public DataTable usp_Deals_Select_By_customer_id_location()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deals_Select_By_customer_id_location]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Merchant_Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, latitude));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, longitude));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deals_Select_By_customer_id_location' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public bool usp_Deal_Approve()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Approve]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_ranking", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ranking));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Approve' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_Reject()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Reject]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Reject' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_UpdatePK_status_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_UpdatePK_status_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_UpdatePK_status_id' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_UpdatePK_ranking()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_UpdatePK_ranking]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_ranking", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ranking));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_UpdatePK_ranking' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Deal_UpdatePK_image()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_UpdatePK_image]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_UpdatePK_image' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public DataTable usp_Deal_Select_By_deal_id_Log_customer_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Select_By_deal_id_Log_customer_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_dal.customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_search_location_type_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_dal.customer_search_location_type_dal.search_location_type_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_dal.zip_code_dal.latitude));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_dal.zip_code_dal.longitude));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, customer_dal.zip_code_dal.zip_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Select_By_deal_id_Log_customer_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Deal_Select_deal_id_customer_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Select_deal_id_customer_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Select_deal_id_customer_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Deal_SelectFK_Deal_Status_status_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_SelectFK_Deal_Status_status_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_SelectFK_Deal_Status_status_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Deals_Pending_Payment()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deals_Pending_Payment]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Merchant_Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deals_Pending_Payment' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Deal_Search()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Deal_Search]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Deal");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_search", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, search));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Deal_Search' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
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


        #region properties

        public SqlInt32 customer_id { get; set; }
        public SqlDecimal distance { get; set; }
        public SqlDouble latitude { get; set; }
        public SqlDouble longitude { get; set; }
        public SqlString promotion_code { get; set; }
        public SqlString image_base64 { get; set; }
        public SqlString search { get; set; }
        public SqlInt32 certificates_remaining { get; set; }
        public SqlInt32 certificates_sold { get; set; }
        public SqlInt32 certificates_redeemed { get; set; }
        public SqlInt32 certificates_unused { get; set; }
        public SqlInt32 certificates_expired { get; set; }
        public SqlDateTime last_redeemed_date { get; set; }
        public SqlDateTime last_assigned_date { get; set; }

        public DAL.Customer customer_dal { get; set; }

        public List<DAL.Fine_Print_Option> fine_print_option_dal_array { get; set; }

		#endregion
	}
}
