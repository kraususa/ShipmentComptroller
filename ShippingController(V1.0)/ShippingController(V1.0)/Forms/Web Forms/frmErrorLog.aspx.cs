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
    public partial class frmErrorLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchLog(string prefixText, int count)
        {
            List<string> lsreturn = new List<string>();
            if (prefixText == "")
            {
                prefixText = "SH";
            }
            List<cstErrorLog> lspcking = cGlobal.call.GetErrorLog();
            foreach (var packing in lspcking)
            {
                string ctext = packing.RowID + " | " + packing.ErrorDesc + " | " + packing.ErrorLocation + " | " + packing.ErrorTime + " | " + packing.UserID;

                if (ctext.Contains(prefixText))
                {
                    lsreturn.Add(ctext);
                }
            }
            return lsreturn;
        }
    }
}