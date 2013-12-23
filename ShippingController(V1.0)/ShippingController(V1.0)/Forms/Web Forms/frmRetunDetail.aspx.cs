using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRetunDetail : System.Web.UI.Page
    {
        ReportController re = new ReportController();
        List<Return> _lsreturn = new List<Return>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _lsreturn = re.ReturnAll();
                FillReturnMasterGv(Obj.Rcall.ReturnAll());
            }
        }


        public void FillReturnMasterGv(List<Return> lsReturn)
        {
            _lsreturn = lsReturn;
            gvReturnInfo.DataSource = lsReturn;
            gvReturnInfo.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> lsRGAROWID = new List<string>();

                foreach (GridViewRow row in gvReturnInfo.Rows)
                {
                    LinkButton lnk = (LinkButton)row.FindControl("RMANumber");
                    lsRGAROWID.Add(lnk.Text);
                }

                modelExportTo.Excel(lsRGAROWID, "RGA Details");
            }
            catch (Exception)
            { }
        }

        protected void txtRMANumber_TextChanged(object sender, EventArgs e)
        {
            var RMA = from returnALL in _lsreturn
                      where returnALL.RMANumber == txtRMANumber.Text
                      select returnALL;

            FillReturnMasterGv(RMA.ToList());
        }

        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            var ShipID = from returnAll in _lsreturn
                         where returnAll.ShipmentNumber == txtShipmentID.Text
                         select returnAll;

            FillReturnMasterGv(ShipID.ToList());
        }

        protected void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            var OrderNum = from all in _lsreturn
                           where all.OrderNumber == txtOrderNumber.Text
                           select all;

            FillReturnMasterGv(OrderNum.ToList());
        }

        protected void txtPoNum_TextChanged(object sender, EventArgs e)
        {
            var PONum = from all in _lsreturn
                           where all.PONumber == txtPoNumber.Text
                           select all;

            FillReturnMasterGv(PONum.ToList());
        }

    }
}