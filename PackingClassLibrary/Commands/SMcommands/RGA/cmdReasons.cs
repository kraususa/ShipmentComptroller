using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReasons
    {
       public List<Reason> ReasonsAll()
       {
           List<Reason> _lsResons = new List<Reason>();
           try
           {
               var resn = from ls in Service.GetRMA.ReasonsAll()
                          select ls;
               foreach (var Ritem in resn)
               {
                   _lsResons.Add(new Reason(Ritem));
               }
           }
           catch (Exception)
           { }
           return _lsResons;

       }
        public List<Reason> ReasonByCategoryName(string CategoryName)
        {
            List<Reason> _lsResons = new List<Reason>();
            try
            {
                var resn = from ls in Service.GetRMA.ReasonByCategoryName(CategoryName)
                           select ls;
                foreach (var Ritem in resn)
                {
                    _lsResons.Add(new Reason(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsResons;
        }

        public string ListOfReasons(Guid ReturnDetailID)
        {
            return Service.GetRMA.ListOfReasons(ReturnDetailID);
        }

    }
}
