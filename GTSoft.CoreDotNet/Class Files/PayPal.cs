using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal;

namespace GTSoft.CoreDotNet
{
    public class PayPal
    {
        #region members

        protected bool _successful;
        protected string _error_text;

        public string api_login_id { get; set; }
        public string transaction_key { get; set; }
        public string authorization_code { get; set; }
        public string response_code { get; set; }
        public string message { get; set; }
        public string transaction_code { get; set; }
        public string card_holder_name { get; set; }
        public string card_number { get; set; }
        public string card_date { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public bool approved { get; set; }

        #endregion



        #region constructors

        public PayPal()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion




        #region public methods

        public void Process_Charge()
        {
            try
            {
                PayPal x = new PayPal();


                x.api_login_id = "gtriarhos";

                x.card_number = "4147202229308208";
                x.card_holder_name = "George Triarhos";
                x.amount = 10;
                x.card_date = "0417";
                x.Process_Charge();

            }
            catch(Exception ex)
            {
                string error = ex.Message.ToString();
            }
            
        }

        #endregion
    }
}