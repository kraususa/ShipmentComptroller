using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Models;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using System.Configuration;
using PackingClassLibrary.Commands.SMcommands.RGA;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAConfig : System.Web.UI.Page
    {
        cmdReasons cRtnreasons = new cmdReasons();
        cmdReasonCategory cCategotyReasons = new cmdReasonCategory();
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
                var resn = from ls in cRtnreasons.ReasonsAll()
                           select new
                           {
                               ls.ReasonID,
                               ls.ReasonPoints,
                               ls.Reason1,
                               Category = GetCategoty(ls.ReasonID)
                           };
                gvReasons.DataSource = resn;
                gvReasons.DataBind();
                
            }
            catch (Exception)
            {
            }
 
        }

        private String GetCategoty(Guid ReasonID)
        {
           String _return = "";
           try
           {
               var Cat = cCategotyReasons.CategotyReasonNameByReasonID(ReasonID);
               foreach (var item in Cat)
               {
                   if(item.CategoryName!="" )
                   _return +=  item.CategoryName+ ",";
               }
           }
           catch (Exception)
           { }
            return _return;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {}
        }

     
    }
}