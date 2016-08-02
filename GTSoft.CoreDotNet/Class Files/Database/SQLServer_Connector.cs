using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace GTSoft.CoreDotNet.Database
{
    public class SQLServer_Connector : IDisposable
    {
        #region Class Member Declarations

        private SqlConnection _dBConnection;
        private bool _isDisposed;
        private int _connection_timeout;

        #endregion


        #region Constructor

        public SQLServer_Connector(string data_source, string authentication, string user_name, string password, string database, int connection_timeout, int port)
        {
            InitClass(data_source, authentication, user_name, password, database, connection_timeout, port);
        }

        public SQLServer_Connector(string xml_data_source)
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
            string data_source = "", authentication = "", user_name = "", password = "", database = "";
            int port = 0;
            bool encrypt = false;

            // create all the objects and initialize other members.
            _dBConnection = new SqlConnection();

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
                    user_name = node_list[i].ChildNodes[3].InnerText;
                    password = node_list[i].ChildNodes[4].InnerText;
                    authentication = node_list[i].ChildNodes[5].InnerText;
                    database = node_list[i].ChildNodes[6].InnerText;
                    _connection_timeout = int.Parse(node_list[i].ChildNodes[7].InnerText);
                    port = int.Parse(node_list[i].ChildNodes[8].InnerText);
                    encrypt = bool.Parse(node_list[i].ChildNodes[9].InnerText);
                }
            }

            // Decrypt data if it is encrypted
            if (encrypt == true)
            {
                byte[] desKey = new ASCIIEncoding().GetBytes("k&hH%g4B");
                byte[] desIV = new ASCIIEncoding().GetBytes("k&hH%g4B");

                Security cryptor = new Security();
                user_name = cryptor.DESDecryptString(user_name, desKey, desIV);
                password = cryptor.DESDecryptString(password, desKey, desIV);
            }

            Build_Connection_String(data_source, authentication, user_name, password, database, _connection_timeout, port);
        }

        private void InitClass(string data_source, string authentication, string user_name, string password, string database, int connection_timeout, int port)
        {
            _dBConnection = new SqlConnection();
            Build_Connection_String(data_source, authentication, user_name, password, database, connection_timeout, port);
        }

        private void Build_Connection_String(string data_source, string authentication, string user_name, string password, string database, int connection_timeout, int port)
        {
            string connectionstring = "";

            // Append port if a specific port is used
            if (port != 0)
                data_source = data_source + "," + port.ToString();

            // Set connection string of the sqlconnection object
            if (authentication.ToLower() == "windows")
                connectionstring = "data source=" + data_source + ";initial catalog=" + database + ";integrated security=SSPI;connect timeout = " + connection_timeout.ToString();
            else
                connectionstring = "data source=" + data_source + ";initial catalog=" + database + ";uid=" + user_name + ";pwd=" + password + ";connect timeout=" + connection_timeout.ToString() + ";";

            _dBConnection.ConnectionString = connectionstring;

            _isDisposed = false;
        }



        #region Class Property Declarations

        public SqlConnection DBConnection
        {
            get
            {
                return _dBConnection;
            }
        }

        public int connection_timeout
        {
            get
            {
                return _connection_timeout;
            }
            set
            {
                _connection_timeout = value;
            }
        }

        #endregion




    }
}
