using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Globalization;


namespace GTSoft.Core_Console
{
    public class Create_REST_Files
    {
        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        #region Constructor

        public Create_REST_Files()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        

        #region Public Methods

        public void Process()
        {
            try
            {
                string model_files = rest_folder + @"\Data\Class_Files\";
                string model_base_files = rest_folder + @"\Data\Base_Class_Files\";
                
                string java_directory_base = output_folder + @"\java\model\object\";
                string ios_directory_base = output_folder + @"\ios\model\base\";
                
                Empty_Directory(java_directory_base);
                Empty_Directory(java_directory_base);

                Empty_Directory(ios_directory_base);
                Empty_Directory(ios_directory_base);

                DirectoryInfo di = new DirectoryInfo(model_files);
                foreach (FileInfo fi in di.GetFiles())
                {
                    string file_name = fi.Name;
                    string object_name = fi.Name.Replace(".cs", "");

                    DataTable dt_columns = new DataTable(object_name);
                    dt_columns.Columns.Add("data_type", typeof(string));
                    dt_columns.Columns.Add("property", typeof(string));

                    using (StreamReader sr_class = new StreamReader(fi.FullName))
                    {
                        if (File.Exists(model_base_files + "Base_" + file_name))
                        {
                            using (StreamReader sr_base_class = new StreamReader(model_base_files + "Base_" + file_name))
                            {
                                Get_Properites(sr_base_class, ref dt_columns);
                            }
                        }

                        Get_Properites(sr_class, ref dt_columns);
                    }

                    IOS_Model_Base_Write(ios_directory_base, dt_columns);
                    Java_Model_Object_Write(java_directory_base, dt_columns);
                }

                //Java_Model_Response_Write();
                Java_Model_Interface_Write();
                Java_Controller_Write();

                IOS_Model_Interface_Write();
                IOS_Controller_Write();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        

        #region Private Methods

        private void Empty_Directory(string directory)
        {
            try
            {

                if (Directory.Exists(directory))
                    Directory.Delete(directory, true);

                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Get_Properites(StreamReader sr, ref DataTable dt)
        {
            try
            {
                string read_line;

                while ((read_line = sr.ReadLine()) != null)
                {
                    read_line = read_line.Trim();
                    if (read_line.Length >= 11)
                    {
                        if (read_line.Substring(0, 11) == "[DataMember")
                        {
                            read_line = sr.ReadLine();

                            read_line = read_line.Substring(read_line.IndexOf("public") + 7, read_line.Length - (read_line.IndexOf("public") + 7));

                            string data_type = read_line.Substring(0, read_line.IndexOf(" "));
                            read_line = read_line.Substring(read_line.IndexOf(" ") + 1, read_line.Length - (read_line.IndexOf(" ") + 1));

                            string property = read_line.Substring(0, read_line.IndexOf(" "));

                            dt.Rows.Add(data_type, property);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private DataTable Get_Objects()
        {
            string model_directory = rest_folder + @"\Data\Class_Files\";

            DataTable dt = new DataTable();
            dt.Columns.Add("object", typeof(string));

            DirectoryInfo di = new DirectoryInfo(model_directory);
            foreach (FileInfo fi in di.GetFiles())
            {
                dt.Rows.Add(fi.Name.Replace(".cs", ""));
            }

            return dt;
        }

        private DataTable Get_Parameters(string argument_list)
        {
            string parameter_name = "", data_type = "";

            DataTable dt = new DataTable();
            dt.Columns.Add("parameter_name", typeof(string));
            dt.Columns.Add("data_type", typeof(string));

            int count = argument_list.Split(',').Length;

            for (int i = 1; i <= count; i++)
            {
                data_type = argument_list.Substring(0, argument_list.IndexOf(" "));
                if (argument_list.IndexOf(",") != -1)
                {
                    parameter_name = argument_list.Substring(argument_list.IndexOf(" ") + 1, argument_list.IndexOf(",") - (argument_list.IndexOf(" ") + 1));
                    argument_list = argument_list.Substring(argument_list.IndexOf(",") + 2, argument_list.Length - (argument_list.IndexOf(",") + 2));
                }
                else
                {
                    parameter_name = argument_list.Substring(argument_list.IndexOf(" ") + 1, argument_list.Length - (argument_list.IndexOf(" ") + 1));
                }

                dt.Rows.Add(parameter_name, data_type);
            }

            return dt;
        }

        private bool Is_Array(string data_type)
        {
            try
            {
                if(data_type.Length < 5)
                {
                    return false;
                }
                else
                {
                    if (data_type.Substring(0, 5) == "List<")
                        return true;
                    else
                        return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void IOS_Model_Base_Write(string directory, DataTable dt)
        {
            try
            {
                string object_name = dt.TableName;
                string file = directory + object_name;

                using (StreamWriter sw = new StreamWriter(file + ".h"))
                {
                    sw.WriteLine("#import <Foundation/Foundation.h>");
                    sw.WriteLine("#import \"BaseClass.h\"");
                    sw.WriteLine("");
                    
                    foreach (DataRow dr in dt.Rows)
                    {
                        string data_type = dr["data_type"].ToString();

                        if (IOS_Is_Custom_Object(data_type))
                        {
                            if(!Is_Array(data_type))
                            {
                                sw.WriteLine("@class " + dr["data_type"].ToString() + ";");
                            }
                        }
                    }
                                        
                    sw.WriteLine("");
                    sw.WriteLine("@interface " + object_name + " : BaseClass");
                    sw.WriteLine("{");
                    sw.WriteLine("");
                    sw.WriteLine("}");
                    sw.WriteLine("");

                    if(object_name == "Credit_Card")
                    {

                    }

                    foreach (DataRow dr in dt.Rows)
                    {
                        sw.WriteLine("@property " + IOS_Property_Type(dr["data_type"].ToString()) + " " + IOS_Data_Type_Format(dr["data_type"].ToString()) + " *" + dr["property"].ToString() + ";");
                    }
                    
                    sw.WriteLine("");
                    sw.WriteLine("@end");

                    sw.Close();
                }

                using (StreamWriter sw = new StreamWriter(file + ".m"))
                {
                    sw.WriteLine("#import \"" + object_name + ".h\"");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (IOS_Is_Custom_Object(dr["data_type"].ToString()))
                        {
                            string data_type = dr["data_type"].ToString();
                            if(!Is_Array(data_type))
                                sw.WriteLine("#import \"" + data_type + ".h\"");
                        }
                    }
                    sw.WriteLine("");
                    sw.WriteLine("@implementation " + object_name);
                    sw.WriteLine("");

                    foreach (DataRow dr in dt.Rows)
                    {
                        sw.WriteLine("@synthesize " + dr["property"].ToString() + ";");
                    }

                    int custom_object_count = IOS_Custom_Object_Count(dt);

                    if (custom_object_count > 0)
                    {
                        sw.WriteLine("");
                        sw.WriteLine("+ (NSDictionary *) specificationProperties");
                        sw.WriteLine("{");
                        sw.WriteLine("\treturn @{");

                        int index = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            string data_type = dr["data_type"].ToString();

                            if (IOS_Is_Custom_Object(data_type))
                            {
                                index++;

                                if (!Is_Array(data_type))
                                {
                                    if (index < custom_object_count)
                                        sw.WriteLine("\t\t@\"" + dr["property"].ToString().ToLower() + "\" : [" + data_type + " class],");
                                    else
                                        sw.WriteLine("\t\t@\"" + dr["property"].ToString().ToLower() + "\" : [" + data_type + " class]");
                                }
                            }
                        }

                        sw.WriteLine("\t\t};");
                        sw.WriteLine("}");
                    }

                    sw.WriteLine("");
                    sw.WriteLine("@end");

                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IOS_Model_Interface_Write()
        {
            try
            {
                string directory = output_folder + @"\ios\model\rest_interface\";
                string service_directory = rest_folder + @"\Services\";
                string interface_directory = rest_folder + @"\Interfaces\";
                DataTable dt_objects = Get_Objects();

                Empty_Directory(directory);
                Empty_Directory(directory);

                DirectoryInfo di = new DirectoryInfo(interface_directory);
                foreach (FileInfo fi in di.GetFiles())
                {
                    string service_name = fi.Name.Replace(".cs", "").Substring(1, fi.Name.Length - 4);

                    string file = directory + @"REST_" + service_name + ".h";
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        sw.WriteLine("#import <Foundation/Foundation.h>");
                        sw.WriteLine("#import \"JSONResponse.h\"");
                        sw.WriteLine("#import \"JSONSuccessfulResponse.h\"");
                        sw.WriteLine("#import \"JSONErrorResponse.h\"");

                        foreach (DataRow dr in dt_objects.Rows)
                        {
                            sw.WriteLine("#import \"" + dr["object"].ToString() + ".h\"");
                        }

                        sw.WriteLine("");
                        sw.WriteLine("@interface REST_" + service_name + " : NSObject");
                        sw.WriteLine("");
                        sw.WriteLine("@property (nonatomic, strong) NSString *web_service;");
                        sw.WriteLine("");
                        sw.WriteLine("-(id) initWithService:(NSString *)str;");
                        sw.WriteLine("");

                        using (StreamReader sr = new StreamReader(fi.FullName))
                        {
                            string read_line;
                            string function_definition;
                            string function_name;
                            string parameter_list_in = "";
                            string parameter_list_out = "";
                            string parameter_name = "";
                            string parameter_data_type = "";
                            bool is_first_parameter = true;
                            DataTable dt_parameters;

                            while ((read_line = sr.ReadLine()) != null)
                            {
                                read_line = read_line.Trim();

                                if ((read_line.IndexOf("JSONResponse") != -1) && (read_line.IndexOf("[return: ") == -1))
                                {
                                    function_definition = read_line.Substring(read_line.IndexOf("JSONResponse") + 13, read_line.Length - (read_line.IndexOf("JSONResponse") + 13));

                                    parameter_list_in = "";
                                    parameter_list_out = "";
                                    is_first_parameter = true;
                                    if (function_definition.IndexOf("(") != -1)
                                    {
                                        function_name = function_definition.Substring(0, function_definition.IndexOf("(")).Trim();

                                        parameter_list_in = function_definition.Substring(function_definition.IndexOf("("), function_definition.Length - (function_definition.IndexOf("("))).Replace("(", "").Replace(")", "").Replace(";", "");

                                        if (parameter_list_in != "")
                                        {
                                            dt_parameters = Get_Parameters(parameter_list_in);
                                            foreach (DataRow dr in dt_parameters.Rows)
                                            {
                                                parameter_name = dr["parameter_name"].ToString();
                                                parameter_data_type = IOS_Data_Type_Format(dr["data_type"].ToString());

                                                if (is_first_parameter)
                                                    parameter_list_out = parameter_list_out + ":(" + parameter_data_type + "*)" + parameter_name;
                                                else
                                                    parameter_list_out = parameter_list_out + " " + parameter_name + ":(" + parameter_data_type + "*)" + parameter_name;

                                                is_first_parameter = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        function_name = function_definition;
                                    }

                                    sw.WriteLine("-(void) " + function_name + parameter_list_out + " withResponse:(void(^)(JSONResponse *response)) completionBlock;");
                                }
                            }

                            sr.Close();
                        }

                        sw.WriteLine("");
                        sw.WriteLine("@end");
                    }

                    file = directory + @"REST_" + service_name + ".m";
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        sw.WriteLine("#import \"REST_" + service_name + ".h\"");
                        sw.WriteLine("");
                        sw.WriteLine("@implementation REST_" + service_name);
                        sw.WriteLine("");
                        sw.WriteLine("@synthesize web_service;");
                        sw.WriteLine("");
                        sw.WriteLine("-(id)initWithService:(NSString *)web_service_config");
                        sw.WriteLine("{");
                        sw.WriteLine("\tself = [super init];");
                        sw.WriteLine("\tif(self)");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tself.web_service = web_service_config;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("\treturn self;");
                        sw.WriteLine("}");
                        sw.WriteLine("");
                        sw.WriteLine("");

                        using (StreamReader sr = new StreamReader(fi.FullName))
                        {
                            string read_line;
                            string call_type = "";
                            string uri = "";
                            string function_definition="";
                            string function_name = "";
                            string parameter_list_in = "";
                            string parameter_list_out = "";
                            string parameter_name = "";
                            string parameter_data_type = "";
                            bool is_first_parameter = true;
                            DataTable dt_parameters=null;
                            string return_type = "";
                            string return_value = "";
                            //string array_value = "";
                           // string array_return_type = "";

                            while ((read_line = sr.ReadLine()) != null)
                            {
                                call_type = "";
                                uri = "";
                                function_definition = "";
                                function_name = "";
                                parameter_name = "";
                                parameter_data_type = "";
                                return_type = "";
                                return_value = "";
                                bool return_is_array = false;

                                read_line = read_line.Trim();
                                if (read_line.IndexOf("[OperationContract]") != -1)
                                {
                                    read_line = sr.ReadLine().Trim();
                                    read_line = sr.ReadLine().Trim();

                                    if (read_line.Trim().Replace("[", "").Substring(0, 6) == "WebGet")
                                    {
                                        call_type = "GET";
                                    }
                                    else
                                    {
                                        call_type = "POST";
                                    }

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();

                                        if (read_line.Substring(0, read_line.Trim().IndexOf(" ")) == "UriTemplate")
                                        {
                                            uri = read_line.Substring(read_line.IndexOf("\""), read_line.Length - read_line.IndexOf("\"")).Replace("\"", "").Replace(")]", "");
                                            break;
                                        }
                                    }

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();
                                        if (read_line.Substring(0, read_line.Trim().IndexOf(" ")) == "JSONResponse")
                                        {
                                            function_definition = read_line.Substring(read_line.IndexOf(" ") + 1, read_line.Length - (read_line.IndexOf(" ") + 1)).Replace(";", "");

                                            parameter_list_in = "";
                                            parameter_list_out = "";
                                            is_first_parameter = true;
                                            if (function_definition.IndexOf("(") != -1)
                                            {
                                                function_name = function_definition.Substring(0, function_definition.IndexOf("(")).Trim();

                                                parameter_list_in = function_definition.Substring(function_definition.IndexOf("("), function_definition.Length - (function_definition.IndexOf("("))).Replace("(", "").Replace(")", "").Replace(";", "");

                                                if (parameter_list_in != "")
                                                {
                                                    dt_parameters = Get_Parameters(parameter_list_in);
                                                    foreach (DataRow dr in dt_parameters.Rows)
                                                    {
                                                        parameter_name = dr["parameter_name"].ToString();
                                                        parameter_data_type = IOS_Data_Type_Format(dr["data_type"].ToString());

                                                        if (is_first_parameter)
                                                            parameter_list_out = parameter_list_out + ":(" + parameter_data_type + "*)" + parameter_name;
                                                        else
                                                            parameter_list_out = parameter_list_out + " " + parameter_name + ":(" + parameter_data_type + "*)" + parameter_name;

                                                        is_first_parameter = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                function_name = function_definition;
                                            }

                                            break;
                                        }
                                    }

                                    if (uri.IndexOf("{") != -1)
                                    {
                                        int index = 1;
                                        uri = "\"" + uri.Substring(0, uri.IndexOf("?") + 1);
                                        is_first_parameter = true;
                                        foreach (DataRow dr in dt_parameters.Rows)
                                        {
                                            if(!is_first_parameter)
                                            {
                                                uri = uri + "&";
                                            }

                                            uri = uri + dr["parameter_name"].ToString() + "=%@";

                                            if (index == dt_parameters.Rows.Count)
                                            {
                                                uri = uri + "\"";
                                            }

                                            is_first_parameter = false;
                                            index++;
                                        }

                                        foreach (DataRow dr in dt_parameters.Rows)
                                        {
                                            uri = uri + ", " + dr["parameter_name"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        uri = "\"" + uri + "\"";
                                    }



                                    if (function_definition == "Create_Validation(Merchant_Contact merchant_contact_obj)")
                                        parameter_list_in = "";
                                            

                                    string read_line_service;
                                    using (StreamReader sr_service = new StreamReader(service_directory + service_name + ".svc.cs"))
                                    {
                                        while ((read_line_service = sr_service.ReadLine()) != null)
                                        {
                                            read_line_service = read_line_service.Trim();
                                            if (read_line_service.IndexOf(function_definition) != -1)
                                            {
                                                while ((read_line_service = sr_service.ReadLine()) != null)
                                                {
                                                    read_line_service = read_line_service.Trim();

                                                    //if (read_line_service.IndexOf("List<") != -1)
                                                    //{
                                                    //      array_value = read_line_service;
                                                    //    return_type = read_line_service.Substring(read_line_service.IndexOf("<") + 1, read_line_service.Length - (read_line_service.IndexOf("<") + 1));
                                                    //    return_type = return_type.Substring(0, return_type.IndexOf(">"));
                                                    //    return_type = ti.ToTitleCase(return_type);
                                                    //    array_return_type = ti.ToTitleCase(return_type);
                                                    //    //break;
                                                    //}

                                                    if (read_line_service.IndexOf("successful_response.data") != -1)
                                                    {
                                                        return_type = read_line_service.Substring(read_line_service.IndexOf("=") + 1, read_line_service.Length - (read_line_service.IndexOf("=") + 1)).Trim().Replace(";", "").Replace("_obj", "");

                                                        if(return_type.IndexOf("array") != -1)
                                                        {
                                                            return_is_array = true;
                                                            return_type = return_type.Replace("_array", "");
                                                        }

                                                        return_type = ti.ToTitleCase(return_type);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    sw.WriteLine("-(void)" + function_name + parameter_list_out + " withResponse:(void (^)(JSONResponse *))completionBlock");
                                    sw.WriteLine("{");
                                    sw.WriteLine("\tNSString *uri=[NSString stringWithFormat:@" + uri + "];");
                                    sw.WriteLine("\tNSURL *url = [NSURL URLWithString:[NSString stringWithFormat:@\"%@%@\", web_service, uri]];");
                                    sw.WriteLine("\tNSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:url];");
                                    sw.WriteLine("");
                                    
                                    if(call_type=="POST")
                                    {
                                        sw.WriteLine("\tNSString *json_request = [@{@\"" + parameter_name + "\": [" + parameter_name + " jsonValue]} jsonString];");
                                        sw.WriteLine("\tNSData *json_data = [json_request dataUsingEncoding:NSUTF8StringEncoding];");
                                        sw.WriteLine("");
                                        sw.WriteLine("\t[request setHTTPMethod:@\"POST\"];");
                                        sw.WriteLine("\t[request setValue:@\"application/json\" forHTTPHeaderField:@\"Accept\"];");
                                        sw.WriteLine("\t[request setValue:@\"application/json\" forHTTPHeaderField:@\"Content-Type\"];");
                                        sw.WriteLine("\t[request setValue:[NSString stringWithFormat:@\"%lu\", (unsigned long) [json_data length]] forHTTPHeaderField:@\"Content-Length\"];");
                                        sw.WriteLine("\t[request setHTTPBody: json_data];");
                                    }
                                    else if (call_type == "GET")
                                    {
                                        sw.WriteLine("\tNSDictionary *_headers = [NSDictionary dictionaryWithObjectsAndKeys:@\"application/json\", @\"accept\", nil];");
                                        sw.WriteLine("\t[request setAllHTTPHeaderFields:_headers];");
                                    }

                                    sw.WriteLine("");
                                    sw.WriteLine("\t[NSURLConnection sendAsynchronousRequest:request queue:[NSOperationQueue mainQueue] completionHandler:^(NSURLResponse* response, NSData* data, NSError* error)");
                                    sw.WriteLine("\t{");
                                    sw.WriteLine("\t\tif(data == nil)");
                                    sw.WriteLine("\t\t{");
                                    sw.WriteLine("\t\t\tSystem_Error *system_error_obj = [[System_Error alloc]init];");
                                    sw.WriteLine("\t\t\tsystem_error_obj.code = @500;");
                                    sw.WriteLine("\t\t\tsystem_error_obj.message = @\"Web service error.  Please check your internet connection\";");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\tJSONErrorResponse *response = [JSONErrorResponse new];");
                                    sw.WriteLine("\t\t\tresponse.successful = NO;");
                                    sw.WriteLine("\t\t\tresponse.system_error_obj = system_error_obj;");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\tcompletionBlock(response);");
                                    sw.WriteLine("\t\t}");

                                    sw.WriteLine("\t\telse");
                                    sw.WriteLine("\t\t{");
                                    sw.WriteLine("\t\t\tNSError *_errorJson = nil;");
                                    sw.WriteLine("\t\t\tNSDictionary *res = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&_errorJson];");
                                    sw.WriteLine("\t\t\tif (_errorJson != nil)");
                                    sw.WriteLine("\t\t\t{");
                                    sw.WriteLine("\t\t\t\tSystem_Error *system_error_obj = [[System_Error alloc]init];");
                                    sw.WriteLine("\t\t\t\tsystem_error_obj.code = @500;");
                                    sw.WriteLine("\t\t\t\tsystem_error_obj.message = @\"Web service error.  Please check your internet connection\";");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\tJSONErrorResponse *response = [JSONErrorResponse new];");
                                    sw.WriteLine("\t\t\t\tresponse.successful = NO;");
                                    sw.WriteLine("\t\t\t\tresponse.system_error_obj = system_error_obj;");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\tcompletionBlock(response);");
                                    sw.WriteLine("\t\t\t}");

                                    sw.WriteLine("\t\t\telse");
                                    sw.WriteLine("\t\t\t{");
                                    sw.WriteLine("\t\t\t\tNSNumber *successful = res[@\"JSONResponse\"][@\"successful\"];");
                                    sw.WriteLine("\t\t\t\tif (successful.boolValue)");
                                    sw.WriteLine("\t\t\t\t{");
                                    sw.WriteLine("\t\t\t\t\tJSONSuccessfulResponse *response = [JSONSuccessfulResponse new];");
                                    sw.WriteLine("\t\t\t\t\tresponse.successful = [successful boolValue];");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\t\tNSDictionary *dictionary_success = res[@\"JSONResponse\"][@\"system_successful_obj\"];");
                                    sw.WriteLine("\t\t\t\t\tresponse.system_successful_obj = [System_Successful objectFromJSON:dictionary_success];");
                                    if (return_is_array)
                                    {
                                        return_value = return_type.ToLower() + "_obj_array";

                                        sw.WriteLine("");
                                        sw.WriteLine("\t\t\t\t\tNSArray *dictionary_data = [res objectForKey:@\"JSONResponse\"][@\"data_obj\"];");
                                        sw.WriteLine("\t\t\t\t\tNSMutableArray *" + return_value + " = dictionary_data.count > 0 ? [NSMutableArray new] : nil;");
                                        sw.WriteLine("\t\t\t\t\t[dictionary_data enumerateObjectsUsingBlock:^(NSDictionary *obj, NSUInteger idx, BOOL *stop) {");
                                        sw.WriteLine("\t\t\t\t\t\t[" + return_value + " addObject:[" + return_type + " objectFromJSON:obj]];");
                                        sw.WriteLine("\t\t\t\t\t}];");
                                        sw.WriteLine("\t\t\t\t\tresponse.data_obj = " + return_value + ";");
                                    }
                                    else if (!return_is_array && return_type != "Null")
                                    {
                                        sw.WriteLine("");
                                        sw.WriteLine("\t\t\t\t\tNSDictionary *dictionary_data = res[@\"JSONResponse\"][@\"data_obj\"];");
                                        sw.WriteLine("\t\t\t\t\tresponse.data_obj = [" + return_type + " objectFromJSON:dictionary_data];");
                                    }
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\t\tcompletionBlock(response);");
                                    sw.WriteLine("\t\t\t\t}");

                                    sw.WriteLine("\t\t\t\telse");
                                    sw.WriteLine("\t\t\t\t{");
                                    sw.WriteLine("\t\t\t\t\tJSONErrorResponse *response = [JSONErrorResponse new];");
                                    sw.WriteLine("\t\t\t\t\tresponse.successful = [successful boolValue];");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\t\tNSDictionary *dictionary_error = res[@\"JSONResponse\"][@\"system_error_obj\"];");
                                    sw.WriteLine("\t\t\t\t\tresponse.system_error_obj = [System_Error objectFromJSON:dictionary_error];");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\t\t\tcompletionBlock(response);");
                                    sw.WriteLine("\t\t\t\t}");
                                    sw.WriteLine("\t\t\t}");
                                    sw.WriteLine("\t\t}");
                                    sw.WriteLine("\t}];");
                                    sw.WriteLine("}");
                                    sw.WriteLine("");
                                    sw.WriteLine("");
                                }
                            }

                            sr.Close();
                        }

                        sw.WriteLine("@end");
                        sw.WriteLine("");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IOS_Controller_Write()
        {
            string directory = output_folder + @"\ios\controller\";
            string service_directory = rest_folder + @"\Services\";
            string interface_directory = rest_folder + @"\Interfaces\";
            string controller_directory;
            DataTable dt_objects = Get_Objects();

            DirectoryInfo di = new DirectoryInfo(service_directory);
            //foreach (FileInfo fi in di.GetFiles("CustomerService.svc.cs"))
            foreach (FileInfo fi in di.GetFiles("*.svc.cs"))
            {
                string service_name = fi.Name.Replace(".svc.cs", "");

                controller_directory = directory + service_name + @"\";

                Empty_Directory(controller_directory);
                Empty_Directory(controller_directory);

                using (StreamReader sr = new StreamReader(fi.FullName))
                {
                    string read_line;

                    while ((read_line = sr.ReadLine()) != null)
                    {
                        if (read_line.Trim().Length >= 9)
                        {
                            if ((read_line.Trim().Substring(0, 8).Trim() == "#region") && (read_line.Trim() != "#region constructors") && (read_line.Trim() != "#region properties"))
                            {
                                read_line= read_line.Trim();

                                string controller = read_line.Substring(read_line.IndexOf(" ") + 1, read_line.Length - (read_line.IndexOf(" ") + 1));
                                controller = ti.ToTitleCase(controller) + "_Controller";

                                string file = controller_directory + controller;
                                using (StreamWriter swHeader = new StreamWriter(file + ".h"))
                                {
                                    StreamWriter swMethod = new StreamWriter(file + ".m");

                                    swHeader.WriteLine("#import <UIKit/UIKit.h>");
                                    swHeader.WriteLine("#import \"Base_Controller.h\"");
                                    swHeader.WriteLine("#import \"JSONResponse.h\"");
                                    swHeader.WriteLine("#import \"JSONSuccessfulResponse.h\"");
                                    swHeader.WriteLine("#import \"JSONErrorResponse.h\"");
                                    swHeader.WriteLine("#import \"REST_" + service_name + ".h\"");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("@interface " + controller + " : Base_Controller");
                                    swHeader.WriteLine("");

                                    swMethod.WriteLine("#import " + "\"" + controller + ".h\"");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("@interface " + controller + "()");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("@end");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("@implementation " + controller);
                                    swMethod.WriteLine("");

                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swMethod.WriteLine("@synthesize " + dr["object"].ToString().ToLower() + "_obj;");
                                        swMethod.WriteLine("@synthesize " + dr["object"].ToString().ToLower() + "_obj_array;");
                                    }
                                    swMethod.WriteLine("");

                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swHeader.WriteLine("@property (nonatomic, strong) " + dr["object"].ToString() + " *" + dr["object"].ToString().ToLower() + "_obj;");
                                        swHeader.WriteLine("@property (nonatomic, copy) NSMutableArray *" + dr["object"].ToString().ToLower() + "_obj_array;");
                                    }
                                    swHeader.WriteLine("");

                                    DataTable dt_non_object_properties = IOS_Get_Non_Object_Properties(fi.FullName, controller, "header");
                                    foreach(DataRow dr in dt_non_object_properties.Rows)
                                    {
                                        swHeader.WriteLine("@property (nonatomic, strong) " + IOS_Data_Type_Format(dr["data_type"].ToString()) + " *" + dr["property"].ToString() + ";");
                                        swMethod.WriteLine("@synthesize " + dr["property"].ToString() + ";");
                                    }

                                    swHeader.WriteLine("");
                                    swMethod.WriteLine("");
                                    swMethod.WriteLine("");

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();

                                        if (read_line.Trim() == "#endregion")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (read_line.IndexOf("JSONResponse") != -1)
                                            {
                                                string function_definition = read_line.Substring(read_line.IndexOf("JSONResponse") + 13, read_line.Length - (read_line.IndexOf("JSONResponse") + 13));
                                                string function_name = "";
                                                string parameter_list_in = "";
                                                string parameter_list_out = "";
                                                bool is_first_parameter = true;
                                                string output_data = "";
                                                string object_data_input = "";

                                                if (function_definition.IndexOf("(") != -1)
                                                {
                                                    function_name = function_definition.Substring(0, function_definition.IndexOf("(")).Trim();

                                                    // debug
                                                    if (function_name == "Deal_Payment")
                                                    {
                                                        int x = 0;
                                                    }

                                                    swHeader.WriteLine("- (void)" + function_name + ":(void(^)(void))completionBlock;");

                                                    swMethod.WriteLine("- (void)" + function_name + ":(void(^)(void))completionBlock");
                                                    swMethod.WriteLine("{");
                                                    swMethod.WriteLine("\tsystem_error_obj = nil;");
                                                    swMethod.WriteLine("\tsystem_successful_obj = nil;");
                                                    swMethod.WriteLine("");
                                                    
                                                    parameter_list_in = function_definition.Substring(function_definition.IndexOf("("), function_definition.Length - (function_definition.IndexOf("("))).Replace("(", "").Replace(")", "").Replace(";", "");

                                                    if (parameter_list_in != "")
                                                    {
                                                        DataTable dt_parameters = Get_Parameters(parameter_list_in);
                                                        foreach (DataRow dr in dt_parameters.Rows)
                                                        {
                                                            string parameter_name = dr["parameter_name"].ToString();
                                                            string parameter_data_type = IOS_Data_Type_Format(dr["data_type"].ToString());

                                                            if (is_first_parameter)
                                                                parameter_list_out = parameter_list_out + parameter_name + " ";
                                                            else
                                                                parameter_list_out = parameter_list_out + " " + parameter_name + ":(" + parameter_data_type + "*)" + parameter_name;

                                                            is_first_parameter = false;
                                                            swMethod.WriteLine("\t" + parameter_name + " = [[" + parameter_data_type + " alloc]init];");
                                                        }
                                                    }
                                                    parameter_list_out = parameter_list_out.Trim();

                                                    function_name = function_definition.Substring(0, function_definition.IndexOf("(")).Trim();
                                                    swMethod.WriteLine("");
                                                    
                                                    if ((controller == "Merchant_Controller") && (service_name == "MerchantService") && (function_name=="Update_Merchant_Contact_Name"))
                                                    {
                                                        string xx = "";
                                                    }

                                                    while ((read_line = sr.ReadLine()) != null)
                                                    {
                                                        string read_line_orig = read_line;
                                                        string read_line_trim = read_line.Trim();

                                                        if (parameter_list_out != "")
                                                        {
                                                            if (read_line.IndexOf("(" + parameter_list_out + ".") != -1)
                                                            {
                                                                object_data_input = read_line_trim.Substring(read_line_trim.IndexOf("(" + parameter_list_out + ".") + parameter_list_out.Length + 2, read_line_trim.Length - read_line_trim.IndexOf("(" + parameter_list_out + ".") - parameter_list_out.Length - 2);

                                                                int index_space = object_data_input.IndexOf(" ");
                                                                int index_paren = object_data_input.IndexOf(")");
                                                                int index_period = object_data_input.IndexOf(".");

                                                                if ((index_space != -1) && ((index_space < index_paren) || (index_paren == -1)) && ((index_space < index_period) || (index_period == -1)))
                                                                    object_data_input = object_data_input.Substring(0, index_space);
                                                                else if ((index_paren != -1) && ((index_paren < index_space) || (index_space == -1)) && ((index_paren < index_period) || (index_period == -1)))
                                                                    object_data_input = object_data_input.Substring(0, index_paren);
                                                                else
                                                                    object_data_input = object_data_input.Substring(0, index_period);


                                                                swMethod.WriteLine("\t" + parameter_list_out + "." + object_data_input + " = " + object_data_input + ";");
                                                            }

                                                            if (read_line_trim.Length >= parameter_list_out.Length)
                                                            {
                                                                if (read_line_trim.Substring(0, parameter_list_out.Length) == parameter_list_out.Replace("obj", "dal"))
                                                                {
                                                                    object_data_input = read_line_trim.Substring(read_line_trim.IndexOf(".") + 1, read_line_trim.Length - read_line_trim.IndexOf(".") - 1);
                                                                    object_data_input = object_data_input.Substring(0, object_data_input.IndexOf(" "));

                                                                    if ((object_data_input.IndexOf("_dal") != -1) && (object_data_input.IndexOf(".") == -1))
                                                                    {
                                                                        swMethod.WriteLine("\t" + parameter_list_out + "." + object_data_input.Replace("_dal", "_obj") + " = " + object_data_input.Replace("_dal", "_obj") + ";");
                                                                    }
                                                                }
                                                            }
                                                        }


                                                        /*
                                                        if ((read_line.IndexOf(parameter_list_out) != -1) && (read_line.IndexOf("new ") == -1))
                                                        {
                                                            if (read_line != "")
                                                            {
                                                                read_line = read_line.Substring(read_line.IndexOf("=") + 2, read_line.Length - (read_line.IndexOf("=") + 2)).Replace(parameter_list_out + ".", "");
                                                                if (read_line.IndexOf("_obj.") != -1)
                                                                {
                                                                    if ((read_line.IndexOf(object_data_input) == -1) || (object_data_input == ""))
                                                                    {
                                                                        object_data_input = read_line.Substring(0, read_line.IndexOf("."));

                                                                        swMethod.WriteLine("\t" + parameter_list_out + "." + object_data_input + " = " + object_data_input + ";");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        */

                                                        if (read_line_orig.IndexOf("successful_response.data") != -1)
                                                        {
                                                            output_data = read_line_orig.Substring(read_line_orig.IndexOf("=") + 2, read_line_orig.Length - (read_line_orig.IndexOf("=") + 2)).Replace(";", "");

                                                            break;
                                                        }
                                                    }

                                                    if (object_data_input != "")
                                                        swMethod.WriteLine("");
                                                    swMethod.WriteLine("\tREST_" + service_name + " *i_rest = [[REST_" + service_name + " alloc] initWithService:" + service_name.Replace("Service", "_service").ToLower() + "];");
                                                    swMethod.WriteLine("\t[i_rest " + function_name + ": " + parameter_list_out.Replace("_data", "_obj") + " withResponse:^(JSONResponse *response)");
                                                    swMethod.WriteLine("\t{");
                                                    swMethod.WriteLine("\t\tsuccessful = response.successful;");
                                                    swMethod.WriteLine("");
                                                    swMethod.WriteLine("\t\tif (!successful)");
                                                    swMethod.WriteLine("\t\t{");
                                                    swMethod.WriteLine("\t\t\tsystem_error_obj = [[System_Error alloc] init];");
                                                    swMethod.WriteLine("\t\t\tsystem_error_obj = ((JSONErrorResponse *)response).system_error_obj;");
                                                    swMethod.WriteLine("\t\t}");
                                                    swMethod.WriteLine("\t\telse");
                                                    swMethod.WriteLine("\t\t{");
                                                    swMethod.WriteLine("\t\t\tsystem_successful_obj = [[System_Successful alloc] init];");
                                                    swMethod.WriteLine("\t\t\tsystem_successful_obj = ((JSONSuccessfulResponse *)response).system_successful_obj;");
                                                    if (output_data != "null")
                                                    {
                                                        swMethod.WriteLine("");
                                                        swMethod.WriteLine("\t\t\t" + output_data + " = ((JSONSuccessfulResponse *)response).data_obj;");
                                                    }
                                                    swMethod.WriteLine("\t\t}");
                                                    swMethod.WriteLine("");
                                                    swMethod.WriteLine("\t\tcompletionBlock();");
                                                    swMethod.WriteLine("\t}];");
                                                    swMethod.WriteLine("}");
                                                    swMethod.WriteLine("");
                                                }
                                            }
                                        }

                                    }

                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("@end");

                                    swMethod.WriteLine("@end");

                                    swMethod.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        private int IOS_Custom_Object_Count(DataTable dt)
        {
            try
            {
                int count=0;

                foreach(DataRow dr in dt.Rows)
                {
                    if(IOS_Is_Custom_Object(dr["data_type"].ToString()))
                    {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IOS_Is_Custom_Object(string data_type)
        {
            try
            {
                switch (data_type)
                {
                    case "bool":
                    case "DateTime":
                    case "decimal":
                    case "Decimal":
                    case "double":
                    case "float":
                    case "int":
                    case "long":
                    case "string":
                    case "Byte[]":
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string IOS_Data_Type_Format(string data_type)
        {
            try
            {
                if (data_type == "string")
                {
                    data_type = "NSString";
                }
                else if (data_type == "DateTime")
                {
                    data_type = "NSDate";
                }
                else if (data_type == "Decimal")
                {
                    data_type = "NSDecimalNumber";
                }
                else if (data_type == "decimal")
                {
                    data_type = "NSDecimalNumber";
                }
                else if (data_type == "int")
                {
                    data_type = "NSNumber";
                }
                else if (data_type == "long")
                {
                    data_type = "NSNumber";
                }
                else if (data_type == "bool")
                {
                    data_type = "NSNumber";
                }
                else if (data_type == "double")
                {
                    data_type = "NSNumber";
                }
                else if (data_type == "float")
                {
                    data_type = "NSNumber";
                }
                else if (data_type == "Byte[]")
                {
                    data_type = "NSData";
                }
                else if(Is_Array(data_type))
                {
                    data_type = "NSMutableArray";
                }

                return data_type;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string IOS_Property_Type(string data_type)
        {
            try
            {
                if(data_type == "bool")
                    return "(nonatomic, retain)";
                else if (Is_Array(data_type))
                    return "(nonatomic, copy)";
                else
                    return "(nonatomic, strong)";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IOS_Add_Copy_To_Property(string data_type)
        {
            try
            {
                switch (data_type)
                {
                    case "bool":
                    case "decimal":
                    case "double":
                    case "int":
                    case "long":
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable IOS_Get_Non_Object_Properties(string file, string controller, string file_type)
        {
            string property = "", data_type = "";
            string read_line;

            DataTable dt = new DataTable();
            dt.Columns.Add("property", typeof(string));
            dt.Columns.Add("data_type", typeof(string));

            controller = controller.Replace("_Controller", "").ToLower();

            using (StreamReader sr = new StreamReader(file))
            {
                while ((read_line = sr.ReadLine()) != null)
                {
                    if (read_line.IndexOf("#region " + controller) != -1)
                    {
                        break;
                    }
                }

                while ((read_line = sr.ReadLine()) != null)
                {
                    if (read_line.IndexOf("JSONResponse") != -1)
                    {
                        string argument_list = read_line.Substring(read_line.IndexOf("("), read_line.Length - (read_line.IndexOf("("))).Replace("(", "").Replace(")", "");

                        if (argument_list != "")
                        {
                            int count = argument_list.Split(',').Length;

                            for (int i = 1; i <= count; i++)
                            {
                                data_type = argument_list.Substring(0, argument_list.IndexOf(" "));
                                if (!IOS_Is_Custom_Object(data_type))
                                {
                                    if (argument_list.IndexOf(",") != -1)
                                    {
                                        property = argument_list.Substring(argument_list.IndexOf(" ") + 1, argument_list.IndexOf(",") - (argument_list.IndexOf(" ") + 1));
                                        argument_list = argument_list.Substring(argument_list.IndexOf(",") + 2, argument_list.Length - (argument_list.IndexOf(",") + 2));
                                    }
                                    else
                                    {
                                        property = argument_list.Substring(argument_list.IndexOf(" ") + 1, argument_list.Length - (argument_list.IndexOf(" ") + 1));
                                    }

                                    DataRow[] result = dt.Select("property = '" + property + "'");
                                    if (result.Count() == 0)
                                    {
                                        dt.Rows.Add(property, data_type);
                                    }
                                }
                            }
                        }
                    }

                    if (read_line.IndexOf("#endregion") != -1)
                    {
                        break;
                    }
                }
            }

            return dt;
        }

        private void Java_Model_Object_Write(string java_directory, DataTable dt)
        {
            try
            {
                string object_name = dt.TableName;
                string java_file = java_directory + object_name + ".java";
                string package_model = package_name + ".model";

                using (StreamWriter sw = new StreamWriter(java_file))
                {
                    sw.WriteLine("package " + package_model + ".object;");
                    sw.WriteLine("");
                    sw.WriteLine("import android.os.Parcel;");
                    sw.WriteLine("import android.os.Parcelable;");
                    sw.WriteLine("");
                    sw.WriteLine("import java.math.BigDecimal;");
                    sw.WriteLine("import java.util.Date;");
                    sw.WriteLine("");
                    sw.WriteLine("public class " + ti.ToTitleCase(object_name) + " implements Parcelable");
                    sw.WriteLine("{");

                    // write variables
                    foreach (DataRow dr in dt.Rows)
                    {
                        string data_type = Java_Data_Type_Format(dr["data_type"].ToString());

                        if (dr["property"].ToString().IndexOf("array") == -1)
                        {
                            sw.WriteLine("\tprivate " + data_type + " " + dr["property"].ToString() + ";");
                        }
                        else
                        {
                            data_type = data_type.Replace("List<", "").Replace(">", "") + "[]";
                            sw.WriteLine("\tprivate " + data_type + " " + dr["property"].ToString() + ";");
                        }
                    }
                    sw.WriteLine("");

                    sw.WriteLine("\tpublic " + object_name + "()");
                    sw.WriteLine("\t{");
                    sw.WriteLine("");
                    sw.WriteLine("\t}");
                    sw.WriteLine("");

                    sw.WriteLine("\t@Override");
                    sw.WriteLine("\tpublic int describeContents()");
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t\treturn 0;");
                    sw.WriteLine("\t}");
                    sw.WriteLine("");

                    sw.WriteLine("\t@Override");
                    sw.WriteLine("\tpublic void writeToParcel(Parcel out, int flag)");
                    sw.WriteLine("\t{");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["property"].ToString().IndexOf("array") == -1)
                        {
                            sw.WriteLine("\t\t" + Java_Parcelable_Out_Value(dr["data_type"].ToString(), dr["property"].ToString()));
                        }
                        else
                        {
                            sw.WriteLine("\t\tout.writeTypedArray(" + dr["property"].ToString() + ", flag);");
                        }
                    }
                    sw.WriteLine("\t}");
                    sw.WriteLine("");

                    sw.WriteLine("\tpublic static final Parcelable.Creator<" + ti.ToTitleCase(object_name) + "> CREATOR = new Parcelable.Creator<" + ti.ToTitleCase(object_name) + ">() ");
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t\tpublic " + ti.ToTitleCase(object_name) + " createFromParcel(Parcel in)");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t\treturn new " + ti.ToTitleCase(object_name) + "(in);");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("");
                    sw.WriteLine("\t\tpublic " + ti.ToTitleCase(object_name) + "[] newArray(int size)");
                    sw.WriteLine("\t\t{");
                    sw.WriteLine("\t\t\treturn new " + ti.ToTitleCase(object_name) + "[size];");
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("\t};");
                    sw.WriteLine("");

                    sw.WriteLine("\tprivate " + ti.ToTitleCase(object_name) + "(Parcel in)");
                    sw.WriteLine("\t{");
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["property"].ToString().IndexOf("array") == -1)
                        {
                            sw.WriteLine("\t\t" + Java_Parcelable_In_Value(dr["data_type"].ToString(), dr["property"].ToString()));
                        }
                        else
                        {
                            sw.WriteLine("\t\t" + dr["property"].ToString() + " = in.createTypedArray(" + dr["data_type"].ToString().Replace("List<", "").Replace(">", "") + ".CREATOR);");
                        }
                    }
                    sw.WriteLine("\t}");

                    foreach (DataRow dr in dt.Rows)
                    {
                        string data_type = Java_Data_Type_Format(dr["data_type"].ToString());
                        string property = dr["property"].ToString();

                        if (dr["property"].ToString().IndexOf("array") != -1)
                        {
                            data_type = data_type.Replace("List<", "").Replace(">", "") + "[]";
                        }

                        sw.WriteLine("");
                        sw.WriteLine("\tpublic " + data_type + " get" + Java_Function_Format(dr["property"].ToString()) + "()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\treturn this." + dr["property"].ToString() + ";");
                        sw.WriteLine("\t}");

                        sw.WriteLine("\tpublic void set" + Java_Function_Format(dr["property"].ToString()) + "(" + data_type + " " + dr["property"].ToString() + ")");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tthis." + dr["property"].ToString() + " = " + dr["property"].ToString() + ";");
                        sw.WriteLine("\t}");
                    }

                    sw.WriteLine("}");

                    sw.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Java_Model_Response_Write()
        {
            try
            {
                string package_model = package_name + ".model";
                string java_directory = output_folder + @"\java\model\response\";

                Empty_Directory(java_directory);
                Empty_Directory(java_directory);

                DataTable dt_objects = Get_Objects();
                foreach (DataRow dr in dt_objects.Rows)
                {
                    string java_file = java_directory + "JSONSuccessfulResponse_" + dr["object"].ToString() + ".java";

                    using (StreamWriter sw = new StreamWriter(java_file))
                    {
                        sw.WriteLine("package " + package_model + ".response;");
                        sw.WriteLine("");
                        sw.WriteLine("import " + package_model + ".base." + dr["object"].ToString() + ";");
                        sw.WriteLine("import " + package_name + ".system.JSONResponse;");
                        sw.WriteLine("");
                        sw.WriteLine("public class JSONSuccessfulResponse_" + dr["object"].ToString() + " extends JSONResponse");
                        sw.WriteLine("{");
                        sw.WriteLine("\tprivate " + dr["object"].ToString() + " data;");
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("\tpublic " + dr["object"].ToString() + " getData()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\t" + "return this.data;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("");
                        sw.WriteLine("\tpublic void setData(" + dr["object"].ToString() + " data)");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\t" + "this.data = data;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                    }

                    string java_array_file = java_directory + "JSONSuccessfulResponse_" + dr["object"].ToString() + "_Array.java";

                    using (StreamWriter sw_array = new StreamWriter(java_array_file))
                    {
                        sw_array.WriteLine("package " + package_model + ".response;");
                        sw_array.WriteLine("");
                        sw_array.WriteLine("import " + package_model + ".base." + dr["object"].ToString() + ";");
                        sw_array.WriteLine("import " + package_name + ".system.JSONResponse;");
                        sw_array.WriteLine("");
                        sw_array.WriteLine("public class JSONSuccessfulResponse_" + dr["object"].ToString() + "_Array extends JSONResponse");
                        sw_array.WriteLine("{");
                        sw_array.WriteLine("\tprivate " + dr["object"].ToString() + "[] data;");
                        sw_array.WriteLine("");
                        sw_array.WriteLine("");
                        sw_array.WriteLine("\tpublic " + dr["object"].ToString() + "[] getData()");
                        sw_array.WriteLine("\t{");
                        sw_array.WriteLine("\t\t" + "return this.data;");
                        sw_array.WriteLine("\t}");
                        sw_array.WriteLine("");
                        sw_array.WriteLine("\tpublic void setData(" + dr["object"].ToString() + "[] data)");
                        sw_array.WriteLine("\t{");
                        sw_array.WriteLine("\t\t" + "this.data = data;");
                        sw_array.WriteLine("\t}");
                        sw_array.WriteLine("}");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Java_Model_Interface_Write()
        {
            try
            {
                string java_directory = output_folder + @"\java\model\rest_interface\";
                string service_directory = rest_folder + @"\Services\";
                string interface_directory = rest_folder + @"\Interfaces\";
                DataTable dt_objects = Get_Objects();

                Empty_Directory(java_directory);
                Empty_Directory(java_directory);

                DirectoryInfo di = new DirectoryInfo(interface_directory);
                foreach (FileInfo fi in di.GetFiles())
                {
                    string service_name = fi.Name.Replace(".cs", "").Substring(1, fi.Name.Length - 4);

                    string java_file = java_directory + @"REST_" + service_name + ".java";

                    using (StreamWriter sw = new StreamWriter(java_file))
                    {
                        sw.WriteLine("package " + package_name + ".controller.rest_interface;");
                        sw.WriteLine("");
                        sw.WriteLine("import com.google.gson.*;");
                        sw.WriteLine("import " + package_name + ".model.object.*;");
                        sw.WriteLine("import " + package_name + ".model.response.*;");
                        sw.WriteLine("import " + package_name + ".system.gtsoft.DotNetDateDeserializer;");
                        sw.WriteLine("import " + package_name + ".system.gtsoft.ServiceHandler;");
                        sw.WriteLine("import java.util.Date;");
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("public class REST_" + service_name);
                        sw.WriteLine("{");
                        sw.WriteLine("\tprivate String web_service;");
                        sw.WriteLine("\tprivate Gson gson;");
                        sw.WriteLine("");
                        sw.WriteLine("\tprivate JSONResponse json_response;");
                        sw.WriteLine("\tprivate JSONSuccessfulResponse json_successful_response;");
                        sw.WriteLine("\tprivate JSONErrorResponse json_error_response;");
                        sw.WriteLine("");
                        sw.WriteLine("");

                        sw.WriteLine("\tpublic REST_" + service_name + "(String _web_service)");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tweb_service = _web_service;");
                        sw.WriteLine("");
                        sw.WriteLine("\t\tGsonBuilder builder = new GsonBuilder();");
                        sw.WriteLine("\t\tbuilder.registerTypeAdapter(Date.class, new DotNetDateDeserializer());");
                        sw.WriteLine("\t\tgson = builder.create();");
                        sw.WriteLine("\t}");

                        using (StreamReader sr = new StreamReader(fi.FullName))
                        {
                            string read_line;
                            string call_type = "";
                            string uri="";
                            string function_definition="";
                            string return_type = "";
                            string input_type = "";
                            string array_value = "";

                            while ((read_line = sr.ReadLine()) != null)
                            {
                                call_type = "";
                                uri = "";
                                function_definition = "";
                                return_type = "";
                                input_type = "";
                                array_value = "";

                                read_line = read_line.Trim();
                                if (read_line.Trim() == "[OperationContract]")
                                {
                                    read_line = sr.ReadLine().Trim();
                                    read_line = sr.ReadLine().Trim();

                                    if (read_line.Trim().Replace("[", "").Substring(0, 6) == "WebGet")
                                    {
                                        call_type = "GET";
                                    }
                                    else
                                    {
                                        call_type = "POST";
                                    }

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();
                                        if(read_line.Substring(0, read_line.Trim().IndexOf(" ")) == "UriTemplate")
                                        {
                                            uri = read_line.Substring(read_line.IndexOf("\""), read_line.Length - read_line.IndexOf("\"")).Replace("\"", "").Replace(")]", "");
                                            break;
                                        }
                                    }

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();
                                        if (read_line.Substring(0, read_line.Trim().IndexOf(" ")) == "JSONResponse")
                                        {
                                            function_definition = read_line.Substring(read_line.IndexOf(" ") + 1, read_line.Length - (read_line.IndexOf(" ") + 1)).Replace(";","");
                                            break;
                                        }
                                    }

                                    string read_line_service;
                                    using (StreamReader sr_service = new StreamReader(service_directory + service_name + ".svc.cs"))
                                    {
                                        while ((read_line_service = sr_service.ReadLine()) != null)
                                        {
                                            read_line_service = read_line_service.Trim();
                                            if (read_line_service.IndexOf(function_definition) != -1)
                                            {
                                                while ((read_line_service = sr_service.ReadLine()) != null)
                                                {
                                                    read_line_service = read_line_service.Trim();

                                                    if(read_line_service.IndexOf("List<") != -1)
                                                    {
                                                        array_value = read_line_service;
                                                        return_type = read_line_service.Substring(read_line_service.IndexOf("<") + 1, read_line_service.Length - (read_line_service.IndexOf("<") + 1));
                                                        return_type = return_type.Substring(0, return_type.IndexOf(">")).ToLower();
                                                        break;
                                                    }

                                                    if (read_line_service.IndexOf("successful_response.data") != -1)
                                                    {
                                                        return_type = read_line_service.Substring(read_line_service.IndexOf("=") + 1, read_line_service.Length - (read_line_service.IndexOf("=") + 1)).Trim().Replace(";","").Replace("_obj","");
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    sw.WriteLine("");
                                    sw.WriteLine("\tpublic void " + function_definition);
                                    sw.WriteLine("\t{");

                                    if (call_type == "GET")
                                    {
                                        string hold = "\t\tString uri = \"" + uri.Replace("{", "\" + ").Replace("}", " + \"") + ";";
                                        hold = hold.Substring(0, hold.Length - 5) + ";";
                                        sw.WriteLine(hold);
                                    }
                                    else
                                    {
                                        sw.WriteLine("\t\tString uri = \"" + uri + "\";");
                                    }
                                    sw.WriteLine("");

                                    if(call_type=="POST")   
                                    {
                                        input_type = function_definition.Substring(function_definition.IndexOf("(") + 1, function_definition.Length - (function_definition.IndexOf("(") + 1));

                                        if (input_type != ")")
                                        {
                                            input_type = input_type.Substring(0, input_type.IndexOf(" "));
                                            sw.WriteLine("\t\tJsonElement je = gson.toJsonTree(" + input_type.ToLower() + "_obj);");
                                            sw.WriteLine("\t\tJsonObject jo = new JsonObject();");
                                            sw.WriteLine("\t\tjo.add(\"" + input_type.ToLower() + "_obj\", je);");
                                            sw.WriteLine("\t\tString json_request_string = jo.toString();");
                                        }
                                    }
                                    sw.WriteLine("");

                                    sw.WriteLine("\t\tServiceHandler sh = new ServiceHandler();");
                                    
                                    if(call_type=="GET")   
                                    {
                                        sw.WriteLine("\t\tString json_response_string = sh.makeServiceCall(web_service + uri, ServiceHandler." + call_type + ", \"\");");
                                    }
                                    else
                                    {
                                        sw.WriteLine("\t\tString json_response_string = sh.makeServiceCall(web_service + uri, ServiceHandler." + call_type + ", json_request_string);");
                                    }

                                    sw.WriteLine("");
                                    sw.WriteLine("\t\tif(json_response_string == null)");
                                    sw.WriteLine("\t\t{");
                                    sw.WriteLine("\t\t\tNo_Internet_Connection();");
                                    sw.WriteLine("\t\t}");
                                    sw.WriteLine("\t\telse");
                                    sw.WriteLine("\t\t{"); 
                                    sw.WriteLine("\t\t\tJsonObject jsonObject = gson.fromJson(json_response_string, JsonObject.class);");
                                    sw.WriteLine("\t\t\tjson_response = gson.fromJson(jsonObject.get(\"JSONResponse\"), JSONResponse.class);");
                                    sw.WriteLine("");
                                    sw.WriteLine("\t\t\tif(json_response.getSuccessful())");
                                    sw.WriteLine("\t\t\t{");
                                    //if(array_value != "")
                                    //{
                                    //    sw.WriteLine("\t\t\t\tJSONSuccessfulResponse_" + ti.ToTitleCase(return_type) + "_Array_Wrapper_" + service_name + " data = gson.fromJson(json_response_string, JSONSuccessfulResponse_" + ti.ToTitleCase(return_type) + "_Array_Wrapper_" + service_name + ".class);");
                                    //    sw.WriteLine("\t\t\t\tjson_successful_response_" + return_type + "_array = data.getJSONResponse();");
                                    //}
                                    //else
                                    //{
                                        sw.WriteLine("\t\t\t\tjson_successful_response = gson.fromJson(jsonObject.get(\"JSONResponse\"), JSONSuccessfulResponse.class);");
                                        if (array_value != "")
                                            sw.WriteLine("\t\t\t\tjson_successful_response.setDataObj(gson.fromJson(((JsonObject)jsonObject.get(\"JSONResponse\")).get(\"data_obj\"), " + ti.ToTitleCase(return_type) + "[].class));");
                                        else if (ti.ToTitleCase(return_type) != "Null")
                                            sw.WriteLine("\t\t\t\tjson_successful_response.setDataObj(gson.fromJson(((JsonObject)jsonObject.get(\"JSONResponse\")).get(\"data_obj\"), " + ti.ToTitleCase(return_type) + ".class));");
                                    //}
                                    sw.WriteLine("\t\t\t}");

                                    sw.WriteLine("\t\t\telse");
                                    sw.WriteLine("\t\t\t{");
                                    sw.WriteLine("\t\t\t\tjson_error_response = gson.fromJson(jsonObject.get(\"JSONResponse\"), JSONErrorResponse.class);");
                                    sw.WriteLine("\t\t\t}");
                                    sw.WriteLine("\t\t}");
                                    sw.WriteLine("\t}");
                                }
                            }
                        }
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("\tprivate void No_Internet_Connection()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\tjson_response = new JSONResponse();");
                        sw.WriteLine("\t\tjson_response.setSuccessful(false);");
                        sw.WriteLine("");
                        sw.WriteLine("\t\tSystem_Error system_error_obj = new System_Error();");
                        sw.WriteLine("\t\tsystem_error_obj.setCode(500);");
                        sw.WriteLine("\t\tsystem_error_obj.setMessage(\"No Internet Connection\");");
                        sw.WriteLine("");
                        sw.WriteLine("\t\tjson_error_response = new JSONErrorResponse();");
                        sw.WriteLine("\t\tjson_error_response.setSuccessful(json_response.getSuccessful());");
                        sw.WriteLine("\t\tjson_error_response.setSystemError(system_error_obj);");
                        sw.WriteLine("\t}");

                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine("\tpublic JSONResponse getJSONResponse()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\treturn this.json_response;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("");
                        sw.WriteLine("\tpublic JSONSuccessfulResponse getJSONSuccessfulResponse()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\treturn this.json_successful_response;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("");
                        sw.WriteLine("\tpublic JSONErrorResponse getJSONErrorResponse()");
                        sw.WriteLine("\t{");
                        sw.WriteLine("\t\treturn this.json_error_response;");
                        sw.WriteLine("\t}");
                        sw.WriteLine("}");
                        sw.WriteLine("");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Java_Controller_Write()
        {
            string directory = output_folder + @"\java\controller\";
            string service_directory = rest_folder + @"\Services\";
            string interface_directory = rest_folder + @"\Interfaces\";
            string controller_directory;
            DataTable dt_objects = Get_Objects();

            DirectoryInfo di = new DirectoryInfo(service_directory);
            foreach (FileInfo fi in di.GetFiles("*svc.cs"))
            //    foreach (FileInfo fi in di.GetFiles("Merchant*.svc.cs"))
            {
                string service_name = fi.Name.Replace(".svc.cs", "");

                controller_directory = directory + service_name + @"\";

                Empty_Directory(controller_directory);
                Empty_Directory(controller_directory);

                using (StreamReader sr = new StreamReader(fi.FullName))
                {
                    string read_line;

                    while ((read_line = sr.ReadLine()) != null)
                    {
                        if (read_line.Trim().Length >= 9)
                        {
                            if (read_line.Trim().Substring(0, 8).Trim() == "#region" && read_line.Trim() != "#region constructors" && read_line.Trim() != "#region properties")
                            {
                                read_line = read_line.Trim();

                                string controller = read_line.Substring(read_line.IndexOf(" ") + 1, read_line.Length - (read_line.IndexOf(" ") + 1));
                                controller = ti.ToTitleCase(controller) + "_Controller";

                                string file = controller_directory + controller;
                                using (StreamWriter swHeader = new StreamWriter(file + ".java"))
                                {
                                    swHeader.WriteLine("package " + package_name + ".controller.object;");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("import android.content.Context;");
                                    swHeader.WriteLine("import android.os.Parcel;");
                                    swHeader.WriteLine("import android.os.Parcelable;");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("import " + package_name + ".controller.base.Base_Controller;");
                                    swHeader.WriteLine("import " + package_name + ".controller.rest_interface.REST_" + service_name + ";");
                                    swHeader.WriteLine("import " + package_name + ".model.object.*;");
                                    swHeader.WriteLine("import " + package_name + ".model.response.*;");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("");

                                    swHeader.WriteLine("public class " + ti.ToTitleCase(controller) + " extends Base_Controller implements Parcelable");
                                    swHeader.WriteLine("{");

                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swHeader.WriteLine("\tprivate " + dr["object"].ToString() + " " + dr["object"].ToString().ToLower() + "_obj;");
                                        swHeader.WriteLine("\tprivate " + dr["object"].ToString() + "[] " + dr["object"].ToString().ToLower() + "_obj_array;");
                                    }
                                    swHeader.WriteLine("");

                                    DataTable dt_non_object_properties = IOS_Get_Non_Object_Properties(fi.FullName, controller, "header");
                                    if (dt_non_object_properties.Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dt_non_object_properties.Rows)
                                        {
                                            swHeader.WriteLine("\tprivate " + Java_Data_Type_Format(dr["data_type"].ToString()) + " " + dr["property"].ToString() + ";");
                                        }
                                        swHeader.WriteLine("");
                                    }
                                    swHeader.WriteLine("");

                                    swHeader.WriteLine("\tpublic " + ti.ToTitleCase(controller) + "(Context context)");
                                    swHeader.WriteLine("\t{");
                                    swHeader.WriteLine("\t}");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("\t@Override");
                                    swHeader.WriteLine("\tpublic int describeContents()");
                                    swHeader.WriteLine("\t{");
                                    swHeader.WriteLine("\t\treturn 0;");
                                    swHeader.WriteLine("\t}");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("\t@Override");
                                    swHeader.WriteLine("\tpublic void writeToParcel(Parcel out, int flag)");
                                    swHeader.WriteLine("\t{");
                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swHeader.WriteLine("\t\tout.writeParcelable(" + dr["object"].ToString().ToLower() + "_obj, flag);");
                                        swHeader.WriteLine("\t\tout.writeTypedArray(" + dr["object"].ToString().ToLower() + "_obj_array, flag);");
                                    }
                                    swHeader.WriteLine("\t}");
                                    swHeader.WriteLine("");

                                    swHeader.WriteLine("\tprivate " + ti.ToTitleCase(controller) + "(Parcel in)");
                                    swHeader.WriteLine("\t{");
                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swHeader.WriteLine("\t\t" + dr["object"].ToString().ToLower() + "_obj = in.readParcelable(" + dr["object"].ToString() + ".class.getClassLoader());");
                                        swHeader.WriteLine("\t\t" + dr["object"].ToString().ToLower() + "_obj_array = in.createTypedArray(" + dr["object"].ToString() + ".CREATOR);");
                                    }
                                    swHeader.WriteLine("\t}");
                                    swHeader.WriteLine("");

                                    swHeader.WriteLine("\tpublic static final Parcelable.Creator<" + ti.ToTitleCase(controller) + "> CREATOR = new Parcelable.Creator<" + ti.ToTitleCase(controller) + ">() ");
                                    swHeader.WriteLine("\t{");
                                    swHeader.WriteLine("\t\tpublic " + ti.ToTitleCase(controller) + " createFromParcel(Parcel in) ");
                                    swHeader.WriteLine("\t\t{");
                                    swHeader.WriteLine("\t\t\treturn new " + ti.ToTitleCase(controller) + "(in);");
                                    swHeader.WriteLine("\t\t}");
                                    swHeader.WriteLine("");
                                    swHeader.WriteLine("\t\tpublic " + ti.ToTitleCase(controller) + "[] newArray(int size) ");
                                    swHeader.WriteLine("\t\t{");
                                    swHeader.WriteLine("\t\t\treturn new " + ti.ToTitleCase(controller) + "[size];");
                                    swHeader.WriteLine("\t\t}");
                                    swHeader.WriteLine("\t};");
                                    swHeader.WriteLine("");

                                    while ((read_line = sr.ReadLine()) != null)
                                    {
                                        read_line = read_line.Trim();

                                        if (read_line.Trim() == "#endregion")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (read_line.IndexOf("JSONResponse") != -1)
                                            {
                                                string function_definition = read_line.Substring(read_line.IndexOf("JSONResponse") + 13, read_line.Length - (read_line.IndexOf("JSONResponse") + 13));
                                                string function_name = "";
                                                string parameter_list_in = "";
                                                string parameter_list_out = "";
                                                string output_object = "";
                                                string output_data = "";
                                                string object_data_input = "";

                                                string parameter_name = "";
                                                string parameter_data_type = "";

                                                if (function_definition.IndexOf("(") != -1)
                                                {
                                                    function_name = function_definition.Substring(0, function_definition.IndexOf("(")).Trim();

                                                    swHeader.WriteLine("\tpublic void " + function_name + "()");
                                                    swHeader.WriteLine("\t{");
                                                    swHeader.WriteLine("\t\tsystem_error_obj = null;");
                                                    swHeader.WriteLine("\t\tsystem_successful_obj = null;");
                                                    swHeader.WriteLine("");

                                                    parameter_list_in = function_definition.Substring(function_definition.IndexOf("("), function_definition.Length - (function_definition.IndexOf("("))).Replace("(", "").Replace(")", "").Replace(";", "");
                                                    DataTable dt_parameters = null;
                                                    if (parameter_list_in != "")
                                                    {
                                                        dt_parameters = Get_Parameters(parameter_list_in);
                                                        foreach (DataRow dr in dt_parameters.Rows)
                                                        {
                                                            parameter_name = dr["parameter_name"].ToString();
                                                            parameter_data_type = Java_Data_Type_Format(dr["data_type"].ToString());

                                                            parameter_list_out = parameter_list_out + parameter_name + ", ";

                                                            if (Java_Is_Custom_Object(parameter_data_type))
                                                                swHeader.WriteLine("\t\t" + parameter_name + " = new " + parameter_data_type + "();");
                                                            else
                                                                swHeader.WriteLine("\t\t" + parameter_name + " = 0;");
                                                        }

                                                        parameter_list_out = parameter_list_out.Substring(0, parameter_list_out.Length - 2);
                                                    }

                                                    
                                                    while ((read_line = sr.ReadLine()) != null)
                                                    {
                                                        if (read_line.IndexOf("successful_response.data") != -1)
                                                        {
                                                            output_data = read_line.Substring(read_line.IndexOf("=") + 2, read_line.Length - (read_line.IndexOf("=") + 2)).Replace(";", "");

                                                            if(output_data.IndexOf("_array") == -1)
                                                                output_object = ti.ToTitleCase(output_data.Replace("_obj", ""));
                                                            else
                                                                output_object = ti.ToTitleCase(output_data.Replace("_obj", "").Replace("_array", "")) + "[]";

                                                            break;
                                                        }
                                                    }

                                                    if (dt_parameters != null)
                                                    {
                                                        if (dt_parameters.Rows.Count > 0)
                                                            swHeader.WriteLine("");
                                                    }

                                                    if (object_data_input != "")
                                                        swHeader.WriteLine("");

                                                    swHeader.WriteLine("\t\tREST_" + service_name + " i_rest = new REST_" + service_name + "(" + service_name.Replace("Service", "_service").ToLower() + ");");
                                                    swHeader.WriteLine("\t\ti_rest." + function_name + "(" + parameter_list_out + ");");
                                                    swHeader.WriteLine("");
                                                    swHeader.WriteLine("\t\tJSONResponse json_response = i_rest.getJSONResponse();");
                                                    swHeader.WriteLine("\t\tsuccessful = json_response.getSuccessful();");
                                                    swHeader.WriteLine("");
                                                    swHeader.WriteLine("\t\tif (!successful)");
                                                    swHeader.WriteLine("\t\t{");
                                                    swHeader.WriteLine("\t\t\tsystem_error_obj = i_rest.getJSONErrorResponse().getSystemError();");
                                                    swHeader.WriteLine("\t\t}");
                                                    swHeader.WriteLine("\t\telse");
                                                    swHeader.WriteLine("\t\t{");
                                                    swHeader.WriteLine("\t\t\tsystem_successful_obj = i_rest.getJSONSuccessfulResponse().getSystemSuccessful();");
                                                    if (output_data != "null")
                                                    {
                                                        swHeader.WriteLine("\t\t\t" + output_data + " = (" + output_object + ")i_rest.getJSONSuccessfulResponse().getDataObj();");
                                                    }
                                                    swHeader.WriteLine("\t\t}");
                                                    swHeader.WriteLine("\t}");
                                                    swHeader.WriteLine("");
                                                }
                                            }
                                        }
                                    }
                                    swHeader.WriteLine("");

                                    foreach (DataRow dr in dt_objects.Rows)
                                    {
                                        swHeader.WriteLine("\tpublic " + dr["object"].ToString() + " get" + Java_Function_Format(dr["object"].ToString()) + "Obj()");
                                        swHeader.WriteLine("\t{");
                                        swHeader.WriteLine("\t\treturn this." + dr["object"].ToString().ToLower() + "_obj;");
                                        swHeader.WriteLine("\t}");
                                        swHeader.WriteLine("");
                                        swHeader.WriteLine("\tpublic void set" + Java_Function_Format(dr["object"].ToString()) + "Obj(" + dr["object"].ToString() + " " + dr["object"].ToString().ToLower() + "_obj)");
                                        swHeader.WriteLine("\t{");
                                        swHeader.WriteLine("\t\tthis." + dr["object"].ToString().ToLower() + "_obj = " + dr["object"].ToString().ToLower() + "_obj;");
                                        swHeader.WriteLine("\t}");
                                        swHeader.WriteLine("");
                                        swHeader.WriteLine("\tpublic " + dr["object"].ToString() + "[] get" + Java_Function_Format(dr["object"].ToString()) + "ObjArray()");
                                        swHeader.WriteLine("\t{");
                                        swHeader.WriteLine("\t\treturn this." + dr["object"].ToString().ToLower() + "_obj_array;");
                                        swHeader.WriteLine("\t}");
                                        swHeader.WriteLine("");
                                    }

                                    foreach (DataRow dr in dt_non_object_properties.Rows)
                                    {
                                        swHeader.WriteLine("\tpublic " + Java_Data_Type_Format(dr["data_type"].ToString()) + " get" + Java_Function_Format(dr["property"].ToString()) + "()");
                                        swHeader.WriteLine("\t{");
                                        swHeader.WriteLine("\t\treturn this." + dr["property"].ToString() + ";");
                                        swHeader.WriteLine("\t}");
                                        swHeader.WriteLine("");
                                        swHeader.WriteLine("\tpublic void set" + Java_Function_Format(dr["property"].ToString()) + "(" + dr["data_type"].ToString() + " " + dr["property"].ToString().ToLower() + ")");
                                        swHeader.WriteLine("\t{");
                                        swHeader.WriteLine("\t\tthis." + dr["property"].ToString().ToLower() + " = " + dr["property"].ToString().ToLower() + ";");
                                        swHeader.WriteLine("\t}");
                                        swHeader.WriteLine("");
                                    }

                                    swHeader.WriteLine("}");
                                }
                            }
                        }
                    }
                }
            }
        }

        private string Java_Data_Type_Format(string data_type)
        {
            try
            {
                if (data_type == "string")
                {
                    data_type = ti.ToTitleCase(data_type);
                }
                else if (data_type == "bool")
                {
                    data_type = "boolean";
                }
                else if (data_type == "DateTime")
                {
                    data_type = "Date";
                }
                else if (data_type == "decimal")
                {
                    data_type = "BigDecimal";
                }
                else if (data_type == "Decimal")
                {
                    data_type = "BigDecimal";
                }

                return data_type;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string Java_Parcelable_Out_Value(string data_type, string property)
        {
            try
            {
                string value;

                switch (data_type)
                {
                    case "double":
                    case "int":
                    case "long":
                    case "string":
                        return value = "out.write" + ti.ToTitleCase(data_type) + "(" + property + ");";
                    case "bool":
                        return value = "out.writeByte((byte) (" + property + " ? 1:0));";
                    case "DateTime":
                    case "decimal":
                    case "Decimal":
                        return value = "out.writeSerializable(" + property + ");";
                    case "Byte[]":
                        return value = "out.writeSerializable(" + property + ");";
                    default:
                        return value = "out.writeParcelable(" + property + ", flag);";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Java_Parcelable_In_Value(string data_type, string property)
        {
            try
            {
                string value;

                switch (data_type)
                {
                    case "double":
                    case "int":
                    case "long":
                    case "string":
                        return value = property + " = in.read" + ti.ToTitleCase(data_type) + "();";
                    case "bool":
                        return value = property + " = in.readByte() != 0;";
                    case "DateTime":
                        return value = property + " = (Date) in.readSerializable();";
                    case "decimal":
                    case "Decimal":
                        return value = property + " = (BigDecimal) in.readSerializable();";
                    case "Byte[]":
                        return value = property + " = (Byte[]) in.readSerializable();";
                    default:
                        return value = property + " = in.readParcelable(" + data_type + ".class.getClassLoader());";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Java_Function_Format(string property)
        {
            try
            {
                property = ti.ToTitleCase(property).Replace("_","");

                return property;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Java_Is_Custom_Object(string data_type)
        {
            try
            {
                switch (data_type)
                {
                    case "bool":
                    case "DateTime":
                    case "decimal":
                    case "double":
                    case "int":
                    case "long":
                    case "string":
                    case "float":
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion



        #region Public Properties
        
        public string package_name { get; set; }
        public string output_folder { get; set; }
        public string rest_folder { get; set; }

        #endregion


    }
}
