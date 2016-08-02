using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet;
using Braintree;

// test from new pc

namespace GTSoft.CoreDotNet
{
    public class Credit_Card_Processing
    {
        #region members

        public bool successful { get; set; }
        public string error_text { get; set; }

        public string card_number { get; set; }
        public string security_code { get; set; }
        public string card_date { get; set; }
        public string customer_first_name { get; set; }
        public string customer_last_name { get; set; }
        public string customer_email { get; set; }
        public string customer_postal_code { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }

        public bool approved { get; set; }
        public string authorization_code { get; set; }
        public string response_code { get; set; }
        public string message { get; set; }
        public string transaction_code { get; set; }
        public string card_type { get; set; }

        #endregion



        #region constructors

        public Credit_Card_Processing()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion




        #region public methods

        public void Authorize_DotNet_Process_Charge(string api_login_id, string transaction_key)
        {
            try
            {
                // Step 1 - Create the request
                var request = new AuthorizationRequest(card_number, card_date, amount, description);

                // Step 2 - Create the gateway, sending in your credentials
                var gate = new Gateway(api_login_id, transaction_key);

                // Step 3 - Send the request to the gateway
                var response = gate.Send(request);
                approved = response.Approved;
                authorization_code = response.AuthorizationCode;
                response_code = response.ResponseCode;
                message = response.Message;
                transaction_code = response.TransactionID;
            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }

        public void Braintree_Process_Charge(string public_key, string private_key, string merchant_id)
        {
            try
            {
                Braintree_Gateway_Transaction(Braintree.Environment.PRODUCTION, public_key, private_key, merchant_id);
            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }

        public void Braintree_Sandbox_Process_Charge(string public_key, string private_key, string merchant_id)
        {
            try
            {
                Braintree_Gateway_Transaction(Braintree.Environment.SANDBOX, public_key, private_key, merchant_id);
            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }

        public void Braintree_Card_Verification(string public_key, string private_key, string merchant_id)
        {
            try
            {
                Braintree_Gateway_Verification(Braintree.Environment.PRODUCTION, public_key, private_key, merchant_id);
            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }

        public void Braintree_Sandbox_Card_Verification(string public_key, string private_key, string merchant_id)
        {
            try
            {
                Braintree_Gateway_Verification(Braintree.Environment.SANDBOX, public_key, private_key, merchant_id);
            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }

        public void IsValid_Credit_Card()
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(card_number))
            {
                successful = false;
            }
            else
            {
                //// 1.	Starting with the check digit double the value of every other digit 
                //// 2.	If doubling of a number results in a two digits number, add up
                ///   the digits to get a single digit number. This will results in eight single digit numbers                    
                //// 3. Get the sum of the digits
                int sumOfDigits = card_number.Where((e) => e >= '0' && e <= '9')
                                .Reverse()
                                .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                                .Sum((e) => e / 10 + e % 10);


                //// If the final sum is divisible by 10, then the credit card number
                //   is valid. If it is not divisible by 10, the number is invalid.     
                if (sumOfDigits % 10 == 0)
                    successful = true;
                else
                    successful = false;
            }
        }

        public void Get_Credit_Card_Type()
        {
            if ((card_number.Length == 15) && (card_number.Substring(0, 2) == "37"))
            {
                card_type = "American Express";
            }
            else if ((card_number.Length == 15) && (card_number.Substring(0, 2) == "34"))
            {
                card_type = "American Express";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 1) == "4"))
            {
                card_type = "Visa";
            }
            else if ((card_number.Length == 13)
                    && (card_number.Substring(0, 1) == "4"))
            {
                card_type = "Visa";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 2) == "51"))
            {
                card_type = "Master Card";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 2) == "52"))
            {
                card_type = "Master Card";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 2) == "53"))
            {
                card_type = "Master Card";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 2) == "54"))
            {
                card_type = "Master Card";
            }
            else if ((card_number.Length == 16)
                    && (card_number.Substring(0, 2) == "55"))
            {
                card_type = "Master Card";
            }
        }

        #endregion

        private void Braintree_Gateway_Transaction(Braintree.Environment environment, string public_key, string private_key, string merchant_id)
        {
            try
            {
                successful = true;
                error_text = "";

                BraintreeGateway gateway = new BraintreeGateway
                {
                    Environment = environment,
                    PublicKey = public_key,
                    PrivateKey = private_key,
                    MerchantId = merchant_id
                };

                var clientToken = gateway.ClientToken.generate();

                TransactionRequest transactionRequest = new TransactionRequest
                {
                    Amount = amount,
                    CreditCard = new TransactionCreditCardRequest
                    {
                        Number = card_number,
                        ExpirationMonth = card_date.Substring(0, 2),
                        ExpirationYear = card_date.Substring(2, 2),
                    }
                };
               
                Result<Braintree.Transaction> result = gateway.Transaction.Sale(transactionRequest);
                approved = result.IsSuccess();

                if (approved)
                {
                    authorization_code = result.Target.ProcessorAuthorizationCode;
                    response_code = result.Target.ProcessorResponseCode;
                    message = result.Target.ProcessorResponseText;
                    transaction_code = result.Target.Id;
                }
                else
                {
                    message = result.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Braintree_Gateway_Verification(Braintree.Environment environment, string public_key, string private_key, string merchant_id)
        {
            try
            {
                card_type = "";

                BraintreeGateway gateway = new BraintreeGateway
                {
                    Environment = environment,
                    PublicKey = public_key,
                    PrivateKey = private_key,
                    MerchantId = merchant_id
                };

                var clientToken = gateway.ClientToken.generate();

                var createRequest = new CustomerRequest
                {                    
                    Email = customer_email,
                    CreditCard = new CreditCardRequest
                    {
                        Number = card_number,
                        ExpirationDate = card_date,
                        CVV = security_code,
                        BillingAddress = new CreditCardAddressRequest
                        {
                            PostalCode = customer_postal_code
                        },
                        Options = new CreditCardOptionsRequest
                        {
                            VerifyCard = true
                        }
                    }
                };

                Result<Braintree.Customer> result = gateway.Customer.Create(createRequest);

                if (result.IsSuccess())
                {
                    VerificationStatus status = result.Target.CreditCards[0].Verification.Status;
                    string cvv_response = result.Target.CreditCards[0].Verification.CvvResponseCode;
                    string zip_response = result.Target.CreditCards[0].Verification.AvsPostalCodeResponseCode;
                    bool? expired = result.Target.CreditCards[0].IsExpired;
                    card_type = result.Target.CreditCards[0].CardType.ToString();

                    if (status == VerificationStatus.VERIFIED)
                    {
                        if (cvv_response != "M")
                        {
                            error_text = "Security code is invalid";
                            successful = false;
                        }
                        else
                        {
                            if (zip_response != "M")
                            {
                                error_text = "Zip code is invalid";
                                successful = false;
                            }
                            else
                            {
                                if (expired == true)
                                {
                                    error_text = "Credit card has expired";
                                    successful = false;
                                }
                                else
                                {
                                    message = result.Target.CreditCards[0].Verification.ProcessorResponseText.ToString();
                                    successful = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        error_text = result.Message;
                        successful = false;
                    }
                }
                else
                {
                    error_text = result.Message;
                    successful = false;
                }



                //var cc = new CreditCardRequest
                //   {
                //       //Number = "411111111111111x",
                //       CustomerId = "514889362",
                //       Number = "4147202229308208",
                //       ExpirationDate = "09/18",
                //       CVV = "264",
                //       BillingAddress = new CreditCardAddressRequest
                //       {
                //           PostalCode = "92014"
                //       },
                //       Options = new CreditCardOptionsRequest
                //       {
                //           VerifyCard = true
                //       }
                //   };

                //Result<Braintree.CreditCard> cc_request = gateway.CreditCard.Create(cc);

                //if (cc_request.IsSuccess())
                //{
                //    VerificationStatus status = cc_request.Target.Verification.Status;
                //    string cvv_response = cc_request.Target.Verification.CvvResponseCode;
                //    string zip_response = cc_request.Target.Verification.AvsPostalCodeResponseCode;
                //    bool? exp = cc_request.Target.IsExpired;

                //    if(status == VerificationStatus.VERIFIED)
                //    {
                //        successful = true;
                //    }
                //    else
                //    {

                //    }
                //}
                //else
                //{
                //    successful = false;
                //}


            }
            catch (Exception ex)
            {
                successful = false;
                error_text = ex.Message.ToString();
            }
        }
    }

}