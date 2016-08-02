using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using Rebex;
using Rebex.Net;

using System.Resources;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace GTSoft.CoreDotNet
{
    public class SFTP
    {
        #region Class Member Declarations

        protected bool _successful;
        protected string _error_text;
        private Sftp _sftp;
        protected string _site, _user_name, _password, _local_file, _local_directory, _remote_file, _remote_directory, _move_to_file;
        protected int _port;

        #endregion

        


        #region Contsructor

        public SFTP()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        


        #region Public Methods

        public void Connect()
        {
            try
            {
                if (_sftp == null)
                {
                    _sftp = new Sftp();

                    _sftp.Timeout = 30000;

                    _sftp.Connect(_site, _port);
                    _sftp.Login(_user_name, _password);
                }

                _successful = true;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public void Disconnect()
        {
            try
            {
                _sftp.Disconnect();

                _successful = true;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public void Download()
        {
            try
            {
                _sftp.TransferType = SftpTransferType.Binary;
                _sftp.GetFile(_remote_file, _local_directory + _local_file);

                _successful = true;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public void Upload()
        {
            try
            {
                _sftp.TransferType = SftpTransferType.Binary;
                _sftp.PutFile(_local_directory + _local_file, _remote_file);

                _successful = true;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public void Move()
        {
            try
            {
                _sftp.TransferType = SftpTransferType.Binary;
                _sftp.Rename(_remote_file, _move_to_file);

                _successful = true;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public void Change_Directory()
        {
            try
            {
                _sftp.ChangeDirectory(_remote_directory);
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        public DataTable GetList()
        {
            try
            {
                SftpItemCollection file_list;
                DataTable dt = new DataTable("File_List");
                DataColumn dc_file_name = new DataColumn();
                DataColumn dc_file_date = new DataColumn();

                dc_file_name.DataType = System.Type.GetType("System.String");
                dc_file_name.ColumnName = "file_name";
                dt.Columns.Add(dc_file_name);

                dc_file_date.DataType = System.Type.GetType("System.DateTime");
                dc_file_date.ColumnName = "file_date";
                dt.Columns.Add(dc_file_date);

                file_list = _sftp.GetList();

                for (int i = 0; i < file_list.Count; i++)
                {
                    SftpItem item = file_list[i];

                    if (item.IsFile)
                    {
                        DataRow dr = dt.NewRow();

                        dr["file_name"] = item.Name;
                        dr["file_date"] = item.Modified;

                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message.ToString();

                throw ex;
            }
        }

        #endregion



        
        #region Private Methods

        #endregion




        #region Class Property Declarations

        public string site
        {
            get
            {
                return _site;
            }
            set
            {
                _site = value;
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

        public string local_file
        {
            get
            {
                return _local_file;
            }
            set
            {
                _local_file = value;
            }
        }

        public string local_directory
        {
            get
            {
                return _local_directory;
            }
            set
            {
                _local_directory = value;
            }
        }

        public string remote_file
        {
            get
            {
                return _remote_file;
            }
            set
            {
                _remote_file = value;
            }
        }

        public string move_to_file
        {
            get
            {
                return _move_to_file;
            }
            set
            {
                _move_to_file = value;
            }
        }

        public string remote_directory
        {
            get
            {
                return _remote_directory;
            }
            set
            {
                _remote_directory = value;
            }
        }

        public int port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public bool successful
        {
            get
            {
                return _successful;
            }
            set
            {
                _successful = value;
            }
        }

        public string error_text
        {
            get
            {
                return _error_text;
            }
            set
            {
                _error_text = value;
            }
        }

        #endregion




    }
}
