using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GTSoft.CoreDotNet
{
    public class LogFile
    {
        #region Class Member Declarations

        protected bool _successful;
        protected string _error_text;
        protected string _file_name;
        protected int _line_count;
        private FileInfo fi;

        #endregion



        // Constructor
        public LogFile(string input_file)
		{
            _file_name = input_file;

            if (_file_name != "")
            {
                _line_count = 0;
                fi = new FileInfo(_file_name);
            }
        }



        #region Public Methods

        public void Write_Tracing(string value)
        {
            try
            {
                string directory = "";
                StreamWriter sw = null;

                CoreDotNet.Utilities utilities = new Utilities();
                directory = utilities.Get_Application_Directory("System");

                Check_Directory_Exists(directory);

                sw = new StreamWriter(directory + "\\digital_mailer.trc", true);
                sw.WriteLine(DateTime.Now.ToString().PadRight(25) + value);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Write_Error(string value)
        {
            try
            {
                string directory = "";
                StreamWriter sw = null;

                CoreDotNet.Utilities utilities = new Utilities();
                directory = utilities.Get_Application_Directory("System");

                Check_Directory_Exists(directory);

                sw = new StreamWriter(directory + "\\error.txt", true);
                sw.WriteLine(DateTime.Now.ToString().PadRight(25) + value);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Write_Log(string full_file_name, string value, bool write_date)
        {
            try
            {
                FileInfo fileinfo = null;
                StreamWriter sw = null;
                string directory="", file_name = "";

                fileinfo = new FileInfo(full_file_name);

                // Get directory and file name
                file_name = fileinfo.Name;
                directory = fileinfo.DirectoryName;

                Check_Directory_Exists(directory);

                sw = new StreamWriter(full_file_name, true);

                if (write_date == true)
                    value = DateTime.Now.ToString().PadRight(25) + value;

                sw.WriteLine(value);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Set_Valid_Directory(string directory, string directory_type)
        {
            try
            {
                try
                {
                    directory = directory + "\\" + directory_type;
                    Check_Directory_Exists(directory);

                    return directory;
                }
                catch
                {
                    string system_folder = "";

                    CoreDotNet.Utilities utilities = new Utilities();
                    system_folder = utilities.Get_Application_Directory("System");

                    Check_Directory_Exists(system_folder);

                    return system_folder;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Count_Lines_In_File()
        {
            try
            {
                StreamReader sr = new StreamReader(fi.FullName);

                if (fi.Exists)
                {
                    try
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine().Trim();
                            _line_count++;
                        }
                    }
                    finally
                    {
                        if (sr != null) sr.Close();
                    }
                }
                else
                {
                    _line_count = -1;
                    _successful = false;
                    _error_text = "File does not exist";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Private Methods

        private void Check_Directory_Exists(string directory)
        {
            try
            {
                DirectoryInfo directoryinfo = null;
                directoryinfo = new DirectoryInfo(directory);
                if (directoryinfo.Exists == false)
                    directoryinfo.Create();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                
        #endregion



        #region Class Property Declarations

        public string file_name
        {
            get
            {
                return _file_name;
            }
            set
            {
                _file_name = value;
            }
        }

        public int line_count
        {
            get
            {
                return _line_count;
            }
            set
            {
                _line_count = value;
            }
        }

        #endregion

    }
}
