﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class Packing : System.Web.UI.Page
    {

        private void Page_PreInit(object sender, EventArgs e)
        {
            string user = Session["UserID"].ToString().ToUpper();
            if (Session["UserID"].ToString().ToUpper() == "0DD3CB2D-33B6-431F-9DA0-042F9FF3963B")
            {
                this.MasterPageFile = "~/Forms/Master Forms/Admin.Master";
            }
            else
            {
                this.MasterPageFile = "~/Forms/Master Forms/TestUser.Master";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString !=null)
            {
                Label1.Text = Request.QueryString["id"].ToString() + Session["PackingID"].ToString();
            }
           
        }
    }
}