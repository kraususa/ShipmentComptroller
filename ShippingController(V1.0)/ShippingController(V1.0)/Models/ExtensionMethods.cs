using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Models
{
    public static class ExtensionMethods
    {
        private static string applicationPath;

        static ExtensionMethods()
        {
            ExtensionMethods.applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        }

        public static string resolveVirtual(this string physicalPath)
        {
            string url = physicalPath.Substring(ExtensionMethods.applicationPath.Length).Replace('\\', '/').Insert(0, "~/");
            return (url);
        }
    }
}