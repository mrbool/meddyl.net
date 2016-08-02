using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plivo.API;
using RestSharp;

namespace GTSoft.CoreDotNet
{
    public class Plivo
    {
        #region constructors

        public Plivo(string _auth_id, string _auth_token)
        {
            auth_id = _auth_id;
            auth_token = _auth_token;
        }

        #endregion


        #region public methods

        public void Send_SMS(string phone_from, string phone_to, string body)
        {
            RestAPI plivo = new RestAPI(auth_id, auth_token);

            IRestResponse<MessageResponse> response = plivo.send_message(new Dictionary<string, string>() 
            {
                { "src", "+1" + phone_from }, // Sender's phone number with country code
                { "dst", "+1" + phone_to }, // Receiver's phone number wiht country code
                { "text",body }, // Your SMS text message
                // To send Unicode text
                // {"text", "こんにちは、元気ですか？"} // Your SMS text message - Japanese
                // {"text", "Ce est texte généré aléatoirement"} // Your SMS text message - French
                { "url", "http://dotnettest.apphb.com/delivery_report"}, // The URL to which with the status of the message is sent
                { "method", "POST"} // Method to invoke the url
            });


            if (response.Data.error != null)
            {
                successful = false;
                message = response.Data.error;
            }
            else
            {
                successful = true;
                message = "";
            }
        }

        #endregion



        #region properties

        public bool successful { get; set; }
        public string message { get; set; }

        public string auth_id { get; set; }
        public string auth_token { get; set; }

        #endregion

    }
}
