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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillUserInformationGridView(Obj.call.GetUserInfoList());
            }
        }

        /// <summary>
        /// Shows User Information list to Grid view.
        /// </summary>
        /// <param name="lsUserMaster">User Information list</param>
        private void fillUserInformationGridView(List<cstUserMasterTbl> lsUserMaster)
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
                    fillUserInformationGridView(Obj.call.GetUserInfoList());
                }
                else
                {
                    fillUserInformationGridView(model_UserFilter.GetUserInfo());
                }
            }
            catch (Exception)
            { }
        }

        protected void txtUserFullName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != "")
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
            if (txtJoiningDateFrom.Text != "")
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
            fillUserInformationGridView(Obj.call.GetUserInfoList());
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

    }
}