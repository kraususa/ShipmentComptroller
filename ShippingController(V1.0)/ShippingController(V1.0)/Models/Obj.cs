using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PackingClassLibrary;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace ShippingController_V1._0_.Models
{
    public class Obj
    {
        public static smController call = new smController();
        public static ReportController Rcall = new ReportController();


      public static List<Return> _lsreturn = new List<Return>();
      public static List<ReturnDetail> _lsReturnDetails = new List<ReturnDetail>();
    }
}