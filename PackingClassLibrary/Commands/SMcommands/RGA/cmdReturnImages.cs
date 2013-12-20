using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReturnImages
    {

       public List<string> ReturnImagesByReturnDetailsID(Guid ReturnDetailsID)
        {

            return Service.GetRMA.ImagePathStringList(ReturnDetailsID).ToList();
        }
    }
}
