using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Classes;
using PackingClassLibrary.CustomEntity.ReportEntitys;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentDetail : System.Web.UI.Page
    {
        smController call = new smController();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUserNameCmb();
                FillStatusCombo();
                dvInfo.Visible = false;
                dvRight.Visible = false;
                dtpFromDate.Text = DateTime.Now.Date.ToString("MMM dd, yyyy");
                dtpToDate.Text = DateTime.Now.Date.ToString("MMM dd, yyyy");
            }
            
        }

        public void FillUserNameCmb()
        {
            try
            {
                List<cstUserMasterTbl> lsUserMaser =call.GetAllUserInfoList();
                ddlUserName.DataValueField = "UserID";
                ddlUserName.DataTextField = "UserFullName";
                ddlUserName.DataSource = lsUserMaser;
                ddlUserName.DataBind();
                ddlUserName.Items.Insert(0, new ListItem("--Select--", "-1"));
                ddlUserName.SelectedIndex = -1;
            }
            catch (Exception)
            { }
        }

        public void FillStatusCombo()
        {
            List<PackingStat> lspackingStatus = new List<PackingStat>();
            PackingStat Status = new PackingStat();
            Status.ID = 0;
            Status.Status = "Packed";
            lspackingStatus.Add(Status);
            PackingStat Status1 = new PackingStat();
            Status1.ID = 1;
            Status1.Status = "Partially Packed";
            lspackingStatus.Add(Status1);
            ddlpackingStatus.DataValueField = "ID";
            ddlpackingStatus.DataTextField = "Status";
            ddlpackingStatus.DataSource=lspackingStatus;
            ddlpackingStatus.DataBind();
            ddlpackingStatus.Items.Insert(0, new ListItem("--Select--", "-1"));
            ddlpackingStatus.SelectedIndex = -1;
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                dvInfo.Visible = true;
                dvLeft.Visible = true;
                dvRight.Visible = false;
                FillShipmentlist();
            }
            catch (Exception)
            {}
        }

        private List<cstPackingTime> FillShipmentlist()
        {
            List<cstPackingTime> lspackingTime = new List<cstPackingTime>();
            try
            {
                if (ddlUserName.SelectedItem.Value != "-1" && ddlpackingStatus.SelectedItem.Value != "-1")
                {
                    int PackingStatus = Convert.ToInt32(ddlpackingStatus.SelectedItem.Value.ToString());
                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Text);
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Text);
                    long UserID = Convert.ToInt64(ddlUserName.SelectedItem.Value);
                    lspackingTime = call.GetPackingTimeQuantity(UserID, FromDate, ToDate, PackingStatus);
                    gvShipmentList.DataSource = lspackingTime;
                    gvShipmentList.DataBind();
                }
                else if (ddlUserName.SelectedItem.Value != "-1" && ddlpackingStatus.SelectedItem.Value == "-1")
                {
                   
                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Text);
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Text);
                    long UserID = Convert.ToInt64(ddlUserName.SelectedItem.Value);
                     lspackingTime = call.GetPackingTimeQuantity(UserID, FromDate, ToDate);
                    gvShipmentList.DataSource = lspackingTime;
                    gvShipmentList.DataBind();
                }
                else if (ddlUserName.SelectedItem.Value == "-1" && ddlpackingStatus.SelectedItem.Value != "-1")
                {
                    int PackingStatus = Convert.ToInt32(ddlpackingStatus.SelectedItem.Value.ToString());
                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Text);
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Text);
                    lspackingTime = call.GetPackingTimeQuantity( FromDate, ToDate, PackingStatus);
                    gvShipmentList.DataSource = lspackingTime;
                    gvShipmentList.DataBind();
                }
                else
                {
                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Text);
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Text);
                    lspackingTime = call.GetPackingTimeQuantity(FromDate, ToDate);
                    gvShipmentList.DataSource = lspackingTime;
                    gvShipmentList.DataBind();
                }
               
            }
            catch (Exception)
            { }
            return lspackingTime;
        }

        protected void gvShipmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlpackingStatus.SelectedItem.Value != "1")
                {
                    dvRight.Visible = true;
                    List<cstPackingTime> packingTime = FillShipmentlist();

                    List<cstPackingDetailTbl> lsPackingDetails = call.GetPackingDetailTbl(gvShipmentList.SelectedRow.Cells[0].Text.ToString());
                    gvShipmentDetail.DataSource = lsPackingDetails;
                    gvShipmentDetail.DataBind();
                    cstPackingTime Pselected = packingTime.SingleOrDefault(i => i.ShipmemtID == lsPackingDetails[0].PackingId);
                    //--
                    lblCShipmentID.Text = lsPackingDetails[0].PackingId.ToString();
                    lblCStatus.Text = "Packed";
                    lblCTime.Text = Pselected.TimeSpend.ToString();
                    lblCSkuQty.Text = Pselected.Quantity.ToString();
                    String Location = lsPackingDetails[0].ShipmentLocation.ToString();
                    String UserName = call.GetSelcetedUserMaster(Convert.ToInt64(call.GetPackingList(lblCShipmentID.Text.ToString(), Location).First().UserID)).First().UserFullName.ToString();

                    foreach (var LocationItem in lsPackingDetails)
                    {
                        lblCUserName.Text = UserName;
                        lblCLocation.Text = Location;
                        if (Location != LocationItem.ShipmentLocation)
                        {
                            Location = LocationItem.ShipmentLocation;
                            UserName = call.GetSelcetedUserMaster(Convert.ToInt64(call.GetPackingList(lblCShipmentID.Text.ToString(), Location).First().UserID)).First().UserFullName.ToString();
                            lblCLocation.Text = Location + " & " + lblCLocation.Text;
                            lblCUserName.Text = UserName + " & " + lblCUserName.Text;
                        }
                    }
                }
                else
                {
                   ScriptManager.RegisterStartupScript(this, Page.GetType(),"alertMsg","alert('This is Partially Packed Shipment. Detail Information not availabel.');",true); 
                }
            }
            catch (Exception)
            { }
        }

        protected void btnShowShipmentInfoID_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtShipmentID.Text.Trim() != "")
                {
                    dvInfo.Visible = true;
                    dvRight.Visible = true;
                    dvLeft.Visible = false;
                    List<cstPackingTime> packingTime = call.GetPackingTimeQuantity();
                    List<cstPackingDetailTbl> lsPackingDetails = call.GetPackingDetailTbl(txtShipmentID.Text);
                    gvShipmentDetail.DataSource = lsPackingDetails;
                    gvShipmentDetail.DataBind();
                    cstPackingTime Pselected = packingTime.SingleOrDefault(i => i.ShipmemtID == lsPackingDetails[0].PackingId);
                    //--
                    lblCShipmentID.Text = lsPackingDetails[0].PackingId.ToString();
                    lblCStatus.Text = "Packed";
                    lblCTime.Text = Pselected.TimeSpend.ToString();
                    lblCSkuQty.Text = Pselected.Quantity.ToString();
                    String Location = lsPackingDetails[0].ShipmentLocation.ToString();
                    String UserName = call.GetSelcetedUserMaster(Convert.ToInt64(call.GetPackingList(lblCShipmentID.Text.ToString(), Location).First().UserID)).First().UserFullName.ToString();

                    foreach (var LocationItem in lsPackingDetails)
                    {
                        lblCUserName.Text = UserName;
                        lblCLocation.Text = Location;
                        if (Location != LocationItem.ShipmentLocation)
                        {
                            Location = LocationItem.ShipmentLocation;
                            UserName = call.GetSelcetedUserMaster(Convert.ToInt64(call.GetPackingList(lblCShipmentID.Text.ToString(), Location).First().UserID)).First().UserFullName.ToString();
                            lblCLocation.Text = Location + " & " + lblCLocation.Text;
                            lblCUserName.Text = UserName + " & " + lblCUserName.Text;
                        }
                    }
                    gvShipmentDetail.DataSource = lsPackingDetails;
                    gvShipmentDetail.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alertMsg", "alert('Please Enter Shipment ID.');", true);
                }
                
            }
            catch (Exception)
            {}
        }
    }
}