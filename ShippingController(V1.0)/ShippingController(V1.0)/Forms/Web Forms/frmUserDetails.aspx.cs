using PackingClassLibrary.CustomEntity;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmUserDetails : System.Web.UI.Page
    {
        public static Guid UpdateUserID = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _fillRoleDDL();
                _fillUserInformationGridView(Obj.call.GetUserInfoList());
            }
        }

        /// <summary>
        /// Shows User Information list to Grid view.
        /// </summary>
        /// <param name="lsUserMaster">User Information list</param>
        private void _fillUserInformationGridView(List<cstUserMasterTbl> lsUserMaster)
        {
            try
            {
                List<cstUserMasterTbl> _lsUserMaseterAll = lsUserMaster;
                gvUserInformation.DataSource = _lsUserMaseterAll;
                gvUserInformation.DataBind();
            }
            catch (Exception)
            { }
        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text != "")
            {
                model_UserFilter.UserName = txtUserName.Text;
            }
        }

        protected void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddress.Text == "" && txtUserName.Text == "" && txtUserFullName.Text == "" && txtJoiningDateTo.Text == "" && txtJoiningDateTo.Text == "" && txtRoleName.Text == "")
                {
                    _fillUserInformationGridView(Obj.call.GetUserInfoList());
                }
                else
                {
                    _fillUserInformationGridView(model_UserFilter.GetUserInfo());
                }
            }
            catch (Exception)
            { }
        }

        protected void txtUserFullName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUserFullName.Text != "")
                {
                    model_UserFilter.UserFullName = txtUserFullName.Text;
                }
                else
                {
                    model_UserFilter.IsFullNameFilterOn = false;
                }
            }
            catch (Exception)
            { }
        }

        protected void txtRoleName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRoleName.Text != "")
                {
                    model_UserFilter.Role = txtRoleName.Text;
                }
                else
                {
                    model_UserFilter.IsRoleFilterOn = false;
                }
            }
            catch (Exception)
            { }
        }

        protected void txtJoiningDateFrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtJoiningDateFrom.Text != "")
                {
                    model_UserFilter.JoiningFromDate = Convert.ToDateTime(txtJoiningDateFrom.Text);
                }
                else
                {
                    model_UserFilter.IsDateFilterOn = false;
                }
            }
            catch (Exception)
            {
            }
        }

        protected void txtJoiningDateTo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtJoiningDateTo.Text != "")
                {
                    model_UserFilter.JoiningToDate = Convert.ToDateTime(txtJoiningDateTo.Text);
                }
                else
                {
                    model_UserFilter.IsDateFilterOn = false;
                }
            }
            catch (Exception)
            {
            }
        }

        protected void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress.Text != "")
            {
                model_UserFilter.Address = txtAddress.Text;
            }
            else
            {
                model_UserFilter.IsAddressFilterOn = false;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            _fillUserInformationGridView(Obj.call.GetUserInfoList());
            _clearAllTextBox();
        }

        /// <summary>
        /// Clear All fields from the Form.
        /// </summary>
        private void _clearAllTextBox()
        {
            try
            {
                txtUserName.Text = "";
                txtUserFullName.Text = "";
                txtRoleName.Text = "";
                txtJoiningDateFrom.Text = "";
                txtJoiningDateTo.Text = "";
                txtAddress.Text = "";
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Fill Roles in Drop Down List
        /// </summary>
        private void _fillRoleDDL()
        {
            try
            {
                List<cstRoleTbl> _lsRoles = Obj.call.GetRole();
                ddlRoles.DataValueField = "RoleId";
                ddlRoles.DataTextField = "Name";
                ddlRoles.DataSource = _lsRoles;
                ddlRoles.DataBind();
            }
            catch (Exception)
            {}
        }

        /// <summary>
        /// clear Update information of user
        /// </summary>
        private void _clearUpdateInfo()
        {
            txtEaddress.Text = "";
            txtEUserFName.Text = "";
            txtEUserName.Text = "";
            txtEPass.Text = "";
            txtEJoiningDate.Text = "";
            ddlRoles.SelectedIndex = -1;
            UpdateUserID = Guid.Empty;
        }

        /// <summary>
        /// User Infrmation Grid View selected
        /// </summary>
        protected void gvUserInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            _clearUpdateInfo();
            
            Guid.TryParse(gvUserInformation.SelectedRow.Cells[1].Text, out UpdateUserID);
            if (UpdateUserID != Guid.Empty)
            {
                cstUserMasterTbl _userInfo = Obj.call.GetSelcetedUserMaster(UpdateUserID).First();
                txtEaddress.Text = _userInfo.UserAddress;
                txtEUserFName.Text = _userInfo.UserFullName;
                txtEUserName.Text = _userInfo.UserName;
                txtEPass.Text = _userInfo.Password;
                txtEJoiningDate.Text = _userInfo.JoiningDate.ToString("MMM dd, yyyy");
                ddlRoles.SelectedValue = _userInfo.Role.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('User Information not found');", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            _clearUpdateInfo();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEaddress.Text != "" && txtEUserFName.Text != "" && txtEUserName.Text != "" && txtEPass.Text != "" && txtEJoiningDate.Text != "" && UpdateUserID != Guid.Empty)
                {
                    List<cstUserMasterTbl> _lsUserMaster = new List<cstUserMasterTbl>();
                    cstUserMasterTbl _userInfo = Obj.call.GetSelcetedUserMaster(UpdateUserID).First();
                    _userInfo.UserAddress = txtEaddress.Text;
                    _userInfo.UserFullName = txtEUserFName.Text;
                    _userInfo.UserName = txtEUserName.Text;
                    _userInfo.Password = txtEPass.Text;
                    _userInfo.JoiningDate = Convert.ToDateTime(txtEJoiningDate.Text);
                    Guid RoleID;
                    Guid.TryParse(ddlRoles.SelectedValue.ToString(), out RoleID);
                    _userInfo.Role = RoleID;
                    _lsUserMaster.Add(_userInfo);
                    Obj.call.SetUserMaster(_lsUserMaster, UpdateUserID);
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('Record Updated Successfully!');", true);
                    _clearUpdateInfo();
                    _clearAllTextBox();
                    _fillUserInformationGridView(Obj.call.GetUserInfoList());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "alert('Please select user information to update.');", true);
                }
            }
            catch (Exception)
            { }
        }

    }
}