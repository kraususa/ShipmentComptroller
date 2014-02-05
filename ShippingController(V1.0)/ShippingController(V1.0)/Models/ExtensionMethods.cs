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

        public static string ImageToVirualPath(this string imageName)
        {
            return VirtualPathUtility.ToAbsolute("~/images/" + imageName);
        }
        public static List<Guid> GetGuid(this String TextFromTextBox)
        {
            List<Guid> _lsReturn = new List<Guid>();
            try
            {
                String InString = TextFromTextBox;
                String[] SplitedString = InString.Split(new char[] { '#' });
                foreach (String sitem in SplitedString)
                {
                    Guid NGuid = new Guid();
                    Guid.TryParse(sitem, out NGuid);
                    if (NGuid != Guid.Empty) _lsReturn.Add(NGuid);
                }
            }
            catch (Exception)
            { }
            return _lsReturn;
        }
    }
}