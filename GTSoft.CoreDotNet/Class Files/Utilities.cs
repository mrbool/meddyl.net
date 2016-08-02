using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceProcess;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;

namespace GTSoft.CoreDotNet
{
    public class Utilities
    {

        #region Constructor

        public Utilities()
        {
        }

        #endregion


        #region Public Methods

        public string Get_Application_Directory(string subfolders)
        {
            try
            {
                //System.Reflection.Module[] modules = System.Reflection.Assembly.GetExecutingAssembly().GetModules();
                //string directory = System.IO.Path.GetDirectoryName(modules[0].FullyQualifiedName);

                 string directory = AppDomain.CurrentDomain.BaseDirectory.ToString();

                // Remove first slash if passed in
                if (subfolders.IndexOf(@"\") == 0)
                    subfolders = subfolders.Substring(1);

                // If there is not slash at the end, then add one
                if (subfolders.LastIndexOf(@"\") != subfolders.Length - 1)
                    subfolders = subfolders + @"\";

                directory = directory + subfolders;

                return directory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Windows_Service_Exists(string service_name)
        {
            try
            {   
                ServiceController[] services = ServiceController.GetServices();  
                
                foreach (ServiceController service in services)  
                {
                    if (service.ServiceName == service_name)      
                        return true;  
                }  
                
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Alpha_Numeric_Standardization(string name)
        {
            try
            {
                string clean_name = "";
                string alpha_num = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ12345676890";
                int index;
                string substring;

                for (int i = 0; i < name.Length; i++)
                {
                    substring = name.Substring(i, 1);
                    index = alpha_num.IndexOf(substring);
                    if (index == -1)
                        clean_name = clean_name + "_";
                    else
                        clean_name = clean_name + name.Substring(i, 1);
                }

                return clean_name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SSIS_Connection_String(string connection_name)
        {
            try
            {
                string connection_string;

                // Create connectionstring for SSIS
                CoreDotNet.Database.SQLServer_Connector connection = new CoreDotNet.Database.SQLServer_Connector(connection_name);
                connection_string = connection.DBConnection.ConnectionString + "Provider=SQLNCLI.1;Auto Translate=False;";

                return connection_string;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsValidEmailAddress(string email)
        {
            int first_at, last_at;
            int last_period;

            if (email == null)
            {
                return false;
            }

            first_at = email.IndexOf('@');
            last_at = email.LastIndexOf('@');
            last_period = email.LastIndexOf('.');

            if ((first_at > 0) && (last_at == first_at) && (first_at < (email.Length - 1)) && (last_period > first_at))
            {
                // address is ok regarding the single @ sign
                return (true);
            }
            else
            {
                return false;
            }
        }

        public static string Get_Client_IP()
        {
            string ip = "";

            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                ip = stream.ReadToEnd();
            }

            int first = ip.IndexOf("Address: ") + 9;
            int last = ip.LastIndexOf("</body>");

            ip = ip.Substring(first, last - first);

            return ip;
        }

        public static string Get_Day_Name(int day_of_week)
        {
            string day_name="";

            switch (day_of_week)
            {
                case 1:
                    day_name = "Sunday";
                    break;
                case 2:
                    day_name = "Monday";
                    break;
                case 3:
                    day_name = "Tuesday";
                    break;
                case 4:
                    day_name = "Wednesday";
                    break;
                case 5:
                    day_name = "Thursday";
                    break;
                case 6:
                    day_name = "Friday";
                    break;
                case 7:
                    day_name = "Saturday";
                    break;
            }

            return day_name;
        }

        public static bool IsNumeric(string input)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static bool IsDate(string input_date)
        {
            bool is_date = true;

            try
            {
                DateTime dt = DateTime.Parse(input_date);
            }
            catch
            {
                is_date = false;
            }

            return is_date;
        }

        public DataTable CheckBoxList_To_DataTable(CheckBoxList cbl, string id, string type)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Clear();
                dt.Columns.Add(id);

                for (int i = 0; i < cbl.Items.Count; i++)
                {
                    if (cbl.Items[i].Selected)
                    {
                        DataRow dr = dt.NewRow();
                        dr[id] = cbl.Items[i].Value;
                        dt.Rows.Add(dr);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int NthIndexOf(string s, string match, int occurence)
        {
            int i = 1;
            int index = 0;

            while (i <= occurence && (index = s.IndexOf(match, index + 1)) != -1)
            {
                if (i == occurence)
                    return index;

                i++;
            }

            return -1;
        }

        public static DataTable Convert_List_To_DataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }


        public static string Mask_Credit_Card(string card_number)
        {
            string mask = "";
            int char_count;

            char_count = card_number.Length;

            if(card_number.Length > 4)
            {
                mask = string.Concat("".PadLeft(char_count - 4, '*'), card_number.Substring(char_count - 4));
            }
            else
            {
                mask = "****";
            }

            return mask;
        }

        #endregion


        #region Private Methods

        #endregion

    }
}
