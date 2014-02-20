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
    public partial class frmReturnEdit : System.Web.UI.Page
    {
       //Create Object of modelRertunUpdate.
        modelReaturnUpdate _Update = new modelReaturnUpdate();

        Models.modelReturn _newRMA = new Models.modelReturn();
        string rga;

        //on Page Load Event Display all information on the Form for Update.
        protected void Page_Load(object sender, EventArgs e)
        {
            rga = Request.QueryString["RGAROWID"].ToString();
            if (!IsPostBack)
            {
                display(Request.QueryString["RGAROWID"].ToString());
                FillReturnDetails(Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString()));
                Obj.ReasonsIDs.PropertyChanged +=ReasonsIDs_PropertyChanged;
                Obj._ReasonList = new List<Views.ReasonList>();
                fillddlotherReasons();
                GetMainReason(Request.QueryString["RGAROWID"].ToString());
            }
        }

        public void fillddlotherReasons()
        {
            // List Of return Reasons.
            List<Reason> lsReturn = _newRMA.GetReasons();

            //Create Object Of Reason.
            //Fill Dropdown list Of OtherReason.
            Reason re = new Reason();
            re.ReasonID = Guid.NewGuid();
            re.Reason1 = "--Select--";

            lsReturn.Insert(0, re);

            ddlotherreasons.DataTextField = "Reason1";
            ddlotherreasons.DataValueField = "ReasonID";
            ddlotherreasons.DataSource = lsReturn;
            ddlotherreasons.DataBind();
        }

        public void GetMainReason(String RGA)
        {
            Return retuen = Obj.Rcall.ReturnByRGAROWID(RGA)[0];
            string[] ReturnReasons = retuen.ReturnReason.Split(new char[] { '.' });
            int flag = 0;

            for (int i = 0; i < ReturnReasons.Count(); i++)
            {
                flag = 0;

                if (ReturnReasons[i].Trim() == chkduplicate.Text.Split(new char[] { '.' })[0])
                {
                    chkduplicate.Checked = true;
                    flag = 1;
                }
                if (ReturnReasons[i].Trim() == chkitemdamaged.Text.Split(new char[] { '.' })[0])
                {
                    chkitemdamaged.Checked = true;
                    flag = 1;
                }
                if (ReturnReasons[i].Trim() == chkitemdifferent.Text.Split(new char[] { '.' })[0])
                {
                    chkitemdifferent.Checked = true;
                    flag = 1;
                }
                if (ReturnReasons[i].Trim() == chkitemordered.Text.Split(new char[] { '.' })[0])
                {
                    chkitemordered.Checked = true;
                    flag = 1;
                }
                if (ReturnReasons[i].Trim() == chknotsatisfied.Text.Split(new char[] { '.' })[0])
                {
                    chknotsatisfied.Checked = true;
                    flag = 1;
                }
                if (ReturnReasons[i].Trim() == chkwrongitem.Text.Split(new char[] { '.' })[0])
                {
                    chkwrongitem.Checked = true;
                    flag = 1;
                }

                if (flag == 0)
                {
                    txtotherreasons.Text = ReturnReasons[i].Trim();
                    ddlotherreasons.Text = ReturnReasons[i].Trim();
                }

            }

           
        }

        private void ReasonsIDs_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Obj.ReasonsIDs.UpdatePopupValue != "")
            {
                Views.ReasonList _Reason = new Views.ReasonList();
                _Reason.ID = Obj.RowID;
                _Reason.ReasonString = Obj.ReasonsIDs.UpdatePopupValue;
                Obj._ReasonList.Add(_Reason);
            }
        }

        //Return Boolean value by passing String RGA.
        //display all values Of Return information on all textboxes for Update. 
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

        /// <summary>
        /// Fill RetunDetails in GridView.
        /// </summary>
        /// <param name="lsReturnDetails">
        /// Pass List of ReturnDetails to the Methods.
        /// </param>
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
                                         ReasonIDs = _Update.ReasonsIdByHasg(Rs.ReturnDetailID)
                                     };

                gvReturnDetails.DataSource = ReaturnDetails.ToList();
                gvReturnDetails.DataBind();
            }
            catch (Exception)
            { }
        }

        //Update All Information of Return and Return Details.
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            //object of return.
            Return ret = Obj.Rcall.ReturnByRGAROWID(rga)[0];

            //list of ReturnDetails by using RGAROWID.
            List<ReturnDetail> lsretundetail = Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString());

            //Set the Return Information in Return Table.
            Guid returnid = _Update.SetReturnTbl(ret, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), Convert.ToDateTime(txtreturndate.Text),ReturnReasons());

            //set Gridview information in ReturnDetail Table.
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                Guid ReturnDetailsID = lsretundetail[i].ReturnDetailID;

                string Dquantity = (gvReturnDetails.Rows[i].FindControl("txtdeliveredquantity") as TextBox).Text;

                string Rquantity = (gvReturnDetails.Rows[i].FindControl("txtreturnquantity") as TextBox).Text;

                String SKUNumber = (gvReturnDetails.Rows[i].FindControl("txtSKU") as TextBox).Text;

                string ProductName = (gvReturnDetails.Rows[i].FindControl("txtproductame") as TextBox).Text;
                foreach (var Rowid in Obj._ReasonList )
                {
                    if (Rowid.ID == i)
                    {
                        //Delete Old SKUreasons.
                        _Update.DeleteSKuReasonsByReturnDetailID(ReturnDetailsID);
                        //Find the ReasonsID.
                        String[] Reasos = Rowid.ReasonString.Split(new char[] { '#' });

                        foreach (var resnItem in Reasos)
                        {
                            //Foreach id Save the SKUReasons table.
                            if(resnItem!="")
                            _Update.SetSkuReasons(Guid.Parse(resnItem.ToString()), ReturnDetailsID);
                        }
                    }
                }

                _Update.SetReturnDetailTbl(lsretundetail[i], Convert.ToInt16(Dquantity), Convert.ToInt16(Rquantity), SKUNumber,ProductName);

            }

            //Clear the Reasons list from Global Object.
            Obj._ReasonList = new List<Views.ReasonList>();

            Response.Redirect("~/Forms/Web Forms/frmRetunDetail.aspx");
        }



        /// <summary>
        /// String of Return Reason.
        /// </summary>
        /// <returns>
        /// Return string Of Reasons.
        /// </returns>
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

        //txtSKU_textChanged for textsuggest SKU number and ProductName.
        protected void txtSKU_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox t = (TextBox)currentRow.FindControl("txtsku");
                string rt = t.Text;

                TextBox txt = (TextBox)currentRow.FindControl("txtproductame");
                txt.Text = productcategory(rt, 0); 
            }

        }
        
        //txtReason Click open popup Window to check RMAReasons.
        protected void txtreasons_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;
              
                TextBox sku = (TextBox)currentRow.FindControl("txtsku");
                Obj.RowID = currentRow.RowIndex;

                string url = "frmPopupForEditRMAReasons.aspx?Category=" + productcategory(sku.Text, 1) + "";

                string s = "window.open('" + url + "', 'popup_window', 'width=500,height=300,left=300,top=300,resizable=yes');";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Script", s, true);
            }
        }

        /// <summary>
        /// This Method is for get Product Category.
        /// </summary>
        /// <param name="sku">
        /// String SKU pass as peremeter for Split.
        /// </param>
        /// <param name="flag">
        /// int flag is for array index.
        /// </param>
        /// <returns>
        /// Return Category.
        /// </returns>
        public string productcategory(string sku, int flag)
        {
            string _returnString = "";
            List<string> lsTrackingTbl = Obj.call._skulist(sku);
            try
            {
                if (flag == 0)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _returnString = TrackItm.ToString().Split(new char[] { '#' })[1];
                    }
                }
                else if (flag == 1)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _returnString = TrackItm.ToString().Split(new char[] { '#' })[2];
                    }
                }
               
            }
            catch (Exception)
            { }
            return _returnString;
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

    }
}