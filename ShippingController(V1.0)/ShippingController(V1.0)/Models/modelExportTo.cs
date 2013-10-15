using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.SMEntitys;
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
        public static void Excel(List<string> lsPCKROWID, string filename)
        {
            List<string> Boxnumbers = new List<string>();

            foreach (var item in lsPCKROWID)
            {
                List<cstBoxPackage> lsBoxInfo = Obj.call.GetBoxPackageByPackingID(Obj.call.GetPackageIDFromROWID(item));
                foreach (cstBoxPackage boxitem in lsBoxInfo)
                {
                    Boxnumbers.Add(boxitem.BOXNUM);
                }
            }
            

            //Find Box information from the Box Numbers
            List<BoxManifist> _lsBoxManifist = new List<BoxManifist>();
            foreach (string item in Boxnumbers)
            {

                //Box Information
                cstBoxPackage _boxInfo = Obj.call.GetBoxPackageByBoxNumber(item);

                //Package Information
                cstPackageTbl packing = Obj.call.GetPackingList(_boxInfo.PackingID, true);

                BoxManifist manifist = new BoxManifist();
                manifist.BoxNumber = _boxInfo.BOXNUM;
                manifist.PackingNumber = packing.PCKROWID;
                manifist.ShippingNumber = packing.ShippingNum;
                try
                {
                    manifist.TrackingNumber = Obj.call.GetTrackingTbl(item).FirstOrDefault().TrackingNum.ToString();
                }
                catch (Exception)
                {
                    manifist.TrackingNumber = "N/A";
                }
                
                manifist.Location = packing.ShipmentLocation;
                manifist.Weight = _boxInfo.BoxWeight.ToString();
                manifist.Width = _boxInfo.BoxWidth.ToString();
                manifist.Lenght = _boxInfo.BoxHeight.ToString();
                manifist.Height = _boxInfo.BoxHeight.ToString();
                manifist.UserName = Obj.call.GetSelcetedUserMaster(packing.UserID).FirstOrDefault().UserFullName;
                manifist.PackedDate = packing.EndTime.ToString("MMM dd, yyyy hh:mm:ss tt");

                _lsBoxManifist.Add(manifist);

            }



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
                    dg.DataSource = _lsBoxManifist;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }
    }


    public class BoxManifist
    {
        public String BoxNumber { get; set; }
        public String PackingNumber { get; set; }
        public String ShippingNumber { get; set; }
        public String TrackingNumber { get; set; }
        public String Width { get; set; }
        public String Height { get; set; }
        public String Lenght { get; set; }
        public String Weight { get; set; }
        public String UserName { get; set; }
        public String Location { get; set; }
        public String PackedDate { get; set; }
    }

}