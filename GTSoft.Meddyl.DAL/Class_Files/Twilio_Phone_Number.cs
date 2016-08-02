using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Twilio_Phone_Number : GTSoft.Meddyl.DAL.Base_Twilio_Phone_Number
	{
		#region constructors

		public Twilio_Phone_Number()
		{
		}

		#endregion


		#region public methods

        public DataTable usp_Twilio_Phone_Number_Current()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Twilio_Phone_Number_Current]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Twilio_Phone_Number");
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

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Twilio_Phone_Number_Current' reported the ErrorCode: " + errorCode);
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
