using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Commands.SMcommands.RGA;
using ShippingController_V1._0_.Views;
using PackingClassLibrary;
using PackingClassLibrary.Commands;
using PackingClassLibrary.CustomEntity;
using System.Data;
using ShippingController_V1._0_.Models;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Threading;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAEnter : System.Web.UI.Page
    {
        #region Declaration

       //Object of ModelReturn.
        Models.modelReturn _newRMA = new Models.modelReturn();

        //call smController Object. 
        smController call = new smController();

        //ReturnDetailID Guid.
        Guid ReturnDetailsID;

        /// <summary>
        ///list of userMaster to store User information.
        /// </summary>
        List<cstUserMasterTbl> lsUserInfo = new List<cstUserMasterTbl>();

        /// <summary>
        /// datatable to Store the All information of ReturnDetails.
        /// viturally added in gridview. 
        /// </summary>
        DataTable dt = new DataTable();

        //Reason String.
        string _reasons;

        //Count Reasons.
        int count;

        //Textbox Object of txtSkuID.
        TextBox txtSKUID;

        //Thread
        public static Thread CopyThread;

        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // List Of return Reasons.
                List<Reason> lsReturn = _newRMA.GetReasons();

                //Create Object Of Reason.
                //Fill Dropdown list Of OtherReason.
                Reason re = new Reason();
                re.ReasonID = Guid.NewGuid();
                re.Reason1 = "--Select--";

                lsReturn.Insert(0, re);

               ddlotherreasons.DataTextField = "Reason1";
               ddlotherreasons.DataValueField = "ReasonID";
               ddlotherreasons.DataSource = lsReturn;
               ddlotherreasons.DataBind();

               //string user = Session["UName"].ToString();
                //fill grid method call.
               fillGrid();

                //set Retquest date to txtrequestdate.
               txtrequestdate.Text = DateTime.UtcNow.Date.ToString("MMM dd, yyyy");

               Obj._popupValue.PropertyChanged += _popupValue_PropertyChanged;
               Obj._popupValue.ReasnValue = "";
               txtSKUID = new TextBox();
            }
        }

        /// <summary>
        /// Fill Grid method Fills GridView.
        /// using Datatable.
        /// </summary>
        public void fillGrid()
        {

            dt.Columns.Add("SKU");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Category");
            dt.Columns.Add("Reasons");
            dt.Columns.Add("SKUID");
            dt.Columns.Add("ImageName");

            DataRow dr = dt.NewRow();

            dr[0] = "";
            dr[1] = "";
            dr[2] = "1";
            dr[3] = "";
            dr[4] = "Reasons";
            dr[5] = "";
            dr[6] = "";

            dt.Rows.Add(dr);

            gvReturnDetails.DataSource = dt;
            gvReturnDetails.DataBind();
        
        }

        void _popupValue_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Obj._popupValue.ReasnValue != "")
            {
                if (Obj._ReasonList.SingleOrDefault(i => i.ID == Obj.RowID) == null)
                {
                    Views.ReasonList _Reason = new ReasonList();
                    _Reason.ID = Obj.RowID;
                    _Reason.ReasonString = Obj._popupValue.ReasnValue;

                    Obj._ReasonList.Add(_Reason);
                }
                else
                {
                    Obj._ReasonList.RemoveAt(Obj._ReasonList.IndexOf(Obj._ReasonList.SingleOrDefault(i => i.ID == Obj.RowID)));
                    Views.ReasonList _Reason = new ReasonList();
                    _Reason.ID = Obj.RowID;
                    _Reason.ReasonString = Obj._popupValue.ReasnValue;

                    Obj._ReasonList.Add(_Reason);
                }
            }

        }
        
        /// <summary>
        /// String of Return Reason.
        /// </summary>
        /// <returns>
        /// Return string Of Reasons.
        /// </returns>
        private String ReturnReasons()
        {
            String _ReturnReason = "";

            if (chkitemdamaged.Checked == true) _ReturnReason = _ReturnReason + chkitemdamaged.Text;

            if (chkitemdifferent.Checked == true) _ReturnReason = _ReturnReason + chkitemdifferent.Text;

            if (chkduplicate.Checked == true) _ReturnReason = _ReturnReason + chkduplicate.Text;

            if (chkitemordered.Checked == true) _ReturnReason = _ReturnReason + chkitemordered.Text;

            if (chknotsatisfied.Checked == true) _ReturnReason = _ReturnReason + chknotsatisfied.Text;

            if (chkwrongitem.Checked == true) _ReturnReason = _ReturnReason + chkwrongitem.Text;

            _ReturnReason += txtotherreasons.Text;

            return _ReturnReason;

        }
        
        /// <summary>
        /// all Information save in all tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Byte Status = Convert.ToByte(ddlstatus.SelectedValue);
            Byte Decision = Convert.ToByte(ddldecision.SelectedValue);

            //List of Return Information.
            List<Return> _lsreturn = new List<Return>();
            Return ret = new Return();
            ret.RMANumber = "";
            ret.VendoeName = txtvendername.Text;
            ret.VendorNumber = txtvendernumber.Text;
            ret.ReturnDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtrequestdate.Text), "Eastern Standard Time");
            ret.PONumber = txtponumber.Text;
            ret.CustomerName1 = txtcustomername.Text;
            ret.Address1 = txtcustomeraddress.Text;
            ret.City = txtcity.Text;
            ret.Country = txtcountry.Text;
            ret.ZipCode = txtzipcode.Text;
            ret.State = txtstate.Text;
            ret.ScannedDate = DateTime.UtcNow;
            ret.ExpirationDate = DateTime.UtcNow.AddDays(60);

            _lsreturn.Add(ret);
                
            //user Name Is Stored in sessoin.
            lsUserInfo = call.GetSelcetedUserMaster(Session["UName"].ToString());

            //Save Return information of New RMA.
            Guid ReturnID = _newRMA.SetReturnTbl(_lsreturn, ReturnReasons(), Status, Decision, lsUserInfo[0].UserID);

            // Display New RMA Number Means RGA number.
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('RMA number for this return is :" + _newRMA.GetNewROWID(ReturnID) + "');", true);

            //Save return Details from The Gridview. 
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                string  sku = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtsku")).Text;
                string  productname = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname")).Text;
                string  quantity = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity")).Text;
                string category = productcategory(sku, 1);

                if (sku != "" && productname != "")
                {
                    ReturnDetailsID = _newRMA.SetReturnDetailTbl(ReturnID, sku, productname, 0, 0, Convert.ToInt32(quantity), category, lsUserInfo[0].UserID);
                }

                if (Obj._ReasonList.Count !=0)
                {
                    string SkuReasons = Obj._ReasonList.SingleOrDefault(j => j.ID == i).ReasonString;
                    if (SkuReasons != "" && SkuReasons != null)
                    {
                        foreach (Guid Ritem in (SkuReasons.GetGuid()))
                        {
                            _newRMA.SetSkuReasons(Ritem, ReturnDetailsID);
                        }
                    }
                }

                string imglist = ((Label)gvReturnDetails.Rows[i].FindControl("lblImagesName")).Text;

                foreach (var item in imglist.Split(new char[] { '\n' }))
                {
                    if(item!=null && item!="")
                    {

                     String NameImage =System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString() +"\\" + item.ToString() ;
                       
                     Guid ImageID = _newRMA.SetReturnedImages(Guid.NewGuid(), ReturnDetailsID, NameImage, lsUserInfo[0].UserID);
                    }
                }
            }
            clear();
        }

        //Set the textbox value when dropdownlist of otherreason selectedindexChanged.
        protected void ddlotherreasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlotherreasons.SelectedIndex == 0)
            {
                txtotherreasons.Text = "";
            }
            else
            {
                txtotherreasons.Text = ddlotherreasons.SelectedItem.Text;
            }
        }

        //set the value of Product Name and Category when txtSKUtextChanged.
        protected void txtSKU_TextChanged(object sender, EventArgs e)
        {
            //find the current row by using GridViewRow.
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
            //find The txtSku textbox.
            TextBox t = (TextBox)currentRow.FindControl("txtsku");
            string rt = t.Text;

            //Find the textBox Productname in current Row
            TextBox txt = (TextBox)currentRow.FindControl("txtproductname");
            txt.Text = productcategory(rt, 0);

            //find the txtcategory textbox In currentRow Which is Visible False.
            TextBox cat = (TextBox)currentRow.FindControl("txtcategory");
            cat.Text = productcategory(rt, 1);

            //find the txtquantity textbox In currentRow.
            TextBox txt1 = (TextBox)currentRow.FindControl("txtquantity");
            txt1.Focus();
        }

        //This Button Click Is used to Add the new row in GridView.
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            String RowString = "";

            try
            {
                RowString = (gvReturnDetails.Rows[Convert.ToInt32(gvReturnDetails.Rows.Count - 1)].Cells[0].FindControl("txtSKU") as TextBox).Text.Trim();
            }
            catch (Exception)
            {
                RowString = "New ROW";
            }
            if (RowString != "")
            {
                dt.Columns.Add("SKU");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Category");
                dt.Columns.Add("Reasons");
                dt.Columns.Add("SKUID");
                dt.Columns.Add("ImageName");

                for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr1 = dt.NewRow();
                        TextBox sku = (TextBox)gvReturnDetails.Rows[i].FindControl("txtsku");
                        TextBox productname = (TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname");
                        TextBox quantity = (TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity");
                        TextBox category = (TextBox)gvReturnDetails.Rows[i].FindControl("txtcategory");

                        LinkButton reasons = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtreasons");
                        TextBox skuID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtskureasons");
                        Label lblimages = (Label)gvReturnDetails.Rows[i].FindControl("lblImagesName");

                        dr1[0] = sku.Text;
                        dr1[1] = productname.Text;
                        dr1[2] = quantity.Text;
                        dr1[3] = category.Text;
                        dr1[4] = reasons.Text;
                        dr1[5] = skuID.Text;
                        dr1[6] = lblimages.Text;

                        dt.Rows.Add(dr1);
                    }
                    catch (Exception)
                    {
                    }
                }
                DataRow dr = dt.NewRow();

                dr[0] = "";
                dr[1] = "";
                dr[2] = "1";
                dr[3] = "";
                dr[4] = "Reasons";
                dr[5] = "";
                dr[6] = "";

                dt.Rows.Add(dr);

                gvReturnDetails.DataSource = dt;
                gvReturnDetails.DataBind();

                dt.Clear();
            }
        }

        /// <summary>
        /// Split Product Category from The Sku String. 
        /// </summary>
        /// <param name="sku">
        /// String SKU for Split.
        /// </param>
        /// <param name="flag">
        /// 
        /// </param>
        /// <returns>
        /// Return the Category.
        /// </returns>
        public string productcategory(string sku,int flag)
        {
            string _productname = "";
            List<string> lsTrackingTbl = Obj.call._skulist(sku);
            try
            {
                if (flag == 0)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _productname = TrackItm.ToString().Split(new char[] { '#' })[1];
                    }
                }
                else if (flag == 1)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _productname = TrackItm.ToString().Split(new char[] { '#' })[2];
                    }
                }
            }
            catch (Exception)
            {}
            return _productname;
        }

        //This button Click Redirect to the home Page.
        protected void btncancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmHomePage.aspx");
        }

        // Clear All the textBoxes,Grid View And The DropDown List.
        public void clear()
        {
            txtcity.Text = "";
            txtcountry.Text = "";
            txtcustomeraddress.Text = "";
            txtcustomername.Text="";
            txtotherreasons.Text = "";
            txtponumber.Text = "";
            txtrmanumber.Text = "";
            txtstate.Text = "";
            txtvendernumber.Text = "";
            txtvendername.Text="";
            txtzipcode.Text = "";
            ddldecision.SelectedIndex = 0;
            ddlotherreasons.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            chkduplicate.Checked = false;
            chkitemdamaged.Checked = false;
            chkitemdifferent.Checked = false;
            chkitemordered.Checked = false;
            chknotsatisfied.Checked = false;
            chkwrongitem.Checked = false;

            dt.Columns.Add("SKU");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Category");
            dt.Columns.Add("Reasons");
            dt.Columns.Add("SKUID");
            dt.Columns.Add("ImageName");

            DataRow dr = dt.NewRow();

            dr[0] = "";
            dr[1] = "";
            dr[2] = "1";
            dr[3] = "";
            dr[4] = "Reasons";
            dr[5] = "";
            dr[6] = "";

            dt.Rows.Add(dr);

            gvReturnDetails.DataSource = dt;
            gvReturnDetails.DataBind();

            Obj._ReasonList = new List<ReasonList>();
        }

        //link button Click to open New Popup of And check the reason in that popup window.
        protected void txtreasons_Click(object sender, EventArgs e)
        {

            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;
            LinkButton t = (LinkButton)currentRow.FindControl("txtreasons");

            TextBox sku = (TextBox)currentRow.FindControl("txtsku");
            Obj.RowID= currentRow.RowIndex;

            TextBox reasonID = (TextBox)currentRow.FindControl("txtskureasons");

            TextBox t1 = (TextBox)currentRow.FindControl("txtcategory");
            string rt = t1.Text;
            FilldgReasons(rt);
            string url = "frmPopup.aspx?Category=" + rt + "";
           
           
           string s = "window.open('" + url + "', 'popup_window', 'width=500,height=300,left=300,top=300,resizable=yes');";
           ScriptManager.RegisterStartupScript(this, Page.GetType(), "Script", s, true);
        }

        /// <summary>
        /// Fill Reasons in CheckBoxList.
        /// </summary>
        /// <param name="cat">
        /// String Reasons as Parameter.
        /// </param>
        public void FilldgReasons(String cat)
        {
            chkreasons.DataSource = _newRMA.GetReasons(cat);
            chkreasons.DataTextField = "Reason1";
            chkreasons.DataValueField = "ReasonID";
            chkreasons.DataBind();
        }

        //add Button Click used for to add reason which is Checked.
        //and count the Reasons.
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
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                try
                {
                    if (ViewState["rowindex"].ToString() == ((TextBox)gvReturnDetails.Rows[i].FindControl("txtsku")).Text)
                    {
                        TextBox category = (TextBox)gvReturnDetails.Rows[i].FindControl("txtskureasons");
                        category.Text = _reasons;

                        LinkButton t = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtreasons");
                        t.Text = count + " " + "Reasons";
                    } 
                }
                catch (Exception)
                {
                }
            }
            pnModelPopup.Visible = false;
        }

        //cancle Button Click to Close popup window.
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnModelPopup.Visible = false;
        }

        //Upload image in the C:\Images folder.
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string updir = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString();
            GridViewRow gvRow = (sender as Button).NamingContainer as GridViewRow;
            FileUpload fileUpload = gvRow.FindControl("FileUpload1") as FileUpload;

            fileUpload.SaveAs(@"C:\Images\" + fileUpload.FileName);
            //method to upload file to the FTP server.
             ExtensionMethods.Upload(@"ftp://fileshare.kraususa.com", "rgauser", "rgaICG2014", "C:\\Images\\" + fileUpload.FileName, fileUpload.FileBytes);
            //delete file from the local.
             File.Delete(@"C:\Images\" + fileUpload.FileName);

            Label lbl = gvRow.FindControl("lblImagesName") as Label;
            lbl.Text = lbl.Text + "\n" + fileUpload.FileName;
        }
        
        // obtains user token
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            GridViewRow gvRow = (sender as FileUpload).NamingContainer as GridViewRow;
            Button btnupload = gvRow.FindControl("btnUpdate") as Button;

            btnupload.Enabled = true;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_valuefor_delete"];
            if (confirmValue == "Yes")
            {
                GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;

                dt.Columns.Add("SKU");
                dt.Columns.Add("ProductName");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Category");
                dt.Columns.Add("Reasons");
                dt.Columns.Add("SKUID");
                dt.Columns.Add("ImageName");

                for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr1 = dt.NewRow();
                        TextBox sku = (TextBox)gvReturnDetails.Rows[i].FindControl("txtsku");
                        TextBox productname = (TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname");
                        TextBox quantity = (TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity");
                        TextBox category = (TextBox)gvReturnDetails.Rows[i].FindControl("txtcategory");

                        LinkButton reasons = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtreasons");
                        TextBox skuID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtskureasons");
                        Label lblimages = (Label)gvReturnDetails.Rows[i].FindControl("lblImagesName");

                        dr1[0] = sku.Text;
                        dr1[1] = productname.Text;
                        dr1[2] = quantity.Text;
                        dr1[3] = category.Text;
                        dr1[4] = reasons.Text;
                        dr1[5] = skuID.Text;
                        dr1[6] = lblimages.Text;

                        dt.Rows.Add(dr1);
                    }
                    catch (Exception)
                    {
                    }
                }
                DataRow dr = dt.NewRow();

                dr[0] = "";
                dr[1] = "";
                dr[2] = "1";
                dr[3] = "";
                dr[4] = "Reasons";
                dr[5] = "";
                dr[6] = "";

                dt.Rows[gvRow.RowIndex].Delete();

                gvReturnDetails.DataSource = dt;
                gvReturnDetails.DataBind(); 
            }
        }

        protected void txtponumber_TextChanged(object sender, EventArgs e)
        {
            if (txtponumber.Text.Trim() != "")
            {
                List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomer(txtponumber.Text);

                if (lsCustomeronfo.Count > 0)
                {
                    txtponumber.Text = lsCustomeronfo[0].PONumber;
                    txtcustomeraddress.Text = lsCustomeronfo[0].Address1;
                    txtcountry.Text = lsCustomeronfo[0].Country;
                    txtcity.Text = lsCustomeronfo[0].City;
                    txtstate.Text = lsCustomeronfo[0].State;
                    txtzipcode.Text = lsCustomeronfo[0].ZipCode;
                    txtcustomername.Text = lsCustomeronfo[0].CustomerName1;
                }
            }
        }
    }
}