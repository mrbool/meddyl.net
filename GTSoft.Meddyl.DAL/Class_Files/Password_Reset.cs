using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Password_Reset : GTSoft.Meddyl.DAL.Base_Password_Reset
    {
        #region constructors

		public Password_Reset()
		{
			//
			// TODO:  Add constructor logic here
			//
		}

        #endregion


        #region public methods

        public bool usp_Password_Reset_Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Password_Reset_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_status_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, status_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_email", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, email));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_reset_id", SqlDbType.UniqueIdentifier, 36, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, reset_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                reset_id = (SqlGuid)scmCmdToExecute.Parameters["@o_reset_id"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Password_Reset_Insert' reported the ErrorCode: " + errorCode);
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

        public bool usp_Password_Reset_UpdatePK()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Password_Reset_UpdatePK]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_reset_id", SqlDbType.UniqueIdentifier, 16, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, reset_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Password_Reset_UpdatePK' reported the ErrorCode: " + errorCode);
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

        #endregion


        #region properties


        #endregion
    }
}
