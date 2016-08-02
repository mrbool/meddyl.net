using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GTSoft.Meddyl.BLL
{
    public class Location
    {

        #region constructors

        public Location()
        {
            zip_code_dal = new DAL.Zip_Code();

            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();

            system_bll = new System();
        }

        public Location(DAL.Zip_Code _zip_code_dal)
        {
            zip_code_dal = _zip_code_dal;
            
            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();

            system_bll = new System();
        }

        #endregion


        #region public methods

        public DataTable Get_Zip_Code()
        {
            try
            {
                DataTable dt = zip_code_dal.usp_Zip_Code_SelectPK_zip_code();

                latitude = 0;
                longitude = 0;

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Zip_Code_From_Coordinates()
        {
            try
            {
                zip_code_dal.latitude = latitude;
                zip_code_dal.longitude = longitude;
                DataTable dt = zip_code_dal.usp_Zip_Code_From_Coordinates();
                foreach(DataRow dr in dt.Rows)
                {
                    zip_code_dal.zip_code = dr["Zip_Code_zip_code"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Get_Coordinates_For_Address()
        {
            try
            {
                DAL.System_Settings system_settings_dal = new DAL.System_Settings();
                DataTable dt_settings = system_settings_dal.usp_System_Settings_SelectAll();
                string google_api_key = dt_settings.Rows[0]["System_Settings_google_api_key"].ToString();

                GTSoft.CoreDotNet.Google_Geocode geocode = new CoreDotNet.Google_Geocode(google_api_key);
                geocode.address_1 = address_1;
                geocode.address_2 = address_2;
                geocode.zip_code = zip_code_dal.zip_code.ToString();
                geocode.Geocode_Coordinates_From_Address();
                latitude = geocode.latitude;
                longitude = geocode.longitude;

                successful = true;
            }
            catch
            {
                latitude = 0;
                longitude = 0;

                successful = false;
                system_error_dal = system_bll.Get_System_Error(5003, "");
            }
        }

        public void Get_Neighborhood_By_Zip()
        {
            try
            {
                DataTable dt_exists = Get_Zip_Code();

                if (dt_exists.Rows.Count == 1)
                {
                    Load_Zip_Code_Properties(dt_exists.Rows[0]);

                    neighborhood_dal_array = new List<DAL.Neighborhood>();

                    DAL.Neighborhood neighborhood_dal = new DAL.Neighborhood();
                    neighborhood_dal.zip_code = zip_code_dal.zip_code;
                    DataTable dt = neighborhood_dal.SelectFK_Zip_Code_zip_code();
                    foreach (DataRow dr in dt.Rows)
                    {
                        neighborhood_dal = new DAL.Neighborhood();
                        neighborhood_dal.neighborhood_id = int.Parse(dr["Neighborhood_neighborhood_id"].ToString());
                        neighborhood_dal.neighborhood = dr["Neighborhood_neighborhood"].ToString();
                        neighborhood_dal_array.Add(neighborhood_dal);
                    }

                    successful = true;
                }
                else
                {
                    successful = false;
                    system_error_dal = system_bll.Get_System_Error(1003, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region private methods

        private void Load_Zip_Code_Properties(DataRow dr)
        {
            try
            {
                DataColumnCollection dc = dr.Table.Columns;

                // instantiate objects
                if (zip_code_dal.city_dal == null)
                    zip_code_dal.city_dal = new DAL.City();

                if (zip_code_dal.city_dal.state_dal == null)
                    zip_code_dal.city_dal.state_dal = new DAL.State();

                /* load data */
                if ((dc.Contains("Zip_Code_latitude")) && (dr["Zip_Code_latitude"] != DBNull.Value))
                    zip_code_dal.latitude = double.Parse(dr["Zip_Code_latitude"].ToString());

                if ((dc.Contains("Zip_Code_longitude")) && (dr["Zip_Code_longitude"] != DBNull.Value))
                    zip_code_dal.longitude = double.Parse(dr["Zip_Code_longitude"].ToString());

                if ((dc.Contains("City_city")) && (dr["City_city"] != DBNull.Value))
                    zip_code_dal.city_dal.city = dr["City_city"].ToString();

                if ((dc.Contains("State_abbreviation")) && (dr["State_abbreviation"] != DBNull.Value))
                    zip_code_dal.city_dal.state_dal.abbreviation = dr["State_abbreviation"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region properties

        public bool successful { get; set; }

        public string address_1 { get; set; }
        public string address_2 { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }

        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }
        public DAL.Zip_Code zip_code_dal { get; set; }

        public List<DAL.Neighborhood> neighborhood_dal_array { get; set; }

        public System system_bll { get; set; }

        #endregion

    }
}
