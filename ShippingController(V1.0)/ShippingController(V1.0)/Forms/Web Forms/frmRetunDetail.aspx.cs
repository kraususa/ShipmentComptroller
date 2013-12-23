using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PackingClassLibrary;

using ShippingController_V1._0_.Models;
using System.IO;



namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRetunDetail : System.Web.UI.Page
    {
        ReportController re = new ReportController();
        List<Return> _lsreturn = new List<Return>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _lsreturn = re.ReturnAll();
                FillReturnMasterGv(Obj.Rcall.ReturnAll());
                FillReturnDetails(Obj.Rcall.ReturnDetailAll());
            }
        }

        #region Functions

        public void FillReturnMasterGv(List<Return> lsReturn)
        {
            _lsreturn = lsReturn;
            gvReturnInfo.DataSource = lsReturn;
            gvReturnInfo.DataBind();
        }

        public void FillReturnDetails(List<ReturnDetail> lsReturnDetails)
        {
            try
            {
                var ReaturnDetails = from Rs in lsReturnDetails
                         select new
                         {
                            Rs.RGADROWID,
                            Rs.SKUNumber,
                            Rs.ProductName,
                            Rs.DeliveredQty,
                            Rs.ReturnQty,
                            ReturnReasons = Obj.Rcall.ReasonsListByReturnDetails(Rs.ReturnDetailID)
                         };
                gvReturnDetails.DataSource = ReaturnDetails.ToList();
                gvReturnDetails.DataBind();
            }
            catch (Exception)
            {}
        }

        /// <summary>
        /// Filter The 
        /// </summary>
        /// <param name="Paramerer"></param>
        /// <param name="lsReturn"></param>
        /// <param name="FilterCount"></param>
        /// <returns></returns>
        public List<Return> Filter(String Paramerer, List<Return> lsReturn, int FilterCount)
        {
            List<Return> _lsReturn = lsReturn;
            try
            {

            }
            catch (Exception)
            {}
            return _lsReturn;
        }

       

        /// <summary>
        /// Text Of link Button
        /// </summary>
        /// <param name="LinkButtonID">
        /// String Link Button ID
        /// </param>
        /// <param name="GridViewName">
        /// Gridview Object link button belongs to
        /// </param>
        /// <returns>
        /// String Text Of Link Button 
        /// </returns>
        private String _linkButtonText(String LinkButtonID, GridView GridViewName)
        {
            String _return = "";

            try
            {
                LinkButton lnk = (LinkButton)GridViewName.SelectedRow.FindControl(LinkButtonID);
                _return = lnk.Text;
            }
            catch (Exception)
            { }
            return _return;
        }
        #endregion



        protected void btnExport_Click(object sender, EventArgs e)
        {

            try
            {
                List<string> lsRGAROWID = new List<string>();

                foreach (GridViewRow row in gvReturnInfo.Rows)
                {
                    LinkButton lnk = (LinkButton)row.FindControl("RMANumber");
                    lsRGAROWID.Add(lnk.Text);
                }

                modelExportTo.Excel(lsRGAROWID, "RGA Details");
            }
            catch (Exception)
            { }
        }


        protected void txtRMANumber_TextChanged(object sender, EventArgs e)
        {
            var RMA = from returnALL in _lsreturn
                      where returnALL.RMANumber == txtRMANumber.Text
                      select returnALL;

            FillReturnMasterGv(RMA.ToList());
        }

        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            var ShipID = from returnAll in _lsreturn
                         where returnAll.ShipmentNumber == txtShipmentID.Text
                         select returnAll;

            FillReturnMasterGv(ShipID.ToList());
        }

        protected void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            var OrderNum = from all in _lsreturn
                           where all.OrderNumber == txtOrderNumber.Text
                           select all;

            FillReturnMasterGv(OrderNum.ToList());
        }

        protected void txtPoNum_TextChanged(object sender, EventArgs e)
        {
            var PONum = from all in _lsreturn
                           where all.PONumber == txtPoNum.Text
                           select all;

            FillReturnMasterGv(PONum.ToList());
        }


        protected void gvReturnInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
             
                 string ReturnROWID = _linkButtonText("lbtnRGANumberID", gvReturnInfo);
                 FillReturnDetails(Obj.Rcall.ReturnDetailByRGAROWID(ReturnROWID));
            }
            catch (Exception)
            {}
        }

        protected void gvReturnDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ReturnROWID = _linkButtonText("lbtnRmaDetailNumberID", gvReturnDetails);
                List<string> lsImages = Obj.Rcall.ReturnImagesByReturnDetailsID(Obj.Rcall.ReturnDetailByRGADROWID(ReturnROWID)[0].ReturnDetailID);

                foreach (var _imageitem in lsImages)
                {
                    string ImageUrl = "~/Images/";
                    string ImagePath = Server.MapPath(ImageUrl);
                   // string filename = Path.GetFileName(_imageitem);
                   String Full= Path.GetFullPath(_imageitem);

                   Uri file = new Uri(_imageitem);
                   // Must end in a slash to indicate folder
                   Uri folder = new Uri(@"C:\GITHUB\ShipmentComptroller\ShippingController(V1.0)\ShippingController(V1.0)\images\");
                   string relativePath =
                   Uri.UnescapeDataString(
                       folder.MakeRelativeUri(file)
                           .ToString()
                           .Replace('/', Path.DirectorySeparatorChar)
                       );
                   int lastSlash = _imageitem.LastIndexOf("\\");
                   string trailingPath = _imageitem.Substring(lastSlash + 1);
                   string fullPath = Server.MapPath("~/Images/") + "\\" + trailingPath;
                   String s = Path.Combine(Server.MapPath(" ~/Images/"), trailingPath);
                   FileUpload1.SaveAs(s);
                    image1.ImageUrl = relativePath;

                }


            }
            catch (Exception)
            {}
        }

        string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

    }
}