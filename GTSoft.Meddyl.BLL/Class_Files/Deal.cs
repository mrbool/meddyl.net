using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using GTSoft.CoreDotNet;
using GTSoft.Meddyl.DAL;


namespace GTSoft.Meddyl.BLL
{
    public class Deal
    {
        #region constructors

        public Deal()
        {
            if (deal_dal == null)
                deal_dal = new DAL.Deal();

            if (merchant_bll == null)
                merchant_bll = new Merchant();

            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();

            system_bll = new System();
        }
     
        public Deal(DAL.Deal _deal_dal)
        {
            try
            {
                deal_dal = _deal_dal;

                if (deal_dal.deal_status_dal == null)
                    deal_dal.deal_status_dal = new DAL.Deal_Status();

                if (deal_dal.deal_validation_dal == null)
                    deal_dal.deal_validation_dal = new DAL.Deal_Validation();

                if (certificate_dal == null)
                    certificate_dal = new Certificate();

                if (deal_dal.promotion_activity_dal == null)
                {
                    promotion_activity_dal = new Promotion_Activity();
                }
                else
                {
                    promotion_activity_dal = deal_dal.promotion_activity_dal;

                    if (promotion_activity_dal.promotion_dal == null)
                        promotion_activity_dal.promotion_dal = new Promotion();
                }

                if (deal_log_dal == null)
                    deal_log_dal = new DAL.Deal_Log();

                if ((!deal_dal.deal_id.IsNull) && (deal_dal.deal_id != 0))
                    Get_Deal();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Deal _deal_dal, DAL.Merchant_Contact _merchant_contact_dal, bool load_deal_data)
        {
            try
            {
                if (_deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = _deal_dal;

                if (deal_dal.deal_status_dal == null)
                    deal_dal.deal_status_dal = new DAL.Deal_Status();

                if (deal_dal.deal_validation_dal == null)
                    deal_dal.deal_validation_dal = new DAL.Deal_Validation();

                if (certificate_dal == null)
                    certificate_dal = new Certificate();

                if (promotion_activity_dal == null)
                {
                    promotion_activity_dal = new Promotion_Activity();
                    promotion_activity_dal.promotion_dal = new Promotion();
                }

                if (deal_dal.promotion_activity_dal != null)
                {
                    promotion_activity_dal.promotion_dal = deal_dal.promotion_activity_dal.promotion_dal;
                }

                if (deal_log_dal == null)
                    deal_log_dal = new DAL.Deal_Log();

                // load properties
                if (load_deal_data)
                {
                    if ((!deal_dal.deal_id.IsNull) && (deal_dal.deal_id != 0))
                        Get_Deal();
                }

                // instantiate business layer
                merchant_bll = new Merchant(_merchant_contact_dal);

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Deal _deal_dal, DAL.Customer _customer_dal)
        {
            try
            {
                if (_deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = _deal_dal;

                customer_bll = new Customer(_customer_dal);

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Certificate _certificate_dal)
        {
            try
            {
                certificate_dal = _certificate_dal;

                if (certificate_dal.deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = certificate_dal.deal_dal;

                if (!deal_dal.deal_id.IsNull)
                    Get_Deal();

                if (!certificate_dal.certificate_id.IsNull)
                    Load_Certificate_Properties();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Certificate _certificate_dal, DAL.Merchant_Contact _merchant_contact)
        {
            try
            {
                certificate_dal = _certificate_dal;

                if (certificate_dal.deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = certificate_dal.deal_dal;

                if (!deal_dal.deal_id.IsNull)
                    Get_Deal();

                if (!certificate_dal.certificate_id.IsNull)
                    Load_Certificate_Properties();

                if(_merchant_contact != null)
                    merchant_bll = new BLL.Merchant(_merchant_contact);

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Certificate _certificate_dal, DAL.Customer _customer_dal)
        {
            try
            {
                if (_certificate_dal == null)
                    certificate_dal = new Certificate();
                else
                    certificate_dal = _certificate_dal;

                if (certificate_dal.deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = certificate_dal.deal_dal;

                certificate_payment_dal = new Certificate_Payment();

                if ((_customer_dal != null) && (_customer_dal.customer_id != 0))
                    customer_bll = new BLL.Customer(_customer_dal);

                if (!deal_dal.deal_id.IsNull)
                {
                    deal_dal.customer_dal = _customer_dal;
                    Get_Deal_Log_Customer();
                }

                if (!certificate_dal.certificate_id.IsNull)
                    Load_Certificate_Properties();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Certificate_Payment _certificate_payment_dal)
        {
            try
            {
                certificate_payment_dal = _certificate_payment_dal;

                if (certificate_payment_dal.certificate_dal == null)
                    certificate_dal = new DAL.Certificate();
                else
                    certificate_dal = certificate_payment_dal.certificate_dal;

                if (certificate_dal.deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = certificate_payment_dal.certificate_dal.deal_dal;

                if (promotion_activity_dal == null)
                {
                    promotion_activity_dal = new DAL.Promotion_Activity();
                    promotion_activity_dal.promotion_dal = new DAL.Promotion();
                }

                if (certificate_payment_dal.promotion_activity_dal != null)
                {
                    promotion_activity_dal = certificate_payment_dal.promotion_activity_dal;
                    promotion_activity_dal.promotion_dal = certificate_payment_dal.promotion_activity_dal.promotion_dal;
                }

                payment_log_dal = new DAL.Payment_Log();

                customer_bll = new BLL.Customer(certificate_payment_dal.credit_card_dal);

                Get_Deal();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new BLL.System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Deal_Payment _deal_payment_dal)
        {
            try
            {
                deal_payment_dal = _deal_payment_dal;
                deal_dal = deal_payment_dal.deal_dal;

                payment_log_dal = new Payment_Log();
                
                merchant_bll = new BLL.Merchant(deal_payment_dal.credit_card_dal);

                Get_Deal();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Deal(DAL.Deal_Log _deal_log_dal)
        {
            try
            {
                deal_log_dal = _deal_log_dal;

                if (deal_log_dal.deal_dal == null)
                    deal_log_dal.deal_dal = new DAL.Deal();
                else
                    deal_dal = deal_log_dal.deal_dal;

                if (deal_log_dal.deal_dal == null)
                    deal_dal = new DAL.Deal();
                else
                    deal_dal = deal_log_dal.deal_dal;

                if (deal_log_dal.merchant_contact_dal != null)
                {
                    merchant_bll = new BLL.Merchant(deal_dal.merchant_contact_dal);
                    //merchant_bll.Load_Merchant_Contact_Properties();
                }

                Get_Deal();

                system_error_dal = new DAL.System_Error();
                system_successful_dal = new DAL.System_Successful();

                system_bll = new System();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region public methods

        public void Get_Fine_Print_Options()
        {
            try
            {
                fine_print_option_dal_array = new List<DAL.Fine_Print_Option>();

                DAL.Fine_Print_Option fine_print_option_dal = new Fine_Print_Option();
                DataTable dt = fine_print_option_dal.usp_Fine_Print_Option_SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    fine_print_option_dal = new DAL.Fine_Print_Option();
                    fine_print_option_dal.option_id = int.Parse(dr["Fine_Print_Option_option_id"].ToString());
                    fine_print_option_dal.display = dr["Fine_Print_Option_display"].ToString();
                    fine_print_option_dal.value = dr["Fine_Print_Option_value"].ToString();
                    fine_print_option_dal.is_selected = bool.Parse(dr["Fine_Print_Option_is_selected"].ToString());

                    fine_print_option_dal_array.Add(fine_print_option_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Deal_Status()
        {
            try
            {
                deal_status_dal_array = new List<Deal_Status>();

                deal_status_dal = new Deal_Status();
                DataTable dt = deal_status_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    deal_status_dal = new Deal_Status();

                    Load_Deal_Status_Properties(dr);

                    deal_status_dal_array.Add(deal_status_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Certificate_Status()
        {
            try
            {
                certificate_status_dal_array = new List<DAL.Certificate_Status>();

                certificate_status_dal = new Certificate_Status();
                DataTable dt = certificate_status_dal.SelectAll();
                foreach (DataRow dr in dt.Rows)
                {
                    certificate_status_dal = new Certificate_Status();

                    Load_Certificate_Status_Properties(dr);

                    certificate_status_dal_array.Add(certificate_status_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Verify_Deal()
        {
            try
            {
                TimeSpan ts = (DateTime.Parse(deal_dal.expiration_date.ToString()) - DateTime.UtcNow);

                //if (merchant_bll.merchant_contact_dal.merchant_contact_status_dal.status_id != 1)
                //{
                //    successful = false;
                //    system_error_dal = system_bll.Get_System_Error(2008, "");
                //}

                    
                if ((deal_dal.percent_off > system_bll.system_settings_dal.percent_off_max) || (deal_dal.percent_off < system_bll.system_settings_dal.percent_off_min))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2001, "Percent off must be between " + system_bll.system_settings_dal.percent_off_min.ToString() + " and " + system_bll.system_settings_dal.percent_off_max.ToString());
                }
                else if ((deal_dal.max_dollar_amount > system_bll.system_settings_dal.dollar_value_max) || (deal_dal.max_dollar_amount < system_bll.system_settings_dal.dollar_value_min))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2002, "Max dollar value must be between " + string.Format("{0:C}", system_bll.system_settings_dal.dollar_value_min) + " and " + string.Format("{0:C}", system_bll.system_settings_dal.dollar_value_max));
                }
                else if ((deal_dal.certificate_quantity > system_bll.system_settings_dal.certificate_quantity_max) || (deal_dal.certificate_quantity < system_bll.system_settings_dal.certificate_quantity_min))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2003, "Number of certificates must be between " + system_bll.system_settings_dal.certificate_quantity_min.ToString() + " and " + system_bll.system_settings_dal.certificate_quantity_max.ToString());
                }
                else if (ts.TotalDays < 1)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2022, "");
                }
                else if (ts.TotalDays > int.Parse(system_bll.system_settings_dal.expiration_days_max.ToString()))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2004, "Expiration date cannot be greater than " + system_bll.system_settings_dal.expiration_days_max.ToString() + " days");
                }
                else
                {
                    deal_dal.usp_Deal_Text_Fine_Print_Text();
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add()
        {
            try
            {
                bool new_deal = false;
                bool has_credit_card = false;

                Verify_Deal();

                if (successful)
                {
                    if(system_bll.system_settings_dal.deal_needs_credit_card)
                    {
                        merchant_bll.Get_Default_Credit_Card();

                        if (!merchant_bll.credit_card_dal.credit_card_id.IsNull)
                            has_credit_card = true;
                    }
                    else
                    {
                        has_credit_card = true;
                    }

                    promotion_activity_dal.promotion_dal.system_error_code = 0;
                    if (promotion_activity_dal.promotion_dal.promotion_code.ToString().Trim() != "")
                    {
                        if (deal_dal.deal_id != 0)
                            promotion_activity_dal.promotion_dal.deal_id = deal_dal.deal_id;
                        promotion_activity_dal.promotion_dal.merchant_contact_id = merchant_bll.merchant_contact_dal.merchant_contact_id;
                        promotion_activity_dal.promotion_dal.usp_Promotion_Validate_Merchant();
                    }

                    if ((merchant_bll.merchant_dal.current_active_deals >= merchant_bll.merchant_dal.max_active_deals) && (deal_dal.deal_id == 0))
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2005, "");
                    }
                    else if (promotion_activity_dal.promotion_dal.system_error_code != 0)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(int.Parse(promotion_activity_dal.promotion_dal.system_error_code.ToString()), "");
                    }
                    else
                    {
                        if (deal_dal.deal_id != 0)
                        {
                            DataTable dt = deal_dal.usp_Deal_SelectPK_deal_id();
                            deal_dal.deal_status_dal.status_id = int.Parse(dt.Rows[0]["Deal_status_id"].ToString());
                        }

                        if (!has_credit_card)
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(2025, "");
                        }
                        else if (deal_dal.deal_id != 0 && (deal_dal.deal_status_dal.status_id != 6) && (deal_dal.deal_status_dal.status_id != 5))
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(2016, "");
                        }
                        else
                        {
                            deal_dal.deal_status_dal.status_id = 5;

                            deal_dal.status_id = deal_dal.deal_status_dal.status_id;
                            deal_dal.merchant_contact_id = merchant_bll.merchant_contact_dal.merchant_contact_id;
                            deal_dal.promotion_code = promotion_activity_dal.promotion_dal.promotion_code;
                            if (deal_dal.deal_id == 0)
                            {
                                new_deal = true;
                                deal_dal.usp_Deal_Insert();
                            }
                            else
                            {
                                new_deal = false;
                                deal_dal.usp_Deal_Update();
                            }

                            string image = system_bll.Save_Image(deal_dal.image_base64.ToString(), int.Parse(deal_dal.deal_id.ToString()), "deal");
                            deal_dal.image = image;
                            deal_dal.usp_Deal_UpdatePK_image();
                            
                            // create validation code
                            DAL.Deal_Validation deal_validation_dal = new Deal_Validation();
                            deal_validation_dal.deal_id = deal_dal.deal_id;
                            deal_validation_dal.usp_Deal_Validation_Insert();

                            SMS_Validation(deal_validation_dal.validation_code.ToString());

                            if (!successful)
                            {
                                system_error_dal = system_bll.Get_System_Error(2021, "Please contact " + system_bll.system_settings_dal.email_admin.ToString());
                            }
                            else
                            {
                                deal_log_dal.notes = "";
                                Log_Deal();

                                successful = true;
                                if(new_deal)
                                    system_successful_dal = system_bll.Get_System_Successful(2005, "");
                                else
                                    system_successful_dal = system_bll.Get_System_Successful(2007, "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send_Validation_Code()
        {
            try
            {
                if (deal_dal.deal_status_dal.status_id != 5)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2024, "");
                }
                else
                {
                    // create validation code
                    DAL.Deal_Validation deal_validation_dal = new Deal_Validation();
                    deal_validation_dal.deal_id = deal_dal.deal_id;
                    deal_validation_dal.usp_Deal_Validation_Insert();

                    SMS_Validation(deal_validation_dal.validation_code.ToString());

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(2006, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Validate()
        {
            try
            {
                if (deal_dal.deal_status_dal.status_id != 5)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2024, "");
                }
                else
                {
                    deal_dal.deal_validation_dal.deal_id = deal_dal.deal_id;
                    deal_dal.deal_validation_dal.usp_Deal_Validate();

                    if (deal_dal.deal_validation_dal.validation_id == 0)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2010, "");
                    }
                    else
                    {
                        Get_Deal();

                        deal_log_dal.notes = "";
                        Log_Deal();

                        Email_Admin_Approval();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(2000, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Approve_Deal()
        {
            try
            {
                if (deal_dal.deal_status_dal.status_id == 5)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2012, "");
                }
                else
                {
                    certificate_dal.deal_id = deal_dal.deal_id;
                    DataTable dt = certificate_dal.SelectFK_Deal_deal_id();

                    if (dt.Rows.Count > 0)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2015, "");
                    }
                    else
                    {
                        deal_dal.usp_Deal_Approve();

                        Get_Deal();

                        if (deal_dal.deal_status_dal.status_id != 1)
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(2009, "");
                        }
                        else
                        {
                            deal_log_dal.notes = "";
                            Log_Deal();

                            Email_Live_Deal();

                            successful = true;
                            system_successful_dal = system_bll.Get_System_Successful(2003, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Reject_Deal()
        {
            try
            {
                if (deal_dal.deal_status_dal.status_id != 4)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2014, "");
                }
                else
                {
                    deal_dal.usp_Deal_Reject();

                    Get_Deal();

                    if (deal_dal.deal_status_dal.status_id != 6)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2011, "");
                    }
                    else
                    {
                        Log_Deal();

                        Email_Deal_Reject();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(2004, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancel_Deal()
        {
            try
            {
                switch (int.Parse(deal_dal.deal_status_dal.status_id.ToString()))
                {
                    case 2:
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2018, "");

                        break;
                    case 3:
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2019, "");

                        break;
                    case 1:
                    case 4:
                    case 5:
                    case 6:
                        deal_dal.usp_Deal_Cancel_deal_id_merchant_contact_id();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(2002, "");

                        break;
                    default:
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(2020, "");

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Merchant_Deals()
        {
            try
            {
                deal_dal_array = new List<DAL.Deal>();

                deal_dal.merchant_contact_id = merchant_bll.merchant_contact_dal.merchant_contact_id;
                DataTable dt = deal_dal.usp_Deal_Merchant_merchant_contact_id();
                foreach (DataRow dr in dt.Rows)
                {
                    deal_dal = new DAL.Deal();
                    deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                    Load_Deal_Properties(dr);

                    deal_dal_array.Add(deal_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Deals_Pending_Approval()
        {
            try
            {
                deal_dal_array = new List<DAL.Deal>();

                deal_dal = new DAL.Deal();
                deal_dal.status_id = 4;
                DataTable dt = deal_dal.usp_Deal_SelectFK_Deal_Status_status_id();
                foreach (DataRow dr in dt.Rows)
                {
                    deal_dal = new DAL.Deal();
                    deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                    Load_Deal_Properties(dr);

                    deal_dal_array.Add(deal_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Deals_Pending_Payment()
        {
            try
            {
                deal_dal_array = new List<DAL.Deal>();

                deal_dal = new DAL.Deal();
                DataTable dt = deal_dal.usp_Deals_Pending_Payment();

                foreach (DataRow dr in dt.Rows)
                {
                    deal_dal = new DAL.Deal();
                    deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                    Load_Deal_Properties(dr);

                    deal_dal_array.Add(deal_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Deals_Search()
        {
            try
            {
                deal_dal_array = new List<DAL.Deal>();

                DataTable dt = deal_dal.usp_Deal_Search();
                foreach (DataRow dr in dt.Rows)
                {
                    deal_dal = new DAL.Deal();
                    deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                    Load_Deal_Properties(dr);

                    deal_dal_array.Add(deal_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Fine_Print_Options_By_Deal()
        {
            try
            {
                fine_print_option_dal_array = new List<DAL.Fine_Print_Option>();

                DAL.Deal_Fine_Print_Option deal_fine_print_option_dal = new Deal_Fine_Print_Option();
                deal_fine_print_option_dal.deal_id = deal_dal.deal_id;
                DataTable dt = deal_fine_print_option_dal.usp_Deal_Fine_Print_Option_SelectFK_Deal_deal_id();
                foreach (DataRow dr in dt.Rows)
                {
                    DAL.Fine_Print_Option fine_print_option_dal = new DAL.Fine_Print_Option();
                    fine_print_option_dal.option_id = int.Parse(dr["Fine_Print_Option_option_id"].ToString());
                    fine_print_option_dal.display = dr["Fine_Print_Option_display"].ToString();
                    fine_print_option_dal.value = dr["Fine_Print_Option_value"].ToString();
                    fine_print_option_dal.is_selected = bool.Parse(dr["Fine_Print_Option_is_selected"].ToString());

                    fine_print_option_dal_array.Add(fine_print_option_dal);
                }

                deal_dal.fine_print_option_dal_array = fine_print_option_dal_array;

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Deal_Status(int status_id)
        {
            try
            {
                deal_dal.status_id = status_id;
                deal_dal.usp_Deal_UpdatePK_status_id();

                deal_log_dal.notes = "";
                Log_Deal();

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Deal_Ranking(int ranking)
        {
            try
            {
                int deal_min_ranking = int.Parse(system_bll.system_settings_dal.deal_min_ranking.ToString());
                int deal_max_ranking = int.Parse(system_bll.system_settings_dal.deal_max_ranking.ToString());

                if ((ranking < deal_min_ranking) || (ranking > deal_max_ranking))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2017, "");
                }
                else
                {
                    deal_dal.ranking = ranking;
                    deal_dal.usp_Deal_UpdatePK_ranking();

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Deal_Promotion()
        {
            try
            {
                DataTable dt = promotion_activity_dal.promotion_dal.SelectUnique_promotion_code();

                if (dt.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4003, "");
                }
                else if (!deal_dal.promotion_activity_dal.promotion_activity_id.IsNull)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4023, "");
                }
                else if (int.Parse(dt.Rows[0]["Promotion_Type_promotion_type_id"].ToString()) != 4)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4024, "");
                }
                else
                {
                    promotion_activity_dal.promotion_dal.expiration_date = DateTime.Parse(dt.Rows[0]["Promotion_expiration_date"].ToString());

                    if (DateTime.UtcNow > promotion_activity_dal.promotion_dal.expiration_date)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(4004, "");
                    }
                    else
                    {
                        deal_dal.usp_Deal_UpdatePK_promotion_code();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(5000, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deal_Payment(string payment_process_type)
        {
            try
            {
                if (deal_dal.deal_amount != 0)
                {
                    GTSoft.CoreDotNet.Credit_Card_Processing cc_processing = new Credit_Card_Processing();
                    cc_processing.card_number = merchant_bll.credit_card_dal.card_number_decrypted.ToString();
                    cc_processing.card_date = merchant_bll.credit_card_dal.expiration_date.ToString();
                    cc_processing.amount = decimal.Parse(deal_dal.deal_amount.ToString());
                    cc_processing.description = "";

                    if (system_bll.system_settings_dal.credit_card_system.ToString() == "authorize.net")
                    {
                        cc_processing.Authorize_DotNet_Process_Charge(system_bll.system_settings_dal.auth_net_api_login_id.ToString(), system_bll.system_settings_dal.auth_net_transaction_key.ToString());
                    }
                    else if (system_bll.system_settings_dal.credit_card_system == "braintree")
                    {
                        cc_processing.Braintree_Process_Charge(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                    }
                    else if (system_bll.system_settings_dal.credit_card_system == "braintree_sandbox")
                    {
                        cc_processing.Braintree_Sandbox_Process_Charge(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                    }

                    payment_log_dal.notes = cc_processing.message;
                    successful = cc_processing.successful;

                    if (successful)
                    {
                        if (cc_processing.approved)
                        {
                            deal_payment_dal = new DAL.Deal_Payment();
                            deal_payment_dal.payment_amount = decimal.Parse(deal_dal.deal_amount.ToString());
                            deal_payment_dal.deal_id = deal_dal.deal_id;
                            deal_payment_dal.credit_card_id = merchant_bll.credit_card_dal.credit_card_id;
                            deal_payment_dal.payment_amount = deal_dal.deal_amount;
                            deal_payment_dal.card_holder_name = merchant_bll.credit_card_dal.card_holder_name;
                            deal_payment_dal.card_number = merchant_bll.credit_card_dal.card_number;
                            deal_payment_dal.card_expiration_date = merchant_bll.credit_card_dal.expiration_date;
                            deal_payment_dal.Insert();

                            Email_Deal_Payment();

                            successful = true;
                            system_successful_dal = system_bll.Get_System_Successful(2001, cc_processing.message);
                        }
                        else
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(2006, cc_processing.message);
                        }
                    }
                    else
                    {
                        system_error_dal = system_bll.Get_System_Error(2006, cc_processing.error_text);
                    }

                    payment_log_dal = new Payment_Log();
                    payment_log_dal.notes = cc_processing.message;
                    Log_Payment(payment_process_type);
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2023, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deal_Payment_Batch()
        {
            try
            {
                DataTable dt_pending = deal_dal.usp_Deals_Pending_Payment();
                foreach (DataRow dr_pending in dt_pending.Rows)
                {
                    deal_dal.deal_id = int.Parse(dr_pending["Deal_deal_id"].ToString());

                    Load_Deal_Properties(dr_pending);

                    merchant_bll = new Merchant();
                    merchant_bll.merchant_contact_dal.merchant_contact_id = int.Parse(dr_pending["Merchant_Contact_merchant_contact_id"].ToString());
                    merchant_bll.Get_Default_Credit_Card();

                    Deal_Payment("batch");
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Certificate_Search()
        {
            try
            {
                certificate_dal_array = new List<DAL.Certificate>();

                DataTable dt = certificate_dal.usp_Certificate_Search();
                foreach (DataRow dr in dt.Rows)
                {
                    certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_id = int.Parse(dr["Certificate_certificate_id"].ToString());

                    Load_Certificate_Properties();

                    certificate_dal_array.Add(certificate_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Lookup_Certificate()
        {
            try
            {
                certificate_dal.merchant_contact_id = merchant_bll.merchant_contact_dal.merchant_contact_id;
                DataTable dt = certificate_dal.usp_Certificate_Lookup_merchant_contact_id_certificate_code();

                if (dt.Rows.Count == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4000, "");
                }
                else if(dt.Rows[0]["Certificate_customer_id"] == DBNull.Value)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4001, "");
                }
                else
                {
                    certificate_dal.certificate_id = int.Parse(dt.Rows[0]["Certificate_certificate_id"].ToString());

                    Load_Certificate_Payment_Properties(dt);
                    Load_Certificate_Properties();

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Redeem_Certificate()
        {
            try
            {
                Lookup_Certificate();

                if(successful)
                {
                    if (certificate_dal.certificate_status_dal.status_id == 3)
                    {
                        certificate_dal.usp_Certificate_Redeem_merchant_contact_id_certificate_id();

                        Email_After_Redeem();

                        successful = true;
                        system_successful_dal = system_bll.Get_System_Successful(3000, "");
                    }
                    else
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(4025, "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Customer_Deals()
        {
            try
            {
                if (customer_bll.customer_dal.customer_search_location_type_dal.search_location_type_id == 1 && customer_bll.contact_dal.zip_code_dal.latitude == 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(2026, "");
                }
                else
                {

                    deal_dal_array = new List<DAL.Deal>();

                    if (customer_bll.customer_dal.customer_id.IsNull)
                        deal_dal.customer_id = 0;
                    else
                        deal_dal.customer_id = customer_bll.customer_dal.customer_id;
                    deal_dal.latitude = customer_bll.contact_dal.zip_code_dal.latitude;
                    deal_dal.longitude = customer_bll.contact_dal.zip_code_dal.longitude;
                    DataTable dt = deal_dal.usp_Deals_Select_By_customer_id_location();
                    foreach (DataRow dr in dt.Rows)
                    {
                        deal_dal = new DAL.Deal();
                        deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                        Load_Deal_Properties(dr);

                        deal_dal_array.Add(deal_dal);
                    }

                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Customer_Active_Certificates()
        {
            try
            {
                certificate_dal_array = new List<DAL.Certificate>();

                certificate_dal.status_id = 1;
                certificate_dal.customer_id = customer_bll.customer_dal.customer_id;
                DataTable dt = certificate_dal.usp_Certificate_Active_SelectFK_Customer_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_id = int.Parse(dr["Certificate_certificate_id"].ToString());

                    Load_Certificate_Properties();

                    certificate_dal_array.Add(certificate_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Customer_Certificates()
        {
            try
            {
                certificate_dal_array = new List<DAL.Certificate>();

                certificate_dal.customer_id = customer_bll.customer_dal.customer_id;
                DataTable dt = certificate_dal.usp_Certificate_SelectFK_Customer_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    certificate_dal = new DAL.Certificate();
                    certificate_dal.certificate_id = int.Parse(dr["Certificate_certificate_id"].ToString());

                    Load_Certificate_Properties();

                    certificate_dal_array.Add(certificate_dal);
                }

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Buy_Certificate()
        {
            try
            {
                DataTable dt_user_merchant = null;

                deal_dal.customer_id = customer_bll.customer_dal.customer_id;
                DataTable dt_used_deal = deal_dal.usp_Deal_Select_deal_id_customer_id();

                if (deal_dal.is_valid_new_customer_only)
                {
                    certificate_dal.customer_id = customer_bll.customer_dal.customer_id;
                    certificate_dal.deal_id = deal_dal.deal_id;
                    dt_user_merchant = certificate_dal.usp_Certificate_Select_Merchant_deal_id_customer_id();
                }

                if (dt_used_deal.Rows.Count > 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4005, "");
                }
                else if ((dt_user_merchant != null) && (dt_user_merchant.Rows.Count > 0))
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4018, "Sorry this deal is only for first time customers to " + deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString());
                }
                else
                {
                    customer_bll.credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                    DataTable dt = customer_bll.credit_card_dal.usp_Credit_Card_SelectPK_credit_card_id();
                    foreach (DataRow dr in dt.Rows)
                    {
                        customer_bll.credit_card_dal.card_holder_name = dr["Credit_Card_card_holder_name"].ToString();
                        customer_bll.credit_card_dal.card_number = dr["Credit_Card_card_number"].ToString();
                        customer_bll.credit_card_dal.card_number_decrypted = dr["Credit_Card_card_number_decrypted"].ToString();
                        customer_bll.credit_card_dal.expiration_date = dr["Credit_Card_expiration_date"].ToString();
                        customer_bll.credit_card_dal.pci_key = system_bll.Decrypt(system_bll.system_settings_dal.pci_key.ToString());
                    }

                    if ((certificate_payment_dal.payment_amount == 0) && (dt.Rows.Count == 0))
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(4020, "");
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        successful = false;
                        system_error_dal = system_bll.Get_System_Error(4019, "");
                    }
                    else if (certificate_payment_dal.payment_amount > 0)
                    {
                        GTSoft.CoreDotNet.Credit_Card_Processing cc_processing = new Credit_Card_Processing();
                        cc_processing.card_number = customer_bll.credit_card_dal.card_number_decrypted.ToString();
                        cc_processing.card_date = customer_bll.credit_card_dal.expiration_date.ToString();
                        cc_processing.amount = decimal.Parse(certificate_payment_dal.payment_amount.ToString());
                        cc_processing.description = "";

                        if (system_bll.system_settings_dal.credit_card_system.ToString() == "authorize.net")
                        {
                            cc_processing.Authorize_DotNet_Process_Charge(system_bll.system_settings_dal.auth_net_api_login_id.ToString(), system_bll.system_settings_dal.auth_net_transaction_key.ToString());
                        }
                        else if (system_bll.system_settings_dal.credit_card_system == "braintree")
                        {
                            cc_processing.Braintree_Process_Charge(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                        }
                        else if (system_bll.system_settings_dal.credit_card_system == "braintree_sandbox")
                        {
                            cc_processing.Braintree_Sandbox_Process_Charge(system_bll.system_settings_dal.braintree_public_key.ToString(), system_bll.system_settings_dal.braintree_private_key.ToString(), system_bll.system_settings_dal.braintree_merchant_id.ToString());
                        }

                        payment_log_dal.notes = cc_processing.message;
                        successful = cc_processing.successful;

                        if (successful)
                        {
                            if (cc_processing.approved)
                            {
                                system_successful_dal = system_bll.Get_System_Successful(4001, cc_processing.message);
                            }
                            else
                            {
                                successful = false;
                                system_error_dal = system_bll.Get_System_Error(4022, cc_processing.message);
                            }                            
                        }
                        else
                        {
                            system_error_dal = system_bll.Get_System_Error(4022, cc_processing.error_text);
                        }
                    }
                    else
                    {
                        payment_log_dal.notes = "No payment for 0 amount";

                        successful = true;
                    }

                    if (successful)
                    {
                        certificate_dal.deal_id = deal_dal.deal_id;
                        certificate_dal.customer_id = customer_bll.customer_dal.customer_id;
                        certificate_dal.credit_card_id = customer_bll.credit_card_dal.credit_card_id;
                        if (promotion_activity_dal.promotion_activity_id != 0)
                            certificate_dal.promotion_activity_id = promotion_activity_dal.promotion_activity_id;
                        certificate_dal.payment_amount = certificate_payment_dal.payment_amount;
                        certificate_dal.usp_Certificate_Assign_To_Customer();
                        certificate_payment_dal.payment_id = certificate_dal.payment_id;

                        if (certificate_dal.certificate_id == 0)
                        {
                            successful = false;
                            system_error_dal = system_bll.Get_System_Error(2000, "");
                        }
                        else
                        {
                            Email_Certificate_Buy();

                            successful = true;
                            system_successful_dal = system_bll.Get_System_Successful(3001, "");
                        }

                        Log_Payment("single");

                        // if promo good add to activity for free use, must be a new customer
                        if (promotion_activity_dal.promotion_activity_id != 0)
                        {
                            promotion_activity_dal.usp_Promotion_Activity_Insert_To_Referral();

                            if (!promotion_activity_dal.promotion_customer_id.IsNull)
                                Email_Referral(int.Parse(promotion_activity_dal.promotion_customer_id.ToString()), promotion_activity_dal.promotion_code.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Set_Certificate_Status(int updated_status_id)
        {
            try
            {
                certificate_dal.status_id = updated_status_id;
                certificate_dal.usp_Certificate_UpdatePK_status_id();

                Load_Certificate_Properties();

                Log_Certificate("");

                successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Get_Customer_Payment_Info()
        {
            try
            {
                customer_bll.customer_dal.deal_id = deal_dal.deal_id;
                DataTable dt = customer_bll.customer_dal.usp_Customer_Payment();

                if (dt.Rows.Count > 0)
                {
                    if (promotion_activity_dal == null)
                        promotion_activity_dal = new Promotion_Activity();

                    promotion_activity_dal.promotion_dal = new Promotion();

                    if (dt.Rows[0]["Promotion_Activity_promotion_activity_id"] == DBNull.Value)
                    {
                        promotion_activity_dal.promotion_activity_id = 0;
                        promotion_activity_dal.promotion_dal.promotion_id = 0;
                        promotion_activity_dal.promotion_dal.promotion_code = "";
                    }
                    else
                    {
                        promotion_activity_dal.promotion_activity_id = int.Parse(dt.Rows[0]["Promotion_Activity_promotion_activity_id"].ToString());
                        promotion_activity_dal.promotion_dal.promotion_id = int.Parse(dt.Rows[0]["Promotion_promotion_id"].ToString());
                        promotion_activity_dal.promotion_dal.promotion_code = dt.Rows[0]["Promotion_promotion_code"].ToString();
                    }

                    customer_bll.credit_card_dal.credit_card_type_dal = new Credit_Card_Type();
                    if (dt.Rows[0]["Credit_Card_credit_card_id"] == DBNull.Value)
                    {
                        customer_bll.credit_card_dal.credit_card_type_dal.type = "";
                        customer_bll.credit_card_dal.credit_card_type_dal.image = "";

                        customer_bll.credit_card_dal.credit_card_id = 0;
                        customer_bll.credit_card_dal.card_number = "";
                    }
                    else
                    {
                        customer_bll.credit_card_dal.credit_card_type_dal.type = dt.Rows[0]["Credit_Card_Type_type"].ToString();
                        customer_bll.credit_card_dal.credit_card_type_dal.image = dt.Rows[0]["Credit_Card_Type_image"].ToString();

                        customer_bll.credit_card_dal.credit_card_id = int.Parse(dt.Rows[0]["Credit_Card_credit_card_id"].ToString());
                        customer_bll.credit_card_dal.card_number = dt.Rows[0]["Credit_Card_card_number"].ToString();
                    }

                    certificate_payment_dal.payment_amount = decimal.Parse(dt.Rows[0]["Certificate_Payment_payment_amount"].ToString());

                    successful = true;
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(4021, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Apply_Promotion_For_Customer()
        {
            try
            {
                int sytem_error_code;

                promotion_activity_dal.promotion_dal.customer_id = customer_bll.customer_dal.customer_id;
                promotion_activity_dal.promotion_dal.usp_Promotion_Validate_Customer();
                sytem_error_code = int.Parse(promotion_activity_dal.promotion_dal.system_error_code.ToString());
                promotion_activity_dal.promotion_dal.promotion_id = int.Parse(promotion_activity_dal.promotion_dal.promotion_id.ToString());

                if (sytem_error_code != 0)
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(sytem_error_code, "");
                }
                else
                {
                    certificate_payment_dal.payment_amount = 0;

                    promotion_activity_dal.customer_id = customer_bll.customer_dal.customer_id;
                    promotion_activity_dal.promotion_code = promotion_activity_dal.promotion_dal.promotion_code;
                    promotion_activity_dal.usp_Promotion_Activity_Insert();

                    Get_Customer_Payment_Info();

                    successful = true;
                    system_successful_dal = system_bll.Get_System_Successful(5000, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region private methods

        private void Get_Deal()
        {
            try
            {
                DataTable dt = deal_dal.usp_Deal_SelectPK_deal_id();
                DataColumnCollection dc = dt.Columns;

                Load_Deal_Properties(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Get_Deal_Log_Customer()
        {
            try
            {
                DataTable dt = deal_dal.usp_Deal_Select_By_deal_id_Log_customer_id();
                DataColumnCollection dc = dt.Columns;

                Load_Deal_Properties(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable Get_Promotion()
        {
            try
            {
                return promotion_activity_dal.promotion_dal.SelectUnique_promotion_code();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log_Deal()
        {
            try
            {
                deal_log_dal.deal_id = deal_dal.deal_id;
                deal_log_dal.status_id = deal_dal.deal_status_dal.status_id;
                deal_log_dal.Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log_Certificate(string notes)
        {
            try
            {
                certificate_log_dal = new Certificate_Log();

                certificate_log_dal.certificate_id = certificate_dal.certificate_id;

                if (customer_bll != null)
                    certificate_log_dal.customer_id = customer_bll.customer_dal.customer_id;

                certificate_log_dal.status_id = certificate_dal.certificate_status_dal.status_id;
                certificate_log_dal.notes = notes;
                certificate_log_dal.Insert();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log_Payment(string payment_process_type)
        {
            try
            {
                if (deal_dal.deal_id != 0)
                    payment_log_dal.deal_id = deal_dal.deal_id;
                if (certificate_dal.certificate_id != 0)
                    payment_log_dal.certificate_id = certificate_dal.certificate_id;
                if (merchant_bll != null)
                {
                    payment_log_dal.payment_id = deal_payment_dal.payment_id;
                    payment_log_dal.merchant_contact_id = merchant_bll.merchant_contact_dal.merchant_contact_id;
                    payment_log_dal.credit_card_id = merchant_bll.credit_card_dal.credit_card_id;
                    payment_log_dal.amount = deal_payment_dal.payment_amount;
                }
                if (customer_bll != null)
                {
                    payment_log_dal.payment_id = certificate_payment_dal.payment_id;
                    payment_log_dal.customer_id = customer_bll.customer_dal.customer_id;
                    payment_log_dal.credit_card_id = customer_bll.credit_card_dal.credit_card_id;
                    payment_log_dal.amount = certificate_payment_dal.payment_amount;
                }
                payment_log_dal.is_successful = successful;
                payment_log_dal.type = payment_process_type;
                payment_log_dal.Insert();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void Email_Admin_Approval()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(7);

                string[] to_emails = new string[1];
                to_emails[0] = system_bll.system_settings_dal.email_admin.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%merchant_contact_id%", merchant_bll.merchant_contact_dal.merchant_contact_id.ToString());
                body = body.Replace("%first_name%", merchant_bll.contact_dal.first_name.ToString());
                body = body.Replace("%last_name%", merchant_bll.contact_dal.last_name.ToString());
                body = body.Replace("%email%", merchant_bll.contact_dal.email.ToString());
                body = body.Replace("%merchant_id%", merchant_bll.merchant_dal.merchant_id.ToString());
                body = body.Replace("%company_name%", merchant_bll.merchant_dal.company_name.ToString());
                body = body.Replace("%deal_id%", deal_dal.deal_id.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);
                successful = system_bll.successful;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_Live_Deal()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(11);

                string[] to_emails = new string[1];
                to_emails[0] = deal_dal.merchant_contact_dal.contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", deal_dal.merchant_contact_dal.contact_dal.first_name.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal_email = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_Deal_Reject()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(9);

                string[] to_emails = new string[1];
                to_emails[0] = deal_dal.merchant_contact_dal.contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", deal_dal.merchant_contact_dal.contact_dal.first_name.ToString());
                body = body.Replace("%notes%", deal_log_dal.notes.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);
                successful = system_bll.successful;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_Deal_Payment()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(12);

                string[] to_emails = new string[1];
                to_emails[0] = deal_dal.merchant_contact_dal.contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", deal_dal.merchant_contact_dal.contact_dal.first_name.ToString());
                body = body.Replace("%deal_amount%", deal_dal.deal_amount.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);
                successful = system_bll.successful;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_Certificate_Buy()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(8);

                string[] to_emails = new string[1];
                to_emails[0] = customer_bll.contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", customer_bll.contact_dal.first_name.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal_email = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_After_Redeem()
        {
            try
            {
                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(13);

                string[] to_emails = new string[1];
                to_emails[0] = certificate_dal.customer_dal.contact_dal.email.ToString();

                string body = email_template_dal.body.ToString();
                body = body.Replace("%first_name%", certificate_dal.customer_dal.contact_dal.first_name.ToString());
                body = body.Replace("%company_name%", deal_dal.merchant_contact_dal.merchant_dal.company_name.ToString());

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);
                successful = system_bll.successful;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void Email_Referral(int customer_id, string promotion_code)
        {
            try
            {
                string first_name = "", email = "";

                DAL.Customer customer_dal = new DAL.Customer();
                customer_dal.customer_id = customer_id;
                DataTable dt = customer_dal.SelectPK_customer_id();
                foreach (DataRow dr in dt.Rows)
                {
                    first_name = dr["Contact_first_name"].ToString();
                    email = dr["Contact_email"].ToString();
                }

                string[] to_emails = new string[1];
                to_emails[0] = email;

                GTSoft.Meddyl.BLL.System system_bll = new System();

                DAL.Email_Template email_template_dal = system_bll.Get_Email_Template(14);

                string body = email_template_dal.body.ToString();
                body = body.Replace("%referral_first_name%", first_name.ToString());
                body = body.Replace("%first_name%", customer_bll.customer_dal.contact_dal.first_name.ToString());
                body = body.Replace("%last_name%", customer_bll.customer_dal.contact_dal.last_name.ToString());
                body = body.Replace("%promotion_code%", promotion_code);

                system_bll.email_template_dal = email_template_dal;
                system_bll.Send_Email(body, to_emails);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal_email = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1021, ex.Message.ToString());
            }
        }

        private void SMS_Validation(string validation_code)
        {
            try
            {
                DAL.SMS_Template sms_template_dal = system_bll.Get_SMS_Template(2);

                string body = sms_template_dal.body.ToString();
                body = body.Replace("%validation_code%", validation_code);

                system_bll.Send_Text(merchant_bll.contact_dal.phone.ToString(), body);

                successful = system_bll.successful;
                system_successful_dal = system_bll.system_successful_dal;
                system_error_dal_email = system_bll.system_error_dal;
            }
            catch (Exception ex)
            {
                successful = false;
                system_error_dal = system_bll.Get_System_Error(1019, ex.Message.ToString());
            }
        }

        private void Load_Certificate_Status_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Certificate_Status_status_id")) && (dr["Certificate_Status_status_id"] != DBNull.Value))
                    certificate_status_dal.status_id = int.Parse(dr["Certificate_Status_status_id"].ToString());

                if ((dc.Contains("Certificate_Status_status")) && (dr["Certificate_Status_status"] != DBNull.Value))
                    certificate_status_dal.status = dr["Certificate_Status_status"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Deal_Status_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                /* Deal_Status */
                if ((dc.Contains("Deal_Status_status_id")) && (dr["Deal_Status_status_id"] != DBNull.Value))
                    deal_status_dal.status_id = int.Parse(dr["Deal_Status_status_id"].ToString());

                if ((dc.Contains("Deal_Status_status")) && (dr["Deal_Status_status"] != DBNull.Value))
                    deal_status_dal.status = dr["Deal_Status_status"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Deal_Properties(DataRow dr)
        {
            try
            {
                //DataTable dt = deal_dal.usp_Deal_SelectPK_deal_id();
                //DataColumnCollection dc = dt.Columns;
                DataColumnCollection dc = dr.Table.Columns;

                // instantiate objects
                if (deal_dal.deal_status_dal == null)
                    deal_dal.deal_status_dal = new DAL.Deal_Status();

                if (deal_dal.time_zone_dal == null)
                    deal_dal.time_zone_dal = new DAL.Time_Zone();

                if (deal_dal.deal_validation_dal == null)
                    deal_dal.deal_validation_dal = new DAL.Deal_Validation();

                if (deal_dal.promotion_activity_dal == null)
                    deal_dal.promotion_activity_dal = new DAL.Promotion_Activity();

                if (deal_dal.merchant_contact_dal == null)
                    deal_dal.merchant_contact_dal = new Merchant_Contact();

                if (deal_dal.merchant_contact_dal.merchant_dal == null)
                    deal_dal.merchant_contact_dal.merchant_dal = new DAL.Merchant();

                if (certificate_dal == null)
                    certificate_dal = new DAL.Certificate();

                deal_dal.fine_print_option_dal_array = new List<Fine_Print_Option>();

                // load deal info
                if ((dc.Contains("Deal_deal_id")) && (dr["Deal_deal_id"] != DBNull.Value))
                    deal_dal.deal_id = int.Parse(dr["Deal_deal_id"].ToString());

                if ((dc.Contains("Deal_merchant_contact_id")) && (dr["Deal_merchant_contact_id"] != DBNull.Value))
                    deal_dal.merchant_contact_id = int.Parse(dr["Deal_merchant_contact_id"].ToString());

                if ((dc.Contains("Deal_deal")) && (dr["Deal_deal"] != DBNull.Value))
                    deal_dal.deal = dr["Deal_deal"].ToString();

                if ((dc.Contains("Deal_fine_print")) && (dr["Deal_fine_print"] != DBNull.Value))
                    deal_dal.fine_print = dr["Deal_fine_print"].ToString();

                if ((dc.Contains("Deal_fine_print_ext")) && (dr["Deal_fine_print_ext"] != DBNull.Value))
                    deal_dal.fine_print_ext = dr["Deal_fine_print_ext"].ToString();

                if ((dc.Contains("Deal_percent_off")) && (dr["Deal_percent_off"] != DBNull.Value))
                    deal_dal.percent_off = int.Parse(dr["Deal_percent_off"].ToString());

                if ((dc.Contains("Deal_Validation_validation_code")) && (dr["Deal_Validation_validation_code"] != DBNull.Value))
                    deal_dal.deal_validation_dal.validation_code = dr["Deal_Validation_validation_code"].ToString();

                if ((dc.Contains("Deal_deal_amount")) && (dr["Deal_deal_amount"] != DBNull.Value))
                    deal_dal.deal_amount = decimal.Parse(dr["Deal_deal_amount"].ToString());

                if ((dc.Contains("Deal_new_customer_only")) && (dr["Deal_new_customer_only"] != DBNull.Value))
                    deal_dal.is_valid_new_customer_only = bool.Parse(dr["Deal_new_customer_only"].ToString());

                if ((dc.Contains("Deal_max_dollar_amount")) && (dr["Deal_max_dollar_amount"] != DBNull.Value))
                    deal_dal.max_dollar_amount = decimal.Parse(dr["Deal_max_dollar_amount"].ToString());

                if ((dc.Contains("Deal_certificate_quantity")) && (dr["Deal_certificate_quantity"] != DBNull.Value))
                    deal_dal.certificate_quantity = int.Parse(dr["Deal_certificate_quantity"].ToString());

                if ((dc.Contains("Deal_use_deal_immediately")) && (dr["Deal_use_deal_immediately"] != DBNull.Value))
                    deal_dal.use_deal_immediately = bool.Parse(dr["Deal_use_deal_immediately"].ToString());

                if ((dc.Contains("Deal_is_valid_new_customer_only")) && (dr["Deal_is_valid_new_customer_only"] != DBNull.Value))
                    deal_dal.is_valid_new_customer_only = bool.Parse(dr["Deal_is_valid_new_customer_only"].ToString());

                if ((dc.Contains("Deal_ranking")) && (dr["Deal_ranking"] != DBNull.Value))
                    deal_dal.ranking = int.Parse(dr["Deal_ranking"].ToString());

                if ((dc.Contains("Deal_expiration_date")) && (dr["Deal_expiration_date"] != DBNull.Value))
                    deal_dal.expiration_date = DateTime.Parse(dr["Deal_expiration_date"].ToString());

                if ((dc.Contains("Deal_entry_date_utc_stamp")) && (dr["Deal_entry_date_utc_stamp"] != DBNull.Value))
                    deal_dal.entry_date_utc_stamp = DateTime.Parse(dr["Deal_entry_date_utc_stamp"].ToString());

                if ((dc.Contains("Deal_image")) && (dr["Deal_image"] != DBNull.Value))
                    deal_dal.image = dr["Deal_image"].ToString();

                if ((dc.Contains("Deal_certificate_amount")) && (dr["Deal_certificate_amount"] != DBNull.Value))
                    deal_dal.certificate_amount = decimal.Parse(dr["Deal_certificate_amount"].ToString());

                if ((dc.Contains("Deal_certificates_remaining")) && (dr["Deal_certificates_remaining"] != DBNull.Value))
                    deal_dal.certificates_remaining = int.Parse(dr["Deal_certificates_remaining"].ToString());

                if ((dc.Contains("Deal_certificates_redeemed")) && (dr["Deal_certificates_redeemed"] != DBNull.Value))
                    deal_dal.certificates_redeemed = int.Parse(dr["Deal_certificates_redeemed"].ToString());

                if ((dc.Contains("Deal_certificates_sold")) && (dr["Deal_certificates_sold"] != DBNull.Value))
                    deal_dal.certificates_sold = int.Parse(dr["Deal_certificates_sold"].ToString());

                if ((dc.Contains("Deal_certificates_unused")) && (dr["Deal_certificates_unused"] != DBNull.Value))
                    deal_dal.certificates_unused = int.Parse(dr["Deal_certificates_unused"].ToString());

                if ((dc.Contains("Deal_certificates_expired")) && (dr["Deal_certificates_expired"] != DBNull.Value))
                    deal_dal.certificates_expired = int.Parse(dr["Deal_certificates_expired"].ToString());

                if ((dc.Contains("Deal_last_assigned_date")) && (dr["Deal_last_assigned_date"] != DBNull.Value))
                    deal_dal.last_assigned_date = DateTime.Parse(dr["Deal_last_assigned_date"].ToString());

                if ((dc.Contains("Deal_last_redeemed_date")) && (dr["Deal_last_redeemed_date"] != DBNull.Value))
                    deal_dal.last_redeemed_date = DateTime.Parse(dr["Deal_last_redeemed_date"].ToString());

                if ((dc.Contains("Deal_distance")) && (dr["Deal_distance"] != DBNull.Value))
                    deal_dal.distance = Decimal.Parse(dr["Deal_distance"].ToString());

                if ((dc.Contains("Deal_instructions")) && (dr["Deal_instructions"] != DBNull.Value))
                    deal_dal.instructions = dr["Deal_instructions"].ToString();

                /* Deal_Status */
                if ((dc.Contains("Deal_Status_status_id")) && (dr["Deal_Status_status_id"] != DBNull.Value))
                    deal_dal.deal_status_dal.status_id = int.Parse(dr["Deal_Status_status_id"].ToString());

                if ((dc.Contains("Deal_Status_status")) && (dr["Deal_Status_status"] != DBNull.Value))
                    deal_dal.deal_status_dal.status = dr["Deal_Status_status"].ToString();

                /* Promotion_Activity */
                if ((dc.Contains("Promotion_Activity_promotion_activity_id")) && (dr["Promotion_Activity_promotion_activity_id"] != DBNull.Value))
                    deal_dal.promotion_activity_dal.promotion_activity_id = int.Parse(dr["Promotion_Activity_promotion_activity_id"].ToString());

                /* Time_Zone */
                if ((dc.Contains("Time_Zone_time_zone_id")) && (dr["Time_Zone_time_zone_id"] != DBNull.Value))
                    deal_dal.time_zone_dal.time_zone_id = int.Parse(dr["Time_Zone_time_zone_id"].ToString());

                if ((dc.Contains("Time_Zone_abbreviation")) && (dr["Time_Zone_abbreviation"] != DBNull.Value))
                    deal_dal.time_zone_dal.abbreviation = dr["Time_Zone_abbreviation"].ToString();

                if ((dc.Contains("Time_Zone_time_zone")) && (dr["Time_Zone_time_zone"] != DBNull.Value))
                    deal_dal.time_zone_dal.time_zone = dr["Time_Zone_time_zone"].ToString();

                if ((dc.Contains("Time_Zone_offset")) && (dr["Time_Zone_offset"] != DBNull.Value))
                    deal_dal.time_zone_dal.offset = int.Parse(dr["Time_Zone_offset"].ToString());

                // load merchant info 
                if ((dc.Contains("Merchant_Contact_merchant_contact_id")) && (dr["Merchant_Contact_merchant_contact_id"] != DBNull.Value))
                {
                    DAL.Merchant_Contact merchant_contact_dal = new Merchant_Contact();
                    merchant_contact_dal.merchant_contact_id = int.Parse(dr["Merchant_Contact_merchant_contact_id"].ToString());

                    BLL.Merchant merchant_bll_temp = new Merchant(merchant_contact_dal);
                    deal_dal.merchant_contact_dal = merchant_bll_temp.merchant_contact_dal;
                    deal_dal.merchant_contact_dal.contact_dal = merchant_bll_temp.contact_dal;
                    deal_dal.merchant_contact_dal.merchant_dal = merchant_bll_temp.merchant_dal;
                }

                // load fine print info
                Get_Fine_Print_Options_By_Deal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Certificate_Properties()
        {
            try
            {
                DataTable dt = certificate_dal.usp_Certificate_SelectPK_certificate_id();
                DataColumnCollection dc = dt.Columns;

                if (dt.Rows.Count == 1)
                {
                    if (certificate_dal.certificate_status_dal == null)
                        certificate_dal.certificate_status_dal = new DAL.Certificate_Status();

                    if ((dc.Contains("Certificate_certificate_id")) && (dt.Rows[0]["Certificate_certificate_id"] != DBNull.Value))
                        certificate_dal.certificate_id = int.Parse(dt.Rows[0]["Certificate_certificate_id"].ToString());

                    if ((dc.Contains("Certificate_certificate_code")) && (dt.Rows[0]["Certificate_certificate_code"] != DBNull.Value))
                        certificate_dal.certificate_code = dt.Rows[0]["Certificate_certificate_code"].ToString();

                    if ((dc.Contains("Certificate_status_text_1")) && (dt.Rows[0]["Certificate_status_text_1"] != DBNull.Value))
                        certificate_dal.status_text_1 = dt.Rows[0]["Certificate_status_text_1"].ToString();

                    if ((dc.Contains("Certificate_status_text_2")) && (dt.Rows[0]["Certificate_status_text_2"] != DBNull.Value))
                        certificate_dal.status_text_2 = dt.Rows[0]["Certificate_status_text_2"].ToString();

                    if ((dc.Contains("Certificate_Status_status_id")) && (dt.Rows[0]["Certificate_Status_status_id"] != DBNull.Value))
                        certificate_dal.certificate_status_dal.status_id = int.Parse(dt.Rows[0]["Certificate_Status_status_id"].ToString());

                    if ((dc.Contains("Certificate_Status_status")) && (dt.Rows[0]["Certificate_Status_status"] != DBNull.Value))
                        certificate_dal.certificate_status_dal.status = dt.Rows[0]["Certificate_Status_status"].ToString();

                    if ((dc.Contains("Certificate_assigned_date")) && (dt.Rows[0]["Certificate_assigned_date"] != DBNull.Value))
                        certificate_dal.assigned_date = DateTime.Parse(dt.Rows[0]["Certificate_assigned_date"].ToString());
                    
                    if ((dc.Contains("Certificate_start_date")) && (dt.Rows[0]["Certificate_start_date"] != DBNull.Value))
                        certificate_dal.start_date = DateTime.Parse(dt.Rows[0]["Certificate_start_date"].ToString());

                    if ((dc.Contains("Certificate_expiration_date")) && (dt.Rows[0]["Certificate_expiration_date"] != DBNull.Value))
                        certificate_dal.expiration_date = DateTime.Parse(dt.Rows[0]["Certificate_expiration_date"].ToString());

                    if ((dc.Contains("Certificate_redeemed_date")) && (dt.Rows[0]["Certificate_redeemed_date"] != DBNull.Value))
                        certificate_dal.redeemed_date = DateTime.Parse(dt.Rows[0]["Certificate_redeemed_date"].ToString());

                    if ((dc.Contains("Certificate_entry_date_utc_stamp")) && (dt.Rows[0]["Certificate_entry_date_utc_stamp"] != DBNull.Value))
                        certificate_dal.entry_date_utc_stamp = DateTime.Parse(dt.Rows[0]["Certificate_entry_date_utc_stamp"].ToString());

                    // load deal information                
                    if ((dc.Contains("Deal_deal_id")) && (dt.Rows[0]["Deal_deal_id"] != DBNull.Value))
                    {
                        deal_dal = new DAL.Deal();
                        deal_dal.deal_id = int.Parse(dt.Rows[0]["Deal_deal_id"].ToString());
                        Get_Deal();
                        certificate_dal.deal_dal = deal_dal;
                    }

                    // load deal information                
                    if ((dc.Contains("Customer_customer_id")) && (dt.Rows[0]["Customer_customer_id"] != DBNull.Value))
                    {
                        DAL.Customer customer_dal = new DAL.Customer();
                        customer_dal.customer_id = int.Parse(dt.Rows[0]["Customer_customer_id"].ToString());

                        BLL.Customer customer_bll_temp = new Customer(customer_dal);
                        certificate_dal.customer_dal = customer_bll_temp.customer_dal;
                        certificate_dal.customer_dal.contact_dal = customer_bll_temp.contact_dal;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Load_Certificate_Payment_Properties(DataTable dt)
        {
            try
            {
                DataColumnCollection dc = dt.Columns;

                certificate_payment_dal = new Certificate_Payment();

                if ((dc.Contains("Certificate_Payment_payment_id")) && (dt.Rows[0]["Certificate_Payment_payment_id"] != DBNull.Value))
                    certificate_payment_dal.payment_id = int.Parse(dt.Rows[0]["Certificate_Payment_payment_id"].ToString());

                if ((dc.Contains("Certificate_Payment_card_number")) && (dt.Rows[0]["Certificate_Payment_card_number"] != DBNull.Value))
                    certificate_payment_dal.card_number = dt.Rows[0]["Certificate_Payment_card_number"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region class properties

        public bool successful { get; set; }
        
        public DAL.Certificate certificate_dal { get; set; }
        public DAL.Certificate_Log certificate_log_dal { get; set; }
        public DAL.Certificate_Payment certificate_payment_dal { get; set; }
        public DAL.Certificate_Status certificate_status_dal { get; set; }
        public DAL.Deal deal_dal { get; set; }
        public DAL.Deal_Log deal_log_dal { get; set; }
        public DAL.Deal_Payment deal_payment_dal { get; set; }
        public DAL.Deal_Status deal_status_dal { get; set; }
        public DAL.Payment_Log payment_log_dal { get; set; }
        public DAL.Promotion_Activity promotion_activity_dal { get; set; }
        public DAL.System_Settings system_settings_dal { get; set; }
        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Error system_error_dal_email { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        public List<DAL.Certificate> certificate_dal_array { get; set; }
        public List<DAL.Certificate_Status> certificate_status_dal_array { get; set; }
        public List<DAL.Deal> deal_dal_array { get; set; }
        public List<DAL.Deal_Status> deal_status_dal_array { get; set; }
        public List<DAL.Fine_Print_Option> fine_print_option_dal_array { get; set; }

        public Merchant merchant_bll { get; set; }
        public Customer customer_bll { get; set; }
        public System system_bll { get; set; }

        #endregion
    }
}
