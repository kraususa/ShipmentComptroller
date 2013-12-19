using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using System.Data.Objects.SqlClient;
using System.Data.Objects;
using PackingClassLibrary.Commands.SMcommands;

namespace PackingClassLibrary.Commands.ReportCommands
{
    public class cmdPackingTimeAndQuantity
    {
        
        /// <summary>
        /// Calculate all shipments toatal Quantity and Time Required to pack the saprate shipment
        /// </summary>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity()
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity1();

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }
        /// <summary>
        /// Shipment With its Time And SKu Quantity up to current Date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(Guid UserID)
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity2(UserID);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }

        /// <summary>
        /// Shipment With its Time And SKu Quantity on specified date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <param name="date"> DateTime For Filter</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(DateTime Fromdate, DateTime Todate)
        {

            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity3(Fromdate, Todate);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }

        /// <summary>
        /// Shipment With its Time And SKu Quantity on specified date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(Guid UserID, DateTime Fromdate, DateTime Todate)
        {

            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity4(UserID, Fromdate, Todate);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }


        public List<cstPackingTime> GetPackingTimeAndQantity(int PackingStatus, Boolean PackingStaus)
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity5(PackingStatus, PackingStaus);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }
        /// <summary>
        /// Shipment With its Time And SKu Quantity up to current Date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(Guid UserID, int PackingStatus)
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity6(UserID, PackingStatus);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }

        /// <summary>
        /// Shipment With its Time And SKu Quantity on specified date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <param name="date"> DateTime For Filter</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(DateTime Fromdate, DateTime Todate, int PackingStatus)
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity7(Fromdate, Todate, PackingStatus);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }

        /// <summary>
        /// Shipment With its Time And SKu Quantity on specified date
        /// </summary>
        /// <param name="UserID">Long UserID</param>
        /// <returns>List<cstPackingTime></returns>
        public List<cstPackingTime> GetPackingTimeAndQantity(Guid UserID, DateTime Fromdate, DateTime Todate, int PackingStatus)
        {
            List<cstPackingTime> _lsreturnPacingTime = new List<cstPackingTime>();
            try
            {
                var packingG = Service.Get.GetPackingTimeAndQantity8(UserID, Fromdate, Todate, PackingStatus);

                foreach (var listItem in packingG)
                {
                    cstPackingTime _Packing = new cstPackingTime();
                    _Packing.PackingID = listItem.PackingID;
                    _Packing.ShippingNumber = listItem.ShippingNumber;
                    _Packing.TimeSpend = listItem.TimeSpend;
                    _Packing.Quantity = listItem.Quantity;
                    _lsreturnPacingTime.Add(_Packing);
                }
            }
            catch (Exception)
            { }
            return _lsreturnPacingTime;
        }
    }
}
