using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Commands.SMcommands.RGA;
using ShippingController_V1._0_.Views;
using PackingClassLibrary;
using PackingClassLibrary.Commands;
using PackingClassLibrary.CustomEntity;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAEnter : System.Web.UI.Page
    {

        Models.modelReturn _newRMA = new Models.modelReturn();
        smController call = new smController();
        List<cstUserMasterTbl> lsUserInfo = new List<cstUserMasterTbl>();

        cstHomePageGv _info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Reason> lsReturn = _newRMA.GetReasons();


                Reason re = new Reason();
                re.ReasonID = Guid.NewGuid();
                re.Reason1 = "--Select--";

                lsReturn.Insert(0, re);

               ddlotherreasons.DataTextField = "Reason1";
               ddlotherreasons.DataValueField = "ReasonID";
               ddlotherreasons.DataSource = lsReturn;
               ddlotherreasons.DataBind();

               //string user = Session["UName"].ToString();

               
            }
        }
        private String ReturnReasons()
        {
            String _ReturnReason = "";

            if (chkitemdamaged.Checked == true) _ReturnReason = _ReturnReason + chkitemdamaged.Text;

            if (chkitemdifferent.Checked == true) _ReturnReason = _ReturnReason + chkitemdifferent.Text;

            if (chkduplicate.Checked == true) _ReturnReason = _ReturnReason + chkduplicate.Text;

            if (chkitemordered.Checked == true) _ReturnReason = _ReturnReason + chkitemordered.Text;

            if (chknotsatisfied.Checked == true) _ReturnReason = _ReturnReason + chknotsatisfied.Text;

            if (chkwrongitem.Checked == true) _ReturnReason = _ReturnReason + chkwrongitem.Text;

            _ReturnReason += txtotherreasons.Text;

            return _ReturnReason;

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Byte Status = Convert.ToByte(ddlstatus.SelectedValue);
            Byte Decision = Convert.ToByte(ddldecision.SelectedValue);

            List<Return> _lsreturn = new List<Return>();
            Return ret = new Return();
            ret.RMANumber = txtrmanumber.Text;
            ret.VendoeName = txtvendername.Text;
            ret.VendorNumber = txtvendernumber.Text;
            //eastern = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(txtRMAReqDate.SelectedDate.Value, "Eastern Standard Time");
            ret.ReturnDate = DateTime.UtcNow;
            ret.PONumber = txtponumber.Text;
            ret.CustomerName1 = txtcustomername.Text;
            ret.Address1 = txtcustomeraddress.Text;
            ret.City = txtcity.Text;
            ret.Country = txtcountry.Text;
            ret.ZipCode = txtzipcode.Text;
            ret.State = txtstate.Text;

            _lsreturn.Add(ret);

            lsUserInfo = call.GetSelcetedUserMaster(Session["UName"].ToString());

            Guid ReturnID = _newRMA.SetReturnTbl(_lsreturn, ReturnReasons(), Status, Decision, lsUserInfo[0].UserID);
        }

        protected void ddlotherreasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlotherreasons.SelectedIndex == 0)
            {
                txtotherreasons.Text = "";
            }
            else
            {
                txtotherreasons.Text = ddlotherreasons.SelectedItem.Text;
            }
        }
    }
}