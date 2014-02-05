using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Models;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using System.Configuration;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAConfig : System.Web.UI.Page
    {
        modelReturn _mReturn = new modelReturn();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtImageServer.Text = System.Configuration.ConfigurationManager.AppSettings["ImageServerPath"].ToString();
                FillReturnGrid();
            }
        }

        private void FillReturnGrid()
        {
            try
            {
                gvReasons.DataSource = _mReturn.GetReasons();
                gvReasons.DataBind();
            }
            catch (Exception)
            {
            }
 
        }

        protected void btnUpdateImageServer_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration objConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                objConfig.AppSettings.Settings.Remove("ImageServerPath");
                objConfig.AppSettings.Settings.Add("ImageServerPath",txtImageServer.Text);
                objConfig.Save();
            }
            catch (Exception)
            {}
        }
    }
}