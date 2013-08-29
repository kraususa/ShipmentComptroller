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
    public partial class AutoCompleteService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> SearchpackingID(string prefixText, int count)
        {
            List<string> lsreturn = new List<string>();
            if (prefixText == "")
            {
                prefixText = "SH";
            }
            List<cstPackageTbl> lspcking = Obj.call.GetPackingTbl();
            foreach (var packing in lspcking)
            {

                if (packing.ShippingNum.Contains(prefixText))
                {
                    lsreturn.Add(packing.ShippingNum.ToString().ToUpper());
                }
            }
            return lsreturn;
        }
    }
}