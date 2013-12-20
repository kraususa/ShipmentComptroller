using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReturn
    {
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

    }
}
