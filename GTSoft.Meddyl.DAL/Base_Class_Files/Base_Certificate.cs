using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Base_Certificate
	{
		#region variables

		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;
		public SqlConnection mainConnection;
		public int connection_timeout;
		public SqlInt32 errorCode;

		#endregion


		#region constructor

		public Base_Certificate()
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
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_code", SqlDbType.Char, 6, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certificate_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_assigned_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, assigned_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_start_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, start_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, expiration_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_redeemed_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, redeemed_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_amount", SqlDbType.Money, 8, ParameterDirection.Input, true, 19, 4, "", DataRowVersion.Proposed, amount));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_certificate_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, certificate_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				certificate_id = (SqlInt32)scmCmdToExecute.Parameters["@o_certificate_id"].Value;
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_Insert' reported the ErrorCode: " + errorCode);
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

		public bool UpdatePK_certificate_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_UpdatePK_certificate_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_code", SqlDbType.Char, 6, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certificate_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_assigned_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, assigned_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_start_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, start_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, expiration_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_redeemed_date", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, redeemed_date));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_amount", SqlDbType.Money, 8, ParameterDirection.Input, true, 19, 4, "", DataRowVersion.Proposed, amount));
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
					throw new Exception("Stored Procedure 'sp_Certificate_UpdatePK_certificate_id' reported the ErrorCode: " + errorCode);
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

		public bool DeletePK_certificate_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_DeletePK_certificate_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_DeletePK_certificate_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Certificate_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_DeleteFK_Certificate_Status_status_id]";
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
					throw new Exception("Stored Procedure 'sp_Certificate_DeleteFK_Certificate_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Customer_customer_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_DeleteFK_Customer_customer_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_DeleteFK_Customer_customer_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Deal_deal_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_DeleteFK_Deal_deal_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_DeleteFK_Deal_deal_id' reported the ErrorCode: " + errorCode);
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
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
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
					throw new Exception("Stored Procedure 'sp_Certificate_SelectAll' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectPK_certificate_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectPK_certificate_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, certificate_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_SelectPK_certificate_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Certificate_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectFK_Certificate_Status_status_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
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
					throw new Exception("Stored Procedure 'sp_Certificate_SelectFK_Certificate_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Customer_customer_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectFK_Customer_customer_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_SelectFK_Customer_customer_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Deal_deal_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectFK_Deal_deal_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_deal_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, deal_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_SelectFK_Deal_deal_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectUnique_certificate_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Certificate_SelectUnique_certificate_code]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Certificate");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_certificate_code", SqlDbType.Char, 6, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, certificate_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Certificate_SelectUnique_certificate_code' reported the ErrorCode: " + errorCode);
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

		public SqlInt32 certificate_id { get; set; }
		public SqlInt32 deal_id { get; set; }
		public SqlInt32 customer_id { get; set; }
		public SqlInt32 status_id { get; set; }
		public SqlString certificate_code { get; set; }
		public SqlDateTime assigned_date { get; set; }
		public SqlDateTime start_date { get; set; }
		public SqlDateTime expiration_date { get; set; }
		public SqlDateTime redeemed_date { get; set; }
		public SqlMoney amount { get; set; }
		public SqlDateTime entry_date_utc_stamp { get; set; }

		public Certificate_Status certificate_status_dal { get; set; }
		public Customer customer_dal { get; set; }
		public Deal deal_dal { get; set; }

		#endregion
	}
}

