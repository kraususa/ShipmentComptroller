using PackingClassLibrary.BusinessLogic;
using PackingClassLibrary.Commands.SMcommands;
using PackingClassLibrary.Commands.SMcommands.RGA;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PackingClassLibrary.Models
{
    /// <summary>
    /// Author: Avinash.
    /// Versiom: Alfa.
    /// Packing Information model.
    /// </summary>
   public class model_Packing
    {
       public static Guid PackingID { get; set;}
       public cstPackageTbl PackageInfo { get; set; }
       public model_Shipment ShipmentInfo { get; set; }
       public Boolean IsMangerOverride { get; set; }
       public Boolean IsSelfOverride{get;set;}
       public int PackingStatus { get; set; }

       /// <summary>
       /// Package Functions.
       /// </summary>
       public cmdPackage PackageFunctions = new cmdPackage();

       /// <summary>
       /// 
       /// </summary>
      // public List<Return> _lsreturn = new List<Return>();
       public List<Return> _lsreturn { get; protected set; }


       /// <summary>
       /// create return detail object.
       /// </summary>
       public List<ReturnDetail> _lsreturndetail { get; protected set;}


       /// <summary>
       /// create object of cmdreturn.
       /// </summary>
       public cmdReturn _cmdreturn = new cmdReturn();


       public cmdReturnDetails cRetutnDetailsTbl = new cmdReturnDetails();

       /// <summary>
       /// Default constructor of Model Packing.
       /// </summary>
       public model_Packing() { }

       /// <summary>
       /// Parameterised Constructor of packing Model.
       /// </summary>
       /// <param name="PackingIDc">Guid Packing ID of the package table</param>
       public model_Packing(Guid PackingIDc)
       {
           PackingID = PackingIDc;
           setPackingInfo();
       }

       /// <summary>
       /// Set packing Information to the Packing object of the model class
       /// </summary>
       public void setPackingInfo()
       {
           cmdPackage _packing = new cmdPackage();
           PackageInfo = _packing.Execute().SingleOrDefault(i => i.PackingId == PackingID);

           //Set Packing Status.
           PackingStatus = PackageInfo.PackingStatus;
           IsMangerOverride = false;
           IsSelfOverride = false;
           if (PackageInfo.MangerOverride  ==1)
           {
               IsMangerOverride = true;
           }
           else if(PackageInfo.MangerOverride == 2)
           {
               IsSelfOverride = true;
           }

           //Packing Information Present then Fill its shipment information
           if (PackageInfo !=null)
           {
               SetShipmentInfo();
           }
       }

       /// <summary>
       /// set shipping information related to the packing model packing ID.
       /// </summary>
       public void SetShipmentInfo()
       {
           ShipmentInfo = new model_Shipment(PackageInfo.ShippingNum);
       }

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
       public Guid SetReturnTbl(Return _lsreturn,byte Status,byte Decision,DateTime orderdate)
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
               TblRerutn.OrderDate = orderdate;
               TblRerutn.DeliveryDate = _lsreturn.DeliveryDate;
               TblRerutn.ReturnDate = _lsreturn.ReturnDate;
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
               TblRerutn.ReturnReason = _lsreturn.ReturnReason;
               TblRerutn.RMAStatus = Status;
               TblRerutn.Decision = Decision;
               TblRerutn.CreatedBy = _lsreturn.CreatedBy;
               TblRerutn.CreatedDate = _lsreturn.CreatedDate;
               TblRerutn.UpdatedBy = null;
               TblRerutn.UpdatedDate = _lsreturn.UpdatedDate;

               if (_cmdreturn.UpdateReturn(TblRerutn)) ReturnID = TblRerutn.ReturnID;
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
       public Guid SetReturnDetailTbl(int deliveredQTY,int returnQTY)
       {
           Guid returndetail = Guid.NewGuid();
           try
           {
               ReturnDetail TblReturnDetails = new ReturnDetail();

              
               TblReturnDetails.ReturnID = _lsreturndetail[0].ReturnID;
               TblReturnDetails.SKUNumber = _lsreturndetail[0].SKUNumber;
               TblReturnDetails.ProductName = _lsreturndetail[0].ProductName;
               TblReturnDetails.DeliveredQty = deliveredQTY;
               TblReturnDetails.ExpectedQty = _lsreturndetail[0].ExpectedQty;
               TblReturnDetails.TCLCOD_0 = _lsreturndetail[0].TCLCOD_0;
               TblReturnDetails.ReturnQty = returnQTY;
               TblReturnDetails.ProductStatus = 0;
               TblReturnDetails.CreatedBy = _lsreturndetail[0].CreatedBy;
               TblReturnDetails.CreatedDate = _lsreturndetail[0].CreatedDate;
               TblReturnDetails.UpadatedDate = _lsreturndetail[0].UpadatedDate;
               TblReturnDetails.UpdatedBy = _lsreturndetail[0].UpdatedBy;

               if (cRetutnDetailsTbl.UpdateReturnDetail(TblReturnDetails)) returndetail = TblReturnDetails.ReturnDetailID;
           }
           catch (Exception)
           {
           }
           return returndetail;
       }


    }
}
