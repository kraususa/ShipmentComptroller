using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentDetailInfo : System.Web.UI.Page
    {
        //packing calss Controller object.
        smController Call = new smController();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShipmentSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGridView(txtShipmentId.Text);
            }
            catch (Exception)
            { }
        }
        public void FillGridView(String ShipmentID)
        {
            try
            {
                var shipmentinfo = from _call in Call.GetShipment_SPCKD(ShipmentID, true)
                                   select new
                                   {
                                       SkuName = _call.SKU,
                                       ProductName = _call.ProductName,
                                       Quantity = _call.Quantity,
                                      
                                   };
                if (shipmentinfo !=null && shipmentinfo.Count() >0)
                {
                    grdSkuInfo.DataSource = shipmentinfo;
                    grdSkuInfo.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('Wrong Shipment ID');", true);
                }
            }
            catch (Exception)
            {}
        }
    }
}