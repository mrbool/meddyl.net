using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.CoreDotNet.Database
{
    public class SQLServer_Data
    {
        #region Class Member Declarations

        protected GTSoft.CoreDotNet.Database.SQLServer_Connector _mainConnect;
        protected SqlConnection _mainConnection;
        protected SqlInt32 _errorCode;

        #endregion



        #region Constructor

        public SQLServer_Data(string data_source, string authentication, string user_name, string password, string database, int connection_timeout, int port)
        {
            _mainConnect = new CoreDotNet.Database.SQLServer_Connector(data_source, authentication, user_name, password, database, connection_timeout, port);
            _mainConnection = _mainConnect.DBConnection;
        }

        public SQLServer_Data(string xml_data_source)
        {
            _mainConnect = new CoreDotNet.Database.SQLServer_Connector(xml_data_source);
            _mainConnection = _mainConnect.DBConnection;
        }

        #endregion




        #region Public Methods

        public DataTable ReturnDataTable(string sql)
        {
			SqlCommand	scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = sql;
			scmCmdToExecute.CommandType = CommandType.Text;
			DataTable toReturn = new DataTable("Query");
			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

			scmCmdToExecute.Connection = _mainConnection;

			try
			{
				// Open connection.
				_mainConnection.Open();

				// Execute query.
				adapter.Fill(toReturn);

				return toReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw ex;
			}
			finally
			{
				_mainConnection.Close();
				scmCmdToExecute.Dispose();
				adapter.Dispose();
			}
        }

        public void ExecuteNonQuery(string sql)
        {
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = sql;
			scmCmdToExecute.CommandType = CommandType.Text;

			scmCmdToExecute.Connection = _mainConnection;

			try
			{				
				// Open connection.
				_mainConnection.Open();

				// Execute query.
				scmCmdToExecute.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw ex;
			}
			finally
			{
				_mainConnection.Close();
				scmCmdToExecute.Dispose();
			}
		}

        #endregion




        #region Class Property Declarations


        #endregion




    }
}
