using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using ShippingController_V1._0_.Classes.DisplayEntitys;
using System;
using System.Collections.Generic;
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
            //HtmlTextArea txt = new HtmlTextArea();
            //txt.InnerHtml="<div  class=\"TitleStrip\"><h2>Station _Name</h2></div>";
            //MainDiv.Controls.Add(txt);
            //MainDiv.Controls.Add(SetTable());
           // StaionInfo();
        }


        public void StaionInfo()
        {
            List<cstStationToatlPacked> _lsTotalPacekedPerStation = cGlobal.Rcall.GetStationTotalPaked();
            List<cstUserCurrentStationAndDeviceID> lsStation = cGlobal.call.GetlastLoginStationAllUsers();

           

                                  


            foreach (cstStationToatlPacked _stationItem in _lsTotalPacekedPerStation)
            {
                cstDashBoardStion _Dstation = new cstDashBoardStion();

                _Dstation.StationName = _stationItem.StationName;
                _Dstation.TotalPacked =_stationItem.TotalPacked;
            }
        }

        public HtmlTable SetTable( cstDashBoardStion cStation)
        {
            HtmlTable StationTable = new HtmlTable();
            StationTable.BgColor = "Transparent";
            StationTable.Border = 2;
            StationTable.BorderColor = "Gray";
            StationTable.Height = "200px";
            StationTable.Width = "45%";
            HtmlTableRow row = new HtmlTableRow();
            //HtmlTableRow Trow = new HtmlTableRow();
            //HtmlTableCell Tcell = new HtmlTableCell();
            //Tcell.ColSpan = 2;
            //Tcell.InnerHtml = " <h2 style=\"font-family: arial; font-size: x-large; color: #CC6600; font-weight: bold\">Station _Name</h2>";
            //Trow.Cells.Add(Tcell);
            HtmlTableCell cell;
            for (int i = 0; i < 2; i++)
            {
               
                cell = new HtmlTableCell();
                if (i == 0)
                {
                    cell.InnerHtml = "<table style=\"width:100%\"><tr><td style=\"text-align:center; font-size:60px; color:darkgreen;\">"+cStation.TotalPacked+"</td></tr><tr><td style=\"text-align:center; font-size:40px; color:black\"> Packed</td></tr></table>";
                }
                if (i==1)
                {
                    cell.InnerHtml = "<table style=\"width: 100%;\"><tr><td style=\"font-size:20px; color:black; text-align:right\">Packer :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">"+cStation.PackerName+"Smith John</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Error Caught :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">"+cStation.ErrorCaught+"</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Packages / Hr :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">"+cStation.packagePerhr+"</td></tr><tr><td style=\"font-size:20px; color:black; text-align:right\">Active Shipment :</td><td style=\"font-size:20px; color:darkblue; text-align:left\">"+cStation.ShipmentNumber+"</td></tr></table>";
                }
                row.Cells.Add(cell);
            }
            //StationTable.Rows.Add(Trow);
            StationTable.Rows.Add(row);
            return StationTable;
        }

    }
}