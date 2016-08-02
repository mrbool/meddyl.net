using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace GTSoft.Core_Console
{

    public class CreateDAL
    {
        #region Class Member Declarations

        private DatabaseControl database_control;
        private StreamWriter _swProcedures;
        private StreamWriter _swBaseClass;
        private StreamWriter _swBaseDataContractClass;
        private DataTable _dt_columns;
        private DataTable _dt_foreign_tables;
        private DataTable _dt_foreign_joins;
        private DataTable _dt_primary_keys;
        private DataTable _dt_unique_constraints;
        private string _table;

        protected string _server, _database, _user_name, _password, _folder, _namespace, _xml_data_source;
        protected string _server_type, _stored_proc_prefix;

        #endregion



        #region Constructor

        public CreateDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion




        #region Public Methods

        public void Process()
        {
            string SQL, proc_name, foreign_queried_table = "";
            DataTable dt;

            database_control = new DatabaseControl();
            database_control.server = _server;
            database_control.database_name = _database;
            database_control.user_id = _user_name;
            database_control.password = password;

            // Empty directory to put all the files
            Empty_Directory();
            Empty_Directory();

            // Create stored proc file writer
            _swProcedures = new StreamWriter(_folder + @"\\st_proc.sql");

            if (_server_type == "SQL Server")
            {
                // access all objects in the databaseb
                SQL = "SELECT * " +
                    "FROM INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_TYPE = 'BASE TABLE' " +
                    "AND TABLE_NAME NOT IN ('sysdiagrams','dtproperties','Name AutoCorrect Save Failures') " +
                    //"AND TABLE_NAME in ('Merchant_Contact') " +
                    "ORDER BY table_name";

                dt = database_control.ReturnDataTable(SQL);
                foreach (DataRow dr in dt.Rows)
                {
                    _table = dr["table_name"].ToString();

                    // Grab foreign tables
                    _dt_foreign_tables = SQLServer_Get_Foreign_Tables();

                    // Grab foreign joins
                    _dt_foreign_joins = SQLServer_Get_Foreign_Joins();

                    // Grab primary key
                    _dt_primary_keys = SQLServer_Get_Primary_Keys();

                    // Grab columns in the table
                    _dt_columns = SQLServer_Get_Columns(_table);

                    _dt_unique_constraints = SQLServer_Get_Unique_Constraints();

                    // Open thread for c# class
                    _swBaseClass = new StreamWriter(_folder + @"\DAL\Base_" + _table + ".cs");
                    _swBaseDataContractClass = new StreamWriter(_folder + @"\API\Base_" + _table + ".cs");

                    // Write C# inherited class file
                    CSharp_Class_File();
                    CSharp_DataContract_Inherited_Class_File();

                    // Write C# base class file
                    CSharp_Base_Class_SQLServer_Header();
                    CSharp_DataContract_Base_Class_Header();

                    // Write header comments
                    SQLServer_Procedure_Comments();

                    // Inserts
                    _swBaseClass.WriteLine("		#region stored procedures");
                    _swBaseClass.WriteLine("");
                    proc_name = SQLServer_Insert_Create();
                    CSharp_BaseClass_SQLServer_Insert(proc_name);

                    // Updates
                    if (_dt_primary_keys.Rows.Count > 0)
                    {
                        proc_name = SQLServer_Update_Create("PK", "");
                        CSharp_BaseClass_SQLServer_Update(proc_name, "PK", "");
                    }

                    // Deletes
                    if (_dt_primary_keys.Rows.Count > 0)
                    {
                        proc_name = SQLServer_Delete_Create("PK", "");
                        CSharp_BaseClass_SQLServer_Delete(proc_name, "PK", "");
                    }
                    if (_dt_foreign_tables.Rows.Count > 0)
                    {
                        foreach (DataRow dr_foreign_tables in _dt_foreign_tables.Rows)
                        {
                            foreign_queried_table = dr_foreign_tables["table_name"].ToString();
                            proc_name = SQLServer_Delete_Create("FK", foreign_queried_table);
                            CSharp_BaseClass_SQLServer_Delete(proc_name, "FK", foreign_queried_table);
                        }
                    }

                    // REST files
                    CSharp_DataContract_Base_Class_Body();

                    // Selects
                    proc_name = SQLServer_Selects_Create("All", "", "");
                    CSharp_BaseClass_SQLServer_Select(proc_name, "All", "", "");
                    if (_dt_primary_keys.Rows.Count > 0)
                    {
                        proc_name = SQLServer_Selects_Create("PK", "", "");
                        CSharp_BaseClass_SQLServer_Select(proc_name, "PK", "", "");
                    }
                    if (_dt_foreign_tables.Rows.Count > 0)
                    {
                        foreach (DataRow dr_foreign_tables in _dt_foreign_tables.Rows)
                        {
                            foreign_queried_table = dr_foreign_tables["table_name"].ToString();
                            proc_name = SQLServer_Selects_Create("FK", foreign_queried_table, "");
                            CSharp_BaseClass_SQLServer_Select(proc_name, "FK", foreign_queried_table, "");
                        }
                    }
                    if (_dt_unique_constraints.Rows.Count > 0)
                    {
                        foreach (DataRow dr_unique_constraint in _dt_unique_constraints.Rows)
                        {
                            string constraint_name = dr_unique_constraint["constraint_name"].ToString();
                            proc_name = SQLServer_Selects_Create("UC", "", constraint_name);
                            CSharp_BaseClass_SQLServer_Select(proc_name, "UC", "", constraint_name);
                        }
                    }
                    _swBaseClass.WriteLine("		#endregion");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("");

                    // Write C# class footer
                    CSharp_Base_Class_SQLServer_Footer();
                    CSharp_DataContract_Base_Class_Footer();

                    _swBaseClass.Close();
                    _swBaseDataContractClass.Close();
                }
            }

            _swProcedures.Close();

            Create_CallClass();
        }

        #endregion



        #region Private Methods

        private void Empty_Directory()
        {
            try
            {
                DirectoryInfo directory_main = new DirectoryInfo(_folder);
                if (directory_main.Exists)
                {
                    directory_main.Delete(true);
                }
                directory_main.Create();

                DirectoryInfo directory_dal = new DirectoryInfo(_folder + @"\DAL");
                directory_dal.Create();

                DirectoryInfo directory_api = new DirectoryInfo(_folder + @"\API");
                directory_api.Create();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Create_CallClass()
        {
            try
            {
                DataTable dt;
                string sql;

                StreamWriter swProcedures = new StreamWriter(_folder + @"\\class_calling.txt");

                database_control = new DatabaseControl();
                database_control.server = _server;
                database_control.database_name = _database;
                database_control.user_id = _user_name;
                database_control.password = _password;
                sql = "select char(9) + char(9) + char(9) + 'else if (class_object == \"' + name + '\")' + char(13) + char(10) + " +
                    "char(9) + char(9) + char(9) + '{' + char(13) + char(10) + " +
                    "char(9) + char(9) + char(9) + '     " + _namespace + ".' + name + ' dal_' + lower(name) + ' = new " + _namespace + ".' + name + '();' + char(13) + char(10) + " +
                    "char(9) + char(9) + char(9) + '     dt = dal_' + lower(name) + '.SelectAll();' + char(13) + char(10) + " +
                    "char(9) + char(9) + char(9) + '}' as code " +
                    "from sysobjects where type = 'U' " +
                    "and name not in ('sysdiagrams','dtproperties') " +
                    "order by name";

                dt = database_control.ReturnDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    swProcedures.WriteLine(dr["code"].ToString());
                }
                swProcedures.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region SQL Server

        private void SQLServer_CreateDrops(string proc)
        {
            string drop = "";

            drop = "\r\nif exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + proc + "]') and objectproperty(id, N'IsProcedure') = 1) \r\n" +
                "drop procedure [dbo].[" + proc + "]\r\n" +
                "go\r\n";

            _swProcedures.WriteLine(drop);
        }

        private string SQLServer_Insert_Create()
        {
            string statement = "", identity = "";
            string column = "", columns = "", values = "", proc_name, return_values = "";
            string parameters = "", data_length = "", data_type = "", length = "", scale = "", precision = "";

            proc_name = _stored_proc_prefix + _table + "_Insert";

            // Get all parameters
            foreach (DataRow dr in _dt_columns.Rows)
            {
                data_type = dr["data_type"].ToString();
                length = dr["column_length"].ToString();
                scale = dr["numeric_scale"].ToString();
                precision = dr["numeric_precision"].ToString();

                data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                if ((dr["IsIdentity"].ToString() != "1") && (data_type != "timestamp") && (data_type != "uniqueidentifier") && (dr["COLUMN_NAME"].ToString().IndexOf("utc_stamp") == -1))
                {
                    parameters = parameters + "\t@p_" + dr["COLUMN_NAME"].ToString() + " " + dr["DATA_TYPE"].ToString().ToLower() + data_length + "," + "\r\n";
                }
                else if (dr["IsIdentity"].ToString() == "1")
                {
                    identity = dr["COLUMN_NAME"].ToString();
                }
            }

            if (identity != "")
            {
                parameters = parameters + "\t@o_" + identity + " int output," + "\r\n";
            }
            parameters = parameters + "\t@o_error_code int output\r\n";

            // Get all columns for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                if ((dr["IsIdentity"].ToString() != "1") && (data_type != "timestamp"))
                {
                    column = dr["column_name"].ToString();
                    columns = columns + "\t\t[dbo].[" + _table + "].[" + column + "],\r\n";
                }
            }
            columns = columns.Substring(0, columns.Length - 3) + "\r\n";

            // Get all paramters for values
            foreach (DataRow dr in _dt_columns.Rows)
            {
                if ((dr["IsIdentity"].ToString() != "1") && (data_type != "timestamp"))
                {
                    if (dr["column_name"].ToString().IndexOf("utc_stamp") >= 0)
                    {
                        column = "getutcdate()";
                        values = values + "\t\t" + column + ",\r\n";
                    }
                    else if (dr["data_type"].ToString() == "uniqueidentifier")
                    {
                        column = "newid()";
                        values = values + "\t\t" + column + ",\r\n";
                    }
                    else
                    {
                        column = dr["column_name"].ToString();
                        values = values + "\t\t@p_" + column + ",\r\n";
                    }
                }
            }
            values = values.Substring(0, values.Length - 3) + "\r\n";

            // return score and error code
            if (identity != "")
                return_values = "\tselect @o_" + identity + "=scope_identity()\r\n";

            return_values = return_values + "\tselect @o_error_code=@@error\r\n";

            SQLServer_CreateDrops(proc_name);

            statement = "create procedure [dbo].[" + proc_name + "]\r\n" +
                parameters +
                "as" + "\r\n\r\n" +
                "\tinsert into [dbo].[" + _table + "]\r\n" +
                "\t(\r\n" +
                columns +
                "\t)\r\n" +
                "\tvalues\r\n" +
                "\t(\r\n" +
                values +
                "\t)\r\n\r\n" +
                return_values +
                "go" + "\r\n";
            _swProcedures.WriteLine(statement);

            return proc_name;
        }

        private string SQLServer_Update_Create(string type, string foreign_queried_table)
        {
            try
            {
                string statement = "", identity = "", primary_key = "";
                string column = "", columns = "", where = "", proc_name = "", return_values = "";
                string parameters = "", data_length = "", data_type = "", length = "", scale = "", precision = "";

                if (type == "PK")
                {
                    proc_name = _stored_proc_prefix + _table + "_UpdatePK_";
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        proc_name = proc_name + dr["column_name"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }

                // Get all parameters
                foreach (DataRow dr in _dt_columns.Rows)
                {
                    data_type = dr["data_type"].ToString();
                    length = dr["column_length"].ToString();
                    scale = dr["numeric_scale"].ToString();
                    precision = dr["numeric_precision"].ToString();

                    data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                    parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";

                    if (dr["IsIdentity"].ToString() == "1")
                    {
                        identity = dr["column_name"].ToString();
                    }
                }
                parameters = parameters + "\t@o_error_code int output\r\n";

                // Get all columns for insert
                foreach (DataRow dr in _dt_columns.Rows)
                {
                    if ((dr["IsIdentity"].ToString() != "1") && (data_type != "timestamp"))
                    {
                        column = dr["column_name"].ToString();
                        columns = columns + "\t\t[dbo].[" + _table + "].[" + column + "] = @p_" + column + ",\r\n";
                    }
                }
                columns = columns.Substring(0, columns.Length - 3) + "\r\n";

                // Get where clause
                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        primary_key = dr["column_name"].ToString();

                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                    }
                }

                // return score and error code
                return_values = "\tselect @o_error_code=@@error\r\n";

                SQLServer_CreateDrops(proc_name);

                statement = "create procedure [dbo].[" + proc_name + "]\r\n" +
                    parameters +
                    "as" + "\r\n\r\n" +
                    "\tupdate [dbo].[" + _table + "]\r\n" +
                    "\tset\r\n" +
                    columns +
                    where +
                    "\r\n" +
                    return_values +
                    "go" + "\r\n";
                _swProcedures.WriteLine(statement);

                return proc_name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SQLServer_Delete_Create(string type, string foreign_queried_table)
        {
            try
            {
                DataTable dt_foreign_queried_columns = null;
                string statement = "", primary_key = "";
                string where = "", proc_name = "", return_values = "";
                string parameters = "", data_length = "", data_type = "", length = "", scale = "", precision = "";

                if (type == "PK")
                {
                    proc_name = _stored_proc_prefix + _table + "_DeletePK_";
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        proc_name = proc_name + dr["column_name"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }
                else if (type == "FK")
                {
                    proc_name = _stored_proc_prefix + _table + "_DeleteFK_" + foreign_queried_table + "_";
                    dt_foreign_queried_columns = SQLServer_Get_Foreign_Queried_Columns(foreign_queried_table);
                    foreach (DataRow dr_foreign_queried_columns in dt_foreign_queried_columns.Rows)
                    {
                        proc_name = proc_name + dr_foreign_queried_columns["foreign_column"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }

                // Get all parameters
                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        data_type = dr["data_type"].ToString();
                        length = dr["column_length"].ToString();
                        scale = dr["numeric_scale"].ToString();
                        precision = dr["numeric_precision"].ToString();

                        data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                        parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";
                    }
                }
                else if (type == "FK")
                {
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        data_type = dr["data_type"].ToString();
                        length = dr["column_length"].ToString();
                        scale = dr["numeric_scale"].ToString();
                        precision = dr["numeric_precision"].ToString();

                        data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                        parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";
                    }
                }
                parameters = parameters + "\t@o_error_code int output\r\n";

                // Get where clause
                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        primary_key = dr["column_name"].ToString();

                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                    }
                }
                else if (type == "FK")
                {
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + dr["column_name"].ToString() + "] = @p_" + dr["column_name"].ToString() + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + dr["column_name"].ToString() + "] = @p_" + dr["column_name"].ToString() + "\r\n";
                    }
                }

                // return score and error code
                return_values = "\tselect @o_error_code=@@error\r\n";

                SQLServer_CreateDrops(proc_name);

                statement = "create procedure [dbo].[" + proc_name + "]\r\n" +
                    parameters +
                    "as" + "\r\n\r\n" +
                    "\tdelete\r\n" +
                    "\tfrom [dbo].[" + _table + "]\r\n" +
                    where +
                    "\r\n" +
                    return_values +
                    "go" + "\r\n";
                _swProcedures.WriteLine(statement);

                return proc_name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SQLServer_Selects_Create(string type, string foreign_queried_table, string unique_constraint)
        {
            try
            {
                DataTable dt_foreign_queried_columns = null, dt_unique_constraint_columns = null;
                string statement = "";
                string columns = "", primary_key = "";
                string foreign_table = "", foreign_column = "", column = "";
                string tables = "", proc_name = "", where = "", order_by = "";
                string parameters = "", data_length = "", data_type = "", length = "", scale = "", precision = "";
                int self_join = 0;

                if (type == "All")
                {
                    proc_name = _stored_proc_prefix + _table + "_SelectAll";
                }
                else if (type == "PK")
                {
                    proc_name = _stored_proc_prefix + _table + "_SelectPK_";
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        proc_name = proc_name + dr["column_name"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }
                else if (type == "FK")
                {
                    proc_name = _stored_proc_prefix + _table + "_SelectFK_" + foreign_queried_table + "_";
                    dt_foreign_queried_columns = SQLServer_Get_Foreign_Queried_Columns(foreign_queried_table);
                    foreach (DataRow dr_foreign_queried_columns in dt_foreign_queried_columns.Rows)
                    {
                        proc_name = proc_name + dr_foreign_queried_columns["foreign_column"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }
                else if (type == "UC")
                {
                    proc_name = _stored_proc_prefix + _table + "_SelectUnique_";
                    dt_unique_constraint_columns = SQLServer_Get_Unique_Constraints_Columns(unique_constraint);
                    foreach (DataRow dr_unique_constraint_columns in dt_unique_constraint_columns.Rows)
                    {
                        proc_name = proc_name + dr_unique_constraint_columns["column_name"].ToString() + "_";
                    }
                    proc_name = proc_name.Substring(0, proc_name.Length - 1);
                }

                // Get all parameters
                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        data_type = dr["data_type"].ToString();
                        length = dr["column_length"].ToString();
                        scale = dr["numeric_scale"].ToString();
                        precision = dr["numeric_precision"].ToString();

                        data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                        parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";
                    }
                }
                else if (type == "FK")
                {
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        data_type = dr["foreign_data_type"].ToString();
                        length = dr["foreign_column_length"].ToString();
                        scale = dr["foreign_numeric_scale"].ToString();
                        precision = dr["foreign_numeric_precision"].ToString();

                        data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                        parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";
                    }
                }
                else if (type == "UC")
                {
                    foreach (DataRow dr in dt_unique_constraint_columns.Rows)
                    {
                        data_type = dr["data_type"].ToString();
                        length = dr["column_length"].ToString();
                        scale = dr["numeric_scale"].ToString();
                        precision = dr["numeric_precision"].ToString();

                        data_length = SQLServer_Parameter_Length(data_type, length, precision, scale);

                        parameters = parameters + "\t@p_" + dr["column_name"].ToString() + " " + dr["data_type"].ToString().ToLower() + data_length + "," + "\r\n";
                    }
                }
                parameters = parameters + "\t@o_error_code int output\r\n";


                if (type == "All")
                {
                    string test = "";
                }


                // Get all columns for select
                self_join = 0;
                columns = SQLServer_SelectColumns(_table, 0);
                foreach (DataRow dr in _dt_foreign_tables.Rows)
                {
                    foreign_table = dr["table_name"].ToString();
                    if (foreign_table == _table)
                        self_join++;
                    else
                        self_join = 0;
                    columns = columns + SQLServer_SelectColumns(foreign_table, self_join);
                }
                columns = columns.Substring(0, columns.Length - 3) + "\r\n";

                // Get all tables
                self_join = 0;
                tables = "\tfrom [dbo].[" + _table + "]\r\n";
                foreach (DataRow dr in _dt_foreign_joins.Rows)
                {
                    _table = dr["table_name"].ToString();
                    column = dr["column_name"].ToString();
                    foreign_table = dr["foreign_table"].ToString();
                    foreign_column = dr["foreign_column"].ToString();
                    if (foreign_table == _table)
                        self_join++;
                    else
                        self_join = 0;

                    if (self_join == 0)
                        tables = tables + "\tleft outer join [dbo].[" + foreign_table + "] on [dbo].[" + foreign_table + "].[" + foreign_column + "] = [dbo].[" + _table + "].[" + column + "] \r\n";
                    else
                        tables = tables + "\tleft outer join [dbo].[" + foreign_table + "] " + " as " + foreign_table + "_" + self_join.ToString() + " on [dbo].[" + foreign_table + "].[" + foreign_column + "] = [dbo].[" + _table + "].[" + column + "] \r\n";
                }

                // Create where clause
                if (type == "All")
                {
                    where = "";
                }
                else if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        primary_key = dr["column_name"].ToString();

                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                    }
                }
                else if (type == "FK")
                {
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + dr["column_name"].ToString() + "] = @p_" + dr["column_name"].ToString() + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + dr["column_name"].ToString() + "] = @p_" + dr["column_name"].ToString() + "\r\n";
                    }
                }
                else if (type == "UC")
                {
                    foreach (DataRow dr in dt_unique_constraint_columns.Rows)
                    {
                        primary_key = dr["column_name"].ToString();

                        if (where == "")
                            where = "\twhere [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                        else
                            where = where + "\tand [dbo].[" + _table + "].[" + primary_key + "] = @p_" + primary_key + "\r\n";
                    }
                }

                // Create order by
                if (_dt_primary_keys.Rows.Count > 0)
                {
                    order_by = "\torder by\r\n";
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        primary_key = dr["column_name"].ToString();
                        order_by = order_by + "\t\t[dbo].[" + _table + "].[" + primary_key + "] asc,\r\n";
                    }
                    order_by = order_by.Substring(0, order_by.Length - 3) + "\r\n";
                }

                // Create drop section
                SQLServer_CreateDrops(proc_name);

                // Put query together
                statement = "create procedure [dbo].[" + proc_name + "]\r\n" +
                    parameters +
                    "as" + "\r\n\r\n" +
                    "\tselect\r\n" +
                    columns +
                    tables +
                    where +
                    order_by +
                    "\r\n" +
                    "\tselect @o_error_code=@@error\r\n" +
                    "go" + "\r\n";
                _swProcedures.WriteLine(statement);

                return proc_name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable SQLServer_Get_Columns(string table)
        {
            DataTable dt;
            string sql;

            sql = "SELECT Information_Schema.Columns.*, \r\n" +
                "(SELECT COLUMNPROPERTY(OBJECT_ID('" + table + "'), Information_Schema.Columns.column_name, 'IsComputed')) AS IsComputed, 	\r\n" +
                "(SELECT COL_LENGTH('" + table + "', Information_Schema.Columns.column_name)) AS column_length, 	\r\n" +
                "(SELECT COLUMNPROPERTY(OBJECT_ID('" + table + "'), Information_Schema.Columns.column_name, 'IsIdentity')) AS IsIdentity, 	\r\n" +
                "(SELECT COLUMNPROPERTY(OBJECT_ID('" + table + "'), Information_Schema.Columns.column_name, 'IsRowGuidCol')) AS IsRowGuidColumn, \r\n" +
                "(ISNULL( \r\n" +
                "	(SELECT CASE Information_Schema.Table_Constraints.constraint_type WHEN 'PRIMARY KEY' THEN 1 END	\r\n" +
                "	FROM Information_Schema.Key_Column_Usage \r\n" +
                "	INNER JOIN Information_Schema.Table_Constraints \r\n" +
                "		ON Information_Schema.Key_Column_Usage.constraint_name=Information_Schema.Table_Constraints.constraint_name \r\n" +
                "	WHERE Information_Schema.Key_Column_Usage.table_name = '" + table + "' \r\n" +
                "	AND Information_Schema.Key_Column_Usage.column_name = Information_Schema.Columns.column_name \r\n" +
                "	AND Information_Schema.Table_Constraints.constraint_type = 'PRIMARY KEY'), 0)) AS IsPrimaryKey, \r\n" +
                "(ISNULL( \r\n" +
                "	(SELECT  CASE Information_Schema.Table_Constraints.constraint_type WHEN 'FOREIGN KEY' THEN 1 END \r\n" +
                "	FROM Information_Schema.Key_Column_Usage \r\n" +
                "	INNER JOIN Information_Schema.Table_Constraints \r\n" +
                "		ON Information_Schema.Key_Column_Usage.constraint_name=Information_Schema.Table_Constraints.constraint_name \r\n" +
                "	WHERE Information_Schema.Key_Column_Usage.table_name = '" + table + "' \r\n" +
                "	AND Information_Schema.Key_Column_Usage.column_name = Information_Schema.Columns.column_name \r\n" +
                "	AND Information_Schema.Table_Constraints.constraint_type = 'FOREIGN KEY'), 0)) AS IsForeignKey \r\n" +
                "FROM Information_Schema.Columns \r\n" +
                "WHERE table_name = '" + table + "'";
            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private string SQLServer_SelectColumns(string table_name, int self_join)
        {
            DataTable dt;
            string columns = "", column = "";
            int i = 1;

            dt = SQLServer_Get_Columns(table_name);
            foreach (DataRow dr in dt.Rows)
            {
                column = dr["COLUMN_NAME"].ToString();

                if (self_join == 0)
                    columns = columns + "\t\t[dbo].[" + table_name + "].[" + column + "] as " + "[" + table_name + "_" + column + "],\r\n";
                else
                    columns = columns + "\t\t" + table_name + "_" + self_join.ToString() + ".[" + column + "] as " + "[" + table_name + "_" + self_join.ToString() + "_" + column + "],\r\n";
                i++;
            }

            return columns;
        }

        private DataTable SQLServer_Get_Primary_Keys()
        {
            DataTable dt;
            string sql;

            sql = "select c.column_name, c.data_type, col_length(c.table_name, c.column_name) as column_length,\r\n" +
                "c.is_nullable, c.numeric_scale, c.numeric_precision\r\n" +
                "from Information_Schema.Key_Column_Usage a\r\n" +
                "inner join Information_Schema.Table_Constraints b on b.constraint_name=a.constraint_name\r\n" +
                "inner join Information_Schema.Columns c\r\n" +
                "	on c.table_name = a.table_name\r\n" +
                "	and c.column_name = a.column_name\r\n" +
                "where a.table_name = '" + _table + "'\r\n" +
                "and b.constraint_type = 'PRIMARY KEY'\r\n" +
                "order by a.ordinal_position";

            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private DataTable SQLServer_Get_Unique_Constraints()
        {
            DataTable dt;
            string sql;

            sql = "select *\r\n" +
                "from Information_Schema.Table_Constraints a\r\n" +
                "where table_name = '" + _table + "'\r\n" +
                "and constraint_type = 'UNIQUE'";

            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private DataTable SQLServer_Get_Unique_Constraints_Columns(string unique_constraint)
        {
            DataTable dt;
            string sql;

            sql = "select c.column_name, c.data_type, col_length(c.table_name, c.column_name) as column_length,\r\n" +
                "c.is_nullable, c.numeric_scale, c.numeric_precision\r\n" +
                "from Information_Schema.Table_Constraints a\r\n" +
                "inner join Information_Schema.Constraint_Column_Usage b on b.constraint_name = a.constraint_name\r\n" +
                "inner join Information_Schema.Columns c\r\n" +
                "	on c.table_name = a.table_name\r\n" +
                "	and c.column_name = b.column_name\r\n" +
                "where b.constraint_name = '" + unique_constraint + "'\r\n" +
                "order by c.ordinal_position";
            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private DataTable SQLServer_Get_Foreign_Queried_Columns(string foreign_table)
        {
            DataTable dt;
            string sql;

            sql = "select a.table_name, a.column_name, a.data_type, col_length(a.table_name, a.column_name) as column_length,\r\n" +
                "   a.numeric_scale, a.numeric_precision, a.is_nullable,\r\n" +
                "   d.table_name as foreign_table, d.column_name as foreign_column,\r\n" +
                "	e.data_type as foreign_data_type, col_length(e.table_name, e.column_name) as foreign_column_length,\r\n" +
                "	e.numeric_scale as foreign_numeric_scale, e.numeric_precision as foreign_numeric_precision\r\n" +
                "from Information_Schema.Columns a\r\n" +
                "inner join Information_Schema.Key_Column_Usage b\r\n" +
                "	on b.column_name = a.column_name\r\n" +
                "	and b.table_name = a.table_name\r\n" +
                "inner join Information_Schema.Referential_Constraints c on c.constraint_name = b.constraint_name\r\n" +
                "inner join Information_Schema.Key_Column_Usage d on d.constraint_name = c.unique_constraint_name\r\n" +
                "inner join Information_Schema.Columns e\r\n" +
                "	on e.table_name = d.table_name\r\n" +
                "	and e.column_name = d.column_name\r\n" +
                "and d.ordinal_position = b.ordinal_position\r\n" +
                "where a.table_name = '" + _table + "'\r\n" +
                "and d.table_name = '" + foreign_table + "'";
            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private DataTable SQLServer_Get_Foreign_Tables()
        {
            DataTable dt;
            string sql;

            sql = "select distinct c.table_name\r\n" +
                "from Information_Schema.Table_Constraints a\r\n" +
                "inner join Information_Schema.Referential_Constraints b on b.constraint_name = a.constraint_name\r\n" +
                "inner join Information_Schema.Table_Constraints c on c.constraint_name = b.unique_constraint_name\r\n" +
                "where a.constraint_type = 'FOREIGN KEY'\r\n" +
                "and a.table_name = '" + _table + "'";
            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private DataTable SQLServer_Get_Foreign_Joins()
        {
            DataTable dt;
            string sql;

            sql = "select a.table_name, a.column_name, d.table_name as foreign_table, d.column_name as foreign_column\r\n" +
                "from Information_Schema.Columns a\r\n" +
                "inner join Information_Schema.Key_Column_Usage b\r\n" +
                "on b.column_name = a.column_name\r\n" +
                "and b.table_name = a.table_name\r\n" +
                "inner join Information_Schema.Referential_Constraints c on c.constraint_name = b.constraint_name\r\n" +
                "inner join Information_Schema.Key_Column_Usage d\r\n" +
                "on d.constraint_name = c.unique_constraint_name\r\n" +
                "and d.ordinal_position = b.ordinal_position\r\n" +
                "where a.table_name = '" + _table + "'";
            dt = database_control.ReturnDataTable(sql);

            return dt;
        }

        private string SQLServer_Parameter_Length(string data_type, string length, string precision, string scale)
        {
            try
            {
                string data_length = "";

                // Use all non identity columns
                if (data_type == "decimal")
                {
                    data_length = "(" + precision + "," + scale + ")";
                }
                else if ((data_type != "tinyint") && (data_type != "smallint") && (data_type != "int") && (data_type != "bigint") &&
                    (data_type != "smalldatetime") && (data_type != "datetime") && (data_type != "timestamp") &&
                    (data_type != "money") && (data_type != "text") && (data_type != "ntext") && (data_type != "bit") &&
                    (data_type != "uniqueidentifier") && (data_type != "image") && (data_type != "real"))

                    data_length = "(" + length + ")";
                else
                    data_length = "";

                return data_length;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SQLServer_Procedure_Comments()
        {
            try
            {
                _swProcedures.WriteLine("-------------------------");
                _swProcedures.WriteLine("-------------------------");
                _swProcedures.WriteLine("--  " + _table + "  ------");
                _swProcedures.WriteLine("-------------------------");
                _swProcedures.WriteLine("-------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region C# Files

        #region SQL Server Base Class

        #region Header

        private void CSharp_Base_Class_SQLServer_Header()
        {
            try
            {
                _swBaseClass.WriteLine("using System;");
                _swBaseClass.WriteLine("using System.Data;");
                _swBaseClass.WriteLine("using System.Data.SqlTypes;");
                _swBaseClass.WriteLine("using System.Data.SqlClient;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("namespace " + _namespace + ".DAL");
                _swBaseClass.WriteLine("{");
                _swBaseClass.WriteLine("	public class Base_" + _table);
                _swBaseClass.WriteLine("	{");
                _swBaseClass.WriteLine("		#region variables");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("		public GTSoft.CoreDotNet.Database.SQLServer_Connector mainConnect;");
                _swBaseClass.WriteLine("		public SqlConnection mainConnection;");
                _swBaseClass.WriteLine("		public int connection_timeout;");
                _swBaseClass.WriteLine("		public SqlInt32 errorCode;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("		#endregion");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("		#region constructor");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("		public Base_" + _table + "()");
                _swBaseClass.WriteLine("		{");
                _swBaseClass.WriteLine("			mainConnect = new GTSoft.CoreDotNet.Database.SQLServer_Connector(\"" + _xml_data_source + "\");");
                _swBaseClass.WriteLine("			mainConnection = mainConnect.DBConnection;");
                _swBaseClass.WriteLine("			connection_timeout = mainConnect.connection_timeout;");
                _swBaseClass.WriteLine("		}");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("		#endregion");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Insert

        private void CSharp_BaseClass_SQLServer_Insert(string proc_name)
        {
            string input_parameters = "", output_parameters = "", output_results = "";
            string column = "", data_type = "", length = "", precision = "", numeric_scale = "", is_nullable = "";

            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                //if ((data_type.ToLower() == "nvarchar") || (data_type.ToLower() == "nchar"))
                //{
                //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                //}

                if (data_type.ToLower() == "float")
                    precision = "10";
                else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                    precision = dr["NUMERIC_PRECISION"].ToString();
                else
                    precision = "0";

                if (data_type.ToLower() == "text")
                {
                    // Set text length to 10000
                    length = "10000";
                }

                if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                    numeric_scale = dr["NUMERIC_SCALE"].ToString();
                else
                    numeric_scale = "0";

                is_nullable = dr["IS_NULLABLE"].ToString();
                if (is_nullable == "NO")
                    is_nullable = "false";
                else if (is_nullable == "YES")
                    is_nullable = "true";

                if ((dr["IsIdentity"].ToString() != "1") && (data_type.ToLower() != "timestamp") && (data_type.ToLower() != "uniqueidentifier") && (column.IndexOf("utc_stamp") == -1))
                    input_parameters = input_parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
            }

            // Write output parameters
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();
                //if ((data_type.ToLower() == "nvarchar") ||
                //    (data_type.ToLower() == "nchar"))
                //{
                //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                //}
                if (data_type.ToLower() == "float")
                    precision = "10";
                else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                    precision = dr["NUMERIC_PRECISION"].ToString();
                else
                    precision = "0";
                if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                    numeric_scale = dr["NUMERIC_SCALE"].ToString();
                else
                    numeric_scale = "0";

                is_nullable = dr["IS_NULLABLE"].ToString();
                if (is_nullable == "NO")
                    is_nullable = "false";
                else if (is_nullable == "YES")
                    is_nullable = "true";

                if (dr["IsIdentity"].ToString() == "1")
                    output_parameters = output_parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Output, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
            }
            output_parameters = output_parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));\r\n";

            // Get identity result
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                length = dr["column_length"].ToString();
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                precision = dr["NUMERIC_PRECISION_RADIX"].ToString();
                numeric_scale = dr["NUMERIC_SCALE"].ToString();

                if (dr["IsIdentity"].ToString() == "1")
                {
                    output_results = output_results + "				" + column + " = (" + CSharp_Data_Type_SQLServer(data_type.ToLower()) + ")scmCmdToExecute.Parameters[\"@o_" + column + "\"].Value;\r\n";
                }
            }

            _swBaseClass.WriteLine("		public bool Insert()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("			SqlCommand scmCmdToExecute = new SqlCommand();");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandText = \"dbo.[" + proc_name + "]\";");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandType = CommandType.StoredProcedure;");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandTimeout = connection_timeout;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.Write(input_parameters);
            _swBaseClass.Write(output_parameters);
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.Write(output_results);
            _swBaseClass.WriteLine("				errorCode = (SqlInt32)scmCmdToExecute.Parameters[\"@o_error_code\"].Value;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				if(errorCode != 0)");
            _swBaseClass.WriteLine("				{");
            _swBaseClass.WriteLine("					/* throw error */");
            _swBaseClass.WriteLine("					throw new Exception(\"Stored Procedure '" + proc_name + "' reported the ErrorCode: \" + errorCode);");
            _swBaseClass.WriteLine("				}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return true;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
        }

        #endregion

        #region Update

        private void CSharp_BaseClass_SQLServer_Update(string proc_name, string type, string foreign_queried_table)
        {
            try
            {
                try
                {
                    DataTable dt_foreign_queried_columns = null;
                    string input_parameters = "", output_parameters = "", output_results = "";
                    string function_name, parameters = "";
                    string column = "", data_type = "", length = "", precision = "", numeric_scale = "", is_nullable = "";

                    function_name = proc_name.Substring(_table.Length + 4, proc_name.Length - 4 - _table.Length);

                    if (type == "PK")
                    {
                        // Write all parameters for insert
                        foreach (DataRow dr in _dt_columns.Rows)
                        {
                            column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                            data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                            length = dr["column_length"].ToString();

                            //if ((data_type.ToLower() == "nvarchar") || (data_type.ToLower() == "nchar"))
                            //{
                            //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                            //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                            //}

                            if (data_type.ToLower() == "float")
                                precision = "10";
                            else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                                precision = dr["NUMERIC_PRECISION"].ToString();
                            else
                                precision = "0";

                            if (data_type.ToLower() == "text")
                            {
                                // Set text length to 10000
                                length = "10000";
                            }

                            if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                                numeric_scale = dr["NUMERIC_SCALE"].ToString();
                            else
                                numeric_scale = "0";

                            is_nullable = dr["IS_NULLABLE"].ToString();
                            if (is_nullable == "NO")
                                is_nullable = "false";
                            else if (is_nullable == "YES")
                                is_nullable = "true";

                            if (data_type.ToLower() != "timestamp")
                                input_parameters = input_parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                        }
                        //foreach (DataRow dr in _dt_primary_keys.Rows)
                        //{
                        //    column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                        //    data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        //    length = dr["column_length"].ToString();
                        //    if ((data_type.ToLower() == "nvarchar") ||
                        //        (data_type.ToLower() == "nchar"))
                        //    {
                        //        // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //        length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //    }
                        //    if (data_type.ToLower() == "float")
                        //        precision = "10";
                        //    else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                        //        precision = dr["NUMERIC_PRECISION"].ToString();
                        //    else
                        //        precision = "0";
                        //    if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                        //        numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        //    else
                        //        numeric_scale = "0";

                        //    is_nullable = dr["IS_NULLABLE"].ToString();
                        //    if (is_nullable == "NO")
                        //        is_nullable = "false";
                        //    else if (is_nullable == "YES")
                        //        is_nullable = "true";

                        //    parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                        //}
                    }
                    else if (type == "FK")
                    {
                        dt_foreign_queried_columns = SQLServer_Get_Foreign_Queried_Columns(foreign_queried_table);
                        foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                        {
                            column = CheckForCSharpReserveWord(dr["foreign_column"].ToString());
                            data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                            length = dr["column_length"].ToString();
                            //if ((data_type.ToLower() == "nvarchar") ||
                            //    (data_type.ToLower() == "nchar"))
                            //{
                            //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                            //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                            //}
                            if (data_type.ToLower() == "float")
                                precision = "10";
                            else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                                precision = dr["NUMERIC_PRECISION"].ToString();
                            else
                                precision = "0";
                            if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                                numeric_scale = dr["NUMERIC_SCALE"].ToString();
                            else
                                numeric_scale = "0";

                            is_nullable = dr["IS_NULLABLE"].ToString();
                            if (is_nullable == "NO")
                                is_nullable = "false";
                            else if (is_nullable == "YES")
                                is_nullable = "true";

                            parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                        }
                    }
                    //parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));\r\n";
                    output_parameters = output_parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));\r\n";

                    _swBaseClass.WriteLine("		public bool " + function_name + "()");
                    _swBaseClass.WriteLine("		{");
                    _swBaseClass.WriteLine("			SqlCommand scmCmdToExecute = new SqlCommand();");
                    _swBaseClass.WriteLine("			scmCmdToExecute.CommandText = \"dbo.[" + proc_name + "]\";");
                    _swBaseClass.WriteLine("			scmCmdToExecute.CommandType = CommandType.StoredProcedure;");
                    _swBaseClass.WriteLine("			scmCmdToExecute.CommandTimeout = connection_timeout;");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("			scmCmdToExecute.Connection = mainConnection;");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("			try");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.Write(input_parameters);
                    _swBaseClass.Write(output_parameters);
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("				/* open database connection */");
                    _swBaseClass.WriteLine("				mainConnection.Open();");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("				/* execute query */");
                    _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
                    _swBaseClass.WriteLine("				errorCode = (SqlInt32)scmCmdToExecute.Parameters[\"@o_error_code\"].Value;");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("				if(errorCode != 0)");
                    _swBaseClass.WriteLine("				{");
                    _swBaseClass.WriteLine("					/* throw error */");
                    _swBaseClass.WriteLine("					throw new Exception(\"Stored Procedure '" + proc_name + "' reported the ErrorCode: \" + errorCode);");
                    _swBaseClass.WriteLine("				}");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("				return true;");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("			catch(Exception ex)");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
                    _swBaseClass.WriteLine("				throw ex;");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("			finally");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				mainConnection.Close();");
                    _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("		}");
                    _swBaseClass.WriteLine("");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete

        private void CSharp_BaseClass_SQLServer_Delete(string proc_name, string type, string foreign_queried_table)
        {
            try
            {
                DataTable dt_foreign_queried_columns = null;
                string function_name, parameters = "";
                string column = "", data_type = "", length = "", precision = "", numeric_scale = "", is_nullable = "";

                function_name = proc_name.Substring(_table.Length + 4, proc_name.Length - 4 - _table.Length);

                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                        data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        length = dr["column_length"].ToString();
                        //if ((data_type.ToLower() == "nvarchar") ||
                        //    (data_type.ToLower() == "nchar"))
                        //{
                        //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //}
                        if (data_type.ToLower() == "float")
                            precision = "10";
                        else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                            precision = dr["NUMERIC_PRECISION"].ToString();
                        else
                            precision = "0";
                        if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                            numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        else
                            numeric_scale = "0";

                        is_nullable = dr["IS_NULLABLE"].ToString();
                        if (is_nullable == "NO")
                            is_nullable = "false";
                        else if (is_nullable == "YES")
                            is_nullable = "true";

                        parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                    }
                }
                else if (type == "FK")
                {
                    dt_foreign_queried_columns = SQLServer_Get_Foreign_Queried_Columns(foreign_queried_table);
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        column = CheckForCSharpReserveWord(dr["column_name"].ToString());
                        data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        length = dr["column_length"].ToString();
                        //if ((data_type.ToLower() == "nvarchar") ||
                        //    (data_type.ToLower() == "nchar"))
                        //{
                        //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //}
                        if (data_type.ToLower() == "float")
                            precision = "10";
                        else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                            precision = dr["NUMERIC_PRECISION"].ToString();
                        else
                            precision = "0";
                        if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                            numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        else
                            numeric_scale = "0";

                        is_nullable = dr["IS_NULLABLE"].ToString();
                        if (is_nullable == "NO")
                            is_nullable = "false";
                        else if (is_nullable == "YES")
                            is_nullable = "true";

                        parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                    }
                }
                parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));\r\n";


                _swBaseClass.WriteLine("		public bool " + function_name + "()");
                _swBaseClass.WriteLine("		{");
                _swBaseClass.WriteLine("			SqlCommand scmCmdToExecute = new SqlCommand();");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandText = \"dbo.[" + proc_name + "]\";");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandType = CommandType.StoredProcedure;");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandTimeout = connection_timeout;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("			scmCmdToExecute.Connection = mainConnection;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("			try");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.Write(parameters);
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				/* open database connection */");
                _swBaseClass.WriteLine("				mainConnection.Open();");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				/* execute query */");
                _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
                _swBaseClass.WriteLine("				errorCode = (SqlInt32)scmCmdToExecute.Parameters[\"@o_error_code\"].Value;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				if(errorCode != 0)");
                _swBaseClass.WriteLine("				{");
                _swBaseClass.WriteLine("					/* throw error */");
                _swBaseClass.WriteLine("					throw new Exception(\"Stored Procedure '" + proc_name + "' reported the ErrorCode: \" + errorCode);");
                _swBaseClass.WriteLine("				}");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				return true;");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("			catch(Exception ex)");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
                _swBaseClass.WriteLine("				throw ex;");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("			finally");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				mainConnection.Close();");
                _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("		}");
                _swBaseClass.WriteLine("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Select

        private void CSharp_BaseClass_SQLServer_Select(string proc_name, string type, string foreign_queried_table, string unique_constraint)
        {
            try
            {
                DataTable dt_foreign_queried_columns = null, dt_unique_constraint_columns = null; ;
                string function_name, parameters = "";
                string column = "", data_type = "", length = "", precision = "", numeric_scale = "", is_nullable = "";

                function_name = proc_name.Substring(_table.Length + 4, proc_name.Length - 4 - _table.Length);

                if (type == "PK")
                {
                    foreach (DataRow dr in _dt_primary_keys.Rows)
                    {
                        column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                        data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        length = dr["column_length"].ToString();
                        //if ((data_type.ToLower() == "nvarchar") ||
                        //    (data_type.ToLower() == "nchar"))
                        //{
                        //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //}
                        if (data_type.ToLower() == "float")
                            precision = "10";
                        else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                            precision = dr["NUMERIC_PRECISION"].ToString();
                        else
                            precision = "0";
                        if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                            numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        else
                            numeric_scale = "0";

                        is_nullable = dr["IS_NULLABLE"].ToString();
                        if (is_nullable == "NO")
                            is_nullable = "false";
                        else if (is_nullable == "YES")
                            is_nullable = "true";

                        parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                    }
                }
                else if (type == "FK")
                {
                    dt_foreign_queried_columns = SQLServer_Get_Foreign_Queried_Columns(foreign_queried_table);
                    foreach (DataRow dr in dt_foreign_queried_columns.Rows)
                    {
                        column = CheckForCSharpReserveWord(dr["column_name"].ToString());
                        data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        length = dr["column_length"].ToString();
                        //if ((data_type.ToLower() == "nvarchar") ||
                        //    (data_type.ToLower() == "nchar"))
                        //{
                        //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //}
                        if (data_type.ToLower() == "float")
                            precision = "10";
                        else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                            precision = dr["NUMERIC_PRECISION"].ToString();
                        else
                            precision = "0";
                        if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                            numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        else
                            numeric_scale = "0";

                        is_nullable = dr["IS_NULLABLE"].ToString();
                        if (is_nullable == "NO")
                            is_nullable = "false";
                        else if (is_nullable == "YES")
                            is_nullable = "true";

                        parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                    }
                }
                else if (type == "UC")
                {
                    dt_unique_constraint_columns = SQLServer_Get_Unique_Constraints_Columns(unique_constraint);
                    foreach (DataRow dr in dt_unique_constraint_columns.Rows)
                    {
                        column = CheckForCSharpReserveWord(dr["column_name"].ToString());
                        data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                        length = dr["column_length"].ToString();
                        //if ((data_type.ToLower() == "nvarchar") ||
                        //    (data_type.ToLower() == "nchar"))
                        //{
                        //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                        //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        //}
                        if (data_type.ToLower() == "float")
                            precision = "10";
                        else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                            precision = dr["NUMERIC_PRECISION"].ToString();
                        else
                            precision = "0";
                        if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                            numeric_scale = dr["NUMERIC_SCALE"].ToString();
                        else
                            numeric_scale = "0";

                        is_nullable = dr["IS_NULLABLE"].ToString();
                        if (is_nullable == "NO")
                            is_nullable = "false";
                        else if (is_nullable == "YES")
                            is_nullable = "true";

                        parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column + "));\r\n";
                    }
                }
                parameters = parameters + "				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));\r\n";

                _swBaseClass.WriteLine("		public DataTable " + function_name + "()");
                _swBaseClass.WriteLine("		{");
                _swBaseClass.WriteLine("			SqlCommand scmCmdToExecute = new SqlCommand();");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandText = \"dbo.[" + proc_name + "]\";");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandType = CommandType.StoredProcedure;");
                _swBaseClass.WriteLine("			scmCmdToExecute.CommandTimeout = connection_timeout;");
                _swBaseClass.WriteLine("			DataTable toReturn = new DataTable(\"" + _table + "\");");
                _swBaseClass.WriteLine("			SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("			scmCmdToExecute.Connection = mainConnection;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("			try");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.Write(parameters);
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				/* open database connection */");
                _swBaseClass.WriteLine("				mainConnection.Open();");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				/* execute query */");
                _swBaseClass.WriteLine("				adapter.Fill(toReturn);");
                _swBaseClass.WriteLine("				errorCode = (SqlInt32)scmCmdToExecute.Parameters[\"@o_error_code\"].Value;");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				if(errorCode != 0)");
                _swBaseClass.WriteLine("				{");
                _swBaseClass.WriteLine("					/* throw error */");
                _swBaseClass.WriteLine("					throw new Exception(\"Stored Procedure '" + proc_name + "' reported the ErrorCode: \" + errorCode);");
                _swBaseClass.WriteLine("				}");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("				return toReturn;");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("			catch(Exception ex)");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
                _swBaseClass.WriteLine("				throw ex;");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("			finally");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				mainConnection.Close();");
                _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
                _swBaseClass.WriteLine("				adapter.Dispose();");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("		}");
                _swBaseClass.WriteLine("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Footer

        private void CSharp_Base_Class_SQLServer_Footer()
        {
            try
            {
                string column = "", data_type = "", is_nullable = "", foreign_table = ""; ;

                _swBaseClass.WriteLine("		#region property declarations");
                _swBaseClass.WriteLine("");

                foreach (DataRow dr in _dt_columns.Rows)
                {
                    column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                    data_type = CSharp_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                    is_nullable = dr["IS_NULLABLE"].ToString();

                    _swBaseClass.WriteLine("		public " + data_type + " " + column + " { get; set; }");
                }
                _swBaseClass.WriteLine("");

                if (_dt_foreign_tables.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dt_foreign_tables.Rows)
                    {
                        foreign_table = dr["table_name"].ToString();

                        _swBaseClass.WriteLine("		public " + foreign_table + " " + foreign_table.ToLower() + "_dal { get; set; }");
                    }
                    _swBaseClass.WriteLine("");
                }
                _swBaseClass.WriteLine("		#endregion");
                _swBaseClass.WriteLine("	}");
                _swBaseClass.WriteLine("}");
                _swBaseClass.WriteLine("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Miscellaneous

        private void CSharp_Class_File()
        {
            StreamWriter swClass;

            swClass = new StreamWriter(_folder + @"\DAL\" + _table + ".cs");

            if (server_type == "SQL Server")
            {
                swClass.WriteLine("using System;");
                swClass.WriteLine("using System.Data;");
                swClass.WriteLine("using System.Data.SqlTypes;");
                swClass.WriteLine("using System.Data.SqlClient;");
                swClass.WriteLine("");
                swClass.WriteLine("namespace " + _namespace + ".DAL");
                swClass.WriteLine("{");
                swClass.WriteLine("	public class " + _table + " : " + _namespace + ".DAL.Base_" + _table);
                swClass.WriteLine("	{");
                swClass.WriteLine("		#region constructors");
                swClass.WriteLine("");
                swClass.WriteLine("		public " + _table + "()");
                swClass.WriteLine("		{");
                swClass.WriteLine("		}");
                swClass.WriteLine("");
                swClass.WriteLine("		#endregion");
                swClass.WriteLine("");
                swClass.WriteLine("");
                swClass.WriteLine("\t\t#region public methods");
                swClass.WriteLine("");
                swClass.WriteLine("");
                swClass.WriteLine("\t\t#endregion");
                swClass.WriteLine("");
                swClass.WriteLine("");
                swClass.WriteLine("\t\t#region properties");
                swClass.WriteLine("");
                swClass.WriteLine("");
                swClass.WriteLine("\t\t#endregion");
                swClass.WriteLine("\t}");
                swClass.WriteLine("}");
            }

            swClass.Close();
        }

        private string CSharp_Data_Type_SQLServer(string sql_data_type)
        {
            string class_data_type = "";

            switch (sql_data_type)
            {
                case "bigint":
                    class_data_type = "SqlInt64";
                    break;
                case "binary":
                    class_data_type = "SqlBinary";
                    break;
                case "bit":
                    class_data_type = "SqlBoolean";
                    break;
                case "char":
                    class_data_type = "SqlString";
                    break;
                case "datetime":
                    class_data_type = "SqlDateTime";
                    break;
                case "decimal":
                    class_data_type = "SqlDecimal";
                    break;
                case "float":
                    class_data_type = "SqlDouble";
                    break;
                case "image":
                    class_data_type = "SqlBinary";
                    break;
                case "int":
                    class_data_type = "SqlInt32";
                    break;
                case "money":
                    class_data_type = "SqlMoney";
                    break;
                case "nchar":
                    class_data_type = "SqlString";
                    break;
                case "ntext":
                    class_data_type = "SqlString";
                    break;
                case "nvarchar":
                    class_data_type = "SqlString";
                    break;
                case "numeric":
                    class_data_type = "SqlDecimal";
                    break;
                case "real":
                    class_data_type = "SqlSingle";
                    break;
                case "smalldatetime":
                    class_data_type = "SqlDateTime";
                    break;
                case "smallint":
                    class_data_type = "SqlInt16";
                    break;
                case "smallmoney":
                    class_data_type = "SqlMoney";
                    break;
                case "sql_variant":
                    class_data_type = "Object";
                    break;
                case "sysname":
                    class_data_type = "SqlString";
                    break;
                case "text":
                    class_data_type = "SqlString";
                    break;
                case "timestamp":
                    class_data_type = "SqlBinary";
                    break;
                case "tinyint":
                    class_data_type = "SqlByte";
                    break;
                case "varbinary":
                    class_data_type = "SqlBinary";
                    break;
                case "varchar":
                    class_data_type = "SqlString";
                    break;
                case "uniqueidentifier":
                    class_data_type = "SqlGuid";
                    break;
            }

            return class_data_type;
        }

        private string CSharp_Cast_Data_Type_SQLServer(string sql_data_type)
        {
            string class_data_type = "";

            switch (sql_data_type)
            {
                case "bigint":
                    class_data_type = "BigInt";
                    break;
                case "binary":
                    class_data_type = "Binary";
                    break;
                case "bit":
                    class_data_type = "Bit";
                    break;
                case "char":
                    class_data_type = "Char";
                    break;
                case "datetime":
                    class_data_type = "DateTime";
                    break;
                case "decimal":
                    class_data_type = "Decimal";
                    break;
                case "float":
                    class_data_type = "Float";
                    break;
                case "image":
                    class_data_type = "Image";
                    break;
                case "int":
                    class_data_type = "Int";
                    break;
                case "money":
                    class_data_type = "Money";
                    break;
                case "nchar":
                    class_data_type = "NChar";
                    break;
                case "ntext":
                    class_data_type = "NText";
                    break;
                case "nvarchar":
                    class_data_type = "NVarChar";
                    break;
                case "numeric":
                    class_data_type = "Decimal";
                    break;
                case "real":
                    class_data_type = "Real";
                    break;
                case "smalldatetime":
                    class_data_type = "SmallDateTime";
                    break;
                case "smallint":
                    class_data_type = "SmallInt";
                    break;
                case "smallmoney":
                    class_data_type = "SmallMoney";
                    break;
                case "sql_variant":
                    class_data_type = "Variant";
                    break;
                case "sysname":
                    class_data_type = "VarChar";
                    break;
                case "text":
                    class_data_type = "Text";
                    break;
                case "timestamp":
                    class_data_type = "Timestamp";
                    break;
                case "tinyint":
                    class_data_type = "TinyInt";
                    break;
                case "varbinary":
                    class_data_type = "VarBinary";
                    break;
                case "varchar":
                    class_data_type = "VarChar";
                    break;
                case "uniqueidentifier":
                    class_data_type = "UniqueIdentifier";
                    break;
            }

            return class_data_type;
        }

        private void CSharp_BaseClass_SQLServer_CountFK(string table, string proc_name, string column_use)
        {
            string column = "", data_type = "", length = "", precision = "", numeric_scale = "", is_nullable = "";

            _swBaseClass.WriteLine("		public int CountFK_" + column_use + "()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("			SqlCommand	scmCmdToExecute = new SqlCommand();");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandText = \"dbo.[" + proc_name + "]\";");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandType = CommandType.StoredProcedure;");
            _swBaseClass.WriteLine("			scmCmdToExecute.CommandTimeout = connection_timeout;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();
                //if ((data_type.ToLower() == "nvarchar") ||
                //    (data_type.ToLower() == "nchar"))
                //{
                //    // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
                //    length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
                //}
                if (data_type.ToLower() == "float")
                    precision = "10";
                else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
                    precision = dr["NUMERIC_PRECISION"].ToString();
                else
                    precision = "0";
                if (dr["NUMERIC_SCALE"].ToString().Length > 0)
                    numeric_scale = dr["NUMERIC_SCALE"].ToString();
                else
                    numeric_scale = "0";

                is_nullable = dr["IS_NULLABLE"].ToString();
                if (is_nullable == "NO")
                    is_nullable = "false";
                else if (is_nullable == "YES")
                    is_nullable = "true";

                if (column == column_use)
                {
                    _swBaseClass.WriteLine("				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@p_" + column_use + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Input, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, " + column_use + "));");
                    _swBaseClass.WriteLine("				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_count_" + column_use + "\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, _count_" + column_use + "));");
                }
            }
            _swBaseClass.WriteLine("				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_error_code\", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, \"\", DataRowVersion.Proposed, errorCode));");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteScalar();");
            _swBaseClass.WriteLine("				_count_" + column_use + " = (SqlInt32)scmCmdToExecute.Parameters[\"@o_count_" + column_use + "\"].Value;");
            _swBaseClass.WriteLine("				errorCode = (SqlInt32)scmCmdToExecute.Parameters[\"@o_error_code\"].Value;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				if(errorCode != 0)");
            _swBaseClass.WriteLine("				{");
            _swBaseClass.WriteLine("					/* throw error */");
            _swBaseClass.WriteLine("					throw new Exception(\"Stored Procedure '" + proc_name + "' reported the ErrorCode: \" + errorCode);");
            _swBaseClass.WriteLine("				}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return Convert.ToInt32(_count_" + column_use + ".Value);");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        #endregion

        #region C# Class Creator Functions

        private string CheckForCSharpReserveWord(string input)
        {
            try
            {
                string output = "";
                string reserve_words = "|class|base|out|internal|";
                int index;

                index = reserve_words.IndexOf("|" + input + "|");
                if (index != -1)
                    output = input + "__";
                else
                    output = input;

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #endregion



        #region C# Web Service Files

        #region Header

        private void CSharp_DataContract_Base_Class_Header()
        {
            try
            {
                _swBaseDataContractClass.WriteLine("using System;");
                _swBaseDataContractClass.WriteLine("using System.Collections.Generic;");
                _swBaseDataContractClass.WriteLine("using System.Linq;");
                _swBaseDataContractClass.WriteLine("using System.Runtime.Serialization;");
                _swBaseDataContractClass.WriteLine("using System.ServiceModel;");
                _swBaseDataContractClass.WriteLine("using System.ServiceModel.Web;");
                _swBaseDataContractClass.WriteLine("using System.Text;");
                _swBaseDataContractClass.WriteLine("");
                _swBaseDataContractClass.WriteLine("namespace " + _namespace + ".API");
                _swBaseDataContractClass.WriteLine("{");
                _swBaseDataContractClass.WriteLine("	[DataContract]");
                _swBaseDataContractClass.WriteLine("	public class Base_" + _table);
                _swBaseDataContractClass.WriteLine("	{");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Body

        private void CSharp_DataContract_Base_Class_Body()
        {
            string column = "", data_type = "", length = "", foreign_table="";

            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_DataContract_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                _swBaseDataContractClass.WriteLine("		[DataMember(EmitDefaultValue=false)]");
                _swBaseDataContractClass.WriteLine("		public " + data_type + " " + column + " { get; set; }");
                _swBaseDataContractClass.WriteLine("");
            }

            foreach (DataRow dr in _dt_foreign_tables.Rows)
            {
                foreign_table = dr["table_name"].ToString();

                _swBaseDataContractClass.WriteLine("		[DataMember(EmitDefaultValue=false)]");
                _swBaseDataContractClass.WriteLine("		public " + foreign_table + " " + foreign_table.ToLower() + "_obj { get; set; }");
                _swBaseDataContractClass.WriteLine("");
            }


        }

        #endregion

        #region Footer

        private void CSharp_DataContract_Base_Class_Footer()
        {
            try
            {
                _swBaseDataContractClass.WriteLine("	}");
                _swBaseDataContractClass.WriteLine("}");
                _swBaseDataContractClass.WriteLine("");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Miscellaneous

        private void CSharp_DataContract_Inherited_Class_File()
        {
            StreamWriter swClass;

            swClass = new StreamWriter(_folder + @"\API\" + _table + ".cs");

            swClass.WriteLine("using System;");
            swClass.WriteLine("using System.Collections.Generic;");
            swClass.WriteLine("using System.Linq;");
            swClass.WriteLine("using System.Runtime.Serialization;");
            swClass.WriteLine("using System.ServiceModel;");
            swClass.WriteLine("using System.ServiceModel.Web;");
            swClass.WriteLine("using System.Text;");
            swClass.WriteLine("");
            swClass.WriteLine("namespace " + _namespace + ".API");
            swClass.WriteLine("{");
            swClass.WriteLine("	[DataContract]");
            swClass.WriteLine("	public class " + _table + " : " + _namespace + ".API.Base_" + _table);
            swClass.WriteLine("	{");
            swClass.WriteLine("	}");
            swClass.WriteLine("}");
            swClass.WriteLine("");

            swClass.Close();
        }

        private string CSharp_DataContract_Cast_Data_Type_SQLServer(string sql_data_type)
        {
            string class_data_type = "";

            switch (sql_data_type)
            {
                case "bigint":
                    class_data_type = "long";
                    break;
                case "binary":
                    class_data_type = "Binary";
                    break;
                case "bit":
                    class_data_type = "bool";
                    break;
                case "char":
                    class_data_type = "string";
                    break;
                case "datetime":
                    class_data_type = "DateTime";
                    break;
                case "decimal":
                    class_data_type = "Decimal";
                    break;
                case "float":
                    class_data_type = "double";
                    break;
                case "image":
                    class_data_type = "Image";
                    break;
                case "int":
                    class_data_type = "int";
                    break;
                case "money":
                    class_data_type = "decimal";
                    break;
                case "nchar":
                    class_data_type = "string";
                    break;
                case "ntext":
                    class_data_type = "string";
                    break;
                case "nvarchar":
                    class_data_type = "string";
                    break;
                case "numeric":
                    class_data_type = "Decimal";
                    break;
                case "real":
                    class_data_type = "Real";
                    break;
                case "smalldatetime":
                    class_data_type = "SmallDateTime";
                    break;
                case "smallint":
                    class_data_type = "SmallInt";
                    break;
                case "smallmoney":
                    class_data_type = "SmallMoney";
                    break;
                case "sql_variant":
                    class_data_type = "Variant";
                    break;
                case "sysname":
                    class_data_type = "VarChar";
                    break;
                case "text":
                    class_data_type = "string";
                    break;
                case "timestamp":
                    class_data_type = "Timestamp";
                    break;
                case "tinyint":
                    class_data_type = "TinyInt";
                    break;
                case "varbinary":
                    class_data_type = "Byte[]";
                    break;
                case "varchar":
                    class_data_type = "string";
                    break;
                case "uniqueidentifier":
                    class_data_type = "string";
                    break;
            }

            return class_data_type;
        }

        #endregion

        #endregion



        #region Access

        private void CSharp_Base_Class_Access_Header()
        {
            string data_type = "";

            _swBaseClass.WriteLine("using System;");
            _swBaseClass.WriteLine("using System.Data;");
            _swBaseClass.WriteLine("using System.Data.OleDb;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("namespace " + _namespace + "");
            _swBaseClass.WriteLine("{");
            _swBaseClass.WriteLine("	public class Base_" + _table);
            _swBaseClass.WriteLine("	{");
            _swBaseClass.WriteLine("		#region Class Member Declarations");
            _swBaseClass.WriteLine("			protected GTSoft.CoreDotNet.Database.SQLServer_Connector _mainConnect;");
            _swBaseClass.WriteLine("			protected OleDbConnection			_mainConnection;");
            _swBaseClass.WriteLine("			protected int				_errorCode;");
            foreach (DataRow dr in _dt_columns.Rows)
            {
                data_type = GetCSaccessDataType(dr["DATA_TYPE"].ToString());
                _swBaseClass.WriteLine("			protected " + data_type + "				_" + dr["COLUMN_NAME"].ToString() + ";");

                if (dr["IsForeignKey"].ToString() == "1")
                {
                    _swBaseClass.WriteLine("			protected int" + "				_count_" + dr["COLUMN_NAME"].ToString() + ";");
                    _swBaseClass.WriteLine("			protected " + data_type + "				_" + dr["COLUMN_NAME"].ToString() + "_Old;");
                }
            }
            _swBaseClass.WriteLine("		#endregion");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("		public Base_" + _table + "()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("			_mainConnect = new GTSoft.CoreDotNet.Database.SQLServer_Connector(\"" + _xml_data_source + "\");");
            _swBaseClass.WriteLine("			_mainConnection = _mainConnect.DBConnection;");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
        }

        private void CSharp_Base_Class_Access_Footer(string table)
        {
            string column = "", data_type = "", is_nullable = "";


            _swBaseClass.WriteLine("		#region Class Property Declarations");
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = GetCSaccessDataType(dr["DATA_TYPE"].ToString());
                is_nullable = dr["IS_NULLABLE"].ToString();

                _swBaseClass.WriteLine("		public " + data_type + " " + column);
                _swBaseClass.WriteLine("		{");
                _swBaseClass.WriteLine("			get");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				return _" + column + ";");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("			set");
                _swBaseClass.WriteLine("			{");
                _swBaseClass.WriteLine("				_" + column + " = value;");
                _swBaseClass.WriteLine("			}");
                _swBaseClass.WriteLine("		}");
                _swBaseClass.WriteLine("");
                _swBaseClass.WriteLine("");

                if (dr["IsForeignKey"].ToString() == "1")
                {
                    _swBaseClass.WriteLine("		public " + data_type + " " + column + "_Old");
                    _swBaseClass.WriteLine("		{");
                    _swBaseClass.WriteLine("			get");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				return _" + column + "_Old;");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("			set");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				_" + column + "_Old = value;");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("		}");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("		public " + data_type + " count_" + column);
                    _swBaseClass.WriteLine("		{");
                    _swBaseClass.WriteLine("			get");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				return _count_" + column + ";");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("			set");
                    _swBaseClass.WriteLine("			{");
                    _swBaseClass.WriteLine("				_count_" + column + " = value;");
                    _swBaseClass.WriteLine("			}");
                    _swBaseClass.WriteLine("		}");
                    _swBaseClass.WriteLine("");
                    _swBaseClass.WriteLine("");
                }
            }
            _swBaseClass.WriteLine("		#endregion");
            _swBaseClass.WriteLine("	}");
            _swBaseClass.WriteLine("}");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_SelectAllFK_Access(string table, string primary_table, string proc_name, string column_use, string columns, string primary_key, string foreign_key)
        {
            string sql_string = "";

            _swBaseClass.WriteLine("\t\tpublic DataTable SelectAllFK_" + primary_table + "_" + column_use + "()");
            _swBaseClass.WriteLine("\t\t{");
            _swBaseClass.WriteLine("\t\t\tOleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("\t\t\tDataTable toReturn = new DataTable(\"" + table + "\");");
            _swBaseClass.WriteLine("\t\t\tOleDbDataAdapter adapter = new OleDbDataAdapter(scmCmdToExecute);");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"SELECT \" +");
            _swBaseClass.WriteLine(columns);
            sql_string = sql_string + "\t\t\t\t\t\t\t\"FROM " + table + " \" +\r\n";
            sql_string = sql_string + "\t\t\t\t\t\t\t\"LEFT OUTER JOIN \" +\r\n" +
                        "\t\t\t\t\t\t\t\t\"[" + primary_table + "] \" +\r\n" +
                        "\t\t\t\t\t\t\t\"ON \" +\r\n" +
                        "\t\t\t\t\t\t\t\t\"[" + primary_table + "].[" + primary_key + "] = [" + table + "].[" + foreign_key + "] \";\r\n";

            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");

            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return toReturn;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("				adapter.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_SelectFK_Access(string table, string primary_table, string proc_name, string column_use, string columns, string primary_key, string foreign_key)
        {
            string sql_string = "";

            _swBaseClass.WriteLine("\t\tpublic DataTable SelectFK_" + primary_table + "_" + column_use + "()");
            _swBaseClass.WriteLine("\t\t{");
            _swBaseClass.WriteLine("\t\t\tOleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("\t\t\tDataTable toReturn = new DataTable(\"" + table + "\");");
            _swBaseClass.WriteLine("\t\t\tOleDbDataAdapter adapter = new OleDbDataAdapter(scmCmdToExecute);");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"SELECT \" +");
            _swBaseClass.WriteLine(columns);
            sql_string = sql_string + "\t\t\t\t\t\t\t\"FROM " + table + " \" +\r\n";
            sql_string = sql_string + "\t\t\t\t\t\t\t\"LEFT OUTER JOIN \" +\r\n" +
                        "\t\t\t\t\t\t\t\t\"[" + primary_table + "] \" +\r\n" +
                        "\t\t\t\t\t\t\t\"ON \" +\r\n" +
                        "\t\t\t\t\t\t\t\t\"[" + primary_table + "].[" + primary_key + "] = [" + table + "].[" + foreign_key + "] \" +\r\n" +
                        "\t\t\t\t\t\t\t\"WHERE \" +\r\n" +
                        "\t\t\t\t\t\t\t\t\"[" + table + "].[" + foreign_key + "] = \" + " + foreign_key + ";\r\n";


            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");

            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return toReturn;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("				adapter.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_SelectPK_Access(string table, string proc_name)
        {
            string column = "", data_type = "", length = "";
            string sql_string = "";

            _swBaseClass.WriteLine("\t\tpublic DataTable SelectPK()");
            _swBaseClass.WriteLine("\t\t{");
            _swBaseClass.WriteLine("\t\t\tOleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("\t\t\tDataTable toReturn = new DataTable(\"" + table + "\");");
            _swBaseClass.WriteLine("\t\t\tOleDbDataAdapter adapter = new OleDbDataAdapter(scmCmdToExecute);");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"SELECT \" +");
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                sql_string = sql_string + "                                      \"" + column + ",\" + " + "\r\n";
            }
            sql_string = sql_string + "                                      \"FROM " + table + " \" +\r\n";

            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() == "1")
                    sql_string = sql_string + "                                      \"WHERE " + column + "\" + _" + column + ".ToString();\r\n";
            }

            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");

            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return toReturn;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("				adapter.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_SelectAll_Access(string table, string proc_name)
        {
            string column = "", data_type = "", length = "";
            string sql_string = "";

            _swBaseClass.WriteLine("\t\tpublic DataTable SelectAll()");
            _swBaseClass.WriteLine("\t\t{");
            _swBaseClass.WriteLine("\t\t\tOleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("\t\t\tDataTable toReturn = new DataTable(\"" + table + "\");");
            _swBaseClass.WriteLine("\t\t\tOleDbDataAdapter adapter = new OleDbDataAdapter(scmCmdToExecute);");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"SELECT \" +");
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                sql_string = sql_string + "                                      \"" + column + ",\" + " + "\r\n";
            }
            sql_string = sql_string + "                                      \"FROM " + table + "\";\r\n";

            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");

            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return toReturn;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("				adapter.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_UpdatePK_Access(string table, string proc_name)
        {
            string column = "", data_type = "", length = "";
            string sql_string = "";

            _swBaseClass.WriteLine("		public bool Update()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("                     OleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"UPDATE " + table + "\" +");
            _swBaseClass.WriteLine("                                      \"SET\" +");
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() != "1")
                    sql_string = sql_string + "                                      \"" + column + "\" + _" + column + " + \",\" + " + "\r\n";
            }
            sql_string = sql_string.Substring(0, sql_string.Length - 9) + "\r\n";

            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() == "1")
                    sql_string = sql_string + "                                      \"WHERE " + column + "\" + _" + column + ";\r\n";
            }

            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return true;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private string GetCSaccessDataType(string sql_data_type)
        {
            string class_data_type = "";

            switch (sql_data_type)
            {
                case "bigint":
                    class_data_type = "long";
                    break;
                case "binary":
                    class_data_type = "SqlBinary";
                    break;
                case "bit":
                    class_data_type = "bool";
                    break;
                case "char":
                    class_data_type = "string";
                    break;
                case "datetime":
                    class_data_type = "DateTime";
                    break;
                case "decimal":
                    class_data_type = "float";
                    break;
                case "float":
                    class_data_type = "float";
                    break;
                case "image":
                    class_data_type = "SqlBinary";
                    break;
                case "int":
                    class_data_type = "int";
                    break;
                case "money":
                    class_data_type = "SqlMoney";
                    break;
                case "nchar":
                    class_data_type = "string";
                    break;
                case "ntext":
                    class_data_type = "string";
                    break;
                case "nvarchar":
                    class_data_type = "string";
                    break;
                case "numeric":
                    class_data_type = "SqlDecimal";
                    break;
                case "real":
                    class_data_type = "SqlSingle";
                    break;
                case "smalldatetime":
                    class_data_type = "DateTime";
                    break;
                case "smallint":
                    class_data_type = "int";
                    break;
                case "smallmoney":
                    class_data_type = "SqlMoney";
                    break;
                case "sql_variant":
                    class_data_type = "Object";
                    break;
                case "sysname":
                    class_data_type = "SqlString";
                    break;
                case "text":
                    class_data_type = "string";
                    break;
                case "timestamp":
                    class_data_type = "SqlBinary";
                    break;
                case "tinyint":
                    class_data_type = "SqlByte";
                    break;
                case "varbinary":
                    class_data_type = "SqlBinary";
                    break;
                case "varchar":
                    class_data_type = "string";
                    break;
                case "uniqueidentifier":
                    class_data_type = "SqlGuid";
                    break;
            }

            return class_data_type;
        }

        private void CSharp_BaseClass_Access_Insert(string table, string proc_name)
        {
            string column = "", data_type = "", length = "";
            string sql_string = "";

            _swBaseClass.WriteLine("		public bool Insert()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("                     OleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"INSERT INTO " + table + "\" +");
            _swBaseClass.WriteLine("                                      \"(\" +");
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() != "1")
                    sql_string = sql_string + "                                      \"" + column + ",\" + " + "\r\n";
            }
            sql_string = sql_string.Substring(0, sql_string.Length - 7) + ") \" +\r\n";
            sql_string = sql_string + "                                      \"VALUES (\" + " + "\r\n";
            // Write all parameters for insert
            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() != "1")
                {
                    if ((data_type == "NVarChar") || (data_type == "Text"))
                    {
                        sql_string = sql_string + "                                      \"'\" + _" + column + ".Replace(\"'\",\"''\") + \"',\" + " + "\r\n";
                    }
                    else
                    {
                        sql_string = sql_string + "                                      _" + column + ".ToString() + \",\" + " + "\r\n";
                    }
                }
            }
            sql_string = sql_string.Substring(0, sql_string.Length - 9) + "\r\n" + "                                      \")\";";
            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");

            //// Write identity parameter
            //foreach (DataRow dr in _dt_columns.Rows)
            //{
            //    column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
            //    data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
            //    length = dr["column_length"].ToString();
            //    if ((data_type.ToLower() == "nvarchar") ||
            //        (data_type.ToLower() == "nchar"))
            //    {
            //        // set length to length mentioned in CHARACTER_MAXIMUM_LENGTH
            //        length = dr["CHARACTER_MAXIMUM_LENGTH"].ToString();
            //    }
            //    if (data_type.ToLower() == "float")
            //        precision = "10";
            //    else if (dr["NUMERIC_PRECISION"].ToString().Length > 0)
            //        precision = dr["NUMERIC_PRECISION"].ToString();
            //    else
            //        precision = "0";
            //    if (dr["NUMERIC_SCALE"].ToString().Length > 0)
            //        numeric_scale = dr["NUMERIC_SCALE"].ToString();
            //    else
            //        numeric_scale = "0";

            //    is_nullable = dr["IS_NULLABLE"].ToString();
            //    if (is_nullable == "NO")
            //        is_nullable = "false";
            //    else if (is_nullable == "YES")
            //        is_nullable = "true";

            //    if (dr["IsPrimaryKey"].ToString() == "1")
            //        _swBaseClass.WriteLine("				scmCmdToExecute.Parameters.Add(new SqlParameter(\"@o_" + column + "\", SqlDbType." + data_type + ", " + length + ", ParameterDirection.Output, " + is_nullable + ", " + precision + ", " + numeric_scale + ", \"\", DataRowVersion.Proposed, _" + column + "));");
            //}


            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return true;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        private void WriteBaseClass_DeletePK_Access(string table, string proc_name)
        {
            string column = "", data_type = "", length = "";
            string sql_string = "";

            _swBaseClass.WriteLine("		public bool DeletePK()");
            _swBaseClass.WriteLine("		{");
            _swBaseClass.WriteLine("                     OleDbCommand	scmCmdToExecute = new OleDbCommand();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			try");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine(" 			        string sql_string = \"\";");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("	    		        sql_string = \"DELETE FROM " + table + " \" +");
            _swBaseClass.WriteLine("                                      \"WHERE \" +");

            foreach (DataRow dr in _dt_columns.Rows)
            {
                column = CheckForCSharpReserveWord(dr["COLUMN_NAME"].ToString());
                data_type = CSharp_Cast_Data_Type_SQLServer(dr["DATA_TYPE"].ToString());
                length = dr["column_length"].ToString();

                if (dr["IsPrimaryKey"].ToString() == "1")
                    sql_string = sql_string + "                                      \"" + column + " = \" + _" + column + ".ToString();\r\n";
            }

            _swBaseClass.WriteLine(sql_string);
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandText = sql_string;");
            _swBaseClass.WriteLine("			        scmCmdToExecute.CommandType = CommandType.Text;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("			        scmCmdToExecute.Connection = mainConnection;");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* open database connection */");
            _swBaseClass.WriteLine("				mainConnection.Open();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				/* execute query */");
            _swBaseClass.WriteLine("				scmCmdToExecute.ExecuteNonQuery();");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("				return true;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			catch(Exception ex)");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				/* some error occured. Bubble it to caller and encapsulate Exception object */");
            _swBaseClass.WriteLine("				throw ex;");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("			finally");
            _swBaseClass.WriteLine("			{");
            _swBaseClass.WriteLine("				mainConnection.Close();");
            _swBaseClass.WriteLine("				scmCmdToExecute.Dispose();");
            _swBaseClass.WriteLine("			}");
            _swBaseClass.WriteLine("		}");
            _swBaseClass.WriteLine("");
            _swBaseClass.WriteLine("");
        }

        #endregion




        #region Public Properties

        public string folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }

        public string xml_data_source
        {
            get
            {
                return _xml_data_source;
            }
            set
            {
                _xml_data_source = value;
            }
        }

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

        public string database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }

        public string user_name
        {
            get
            {
                return _user_name;
            }
            set
            {
                _user_name = value;
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

        public string namespace_used
        {
            get
            {
                return _namespace;
            }
            set
            {
                _namespace = value;
            }
        }

        public string server_type
        {
            get
            {
                return _server_type;
            }
            set
            {
                _server_type = value;
            }
        }

        public string stored_proc_prefix
        {
            get
            {
                return _stored_proc_prefix;
            }
            set
            {
                _stored_proc_prefix = value;
            }
        }

        #endregion


    }
}
