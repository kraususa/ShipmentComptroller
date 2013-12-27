using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Models;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmReturnEdit : System.Web.UI.Page
    {
        model_Packing modelpack = new model_Packing();

        protected void Page_Load(object sender, EventArgs e)
        {
         // txtRMAnumber.Text= Request.QueryString["RGAROWID"].ToString();
         // Return retuen = Obj.Rcall.ReturnByRGAROWID(Request.QueryString["RGAROWID"].ToString())[0];

            if (!IsPostBack)
            {
                display(Request.QueryString["RGAROWID"].ToString());
                FillReturnDetails(Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString()));
            }

        }

        public Boolean display(String RGA)
        {
            Boolean _flag = false;
            try
            {
                Return retuen = Obj.Rcall.ReturnByRGAROWID(RGA)[0];
                txtcustomerName.Text = retuen.CustomerName1;
                txtponumber.Text = retuen.PONumber;
                txtvendorName.Text = retuen.VendoeName;
                txtRMAnumber.Text = retuen.RMANumber;
                txtshipmentnumber.Text = retuen.ShipmentNumber;
                txtvendornumber.Text = retuen.VendorNumber;
                txtrganumber.Text = retuen.RGAROWID;
                txtreturndate.Text =Convert.ToString(retuen.ReturnDate.ToShortDateString());
                txtorderdate.Text = Convert.ToString(retuen.OrderDate.ToShortDateString());
                txtordernumber.Text = retuen.OrderNumber;
                ddlstatus.SelectedIndex = Convert.ToInt16(retuen.RMAStatus);
                ddldecision.SelectedIndex =  Convert.ToInt16(retuen.Decision);
                _flag = true;
            }
            catch (Exception)
            {
            }
            return _flag;
        }

        public void FillReturnDetails(List<ReturnDetail> lsReturnDetails)
        {
            try
            {
                Obj._lsReturnDetails = lsReturnDetails;
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
            { }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            //string sta = ddlstatus.SelectedValue.ToString();
            //string dec = ddldecision.SelectedValue.ToString();

            //Guid returnid = modelpack.SetReturnTbl(Convert.ToByte(ddlstatus.SelectedValue.ToString()),Convert.ToByte(ddldecision.SelectedValue.ToString()));

           // string RMA = _TextBox("txtSKU", gvReturnDetails);

           // Guid ReturnDetailsID = modelpack.SetReturnDetailTbl();//ReturnTblID, SkuNumber.Text, ProcutName.Text, DeliveredQty, ExpectedQty, Convert.ToInt32(txtRetutn.Text), tck, clGlobal.mCurrentUser.UserInfo.UserID);

        }

        /// <summary>
        /// Text Of Textbox
        /// </summary>
        /// <param name="TextBoxID">
        /// String textbox ID
        /// </param>
        /// <param name="GridViewName">
        /// Gridview Object textbox belongs to
        /// </param>
        /// <returns>
        /// String Text Of TextBox 
        /// </returns>
        private String _TextBox(String TextBoxID, GridView GridViewName)
        {
            String _return = "";

            try
            {
                TextBox txt = (TextBox)GridViewName.Rows[0].FindControl(TextBoxID);
                _return = txt.Text;
            }
            catch (Exception)
            { }
            return _return;
        }


    }
}