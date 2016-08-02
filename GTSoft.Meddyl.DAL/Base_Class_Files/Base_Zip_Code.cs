using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Base_Zip_Code
	{
		#region variables

		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;
		public SqlConnection mainConnection;
		public int connection_timeout;
		public SqlInt32 errorCode;

		#endregion


		#region constructor

		public Base_Zip_Code()
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
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_city_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_time_zone_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, time_zone_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, latitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, longitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_Insert' reported the ErrorCode: " + errorCode);
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

		public bool UpdatePK_zip_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_UpdatePK_zip_code]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, zip_code));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_city_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_time_zone_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, time_zone_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_latitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, latitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_longitude", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, longitude));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_UpdatePK_zip_code' reported the ErrorCode: " + errorCode);
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

		public bool DeletePK_zip_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_DeletePK_zip_code]";
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
					throw new Exception("Stored Procedure 'sp_Zip_Code_DeletePK_zip_code' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_City_city_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_DeleteFK_City_city_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_city_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_DeleteFK_City_city_id' reported the ErrorCode: " + errorCode);
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

		public bool DeleteFK_Time_Zone_time_zone_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_DeleteFK_Time_Zone_time_zone_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_time_zone_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, time_zone_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				scmCmdToExecute.ExecuteNonQuery();
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_DeleteFK_Time_Zone_time_zone_id' reported the ErrorCode: " + errorCode);
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
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Zip_Code");
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
					throw new Exception("Stored Procedure 'sp_Zip_Code_SelectAll' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectPK_zip_code()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_SelectPK_zip_code]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Zip_Code");
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
					throw new Exception("Stored Procedure 'sp_Zip_Code_SelectPK_zip_code' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_City_city_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_SelectFK_City_city_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Zip_Code");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_city_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_SelectFK_City_city_id' reported the ErrorCode: " + errorCode);
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

		public DataTable SelectFK_Time_Zone_time_zone_id()
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[sp_Zip_Code_SelectFK_Time_Zone_time_zone_id]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.CommandTimeout = connection_timeout;
			DataTable toReturn = new DataTable("Zip_Code");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = mainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@p_time_zone_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, time_zone_id));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

				/* open database connection */
				mainConnection.Open();

				/* execute query */
				adapter.Fill(toReturn);
				errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

				if(errorCode != 0)
				{
					/* throw error */
					throw new Exception("Stored Procedure 'sp_Zip_Code_SelectFK_Time_Zone_time_zone_id' reported the ErrorCode: " + errorCode);
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

		public SqlString zip_code { get; set; }
		public SqlInt32 city_id { get; set; }
		public SqlInt32 time_zone_id { get; set; }
		public SqlDouble latitude { get; set; }
		public SqlDouble longitude { get; set; }

		public City city_dal { get; set; }
		public Time_Zone time_zone_dal { get; set; }

		#endregion
	}
}

