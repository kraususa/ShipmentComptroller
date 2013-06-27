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
                    S.ActiveStatus = "Deative";
                }
                else
                {
                    S.ActiveStatus = "Active";
                }
                S.DeviceID = Stationitem.DeviceID;
                S.RequestedUserName = call.GetSelcetedUserMaster(Stationitem.UserID).FirstOrDefault().UserFullName;
                S.RequestedDate = Stationitem.RegistrationDate.ToString("MMM dd, yyyy hh:mm tt");
                lsStations.Add(S);
            }

            gvStations.DataSource = lsStations;
            gvStations.DataBind();

        }
    }
}