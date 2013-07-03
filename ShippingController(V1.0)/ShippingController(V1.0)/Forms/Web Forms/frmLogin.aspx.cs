using PackingClassLibrary;
using PackingClassLibrary.CustomEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmLogin : System.Web.UI.Page
    {
        smController call = new smController(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                List<cstUserMasterTbl> lsUserInfo = call.GetSelcetedUserMaster(txtUserName.Text.ToString());
                if (lsUserInfo.Count>0)
                {
                    Session["UserFullName"] = lsUserInfo[0].UserFullName;
                    Session["UserID"] = lsUserInfo[0].UserID;
                    Session["UserName"] = lsUserInfo[0].UserName;
                    String Password = lsUserInfo[0].Password.ToString();
                    if (String.Compare(Password,txtPassword.Text) ==0)
                    {
                        Server.Transfer(@"~\Forms\Web Forms\frmHomePage.aspx");
                    }
                   
                }
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('User Name Password not match');", true);


            }
            catch (Exception)
            {
                
            }
           
        }
    }
}