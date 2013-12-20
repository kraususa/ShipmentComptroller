using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRetunDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillReturnMasterGv(Obj.Rcall.ReturnAll());
            }
        }


        public void FillReturnMasterGv(List<Return> lsReturn)
        {
            gvReturnInfo.DataSource = lsReturn;
            gvReturnInfo.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
           //var ReturnD  = from rr in Obj.Rcall.ReturnDe
        }
    }
}