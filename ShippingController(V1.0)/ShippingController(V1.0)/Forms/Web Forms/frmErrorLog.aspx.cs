using PackingClassLibrary.CustomEntity;
using ShippingController_V1._0_.Classes;
using ShippingController_V1._0_.Classes.DisplayEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmErrorLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillErrorLogGrid();
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchLog(string prefixText, int count)
        {
            List<string> lsreturn = new List<string>();
            if (prefixText == "")
            {
                prefixText = "SH";
            }
           List<cstErrorLog> lsErrorLog = cGlobal.call.GetErrorLog();
                List<cstDspErrorLog> LsNewErroe = new List<cstDspErrorLog>();
                foreach (var Erroritem in lsErrorLog)
                {
                    cstDspErrorLog _error = new cstDspErrorLog();
                    _error.ErrorDescription = Erroritem.ErrorDesc;
                    _error.ErrorLocation = Erroritem.ErrorLocation;
                    _error.ErrorDate = Erroritem.ErrorTime;
                    _error.ErrorID = Erroritem.ErrorLogID;
                    _error.UserName = "--";
                    if (Erroritem.ErrorLogID != Guid.Empty)
                    {
                        try
                        { _error.UserName = cGlobal.call.GetSelcetedUserMaster(Erroritem.UserID).SingleOrDefault(o => o.UserID == Erroritem.UserID).UserFullName; }
                        catch (Exception) { }

                    }
                    LsNewErroe.Add(_error);
                }
            foreach (var packing in LsNewErroe)
            {
                string ctext = packing.ErrorID  + " | " + packing.UserName+ " | " + packing.ErrorDescription  + " | " + packing.ErrorDate;

                if (ctext.Contains(prefixText))
                {
                    lsreturn.Add(ctext);
                }
            }
            return lsreturn;
        }

        public void fillErrorLogGrid()
        {
            try
            {
                List<cstErrorLog> lsErrorLog = cGlobal.call.GetErrorLog();
                List<cstDspErrorLog> LsNewErroe = new List<cstDspErrorLog>();
                foreach (var Erroritem in lsErrorLog)
                {
                    cstDspErrorLog _error = new cstDspErrorLog();
                    _error.ErrorDescription = Erroritem.ErrorDesc;
                    _error.ErrorLocation = Erroritem.ErrorLocation;
                    _error.ErrorDate = Erroritem.ErrorTime; 
                    _error.ErrorID = Erroritem.ErrorLogID;
                    _error.UserName = "--";
                    if (Erroritem.UserID != Guid.Empty)
                    {
                        try
                        { _error.UserName = cGlobal.call.GetSelcetedUserMaster(Erroritem.UserID).SingleOrDefault(o => o.UserID == Erroritem.UserID).UserFullName; }
                        catch (Exception) { }

                    }
                    LsNewErroe.Add(_error);
                   
                }
                gvErrorInformation.DataSource = LsNewErroe;
                gvErrorInformation.DataBind();
            }
            catch (Exception)
            {}
        }

        protected void txtSearchLog_TextChanged(object sender, EventArgs e)
        {
            txtSearchLog.Focus();

            try
            {
                String SearchText = txtSearchLog.Text;
                String[] Part = SearchText.Split(new char[] { '|' });

                if (Part[0].ToString() != "" || Part[0].ToString() != null)
                {
                    Guid Rowid ;
                    Guid.TryParse(Part[0].ToString(), out Rowid);
                    List<cstErrorLog> lsErrorLog = cGlobal.call.GetErrorLog();

                    cstErrorLog _Err = lsErrorLog.SingleOrDefault(i => i.ErrorLogID == Rowid);
                    cstDspErrorLog _error = new cstDspErrorLog();
                    _error.ErrorDescription = _Err.ErrorDesc;
                    _error.ErrorLocation = _Err.ErrorLocation;
                    _error.ErrorDate = _Err.ErrorTime;
                    _error.ErrorID = _Err.ErrorLogID;
                    _error.UserName = "--";
                    if (_Err.UserID != Guid.Empty)
                    {
                        try
                        { _error.UserName = cGlobal.call.GetSelcetedUserMaster(_Err.UserID).SingleOrDefault(o => o.UserID == _Err.UserID).UserFullName; }
                        catch (Exception) { }

                    }
                    List<cstDspErrorLog> lsDataSource = new List<cstDspErrorLog>();
                    lsDataSource.Add(_error);
                    gvErrorInformation.DataSource = lsDataSource;
                    gvErrorInformation.DataBind();
                    txtSearchLog.Text = "";
                }
            }
            catch (Exception)
            { }
            
        }
    }
}