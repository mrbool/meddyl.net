using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Base_Merchant
	{
		#region variables

		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;
		public SqlConnection mainConnection;
		public int connection_timeout;
		public SqlInt32 errorCode;

		#endregion


		#region constructor

		public Base_Merchant()
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
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_industry_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, industry_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_neighborhood_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, neighborhood_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_rating_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, rating_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_company_name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, company_name));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_address_1", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, address_1));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_address_2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, address_2));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, latitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, longitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_phone", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, phone));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_website", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, website));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_max_active_deals", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, max_active_deals));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_yelp_business_id", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, yelp_business_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_merchant_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				merchant_id = (SqlInt32)scmCmdToExecute.Parameters["@o_merchant_id"].Value;
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Insert' reported the ErrorCode: " + errorCode);
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

		public bool UpdatePK_merchant_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_UpdatePK_merchant_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_industry_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, industry_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_neighborhood_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, neighborhood_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_rating_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, rating_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_company_name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, company_name));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_address_1", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, address_1));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_address_2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, address_2));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, latitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, longitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_phone", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, phone));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_website", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, website));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, description));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_image", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, image));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_max_active_deals", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, max_active_deals));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_yelp_business_id", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, yelp_business_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_entry_date_utc_stamp", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, entry_date_utc_stamp));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_UpdatePK_merchant_id' reported the ErrorCode: " + errorCode);
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

		public bool DeletePK_merchant_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeletePK_merchant_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeletePK_merchant_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Industry_industry_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeleteFK_Industry_industry_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_industry_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, industry_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeleteFK_Industry_industry_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Merchant_Rating_rating_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeleteFK_Merchant_Rating_rating_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_rating_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, rating_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeleteFK_Merchant_Rating_rating_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Merchant_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeleteFK_Merchant_Status_status_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeleteFK_Merchant_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Neighborhood_neighborhood_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeleteFK_Neighborhood_neighborhood_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_neighborhood_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, neighborhood_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeleteFK_Neighborhood_neighborhood_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Zip_Code_zip_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_DeleteFK_Zip_Code_zip_code]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_DeleteFK_Zip_Code_zip_code' reported the ErrorCode: " + errorCode);
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
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
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
					throw new Exception("Stored Procedure 'sp_Merchant_SelectAll' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectPK_merchant_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectPK_merchant_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectPK_merchant_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Industry_industry_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectFK_Industry_industry_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_industry_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, industry_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectFK_Industry_industry_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Merchant_Rating_rating_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectFK_Merchant_Rating_rating_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_rating_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, rating_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectFK_Merchant_Rating_rating_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Merchant_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectFK_Merchant_Status_status_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectFK_Merchant_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Neighborhood_neighborhood_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectFK_Neighborhood_neighborhood_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_neighborhood_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, neighborhood_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectFK_Neighborhood_neighborhood_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Zip_Code_zip_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_SelectFK_Zip_Code_zip_code]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_SelectFK_Zip_Code_zip_code' reported the ErrorCode: " + errorCode);
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

		public SqlInt32 merchant_id { get; set; }
		public SqlInt32 industry_id { get; set; }
		public SqlInt32 status_id { get; set; }
		public SqlString zip_code { get; set; }
		public SqlInt32 neighborhood_id { get; set; }
		public SqlInt32 rating_id { get; set; }
		public SqlString company_name { get; set; }
		public SqlString address_1 { get; set; }
		public SqlString address_2 { get; set; }
		public SqlDouble latitude { get; set; }
		public SqlDouble longitude { get; set; }
		public SqlString phone { get; set; }
		public SqlString website { get; set; }
		public SqlString description { get; set; }
		public SqlString image { get; set; }
		public SqlInt32 max_active_deals { get; set; }
		public SqlString yelp_business_id { get; set; }
		public SqlDateTime entry_date_utc_stamp { get; set; }

		public Industry industry_dal { get; set; }
		public Merchant_Rating merchant_rating_dal { get; set; }
		public Merchant_Status merchant_status_dal { get; set; }
		public Neighborhood neighborhood_dal { get; set; }
		public Zip_Code zip_code_dal { get; set; }

		#endregion
	}
}

