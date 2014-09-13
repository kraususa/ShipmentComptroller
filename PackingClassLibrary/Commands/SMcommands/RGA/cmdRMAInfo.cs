using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
    class cmdRMAInfo
    {
        public List<RMAInfo> ReturnDetailBySRNumber(string SRNumber)
        {
            List<RMAInfo> _lsreturn = new List<RMAInfo>();
            try
            {
                Service.GetRMA.RMAInfoBySRNumber(SRNumber);
                var v = from ls in Service.GetRMA.RMAInfoBySRNumber(SRNumber)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new RMAInfo(Ritem));
                }
                //var v = from ls in Service.GetRMA.ReturnDetailByRGADROWID(RMANumber)
                //        select ls;

                //foreach (var Ritem in v)
                //{
                //    _lsreturn.Add(new ReturnDetail(Ritem));
                //}

            }
            catch (Exception)
            { }
            return _lsreturn;
        }

        public List<RMAInfo> ReturnDetailByPonumber(string POnumber)
        {
            List<RMAInfo> _lsreturn = new List<RMAInfo>();
            try
            {
                var v = from ls in Service.GetRMA.RMAInfoByPONumber(POnumber)
                        select ls;

                foreach (var Ritem in v)
                {
                    _lsreturn.Add(new RMAInfo(Ritem));
                }
            }
            catch (Exception)
            { }
            return _lsreturn;
        }


    }
}
