using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary.Commands.SMcommands.RGA;

namespace ShippingController_V1._0_.Forms.Master_Forms
{
    public partial class TestUser : System.Web.UI.MasterPage
    {
        cmdReturnDetails _ReturnDetails = new cmdReturnDetails();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblUserNameTop.Text = "Admin";//Session["UserFullName"].ToString();
                }
                catch (Exception)
                {
                    Response.Redirect("../Web%20Forms/frmLogin.aspx");
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('Session expired. Please Login again to continue');", true);
                }
            }
        }

        public void Showrt(Object sender, EventArgs e)
        {

        }
        protected void TreeView1_SelectedNodeChanged1(object sender, EventArgs e)
        {
            //try
            //{
            //    //tvMenu.SelectedNode. = System.Drawing.Color.Red;

            //    tvMenu.SelectedNodeStyle.BackColor = System.Drawing.Color.Red;
            //    //TreeView Tr = (TreeView)sender;
            //    //Tr.SelectedNode.Selected = true;
            //    Response.Redirect(tvMenu.SelectedValue);



            //   // Server.Transfer(tvMenu.SelectedValue);
            //}
            //catch (Exception)
            //{}
        }

        protected void TreeView1_Load(object sender, EventArgs e)
        {

        }

        protected void Logout_buttonClicked(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmLogin.aspx");
            //Server.Transfer("~/Forms/Web Forms/frmLogin.aspx");
        }



        protected void btnYesPO_Click(object sender, EventArgs e)
        {
            String po = txtPONumber.Text.Trim();
            if (_ReturnDetails.IsPONumberAlreadyPresent(po))
            {
                mpeForPresentedPO.Show();
            }
            else
            {
                Response.Redirect("~/Forms/Web Forms/frmRMAEnterWithPO.aspx?RMAPO=" + po);
            }
        }


        protected void btnYesSR_Click(object sender, EventArgs e)
        {
            String srnumber = txtSRNumber.Text.Trim();
            if (_ReturnDetails.IsSRNumberAlreadyPresent(srnumber))
            {
                mpeForPresentedSR.Show();
            }
            else
            {
                Response.Redirect("~/Forms/Web Forms/frmSRNumber.aspx?RMANumber=" + srnumber);
            }
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                //case "Home":
                //    Response.Redirect("~/Forms/Web Forms/frmHomePage.aspx");
                case "Add RMA with PO":
                    // Response.Write("<script>window.open('Default.aspx', 'hello', 'width=700,height=400,scrollbars=yes');</script>");
                    mpeForPO.Show();
                    return;
                case "Add RMA without PO":
                    return;
                case "Add RMA with SR":
                    mpeForSR.Show();
                    return;
            }
        }

    }
}