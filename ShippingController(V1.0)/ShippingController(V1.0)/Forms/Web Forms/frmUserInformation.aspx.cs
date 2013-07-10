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

                List<cstHomePageGv> lsHomeinfo = new List<cstHomePageGv>();


                List<cstPackingTbl> lsPackingtbl = cGlobal.call.GetPackingTbl();
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
                    try{HomeGv.CurrentPackingShipmentID = CurrentShp.SingleOrDefault(k => k.UserID == Packingitem.UserID).PackingID;}catch (Exception){}
                    HomeGv.StationName = Packingitem.StationName;
                    HomeGv.DeviceID = Packingitem.DeviceID;
                    HomeGv.Datetime = Packingitem.Datetime;
                    lsHomeinfo.Add(HomeGv);
                }
                gvLatestLogin.DataSource = lsHomeinfo;
                gvLatestLogin.DataBind();
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
                    
                   
                    
              


               
            }
            catch (Exception)
            {
            }
        }
    }
}