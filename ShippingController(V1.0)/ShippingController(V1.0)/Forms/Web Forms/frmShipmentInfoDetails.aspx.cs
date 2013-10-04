using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using PackingClassLibrary.CustomEntity.SMEntitys;
using ShippingController_V1._0_.Models;
using ShippingController_V1._0_.Views;
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
using System.Web.Services;


namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentInfoDetails : System.Web.UI.Page
    {
        //Static value for time spend.
        public static string TImespend ="ZERO";

        //Packing Detail Detail Information fetch
        List<cstPackageTbl> lsPacking = Obj.call.GetPackingTbl();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Maintain scrollbar position 
           
            if (!IsPostBack)
            {
                //Fill all gridview default.
                FillGvPackingInforamtion(lsPacking,true);
                _fillShippingInformationGrid(lsPacking);
                FillUserNameCmb();
                ScrolBar();
            }
        }

        #region Tracking Graph
        public void SetGraph(List<cstShipmentNumStatus> _lsGrapgPar)
        {
            Series[] sr = null;
            String[] Locations = new String[_lsGrapgPar.Count];
            List<object[]> lsObj = new List<object[]>();

            for (int i = 0; i < 6; i++)
            {
                object[] a = new object[] { 1, 1 };
                lsObj.Add(a);
            }
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
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(lsObj[5].ToArray()), Color = System.Drawing.Color.White };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(lsObj[4].ToArray()), Color = System.Drawing.Color.White };
                        sr[2] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    else if (_lsGrapgPar.Max(i => i.ShippingCompletedInt) == 5)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(lsObj[5].ToArray()), Color = System.Drawing.Color.White };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(lsObj[4].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    else if (_lsGrapgPar.Max(i => i.ShippingCompletedInt) == 6)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(lsObj[5].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(lsObj[4].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(lsObj[3].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(lsObj[2].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(lsObj[1].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(lsObj[0].ToArray()), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    #endregion
                }
                else //Single location Shipment
                {
                    #region Single location
                    if (item.ShippingCompletedInt == 4)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.White };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.White };
                        sr[2] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    else if (item.ShippingCompletedInt == 5)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.White };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    else if (item.ShippingCompletedInt == 6)
                    {
                        sr = new Series[6];
                        sr[0] = new Series { Name = "Shipped", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[1] = new Series { Name = "Shipping", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[2] = new Series { Name = "Packing", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[3] = new Series { Name = "Picking ", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[4] = new Series { Name = "Allocated", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                        sr[5] = new Series { Name = "New", Data = new Data(new object[] { 1 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                    }
                    #endregion
                }

                #region Chart code
                Highcharts chart = new Highcharts("chart")
                  .InitChart(new Chart
                  {
                      Type = ChartTypes.Bar,
                      BackgroundColor = new BackColorOrGradient(System.Drawing.Color.White),
                      Height = 200,
                      Width = 578
                  })

                  .SetTitle(new Title
                  {
                      Text = "Shipment Tracking :- " + item.ShippingNum,
                      Style = "fontSize: '20px',fontFamily: 'Verdana', fontBold: 'true', color: 'Black' "
                  })
                  .SetXAxis(new XAxis
                  {
                      Categories = (Locations),
                      Labels = new XAxisLabels { Style = "fontSize: '10px', fontFamily: 'Verdana', fontBold: 'true', color: 'Transparent'" , Enabled=false},
                      Min=0,
                      GridLineWidth=0,
                      GridLineColor= System.Drawing.Color.White
                  })
                  .SetYAxis(new YAxis
                  {
                      Min = 0,
                      Title = new YAxisTitle { Text = "", Style = "fontSize: '15px', fontFamily: 'Verdana', color: 'Transparent'" },
                      Labels = new YAxisLabels { Enabled = false },
                      GridLineWidth = 0
                  })
                  .SetTooltip(new Tooltip { Formatter = "function() { return this.series.name +'<br/>'+ this.x}" , Enabled=false})
                  .SetPlotOptions(new PlotOptions
                  {
                      Bar = new PlotOptionsBar
                      {
                          Stacking = Stackings.Percent,
                          BorderWidth =0,
                          BorderColor = System.Drawing.Color.White,
                          Shadow = false,
                          
                          DataLabels = new PlotOptionsBarDataLabels
                          {
                              Enabled = true,
                            Formatter = "function() { return '<div class=\"Test\"/></br>&nbsp;&nbsp;&nbsp;&nbsp;'+this.series.name;}",
                              Color = System.Drawing.Color.Black,
                              UseHTML=true,
                              Style = "fontSize: '14px', fontFamily: 'Arial', fontBold: 'true', color: 'Black'"
                          },
                          PointWidth = 40,
                          Point = new PlotOptionsBarPoint { },
                      }
                  })
                  .SetSeries(sr)
                  .SetLegend(new Legend
                  {
                      Enabled = false,
                      BackgroundColor = new BackColorOrGradient(System.Drawing.Color.White),
                      Align = HorizontalAligns.Center
                  });

                #endregion
                ltrChart.Text = chart.ToHtmlString();
            }
        }
          
        #endregion

        #region Functions

        [WebMethod]
        public static string ProcessIT()
        {
            string result = TImespend;
            return result;
        }

        /// <summary>
        /// Fill Grid View Of shipment information Depending on package table.
        /// </summary>
        /// <param name="PackageTableObj">list of cstPackageTbl information.</param>
        public void FillGvPackingInforamtion(List<cstPackageTbl> PackageTableObj, Boolean IsFilterShipmentAlso)
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

                    List<cstBoxPackage> boxpackage = Obj.call.GetBoxPackageByPackingID(Pckitem.PackingId);
                    foreach (var box in boxpackage)
                    {
                        TrackingNum = Obj.call.IsTrackingNum(box.BOXNUM);
                        if (TrackingNum =="")
                        {
                            TrackingNum = "1";
                        }
                    }
                    if (TrackingNum == "1")
                    {
                        TrackingNum = "N/A";
                    }
                   
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
                    if (TrackingNum == "1")
                    {
                        TrackingNum = "N/A";
                    }
                    if (TrackingNum != "N/A")
                    {
                        ShippingStatus = "Shipped";
                    }
                    
                    _shipmentInfo.TrackingNumber = TrackingNum;
                    _shipmentInfo.ShippedStatus = ShippingStatus;
                    _shipmentInfo.ManagerOVerride = Override;
                    _shipmentInfo.PackingStatus = status;
                    _shipmentInfo.PCKRowID = Pckitem.PCKROWID;
                    TimeSpan Tspent = Pckitem.EndTime - Pckitem.StartTime;
                    _shipmentInfo.StartTime = Pckitem.StartTime.ToShortTimeString();
                    _shipmentInfo.TimeSpent = Tspent.ToString(@"hh\:mm\:ss");
                    lsPacking.Add(_shipmentInfo);
                }
                gvPackingInformation.DataSource = lsPacking;
                gvPackingInformation.DataBind();
                foreach (GridViewRow row in gvPackingInformation.Rows)
                {
                    if (row.Cells[7].Text != "Packed")
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(223, 163, 137);
                    }
                    if (row.Cells[9].Text == "Shipped")
                    {
                        row.BackColor = System.Drawing.Color.FromArgb(171, 232, 134);
                    }
                }
                if (IsFilterShipmentAlso)
                {
                    _fillShippingInformationGrid(lsPackingTbl);    
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
                List<cstUserMasterTbl> lsUserMaser = Obj.call.GetUserInfoList();
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
      
        /// <summary>
        /// Clear shipment Information 
        /// </summary>
        private void _clearSKuInfo()
        {   ltrChart.Text = "";
            lblDShipmentID.Text = "";
            lblDUserName.Text = "";
            lblDPackingStatus.Text = "";
            lblDTimeSpend.Text = "";
            lblDSKUQuantity.Text = "";
            lblDLocation.Text = "";
            lblDOverrideType.Text = "";
            lblDshippingStatus.Text = "";
            lblDTrackingNumber.Text = "";
            gvSKUinfo.DataSource = new List<cstPackageDetails>();
            gvSKUinfo.DataBind();
            lblpdShipNumSelected.Text = "";
            lblBoxDetailFor.Text = "";
            gvBoxDetails.DataSource = new List<cstBoxPackage>();
            gvBoxDetails.DataBind();
            
        }

        /// <summary>
        /// fill shipping information grid from database
        /// </summary>
        /// <param name="lsPackage"></param>
        private void _fillShippingInformationGrid(List<cstPackageTbl> lsPackage)
        {
            try
            {
                List<cstShippingTbl> lsShipping = new List<cstShippingTbl>();
                var DistList = from ls in lsPackage
                               group ls by ls.ShippingNum into Gls
                               select Gls;

                foreach (var Gitm in DistList)
                {
                    cstShippingTbl _ShippingInfo = new cstShippingTbl();
                    _ShippingInfo = Obj.call.GetShippingTbl().SingleOrDefault(i => i.ShippingNum == Gitm.Key);
                    lsShipping.Add(_ShippingInfo);
                }
                gvShippingInfo.DataSource = lsShipping;
                gvShippingInfo.DataBind();
               
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Get all Box information related to the Package.
        /// </summary>
        /// <param name="PackingID">Guid Packing Id Of selected Packing </param>
        private void _fillBoxInformationGrid(Guid PackingID)
        {
            try
            {
                List<cstBoxPackage> lsBoxPackage = new List<cstBoxPackage>();
                lsBoxPackage = Obj.call.GetBoxPackageByPackingID(PackingID);
                var trackingBoxes = from box in lsBoxPackage
                                    select new
                                    {
                                        box.BOXNUM,
                                        box.BoxWeight,
                                        box.BoxHeight,
                                        box.BoxLength,
                                        box.BoxWidth,
                                        box.BoxCreatedTime,
                                        TrackingNumber = Obj.call.IsTrackingNum(box.BOXNUM)
                                    };

                ///Bind Datasource to the Grid.
                gvBoxDetails.DataSource = trackingBoxes;
                gvBoxDetails.DataBind();

            }
            catch (Exception)
            {}
        } 
        
        private void _showAdvanceSearch()
        {
            try
            {
                //_clearSKuInfo();

                lblPShipNumSelected.Text = "";
                List<cstPackageTbl> _gvPassList = modelShipmentFilter.GetPackageTbl();
                if (_gvPassList.Count > 0)
                {
                    FillGvPackingInforamtion(_gvPassList, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No record found ');", true);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Events 
       
        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            if (txtShipmentID.Text != "")
            {

                FillGvPackingInforamtion(Obj.call.GetPackingTbl(),true);
                modelShipmentFilter.ShipmentNumber = txtShipmentID.Text;
                _clearSKuInfo();
                lblPShipNumSelected.Text = "";
                List<cstPackageTbl> _gvPassList = modelShipmentFilter.GetPackageTbl();
                if (_gvPassList.Count > 0)
                {
                    FillGvPackingInforamtion(_gvPassList, true);
                    modelShipmentFilter.IsShipmentNumberFilterOn = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Shipment ID " + txtShipmentID.Text + "');", true);
                }
                txtShipmentID.Text = "";
            }
            else
            {
                modelShipmentFilter.IsShipmentNumberFilterOn = false;
            }
        }

        protected void txtBoxNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxNumber.Text.Trim() != "")
                {
                    //clear all information
                    _clearSKuInfo();

                    //Get Packing ID from packing Table
                    cstBoxPackage _packingBox = Obj.call.GetBoxPackageByBoxNumber(txtBoxNumber.Text);
                    List<cstBoxPackage> _lsBoxP = new List<cstBoxPackage>();

                    _lsBoxP.Add(_packingBox);

                    if (_packingBox != null)
                    {
                        //Packing table information for packing id taken from BoxPackage Table.
                        cstPackageTbl _packingTbl = Obj.call.GetPackingList(_packingBox.PackingID, true);
                        List<cstPackageTbl> _lspackingtbl = new List<cstPackageTbl>();
                        _lspackingtbl.Add(_packingTbl);

                        //Assign text to shipping number and process same as shipping number search.
                        txtShipmentID.Text = _packingTbl.ShippingNum.ToString();

                        //package and Package detail table information 
                        lblPShipNumSelected.Text = "";
                        FillGvPackingInforamtion(_lspackingtbl, true);
                        modelShipmentFilter.IsShipmentNumberFilterOn = false;
                        txtShipmentID.Text = "";

                        ///Sku information data Grid fill
                        List<cstPackageDetails> _lsPackingDetails = new List<cstPackageDetails>();
                        _lsPackingDetails = Obj.call.GetPackingDetailTbl(txtBoxNumber.Text);
                        gvSKUinfo.DataSource = _lsPackingDetails;
                        gvSKUinfo.DataBind();



                        ///Add Box Grid data source
                        var trackingBoxes = from box in _lsBoxP
                                            select new
                                            {
                                                box.BOXNUM,
                                                box.BoxWeight,
                                                box.BoxHeight,
                                                box.BoxLength,
                                                box.BoxWidth,
                                                box.BoxCreatedTime,
                                                TrackingNumber = Obj.call.IsTrackingNum(box.BOXNUM)
                                            };

                        ///Bind Datasource to the Grid.
                        gvBoxDetails.DataSource = trackingBoxes;
                        gvBoxDetails.DataBind();

                        lblDShipmentID.Text = _packingTbl.ShippingNum;
                        lblDUserName.Text = gvPackingInformation.Rows[0].Cells[4].Text;
                        lblDPackingStatus.Text = gvPackingInformation.Rows[0].Cells[7].Text;
                        lblDTimeSpend.Text = gvPackingInformation.Rows[0].Cells[6].Text;
                        lblDTrackingNumber.Text = gvBoxDetails.Rows[0].Cells[7].Text;
                        lblDshippingStatus.Text = gvPackingInformation.Rows[0].Cells[9].Text;
                        lblDLocation.Text = gvPackingInformation.Rows[0].Cells[3].Text;
                        lblDOverrideType.Text = gvPackingInformation.Rows[0].Cells[8].Text;

                        ///Sku Quantity
                        int SkuCount = 0;
                        foreach (cstPackageDetails item in _lsPackingDetails)
                        {
                            SkuCount = item.SKUQuantity + SkuCount;
                        }

                        lblDSKUQuantity.Text = SkuCount.ToString();

                    }
                    else
                    {
                        modelShipmentFilter.IsShipmentNumberFilterOn = false;
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Box Number " + txtBoxNumber.Text + "');", true);
                }

                txtBoxNumber.Text = "";
                txtShipmentID.Text = "";
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Box Number " + txtBoxNumber.Text + "');", true);
                txtBoxNumber.Text = "";
                //clear all information
                _clearSKuInfo();
                gvBoxDetails.DataSource = new List<cstBoxPackage>();
                gvBoxDetails.DataBind();
            }

        }

        /// <summary>
        /// Grid View Shipment Detail Information selected index changed event.
        /// </summary>
        protected void gvPackingInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _clearSKuInfo();
                lblpdShipNumSelected.Text = " for "+gvPackingInformation.SelectedRow.Cells[1].Text;
                int OverrideMode = 0;
                if (gvPackingInformation.SelectedRow.Cells[8].Text == "Self")
                {
                    OverrideMode = 2;
                }
                else if (gvPackingInformation.SelectedRow.Cells[8].Text == "Manager")
                {
                    OverrideMode = 1;
                }
                Guid PackingID = Guid.Empty;
                try
                {
                    PackingID = Obj.call.GetPackingTbl().SingleOrDefault(i => i.ShippingNum == gvPackingInformation.SelectedRow.Cells[2].Text
                        && i.ShipmentLocation == gvPackingInformation.SelectedRow.Cells[3].Text &&
                     i.MangerOverride == OverrideMode).PackingId;
                }
                catch (Exception)
                {
                }
                if (PackingID != Guid.Empty)
                {
                    //Fill Box Information grid
                    _fillBoxInformationGrid(PackingID);

                    //set label For 
                    lblBoxDetailFor.Text = " for " + gvPackingInformation.SelectedRow.Cells[1].Text;


                    List<cstPackageDetails> _lsPackingDetail = Obj.call.GetPackingDetailTbl(PackingID);
                    cstPackageTbl _PackageTbl = Obj.call.GetPackingList(PackingID, false);
                    //Number of SKUs not count of SKU's
                    int SkuCount = 0;
                    foreach (cstPackageDetails item in _lsPackingDetail)
                    {
                        SkuCount = item.SKUQuantity + SkuCount;
                    }

                    lblDSKUQuantity.Text = SkuCount.ToString();

                    if (_lsPackingDetail.Count > 0)
                    {
                        gvSKUinfo.DataSource = _lsPackingDetail;
                        gvSKUinfo.DataBind();
                        List<cstShipmentNumStatus> _lsGrapgPar = Obj.Rcall.GetShippingStatus(gvPackingInformation.SelectedRow.Cells[2].Text);
                        SetGraph(_lsGrapgPar);
                        lblDShipmentID.Text = gvPackingInformation.SelectedRow.Cells[2].Text;
                        lblDUserName.Text = gvPackingInformation.SelectedRow.Cells[4].Text;
                        lblDPackingStatus.Text = gvPackingInformation.SelectedRow.Cells[7].Text;
                        lblDTimeSpend.Text = gvPackingInformation.SelectedRow.Cells[6].Text;
                        lblDTrackingNumber.Text = gvPackingInformation.SelectedRow.Cells[10].Text;
                        lblDshippingStatus.Text = gvPackingInformation.SelectedRow.Cells[9].Text;
                        lblDLocation.Text = gvPackingInformation.SelectedRow.Cells[3].Text;
                        lblDOverrideType.Text = gvPackingInformation.SelectedRow.Cells[8].Text;

                        //Box Detils.
                        if (_PackageTbl.BoxDimension != null)
                        {
                         
                        }
                    }
                    else
                    {
                        List<cstShipmentNumStatus> _lsGrapgPar = Obj.Rcall.GetShippingStatus(gvPackingInformation.SelectedRow.Cells[2].Text);
                        SetGraph(_lsGrapgPar);
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Packing detail information no available ');", true);
                    }
                }
            }
            catch (Exception)
            { }
        }

        protected void gvShippingInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _clearSKuInfo();
                lblpdShipNumSelected.Text = "";
               
                List<cstPackageTbl> _lsPackage = Obj.call.GetPackingListByShippingNumber(gvShippingInfo.SelectedRow.Cells[1].Text);
                lblPShipNumSelected.Text = " for " + gvShippingInfo.SelectedRow.Cells[1].Text;
                FillGvPackingInforamtion(_lsPackage,false);
            }
            catch (Exception)
            {
            }
        }

        protected void gvBoxDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //_clearSKuInfo();

                List<cstPackageDetails> _lsPackingDetails = new List<cstPackageDetails>();
                _lsPackingDetails = Obj.call.GetPackingDetailTbl(gvBoxDetails.SelectedRow.Cells[1].Text);
                if (_lsPackingDetails.Count>0)
                {
                    gvSKUinfo.DataSource = _lsPackingDetails;
                    gvSKUinfo.DataBind();
                }
            }
            catch (Exception)
            { }
        }

        #endregion

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            List<cstPackageTbl> lsPacking = Obj.call.GetPackingTbl();
            _clearSKuInfo();
            txtBoxNumber.Text = "";
            lblPShipNumSelected.Text = "";
            txtShipmentID.Text = "";
            FillGvPackingInforamtion(lsPacking,true);
            FillUserNameCmb();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {}

        protected void ddlUserName_TextChanged(object sender, EventArgs e)
        {
            //User Name filter
            if (ddlUserName.SelectedValue != "-1")
            {
                Guid _userID = Guid.Empty;
                Guid.TryParse(ddlUserName.SelectedValue, out _userID);
                modelShipmentFilter.UserID = _userID;
            }
            else
            {
                modelShipmentFilter.IsUserFilerOn = false;
            }
            _showAdvanceSearch();
        }

        protected void ddlpackingStatus_TextChanged(object sender, EventArgs e)
        {
            //packing Status Filter
            if (ddlpackingStatus.SelectedValue != "-1")
            {
                modelShipmentFilter.PackingStatus = Convert.ToInt32(ddlpackingStatus.SelectedValue);
            }
            else
            {
                modelShipmentFilter.IsPackingStatusFilterOn = false;
            }
            _showAdvanceSearch();
        }

        protected void ddlOverrideMode_TextChanged(object sender, EventArgs e)
        {
            //Override Mode Filter
            if (ddlOverrideMode.SelectedValue != "-1")
            {
                modelShipmentFilter.OverrdeMode = Convert.ToInt32(ddlOverrideMode.SelectedValue);
            }
            else
            {
                modelShipmentFilter.IsOverrideModeFilterOn = false;
            }
            _showAdvanceSearch();
        }

        protected void dtpFromDate_TextChanged(object sender, EventArgs e)
        {
            //Fromand Todate
            if (dtpFromDate.Text != "" && dtpToDate.Text != "")
            {
                modelShipmentFilter.Todate = Convert.ToDateTime(dtpToDate.Text);
                modelShipmentFilter.FromDate = Convert.ToDateTime(dtpFromDate.Text);
            }
            else
            {
                modelShipmentFilter.IsDateTimeFilterOn = false;
            }
            _showAdvanceSearch();
        }

        protected void dtpToDate_TextChanged(object sender, EventArgs e)
        {
            //Fromand Todate
            if (dtpFromDate.Text != "" && dtpToDate.Text != "")
            {
                modelShipmentFilter.Todate = Convert.ToDateTime(dtpToDate.Text);
                modelShipmentFilter.FromDate = Convert.ToDateTime(dtpFromDate.Text);
            }
            else
            {
                modelShipmentFilter.IsDateTimeFilterOn = false;
            }
            _showAdvanceSearch();
        }

        protected void ddlLocation_TextChanged(object sender, EventArgs e)
        {
            //Location 
            if (ddlLocation.SelectedValue != "-1")
            {
                modelShipmentFilter.Location = ddlLocation.SelectedItem.Text;
            }
            else
            {
                modelShipmentFilter.IsLocationFilterOn = false;
            }
            _showAdvanceSearch();
        }

        protected void txtPoNumber_TextChanged(object sender, EventArgs e)
        {
            //Po Number
            if (txtPoNumber.Text != "")
            {
                modelShipmentFilter.CusTomerPo = txtPoNumber.Text;
            }
            else
            {
                modelShipmentFilter.IsCuStomerPOFilterOn = false;
                txtPoNumber.Text = "";
            }
            _showAdvanceSearch();
        }
    }
}