using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary;
using System.IO;
using System.Data;


namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRetunDetail : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillReturnMasterGv(Obj.Rcall.ReturnAll());
             
            }
        }

        #region Functions

        public void FillReturnMasterGv(List<Return> lsReturn)
        {
            Obj._lsreturn = lsReturn;
            gvReturnInfo.DataSource = lsReturn;
            gvReturnInfo.DataBind();


            var ReturnDetais = from rm in lsReturn
                               join Rd in Obj.Rcall.ReturnDetailAll()
                               on rm.ReturnID equals Rd.ReturnID
                               select new
                               {
                                   Rd.ReturnDetailID,
                                   Rd.ReturnID,
                                   Rd.SKUNumber,
                                   Rd.ProductName,
                                   Rd.TCLCOD_0,
                                   Rd.DeliveredQty,
                                   Rd.ExpectedQty,
                                   Rd.ReturnQty,
                                   Rd.ProductStatus,
                                   Rd.CreatedBy,
                                   Rd.UpdatedBy,
                                   Rd.CreatedDate,
                                   Rd.UpadatedDate,
                                   Rd.RGADROWID
                               };
            List<ReturnDetail> lsReD = new List<ReturnDetail>();
            foreach (var ReturnDetails in ReturnDetais)
            {
                ReturnDetail Rd1 = new ReturnDetail();
                Rd1.ReturnDetailID = ReturnDetails.ReturnDetailID;
                Rd1.ReturnID = ReturnDetails.ReturnID;
                Rd1.SKUNumber = ReturnDetails.SKUNumber;
                Rd1.ProductName = ReturnDetails.ProductName;
                Rd1.TCLCOD_0 = ReturnDetails.TCLCOD_0;
                Rd1.DeliveredQty = (int)ReturnDetails.DeliveredQty;
                Rd1.ExpectedQty = (int)ReturnDetails.ExpectedQty;
                Rd1.ReturnQty = (int)ReturnDetails.ReturnQty;
                Rd1.ProductStatus = (int)ReturnDetails.ProductStatus;
                Rd1.CreatedBy = (Guid)ReturnDetails.CreatedBy;
                Rd1.UpdatedBy = (Guid)ReturnDetails.UpdatedBy;
                Rd1.CreatedDate = (DateTime)ReturnDetails.CreatedDate;
                Rd1.UpadatedDate = (DateTime)ReturnDetails.UpadatedDate;
                Rd1.RGADROWID = ReturnDetails.RGADROWID;
                lsReD.Add(Rd1);
            }
            FillReturnDetails(lsReD);

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

        public void ResetAll()
        {
            txtCustomerName.Text = "";
            txtOrderNumber.Text = "";
            txtPoNum.Text = "";
            txtRMANumber.Text = "";
            txtShipmentID.Text = "";
            txtVendorName.Text = "";
            txtVendorNumber.Text = "";
            FillReturnMasterGv(Obj.Rcall.ReturnAll());
            dtpFromDate.Text = "";
            dtpToDate.Text = "";

        }


        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                modelExportTo.RGAExcel(Obj._lsreturn);
            }
            catch (Exception)
            { }
        }

        protected void txtRMANumber_TextChanged(object sender, EventArgs e)
        {
            if (txtRMANumber.Text.Trim()!="")
            {
                var RMA = from returnALL in Obj._lsreturn
                          where returnALL.RMANumber == txtRMANumber.Text
                          select returnALL;

                FillReturnMasterGv(RMA.ToList()); 
            }
        }

        protected void txtShipmentID_TextChanged(object sender, EventArgs e)
        {
            if (txtShipmentID.Text.Trim() != "")
            {
                var ShipID = from returnAll in Obj._lsreturn
                             where returnAll.ShipmentNumber == txtShipmentID.Text
                             select returnAll;

                FillReturnMasterGv(ShipID.ToList());
            }
        }

        protected void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtOrderNumber.Text.Trim() != "")
            {
                var OrderNum = from all in Obj._lsreturn
                               where all.OrderNumber == txtOrderNumber.Text
                               select all;

                FillReturnMasterGv(OrderNum.ToList());
            }
        }

        protected void txtPoNum_TextChanged(object sender, EventArgs e)
        {
            if (txtPoNum.Text.Trim()!="")
            {
                var PONum = from all in Obj._lsreturn
                            where all.PONumber == txtPoNum.Text
                            select all;

                FillReturnMasterGv(PONum.ToList());
            } 
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

        protected void btnRefresh2_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        protected void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() != "")
            {
                List<Return> LsCustomers = new List<Return>();
                foreach (var item in Obj._lsreturn)
                {
                    if (item.CustomerName1.Contains(txtCustomerName.Text))
                    {
                        LsCustomers.Add(item);
                    }
                }
                FillReturnMasterGv(LsCustomers.ToList());
            } 
        }

        protected void txtVendorName_TextChanged(object sender, EventArgs e)
        {
            if (txtVendorName.Text.Trim() != "")
            {
                List<Return> LsVendor = new List<Return>();
                foreach (var item in Obj._lsreturn)
                {
                    if (item.VendoeName.Contains(txtVendorName.Text))
                    {
                        LsVendor.Add(item);
                    }
                }
                FillReturnMasterGv(LsVendor.ToList());
            } 
        }

        protected void dtpToDate_TextChanged(object sender, EventArgs e)
        {
            if (dtpToDate.Text.Trim() != "" && dtpFromDate.Text.Trim() != "")
            {
                DateTime Fdate;
                DateTime TDate;
                DateTime.TryParse(dtpFromDate.Text, out Fdate);
                DateTime.TryParse(dtpToDate.Text, out TDate);

                var fromTo = from ls in Obj._lsreturn
                             where ls.ReturnDate.Date >= Fdate.Date && ls.ReturnDate <= TDate.Date
                             select ls;
                FillReturnMasterGv(fromTo.ToList());
            }
        }

        protected void txtVendorNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtVendorNumber.Text.Trim() != "")
            {
                List<Return> LsVendorNUm = new List<Return>();
                foreach (var item in Obj._lsreturn)
                {
                    if (item.VendorNumber.Contains(txtVendorNumber.Text))
                    {
                        LsVendorNUm.Add(item);
                    }
                }
                FillReturnMasterGv(LsVendorNUm.ToList());
            } 

        }

       
    }
}