using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using ShippingController_V1._0_.Classes.DisplayEntitys;
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

        public void StaionInfo()
        {
            List<cstStationToatlPacked> _lsTotalPacekedPerStation = cGlobal.Rcall.GetStationTotalPaked();
            List<cstUserCurrentStationAndDeviceID> lsStation = cGlobal.call.GetlastLoginStationAllUsers();
            List<cstPackageTbl> lsShipmetn = cGlobal.call.GetPackingTbl();
            var UnderPacking = (from s in lsShipmetn
                                where s.PackingStatus == 1
                                && s.StartTime.Date == DateTime.Now.Date && s.StartTime.Month == DateTime.Now.Month && s.StartTime.Year == DateTime.Now.Year
                                select new
                                {
                                    PackingID = s.ShippingNum,
                                    ShipmentLocation = s.ShipmentLocation,
                                    UserName = cGlobal.call.GetSelcetedUserMaster(s.UserID).FirstOrDefault().UserFullName,
                                    Date = s.StartTime
                                }).OrderByDescending(X => X.Date);

            var StationInfo = from lsTotal in _lsTotalPacekedPerStation
                              join lsStat in lsStation on lsTotal.StationName equals lsStat.StationName
                              join lsuPacking in UnderPacking on lsStat.UserName equals lsuPacking.UserName
                              select new
                              {
                                  StationName = lsTotal.StationName,
                                  TotalPacked = lsTotal.TotalPacked,
                                  UserName = lsStat.UserName,
                                  shipmentNumber = lsuPacking.PackingID
                              };
            List<cstDashBoardStion> lsDashBoard = new List<cstDashBoardStion>();


            foreach (var infoItem in StationInfo)
            {
                cstDashBoardStion _Dstation = new cstDashBoardStion();
                _Dstation.StationName = infoItem.StationName;
                _Dstation.TotalPacked = infoItem.TotalPacked;
                _Dstation.PackerName = infoItem.UserName;
                _Dstation.ShipmentNumber = infoItem.shipmentNumber;
                _Dstation.ErrorCaught = 10;
                _Dstation.packagePerhr = 100;
                lsDashBoard.Add(_Dstation);
            }

            foreach (cstDashBoardStion Dab in lsDashBoard)
            {
                MainDiv.Controls.Add(SetTable(Dab));
            }
        }

        public HtmlTable SetTable(cstDashBoardStion cStation)
        {
            HtmlTable StationTable = new HtmlTable();
            StationTable.BgColor = "Transparent";
            StationTable.Border = 2;
            StationTable.BorderColor = "Gray";
            StationTable.Width = "40%";
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
                    cell.InnerHtml = "<table style=\"width: 100%;\"><tr><td style=\"font-size:20px; color:black; text-align:right\">Packer :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.PackerName + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Error Caught :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.ErrorCaught + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Packages / Hr :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.packagePerhr + "</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Active Shipment :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">" + cStation.ShipmentNumber + "</td></tr></table>";
                }
                row.Cells.Add(cell);
            }
            StationTable.Rows.Add(trow);
            StationTable.Rows.Add(row);
            return StationTable;
        }
    }
}