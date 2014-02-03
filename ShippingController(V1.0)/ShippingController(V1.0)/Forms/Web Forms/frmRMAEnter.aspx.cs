using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Commands.SMcommands.RGA;
using ShippingController_V1._0_.Views;
using PackingClassLibrary;
using PackingClassLibrary.Commands;
using PackingClassLibrary.CustomEntity;
using System.Data;
using ShippingController_V1._0_.Models;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAEnter : System.Web.UI.Page
    {

        Models.modelReturn _newRMA = new Models.modelReturn();
        smController call = new smController();
        Guid ReturnDetailsID;
        List<cstUserMasterTbl> lsUserInfo = new List<cstUserMasterTbl>();
        DataTable dt = new DataTable();
               

        cstHomePageGv _info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Reason> lsReturn = _newRMA.GetReasons();


                Reason re = new Reason();
                re.ReasonID = Guid.NewGuid();
                re.Reason1 = "--Select--";

                lsReturn.Insert(0, re);

               ddlotherreasons.DataTextField = "Reason1";
               ddlotherreasons.DataValueField = "ReasonID";
               ddlotherreasons.DataSource = lsReturn;
               ddlotherreasons.DataBind();

               //string user = Session["UName"].ToString();
               dt.Columns.Add("SKU");
               dt.Columns.Add("ProductName");
               dt.Columns.Add("Quantity");

               DataRow dr = dt.NewRow();

               dr[0] = "";
               dr[1] = "";
               dr[2] = "";

               dt.Rows.Add(dr);

               gvReturnDetails.DataSource = dt;
               gvReturnDetails.DataBind();
            }
        }
        private String ReturnReasons()
        {
            String _ReturnReason = "";

            if (chkitemdamaged.Checked == true) _ReturnReason = _ReturnReason + chkitemdamaged.Text;

            if (chkitemdifferent.Checked == true) _ReturnReason = _ReturnReason + chkitemdifferent.Text;

            if (chkduplicate.Checked == true) _ReturnReason = _ReturnReason + chkduplicate.Text;

            if (chkitemordered.Checked == true) _ReturnReason = _ReturnReason + chkitemordered.Text;

            if (chknotsatisfied.Checked == true) _ReturnReason = _ReturnReason + chknotsatisfied.Text;

            if (chkwrongitem.Checked == true) _ReturnReason = _ReturnReason + chkwrongitem.Text;

            _ReturnReason += txtotherreasons.Text;

            return _ReturnReason;

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Byte Status = Convert.ToByte(ddlstatus.SelectedValue);
            Byte Decision = Convert.ToByte(ddldecision.SelectedValue);

            List<Return> _lsreturn = new List<Return>();
            Return ret = new Return();
            ret.RMANumber = txtrmanumber.Text;
            ret.VendoeName = txtvendername.Text;
            ret.VendorNumber = txtvendernumber.Text;
            ret.ReturnDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtrequestdate.Text), "Eastern Standard Time");
            ret.PONumber = txtponumber.Text;
            ret.CustomerName1 = txtcustomername.Text;
            ret.Address1 = txtcustomeraddress.Text;
            ret.City = txtcity.Text;
            ret.Country = txtcountry.Text;
            ret.ZipCode = txtzipcode.Text;
            ret.State = txtstate.Text;

            _lsreturn.Add(ret);

            lsUserInfo = call.GetSelcetedUserMaster(Session["UName"].ToString());

            Guid ReturnID = _newRMA.SetReturnTbl(_lsreturn, ReturnReasons(), Status, Decision, lsUserInfo[0].UserID);

            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                string  sku = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtsku")).Text;
                string  productname = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname")).Text;
                string  quantity = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity")).Text;
                string category = productcategory(sku, 1);

                if (sku != "" && productname != "")
                {
                    ReturnDetailsID = _newRMA.SetReturnDetailTbl(ReturnID, sku, productname, 0, 0, Convert.ToInt32(quantity), category, lsUserInfo[0].UserID);
                }

            }
            clear();
        }

        protected void ddlotherreasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlotherreasons.SelectedIndex == 0)
            {
                txtotherreasons.Text = "";
            }
            else
            {
                txtotherreasons.Text = ddlotherreasons.SelectedItem.Text;
            }
        }

        protected void txtSKU_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox t = (TextBox)currentRow.FindControl("txtsku");
            string rt = t.Text;
            TextBox txt = (TextBox)currentRow.FindControl("txtproductname");
            txt.Text = productcategory(rt, 0);
            TextBox txt1 = (TextBox)currentRow.FindControl("txtquantity");
            txt1.Focus();

           // Int32 count = Convert.ToInt32(txt.Text);
          //string str= txt.Text;
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            dt.Columns.Add("SKU");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");
    
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
               // GridViewRow row = (GridViewRow)(gvReturnDetails.Rows[i]).Parent.Parent;

                try
                {
                    DataRow dr1 = dt.NewRow();
                    TextBox sku = (TextBox)gvReturnDetails.Rows[i].FindControl("txtsku");
                    TextBox productname = (TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname");
                    TextBox quantity = (TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity");

                    dr1[0] = sku.Text;
                    dr1[1] = productname.Text;
                    dr1[2] = quantity.Text;

                    dt.Rows.Add(dr1);
                }
                catch (Exception)
                {
                }
            }
            DataRow dr = dt.NewRow(); 
           
            dr[0] = "";
            dr[1] = "";
            dr[2] = "";

            dt.Rows.Add(dr);

            gvReturnDetails.DataSource = dt;
            gvReturnDetails.DataBind();

            dt.Clear();
        }

        public string productcategory(string sku,int flag)
        {
            string _productname = "";
            List<string> lsTrackingTbl = Obj.call._skulist(sku);
            try
            {
                if (flag == 0)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _productname = TrackItm.ToString().Split(new char[] { '#' })[1];
                    }
                }
                else if (flag == 1)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _productname = TrackItm.ToString().Split(new char[] { '#' })[2];
                    }
                }
            }
            catch (Exception)
            {}
            return _productname;
        }

        protected void btncancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmHomePage.aspx");
        }


        public void clear()
        {
            txtcity.Text = "";
            txtcountry.Text = "";
            txtcustomeraddress.Text = "";
            txtcustomername.Text="";
            txtotherreasons.Text = "";
            txtponumber.Text = "";
            txtrmanumber.Text = "";
            txtstate.Text = "";
            txtvendernumber.Text = "";
            txtvendername.Text="";
            txtzipcode.Text = "";
            ddldecision.SelectedIndex = 0;
            ddlotherreasons.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            chkduplicate.Checked = false;
            chkitemdamaged.Checked = false;
            chkitemdifferent.Checked = false;
            chkitemordered.Checked = false;
            chknotsatisfied.Checked = false;
            chkwrongitem.Checked = false;

            dt.Columns.Add("SKU");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");

            DataRow dr = dt.NewRow();

            dr[0] = "";
            dr[1] = "";
            dr[2] = "";

            dt.Rows.Add(dr);

            gvReturnDetails.DataSource = dt;
            gvReturnDetails.DataBind();

        }

    }
}