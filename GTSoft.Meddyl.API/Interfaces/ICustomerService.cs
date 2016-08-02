using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GTSoft.Meddyl.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(JSONSuccessfulResponse))]
    [ServiceKnownType(typeof(JSONErrorResponse))]

    [ServiceKnownType(typeof(Application_Type))]
    [ServiceKnownType(typeof(Contact))]
    [ServiceKnownType(typeof(Certificate))]
    [ServiceKnownType(typeof(Certificate_Payment))]
    [ServiceKnownType(typeof(Credit_Card))]
    [ServiceKnownType(typeof(Customer))]
    [ServiceKnownType(typeof(Deal))]
    [ServiceKnownType(typeof(Promotion))]
    [ServiceKnownType(typeof(System_Settings))]

    [ServiceKnownType(typeof(List<Certificate>))]
    [ServiceKnownType(typeof(List<Credit_Card>))]
    [ServiceKnownType(typeof(List<Deal>))]
    [ServiceKnownType(typeof(List<Industry>))]
    [ServiceKnownType(typeof(List<Promotion_Activity>))]


    public interface ICustomerService
    {
        #region system

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "system/application_settings")]
        JSONResponse Get_Application_Settings(Login_Log login_log_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "system/system_settings")]
        JSONResponse Get_System_Settings(System_Settings system_settings_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "system/industry/parent")]
        JSONResponse Get_Industry_Parent_Level(Industry industry_obj);

        #endregion


        #region customer

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/register")]
        JSONResponse Register(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/login")]
        JSONResponse Login(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/forgot_password")]
        JSONResponse Forgot_Password(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/login_facebook")]
        JSONResponse Login_With_Facebook(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/profile")]
        JSONResponse Get_Customer_Profile(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/update")]
        JSONResponse Update_Customer(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/update_settings")]
        JSONResponse Update_Customer_Settings(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/credit_card/add")]
        JSONResponse Add_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/credit_card/delete")]
        JSONResponse Delete_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/credit_card/set_default")]
        JSONResponse Set_Default_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/credit_card/get_all")]
        JSONResponse Get_Credit_Cards(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "customer/valid_promotions")]
        JSONResponse Get_Valid_Promotions(Customer customer_obj);

        #endregion


        #region deal

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/deals/?customer_id={customer_id}&latitude={latitude}&longitude={longitude}")]
        JSONResponse Get_Deals(int customer_id, float latitude, float longitude);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/deal_detail")]
        JSONResponse Get_Deal_Detail(Certificate certificate_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificate/buy")]
        JSONResponse Buy_Certificate(Certificate_Payment certificate_payment_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificates/active")]
        JSONResponse Get_Customer_Active_Certificates(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificates")]
        JSONResponse Get_Customer_Certificates(Customer customer_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificate")]
        JSONResponse Get_Certificate_Detail(Certificate certificate_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/get_payment")]
        JSONResponse Get_Payment(Certificate certificate_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/apply_promotion")]
        JSONResponse Apply_Promotion(Certificate_Payment certificate_payment_obj);

        #endregion

    }
}
