using PackingClassLibrary.CustomEntity;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmUserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // _fillUserInformationGridView();
            }
        }

        private void _fillUserInformationGridView()
        {
            try
            {
                List<cstUserMasterTbl> lsUserMaseterAll = Obj.call.GetUserInfoList();
                gvUserInformation.DataSource = lsUserMaseterAll;
                gvUserInformation.DataBind();
            }
            catch (Exception)
            { }
        }
        
    }
}