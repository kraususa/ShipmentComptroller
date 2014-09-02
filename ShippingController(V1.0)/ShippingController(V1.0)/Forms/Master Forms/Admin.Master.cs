using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Master_Forms
{
    public partial class Admin : System.Web.UI.MasterPage
    {
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
            try
            {
                //TreeView Tr = (TreeView)sender;
                //Tr.SelectedNode.Selected = true;
                Response.Redirect(tvMenu.SelectedValue);
               // Server.Transfer(tvMenu.SelectedValue);
            }
            catch (Exception)
            {}
        }

        protected void TreeView1_Load(object sender, EventArgs e)
        {
           
        }

        protected void Logout_buttonClicked(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmLogin.aspx");
            //Server.Transfer("~/Forms/Web Forms/frmLogin.aspx");
        }
    }
}