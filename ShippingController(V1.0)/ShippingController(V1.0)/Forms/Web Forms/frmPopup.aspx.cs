using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ShippingController_V1._0_.Models;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmPopup : System.Web.UI.Page
    {
         Models.modelReturn _newRMA = new Models.modelReturn();
        string _reasons;
        int count;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String s = Request.QueryString["Category"];


                //string rt = t1.Text;
                FilldgReasons(s);
            }
        }
         public void FilldgReasons(String cat)
        {
            chkreasons.DataSource = _newRMA.GetReasons(cat);
            chkreasons.DataTextField = "Reason1";
            chkreasons.DataValueField = "ReasonID";
            chkreasons.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            count = 0;
            foreach (ListItem li in chkreasons.Items)
            {
                if (li.Selected)
                {
                    _reasons += li.Value + "#";
                    count++;
                }
            }

         // ScriptManager.RegisterStartupScript("close","<script language=javascript>window.opener.document.getElementById('Label1').value = '"+_reasons+"';self.close();</script>");

  

           // ClientScript.RegisterStartupScript("Close",.RegisterStartupScript66("close", "<script language=javascript>window.opener.document.getElementById('Label1').value = '" + _reasons + "';self.close();</script>");


        //    for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (ViewState["SKU"].ToString()== ((TextBox)gvReturnDetails.Rows[i].FindControl("txtsku")).Text)
        //            {
        //                TextBox category = (TextBox)gvReturnDetails.Rows[i].FindControl("txtskureasons");
        //                category.Text = _reasons;

        //                LinkButton t = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtreasons");
        //                t.Text = count + " " + "Reasons";
        //            } 
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    pnModelPopup.Visible = false;
        //}
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            count = 0;
            foreach (ListItem li in chkreasons.Items)
            {
                if (li.Selected)
                {
                _reasons += li.Value + "#";
                count++;
                }
            }
            Obj._popupValue.ReasnValue = _reasons;
            string script = "self.close();</script>";
          //  Page.RegisterStartupScript("close", "<script language=javascript>window.opener.document.getElementById('Label1').value = '" + _reasons + "';self.close();</script>");
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "Close", script, true);// RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.document.getElementById('txtPopResult').value = '" + _reasons + "';self.close();</script>");

           // Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.document.getElementById('txtvendernumber').Value = '" + _reasons + "';self.close();</script>");

            chkreasons.ClearSelection();
        }

      
    }
}