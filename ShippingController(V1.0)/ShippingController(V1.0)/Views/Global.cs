using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using System.Data;

namespace ShippingController_V1._0_.Views
{
    public static class Global
    {

        public static Return ReteunGlobal = new Return();

        public static List<StatusAndPoints> listofstatusAndPoint = new List<StatusAndPoints>();

        public static string[] arr;

        public static List<ReturnDetail> lsReturnDetail = new List<ReturnDetail>();

        public static string SKU_Staus;

        public static int TotalPoints;

        public static int IsScanned;

        public static int IsManually;

        public static int NewItemQty;

        public static int _SKU_Qty_Seq;

        public static List<SKUReason> lsSKUReasons = new List<SKUReason>();

        public static DataTable DT1 = new DataTable();

        public static List<SkuReasonIDSequence> _lsReasonSKU = new List<SkuReasonIDSequence>();

    }
}