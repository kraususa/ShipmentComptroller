﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingClassLibrary.CustomEntity.SMEntitys.RGA
{
    public class ReturnDetail
    {

        public Guid ReturnDetailID { get; set; }
        public Guid ReturnID { get; set; }
        public String SKUNumber { get; set; }
        public String ProductName { get; set; }
        public String TCLCOD_0 { get; set; }
        public int DeliveredQty { get; set; }
        public int ExpectedQty { get; set; }
        public int ReturnQty { get; set; }
        public int ProductStatus { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpadatedDate { get; set; }
        public String RGADROWID { get; set; }

        public ReturnDetail(GetRGAService.ReturnDetailsDTO _ReturnDetails)
        {
            if (_ReturnDetails.ReturnDetailID != Guid.Empty) this.ReturnDetailID = _ReturnDetails.ReturnDetailID;
            if (_ReturnDetails.ReturnID != Guid.Empty) this.ReturnID = _ReturnDetails.ReturnID;
            if (_ReturnDetails.SKUNumber != null) this.SKUNumber = _ReturnDetails.SKUNumber;
            if (_ReturnDetails.ProductName != null) this.ProductName = _ReturnDetails.ProductName;
            if (_ReturnDetails.TCLCOD_0 != null) this.TCLCOD_0 = _ReturnDetails.TCLCOD_0;
            this.DeliveredQty = (int)_ReturnDetails.DeliveredQty;
            this.ExpectedQty = (int)_ReturnDetails.ExpectedQty;
            this.ReturnQty = (int)_ReturnDetails.ReturnQty;
            this.ProductStatus = (int)_ReturnDetails.ProductStatus;
            if (_ReturnDetails.CreatedBy != Guid.Empty) this.CreatedBy = (Guid)_ReturnDetails.CreatedBy;
            if (_ReturnDetails.UpdatedBy != Guid.Empty) this.UpdatedBy = (Guid)_ReturnDetails.UpdatedBy;
            if (_ReturnDetails.CreatedDate != null) this.CreatedDate = (DateTime)_ReturnDetails.CreatedDate;
            if (_ReturnDetails.UpadatedDate != null) this.UpadatedDate = (DateTime)_ReturnDetails.UpadatedDate;
            this.RGADROWID = _ReturnDetails.RGADROWID;
        }

        public SetRGAService.ReturnDetailsDTO ConvertToSaveDTO(ReturnDetail _ReturnDetails)
        {
            SetRGAService.ReturnDetailsDTO _return = new SetRGAService.ReturnDetailsDTO();

            if (_ReturnDetails.ReturnDetailID != Guid.Empty) _return.ReturnDetailID = _ReturnDetails.ReturnDetailID;
            if (_ReturnDetails.ReturnID != Guid.Empty) _return.ReturnID = _ReturnDetails.ReturnID;
            if (_ReturnDetails.SKUNumber != null) _return.SKUNumber = _ReturnDetails.SKUNumber;
            if (_ReturnDetails.ProductName != null) _return.ProductName = _ReturnDetails.ProductName;
            if (_ReturnDetails.TCLCOD_0 != null) _return.TCLCOD_0 = _ReturnDetails.TCLCOD_0;
            _return.DeliveredQty = (int)_ReturnDetails.DeliveredQty;
            _return.ExpectedQty = (int)_ReturnDetails.ExpectedQty;
            _return.ReturnQty = (int)_ReturnDetails.ReturnQty;
            _return.ProductStatus = (int)_ReturnDetails.ProductStatus;
            if (_ReturnDetails.CreatedBy != Guid.Empty) _return.CreatedBy = (Guid)_ReturnDetails.CreatedBy;
            if (_ReturnDetails.UpdatedBy != Guid.Empty) _return.UpdatedBy = (Guid)_ReturnDetails.UpdatedBy;
            if (_ReturnDetails.CreatedDate != null) _return.CreatedDate = (DateTime)_ReturnDetails.CreatedDate;
            if (_ReturnDetails.UpadatedDate != null) _return.UpadatedDate = (DateTime)_ReturnDetails.UpadatedDate;
            _return.RGADROWID = _ReturnDetails.RGADROWID;
            return _return;

        }
        public ReturnDetail()
        {

        }
    }
}
