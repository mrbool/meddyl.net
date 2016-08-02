using System;
using System.Data;
using System.Data.Odbc;

namespace GTSoft.CoreDotNet.Database
{
    public class ODBC_Data
    {
        #region Class Member Declarations

        protected CoreDotNet.Database.ODBC_Connector _mainConnect;
        protected OdbcConnection _mainConnection;

        #endregion



        #region Constructor

        public ODBC_Data(string data_source, int connection_timeout)
        {
            _mainConnect = new CoreDotNet.Database.ODBC_Connector(data_source, connection_timeout);
            _mainConnection = _mainConnect.DBConnection;
        }

        public ODBC_Data(string xml_data_source)
        {
            _mainConnect = new CoreDotNet.Database.ODBC_Connector(xml_data_source);
            _mainConnection = _mainConnect.DBConnection;
        }

        #endregion




        #region Public Methods

        public DataTable ReturnDataTable(string sql)
        {
            OdbcCommand scmCmdToExecute = new OdbcCommand();
            scmCmdToExecute.CommandText = sql;
            scmCmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Query");
            OdbcDataAdapter adapter = new OdbcDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);

                return toReturn;
            }
            catch (Exception ex)
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
            OdbcCommand scmCmdToExecute = new OdbcCommand();
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
            catch (Exception ex)
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
