using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Classes.DisplayEntitys;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmHomeIcon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //fill active gridview.
                FillgvActiveUsers();
            }
        }

        public void FillgvActiveUsers()
        {
            try
            {
                List<cstUserCurrentStationAndDeviceID> lsCurrent = new List<cstUserCurrentStationAndDeviceID>();
                List<cstUserCurrentStationAndDeviceID> lsStation = cGlobal.call.GetlastLoginStationAllUsers();
                foreach (var Stationitem in lsStation)
                {
                    DateTime Dt = Convert.ToDateTime(Stationitem.Datetime);
                    if (Dt.Date == DateTime.Now.Date)
                    {
                        lsCurrent.Add(Stationitem);
                    }
                }
                List<cstShipmentPackedTodayAndAvgTime> lsAvg = cGlobal.call.GetPackingCountCurrentShipmentUserName();
                var packedCountAdded = from t1 in lsCurrent
                        join t2 in lsAvg
                        on t1.UserID equals t2.UserID
                        select new
                        {
                            t1.UserID,
                            t1.UserName,
                            t1.Datetime,
                            t1.StationName,
                            t1.DeviceID,
                            t2.Packed
                        };
                List<cstPackingTbl> lsShipmetn = cGlobal.call.GetPackingTbl();
                var v = (from s in lsShipmetn
                         where s.PackingStatus == 1
                         select new
                         {
                             PackingID = s.PackingID,
                             ShipmentLocation = s.ShipmentLocation,
                            s.UserID,
                             Date = s.StartTime
                         }).OrderByDescending(X => X.Date);

                List<cstHomePageGv> lsHomeGv = new List<cstHomePageGv>();

                foreach (var Pitem in packedCountAdded)
                {
                    cstHomePageGv HomeGv = new cstHomePageGv();
                            HomeGv.UserID = Pitem.UserID;
                            HomeGv.UserName = Pitem.UserName;
                            HomeGv.Packed = Pitem.Packed;
                            HomeGv.PackingID = "NotPacking";
                            HomeGv.StationName = Pitem.StationName;
                            HomeGv.DeviceID = Pitem.DeviceID;
                            HomeGv.Datetime = Pitem.Datetime;
                            lsHomeGv.Add(HomeGv);
                }

                    //foreach (var citem in v)
                    //{
                    //    DateTime pdt =Convert.ToDateTime( Pitem.Datetime);
                    //    if (pdt.Date == citem.Date.Date && Pitem.UserID == citem.UserID)
                    //    {
                    //        if (citem.PackingID != "" || citem.PackingID != null)
                    //        {
                    //            CurrentPackingID = citem.PackingID;
                    //        }

                    //        cstHomePageGv HomeGv = new cstHomePageGv();
                    //        HomeGv.UserID = Pitem.UserID;
                    //        HomeGv.UserName = Pitem.UserName;
                    //        HomeGv.Packed = Pitem.Packed;
                    //        HomeGv.PackingID = CurrentPackingID;
                    //        HomeGv.StationName = Pitem.StationName;
                    //        HomeGv.DeviceID = Pitem.DeviceID;
                    //        HomeGv.Datetime = Pitem.Datetime;
                    //        lsHomeGv.Add(HomeGv);
                    //        break;

                    //    }
                    //}
                    
                    if (lsHomeGv.Count>0)
                    {
                       var GvBindVar = from ls in lsHomeGv
                                        group ls by ls.UserID into Gnewusers
                                        select Gnewusers;

                       gvLatestLogin.DataSource = GvBindVar.ToList();
                       gvLatestLogin.DataBind();
                    }
                    
              


               
            }
            catch (Exception)
            {
            }
        }
    }
}