using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;

namespace GTSoft.CoreDotNet
{
    public class Google_Geocode
    {
        #region constructors

        public Google_Geocode(string api_key)
        {
            geocode_api_key = api_key;
        }

        #endregion


        #region public methods

        public void Geocode_Coordinates_From_Address()
        {
            try
            {
                string address = address_1.Replace(" ", "+") + "+" + address_2.Replace(" ", "+") + "+" + zip_code.Replace(" ", "+");
                url = string.Format("https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", address, geocode_api_key);
                var response = new System.Net.WebClient().DownloadString(url);
                GoogleGeoCodeResponse json = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(response);

                latitude = json.results[0].geometry.location.lat;
                longitude = json.results[0].geometry.location.lng;
            }
            catch 
            {
                latitude = 0;
                longitude = 0;
            }
        }

        #endregion


        #region properties

        public string url { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string zip_code { get; set; }
        public string geocode_api_key { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        #endregion
    }

    class GoogleGeoCodeResponse
    {

        public string status { get; set; }
        public results[] results { get; set; }

    }

    class results
    {
        public string formatted_address { get; set; }
        public geometry geometry { get; set; }
        public string[] types { get; set; }
        public address_component[] address_components { get; set; }
    }

    class geometry
    {
        public string location_type { get; set; }
        public location location { get; set; }
    }

    class location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    class address_component
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}
