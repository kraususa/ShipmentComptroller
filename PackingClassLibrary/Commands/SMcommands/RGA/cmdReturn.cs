using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReturn
   {
       #region Get Methods
       
       
       public List<Return> GetallReturn()
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnAll()
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           {}
           return _lsreturn;
       }

       public Return ReturnByReturnID(Guid ReturnID)
       {
           return new Return(Service.GetRMA.ReturnByReturnID(ReturnID));
       }

       public Return ReturnByRMANumber(string RMANumber)
       {
           return new Return(Service.GetRMA.ReturnByRMANumber(RMANumber));
       }

       public List<Return> ReturnByOrderNum(string OrderNum)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByOrderNum(OrderNum)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByVendoeNum(string VendorNumber)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByVendoeNum(VendorNumber)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByVendorName(string VendorName)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByVendorName(VendorName)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByShipmentNumber(string ShipmentNumber)
       {

           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByShipmentNumber(ShipmentNumber)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByPONumber(string PONumber)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByPONumber(PONumber)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByRGAROWID(string RGAROWID)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByRGAROWID(RGAROWID)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public List<Return> ReturnByRGADROWID(string RGADROWID)
       {
           List<Return> _lsreturn = new List<Return>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnByRGADROWID(RGADROWID)
                       select ls;

               foreach (var Ritem in v)
               {
                   _lsreturn.Add(new Return(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsreturn;
       }

       public Return GetReturnTblByReturnID(Guid ReturnID)
       {
           Return _returnObj = new Return();
           try
           {
               _returnObj = new Return(Service.GetRMA.ReturnByReturnID(ReturnID));
           }
           catch (Exception )
           {
           }
           return _returnObj;
       }

       #endregion

       #region Set Method

       /// <summary>
       /// Update return Table information.
       /// </summary>
       /// <param name="_lsreturn">
       /// pass return object as parameter.
       /// </param>
       /// <returns>
       /// return Bolean
       /// </returns>
       public Boolean UpdateReturn(Return _lsreturn)
       {
           Boolean _flag = false;
           try
           {
               _flag = Service.SetRMA.Return(_lsreturn.CopyToSaveDTO(_lsreturn));
           }
           catch (Exception)
           {
           }
           return _flag;
       
       }


       public List<String> GetNewRMANumber(String Chars)
       {
           List<String> lsRMAInfo = new List<String>();
           try
           {
               var NewRMAdetailsInfo = Service.GetRMA.ProductMachingNameCat(Chars);
               if (NewRMAdetailsInfo.Count() > 0)
               {
                   foreach (var RMAitem in NewRMAdetailsInfo)
                   {
                       lsRMAInfo.Add(RMAitem);
                   }
               }
           }
           catch (Exception)
           {
              
           }
           return lsRMAInfo;
       }

       #endregion
   }
}
