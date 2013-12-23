using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.Commands;
using PackingClassLibrary.Commands.ReportCommands;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Commands.SMcommands.RGA;

namespace PackingClassLibrary
{
   public class ReportController
   {

       #region Declaration

       cmdReturn _return = new cmdReturn();
       cmdReturnDetails _returnDetail = new cmdReturnDetails();
       cmdReasons _reasons = new cmdReasons();

       #endregion


       #region Shipping table Processing
       /// <summary>
       /// Get all Shipping ids information with Delivery provider name
       /// </summary>
        /// <returns>List<cstShippingInfoBPName></returns>
       public List<cstShippingInfoBPName> GetBpinfoOFShippingNum()
       {
           cmdBPNameShippingNum command = new cmdBPNameShippingNum();
           return command.GetBpinfoOFShippingNum();
       }

       /// <summary>
       /// get Delivery provider Name from the number
       /// </summary>
       /// <param name="BPCNUM">Strign Delivery provider Number</param>
       /// <returns>String Delivery provider Name</returns>
       public String GetDeliveryProviderNameFromBPCNUM(String BPCNUM)
       {
           cmdBPNameShippingNum command = new cmdBPNameShippingNum();
           return command.getBPNameFromBPNUM(BPCNUM);
       }
       #endregion
       
       #region Total Shipment Packed By user Per date
       /// <summary>
       /// User packed total shipment count per day per user
       /// </summary>
       /// <returns>List of cstUserShipmentCount</returns>
       public List<cstUserShipmentCount> GetUserTotalPakedPerDay()
       {
           cmdUserShipmentCount command = new cmdUserShipmentCount();
           return command.GetAllShipmentCountByUser();
       }
        
       #endregion
       
       /// <summary>
       /// Shipment number serch for information of packing status
       /// </summary>
       /// <param name="ShippingNumber">String Shipping Number</param>
       /// <returns>List<cstShipmentNumStatus> depending on location retuersn shipping number information</returns>
       #region Shipping Number with status and location

       public List<cstShipmentNumStatus> GetShippingStatus(String ShippingNumber)
       {
           cmdShippinNumStatus command = new cmdShippinNumStatus();
          return command.GetStaus(ShippingNumber);
       }

       #endregion
       
       #region Station Total Packed and Unpacked.
       /// <summary>
       /// Total Shipment packed per station and under packing Shipments per station
       /// </summary>
       /// <returns>List<cstStationToatlPacked>  information</returns>
       public List<cstStationToatlPacked> GetStationTotalPaked()
       {
           cmdStationTotalPacked command = new cmdStationTotalPacked();
           return command.GetEachStationPacked();
       }

       /// <summary>
       /// For Station Dashboard screen
       /// </summary>
       /// <param name="DatetimeNow"></param>
       /// <returns></returns>
       public List<cstDashBoardStion> GetStationDashboard(DateTime DatetimeNow)
       {
           cmdStationTotalPacked command = new cmdStationTotalPacked();
           return command.GetStationByReport(DatetimeNow);
       }

       /// <summary>
       /// Total Shipment packed per station and under packing Shipments per station on the given date
       /// </summary>
       /// <param name="ReportDate">Date Time Report Date</param>
       /// <returns>List<cstStationToatlPacked></returns>
       public List<cstStationToatlPacked> GetStationTotalPaked(DateTime ReportDate)
       {
           cmdStationTotalPacked command = new cmdStationTotalPacked();
           return command.GetEachStationPacked(ReportDate);
       }

       /// <summary>
       /// Station Total Packed Shipment Today by staion Name
       /// </summary>
       /// <param name="StationName">
       /// String Staion name.
       /// </param>
       /// <returns>
       /// inter total packed Shipment Count. else 0.
       /// </returns>
       public int TotalPackedTodayByStationID(String  StationName)
       {
           cmdStationTotalPacked cmd = new cmdStationTotalPacked();
           return cmd.PackedTodayByStationID(StationName);
       }


       public String GetShippingNumByStation(String StationName)
       {
           cmdStationTotalPacked cmd = new cmdStationTotalPacked();
           return cmd.UnderPackingID(StationName);
       }
       #endregion



       #region RGA 

       #region Return

       public List<Return> ReturnAll()
       {
           return _return.GetallReturn();
       }

       public Return ReturnByReturnID(Guid ReturnID)
       {
           return _return.ReturnByReturnID(ReturnID);
       }
       public Return ReturnByRMANumber(string RMANumber)
       {
           return _return.ReturnByRMANumber(RMANumber);
       }

       public List<Return> ReturnByOrderNum(string OrderNum)
       {
           return _return.ReturnByOrderNum(OrderNum);
       }

       public List<Return> ReturnByVendoeNum(string VendorNumber)
       {
           return _return.ReturnByVendoeNum(VendorNumber);
       }

       public List<Return> ReturnByVendorName(string VendorName)
       {
           return _return.ReturnByVendorName(VendorName);
       }

       public List<Return> ReturnByShipmentNumber(string ShipmentNumber)
       {
           return _return.ReturnByShipmentNumber(ShipmentNumber);
       }

       public List<Return> ReturnByPONumber(string PONumber)
       {
           return _return.ReturnByPONumber(PONumber);
       }

       public List<Return> ReturnByRGAROWID(string RGAROWID)
       {
           return _return.ReturnByRGAROWID(RGAROWID);
       }

       public List<Return> ReturnByRGADROWID(string RGADROWID)
       {
           return _return.ReturnByRGADROWID(RGADROWID);
       }

       #endregion

       #region Return Detail

       public List<ReturnDetail> ReturnDetailAll()
       {
           return _returnDetail.ReturnDetailAll();
       }

       public List<ReturnDetail> ReturnDetailByretrnID(Guid RetunID)
       {
           return _returnDetail.ReturnDetailByretrnID(RetunID);
       }

       public List<ReturnDetail> ReturnDetailByRetundetailID(Guid RetundetailID)
       {
           return _returnDetail.ReturnDetailByRetundetailID(RetundetailID);
       }

       public List<ReturnDetail> ReturnDetailByRGADROWID(string RGADROWID)
       {
           return _returnDetail.ReturnDetailByRGADROWID(RGADROWID);
       }

       public List<ReturnDetail> ReturnDetailByRGAROWID(string RGAROWID)
       {
           return _returnDetail.ReturnDetailByRGAROWID(RGAROWID);
       }
       #endregion

       #region Resons

       public List<Reason> ReasonsAll()
       {
           return _reasons.ReasonsAll();
       }

       public List<Reason> ReasonByCategoryName(string CategoryName)
       {
           return _reasons.ReasonByCategoryName(CategoryName);
       }

       public string ReasonsListByReturnDetails(Guid ReturnDetailID)
       {
           return _reasons.ListOfReasons(ReturnDetailID);
       }


       #endregion


       #region Return Images

       public List<String> ReturnImagesByReturnDetailsID(Guid ReturnDetailsID)
       {
           cmdReturnImages _images = new cmdReturnImages();
           return _images.ReturnImagesByReturnDetailsID(ReturnDetailsID);
       }

       #endregion


       #endregion

   }
}
