using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.GetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Models
{
    public class modelReturn
    {
        public List<ReturnDetail> ReturnAllRowsfromReturnTbl(List<Return> lsReturn)
        {
            List<ReturnDetail> lsReD = new List<ReturnDetail>();
            try
            {
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

            }
            catch (Exception)
            { }

            return lsReD;

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
        public String linkButtonText(String LinkButtonID, GridView GridViewName)
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

        public String ConvertToDecision(int Value)
        {
            switch (Value)
            {
                case 0:
                    return "New";

                case 1:
                    return "Approved";


                case 2:
                    return "Pending";


                case 3:
                    return "Canceled";

                default:
                    return "";
            }
        }

        public List<Return> SortedListOFReturn(String sortExperssion)
        {
            List<Return> lsShippingSorted = new List<Return>();
            try
            {

                switch (sortExperssion)
                {
                    case "RGAROWID":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.RGAROWID).ToList());
                        break;
                    case "RMANumber":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.RMANumber).ToList());
                        break;
                    case "RMAStatus":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.RMAStatus).ToList());
                        break;
                    case "Decision":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.Decision).ToList());
                        break;
                    case "CustomerName":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.CustomerName1).ToList());
                        break;
                    case "ShipmentNumber":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.ShipmentNumber).ToList());
                        break;
                    case "VendorNumber":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.VendorNumber).ToList());
                        break;
                    case "VendoeName":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.VendoeName).ToList());
                        break;
                    case "ReturnDate":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.ReturnDate).ToList());
                        break;
                    case "PONumber":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.PONumber).ToList());
                        break;
                    case "OrderNumber":
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.OrderNumber).ToList());
                        break;

                    default:
                        lsShippingSorted = (Obj._lsreturn.OrderBy(i => i.RGAROWID).ToList());
                        break;
                }
            }
            catch (Exception)
            {
            }
            return lsShippingSorted;
        }

        public List<ReturnDetail> SortedListOfReturnDetails(string SortExpression)
        {
            List<ReturnDetail> lsShippingSorted = new List<ReturnDetail>();
            try
            {
                switch (SortExpression)
            {
                case "RGADROWID":
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.RGADROWID).ToList());
                    break;
                case "SKUNumber":
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.SKUNumber).ToList());
                    break;
                case "ProductName":
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.ProductName).ToList());
                    break;
                case "DeliveredQty":
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.DeliveredQty).ToList());
                    break;
                case "ReturnQty":
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.ReturnQty).ToList());
                    break;
                default:
                    lsShippingSorted = (Obj._lsReturnDetails.OrderBy(i => i.RGADROWID).ToList());
                    break;
            }
            }
            catch (Exception)
            {}
            return lsShippingSorted;

        }
    }

}