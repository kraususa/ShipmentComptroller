using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;
using ShippingController_V1._0_.Classes;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShippingInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillGvShippingInfo();
        }

        /// <summary>
        /// Gridview Shipping Information fill 
        /// </summary>
        public void FillGvShippingInfo()
        {
            try
            {
               var ShippingInfo= Obj.Rcall.GetBpinfoOFShippingNum();
               gvShippingInfo.DataSource = ShippingInfo.ToList();
               gvShippingInfo.DataBind();
            }
            catch (Exception)
            {}
        }
        
    }
}