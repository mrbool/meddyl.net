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
    public class FTP
    {
        #region Class Member Declarations

        protected bool _successful;
        protected string _error_text;
        private Ftp _ftp;
        protected string _site, _user_name, _password, _local_file, _local_directory, _remote_file, _remote_directory, _move_to_file;
        protected int _port;

        #endregion




        #region Contsructor

        public FTP()
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
                if (_ftp == null)
                {
                    _ftp = new Ftp();

                    _ftp.Timeout = 30000;

                    _ftp.Connect(_site, _port);
                    _ftp.Login(_user_name, _password);
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
                _ftp.Disconnect();

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
                _ftp.TransferType = FtpTransferType.Binary;
                _ftp.GetFile(_remote_file, _local_directory + _local_file);

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
                _ftp.TransferType = FtpTransferType.Binary;
                _ftp.PutFile(_local_directory + _local_file, _remote_file);

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
                _ftp.TransferType = FtpTransferType.Binary;
                _ftp.Rename(_remote_file, _move_to_file);

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
                _ftp.ChangeDirectory(_remote_directory);
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
