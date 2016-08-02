using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace GTSoft.CoreDotNet
{
    public class Twilio
    {
        
        #region constructors

        public Twilio(string _account_sid, string _auth_token)
        {
            account_sid = _account_sid;
            auth_token = _auth_token;
        }

        #endregion


        #region public methods

        public void Send_SMS(string phone_from, string phone_to, string body)
        {

            var client = new TwilioRestClient(account_sid, auth_token);
            Message result = client.SendMessage("+1" + phone_from, "+1" + phone_to, body);
            //Message result = client.SendMessage("+18582390016", "+18586990966", "Hey, Monkey Party at 6PM. Bring Bananas!");
            //Message result = client.SendMessage("+15005550006", "+18586990966", "Hey, Monkey Party at 6PM. Bring Bananas!");

            if (result.RestException != null)
            {
                successful = false;
                message = result.RestException.Message;
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

        public string account_sid { get; set; }
        public string auth_token { get; set; }

        #endregion

    }
}
