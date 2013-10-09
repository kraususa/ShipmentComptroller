using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingController_V1._0_.Models;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRoles : System.Web.UI.Page
    {
        /// <summary>
        /// Model Object
        /// </summary>
        modelRoles MRole = new modelRoles();

        protected void Page_Load(object sender, EventArgs e)
        {

            _fillGvRoles();
        }

        /// <summary>
        /// Fill Grid View Of Role information.
        /// </summary>
        private void _fillGvRoles()
        {
            var roles = from rol in Obj.call.GetRole()
                        select new
                        {
                            rol.RoleId,
                            rol.Name,
                            permission = MRole.ActionString(rol.Action)
                        };

            gvUserInformation.DataSource = roles;
            gvUserInformation.DataBind();


        }
    }
}