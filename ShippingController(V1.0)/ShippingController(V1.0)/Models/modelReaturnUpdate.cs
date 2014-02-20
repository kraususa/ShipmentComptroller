using PackingClassLibrary.Commands.SMcommands.RGA;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Models
{
    public class modelReaturnUpdate
    {
        cmdReasons cRtnreasons = new cmdReasons();
        /// <summary>
        /// update all return information.
        /// </summary>
        /// <param name="Status">
        /// pass status as parameter.
        /// </param>
        /// <param name="Decision">
        /// pass decision as parameter.
        /// </param>
        /// <returns></returns>
        public Guid SetReturnTbl(Return _lsreturn, byte Status, byte Decision, DateTime returndate,string reasons)
        {
            Guid ReturnID = Guid.NewGuid();
            try
            {
                Return TblRerutn = new Return();

                TblRerutn.ReturnID = _lsreturn.ReturnID;
                TblRerutn.RMANumber = _lsreturn.RMANumber;
                TblRerutn.ShipmentNumber = _lsreturn.ShipmentNumber;
                TblRerutn.OrderNumber = _lsreturn.OrderNumber;
                TblRerutn.PONumber = _lsreturn.PONumber;
                TblRerutn.OrderDate = _lsreturn.OrderDate;
                TblRerutn.DeliveryDate = _lsreturn.DeliveryDate;
                TblRerutn.ReturnDate = returndate;
                TblRerutn.ScannedDate = _lsreturn.ScannedDate;
                TblRerutn.ExpirationDate = _lsreturn.ExpirationDate;
                TblRerutn.VendorNumber = _lsreturn.VendorNumber;
                TblRerutn.VendoeName = _lsreturn.VendoeName;
                TblRerutn.CustomerName1 = _lsreturn.CustomerName1;
                TblRerutn.CustomerName2 = _lsreturn.CustomerName2;
                TblRerutn.Address1 = _lsreturn.Address1;
                TblRerutn.Address2 = _lsreturn.Address2;
                TblRerutn.Address3 = _lsreturn.Address3;
                TblRerutn.ZipCode = _lsreturn.ZipCode;
                TblRerutn.City = _lsreturn.City;
                TblRerutn.State = _lsreturn.State;
                TblRerutn.Country = _lsreturn.Country;
                TblRerutn.ReturnReason = reasons.ToString();
                TblRerutn.RMAStatus = Status;
                TblRerutn.Decision = Decision;
                TblRerutn.CreatedBy = _lsreturn.CreatedBy;
                TblRerutn.CreatedDate = _lsreturn.CreatedDate;
                TblRerutn.UpdatedBy = _lsreturn.CreatedBy;
                TblRerutn.UpdatedDate = _lsreturn.UpdatedDate;

                if (Obj.Rcall.UpsetReturnTbl(TblRerutn)) ReturnID = TblRerutn.ReturnID;
            }
            catch (Exception)
            {
            }
            return ReturnID;

        }

        /// <summary>
        /// update return detail information.
        /// </summary>
        /// <returns>
        /// retund returndetailID
        /// </returns>
        public Guid SetReturnDetailTbl(ReturnDetail _lsreturndetail, int deliveredQTY, int returnQTY, String SKuNumber, String ProductName)
        {
            Guid returndetail = Guid.NewGuid();
            try
            {
                ReturnDetail TblReturnDetails = new ReturnDetail();

                TblReturnDetails.ReturnDetailID = _lsreturndetail.ReturnDetailID;
                TblReturnDetails.ReturnID = _lsreturndetail.ReturnID;
                TblReturnDetails.SKUNumber = SKuNumber;
                TblReturnDetails.ProductName = ProductName;
                TblReturnDetails.DeliveredQty = deliveredQTY;
                TblReturnDetails.ExpectedQty = _lsreturndetail.ExpectedQty;
                TblReturnDetails.TCLCOD_0 = _lsreturndetail.TCLCOD_0;
                TblReturnDetails.ReturnQty = returnQTY;
                TblReturnDetails.ProductStatus = 0;
                TblReturnDetails.CreatedBy = _lsreturndetail.CreatedBy;
                TblReturnDetails.CreatedDate = _lsreturndetail.CreatedDate;
                TblReturnDetails.UpadatedDate = _lsreturndetail.UpadatedDate;
                TblReturnDetails.UpdatedBy = _lsreturndetail.UpdatedBy;

                if (Obj.Rcall.UpsetReturnDetails(TblReturnDetails)) returndetail = TblReturnDetails.ReturnDetailID;
            }
            catch (Exception)
            {
            }
            return returndetail;
        }

        /// <summary>
        /// this Methods is used for to get ReasonID By ReturnDetails.
        /// </summary>
        /// <param name="ReturnDetailID">
        /// ReturnID pass as parameter.
        /// </param>
        /// <returns>
        /// Return String as ReturnDetailID.
        /// </returns>
        public String ReasonsIdByHasg(Guid ReturnDetailID)
        {
            String _return = "";
            var lsReasonId = Obj.Rcall.ReasonsByReturnDetailID(ReturnDetailID);
            try
            {
                foreach (var Resnitem in lsReasonId)
                {
                    _return += Resnitem.ReasonID + "#";
                }
            }
            catch (Exception)
            {
            }
            return _return;
        }

        /// <summary>
        /// this Method is used to count selected Reasons.
        /// </summary>
        /// <param name="ReturnDetailID">
        /// pass ReturnDetailID as Parameter.
        /// </param>
        /// <returns>
        /// Return Int Value.
        /// </returns>
        public int ReasonCount(Guid ReturnDetailID)
        {
            int _return = 0;
            var lsReasonId = Obj.Rcall.ReasonsByReturnDetailID(ReturnDetailID);
            try
            {
                _return = lsReasonId.Count();
            }
            catch (Exception)
            {
            }
            return _return;
        }

        /// <summary>
        /// This Method is for Delete SKUreasons By ReturnDetailID.
        /// </summary>
        /// <param name="ReturnDetailsID">
        /// Pass ReturnDetailID as parameter.
        /// </param>
        /// <returns>
        /// Return Boolean Value.
        /// </returns>
        public Boolean DeleteSKuReasonsByReturnDetailID(Guid ReturnDetailsID)
        {
            return Obj.Rcall.DeleteSKUReasonsByReturnDetailID(ReturnDetailsID);
        }

        /// <summary>
        /// This Function is for Set SKUReasons table.
        /// </summary>
        /// <param name="ReasonID">
        /// Pass ReasonID as parameter.
        /// </param>
        /// <param name="ReturnDetailID">
        /// pass ReturnDetailID as parameter.
        /// </param>
        /// <returns></returns>
        public Guid SetSkuReasons(Guid ReasonID, Guid ReturnDetailID)
        {
            Guid _transationID = Guid.Empty;
            try
            {
                SKUReason tra = new SKUReason();
                tra.SKUReasonID = Guid.NewGuid();
                tra.ReasonID = ReasonID;
                tra.ReturnDetailID = ReturnDetailID;

                if (cRtnreasons.SetSKuReasons(tra)) _transationID = tra.SKUReasonID;
            }
            catch (Exception)
            {

            }
            return _transationID;
        }
    }
}