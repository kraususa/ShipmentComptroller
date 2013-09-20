using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Models
{
    public class modelExportTo
    {
         /// <summary>
        /// Export all data to excel file.
        /// </summary>
        /// <param name="ds">Dataset table to be export</param>
        /// <param name="filename"> File name of excel.</param>
        public static void Excel<T>(T ds, string filename)
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel
            response.ContentType = "application/vnd.MS-Excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + ".xls\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
    
    }
}