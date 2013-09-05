using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using ShippingController_V1._0_.Classes;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmHomePage : System.Web.UI.Page
    {
        smController Call = new smController();
        protected void Page_Load(object sender, EventArgs e)
        {
           //ss MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
               
                FillgvPackingShipments();
                FillgvlatestLogin();
                SetGraph();
            }
        }

        public void SetGraph()
        {
            List<cstStationToatlPacked> _lsTotalPacekedPerStation = Obj.Rcall.GetStationTotalPaked(DateTime.Now);

            Series[] sr = new Series[_lsTotalPacekedPerStation.Count];

            // chart Veriables
            String[] StationNames = new string[_lsTotalPacekedPerStation.Count];

            for (int i = 0; i < _lsTotalPacekedPerStation.Count; i++)
            {
                sr[i] = new Series { Name = _lsTotalPacekedPerStation[i].StationName, Data = new Data(new object[] { _lsTotalPacekedPerStation[i].TotalPacked, _lsTotalPacekedPerStation[i].PartiallyPacked }) };
            }

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("Chart")
            .InitChart(new DotNet.Highcharts.Options.Chart
            {
                Type = ChartTypes.Bar,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.White),
                Height =299,


            })
            .SetXAxis(new DotNet.Highcharts.Options.XAxis
            {

                Categories = (new string[] { "Packed", "Partially Packed" }),
                Title = new XAxisTitle { Text = "Packing Status", Style = "fontSize: '15px', fontFamily: 'Verdana', color: 'Black'" }

            })
             .SetTitle(new Title
             {
                 Text = "Station Information ",
                 Style = "fontSize: '30px',fontFamily: 'Verdana', fontBold: 'true', color: 'Black' "
             })
             .SetYAxis(new YAxis
             {
                 Title = new YAxisTitle { Text = "Staion Names", Style = "fontSize: '15px', fontFamily: 'Verdana', color: 'Black'" },
             })
             .SetSeries(sr);

            ltrChart.Text = chart.ToHtmlString();


        }
       
        public void FillgvPackingShipments()
        {
            try
            {
                List<cstPackageTbl> lsShipmetn =Obj.call.GetPackingTbl();
                var v = (from s in lsShipmetn
                        where s.PackingStatus == 1
                        && s.StartTime.Date == DateTime.Now.Date && s.StartTime.Month == DateTime.Now.Month && s.StartTime.Year == DateTime.Now.Year
                        select new
                        {
                            PackingID = s.ShippingNum,
                            ShipmentLocation = s.ShipmentLocation,
                            UserName = Call.GetSelcetedUserMaster(s.UserID).FirstOrDefault().UserFullName,
                            Date = s.StartTime
                        }).OrderByDescending(X=>X.Date);

                gvShipmentPacking.DataSource = v;
                gvShipmentPacking.DataBind();
            }
            catch (Exception)
            {
              
            }
        }
        public void FillgvlatestLogin()
        {
            try
            {
                List<cstUserCurrentStationAndDeviceID> lsCurrent = new List<cstUserCurrentStationAndDeviceID>();
                List<cstUserCurrentStationAndDeviceID> lsStation = Call.GetlastLoginStationAllUsers();
                foreach (var Stationitem in lsStation)
                {
                    DateTime Dt = Convert.ToDateTime(Stationitem.Datetime);
                    if (Dt.Date == DateTime.Now.Date)
                    {
                        lsCurrent.Add(Stationitem);
                    }
                }
                gvLatestLogin.DataSource = lsCurrent;
                gvLatestLogin.DataBind();
            }
            catch (Exception)
            {
            }
        }
       
    }
}