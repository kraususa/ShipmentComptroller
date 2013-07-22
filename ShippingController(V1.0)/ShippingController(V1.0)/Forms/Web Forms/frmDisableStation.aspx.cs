using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using ShippingController_V1._0_.Classes;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmDisableStation : System.Web.UI.Page
    {
        smController call = new smController();

        protected void Page_Load(object sender, EventArgs e)
        {
            FillDgvActiveStation();
        }

        /// <summary>
        /// Fill the dataGrid View
        /// </summary>
        public void FillDgvActiveStation()
        {
            List<cDeactivateStation> lsStations = new List<cDeactivateStation>();
            List<cstStationMasterTbl> lsStationMaster = call.GetStationMaster();
            foreach (var Stationitem in lsStationMaster)
            {
                cDeactivateStation S = new cDeactivateStation();
                S.StationName = Stationitem.StationName;
                if (Stationitem.StationAlive == 0)
                {
                    S.ActiveStatus = "InActive";
                }
                else
                {
                    S.ActiveStatus = "Active";
                }
                S.DeviceID = Stationitem.DeviceNumber;
                S.RequestedUserName = call.GetSelcetedUserMaster(Stationitem.RequestedUserID).FirstOrDefault().UserFullName;
                S.RequestedDate = Stationitem.RegistrationDate.ToString("MMM dd, yyyy hh:mm tt");
                lsStations.Add(S);
            }
            gvStations.DataSource = lsStations;
            gvStations.DataBind();

            //Color the Row
            foreach (GridViewRow row in gvStations.Rows)
            {
                if (row.Cells[2].Text.ToString() == "Active")
                {
                    row.Cells[2].BackColor = System.Drawing.Color.FromName("#009933");
                }
                else
                {
                    row.Cells[2].BackColor = System.Drawing.Color.FromName("#D05502");
                }
            }
        }

        protected void gvStations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvStations.SelectedRow;
                List<cstStationMasterTbl> lsStationMaster = call.GetStationMaster(row.Cells[3].Text.ToString());
                cstStationMasterTbl Station = lsStationMaster[0];
                Station.StationAlive = 1;
                if (row.Cells[2].Text.ToString()=="Active")
                {
                    Station.StationAlive = 0;
                }
                List<cstStationMasterTbl> _lsStation = new List<cstStationMasterTbl>();
                _lsStation.Add(Station);

                call.SaveStationMaster(_lsStation, row.Cells[3].Text.ToString());

                //refresh Grid View
                FillDgvActiveStation();
            }
            catch (Exception)
            {}
        }

        protected void gvStations_Load(object sender, EventArgs e)
        {
           
        }
    }
}