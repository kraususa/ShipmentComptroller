using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.SqlClient;
using ShippingController_V1._0_.Classes.DisplayEntitys;
using PackingClassLibrary.CustomEntity.SMEntitys;
using System.Reflection;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentInfoALl : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Maintain Scroll position in Gridview..

            ScrolBar();
           
            
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["location"] != null && Request.QueryString["location"] != "")
                    {
                        PropertyInfo isreadonly =typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                        // make collection editable
                        isreadonly.SetValue(this.Request.QueryString, false, null);
                        Request.QueryString.Clear();
                        txtShipmentID.Text = Session["PackingID"].ToString();
                        textchanted();
                    }
                    else
                    {
                        txtShipmentID.Focus();
                        FillGvShipmentInformation();
                    }
                }
                catch (Exception)
                { }
                
            }
        }
        private void ScrolBar()
        {
            string script;
            script = "window.document.getElementById('" + PosX.ClientID + "').value = "
                      + "window.document.getElementById('" + panel1.ClientID + "').scrollLeft;"
                      + "window.document.getElementById('" + PosY.ClientID + "').value = "
                      + "window.document.getElementById('" + panel1.ClientID + "').scrollTop;";
            this.ClientScript.RegisterOnSubmitStatement(this.GetType(), "SavePanelScroll", script);
            if (IsPostBack)
            {
                script = "window.document.getElementById('" + panel1.ClientID + "').scrollLeft = "
                        + "window.document.getElementById('" + PosX.ClientID + "').value;"
                        + "window.document.getElementById('" +  panel1.ClientID + "').scrollTop = "
                        + "window.document.getElementById('" + PosY.ClientID + "').value;";

                this.ClientScript.RegisterStartupScript(this.GetType(), "SetPanelScroll", script, true);
            }
        }


        public void FillGvShipmentInformation()
        {
            try
            {
                List<cstShipmentInformationAll> lsPacking = new List<cstShipmentInformationAll>();
                List<cstPackageTbl> lsPackingTbl = Obj.call.GetPackingTbl();

                foreach (var Pckitem in lsPackingTbl)
                {  String status = "Packed";
                    String Override = "No";
                    String ShippingStatus = "Not Shipped";
                    String TrackingNum = "N/A";
                    cstTrackingTbl Trackingtbl = null;
                    try
                    {
                        Trackingtbl = Obj.call.GetTrackingTbl(Pckitem.PackingId, Pckitem.ShippingID)[0];
                    }
                    catch (Exception)
                    {}
                    cstShipmentInformationAll _shipmentInfo = new cstShipmentInformationAll();
                    _shipmentInfo.ShipmentID = Pckitem.ShippingNum;
                    _shipmentInfo.UserName = Obj.call.GetSelcetedUserMaster(Pckitem.UserID).FirstOrDefault().UserFullName.ToString();
                    _shipmentInfo.Location = Pckitem.ShipmentLocation;
                  
                    if (Pckitem.PackingStatus ==1)
                    {
                        status = "Partially packed";
                    }
                   
                    if (Pckitem.MangerOverride == 1)
                    {
                        Override = "Manager";
                    }
                    else if(Pckitem.MangerOverride == 2)
                    {
                        Override = "Self";
                    }
                    if (Trackingtbl != null )
                    {
                        ShippingStatus = "Shipped";
                        TrackingNum = Trackingtbl.TrackingNum;
                    }
                    _shipmentInfo.TrackingNumber = TrackingNum;
                    _shipmentInfo.ShippedStatus = ShippingStatus;
                    _shipmentInfo.ManagerOVerride = Override;
                    _shipmentInfo.PackingStatus = status;
                    TimeSpan Tspent = Pckitem.EndTime - Pckitem.StartTime;
                    _shipmentInfo.StartTime = Pckitem.StartTime.ToShortTimeString();
                    _shipmentInfo.TimeSpent = Tspent.ToString(@"hh\:mm\:ss");
                    lsPacking.Add(_shipmentInfo);
                }
                gvShipmentInformation.DataSource = lsPacking;
                gvShipmentInformation.DataBind();
                foreach ( GridViewRow row in gvShipmentInformation.Rows)
                {
                    if (row.Cells[6].Text != "Packed")
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(210,127,91);
                    }
                    if (row.Cells[8].Text =="Shipped")
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(93,188,111);
                    }
                }

            }
            catch (Exception)
            {}
        }

        protected void gvShipmentInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int OverrideMode = 0;
                if (gvShipmentInformation.SelectedRow.Cells[7].Text.ToString() == "Manager")
                {
                    OverrideMode = 1;
                }
                else if (gvShipmentInformation.SelectedRow.Cells[7].Text.ToString() == "Self")
                {
                    OverrideMode = 2;
                }
                Session["ShipmentID"] = Obj.call.GetPackingNum(gvShipmentInformation.SelectedRow.Cells[1].Text.ToString(), OverrideMode, gvShipmentInformation.SelectedRow.Cells[2].Text.ToString());
                
                Response.Redirect("~/Forms/Web Forms/frmShipmentDetail.aspx");
            }
            catch (Exception)
            {}
        }



        public void textchanted()
        {
            try
            {
                try
                {
                    List<cstShipmentInformationAll> lsPacking = new List<cstShipmentInformationAll>();
                    List<cstPackageTbl> lsPackingTbl = Obj.call.GetPackingTbl();
                    var FilterList = from ls in lsPackingTbl
                                     where ls.ShippingNum == txtShipmentID.Text
                                     select ls;

                    if (FilterList.Count() > 0)
                    {


                        foreach (var Pckitem in FilterList)
                        {
                            String status = "Packed";
                            String Override = "No";
                            String ShippingStatus = "Not Shipped";
                            String TrackingNum = "N/A";
                            cstTrackingTbl Trackingtbl = null;
                            try
                            {
                                Trackingtbl = Obj.call.GetTrackingTbl(Pckitem.PackingId, Pckitem.ShippingID)[0];
                            }
                            catch (Exception)
                            { }
                            cstShipmentInformationAll _shipmentInfo = new cstShipmentInformationAll();
                            _shipmentInfo.ShipmentID = Pckitem.ShippingNum.ToUpper();
                            _shipmentInfo.UserName = Obj.call.GetSelcetedUserMaster(Pckitem.UserID).FirstOrDefault().UserFullName.ToString();
                            _shipmentInfo.Location = Pckitem.ShipmentLocation;
                            if (Pckitem.PackingStatus == 1)
                            {
                                status = "Partially packed";
                            }
                            if (Pckitem.MangerOverride == 1)
                            {
                                Override = "Manager";
                            }
                            else if (Pckitem.MangerOverride == 2)
                            {
                                Override = "Self";
                            }
                            if (Trackingtbl != null)
                            {
                                ShippingStatus = "Shipped";
                                TrackingNum = Trackingtbl.TrackingNum;
                            }
                            _shipmentInfo.TrackingNumber = TrackingNum;
                            _shipmentInfo.ShippedStatus = ShippingStatus;
                            _shipmentInfo.ManagerOVerride = Override;
                            _shipmentInfo.PackingStatus = status;
                            TimeSpan Tspent = Pckitem.EndTime - Pckitem.StartTime;
                            _shipmentInfo.StartTime = Pckitem.StartTime.ToShortTimeString();
                            _shipmentInfo.TimeSpent = Tspent.ToString(@"hh\:mm\:ss");
                            lsPacking.Add(_shipmentInfo);

                        }

                        gvShipmentInformation.DataSource = lsPacking;
                        gvShipmentInformation.DataBind();

                        foreach (GridViewRow row in gvShipmentInformation.Rows)
                        {
                            if (row.Cells[6].Text != "Packed")
                            {
                                row.BackColor = System.Drawing.Color.FromArgb(210, 127, 91);
                            }
                            if (row.Cells[8].Text == "Shipped")
                            {
                                row.BackColor = System.Drawing.Color.FromArgb(93, 188, 111);
                            }
                        }
                    }
                    else
                    {
                        FillGvShipmentInformation();
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            {}
        }

        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            textchanted();
        }
    }
}