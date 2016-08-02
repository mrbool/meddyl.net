using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Promotion_Activity : GTSoft.Meddyl.DAL.Base_Promotion_Activity
    {
        #region constructors

		public Promotion_Activity()
		{
			//
			// TODO:  Add constructor logic here
			//
		}

        #endregion


        #region public methods

        public DataTable usp_Promotion_Activity_Valid_SelectFK_Customer_customer_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Promotion_Activity_Valid_SelectFK_Customer_customer_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Promotion_Activity");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Promotion_Activity_Valid_SelectFK_Customer_customer_id' reported the ErrorCode: " + errorCode);
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

        public bool usp_Promotion_Activity_Insert_By_Referral()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Promotion_Activity_Insert_By_Referral]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_promotion_customer_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, promotion_customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                promotion_customer_id = (SqlInt32)scmCmdToExecute.Parameters["@o_promotion_customer_id"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Promotion_Activity_Insert_By_Referral' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
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

        public bool usp_Promotion_Activity_Insert_To_Referral()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Promotion_Activity_Insert_To_Referral]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_activity_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, promotion_activity_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_promotion_customer_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, promotion_customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_promotion_code", SqlDbType.VarChar, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                promotion_customer_id = (SqlInt32)scmCmdToExecute.Parameters["@o_promotion_customer_id"].Value;
                promotion_code = (SqlString)scmCmdToExecute.Parameters["@o_promotion_code"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Promotion_Activity_Insert_To_Referral' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
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

        public bool usp_Promotion_Activity_Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Promotion_Activity_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_promotion_code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, promotion_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_promotion_activity_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, promotion_activity_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                promotion_activity_id = (SqlInt32)scmCmdToExecute.Parameters["@o_promotion_activity_id"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Promotion_Activity_Insert' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
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

		#endregion


        #region properties

        public SqlString promotion_code { get; set; }
        public SqlInt32 promotion_customer_id { get; set; }

        #endregion
    }
}
