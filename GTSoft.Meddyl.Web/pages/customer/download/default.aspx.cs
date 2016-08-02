using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GTSoft.Meddyl.Web.customer.download
{
    public partial class _default : System.Web.UI.Page
    {
        #region events

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
                MasterPageFile = "~/pages/customer/master/Mobile.Master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.System system_bll = new BLL.System();

            this.imbGoogle.PostBackUrl = "https://play.google.com/store/apps/details?id=" + system_bll.system_settings_dal.customer_app_android_id.ToString();
            this.imbApple.PostBackUrl = "https://itunes.apple.com/us/app/apple-store/" + system_bll.system_settings_dal.customer_app_ios_id.ToString();
        }

        #endregion
    }
}