using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingClassLibrary.CustomEntity.SMEntitys.RGA
{
    public class Reason
    {

        public Guid ReasonID { get; set; }
        public string Reason1 { get; set; }

        public Reason()
        {

        }

        public Reason(SetRGAService.ReasonsDTO _ReasonsDTO)
        {
            if (_ReasonsDTO.ReasonID != null) this.ReasonID = _ReasonsDTO.ReasonID;
            if (_ReasonsDTO.Reason != null) this.Reason1 = _ReasonsDTO.Reason;
        }

        public Reason(GetRGAService.ReasonsDTO _ReasonsDTO)
        {
            if (_ReasonsDTO.ReasonID != null) this.ReasonID = _ReasonsDTO.ReasonID;
            if (_ReasonsDTO.Reason != null) this.Reason1 = _ReasonsDTO.Reason;
        }

        public GetRGAService.ReasonsDTO CopyToGetDTO(Reason _Reason)
        {
            GetRGAService.ReasonsDTO _return = new GetRGAService.ReasonsDTO();
            if (_Reason.ReasonID != null) _return.ReasonID = _Reason.ReasonID;
            if (_Reason.Reason1 != null) _return.Reason = _Reason.Reason1;
            return _return;
        }

        public SetRGAService.ReasonsDTO CopyToSaveDTO(Reason _Reason)
        {
            SetRGAService.ReasonsDTO _return = new SetRGAService.ReasonsDTO();
            if (_Reason.ReasonID != null) _return.ReasonID = _Reason.ReasonID;
            if (_Reason.Reason1 != null) _return.Reason = _Reason.Reason1;
            return _return;
        }
    }
}
