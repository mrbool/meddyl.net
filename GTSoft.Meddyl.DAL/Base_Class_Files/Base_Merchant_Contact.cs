using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Base_Merchant_Contact
	{
		#region variables

		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;
		public SqlConnection mainConnection;
		public int connection_timeout;
		public SqlInt32 errorCode;

		#endregion


		#region constructor

		public Base_Merchant_Contact()
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
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_validation_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, validation_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_title", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, title));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				merchant_contact_id = (SqlInt32)scmCmdToExecute.Parameters["@o_merchant_contact_id"].Value;
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_Insert' reported the ErrorCode: " + errorCode);
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

		public bool UpdatePK_merchant_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_UpdatePK_merchant_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_validation_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, validation_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_title", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, title));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_accept_terms_date_utc_stamp", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, accept_terms_date_utc_stamp));
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_UpdatePK_merchant_contact_id' reported the ErrorCode: " + errorCode);
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

		public bool DeletePK_merchant_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_DeletePK_merchant_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_DeletePK_merchant_contact_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Contact_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_DeleteFK_Contact_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_DeleteFK_Contact_contact_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Merchant_merchant_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_DeleteFK_Merchant_merchant_id]";
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_DeleteFK_Merchant_merchant_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Merchant_Contact_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_DeleteFK_Merchant_Contact_Status_status_id]";
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_DeleteFK_Merchant_Contact_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Merchant_Contact_Validation_validation_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_DeleteFK_Merchant_Contact_Validation_validation_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_validation_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, validation_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_DeleteFK_Merchant_Contact_Validation_validation_id' reported the ErrorCode: " + errorCode);
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
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectAll' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectPK_merchant_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectPK_merchant_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectPK_merchant_contact_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Contact_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectFK_Contact_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectFK_Contact_contact_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Merchant_merchant_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectFK_Merchant_merchant_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectFK_Merchant_merchant_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Merchant_Contact_Status_status_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectFK_Merchant_Contact_Status_status_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
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
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectFK_Merchant_Contact_Status_status_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Merchant_Contact_Validation_validation_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectFK_Merchant_Contact_Validation_validation_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_validation_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, validation_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectFK_Merchant_Contact_Validation_validation_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectUnique_contact_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Merchant_Contact_SelectUnique_contact_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Merchant_Contact");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Merchant_Contact_SelectUnique_contact_id' reported the ErrorCode: " + errorCode);
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

		public SqlInt32 merchant_contact_id { get; set; }
		public SqlInt32 merchant_id { get; set; }
		public SqlInt32 contact_id { get; set; }
		public SqlInt32 status_id { get; set; }
		public SqlInt32 validation_id { get; set; }
		public SqlString title { get; set; }
		public SqlDateTime accept_terms_date_utc_stamp { get; set; }
		public SqlDateTime entry_date_utc_stamp { get; set; }

		public Contact contact_dal { get; set; }
		public Merchant merchant_dal { get; set; }
		public Merchant_Contact_Status merchant_contact_status_dal { get; set; }
		public Merchant_Contact_Validation merchant_contact_validation_dal { get; set; }

		#endregion
	}
}

