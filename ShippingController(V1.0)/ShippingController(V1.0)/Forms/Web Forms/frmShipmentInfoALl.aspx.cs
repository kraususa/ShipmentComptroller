using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects.SqlClient;
using ShippingController_V1._0_.Classes.DisplayEntitys;
using System.Windows.Forms;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmShipmentInfoALl : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Maintain Scroll position in Gridview..
            ScrolBar();

            if (!IsPostBack)
            {
                FillGvShipmentInformation();
            }
        }

       

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            { }
        }

        //Maintain Scroll position in Gridview;
        private void ScrolBar()
        {
            string script;
            script = "window.document.getElementById('" + PosX.ClientID + "').value = "
                      + "window.document.getElementById('" + panel1.ClientID + "').scrollLeft;"
                      + "window.document.getElementById('" + PosY.ClientID + "').value = "
                      + "window.document.getElementById('" + panel1.ClientID + "').scrollTop;";
            this.ClientScript.RegisterOnSubmitStatement(this.GetType(), "SavePanelScroll", script);
            if (IsPostBack)
            {
                script = "window.document.getElementById('" + panel1.ClientID + "').scrollLeft = "
                        + "window.document.getElementById('" + PosX.ClientID + "').value;"
                        + "window.document.getElementById('" +  panel1.ClientID + "').scrollTop = "
                        + "window.document.getElementById('" + PosY.ClientID + "').value;";

                this.ClientScript.RegisterStartupScript(this.GetType(), "SetPanelScroll", script, true);
            }
        }


        public void FillGvShipmentInformation()
        {
            try
            {
                List<cstShipmentInformationAll> lsPacking = new List<cstShipmentInformationAll>();
                List<cstPackingTbl> lsPackingTbl = cGlobal.call.GetPackingTbl();

                foreach (var Pckitem in lsPackingTbl)
                {
                    cstShipmentInformationAll _shipmentInfo = new cstShipmentInformationAll();
                    _shipmentInfo.ShipmentID = Pckitem.PackingID.ToUpper();
                    _shipmentInfo.UserName = cGlobal.call.GetSelcetedUserMaster(Pckitem.UserID).FirstOrDefault().UserFullName.ToString();
                    _shipmentInfo.Location = Pckitem.ShipmentLocation;
                    string status = "Packed";
                    if (Pckitem.PackingStatus ==1)
                    {
                        status = "Partially packed";
                    }
                    _shipmentInfo.PackingStatus = status;
                    TimeSpan Tspent = Pckitem.EndTime - Pckitem.StartTime;
                    _shipmentInfo.StartTime = Pckitem.StartTime.ToShortTimeString();
                    _shipmentInfo.TimeSpent = Tspent.ToString(@"hh\:mm\:ss");
                    lsPacking.Add(_shipmentInfo);

                }

                gvShipmentInformation.DataSource = lsPacking;
                gvShipmentInformation.DataBind();

            }
            catch (Exception)
            {}
        }

        protected void gvShipmentInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception)
            {
            }
        }


        
    }
}