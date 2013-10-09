using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Models
{
    public class modelRoles
    {
        /// <summary>
        /// String split by charactor in permission set on the action Veriable
        /// </summary>
        /// <param name="Action">
        /// string Permission set
        /// </param>
        /// <returns>
        /// String permission set
        /// </returns>
        public String ActionString(String Action)
        {
            string _return="";
            try
            {
                String[] Permission = Action.Split('-', '&');
                //User Permission
                _return = _return + "User Permission=";
                if (Convert.ToBoolean(Permission[0].ToString()))
                    _return = _return + " View";
                if (Convert.ToBoolean(Permission[1].ToString()))
                    _return = _return + ", New";
                if (Convert.ToBoolean(Permission[2].ToString()))
                    _return = _return + ", Edit";
                if (Convert.ToBoolean(Permission[3].ToString()))
                    _return = _return + ", Delete";
                
                //Shipment permission
                _return = _return + " | Shipment Permission=";
                if (Convert.ToBoolean(Permission[4].ToString()))
                    _return = _return + " View";
                if (Convert.ToBoolean(Permission[5].ToString()))
                    _return = _return + ", Scan";
                if (Convert.ToBoolean(Permission[6].ToString()))
                    _return = _return + ", ReScan";
                if (Convert.ToBoolean(Permission[7].ToString()))
                    _return = _return + ", Override";
            }
            catch (Exception)
            {}
            return _return;
        }
        
    }

    
}