using System;
using System.Data;
using System.Data.SqlClient;

namespace GTSoft.Core_Console
{
    /// <summary>
    /// Summary description for DB_Control.
    /// </summary>
    public class DatabaseControl
    {
        #region Class Member Declarations

        protected string _database_name = "";
        protected string _server = "";
        protected string _user_id = "";
        protected string _password = "";
        protected SqlConnection cnSql;

        #endregion


        public DatabaseControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        #region Public Methods

        public SqlConnection SQL_Connect()
        {
            try
            {
                string sConn;

                sConn = @"Data Source=" + _server + ";Database=" + _database_name + ";UID=" + _user_id + ";PWD=" + _password;
                cnSql = new SqlConnection(sConn);
                return this.cnSql;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable ReturnDataTable(string sSQL)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlConnection cnSql;

                cnSql = SQL_Connect();
                cnSql.Open();

                SqlCommand cmdSql = new SqlCommand(sSQL, cnSql);
                SqlDataAdapter daSql = new SqlDataAdapter(cmdSql);
                daSql.Fill(dt);

                cnSql.Close();
                cnSql = null;

                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ExecuteScalar_String(string sSQL)
        {
            try
            {
                string sValue;

                SqlConnection cnSql;
                SqlCommand cmd;

                cnSql = SQL_Connect();
                cnSql.Open();

                cmd = new SqlCommand(sSQL, cnSql);

                sValue = cmd.ExecuteScalar().ToString();

                cnSql.Close();
                cnSql = null;

                return sValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ExecuteScalar_Int(string sSQL)
        {
            try
            {
                int iValue;

                SqlConnection cnSql;
                SqlCommand cmd;

                cnSql = SQL_Connect();
                cnSql.Open();

                cmd = new SqlCommand(sSQL, cnSql);
                iValue = int.Parse(cmd.ExecuteScalar().ToString());

                cnSql.Close();
                cnSql = null;

                return iValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                int iValue;

                SqlConnection cnSql;
                SqlCommand cmd;

                cnSql = SQL_Connect();
                cnSql.Open();

                cmd = new SqlCommand(sSQL, cnSql);

                iValue = cmd.ExecuteNonQuery();

                cnSql.Close();
                cnSql = null;

                return iValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion




        #region Class Property Declarations

        public string server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public string database_name
        {
            get
            {
                return _database_name;
            }
            set
            {
                _database_name = value;
            }
        }

        public string user_id
        {
            get
            {
                return _user_id;
            }
            set
            {
                _user_id = value;
            }
        }

        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        #endregion

    }
}
