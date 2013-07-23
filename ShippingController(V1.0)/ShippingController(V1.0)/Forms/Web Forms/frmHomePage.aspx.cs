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
            }
        }
        public void FillgvPackingShipments()
        {
            try
            {
                List<cstPackingTbl> lsShipmetn =cGlobal.call.GetPackingTbl();
                var v = (from s in lsShipmetn
                        where s.PackingStatus == 1
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