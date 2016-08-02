using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Credit_Card_Type : GTSoft.Meddyl.DAL.Base_Credit_Card_Type
	{
		#region constructors

		public Credit_Card_Type()
		{
		}

		#endregion


		#region public methods

        public DataTable usp_Credit_Card_Type_Select_By_type()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Type_Select_By_type]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card_Type");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_type", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, type));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Type_Select_By_type' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
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
                adapter.Dispose();
            }
        }

		#endregion


		#region properties


		#endregion
	}
}
