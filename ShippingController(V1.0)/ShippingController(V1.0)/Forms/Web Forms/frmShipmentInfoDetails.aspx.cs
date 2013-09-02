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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;


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
                      BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Transparent), Height=200, Width=1000
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
                          PointWidth = 50,
                          Point = new PlotOptionsBarPoint
                          {
                            //  Events = new PlotOptionsBarPointEvents { Click = " function() { location.href ='#='+ this.category; }" }
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
                        row.BackColor = System.Drawing.Color.FromArgb(223, 163, 137);
                    }
                    if (row.Cells[8].Text == "Shipped")
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(171, 232, 134);
                    }
                }
                _fillShippingInformationGrid(lsPackingTbl);

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
                ddlUserName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All Users--", "-1"));
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
            if (txtShipmentID.Text != "")
            {
                model_Filter.ShipmentNumber = txtShipmentID.Text;
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
            _clearSKuInfo();
             List<cstPackageTbl> _gvPassList = model_Filter.GetPackageTbl();
             if (_gvPassList.Count > 0)
             {
                 FillGvShipmentInformation(_gvPassList);
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found ');", true); 
             }
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

        protected void ddlOverrideMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOverrideMode.SelectedValue !="-1")
            {
                model_Filter.OverrdeMode = Convert.ToInt32(ddlOverrideMode.SelectedValue);
            }
            else
            {
                model_Filter.IsOverrideModeFilterOn = false;
            }
        }

        protected void dtpFromDate_TextChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Text != "" && dtpToDate.Text != "")
            {
                model_Filter.Todate = Convert.ToDateTime(dtpToDate.Text);
                model_Filter.FromDate = Convert.ToDateTime(dtpFromDate.Text);
            }
            else
            {
                model_Filter.IsDateTimeFilterOn = false;
            }
        }

        protected void dtpToDate_TextChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Text != "" && dtpToDate.Text!="")
            {
                model_Filter.Todate = Convert.ToDateTime(dtpToDate.Text);
                model_Filter.FromDate = Convert.ToDateTime(dtpFromDate.Text);
            }
            else
            {
                model_Filter.IsDateTimeFilterOn = false;
            }
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedValue!="-1")
            {
                model_Filter.Location = ddlLocation.SelectedItem.Text;
            }
            else
            {
                model_Filter.IsLocationFilterOn = false;
            }
        }

        protected void btnShowShipmentInfoID_Click(object sender, EventArgs e)
        {
            _clearSKuInfo();
            List<cstPackageTbl> _gvPassList = model_Filter.GetPackageTbl();
            if (_gvPassList.Count > 0)
            {
                FillGvShipmentInformation(_gvPassList);
                model_Filter.IsShipmentNumberFilterOn = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Shipment ID " + txtShipmentID.Text + "');", true);
            }
            txtShipmentID.Text = "";
        }

        protected void txtPoNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtPoNumber.Text != "")
            {
                model_Filter.CusTomerPo = txtPoNumber.Text;
            }
            else
            {
                model_Filter.IsCuStomerPOFilterOn = false;
                txtPoNumber.Text = "";
            }
        }

        /// <summary>
        /// Grid View Shipment Detail Information selected index changed event.
        /// </summary>
        protected void gvShipmentInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _clearSKuInfo();

                int OverrideMode = 0;
                if (gvShipmentInformation.SelectedRow.Cells[7].Text == "Self")
                {
                    OverrideMode = 2;
                }
                else if (gvShipmentInformation.SelectedRow.Cells[7].Text == "Manager")
                {
                    OverrideMode = 1;
                }
                Guid PackingID = Guid.Empty;
                try
                {
                    PackingID = Obj.call.GetPackingTbl().SingleOrDefault(i => i.ShippingNum == gvShipmentInformation.SelectedRow.Cells[1].Text
                        && i.ShipmentLocation == gvShipmentInformation.SelectedRow.Cells[2].Text &&
                     i.MangerOverride == OverrideMode).PackingId;
                }
                catch (Exception)
                {
                }
                if (PackingID != Guid.Empty)
                {
                    List<cstPackageDetails> _lsPackingDetail = Obj.call.GetPackingDetailTbl(PackingID);
                    cstPackageTbl _PackageTbl = Obj.call.GetPackingList(PackingID, false).First();
                    int SkuCount = 0;
                    foreach (cstPackageDetails item in _lsPackingDetail)
                    {
                        SkuCount = item.SKUQuantity + SkuCount;
                    }
                    lblDSKUQuantity.Text = SkuCount.ToString();

                    if (_lsPackingDetail.Count > 0)
                    {
                        gvShipmentDetail.DataSource = _lsPackingDetail;
                        gvShipmentDetail.DataBind();
                        List<cstShipmentNumStatus> _lsGrapgPar = Obj.Rcall.GetShippingStatus(gvShipmentInformation.SelectedRow.Cells[1].Text);
                        SetGraph(_lsGrapgPar);
                        lblDShipmentID.Text = gvShipmentInformation.SelectedRow.Cells[1].Text;
                        lblDUserName.Text = gvShipmentInformation.SelectedRow.Cells[3].Text;
                        lblDPackingStatus.Text = gvShipmentInformation.SelectedRow.Cells[6].Text;
                        lblDTimeSpend.Text = gvShipmentInformation.SelectedRow.Cells[5].Text;
                        lblDTrackingNumber.Text = gvShipmentInformation.SelectedRow.Cells[9].Text;
                        lblDshippingStatus.Text = gvShipmentInformation.SelectedRow.Cells[8].Text;
                        lblDLocation.Text = gvShipmentInformation.SelectedRow.Cells[2].Text;
                        lblDOverrideType.Text = gvShipmentInformation.SelectedRow.Cells[7].Text;

                        //Box Detils.
                        if (_PackageTbl.BoxDimension!=null)
                        {
                            lblBHeight.Text = _PackageTbl.BoxHeight.ToString() + " inch";
                            lblBwidth.Text = _PackageTbl.BoxWidth.ToString() + " inch";
                            lblBWeight.Text = _PackageTbl.BoxWeight.ToString() + " Kg.";
                            lblBlength.Text = _PackageTbl.BoxLength.ToString() + " inch";
                            lblBMeasureTime.Text = Convert.ToDateTime(_PackageTbl.BoxDimension.ToString()).ToString("MMM dd, yyyy hh:mm tt");
                            lblBType.Text = _PackageTbl.BoxType.ToString();

                        }
                        
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Packing detail information no available ');", true);
                    }
                }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Clear shipment Information 
        /// </summary>
        private void _clearSKuInfo()
        {
         //   txtShipmentID.Text = "";
            ltrChart.Text = "";
            List<cstPackageDetails> _lsPackingDetail = new List<cstPackageDetails>();
            lblDShipmentID.Text = "";
            lblDUserName.Text = "";
            lblDPackingStatus.Text = "";
            lblDTimeSpend.Text = "";
            lblDSKUQuantity.Text = "";
            lblDLocation.Text = "";
            lblDOverrideType.Text = "";
            lblDshippingStatus.Text = "";
            lblDTrackingNumber.Text = "";
            gvShipmentDetail.DataSource = _lsPackingDetail;
            gvShipmentDetail.DataBind();
            lblBHeight.Text = "";
            lblBlength.Text = "";
            lblBMeasureTime.Text = "";
            lblBType.Text = "Unknown";
            lblBWeight.Text = "";
            lblBwidth.Text = "";
        }

        private void _fillShippingInformationGrid(List<cstPackageTbl> lsPackage)
        {
            try
            {
                List<cstShippingTbl> lsShipping = new List<cstShippingTbl>();
                //List<cstPackageTbl> _lsDist = lsPackage.GroupBy(i => i.ShippingNum).ToList();

                var DistList = from ls in lsPackage
                                group ls by ls.ShippingNum into Gls
                                select Gls;

                foreach (var Gitm in DistList)
                {
                    cstShippingTbl _ShippingInfo = new cstShippingTbl();
                    _ShippingInfo = Obj.call.GetShippingTbl().SingleOrDefault(i => i.ShippingNum == Gitm.Key);
                    lsShipping.Add(_ShippingInfo);
                }
                //foreach (cstPackageTbl packageItem in _lsDist)
                //{
                //    cstShippingTbl _ShippingInfo = new cstShippingTbl();
                //    _ShippingInfo = Obj.call.GetShippingTbl().SingleOrDefault(i => i.ShippingNum == packageItem.ShippingNum);
                //    lsShipping.Add(_ShippingInfo);
                //}
                gvShippingInfo.DataSource = lsShipping;
                gvShippingInfo.DataBind();
            }
            catch (Exception)
            {}
        }

        protected void gvShippingInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<cstPackageTbl> _lsPackage = Obj.call.GetPackingListByShippingNumber(gvShippingInfo.SelectedRow.Cells[1].Text);

                FillGvShipmentInformation(_lsPackage);
            }
            catch (Exception)
            {
            }
        }

      
    }        
}