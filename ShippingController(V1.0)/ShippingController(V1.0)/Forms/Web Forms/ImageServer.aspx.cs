using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class ImageServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                String ImagePth = Request.QueryString["FileName"];
                Response.ContentType = "image/jpeg"; // for JPEG file
                string physicalFileName = ImagePth;
                Response.WriteFile(physicalFileName);
            }
            catch (Exception)
            {}
        }
        
    }
}