using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GTSoft.Meddyl.API
{

    [DataContract]
    public class Deal : Base_Deal
    {
        [DataMember(EmitDefaultValue = false)]
        public string derived_text_1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string derived_text_2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Decimal distance { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int certificates_remaining { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int certificates_sold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int certificates_redeemed { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int certificates_unused { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int certificates_expired { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime last_redeemed_date { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime last_assigned_date { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string image_base64 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string search { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<Fine_Print_Option> fine_print_option_obj_array { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Application_Type application_type_obj { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Login_Log login_log_obj { get; set; }
    }

}