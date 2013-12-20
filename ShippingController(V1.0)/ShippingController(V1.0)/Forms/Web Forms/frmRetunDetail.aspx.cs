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
                FillReturnDetails(Obj.Rcall.ReturnDetailAll());
            }
        }

        #region Functions

        public void FillReturnMasterGv(List<Return> lsReturn)
        {
            gvReturnInfo.DataSource = lsReturn;
            gvReturnInfo.DataBind();
        }

        public void FillReturnDetails(List<ReturnDetail> lsReturnDetails)
        {
            try
            {
                var ReaturnDetails = from Rs in lsReturnDetails
                         select new
                         {
                            Rs.RGADROWID,
                            Rs.SKUNumber,
                            Rs.ProductName,
                            Rs.DeliveredQty,
                            Rs.ReturnQty,
                            ReturnReasons = Obj.Rcall.ReasonsListByReturnDetails(Rs.ReturnDetailID)
                         };
                gvReturnDetails.DataSource = ReaturnDetails.ToList();
                gvReturnDetails.DataBind();
            }
            catch (Exception)
            {}
        }

        #endregion



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
    }
}