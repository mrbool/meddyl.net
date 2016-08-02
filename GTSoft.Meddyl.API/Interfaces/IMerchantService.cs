using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GTSoft.Meddyl.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMerchantService" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(JSONSuccessfulResponse))]
    [ServiceKnownType(typeof(JSONErrorResponse))]

    [ServiceKnownType(typeof(Application_Type))]
    [ServiceKnownType(typeof(Certificate))]
    [ServiceKnownType(typeof(Certificate_Payment))]
    [ServiceKnownType(typeof(Contact))]
    [ServiceKnownType(typeof(Credit_Card))]
    [ServiceKnownType(typeof(Customer))]
    [ServiceKnownType(typeof(Deal))]
    [ServiceKnownType(typeof(Deal_Payment))]
    [ServiceKnownType(typeof(Merchant_Contact))]
    [ServiceKnownType(typeof(Merchant_Contact_Validation))]
    [ServiceKnownType(typeof(Promotion))]
    [ServiceKnownType(typeof(Promotion_Activity))]
    [ServiceKnownType(typeof(System_Settings))]
    [ServiceKnownType(typeof(Zip_Code))]

    [ServiceKnownType(typeof(List<Credit_Card>))]
    [ServiceKnownType(typeof(List<Deal>))]
    [ServiceKnownType(typeof(List<Industry>))]
    [ServiceKnownType(typeof(List<Fine_Print_Option>))]
    [ServiceKnownType(typeof(List<Neighborhood>))]

    public interface IMerchantService
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

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "system/neighborhood")]
        JSONResponse Get_Neighborhood_By_Zip(Zip_Code zip_code_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "system/fine_print_options")]
        JSONResponse Get_Fine_Print_Options(Login_Log login_log_obj);

        #endregion


        #region merchant

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/create_validation")]
        JSONResponse Create_Validation(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/validate")]
        JSONResponse Validate(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/register")]
        JSONResponse Register(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/login")]
        JSONResponse Login(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/forgot_password")]
        JSONResponse Forgot_Password(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/details")]
        JSONResponse Get_Merchant_Contact(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/update")]
        JSONResponse Update_Merchant(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/update")]
        JSONResponse Update_Merchant_Contact(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/credit_card/add")]
        JSONResponse Add_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/credit_card/delete")]
        JSONResponse Delete_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/credit_card/set_default")]
        JSONResponse Set_Default_Credit_Card(Credit_Card credit_card_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/credit_card/get_all")]
        JSONResponse Get_Credit_Cards(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "merchant/merchant_contact/credit_card/get_default")]
        JSONResponse Get_Default_Credit_Card(Merchant_Contact merchant_contact_obj);

        #endregion


        #region deal

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/verify")]
        JSONResponse Verify_Deal(Deal deal_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/add")]
        JSONResponse Add_Deal(Deal deal_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/send_validation_code")]
        JSONResponse Send_Deal_Validation(Deal deal_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/validate")]
        JSONResponse Validate_Deal(Deal deal_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/deals")]
        JSONResponse Get_Deals(Merchant_Contact merchant_contact_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/detail")]
        JSONResponse Get_Deal_Details(Deal deal_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/cancel")]
        JSONResponse Cancel_Deal(Deal deal_obj);
        
        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificate/lookup")]
        JSONResponse Lookup_Certificate(Certificate certificate_obj);

        [OperationContract]
        [return: MessageParameter(Name = "JSONResponse")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "deal/certificate/redeem")]
        JSONResponse Redeem_Certificate(Certificate certificate_obj);

        #endregion

    }
}
