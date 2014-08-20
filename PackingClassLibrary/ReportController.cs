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

       cmdReturn _cReturn = new cmdReturn();
       cmdReturnDetails _cReturnDetail = new cmdReturnDetails();
       cmdReasons _cReasons = new cmdReasons();
       cmdReasonCategory _cReasonCategoty = new cmdReasonCategory();
       cmdSKUReasons _cSKUReasons = new cmdSKUReasons();

       cmdUserforRGA _cuser = new cmdUserforRGA();

       cmdRMAComment _cRMAComment = new cmdRMAComment();

       cmdReturnedSKUPoints _cReturnedSKUPoint = new cmdReturnedSKUPoints();

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

       #region ReturnedSkuPoints

       public List<ReturnedSKUPoints> ReturnedSKUansPoints(Guid ReturnID)
       {
           return _cReturnedSKUPoint.GetReturnedSKUPointsByReturnID(ReturnID);
       
       }

       #endregion

       #region RGA

       #region Return

       public List<Return> ReturnAll()
       {
           return _cReturn.GetallReturn();
       }

       public Return ReturnByReturnID(Guid ReturnID)
       {
           return _cReturn.ReturnByReturnID(ReturnID);
       }
       public Return ReturnByRMANumber(string RMANumber)
       {
           return _cReturn.ReturnByRMANumber(RMANumber);
       }

       public List<Return> ReturnByOrderNum(string OrderNum)
       {
           return _cReturn.ReturnByOrderNum(OrderNum);
       }

       public List<Return> ReturnByVendoeNum(string VendorNumber)
       {
           return _cReturn.ReturnByVendoeNum(VendorNumber);
       }

       public List<Return> ReturnByVendorName(string VendorName)
       {
           return _cReturn.ReturnByVendorName(VendorName);
       }

       public List<Return> ReturnByShipmentNumber(string ShipmentNumber)
       {
           return _cReturn.ReturnByShipmentNumber(ShipmentNumber);
       }

       public List<Return> ReturnByPONumber(string PONumber)
       {
           return _cReturn.ReturnByPONumber(PONumber);
       }

       public List<Return> ReturnByRGAROWID(string RGAROWID)
       {
           return _cReturn.ReturnByRGAROWID(RGAROWID);
       }

       public List<Return> ReturnByRGADROWID(string RGADROWID)
       {
           return _cReturn.ReturnByRGADROWID(RGADROWID);
       }

       public Boolean UpsetReturnTbl(Return Rtn)
       {
           return _cReturn.UpdateReturn(Rtn);
       }
       public List<String> GetCustByPOnumber(string chars)
       {
           return _cReturn.GetPONumber(chars);
       }
       public List<string> VenderName(string chars)
       {
           return _cReturn.GetVenderName(chars);
       }
       public List<string> VenderNumber(string chars)
       {
           return _cReturn.GetVenderNumber(chars);
       }
       public string VenderNameByVenderNumber(string VenderNum)
       {
           return _cReturn.GetVenderNamebyVenderNumber(VenderNum);
       }
       public string VenderNumberByVenderName(string VenderName)
       {
           return _cReturn.GetVenderNumberByVenderName(VenderName);
       }
       #endregion

       #region Return Detail

       public List<ReturnDetail> ReturnDetailAll()
       {
           return _cReturnDetail.ReturnDetailAll();
       }

       public List<ReturnDetail> ReturnDetailByretrnID(Guid RetunID)
       {
           return _cReturnDetail.ReturnDetailByretrnID(RetunID);
       }

       public List<ReturnDetail> ReturnDetailByRetundetailID(Guid RetundetailID)
       {
           return _cReturnDetail.ReturnDetailByRetundetailID(RetundetailID);
       }

       public List<ReturnDetail> ReturnDetailByRGADROWID(string RGADROWID)
       {
           return _cReturnDetail.ReturnDetailByRGADROWID(RGADROWID);
       }

       public List<ReturnDetail> ReturnDetailByRGAROWID(string RGAROWID)
       {
           return _cReturnDetail.ReturnDetailByRGAROWID(RGAROWID);
       }

       public Boolean UpsetReturnDetails(ReturnDetail ReturnDtls)
       {
           return _cReturnDetail.UpdateReturnDetail(ReturnDtls);
       }

       #endregion

       #region Resons

       public List<Reason> ReasonsAll()
       {
           return _cReasons.ReasonsAll();
       }

       public List<Reason> ReasonByCategoryName(string CategoryName)
       {
           return _cReasons.ReasonByCategoryName(CategoryName);
       }

       public string ReasonsListByReturnDetails(Guid ReturnDetailID)
       {
           return _cReasons.ListOfReasons(ReturnDetailID);
       }
       public Guid UpsertReasons(string _Reason)
       {
           //return _cReasons.UpsertReason(_Reason);

           Guid _reasonID = Guid.Empty;

           try
           {
               Reason ReasonTable = new Reason();

               ReasonTable.ReasonID = Guid.NewGuid();
               ReasonTable.Reason1 = _Reason;

               if (_cReasons.UpsertReason(ReasonTable)) _reasonID = ReasonTable.ReasonID;
           }
           catch (Exception)
           {
           }
           return _reasonID;
       }

       public Guid SetTransaction(Guid SKUReasonID, Guid ReasonID, Guid ReturnDetailID)
       {
           Guid _transationID = Guid.Empty;
           try
           {
               SKUReason tra = new SKUReason();
               tra.SKUReasonID = SKUReasonID;
               tra.ReasonID = ReasonID;
               tra.ReturnDetailID = ReturnDetailID;

               if (_cReasons.SetSKuReasons(tra)) _transationID = tra.SKUReasonID;
           }
           catch (Exception)
           {
              
           }
           return _transationID;
       }






       public List<Reason> ReasonsByReturnDetailID(Guid ReturnDetailID)
       {
           return _cReasons.GetReasonsByReturnDetailID(ReturnDetailID);
       }

       public string GetReasonsInStringByReturnDetailIDF(Guid ReturnDetailID)
       {
           return _cReasons.GetReasonsInStringByReturnDetailID(ReturnDetailID);
       }

       public Boolean DeleteReasonByReasonID(Guid ReasonID)
       {
           return _cReasons.DeleteByReasonID(ReasonID);
       }

       #endregion

       #region SKUReasons

       public Boolean DeleteSKUReasonsByReturnDetailID(Guid ReturnDetailID)
       {
           return _cSKUReasons.DeleteByReturnDetailsID(ReturnDetailID);
       }

       public List<SKUReason> SKUReasonsByReturnDetails(List<ReturnDetail> LsRetnDetails)
       {
           return _cSKUReasons.GetReasons(LsRetnDetails);
       }

       public string GetReasonstringbyReturnID(Guid ReturnDetialID)
       {
           return _cReasons.GetReasonstringByReturnDetailID(ReturnDetialID);
       }



       #endregion

       #region Return Images

       public List<String> ReturnImagesByReturnDetailsID(Guid ReturnDetailsID)
       {
           cmdReturnImages _images = new cmdReturnImages();
           return _images.ReturnImagesByReturnDetailsID(ReturnDetailsID);
       }

       #endregion

       #region CategotyReson table

       public List<ReasonCategoty> GetReasonCategotyAll()
       {
           return _cReasonCategoty.All();
       }

       public List<ReasonCategoty> GetReasonCategoryByReasonID(Guid ReasonID)
       {
           return _cReasonCategoty.CategotyReasonNameByReasonID(ReasonID);
       }

       public Boolean UpsertReasonCategory(ReasonCategoty ReasonCat)
       {
           return _cReasonCategoty.UpsertReasonCategory(ReasonCat);
       }

    
       #endregion
       
       
       #endregion


       #region User
       public UserMaster GetUserInfobyUserID(Guid UserID)
       {
           return _cuser.UserInfoByUserID(UserID);
       }
       #endregion

       #region RMAComment
       public List<RMAComment> GetRMACommentByReturnID(Guid ReturnID)
       {
           return _cRMAComment.GetCommentByReturnID(ReturnID);
       }

       public Boolean InsertRMACommnt(RMAComment Comment)
       {
           return _cRMAComment.InsertComment(Comment);
       }


       #endregion


   }
}
