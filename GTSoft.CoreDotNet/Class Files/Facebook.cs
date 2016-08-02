using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;

namespace GTSoft.CoreDotNet
{
    public class Facebook
    {
        #region members

        protected bool _successful;
        protected string _error_text;

        protected string _app_id;
        protected string _app_secret;
        protected string _scope;
        protected string _fb_login_url;
        protected string _redirect_url;
        protected string _code;
        protected string _access_token;
        protected long _user_id;

        #endregion


        
        #region constructors

        public Facebook()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion



        #region public methods

        public void Authenticate()
        {
            try
            {
                _fb_login_url = string.Format(
                        "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                        app_id, _redirect_url, scope);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Token()
        {
            try
            {

                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                    _app_id, _redirect_url, _scope, _code, _app_secret);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();

                    foreach (string token in vals.Split('&'))
                    {
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }
                }

                _access_token = tokens["access_token"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Profile_Favorite_Atheletes_Data(ref JsonTextReader jreader)
        {
            string token_type="", property;
            long id=0;
            string name="";

            favorite_athletes = new DataTable();
            favorite_athletes.Columns.Add("id", typeof(long));
            favorite_athletes.Columns.Add("name", typeof(string));

            while (token_type != "EndArray")
            {
                jreader.Read();
                token_type = jreader.TokenType.ToString();

                if (token_type == "PropertyName")
                {
                    property = jreader.Value.ToString();
                    jreader.Read();
                    token_type = jreader.TokenType.ToString();

                    switch (property)
                    {
                        case "id":
                            id = long.Parse(jreader.Value.ToString());
                            break;
                        case "name":
                            name = jreader.Value.ToString();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else if(token_type == "EndObject")
                {
                    favorite_athletes.Rows.Add(id, name);
                }
            }
        }

        public void Get_Profile_Location_Data(ref JsonTextReader jreader)
        {
            string token_type = "", property;
            long id = 0;
            string name = "";

            locations = new DataTable();
            locations.Columns.Add("id", typeof(long));
            locations.Columns.Add("name", typeof(string));

            while (token_type != "EndObject")
            {
                jreader.Read();
                token_type = jreader.TokenType.ToString();

                if (token_type == "PropertyName")
                {
                    property = jreader.Value.ToString();
                    jreader.Read();
                    token_type = jreader.TokenType.ToString();

                    switch (property)
                    {
                        case "id":
                            id = long.Parse(jreader.Value.ToString());
                            break;
                        case "name":
                            name = jreader.Value.ToString();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else if (token_type == "EndObject")
                {
                    locations.Rows.Add(id, name);
                }

            }
        }

        public void Get_Profile_Hometown_Data(ref JsonTextReader jreader)
        {
            string token_type = "", property;
            long id = 0;
            string name = "";

            hometowns = new DataTable();
            hometowns.Columns.Add("id", typeof(long));
            hometowns.Columns.Add("name", typeof(string));

            while (token_type != "EndObject")
            {
                jreader.Read();
                token_type = jreader.TokenType.ToString();

                if (token_type == "PropertyName")
                {
                    property = jreader.Value.ToString();
                    jreader.Read();
                    token_type = jreader.TokenType.ToString();

                    switch (property)
                    {
                        case "id":
                            id = long.Parse(jreader.Value.ToString());
                            break;
                        case "name":
                            name = jreader.Value.ToString();
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }
                else if (token_type == "EndObject")
                {
                    hometowns.Rows.Add(id, name);
                }

            }
        }

        public void Get_Profile_Data()
        {
            try
            {
                string token_type, property;

                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/me/?fields=id,name,email,birthday,first_name,last_name,gender,hometown,link,locale,location,middle_name,timezone,verified,updated_time &access_token={0}",
                    _access_token);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string vals = reader.ReadToEnd();

                    JsonTextReader jreader = new JsonTextReader(new StringReader(vals));
                    while (jreader.Read())
                    {
                        token_type = jreader.TokenType.ToString();

                        if (token_type == "PropertyName")
                        {
                            property = jreader.Value.ToString();
                            jreader.Read();
                            token_type = jreader.TokenType.ToString();

                            switch (property)
                            {
                                case "id":
                                    id = long.Parse(jreader.Value.ToString());
                                    break;
                                case "birthday":
                                    birthday = jreader.Value.ToString();
                                    break;
                                case "email":
                                    email = jreader.Value.ToString();
                                    break;
                                case "favorite_athletes":
                                    Get_Profile_Favorite_Atheletes_Data(ref jreader);
                                    break;
                                case "first_name":
                                    first_name = jreader.Value.ToString();
                                    break;
                                case "gender":
                                    gender = jreader.Value.ToString();
                                    break;
                                case "hometown":
                                    Get_Profile_Hometown_Data(ref jreader);
                                    break;
                                case "last_name":
                                    last_name = jreader.Value.ToString();
                                    break;
                                case "link":
                                    link = jreader.Value.ToString();
                                    break;
                                case "locale":
                                    locale = jreader.Value.ToString();
                                    break;
                                case "location":
                                    Get_Profile_Location_Data(ref jreader);
                                    break;
                                case "middle_name":
                                    middle_name = jreader.Value.ToString();
                                    break;
                                case "name":
                                    name = jreader.Value.ToString();
                                    break;
                                case "timezone":
                                    timezone = jreader.Value.ToString();
                                    break;
                                case "updated_time":
                                    updated_time = jreader.Value.ToString();
                                    break;
                                case "username":
                                    username = jreader.Value.ToString();
                                    break;
                                case "verified":
                                    verified = jreader.Value.ToString();
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }
                        }
                    }
                }

                _successful = true;
                _error_text = "";
            }
            catch (Exception ex)
            {
                _successful = false;
                _error_text = ex.Message;
            }
        }

        public void Get_Friends_Data()
        {
            try
            {
                dt_friends = new DataTable();
                dt_friends.Columns.Add("id", typeof(long));
                dt_friends.Columns.Add("name", typeof(string));

                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/v2.2/me/friends?access_token={0}", _access_token);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string vals = reader.ReadToEnd();

                    JsonTextReader jreader = new JsonTextReader(new StringReader(vals));

                    string token_type = "", property = "";
                    while (token_type != "EndArray")
                    {
                        jreader.Read();
                        token_type = jreader.TokenType.ToString();

                        if (token_type == "PropertyName")
                        {
                            property = jreader.Value.ToString();
                            jreader.Read();
                            token_type = jreader.TokenType.ToString();

                            switch (property)
                            {
                                case "id":
                                    id = long.Parse(jreader.Value.ToString());
                                    break;
                                case "name":
                                    name = jreader.Value.ToString();
                                    break;
                                default:
                                    Console.WriteLine("Default case");
                                    break;
                            }
                        }
                        else if (token_type == "EndObject")
                        {
                            dt_friends.Rows.Add(id, name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Music_Data()
        {
            try
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/1067954818/music/?access_token={0}",
                    _access_token);
                HttpWebRequest requestx = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse responsex = requestx.GetResponse() as HttpWebResponse)
                {
                    StreamReader readerx = new StreamReader(responsex.GetResponseStream());
                    string vals = readerx.ReadToEnd();
                }

                _access_token = tokens["access_token"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Permissions_Data()
        {
            try
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/me/permissions/?access_token={0}",
                    _access_token);
                HttpWebRequest requestx = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse responsex = requestx.GetResponse() as HttpWebResponse)
                {
                    StreamReader readerx = new StreamReader(responsex.GetResponseStream());
                    string vals = readerx.ReadToEnd();
                }

                _access_token = tokens["access_token"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion



        #region properties

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

        public string app_id
        {
            get
            {
                return _app_id;
            }
            set
            {
                _app_id = value;
            }
        }

        public string app_secret
        {
            get
            {
                return _app_secret;
            }
            set
            {
                _app_secret = value;
            }
        }

        public string scope
        {
            get
            {
                return _scope;
            }
            set
            {
                _scope = value;
            }
        }

        public string fb_login_url
        {
            get
            {
                return _fb_login_url;
            }
            set
            {
                _fb_login_url = value;
            }
        }

        public string redirect_url
        {
            get
            {
                return _redirect_url;
            }
            set
            {
                _redirect_url = value;
            }
        }

        public string code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        public string access_token
        {
            get
            {
                return _access_token;
            }
            set
            {
                _access_token = value;
            }
        }

        
        public long id {get; set;}
        public string birthday { get; set; }     
        public string email { get; set; }
        public string first_name { get; set; }
        public string gender { get; set; }
        public string last_name { get; set; }
        public string link {get; set;}   
        public string locale {get; set;}   
        public string middle_name {get; set;}   
        public string name {get; set;}
        public string timezone { get; set; }   
        public string updated_time {get; set;}   
        public string username {get; set;}     
        public string verified {get; set;}

        public DataTable dt_friends { get; set; }
        public DataTable locations { get; set; }
        public DataTable hometowns { get; set; }
        public DataTable favorite_athletes { get; set; }
        
        #endregion


    }
}
