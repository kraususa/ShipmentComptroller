using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReturnDetails
    {
        public List<ReturnDetail> GetreturnDetailByretrnID(Guid RetunID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.GetreturnDetailByretrnID(RetunID)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new ReturnDetail(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsreturn;
        }

        public List<ReturnDetail> GetreturnDetailByRetundetailID(Guid RetundetailID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.GetreturnDetailByRetundetailID(RetundetailID)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new ReturnDetail(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsreturn;
        }

        public List<ReturnDetail> GetreturnDetailByRGADROWID(string RGADROWID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.GetreturnDetailByRGADROWID(RGADROWID)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new ReturnDetail(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsreturn;
        }

        public List<ReturnDetail> GetreturnDetailByRGAROWID(string RGAROWID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.GetreturnDetailByRGAROWID(RGAROWID)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new ReturnDetail(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsreturn;
        }
    }
}
