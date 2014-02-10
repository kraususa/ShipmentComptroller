using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdSKUReasons
    {
       public Boolean DeleteByReturnDetailsID(Guid ReturnDetailsID)
       {
           return Service.DeleteRMA.SKUReasonsByReturnDetailsID(ReturnDetailsID);
       }

    }
}
