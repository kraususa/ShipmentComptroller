using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using PackingClassLibrary.CustomEntity.SMEntitys;
using ShippingController_V1._0_.Classes;
using ShippingController_V1._0_.Classes.DisplayEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentInfoDetails : System.Web.UI.Page
    {
        List<cstPackageTbl> lsPacking = Obj.call.GetPackingTbl();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Maintain scrollbar position 
            ScrolBar();

            if (!IsPostBack)
            {
                FillGvShipmentInformation(lsPacking);
                 FillUserNameCmb();
            }
        }


        #region Tracking Graph
        public void SetGraph(List<cstShipmentNumStatus> _lsGrapgPar)
        {
            Series[] sr = null;
            String[] Locations = new String[_lsGrapgPar.Count];
            List<object[]> lsObj = new List<object[]>();


            #region multilocation shipment data
            if (_lsGrapgPar.Count > 1 && _lsGrapgPar[0].ShippingCompletedInt != _lsGrapgPar[1].ShippingCompletedInt)
            {
                if (_lsGrapgPar[1].ShippingCompletedInt > _lsGrapgPar[0].ShippingCompletedInt)
                {

                    int lsdiff = _lsGrapgPar[1].ShippingCompletedInt - _lsGrapgPar[0].ShippingCompletedInt;
                    for (int i = 0; i < _lsGrapgPar[0].ShippingCompletedInt; i++)
                    {
                        object[] a = new object[] { 1, 1 };
                        lsObj.Add(a);
                    }

                    for (int i = 0; i < lsdiff; i++)
                    {
                        object[] a = new object[] { 1 };
                        lsObj.Add(a);
                    }
                }
                else if (_lsGrapgPar[0].ShippingCompletedInt > _lsGrapgPar[1].ShippingCompletedInt)
                {
                    int lsdiff = _lsGrapgPar[0].ShippingCompletedInt - _lsGrapgPar[1].ShippingCompletedInt;
                    for (int i = 0; i < _lsGrapgPar[1].ShippingCompletedInt; i++)
                    {
                        object[] a = new object[] { 1, 1 };
                        lsObj.Add(a);
                    }

                    for (int i = 0; i < lsdiff; i++)
                    {
                        object[] a = new object[] { 1 };
                        lsObj.Add(a);
                    }
                }
            }
            else if (_lsGrapgPar.Count > 1 && _lsGrapgPar[0].ShippingCompletedInt == _lsGrapgPar[1].ShippingCompletedInt)
            {
                for (int i = 0; i < _lsGrapgPar.Max(j => j.ShippingCompletedInt); i++)
                {
                    object[] a = new object[] { 1, 1 };
                    lsObj.Add(a);
                }
            }
            #endregion

            //Add locations to graph
            for (int i = 0; i < _lsGrapgPar.Count; i++)
            {
                Locations[i] = _lsGrapgPar[i].Location.ToString();
            }

            foreach (cstShipmentNumStatus item in _lsGrapgPar)
            {
                if (_lsGrapgPar.Count > 1) //Multilocation Shipment
                {
                    #region multilocation Shipment
                    if (_lsGrapgPar.Max(i => i.ShippingCompletedInt) == 4)
                    {
                        sr = new Series[4];
                        sr[0] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[1] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[2] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[3] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    else if (_lsGrapgPar.Max(i => i.ShippingCompletedInt) == 5)
                    {
                        sr = new Series[5];
                        sr[0] = new Series { Name = "Shipping", Data = new Data(lsObj[4].ToArray()), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                        sr[1] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[2] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[3] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[4] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    else if (_lsGrapgPar.Max(i => i.ShippingCompletedInt) == 6)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(lsObj[5].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(lsObj[4].ToArray()), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[5] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    #endregion
                }
                else //Single location Shipment
                {
                    #region Single location
                    if (item.ShippingCompletedInt == 4)
                    {
                        sr = new Series[4];
                        sr[0] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[1] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[2] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[3] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    else if (item.ShippingCompletedInt == 5)
                    {
                        sr = new Series[5];
                        sr[0] = new Series { Name = "Shipping", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                        sr[1] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[2] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[3] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[4] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    else if (item.ShippingCompletedInt == 6)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                        sr[5] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
                    }
                    #endregion
                }

                #region Chart code
                Highcharts chart = new Highcharts("chart")
                  .InitChart(new Chart
                  {
                      Type = ChartTypes.Bar,
                      BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Transparent)
                  })
                  .SetTitle(new Title
                  {
                      Text = "Shipment Tracking :- " + item.ShippingNum,
                      Style = "fontSize: '30px',fontFamily: 'Verdana', fontBold: 'true', color: 'White' "
                  })
                  .SetXAxis(new XAxis
                  {
                      Categories = (Locations),
                      Labels = new XAxisLabels { Style = "fontSize: '25px', fontFamily: 'Verdana', fontBold: 'true', color: 'White'" }
                  })
                  .SetYAxis(new YAxis
                  {
                      Min = 0,
                      Title = new YAxisTitle { Text = "", Style = "fontSize: '15px', fontFamily: 'Verdana', color: 'White'" },
                      Labels = new YAxisLabels { Enabled = false },
                      GridLineWidth = 0
                  })
                  .SetTooltip(new Tooltip { Formatter = "function() { return this.series.name +'<br/>'+ this.x}" })
                  .SetPlotOptions(new PlotOptions
                  {
                      Bar = new PlotOptionsBar
                      {
                          Stacking = Stackings.Normal,
                          Shadow = true,

                          DataLabels = new PlotOptionsBarDataLabels
                          {
                              Enabled = true,
                              Formatter = "function() { return this.series.name; }",
                              Color = System.Drawing.Color.Black,
                              Style = "fontSize: '25px', fontFamily: 'Verdana', fontBold: 'true', color: 'Black'"
                          },
                          //   Events = new PlotOptionsBarEvents { Click = "function() {''}" },
                          PointWidth = 70,
                          Point = new PlotOptionsBarPoint
                          {
                              Events = new PlotOptionsBarPointEvents { Click = " function() { location.href ='/Forms/Web Forms/frmShipmentInfoALl.aspx?location='+ this.category; }" }
                          }//'+ this.series.name +'
                      }
                  })
                  .SetSeries(sr)
                  .SetLegend(new Legend
                  {
                      BackgroundColor = new BackColorOrGradient(System.Drawing.Color.WhiteSmoke),
                      Align = HorizontalAligns.Center
                  });

                #endregion
                ltrChart.Text = chart.ToHtmlString();
            }
        }
        #endregion

        /// <summary>
        /// Fill Grid View Of shipment information Depending on package table.
        /// </summary>
        /// <param name="PackageTableObj">list of cstPackageTbl information.</param>
        public void FillGvShipmentInformation( List<cstPackageTbl> PackageTableObj)
        {
            try
            {
                List<cstShipmentInformationAll> lsPacking = new List<cstShipmentInformationAll>();
                 List<cstPackageTbl> lsPackingTbl = PackageTableObj;

                foreach (var Pckitem in lsPackingTbl)
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
                    _shipmentInfo.ShipmentID = Pckitem.ShippingNum;
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
            catch (Exception)
            { }
        }

        /// <summary>
        /// Fill UserName Combo.
        /// </summary>
        public void FillUserNameCmb()
        {
            

            try
            {
                List<cstUserMasterTbl> lsUserMaser =Obj.call.GetUserInfoList();
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
      
        /// <summary>
        /// Maintatin Scrollbar position on post back.
        /// </summary>
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
                        + "window.document.getElementById('" + panel1.ClientID + "').scrollTop = "
                        + "window.document.getElementById('" + PosY.ClientID + "').value;";

                this.ClientScript.RegisterStartupScript(this.GetType(), "SetPanelScroll", script, true);
            }
        }

        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            if (txtShipmentID.Text !="")
            {
                model_Filter.ShipmentNumber = txtShipmentID.Text;
                List<cstShipmentNumStatus> _lsGrapgPar = Obj.Rcall.GetShippingStatus(txtShipmentID.Text);
                SetGraph(_lsGrapgPar);
            }
            else
            {
                model_Filter.IsShipmentNumberFilterOn = false;
            }
        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUserName.SelectedValue != "-1")
            {
                Guid _userID = Guid.Empty;
                Guid.TryParse(ddlUserName.SelectedValue, out _userID);
                model_Filter.UserID = _userID;
            }
            else
            {
                model_Filter.IsUserFilerOn = false;
            }
            
            
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            FillGvShipmentInformation(model_Filter.GetPackageTbl());
        }

        protected void ddlpackingStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpackingStatus.SelectedValue !="-1")
            {
                model_Filter.PackingStatus = Convert.ToInt32(ddlpackingStatus.SelectedValue);
            }
            else
            {
                model_Filter.IsPackingStatusFilterOn = false;
            }
        }
    }
}