using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Classes.DisplayEntitys
{
    public class cstErrorLog
    {
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorLocation { get; set; }
        public string UserName { get; set; }
        public DateTime ErrorDate { get; set; }
    }
}