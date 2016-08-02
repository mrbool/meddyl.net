using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace GTSoft.Meddyl.DAL
{
	public class Credit_Card : GTSoft.Meddyl.DAL.Base_Credit_Card
    {
        #region constructors

		public Credit_Card()
		{
			//
			// TODO:  Add constructor logic here
			//
		}

        #endregion


        #region public methods

        public bool usp_Credit_Card_Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_type_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, type_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_card_holder_name", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, card_holder_name));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_card_number", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, card_number));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_expiration_date", SqlDbType.Char, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, expiration_date));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_billing_zip_code", SqlDbType.Char, 5, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, billing_zip_code));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_pci_key", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pci_key));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                credit_card_id = (SqlInt32)scmCmdToExecute.Parameters["@o_credit_card_id"].Value;
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Insert' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public DataTable usp_Credit_Card_SelectFK_Merchant_Contact_merchant_contact_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_SelectFK_Merchant_Contact_merchant_contact_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_pci_key", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pci_key));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_SelectFK_Merchant_Contact_merchant_contact_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Credit_Card_SelectFK_Customer_customer_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_SelectFK_Customer_customer_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_SelectFK_Customer_customer_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Credit_Card_Merchant_Contact_Select_default()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Merchant_Contact_Select_default]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_pci_key", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pci_key));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Merchant_Contact_Select_default' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public DataTable usp_Credit_Card_Customer_Select_default()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Customer_Select_default]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                // Open connection.
                mainConnection.Open();

                // Execute query.
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Customer_Select_default' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public bool usp_Credit_Card_Merchant_Contact_Set_Default()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Merchant_Contact_Set_Default]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
               scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Merchant_Contact_Set_Default' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Credit_Card_Delete_Merchant_Contact()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Delete_Merchant_Contact]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_merchant_contact_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, merchant_contact_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Delete_Merchant_Contact' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Credit_Card_Customer_Set_Default()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Customer_Set_Default]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Customer_Set_Default' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool usp_Credit_Card_Delete_Customer()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_Delete_Customer]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_customer_id", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, customer_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                scmCmdToExecute.ExecuteNonQuery();
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_Delete_Customer' reported the ErrorCode: " + errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public DataTable usp_Credit_Card_SelectPK_credit_card_id()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[usp_Credit_Card_SelectPK_credit_card_id]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = connection_timeout;
            DataTable toReturn = new DataTable("Credit_Card");
            SqlDataAdapter adapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_credit_card_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, credit_card_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@p_pci_key", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pci_key));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@o_error_code", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errorCode));

                /* open database connection */
                mainConnection.Open();

                /* execute query */
                adapter.Fill(toReturn);
                errorCode = (SqlInt32)scmCmdToExecute.Parameters["@o_error_code"].Value;

                if (errorCode != 0)
                {
                    /* throw error */
                    throw new Exception("Stored Procedure 'usp_Credit_Card_SelectPK_credit_card_id' reported the ErrorCode: " + errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                /* some error occured. Bubble it to caller and encapsulate Exception object */
                throw ex;
            }
            finally
            {
                mainConnection.Close();
                scmCmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

		#endregion


        #region properties

        public SqlString pci_key { get; set; }
        public SqlString card_number_decrypted { get; set; }
        public SqlString security_code { get; set; }

        #endregion
    }
}
