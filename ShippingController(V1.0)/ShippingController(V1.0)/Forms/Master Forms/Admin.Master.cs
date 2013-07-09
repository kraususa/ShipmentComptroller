using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Master_Forms
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserNameTop.Text = "Avinash Patil";
        }

        public void Showrt(Object sender, EventArgs e)
        {
            
        }

       
    }
}