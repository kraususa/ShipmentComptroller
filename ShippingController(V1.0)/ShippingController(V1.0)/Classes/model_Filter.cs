using PackingClassLibrary.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShippingController_V1._0_.Classes
{
    public class model_Filter
    {  /// <summary>
        /// Filter ON Status
        /// </summary>

        public static bool IsUserFilerOn = false;
        public static bool IsShipmentNumberFilterOn = false;
        public static bool IsPackingStatusFilterOn = false;
        public static bool IsDateTimeFilterOn = false;
        public static bool IsCuStomerPOFilterOn = false;
        public static bool IsLocationFilterOn = false;
        public static bool IsOverrideModeFilterOn = false;

        protected static Guid _userID;
        protected static String _shipmentNumber;
        protected static int _packingStatus;
        protected static DateTime _fromDate;
        protected static DateTime _toDate;
        protected static String _cusTomerPo;
        protected static String _location;
        protected static int _overrdeMode;


        public static Guid UserID
        {
            get { return _userID; }
            set
            {
                IsUserFilerOn = true;
                _userID = value;
            }
        }
        public static string ShipmentNumber
        {
            get { return _shipmentNumber; }
            set
            {
                IsShipmentNumberFilterOn = true;
                _shipmentNumber = value;
            }
        }
        public static int PackingStatus
        {
            get { return _packingStatus; }
            set
            {
                IsPackingStatusFilterOn = true;
                _packingStatus = value;
            }
        }
        public static DateTime FromDate
        {
            get { return _fromDate; }
            set
            {
                IsDateTimeFilterOn = true;
                _fromDate = value;
            }
        }
        public static DateTime Todate
        {
            get { return _toDate; }
            set
            {
                IsDateTimeFilterOn = true;
                _toDate = value;
            }
        }
        public static String CusTomerPo
        {
            get { return _cusTomerPo; }
            set
            {
                IsCuStomerPOFilterOn = true;
                _cusTomerPo = value;
            }
        }
        public static String Location
        {
            get { return _location; }
            set
            {
                IsLocationFilterOn = true;
                _location = value;
            }
        }
        public static int OverrdeMode
        {
            get { return _overrdeMode; }
            set
            {
                IsOverrideModeFilterOn = true;
                _overrdeMode = value;
            }
        }


        public static List<cstPackageTbl> GetPackageTbl()
        {
            List<cstPackageTbl> _lspackagetbl = Obj.call.GetPackingTbl();
            try
            {
                if (IsShipmentNumberFilterOn)
                {
                    List<cstPackageTbl> _tempPacktbl = new List<cstPackageTbl>();
                    foreach (cstPackageTbl lsitem in _lspackagetbl)
                    {
                        if (lsitem.ShippingNum == _shipmentNumber)
                        {
                            _tempPacktbl.Add(lsitem);
                        }
                    }
                    _lspackagetbl = _tempPacktbl;
                }

                if (IsUserFilerOn)
                {
                    List<cstPackageTbl> _tempPacktbl = new List<cstPackageTbl>();
                    foreach (cstPackageTbl lsitem in _lspackagetbl)
                    {
                        if (lsitem.UserID == _userID)
                        {
                            _tempPacktbl.Add(lsitem);
                        }
                    }
                    _lspackagetbl = _tempPacktbl;
                }

                if (IsLocationFilterOn)
                {
                    List<cstPackageTbl> _tempPacktbl = new List<cstPackageTbl>();
                    foreach (cstPackageTbl lsitem in _lspackagetbl)
                    {
                        if (lsitem.ShipmentLocation == _location)
                        {
                            _tempPacktbl.Add(lsitem);
                        }
                    }
                    _lspackagetbl = _tempPacktbl;
                }

                if (IsPackingStatusFilterOn)
                {
                    List<cstPackageTbl> _tempPacktbl = new List<cstPackageTbl>();
                    foreach (cstPackageTbl lsitem in _lspackagetbl)
                    {
                        if (lsitem.PackingStatus == _packingStatus)
                        {
                            _tempPacktbl.Add(lsitem);
                        }
                    }
                    _lspackagetbl = _tempPacktbl;
                }

            }
            catch (Exception)
            { }
            return _lspackagetbl;
        }
    }
}