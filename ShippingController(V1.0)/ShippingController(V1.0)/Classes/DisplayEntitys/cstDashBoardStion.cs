using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Classes.DisplayEntitys
{
    public class cstDashBoardStion
    {
        public String StationName { get; set; }
        public int ErrorCaught{ get; set; }
        public int  TotalPacked { get; set; }
        public int packagePerhr { get; set; }
        public String ShipmentNumber { get; set; }
        public String PackerName { get; set; }
    }
}