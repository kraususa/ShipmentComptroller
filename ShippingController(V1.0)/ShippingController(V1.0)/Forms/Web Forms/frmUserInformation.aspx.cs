using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Objects;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Classes.DisplayEntitys;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmHomeIcon : System.Web.UI.Page
    {
        int ActiveUsers = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //fill active gridview.
                FillgvActiveUsers();
                FillCounter();
               
            }
        }

        public void FillCounter()
        {
            int TotalUsers = Obj.call.GetUserInfoList().Count();
            int InActiveUsers = TotalUsers - ActiveUsers;
            
            //Set Users to label
            lblCActiveUsers.Text = ActiveUsers.ToString();
            lblCInactiveUsers.Text = InActiveUsers.ToString();
            lblCTotalUsers.Text = TotalUsers.ToString();
        }


        public void FillgvActiveUsers()
        {
            try
            {
                List<cstUserCurrentStationAndDeviceID> lsCurrent = new List<cstUserCurrentStationAndDeviceID>();
                List<cstUserCurrentStationAndDeviceID> lsStation = Obj.call.GetlastLoginStationAllUsers();
                foreach (var Stationitem in lsStation)
                {
                    DateTime Dt = Convert.ToDateTime(Stationitem.Datetime);
                    if (Dt.Date == DateTime.Now.Date)
                    {
                        lsCurrent.Add(Stationitem);
                    }
                }
                List<cstShipmentPackedTodayAndAvgTime> lsAvg = Obj.call.GetPackingCountCurrentShipmentUserName();

                List<cstHomePageGv> lsHomeinfo = new List<cstHomePageGv>();


                List<cstPackageTbl> lsPackingtbl = Obj.call.GetPackingTbl();
                var CurrentShp = from current in lsPackingtbl
                                 where current.PackingStatus == 1
                                 select current;


                foreach (var Packingitem in lsCurrent)
                {
                    cstHomePageGv HomeGv = new cstHomePageGv();
                    HomeGv.UserID = Packingitem.UserID;
                    HomeGv.UserName = Packingitem.UserName;
                    HomeGv.Packed = 0;
                    try{HomeGv.Packed = lsAvg.SingleOrDefault(i => i.UserID == Packingitem.UserID).Packed;}catch (Exception){}
                    HomeGv.CurrentPackingShipmentID ="Not Packing";
                    try{HomeGv.CurrentPackingShipmentID = CurrentShp.SingleOrDefault(k => k.UserID == Packingitem.UserID).ShippingNum;}catch (Exception){}
                    HomeGv.StationName = Packingitem.StationName;
                    HomeGv.DeviceID = Packingitem.DeviceID;
                    HomeGv.Datetime = Packingitem.Datetime;
                    lsHomeinfo.Add(HomeGv);
                }
                //Count Active Users
                ActiveUsers = lsHomeinfo.Count();

                if (lsHomeinfo.Count>0)
                {
                    lblActive.Text = "Active Users";
                    lblActive.ForeColor = System.Drawing.Color.White;
                    gvLatestLogin.DataSource = lsHomeinfo;
                    gvLatestLogin.DataBind();    
                }
                else
                {
                    lblActive.Text = "No Active User";
                    lblActive.ForeColor = System.Drawing.Color.FromArgb(255, 140, 0);
                }

            }
            catch (Exception)
            {
            }
        }
    }
}