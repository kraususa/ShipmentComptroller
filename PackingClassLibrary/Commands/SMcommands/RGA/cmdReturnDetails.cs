using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;

namespace PackingClassLibrary.Commands.SMcommands.RGA
{
   public class cmdReturnDetails
    {

       public List<ReturnDetail> ReturnDetailAll()
       {
           List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
           try
           {
               var v = from ls in Service.GetRMA.ReturnDetailAll()
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
        public List<ReturnDetail> ReturnDetailByretrnID(Guid RetunID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.ReturnDetailByretrnID(RetunID)
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

        public List<ReturnDetail> ReturnDetailByRetundetailID(Guid RetundetailID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.ReturnDetailByRetundetailID(RetundetailID)
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

        public List<ReturnDetail> ReturnDetailByRGADROWID(string RGADROWID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.ReturnDetailByRGADROWID(RGADROWID)
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

        public List<ReturnDetail> ReturnDetailByRGAROWID(string RGAROWID)
        {
            List<ReturnDetail> _lsreturn = new List<ReturnDetail>();
            try
            {
                var v = from ls in Service.GetRMA.ReturnDetailByRGAROWID(RGAROWID)
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

        #region Set Method

        /// <summary>
        /// Update returndetail Table information.
        /// </summary>
        /// <param name="_lsreturn">
        /// pass return object as parameter.
        /// </param>
        /// <returns>
        /// return Bolean
        /// </returns>
        public Boolean UpdateReturnDetail(ReturnDetail _lsreturndetail)
        {
            Boolean _flag = false;
            try
            {
                _flag = Service.SetRMA.ReturnDetails(_lsreturndetail.ConvertToSaveDTO(_lsreturndetail));
            }
            catch (Exception)
            {
            }
            return _flag;

        }

        #endregion

    }
}
