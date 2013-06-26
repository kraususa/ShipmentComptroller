using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using System.Collections;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmCurrentPackingShipments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            smController Call = new smController();
           List<cstPackingTbl> lsShipmetn = Call.GetPackingTbl();
           var v = from s in lsShipmetn
                   where s.PackingStatus == 1
                   select new
                   {
                       PackingID = s.PackingID,
                       ShipmentLocation = s.ShipmentLocation,
                       UserName = Call.GetSelcetedUserMaster(s.UserID).FirstOrDefault().UserFullName
                   };
            gvShipmentPacking.DataSource = v;
            gvShipmentPacking.DataBind();
           
        }
    }
}