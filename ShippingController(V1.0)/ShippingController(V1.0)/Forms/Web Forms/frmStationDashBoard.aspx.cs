using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Models;
using ShippingController_V1._0_.Views;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmStationDashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StaionInfo();
        }


        private int _getUserLogErrors(String Username)
        {
            int Errorcount=0;
            try
            {
                Guid UserId = Obj.call.GetUserInfoList().SingleOrDefault(i => i.UserFullName == Username).UserID;
                List<cstAutditLog> lsAudit = Obj.call.GetUserLogAll(UserId, DateTime.UtcNow);
                foreach (cstAutditLog _audit in lsAudit)
                {
                    if (_audit.ActionType.Contains("_00"))
                    {
                        Errorcount++;
                    }
                }

            }
            catch (Exception)
            {}
            return Errorcount;
        }

        public void StaionInfo()
        {
            List<cstStationToatlPacked> _lsTotalPacekedPerStation = Obj.Rcall.GetStationTotalPaked(DateTime.UtcNow);
            List<cstPackageTbl> lsShipmetn = Obj.call.GetPackingTbl();
            List<cstUserCurrentStationAndDeviceID> lsCurrent = new List<cstUserCurrentStationAndDeviceID>();
            List<cstUserCurrentStationAndDeviceID> lsStation =Obj.call.GetlastLoginStationAllUsers();
            foreach (var Stationitem in lsStation)
            {
                DateTime Dt = Convert.ToDateTime(Stationitem.Datetime);
                if (Dt.Date == DateTime.UtcNow.Date)
                {
                    lsCurrent.Add(Stationitem);
                }
            }


            try
            {
                var UnderPacking = (from s in lsShipmetn
                                    where s.StartTime.Date == DateTime.UtcNow.Date
                                    && s.PackingStatus == 1
                                    select new
                                    {
                                        PackingID = s.ShippingNum,
                                        ShipmentLocation = s.ShipmentLocation,
                                        StationID = s.StationID,
                                        UserName = Obj.call.GetSelcetedUserMaster(s.UserID).FirstOrDefault().UserFullName,
                                        userID = s.UserID,
                                        Date = s.StartTime,
                                        s.PackingStatus
                                    }).OrderByDescending(X => X.Date);
                //Left Outer Join with Null values
                var StationInfo = from lsTotal in _lsTotalPacekedPerStation
                                  join lsuPacking in UnderPacking
                                  on lsTotal.StationID equals lsuPacking.StationID
                                  into pp
                                  from lsuPacking in pp.DefaultIfEmpty()
                                  select new
                                  {
                                      lsTotal.StationID,
                                      StationName = lsTotal.StationName,
                                      TotalPacked = lsTotal.TotalPacked,
                                      UserName = lsuPacking == null ? "No user Logged" : lsuPacking.UserName,
                                      userID = lsuPacking == null ? Guid.Empty : lsuPacking.userID,
                                      shipmentNumber = lsuPacking == null ? "Not Packing" : lsuPacking.PackingID
                                  };

                var SFinalJoin = from sf in StationInfo
                        join ct in lsCurrent
                        on sf.StationName equals ct.StationName
                        select new
                        {
                            stationName = sf.StationName,
                            TotalPacked = sf.TotalPacked,
                            UserName = ct.UserName,
                            userid = ct.UserID,
                            shippingnum = sf.shipmentNumber
                        };

                
                List<cstDashBoardStion> lsDashBoard = new List<cstDashBoardStion>();
                foreach (var infoItem in SFinalJoin)
                {
                    cstDashBoardStion _Dstation = new cstDashBoardStion();
                    _Dstation.StationName = infoItem.stationName;
                    _Dstation.TotalPacked = infoItem.TotalPacked;
                    _Dstation.PackerName = infoItem.UserName;
                    _Dstation.ErrorCaught = _getUserLogErrors(infoItem.UserName);
                    _Dstation.ShipmentNumber = infoItem.shippingnum;
                    _Dstation.packagePerhr = AvgPackingTimerPerUser(infoItem.userid);
                    lsDashBoard.Add(_Dstation);
                }


                foreach (cstDashBoardStion Dab in lsDashBoard)
                {
                    MainDiv.Controls.Add(SetTable(Dab));
                }
            }
            catch (Exception)
            { }
            
           
        }

        public HtmlTable SetTable(cstDashBoardStion cStation)
        {
            HtmlTable StationTable = new HtmlTable();
            StationTable.BgColor = "White";
            StationTable.Border = 2;
            StationTable.BorderColor = "Gray";
            StationTable.Style.Add("float", "left");
            StationTable.Style.Add("margin-right", "10px");
            StationTable.Style.Add("margin", "5px");
            StationTable.Style.Add("width", "47%");
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableRow trow = new HtmlTableRow();
            HtmlTableCell tcell = new HtmlTableCell();
            tcell.ColSpan = 2;
            tcell.InnerHtml = " <table style=\"width: 100%;\"><tr><td style=\"text-align: center; font-size: 40px; color: #ff6a00; background-color:#1e1d1d\">" + cStation.StationName + "</td></tr></table>";
            trow.Cells.Add(tcell);
            HtmlTableCell cell;
            for (int i = 0; i < 2; i++)
            {
                cell = new HtmlTableCell();
                if (i == 0)
                {
                    cell.InnerHtml = "<table style=\"width:100%\"><tr><td style=\"text-align:center; font-size:60px; color:darkgreen;\">" + cStation.TotalPacked + "</td></tr><tr><td style=\"text-align:center; font-size:40px; color:black\"> Packed</td></tr></table>";
                }
                if (i == 1)
                {
                    cell.InnerHtml = "<table style=\"width: 100%;\"><tr><td style=\"font-size:20px; color:black; text-align:right\">Packer :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.PackerName + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Error Caught :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.ErrorCaught + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Avg Shipment Packing Time :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.packagePerhr + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Active Shipment :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.ShipmentNumber + "</td></tr></table>";
                }
                row.Cells.Add(cell);
            }
            StationTable.Rows.Add(trow);
            StationTable.Rows.Add(row);
            return StationTable;
        }


        public String AvgPackingTimerPerUser(Guid UserID)
        {
            String _return = "N/A";
            // Code for average time
            TimeSpan Tm = TimeSpan.FromSeconds(Obj.call.GetAverageTime(UserID)[0].Value);
            string min = Tm.Minutes.ToString();
            string sec = Tm.Seconds.ToString();
            sec = sec.TrimStart(new char[] { '0' }) + "";
            if (sec != "")
            {
                sec = "" + sec + "sec";
            }
            min = min.TrimStart(new char[] { '0' }) + "";
            if (min != "")
            {
                min = min + "min:";
            }
            if( (min + sec )!= "")
                _return = min + sec;


            return _return;
        }
    }
}