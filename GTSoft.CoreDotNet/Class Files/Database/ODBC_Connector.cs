using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Xml;
using System.IO;

namespace GTSoft.CoreDotNet.Database
{
    public class ODBC_Connector : IDisposable
    {
        #region Class Member Declarations

        private OdbcConnection _dBConnection;
        private bool _isDisposed;

        #endregion


        #region Constructor

        public ODBC_Connector(string data_source, int connection_timeout)
        {
            InitClass(data_source, connection_timeout);
        }

        public ODBC_Connector(string xml_data_source)
		{
            InitClass(xml_data_source);
        }

        #endregion




        /// <summary>
        /// Purpose: Implements the IDispose' method Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Purpose: Implements the Dispose functionality.
        /// </summary>
        protected virtual void Dispose(bool isDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    // Dispose managed resources.
                    if (_dBConnection != null)
                    {
                        // closing the connection will abort (rollback) any pending transactions
                        _dBConnection.Close();
                        _dBConnection.Dispose();
                        _dBConnection = null;
                    }
                }
            }
            _isDisposed = true;
        }

        /// <summary>
        /// Purpose: Initializes class members.
        /// </summary>
        private void InitClass(string xml_data_source)
        {
            string system_folder = "", path = "", connection_name = "", server_type = "";
            string data_source = "";

            // create all the objects and initialize other members.
            _dBConnection = new OdbcConnection();

            CoreDotNet.Utilities utilities = new Utilities();
            system_folder = utilities.Get_Application_Directory("System");
            path = system_folder + @"\\connections.xml";

            FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            System.Xml.XmlDocument xml_connection = new System.Xml.XmlDocument();
            xml_connection.Load(reader);

            XmlNodeList node_list = xml_connection.GetElementsByTagName("connection");
            for (int i = 0; i < node_list.Count; i++)
            {
                connection_name = node_list[i].ChildNodes[0].InnerText;
                if (connection_name == xml_data_source)
                {
                    server_type = node_list[i].ChildNodes[1].InnerText;
                    data_source = node_list[i].ChildNodes[2].InnerText;
                }
            }

            Build_Connection_String(data_source);
        }

        private void InitClass(string data_source, int connection_timeout)
        {
            _dBConnection = new OdbcConnection();
            Build_Connection_String(data_source);
        }

        private void Build_Connection_String(string data_source)
        {
            string connectionstring = "";

            connectionstring = "dsn=" + data_source + ";";

            _dBConnection.ConnectionString = connectionstring;

            _isDisposed = false;
        }




        #region Class Property Declarations

        public OdbcConnection DBConnection
        {
            get
            {
                return _dBConnection;
            }
        }

        #endregion




    }
}
