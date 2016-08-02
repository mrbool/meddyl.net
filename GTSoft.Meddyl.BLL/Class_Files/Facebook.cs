using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GTSoft.Meddyl.BLL
{
    public class Facebook
    {
        #region constructors

        public Facebook()
        {
            system_error_dal = new DAL.System_Error();
            system_successful_dal = new DAL.System_Successful();
            
            system_bll = new System();
        }

        #endregion


        #region public methods

        public void Load_Facebook_Data(string access_token)
        {
            DAL.Facebook_Data_Profile fb_profile_dal = new DAL.Facebook_Data_Profile();
            DAL.Facebook_Data_Location fb_locations_dal = new DAL.Facebook_Data_Location();
            DAL.Facebook_Data_Hometown fb_hometowns_dal = new DAL.Facebook_Data_Hometown();

            GTSoft.CoreDotNet.Facebook fb = new GTSoft.CoreDotNet.Facebook();
            fb.access_token = access_token;

            fb.Get_Profile_Data();
            successful = fb.successful;
            system_error_dal = system_bll.Get_System_Error(5004, "");

            if (successful)
            {
                facebook_id = (fb.id == 0) ? 0 : fb.id;

                fb_profile_dal.fb_profile_id = facebook_id;
                fb_profile_dal.birthday = (fb.birthday == null) ? "" : fb.birthday;
                fb_profile_dal.email = (fb.email == null) ? "" : fb.email;
                fb_profile_dal.first_name = (fb.first_name == null) ? "" : fb.first_name;
                fb_profile_dal.gender = (fb.gender == null) ? "" : fb.gender;
                fb_profile_dal.last_name = (fb.last_name == null) ? "" : fb.last_name;
                fb_profile_dal.link = (fb.link == null) ? "" : fb.link;
                fb_profile_dal.locale = (fb.locale == null) ? "" : fb.locale;
                fb_profile_dal.middle_name = (fb.middle_name == null) ? "" : fb.middle_name;
                fb_profile_dal.name = (fb.name == null) ? "" : fb.name;
                fb_profile_dal.timezone = (fb.timezone == null) ? "" : fb.timezone;
                fb_profile_dal.updated_time = (fb.updated_time == null) ? "" : fb.updated_time;
                fb_profile_dal.username = (fb.username == null) ? "" : fb.username;
                fb_profile_dal.verified = (fb.verified == null) ? "" : fb.verified;
                fb_profile_dal.Insert();

                if (fb.locations != null)
                {
                    foreach (DataRow dr in fb.locations.Rows)
                    {
                        fb_locations_dal.fb_profile_id = facebook_id;
                        fb_locations_dal.fb_location_id = long.Parse(dr["id"].ToString());
                        fb_locations_dal.name = dr["name"].ToString();
                        fb_locations_dal.Insert();
                    }
                }

                if (fb.hometowns != null)
                {
                    foreach (DataRow dr in fb.hometowns.Rows)
                    {
                        fb_hometowns_dal.fb_profile_id = facebook_id;
                        fb_hometowns_dal.fb_hometown_id = long.Parse(dr["id"].ToString());
                        fb_hometowns_dal.name = dr["name"].ToString();
                        fb_hometowns_dal.Insert();
                    }
                }
            }

            fb.Get_Friends_Data();
            successful = fb.successful;
            system_error_dal = system_bll.Get_System_Error(5005, "");

            if (successful)
            {
                if(fb.dt_friends != null)
                {
                    DAL.Facebook_Data_Friends fb_friends_dal = new DAL.Facebook_Data_Friends();
                    foreach(DataRow dr in fb.dt_friends.Rows)
                    {
                        fb_friends_dal.fb_profile_id = facebook_id;
                        fb_friends_dal.fb_friend_id = long.Parse(dr["id"].ToString());
                        fb_friends_dal.name = dr["name"].ToString();
                        fb_friends_dal.Insert();
                    }
                }
            }

            fb_profile_dal.usp_Facebook_Data_Post_Load();

        }

        #endregion


        #region properties

        public bool successful { get; set; }

        public DAL.System_Error system_error_dal { get; set; }
        public DAL.System_Successful system_successful_dal { get; set; }

        public long facebook_id { get; set; }

        public System system_bll { get; set; }

        #endregion
    }
}
