using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typesafe.Mailgun;
using System.Net.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;


namespace GTSoft.CoreDotNet
{
    public class Mailgun
    {


        #region constructors

        public Mailgun(string _domain, string _api_private_key, string _api_public_key, string _url)
        {
            domain = _domain;
            api_private_key = _api_private_key;
            api_public_key = _api_public_key;
            url = _url;
        }

        #endregion


        #region public methods

        public void Send_Mail()
        {
            try
            {
                MailAddress from_address = new MailAddress(from, from_display, System.Text.Encoding.UTF8);
                MailMessage message = new MailMessage();
                string to_email, cc_email;

                message.From = from_address;

                if (to_emails != null)
                {
                    for (int i = 0; i <= to_emails.GetUpperBound(0); i++)
                    {
                        to_email = to_emails[i].ToString();

                        if (to_email.Trim() != "")
                            message.To.Add(to_email);
                    }
                }

                if (cc_emails != null)
                {
                    for (int i = 0; i <= cc_emails.GetUpperBound(0); i++)
                    {
                        cc_email = cc_emails[i].ToString();

                        if (cc_email.Trim() != "")
                            message.CC.Add(cc_email);
                    }
                }

                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = is_html;

                MailgunClient x = new MailgunClient(domain, api_private_key);
                SendMailCommandResult results = x.SendMail(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region private methods

        public bool Validate_Email(string email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(url);
            client.Authenticator = new HttpBasicAuthenticator("api", api_public_key);

            RestRequest request = new RestRequest();
            request.Resource = "/address/validate";
            request.AddParameter("address", email);

            IRestResponse response = client.Execute(request);
            Mailgun_Validation mailgun_validation_obj = JsonConvert.DeserializeObject<Mailgun_Validation>(response.Content);

            return mailgun_validation_obj.is_valid;
        }

        #endregion



        #region Class Property Declarations


        public string api_private_key { get; set; }
        public string api_public_key { get; set; }
        public string body { get; set; }
        public string domain { get; set; }
        public string from { get; set; }
        public string from_display { get; set; }
        public bool is_html { get; set; }
        public string subject { get; set; }
        public string url { get; set; }

        public string[] to_emails { get; set; }
        public string[] cc_emails { get; set; }

        #endregion


    }

    public class Mailgun_Validation
    {
        public bool is_valid { get; set; }
    }
}
