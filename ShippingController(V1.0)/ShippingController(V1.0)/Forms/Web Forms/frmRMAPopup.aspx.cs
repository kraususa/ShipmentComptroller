using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAPopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    if (RadioButtonList1.SelectedIndex==-1)
        //    {
        //        lblMessage.Text = "Please select appropriate Radiobutton";
        //    }
        //    else
        //    {
        //        if(RadioButtonList1.SelectedIndex==0)
        //        {                  
                   
        //        }
        //        else if(RadioButtonList1.SelectedIndex==1)
        //        {

        //        }
        //        else if (RadioButtonList1.SelectedIndex == 2)
        //        {
        //            String RowId = txtRMAwith.Text.Trim();
        //            Response.Redirect("~/Forms/Web Forms/frmSRNumber.aspx?RGAROWID=" + RowId);
        //        }
        //    }
        //}

        protected void txtRMAwith_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedItem != null)
            {
                string rbd = RadioButtonList1.SelectedItem.Value;
                if (rbd == "wthpo")
                {
                    Response.Redirect(@"~\Forms\Web Forms\frmRMAEnterWithPO.aspx");
                }
                else if (rbd == "wthotpo")
                {
                    // Server.Transfer(@"~\Forms\Web Forms\frmRMAEnterWithPO.aspx");

                }
                else if (rbd == "wthsr")
                {
                   // Response.Redirect(@"~\Forms\Web Forms\frmRMAEnterWithSR.aspx");
                    String srnumber = txtRMAwith.Text.Trim();
                    Response.Redirect("~/Forms/Web Forms/frmSRNumber.aspx?RMANumber=" + srnumber);
                }

            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRMAwith.Visible = true;
            txtRMAwith.Focus();
        }



    }
}