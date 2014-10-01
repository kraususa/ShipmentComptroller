using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
using PackingClassLibrary.Commands.SMcommands.RGA;
using ShippingController_V1._0_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        #region Declarations
        //Create Object of modelRertunUpdate.
        cmdReturn _retn = new cmdReturn();
        modelReaturnUpdate _Update = new modelReaturnUpdate();
       
        Models.modelReturn _newRMA = new Models.modelReturn();

        DataTable dt = new DataTable();

        //  DataTable DT1 = new DataTable();

        DataTable DtReturnReason = new DataTable();

        List<SkuReasonIDSequence> _lsReasonSKU = new List<SkuReasonIDSequence>();

        List<ReturnedSKUPoints> listofstatus = new List<ReturnedSKUPoints>();


        List<Return> listofReturn = new List<Return>();

          List<Return> listofReturnDetails = new List<Return>();
        //  Return retuen = new Return();

        Boolean NonPo = true;

        // List<StatusAndPoints> listofstatusAndPoint = new List<StatusAndPoints>();

        // List<SKUReason> lsSKUReasons = new List<SKUReason>();

              // Adding Flag for Creating Columns for DtReturnReason table
        int flagForDtReturnReason;
        Guid returnid;
        string srnumber;
        #endregion

        private void Page_PreInit(object sender, EventArgs e)
        {
            string user = Session["UserID"].ToString().ToUpper();
            if (Session["UserID"].ToString().ToUpper() == "0DD3CB2D-33B6-431F-9DA0-042F9FF3963B")
            {
                this.MasterPageFile = "~/Forms/Master Forms/Admin.Master";
            }
            else
            {
                this.MasterPageFile = "~/Forms/Master Forms/TestUser.Master";
            }

        }


        #region Page Load Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DtReturnReason.Columns.Add("SKU", typeof(string));
                DtReturnReason.Columns.Add("Reason", typeof(string));
                DtReturnReason.Columns.Add("Reason_Value", typeof(string));
                DtReturnReason.Columns.Add("Points", typeof(int));
                DtReturnReason.Columns.Add("ItemQuantity", typeof(string));
                // DtReturnReason.Columns.Add("ReturnedSKUID", typeof(Guid));
                // DtReturnReason.Columns.Add("ReturnDetailID", typeof(Guid));
                Session["dtsr"] = DtReturnReason;

                List<cSlipInfo> lsprint=new List<cSlipInfo>();

                Session["lsSlipInfo"] = lsprint;


                Session["_lsSlipPrintSKUNumber"] = new List<string>();
                List<RMAComment> rmaComment = new List<RMAComment>();

                Session["rmacommentsr"] = rmaComment;

                List<SkuReasonIDSequence> lsskureason = new List<SkuReasonIDSequence>();
                Session["_lsReasonSKU"] = lsskureason;

                List<cSlipInfo> _lsslipinfo = new List<cSlipInfo>();
                        // Adding Flag for Creating Columns for DtReturnReason table
                Views.Global.flagForDtReturnReason = flagForDtReturnReason;
                        // Getting SRNumber/RMANumber from frmRMAPopup through Query String
                srnumber = Request.QueryString["RMANumber"].ToString();
                //listofReturn = Views.Global.lsGetValuesFromSR;
               
               // List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomer(srnumber);
                //rga = Request.QueryString["RGAROWID"].ToString();

                //       // Creating Columns for DtReturnReason table
                //DtReturnReason.Columns.Add("SKU", typeof(string));
                //DtReturnReason.Columns.Add("Reason", typeof(string));
                //DtReturnReason.Columns.Add("Reason_Value", typeof(string));
                //DtReturnReason.Columns.Add("Points", typeof(int));
                //DtReturnReason.Columns.Add("ItemQuantity", typeof(string));
                //DtReturnReason.Columns.Add("ReturnedSKUID", typeof(Guid));
                //DtReturnReason.Columns.Add("ReturnDetailID", typeof(Guid));

                        // Displaying Rerurn Data for Customer
                Display(Request.QueryString["RMANumber"].ToString());

                        // Fill Return Details in Gridview
                FillReturnDetails(Obj.Rcall.ReturnDetailBySRNumber(Request.QueryString["RMANumber"].ToString()));

               // FillStatus(Request.QueryString["RMANumber"].ToString());



               

                //fillReturnedstatusandpoit();

               
            
               // Session["dtsr"] = DtReturnReason;

               // fillReturnDetailAndStatus();

                List<StatusAndPoints> listofstatusAndPoint = new List<StatusAndPoints>();
                Session["listofstatusAndPoint"] = listofstatusAndPoint;


               // GetLatestUser();

                Obj.ReasonsIDs.PropertyChanged += ReasonsIDs_PropertyChanged;

                Obj._ReasonList = new List<Views.ReasonList>();

                fillddlotherReasons();

               // ShowComments();
            }
          //  ShowComments();
        }
        #endregion

        #region Displaying Status
        public void FillStatus(String RmaNumber)
        {
            try
            {
                btnsubmit.Enabled = true;

                brdDefecttransite.Enabled = true;
                brdManufacturer.Enabled = true;
                brdstatus.Enabled = true;
                brdInstalled.Enabled = true;
                brdItemNew.Enabled = true;

                brdItemNew.Items.FindByText("Yes").Selected = false;
                brdItemNew.Items.FindByText("No").Selected = false;

                brdDefecttransite.Items.FindByText("Yes").Selected = false;
                brdDefecttransite.Items.FindByText("No").Selected = false;

                brdManufacturer.Items.FindByText("Yes").Selected = false;
                brdManufacturer.Items.FindByText("No").Selected = false;

                brdstatus.Items.FindByText("Yes").Selected = false;
                brdstatus.Items.FindByText("No").Selected = false;

                brdInstalled.Items.FindByText("Yes").Selected = false;
                brdInstalled.Items.FindByText("No").Selected = false;

                DataTable DT = new DataTable();
                DT = ViewState["dt"] as DataTable;


            }
            catch(Exception )
            {
                
            }
        }



        protected void btnOk_Click(object sender, EventArgs e)
        {

            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

            Response.Redirect(@"~\Forms\Web Forms\DemoGrid.aspx");
        }



        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnsubmit.Enabled = true;

                brdDefecttransite.Enabled = true;
                brdManufacturer.Enabled = true;
                brdstatus.Enabled = true;
                brdInstalled.Enabled = true;
                brdItemNew.Enabled = true;

                brdItemNew.Items.FindByText("Yes").Selected = false;
                brdItemNew.Items.FindByText("No").Selected = false;

                brdDefecttransite.Items.FindByText("Yes").Selected = false;
                brdDefecttransite.Items.FindByText("No").Selected = false;

                brdManufacturer.Items.FindByText("Yes").Selected = false;
                brdManufacturer.Items.FindByText("No").Selected = false;

                brdstatus.Items.FindByText("Yes").Selected = false;
                brdstatus.Items.FindByText("No").Selected = false;

                brdInstalled.Items.FindByText("Yes").Selected = false;
                brdInstalled.Items.FindByText("No").Selected = false;



                DataTable DT = new DataTable();
                DT = ViewState["dt"] as DataTable;

                for (int j = 0; j < gvReturnDetails.Rows.Count; j++)
                {
                    RadioButton rb = (gvReturnDetails.Rows[j].FindControl("RadioButton1")) as RadioButton;
                    if (rb.Checked == true)
                    {

                        #region Deepak
                       // Session["_lsSlipPrintSKUNumber"] = new List<String>();
                        String SKUNumberforprint = (gvReturnDetails.Rows[j].FindControl("txtSKU") as TextBox).Text;
                        ((List<String>)Session["_lsSlipPrintSKUNumber"]).Add(SKUNumberforprint);

                        // _lsSlipPrintSKUNumber.Add(SKUNumberforprint);


                        #endregion
                        String LinetType = (gvReturnDetails.Rows[j].FindControl("txtLineType") as TextBox).Text;

                        if (Convert.ToInt16(LinetType) != 6)
                        {
                            String SKUNumber = (gvReturnDetails.Rows[j].FindControl("txtSKU") as TextBox).Text;

                            ViewState["SelectedskuName"] = SKUNumber;

                            String SKUSequence = (gvReturnDetails.Rows[j].FindControl("txtSKU_Sequence") as TextBox).Text;

                            ViewState["ItemQuantity"] = SKUSequence;

                            (gvReturnDetails.Rows[j].FindControl("txtSKU_Qty_Seq") as TextBox).Text = "1";

                            String SkuQuantitySequence = (gvReturnDetails.Rows[j].FindControl("txtSKU_Qty_Seq") as TextBox).Text;
                           
                            ViewState["SkuQuantitySequence"] = SkuQuantitySequence;

                            //if (SKUStatus != "")
                            //{
                                //for (int i = 0; i < DT.Rows.Count; i++)
                                //{

                                //    // string kU = DT.Rows[i][1].ToString();

                                //    if (SKUNumber == DT.Rows[i][0].ToString() && SKUSequence == DT.Rows[i][4].ToString())
                                //    {
                                //        // msg = dt.Rows[i][1].ToString() + " : " + dt.Rows[i][2].ToString() + "\n" + msg;

                                //        string data1 = DT.Rows[i][1].ToString();
                                //        string data2 = DT.Rows[i][2].ToString();

                                //        if (DT.Rows[i][1].ToString() == "Item is New" && DT.Rows[i][2].ToString() == "Yes")
                                //        {
                                //            brdItemNew.Items.FindByText("Yes").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Item is New" && DT.Rows[i][2].ToString() == "No"))
                                //        {
                                //            brdItemNew.Items.FindByText("No").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Installed" && DT.Rows[i][2].ToString() == "Yes"))
                                //        {
                                //            brdInstalled.Items.FindByText("Yes").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Installed" && DT.Rows[i][2].ToString() == "No"))
                                //        {
                                //            brdInstalled.Items.FindByText("No").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Chip/Bended/Scratch/Broken" && DT.Rows[i][2].ToString() == "Yes"))
                                //        {
                                //            brdstatus.Items.FindByText("Yes").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Chip/Bended/Scratch/Broken" && DT.Rows[i][2].ToString() == "No"))
                                //        {
                                //            brdstatus.Items.FindByText("No").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Manufacturer Defective" && DT.Rows[i][2].ToString() == "Yes"))
                                //        {
                                //            brdManufacturer.Items.FindByText("Yes").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Manufacturer Defective" && DT.Rows[i][2].ToString() == "No"))
                                //        {
                                //            brdManufacturer.Items.FindByText("No").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Defect in Transite" && DT.Rows[i][2].ToString() == "Yes"))
                                //        {
                                //            brdDefecttransite.Items.FindByText("Yes").Selected = true;
                                //        }
                                //        else if ((DT.Rows[i][1].ToString() == "Defect in Transite" && DT.Rows[i][2].ToString() == "No"))
                                //        {
                                //            brdDefecttransite.Items.FindByText("No").Selected = true;
                                //        }
                                //    }
                                //}


                                //for (int k = 0; k < Views.Global.lsSKUReasons.Count; k++)
                                //{
                                //    if (Views.Global.lsSKUReasons[k].ReturnDetailID == Guid.Parse(GuidReturnDetail))
                                //    {
                                //        System.Guid ReturnID = Views.Global.lsSKUReasons[k].ReturnDetailID;

                                //        string reas = Obj.Rcall.GetReasonstringbyReturnID(ReturnID);

                                //        ddlotherreasons.SelectedItem.Text = reas;

                                //        //cmbSkuReasons.Text = reas;
                                //    }
                                //}

                            //}
                            //else
                            //{

                            //    brdItemNew.Items.FindByText("Yes").Selected = false;
                            //    brdItemNew.Items.FindByText("No").Selected = false;

                            //    brdDefecttransite.Items.FindByText("Yes").Selected = false;
                            //    brdDefecttransite.Items.FindByText("No").Selected = false;

                            //    brdManufacturer.Items.FindByText("Yes").Selected = false;
                            //    brdManufacturer.Items.FindByText("No").Selected = false;

                            //    brdstatus.Items.FindByText("Yes").Selected = false;
                            //    brdstatus.Items.FindByText("No").Selected = false;

                            //    brdInstalled.Items.FindByText("Yes").Selected = false;
                            //    brdInstalled.Items.FindByText("No").Selected = false;


                            //}
                        }
                        else
                        {
                            mpeForLineType.Show();
                            //ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('Can not add comment/parent sku for combination item');</script>");
                            lblMassege.Text = "Can not add comment/parent sku for combination item";
                            //  string display = "This is Line Type 6";
                            // ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);

                            rb.Checked = false;

                            brdDefecttransite.Enabled = false;
                            brdManufacturer.Enabled = false;
                            brdstatus.Enabled = false;
                            brdInstalled.Enabled = false;
                            brdItemNew.Enabled = false;

                            brdItemNew.Items.FindByText("Yes").Selected = false;
                            brdItemNew.Items.FindByText("No").Selected = false;

                            brdDefecttransite.Items.FindByText("Yes").Selected = false;
                            brdDefecttransite.Items.FindByText("No").Selected = false;

                            brdManufacturer.Items.FindByText("Yes").Selected = false;
                            brdManufacturer.Items.FindByText("No").Selected = false;

                            brdstatus.Items.FindByText("Yes").Selected = false;
                            brdstatus.Items.FindByText("No").Selected = false;

                            brdInstalled.Items.FindByText("Yes").Selected = false;
                            brdInstalled.Items.FindByText("No").Selected = false;


                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            //  GetCount();

        }

        #endregion


        #region Displaying Rerurn Data for Customer
        //Return Boolean value by passing String RGA.
        //display all values Of Return information on all textboxes for Update. 


        //public Boolean display(String RMA)
        //{
        //    Boolean _flag = false;
        //    try
        //    {
        //        Views.Global.lsGetValuesFromSR=Obj.Rcall.ReturnByRMANumber(RMA);

        //        txtrganumber.Text = Views.Global.lsGetValuesFromSR.RGAROWID;
        //        txtRMAnumber.Text = Views.Global.lsGetValuesFromSR.RMANumber;
        //        ddlstatus.Text = Views.Global.lsGetValuesFromSR.RMAStatus.ToString();
        //        txtshipmentnumber.Text = Views.Global.lsGetValuesFromSR.ShipmentNumber;

        //        txtcustomerName.Text = Views.Global.lsGetValuesFromSR.CustomerName1;
        //        ddldecision.SelectedIndex = Convert.ToInt16(Views.Global.lsGetValuesFromSR.Decision);
        //        txtponumber.Text = Views.Global.lsGetValuesFromSR.PONumber;
        //        txtreturndate.Text = Convert.ToString(Views.Global.lsGetValuesFromSR.ReturnDate.ToShortDateString());
        //        txtorderdate.Text = Convert.ToString(Views.Global.lsGetValuesFromSR.OrderDate.ToShortDateString());
        //        txtcustomerName.Text = Views.Global.lsGetValuesFromSR.CustomerName1;
        //        txtvendorName.Text = Views.Global.lsGetValuesFromSR.VendoeName;
        //        txtordernumber.Text = Views.Global.lsGetValuesFromSR.OrderNumber;
        //        txtvendornumber.Text = Views.Global.lsGetValuesFromSR.VendorNumber;
        //        txtCalltag.Text = Views.Global.lsGetValuesFromSR.CallTag;

        //        if (Views.Global.lsGetValuesFromSR.ProgressFlag == 1)
        //        {
        //            chkflag.Checked = true;
        //        }
        //        _flag = true;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return _flag;
        //}


        public void Display(String RMA)
        {
            try
            {//GetCustomer
               // List<RMAInfo> lsCustomeronfo = _newRMA.ReturnByRMANumber(txtponumber.Text);
               // List<Return> lsCustomeronfo = _newRMA.GetCustomerByRMANumber(RMA);




              //  Views.Global.lsReturnGlobalBySRNumber = _newRMA.GetCustomerByRMANumber(RMA); 
                
                List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomerByRMANumber(RMA);
                Session["lsReturnGlobalBySRNumber"] = lsCustomeronfo;


                if (lsCustomeronfo.Count > 0)
                {

                   // Views.Global.RMAInfoGlobal = lsCustomeronfo[0];
                    
                    txtponumber.Text = lsCustomeronfo[0].PONumber;
                    txtCustomerAddress.Text = lsCustomeronfo[0].Address1;
                    txtcustomerName.Text = lsCustomeronfo[0].CustomerName1;
                    // txtcountry.Text = lsCustomeronfo[0].Country;
                     txtCustomerCity.Text = lsCustomeronfo[0].City;
                     txtCustomerState.Text = lsCustomeronfo[0].State;
                     txtCustomerZip.Text = lsCustomeronfo[0].ZipCode;
                    txtvendorName.Text = lsCustomeronfo[0].VendorName;
                    txtvendornumber.Text = lsCustomeronfo[0].VendorNumber;
                    txtRMAnumber.Text = lsCustomeronfo[0].RMANumber;
                    txtcustomerName.Text = lsCustomeronfo[0].CustomerName1;
                    //txtrganumber.Text=lsCustomeronfo[0]
                    txtshipmentnumber.Text = lsCustomeronfo[0].ShipmentNumber;
                    // TextBox1.Text = lsCustomeronfo[0].CallTag;
                   // txtordernumber.Text = lsCustomeronfo[0].OrderNumber;
                    DateTime dt = lsCustomeronfo[0].OrderDate;
                   // txtorderdate.Text = dt.ToString("MM/dd/yyyy hh:mm tt");
                    txtCalltag.Text = lsCustomeronfo[0].CallTag;
                    DateTime dtReturnDate = lsCustomeronfo[0].ReturnDate;
                    txtreturndate.Text = dtReturnDate.ToString("MM/dd/yyyy hh:mm tt");
                   // txtorderdate.Text = dtReturnDate.ToString("MM/dd/yyyy hh:mm tt");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion

        #region Fill Return Details in Gridview
        public void FillReturnDetails(List<RMAInfo> lsReturnDetails)
        {
            try
            {
                Obj._lsReturnDetailsWithSR = lsReturnDetails;
               // Obj._lsreturn = lsReturnDetails;
                var ReaturnDetails = from Rs in lsReturnDetails
                                     select new
                                     {
                                       //  Rs.RGADROWID,
                                         Rs.SKUNumber,
                                         Rs.SKU_Qty_Seq,
                                       //  Rs.SKU_Status,
                                         Rs.SKU_Sequence,
                                         Rs.ProductID,
                                         Rs.SalesPrice,
                                         Rs.LineType,
                                         Rs.ShipmentLines,
                                         Rs.ReturnLines,
                                      //   Rs.ReturnDetailID,
                                       //  ReasonIDs = _Update.ReasonsIdByHasg(Rs.ReturnDetailID),
                                         ImageName = "",
                                         NoofImages = "",  
                                     };
                gvReturnDetails.DataSource = ReaturnDetails.ToList();
                gvReturnDetails.DataBind();

                GetCount();
              
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Getting Image Counts
        public void GetCount()
        {
            foreach (GridViewRow row in gvReturnDetails.Rows)
            {
                //string GuidReturnDetail = (row.FindControl("lblguid") as Label).Text;
                //List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(GuidReturnDetail));
                //string ImageCount = Convert.ToString(lsImages2.Count);
                (row.FindControl("txtImageCount") as LinkButton).Text = "0" + " " + "Image(s)";
            }
        }
        #endregion

        #region Creating Rows for Columns
        //public void fillReturnedstatusandpoit()
        //{           
        //    listofstatus = Obj.Rcall.ReturnedSKUansPoints(Views.Global.ReteunGlobal.ReturnID);
        //    for (int i = 0; i < listofstatus.Count; i++)
        //    {
        //        DataRow dr0 = DtReturnReason.NewRow();
        //        dr0["SKU"] = listofstatus[i].SKU;
        //        dr0["Reason"] = listofstatus[i].Reason;
        //        dr0["Reason_Value"] = listofstatus[i].Reason_Value;
        //        dr0["Points"] = listofstatus[i].Points;
        //        dr0["ItemQuantity"] = listofstatus[i].SkuSequence;
        //        dr0["ReturnedSKUID"] = listofstatus[i].ID;
        //        dr0["ReturnDetailID"] = listofstatus[i].ReturnDetailID;
        //        DtReturnReason.Rows.Add(dr0);
        //    }
        //}
        #endregion

        #region Fill return Details & Status 
        //public void fillReturnDetailAndStatus()
        //{
        //    List<ReturnDetail> retuen = Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"]);
        //    Views.Global.lsSKUReasons = Obj.Rcall.SKUReasonsByReturnDetails(retuen);
        //    for (int i = 0; i < Obj._lsReturnDetails.Count; i++)
        //    {
        //        StatusAndPoints _lsstatusandpoints = new StatusAndPoints();
        //        if (Obj._lsReturnDetails[i].SKU_Status != "")
        //        {
        //            _lsstatusandpoints.SKUName = Obj._lsReturnDetails[i].SKUNumber;
        //            _lsstatusandpoints.Status = Obj._lsReturnDetails[i].SKU_Status;
        //            _lsstatusandpoints.Points = Obj._lsReturnDetails[i].SKU_Reason_Total_Points;
        //            _lsstatusandpoints.IsMannually = Obj._lsReturnDetails[i].IsManuallyAdded;
        //            _lsstatusandpoints.IsScanned = Obj._lsReturnDetails[i].IsSkuScanned;
        //            _lsstatusandpoints.NewItemQuantity = Obj._lsReturnDetails[i].SKU_Sequence;
        //            _lsstatusandpoints.skusequence = Obj._lsReturnDetails[i].SKU_Qty_Seq;
        //            Views.Global.listofstatusAndPoint.Add(_lsstatusandpoints);
        //        }
        //    }           
        //}
        #endregion       

        #region Getting User Information Method
        //public void GetLatestUser()
        //{          
        //    try
        //    {
        //        Guid userId = (Guid)Views.Global.ReteunGlobal.CreatedBy;
        //        Obj.Rcall.GetUserInfobyUserID(userId);
        //        lblUserName.Text = Obj.Rcall.GetUserInfobyUserID(userId).UserFullName;               
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        #endregion

        #region
        private void ReasonsIDs_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Obj.ReasonsIDs.UpdatePopupValue != "")
            {
                Views.ReasonList _Reason = new Views.ReasonList();
                _Reason.ID = Obj.RowID;
                _Reason.ReasonString = Obj.ReasonsIDs.UpdatePopupValue;
                Obj._ReasonList.Add(_Reason);
            }
        }
        #endregion

        #region
        public void fillddlotherReasons()
        {
            List<Reason> lsReturn = _newRMA.GetReasons();
            Reason re = new Reason();
            re.ReasonID = Guid.NewGuid();
            re.Reason1 = "--Select--";
            lsReturn.Insert(0, re);
            ddlotherreasons.DataTextField = "Reason1";
            ddlotherreasons.DataValueField = "ReasonID";
            ddlotherreasons.DataSource = lsReturn;
            ddlotherreasons.DataBind();
        }
        #endregion

        #region Showing Comments
        public void ShowComments()
        {

           // List<RMAComment> rmaComment = new List<RMAComment>();

           // rmaComment = (List<RMAComment>)Session["rmacommentsr"];



           // this.Controls.Add(new LiteralControl("<div style=' border-radius: 11px 0 0 11px;  border: 1px solid; position : absolute; color:#179090; left :950px; right : 50px; top :235px;width:398px;height:258px;overflow: auto;'>"));
           //// List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);
           // foreach (var item in rmaComment.OrderByDescending(y => y.CommentDate))
           // {   
           //     this.Controls.Add(new LiteralControl("<table width='100%' >"));
           //     this.Controls.Add(new LiteralControl("<tr><td bgcolor='#8DC6FF'>"));
           //     this.Controls.Add(new LiteralControl("<h8> " + Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName + " || " + item.CommentDate.ToString("MM/dd/yyyy hh:mm tt") + "</h8> "));
           //     this.Controls.Add(new LiteralControl("</td></tr><tr><td bgcolor='#FFFFFF'shape='rect'><b>" + item.Comment + "</td></tr>"));                
           //     this.Controls.Add(new LiteralControl(" </table>"));
           // }          
           // this.Controls.Add(new LiteralControl("</div>"));           

            List<RMAComment> rmaComment = new List<RMAComment>();

            rmaComment = (List<RMAComment>)Session["rmacommentsr"];


            DataTable dtRepeater = new DataTable();
            dtRepeater.Columns.Add("UserName");
            dtRepeater.Columns.Add("Time");
            dtRepeater.Columns.Add("Content");

            // this.Controls.Add(new LiteralControl("<div style=' border-radius: 11px 0 0 11px;  border: 1px solid; position : absolute; color:#179090; left :  1190px; right : 50px; top :137px;width:360px;height:220px;overflow: auto;'>"));
            //List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);
            foreach (var item in rmaComment.OrderByDescending(y => y.CommentDate))
            {
                DataRow rd = dtRepeater.NewRow();
                string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName;

                rd["UserName"] = Usernm;
                rd["Time"] = item.CommentDate.ToString("MM/dd/yyyy hh:mm tt");
                rd["Content"] = item.Comment;
                dtRepeater.Rows.Add(rd);
            }

            Repeater1.DataSource = dtRepeater;
            Repeater1.DataBind();
        }
        #endregion

        #region Add Comment Button click event
        protected void btnComment_Click(object sender, EventArgs e)
        {
            fnforComment();
            ShowComments();
            txtcomment.Text = "";
          //  fnforComment();
          ////  List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);
          //  DataTable dtRepeater = new DataTable();
          //  dtRepeater.Columns.Add("UserName");
          //  dtRepeater.Columns.Add("Time");
          //  dtRepeater.Columns.Add("Content");
          //  foreach (var item in lsComment.OrderByDescending(y => y.CommentDate))
          //  {

          //      DataRow rd = dtRepeater.NewRow();
          //      string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName;

          //      rd["UserName"] = Usernm;
          //      rd["Time"] = item.CommentDate.ToString("MM/dd/yyyy hh:mm tt");
          //      rd["Content"] = item.Comment;
          //      dtRepeater.Rows.Add(rd);

          //  }
          //  Repeater1.DataSource = dtRepeater;
          //  Repeater1.DataBind();



            lblMassege.Text = "Comment Added";
            mpePopupForCommentYes.Show();
        }
        #endregion

        #region Function For Comment
        public void fnforComment()
        {
            List<RMAComment> lscoment = new List<RMAComment>();

            lscoment = (List<RMAComment>)Session["rmacommentsr"];

            RMAComment lscomment = new RMAComment();
            lscomment.RMACommentID = Guid.NewGuid();
            lscomment.ReturnID = Guid.NewGuid();
            lscomment.UserID = (Guid)Session["UserID"];
            lscomment.Comment = txtcomment.Text;
            lscomment.CommentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");

            // Views.Global.rmaComment.Add(lscomment);


            lscoment.Add(lscomment);

            Session["rmacommentsr"] = lscoment;
        }
        #endregion

        #region Add New Product Button Click Event
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            txtNewItem.Visible = true;
            BtnAddNewItem.Visible = true;

            //mpePopupForSaveYes.Show();
        }
        #endregion

        #region Add Button Click Event

        int max, shipmax, returnmax;

        protected void BtnAddNewItem_Click(object sender, EventArgs e)
        {
            if (txtNewItem.Text != "")
            {
               // dt.Columns.Add("RGADROWID");
                dt.Columns.Add("SKUNumber");
                dt.Columns.Add("SKU_Qty_Seq");
               // dt.Columns.Add("SKU_Status");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("SKU_Sequence");
                dt.Columns.Add("SalesPrice");
                dt.Columns.Add("NoofImages");
                dt.Columns.Add("ImageName");
                dt.Columns.Add("LineType");
                dt.Columns.Add("ShipmentLines");
                dt.Columns.Add("ReturnLines");
               // dt.Columns.Add("ReturnDetailID");

                for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr1 = dt.NewRow();

                      //  TextBox RowID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtRGANumberID");
                        TextBox SKUNumber = (TextBox)gvReturnDetails.Rows[i].FindControl("txtsku");
                        TextBox SKU_Qty_Seq = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq");
                      //  TextBox SKU_Status = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Status");
                        TextBox ProductID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtProductID");
                        TextBox SKU_Sequence = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence");                      
                        TextBox SalesPrice = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSalesPrice");
                        TextBox LineType = (TextBox)gvReturnDetails.Rows[i].FindControl("txtLineType");
                        TextBox ShipmentLines = (TextBox)gvReturnDetails.Rows[i].FindControl("txtShipmentLines");
                        TextBox ReturnLines = (TextBox)gvReturnDetails.Rows[i].FindControl("txtReturnLines");
                        Label lblimages = (Label)gvReturnDetails.Rows[i].FindControl("lblImagesName");
                        LinkButton NoOfImages = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtImageCount");
                       // Label lblReturnDetailID = (Label)gvReturnDetails.Rows[i].FindControl("lblguid");

                        //dr1[0] = RowID.Text;
                        //dr1[1] = SKUNumber.Text;
                        //dr1[2] = SKU_Qty_Seq.Text;
                        //dr1[3] = SKU_Status.Text;
                        //dr1[4] = ProductID.Text;
                        //dr1[5] = SKU_Sequence.Text;
                        //dr1[6] = SalesPrice.Text;
                        //dr1[7] = NoOfImages.Text;
                        //dr1[8] = lblimages.Text;
                        //dr1[9] = LineType.Text;
                        //dr1[10] = ShipmentLines.Text;
                        //dr1[11] = ReturnLines.Text;
                        //dr1[12] = lblReturnDetailID.Text;


                       
                        dr1[0] = SKUNumber.Text;
                        dr1[1] = SKU_Qty_Seq.Text;
                        dr1[2] = ProductID.Text;
                        dr1[3] = SKU_Sequence.Text;
                        dr1[4] = SalesPrice.Text;
                        dr1[5] = NoOfImages.Text;
                        dr1[6] = lblimages.Text;
                        dr1[7] = LineType.Text;
                        dr1[8] = ShipmentLines.Text;
                        dr1[9] = ReturnLines.Text;
                       

                        dt.Rows.Add(dr1);

                        if (SKUNumber.Text == txtNewItem.Text)
                        {
                            NonPo = false;
                            if (max < Convert.ToInt16(SKU_Sequence.Text))
                            {
                                max = Convert.ToInt16(SKU_Sequence.Text);
                            }
                            if (shipmax < Convert.ToInt16(ShipmentLines.Text))
                            {
                                shipmax = Convert.ToInt16(ShipmentLines.Text);
                            }

                            if (returnmax < Convert.ToInt16(ReturnLines.Text))
                            {
                                returnmax = Convert.ToInt16(ReturnLines.Text);
                            }
                        }
                        else
                        {
                            NonPo = false;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                DataRow dr = dt.NewRow();

               //// dr[0] = "";
               // dr[1] = txtNewItem.Text;
               // dr[2] = "0";
               //// dr[3] = "";
               // dr[4] = "0";
               // dr[5] = max + 1000;
               // dr[6] = "0";
               // dr[7] = "0 Image(s)";
               // dr[8] = "";
               // dr[9] = "1";
               // dr[10] = shipmax + 1000;
               // dr[11] = returnmax + 1000;
               //// dr[12] = "";

                // dr[0] = "";
                dr[0] = txtNewItem.Text;
                dr[1] = "0";
                // dr[3] = "";
                dr[2] = "0";
                dr[3] = max + 1000;
                dr[4] = "0";
                dr[5] = "0 Image(s)";
                dr[6] = "";
                dr[7] = "1";
                dr[8] = shipmax + 1000;
                dr[9] = returnmax + 1000;
                // dr[12] = "";

                dt.Rows.Add(dr);

                max = 0;
                returnmax = 0;
                shipmax = 0;
                txtNewItem.Text = "";

                gvReturnDetails.DataSource = dt;
                gvReturnDetails.DataBind();

                dt.Clear();
                lblMassege.Text = "SKU Added";

                mpePopupForAddYes.Show();
            }            
            else
            {
                lblMassege.Text = "Please Enter SKU Name";
                mpePopupForAddNo.Show();
            }
        }
        #endregion

        #region Update Button Click Event
        //protected void btnupdate_Click(object sender, EventArgs e)
        //{
        //    //object of return.


        //    int InProgress = 0;

        //    if (chkflag.Checked == true)
        //    {
        //        InProgress = 1;
        //    }


        //    //  Return ret = Obj.Rcall.ReturnByRGAROWID(rga)[0];

        //    DateTime ScannedDate = DateTime.UtcNow;
        //    DateTime ExpirationDate = DateTime.UtcNow.AddDays(60);

        //    #region ReturnDetail




        //    //list of ReturnDetails by using RGAROWID.
        //    Views.Global.lsReturnDetail = Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString());

        //    //Set the Return Information in Return Table.
        //    // Guid returnid = _Update.SetReturnTbl(ret, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), Convert.ToDateTime(txtreturndate.Text),"");

        //    //  returnid = _Update.SetReturnByRGANumber(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);


        //    if (Views.Global.ReteunGlobal.RMANumber == "N/A")
        //    {
        //        if (Views.Global.ReteunGlobal.OrderNumber == "N/A")
        //        {
        //            returnid = _Update.SetReturnByRGANumber(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
        //        }
        //        else
        //        {
        //            returnid = _Update.SetReturnByPonumberTbl(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
        //        }
        //    }
        //    else
        //    {

        //        returnid = _Update.SetReturnTbl(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
        //    }
        //    //set Gridview information in ReturnDetail Table.
        //    for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
        //    {
        //        int flag = 0;

        //        //  Guid ReturnDetailsID = Views.Global.lsReturnDetail[i].ReturnDetailID;

        //        //string Dquantity = (gvReturnDetails.Rows[i].FindControl("txtdeliveredquantity") as TextBox).Text;

        //        string Rquantity = (gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq") as TextBox).Text;

        //        String SKUNumber = (gvReturnDetails.Rows[i].FindControl("txtSKU") as TextBox).Text;

        //        string ProductID = (gvReturnDetails.Rows[i].FindControl("txtProductID") as TextBox).Text;

        //        string SKUSequence = (gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence") as TextBox).Text;

        //        string SalesPrice = (gvReturnDetails.Rows[i].FindControl("txtSalesPrice") as TextBox).Text;

        //        string Linetype = (gvReturnDetails.Rows[i].FindControl("txtLineType") as TextBox).Text;

        //        string ShipmentLine = (gvReturnDetails.Rows[i].FindControl("txtShipmentLines") as TextBox).Text;

        //        string ReturnLine = (gvReturnDetails.Rows[i].FindControl("txtReturnLines") as TextBox).Text;

        //        string GuidReturnDetail = (gvReturnDetails.Rows[i].FindControl("lblguid") as Label).Text;

        //        string imglist = ((Label)gvReturnDetails.Rows[i].FindControl("lblImagesName")).Text;

        //        string SKUNewName = "";
        //        Boolean checkflag = false;
        //        if (Views.Global.listofstatusAndPoint.Count > 0)
        //        {
        //            for (int j = Views.Global.listofstatusAndPoint.Count - 1; j >= 0; j--)
        //            {
        //                if (Views.Global.listofstatusAndPoint[j].SKUName == SKUNumber && Views.Global.listofstatusAndPoint[j].NewItemQuantity == Convert.ToInt16(SKUSequence))
        //                {
        //                    SKUNewName = SKUNumber;
        //                    Views.Global.SKU_Staus = Views.Global.listofstatusAndPoint[j].Status;
        //                    Views.Global.TotalPoints = Views.Global.listofstatusAndPoint[j].Points;
        //                    Views.Global.IsScanned = Views.Global.listofstatusAndPoint[j].IsScanned;
        //                    Views.Global.IsManually = Views.Global.listofstatusAndPoint[j].IsMannually;
        //                    Views.Global.NewItemQty = Views.Global.listofstatusAndPoint[j].NewItemQuantity;
        //                    Views.Global._SKU_Qty_Seq = Views.Global.listofstatusAndPoint[j].skusequence;

        //                    Views.Global.listofstatusAndPoint.RemoveAt(j);
        //                    checkflag = true;

        //                    break;
        //                }
        //            }
        //            if (!checkflag)
        //            {
        //                Views.Global.SKU_Staus = "";
        //                Views.Global.TotalPoints = 0;
        //                Views.Global.IsScanned = 1;//listofstatus[i].IsScanned;
        //                Views.Global.IsManually = 1;//listofstatus[i].IsMannually;
        //                Views.Global.NewItemQty = 1;
        //                Views.Global._SKU_Qty_Seq = 0;
        //            }
        //        }
        //        else
        //        {
        //            SKUNewName = SKUNumber;
        //            Views.Global.SKU_Staus = "";
        //            Views.Global.TotalPoints = 0;
        //            Views.Global.IsScanned = 1;
        //            Views.Global.IsManually = 1;
        //            Views.Global.NewItemQty = 1;
        //            Views.Global._SKU_Qty_Seq = 0;

        //        }

        //        //for (int j = 0; j < Views.Global.lsReturnDetail.Count; j++)
        //        //{
        //        //    if (Views.Global.lsReturnDetail[j].SKUNumber == SKUNumber && Views.Global.lsReturnDetail[j].SKU_Sequence == Convert.ToInt16(SKUSequence))
        //        //    {
        //        //        flag = 1;
        //        //        break;
        //        //    }

        //        //}
        //        Guid ReturnDetailsID = Guid.NewGuid();
        //        if (GuidReturnDetail != "")
        //        {
        //            ReturnDetailsID = _Update.SetReturnDetailTbl(Guid.Parse(GuidReturnDetail), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], Views.Global.SKU_Staus, Views.Global.TotalPoints, Views.Global.IsScanned, Views.Global.IsManually, Convert.ToInt16(SKUSequence), Views.Global._SKU_Qty_Seq, ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));

        //        }
        //        else
        //        {
        //            ReturnDetailsID = _Update.SetReturnDetailNewInsertTbl(Guid.NewGuid(), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], Views.Global.SKU_Staus, Views.Global.TotalPoints, Views.Global.IsScanned, Views.Global.IsManually, Convert.ToInt16(SKUSequence), Views.Global._SKU_Qty_Seq, ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));
        //        }

        //    #endregion



        //        #region SKUReasons Delete and Insert

        //        //Guid NewReturnID = Guid.Parse(GuidReturnDetail);

        //        //  Obj.Rcall.DeleteSKUReasonsByReturnDetailID(ReturnDetailsID);


        //        if (Views.Global._lsReasonSKU.Count > 0)
        //        {
        //            for (int k = Views.Global._lsReasonSKU.Count - 1; k >= 0; k--)
        //            {
        //                if (Views.Global._lsReasonSKU[k].SKUName == SKUNumber && Views.Global._lsReasonSKU[k].SKU_sequence == Convert.ToInt16(SKUSequence))
        //                {
        //                    Obj.Rcall.SetTransaction(Guid.NewGuid(), Views.Global._lsReasonSKU[k].ReasonID, ReturnDetailsID);
        //                    Views.Global._lsReasonSKU.RemoveAt(k);
        //                }
        //            }
        //        }


        //        #endregion




        //        #region ReturnedQuantity


        //        if (Views.Global.DT1.Rows.Count > 0)
        //        {
        //            for (int k = Views.Global.DT1.Rows.Count - 1; k >= 0; k--)
        //            {
        //                DataRow d = Views.Global.DT1.Rows[k];
        //                if (d["SKU"].ToString() == SKUNumber && d["ItemQuantity"].ToString() == SKUSequence)
        //                {
        //                    string RetirID = d["ReturnDetailID"].ToString();

        //                    if (Guid.Parse(d["ReturnDetailID"].ToString()) == ReturnDetailsID && d["ReturnedSKUID"].ToString() != null && d["ReturnedSKUID"].ToString() != "")
        //                    {
        //                        // Guid skureturn = Guid.Parse(d["ReturnedSKUID"].ToString());

        //                        Guid ReturnedSKUPoints = _Update.SetReturnedSKUPoints(Guid.Parse(d["ReturnedSKUID"].ToString()), ReturnDetailsID, returnid, Views.Global.DT1.Rows[k][0].ToString(), Views.Global.DT1.Rows[k][1].ToString(), Views.Global.DT1.Rows[k][2].ToString(), Convert.ToInt16(Views.Global.DT1.Rows[k][3].ToString()), Convert.ToInt16(Views.Global.DT1.Rows[k][4].ToString()));
        //                        d.Delete();
        //                    }
        //                    else
        //                    {
        //                        _Update.SetReturnedSKUPoints(Guid.NewGuid(), ReturnDetailsID, returnid, Views.Global.DT1.Rows[k][0].ToString(), Views.Global.DT1.Rows[k][1].ToString(), Views.Global.DT1.Rows[k][2].ToString(), Convert.ToInt16(Views.Global.DT1.Rows[k][3].ToString()), Convert.ToInt16(Views.Global.DT1.Rows[k][4].ToString()));
        //                        d.Delete();
        //                    }

        //                }
        //            }
        //        }


        //        #endregion

        //        #region InsertImages

        //        foreach (var item in imglist.Split(new char[] { '\n' }))
        //        {
        //            if (item != null && item != "")
        //            {

        //                String NameImage = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString() + "\\" + item.ToString();

        //                Guid ImageID = _newRMA.SetReturnedImages(Guid.NewGuid(), ReturnDetailsID, NameImage, (Guid)Session["UserID"]);
        //            }
        //        }

        //        #endregion









        //        // _Update.SetReturnDetailTbl(lsretundetail[i], Convert.ToInt16(Dquantity), Convert.ToInt16(Rquantity), SKUNumber,ProductName);

        //    }

        //    //Clear the Reasons list from Global Object.
        //    Obj._ReasonList = new List<Views.ReasonList>();

        //    //  Response.Redirect("~/Forms/Web Forms/frmRetunDetail.aspx");
        //    lblUser.Text = "Please Select Any One Option";
        //    ModalPopupExtender1.Show();
        //}

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            //object of return.


            int InProgress = 0;

            if (chkflag.Checked == true)
            {
                InProgress = 1;
            }


            //  Return ret = Obj.Rcall.ReturnByRGAROWID(rga)[0];

            DateTime ScannedDate = DateTime.UtcNow;
            DateTime ExpirationDate = DateTime.UtcNow.AddDays(60);

            #region ReturnDetail




                        //list of ReturnDetails by using RMANumber.
          //  Views.Global.lsReturnDetailBySRNumber = Obj.Rcall.ReturnDetailBySRNumber(Request.QueryString["RMANumber"].ToString());

                        //Set the Return Information in Return Table.
            // Guid returnid = _Update.SetReturnTbl(ret, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), Convert.ToDateTime(txtreturndate.Text),"");

            //  returnid = _Update.SetReturnByRGANumber(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);


            //if (Views.Global.ReteunGlobal.RMANumber == "N/A")
            //{
            //    if (Views.Global.ReteunGlobal.OrderNumber == "N/A")
            //    {
            //Views.Global.RMAInfoGlobal
            List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomerByRMANumber(Request.QueryString["RMANumber"].ToString());          
             DateTime DeliveryDate = lsCustomeronfo[0].DeliveryDate;
            DateTime CurrentDate = DateTime.UtcNow;
            TimeSpan Diff = CurrentDate.Subtract(DeliveryDate);
            int Days = Diff.Days;
            ViewState["Days"] = Days;

            string wrongRMA="0";
            string Warranty="1";

            returnid = _Update.SetReturnTblForRMA((List<RMAInfo>)Session["lsReturnGlobalBySRNumber"], Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text, wrongRMA, Warranty, 60, (int)ViewState["Days"]);

            //Byte RMAStatus = Convert.ToByte(ddlstatus.SelectedValue.ToString());

            //Byte Decision = Convert.ToByte(ddldecision.SelectedValue.ToString());

            //List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomerByRMANumber(Request.QueryString["RMANumber"].ToString());          
            //DateTime DeliveryDate = lsCustomeronfo[0].DeliveryDate;
            //DateTime CurrentDate = DateTime.UtcNow;
            //TimeSpan Diff = CurrentDate.Subtract(DeliveryDate);
            //int Days = Diff.Days;
            //Views.Global.ShipDate_ScanDate_Diff = Days;

            //Guid userID = Guid.NewGuid();

            //returnid = _Update.SetReturnTblForRMA("", RMAStatus, Decision, clGlobal.mCurrentUser.UserInfo.UserID, ScannedDate, ExpirationDate, 0, 1, 60, Views.Global.ShipDate_ScanDate_Diff, InProgress, txtCalltag.Text, DateTime.UtcNow, userID);//ReturnReasons()


            //    }
            //    else
            //    {
             //       returnid = _Update.SetReturnByPonumberTbl(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
            //    }
            //}
            //else
            //{

            //    returnid = _Update.SetReturnTbl(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
            //}
            //set Gridview information in ReturnDetail Table.
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                int flag = 0;

                //  Guid ReturnDetailsID = Views.Global.lsReturnDetail[i].ReturnDetailID;

                //string Dquantity = (gvReturnDetails.Rows[i].FindControl("txtdeliveredquantity") as TextBox).Text;

                string Rquantity = (gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq") as TextBox).Text;

                String SKUNumber = (gvReturnDetails.Rows[i].FindControl("txtSKU") as TextBox).Text;

               // ViewState["SKUNumberPrint"]=

                string ProductID = (gvReturnDetails.Rows[i].FindControl("txtProductID") as TextBox).Text;

                string SKUSequence = (gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence") as TextBox).Text;

                string SalesPrice = (gvReturnDetails.Rows[i].FindControl("txtSalesPrice") as TextBox).Text;

                string Linetype = (gvReturnDetails.Rows[i].FindControl("txtLineType") as TextBox).Text;

                string ShipmentLine = (gvReturnDetails.Rows[i].FindControl("txtShipmentLines") as TextBox).Text;

                string ReturnLine = (gvReturnDetails.Rows[i].FindControl("txtReturnLines") as TextBox).Text;

               // string GuidReturnDetail = (gvReturnDetails.Rows[i].FindControl("lblguid") as Label).Text;

                string imglist = ((Label)gvReturnDetails.Rows[i].FindControl("lblImagesName")).Text;

                string SKUNewName = "";
                Boolean checkflag = false;

                List<StatusAndPoints> statusAndPoint = new List<StatusAndPoints>();
                statusAndPoint = (List<StatusAndPoints>)Session["listofstatusAndPoint"];



                if (((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Count > 0)
                {
                    for (int j = ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Count - 1; j >= 0; j--)
                    {
                        if (((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].SKUName == SKUNumber && ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].NewItemQuantity == Convert.ToInt16(SKUSequence))
                        {
                            SKUNewName = SKUNumber;
                            ViewState["SKU_Staus"] = statusAndPoint[j].Status;
                            ViewState["TotalPoints"] = statusAndPoint[j].Points;
                            ViewState["IsScanned"] = statusAndPoint[j].IsScanned;
                            ViewState["IsManually"] = statusAndPoint[j].IsMannually;
                            ViewState["NewItemQty"] = statusAndPoint[j].NewItemQuantity;
                            ViewState["_SKU_Qty_Seq"] = statusAndPoint[j].skusequence;

                            ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).RemoveAt(j);
                            checkflag = true;

                            break;
                        }
                    }
                    if (!checkflag)
                    {
                        ViewState["SKU_Staus"] = "";
                        ViewState["TotalPoints"] = 0;
                        ViewState["IsScanned"] = 1;//listofstatus[i].IsScanned;
                        ViewState["IsManually"] = 1;//listofstatus[i].IsMannually;
                        ViewState["NewItemQty"] = 1;
                        ViewState["_SKU_Qty_Seq"] = 0;
                    }
                }
                else
                {
                    SKUNewName = SKUNumber;
                    ViewState["SKU_Staus"] = "";
                    ViewState["TotalPoints"] = 0;
                    ViewState["IsScanned"] = 1;
                    ViewState["IsManually"] = 1;
                    ViewState["NewItemQty"] = 1;
                    ViewState["_SKU_Qty_Seq"] = 0;

                }
                

                //for (int j = 0; j < Views.Global.lsReturnDetail.Count; j++)
                //{
                //    if (Views.Global.lsReturnDetail[j].SKUNumber == SKUNumber && Views.Global.lsReturnDetail[j].SKU_Sequence == Convert.ToInt16(SKUSequence))
                //    {
                //        flag = 1;
                //        break;
                //    }

                //}
                Guid ReturnDetailsID = Guid.NewGuid();
                //if (GuidReturnDetail != "")
                //{
                //    ReturnDetailsID = _Update.SetReturnDetailTbl(Guid.Parse(GuidReturnDetail), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], Views.Global.SKU_Staus, Views.Global.TotalPoints, Views.Global.IsScanned, Views.Global.IsManually, Convert.ToInt16(SKUSequence), Views.Global._SKU_Qty_Seq, ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));

                //}
                //else
                //{
                    //ReturnDetailsID = _Update.SetReturnDetailNewInsertTbl(Guid.NewGuid(), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], Views.Global.SKU_Staus, Views.Global.TotalPoints, Views.Global.IsScanned, Views.Global.IsManually, Convert.ToInt16(SKUSequence), Views.Global._SKU_Qty_Seq, ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));
                ReturnDetailsID = _Update.SetReturnDetailNewInsertTbl(Guid.NewGuid(), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], (String)ViewState["SKU_Staus"], (int)ViewState["TotalPoints"], (int)ViewState["IsScanned"], (int)ViewState["IsManually"], Convert.ToInt16(SKUSequence), Convert.ToInt32(Rquantity), ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));
                //}

            #endregion



                #region SKUReasons Delete and Insert

                //Guid NewReturnID = Guid.Parse(GuidReturnDetail);

                //  Obj.Rcall.DeleteSKUReasonsByReturnDetailID(ReturnDetailsID);


                if (((List<SkuReasonIDSequence>)Session["_lsReasonSKU"]).Count > 0)
                {
                    for (int k = ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"]).Count - 1; k >= 0; k--)
                    {
                        if (((List<SkuReasonIDSequence>)Session["_lsReasonSKU"])[k].SKUName == SKUNumber && ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"])[k].SKU_sequence == Convert.ToInt16(SKUSequence))
                        {
                            Obj.Rcall.SetTransaction(Guid.NewGuid(), ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"])[k].ReasonID, ReturnDetailsID);
                            ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"]).RemoveAt(k);
                        }
                    }
                }


                #endregion




                #region ReturnedQuantity


                if (((DataTable)Session["dtsr"]).Rows.Count > 0)
                {
                    for (int k = ((DataTable)Session["dtsr"]).Rows.Count - 1; k >= 0; k--)
                    {
                        DataRow d = ((DataTable)Session["dtsr"]).Rows[k];
                        if (d["SKU"].ToString() == SKUNumber && d["ItemQuantity"].ToString() == SKUSequence)
                        {
                            //string RetirID = d["ReturnDetailID"].ToString();

                            //if (Guid.Parse(d["ReturnDetailID"].ToString()) == ReturnDetailsID && d["ReturnedSKUID"].ToString() != null && d["ReturnedSKUID"].ToString() != "")
                            //{
                            //    // Guid skureturn = Guid.Parse(d["ReturnedSKUID"].ToString());

                            //    Guid ReturnedSKUPoints = _Update.SetReturnedSKUPoints(Guid.Parse(d["ReturnedSKUID"].ToString()), ReturnDetailsID, returnid, ((DataTable)Session["dtsr"]).Rows[k][0].ToString(), ((DataTable)Session["dtsr"]).Rows[k][1].ToString(), ((DataTable)Session["dtsr"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["dtsr"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["dtsr"]).Rows[k][4].ToString()));
                            //    d.Delete();
                            //}
                            //else
                            //{
                            _Update.SetReturnedSKUPoints(Guid.NewGuid(), ReturnDetailsID, returnid, ((DataTable)Session["dtsr"]).Rows[k][0].ToString(), ((DataTable)Session["dtsr"]).Rows[k][1].ToString(), ((DataTable)Session["dtsr"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["dtsr"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["dtsr"]).Rows[k][4].ToString()));
                            d.Delete();
                            //}

                        }
                    }
                }


                #endregion


                #region SaveComments

                if (((List<RMAComment>)Session["rmacommentsr"]).Count > 0)
                {

                    foreach (var item in ((List<RMAComment>)Session["rmacommentsr"]))
                    {
                        RMAComment lscomments = new RMAComment();
                        lscomments.CommentDate = item.CommentDate;
                        lscomments.ReturnID = returnid;
                        lscomments.RMACommentID = item.RMACommentID;
                        lscomments.UserID = item.UserID;
                        lscomments.Comment = item.Comment;
                        Obj.Rcall.InsertRMACommnt(lscomments);
                    }
                    // Views.Global.rmaComment = null;
                }
                #endregion





                #region InsertImages

                foreach (var item in imglist.Split(new char[] { '\n' }))
                {
                    if (item != null && item != "")
                    {

                        String NameImage = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString() + "\\" + item.ToString();

                        Guid ImageID = _newRMA.SetReturnedImages(Guid.NewGuid(), ReturnDetailsID, NameImage, (Guid)Session["UserID"]);
                    }
                }

                #endregion



                #region Deepak Slip Barcode Print
                //foreach (var n in ((List<String>)Session["_lsSlipPrintSKUNumber"]))
                //{
                //    if (n == SKUNumber)
                //    {
                //        string encd = Obj.Rcall.EncodeCode(n);
                //        Guid userId = (Guid)Session["UserID"];
                //        string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                //        //_retn.GetReturnTblByReturnID(returnid)
                //        var rr = _retn.GetReturnTblByReturnID(returnid).RMANumber;

                //        string nrr = rr.ToString();

                //        List<cSlipInfo> lspr = new List<cSlipInfo>();

                //        lspr = _Update.GetSlipInfo(SKUNumber, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                //        Session["lsSlipInfo"] = _Update.GetSlipInfo(SKUNumber, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                //      //Session["lsSlipInfo"] = _Update.GetSlipInfo(SKUNumber, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);
                //        //  Views.Global.lsSlipInfo = _Update.GetSlipInfo(_lsreturn, Global.arr[i], Obj.Rcall.EncodeCode(Global.arr[i]), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);




                       


                //        //  string script = "window.open('http://192.168.1.16:12/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";






                //        // literal.Text += "a ID='linkcontact' runat='server' href='" + "www.website./pagename.aspx?ID=" + id + "'>contact</a>";
                //    }
                //}
                #endregion



           


                // _Update.SetReturnDetailTbl(lsretundetail[i], Convert.ToInt16(Dquantity), Convert.ToInt16(Rquantity), SKUNumber,ProductName);

            }
            List<cSlipInfo> lspr = new List<cSlipInfo>();
            foreach (var n in ((List<String>)Session["_lsSlipPrintSKUNumber"]))
            {
                string encd = Obj.Rcall.EncodeCode(n);
                Guid userId = (Guid)Session["UserID"];
                string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                //_retn.GetReturnTblByReturnID(returnid)
                var rr = ((List<RMAInfo>)Session["lsReturnGlobalBySRNumber"])[0].RMANumber;

                string nrr = rr.ToString();

                

                lspr.Add(_Update.GetSlipInfo(n, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm));

                //Session["lsSlipInfo"] //= _Update.GetSlipInfo(n, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                //Session["lsSlipInfo"] = _Update.GetSlipInfo(SKUNumber, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);
                //  Views.Global.lsSlipInfo = _Update.GetSlipInfo(_lsreturn, Global.arr[i], Obj.Rcall.EncodeCode(Global.arr[i]), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                //  string script = "window.open('http://192.168.1.16:12/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";
                // literal.Text += "a ID='linkcontact' runat='server' href='" + "www.website./pagename.aspx?ID=" + id + "'>contact</a>";

            }
            Session["lsSlipInfo"] = lspr;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmSlipPrint.aspx','_newtab');", true);
            //Clear the Reasons list from Global Object.
            Obj._ReasonList = new List<Views.ReasonList>();

            //  Response.Redirect("~/Forms/Web Forms/frmRetunDetail.aspx");

          //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmSlipPrint.aspx','_newtab');", true);

            lblMassege.Text = "Success";
          //  ModalPopupExtender1.Show();

            mpePopupForSaveYes.Show();
           // lblUser.Text = "Please Select Any One Option";
           // ModalPopupExtender1.Show();
         
        }
        #endregion

        #region Submit Button Click event
        //protected void btnsubmit_Click(object sender, EventArgs e)
        //{
        //    Views.Global.DT1 = ViewState["dt"] as DataTable;

        //    for (int i = Views.Global.DT1.Rows.Count - 1; i >= 0; i--)
        //    {
        //        DataRow d = Views.Global.DT1.Rows[i];
        //        if (d["SKU"].ToString() == ViewState["SelectedskuName"].ToString() && d["ItemQuantity"].ToString() == ViewState["ItemQuantity"].ToString())
        //            d.Delete();
        //    }

        //    #region DtOperaion
        //    DataRow dr = Views.Global.DT1.NewRow();
        //    dr["SKU"] = ViewState["SelectedskuName"];
        //    dr["ItemQuantity"] = ViewState["ItemQuantity"];

        //    string retun = ViewState["ReturnDetailID"].ToString();

        //    if (ViewState["ReturnDetailID"].ToString() == "")
        //    {
        //        dr["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
        //    }
        //    else
        //    {
        //        dr["ReturnDetailID"] = ViewState["ReturnDetailID"];
        //    }
       
        //    if (brdItemNew.Items.FindByText("Yes").Selected == true)
        //    {
        //        dr["Reason"] = lblitemNew.Text;
        //        dr["Reason_Value"] = "Yes";
        //        dr["Points"] = 100;
        //        Views.Global.DT1.Rows.Add(dr);
        //    }
        //    else if (brdItemNew.Items.FindByText("No").Selected == true)
        //    {
        //        dr["Reason"] = lblitemNew.Text;
        //        dr["Reason_Value"] = "No";
        //        dr["Points"] = 0;
        //        Views.Global.DT1.Rows.Add(dr);
        //    }

        //    DataRow dr1 = Views.Global.DT1.NewRow();
        //    dr1["SKU"] = ViewState["SelectedskuName"];
        //    dr1["ItemQuantity"] = ViewState["ItemQuantity"];
        //    if (ViewState["ReturnDetailID"] == "")
        //    {
        //        dr1["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
        //    }
        //    else
        //    {
        //        dr1["ReturnDetailID"] = ViewState["ReturnDetailID"];
        //    }
        //    if (brdInstalled.Items.FindByText("Yes").Selected == true)
        //    {
        //        dr1["Reason"] = lblInstalled.Text;
        //        dr1["Reason_Value"] = "Yes";
        //        dr1["Points"] = 0;
        //        Views.Global.DT1.Rows.Add(dr1);
        //    }
        //    else if (brdInstalled.Items.FindByText("No").Selected == true)
        //    {
        //        dr1["Reason"] = lblInstalled.Text;
        //        dr1["Reason_Value"] = "No";
        //        dr1["Points"] = 100;
        //        Views.Global.DT1.Rows.Add(dr1);
        //    }

        //    DataRow dr2 = Views.Global.DT1.NewRow();
        //    dr2["SKU"] = ViewState["SelectedskuName"];
        //    dr2["ItemQuantity"] = ViewState["ItemQuantity"];
        //    if (ViewState["ReturnDetailID"] == "")
        //    {
        //        dr2["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
        //    }
        //    else
        //    {
        //        dr2["ReturnDetailID"] = ViewState["ReturnDetailID"];
        //    }
        //    if (brdstatus.Items.FindByText("Yes").Selected == true)
        //    {
        //        dr2["Reason"] = lblstatus.Text;
        //        dr2["Reason_Value"] = "Yes";
        //        dr2["Points"] = 0;
        //        Views.Global.DT1.Rows.Add(dr2);
        //    }
        //    else if (brdstatus.Items.FindByText("No").Selected == true)
        //    {
        //        dr2["Reason"] = lblstatus.Text;
        //        dr2["Reason_Value"] = "No";
        //        dr2["Points"] = 100;
        //        Views.Global.DT1.Rows.Add(dr2);
        //    }

        //    DataRow dr3 = Views.Global.DT1.NewRow();
        //    dr3["SKU"] = ViewState["SelectedskuName"];
        //    dr3["ItemQuantity"] = ViewState["ItemQuantity"];
        //    if (ViewState["ReturnDetailID"] == "")
        //    {
        //        dr3["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
        //    }
        //    else
        //    {
        //        dr3["ReturnDetailID"] = ViewState["ReturnDetailID"];
        //    }
        //    if (brdManufacturer.Items.FindByText("Yes").Selected == true)
        //    {
        //        dr3["Reason"] = lblManifacturerDefective.Text;
        //        dr3["Reason_Value"] = "Yes";
        //        dr3["Points"] = 100;
        //        Views.Global.DT1.Rows.Add(dr3);
        //    }
        //    else if (brdManufacturer.Items.FindByText("No").Selected == true)
        //    {
        //        dr3["Reason"] = lblManifacturerDefective.Text;
        //        dr3["Reason_Value"] = "No";
        //        dr3["Points"] = 0;
        //        Views.Global.DT1.Rows.Add(dr3);
        //    }

        //    DataRow dr4 = Views.Global.DT1.NewRow();
        //    dr4["SKU"] = ViewState["SelectedskuName"];
        //    dr4["ItemQuantity"] = ViewState["ItemQuantity"];
        //    if (ViewState["ReturnDetailID"] == "")
        //    {
        //        dr4["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
        //    }
        //    else
        //    {
        //        dr4["ReturnDetailID"] = ViewState["ReturnDetailID"];
        //    }
        //    if (brdDefecttransite.Items.FindByText("Yes").Selected == true)
        //    {
        //        dr4["Reason"] = lblDefectintransite.Text;
        //        dr4["Reason_Value"] = "Yes";
        //        dr4["Points"] = 100;
        //        Views.Global.DT1.Rows.Add(dr4);
        //    }
        //    else if (brdDefecttransite.Items.FindByText("No").Selected == true)
        //    {
        //        dr4["Reason"] = lblDefectintransite.Text;
        //        dr4["Reason_Value"] = "No";
        //        dr4["Points"] = 0;
        //        Views.Global.DT1.Rows.Add(dr4);
        //    }
        //    #endregion

        //    StatusAndPoints _lsstatusandpoints = new StatusAndPoints();
        //    _lsstatusandpoints.SKUName = ViewState["SelectedskuName"].ToString();
        //    _lsstatusandpoints.Status = ViewState["Sku_status"].ToString();
        //    _lsstatusandpoints.Points = 100;//Views.clGlobal.TotalPoints;
        //    _lsstatusandpoints.NewItemQuantity = Convert.ToInt16(ViewState["ItemQuantity"]);
        //    _lsstatusandpoints.skusequence = Convert.ToInt16(ViewState["SkuQuantitySequence"]);

        //    for (int i = Views.Global.listofstatusAndPoint.Count - 1; i >= 0; i--)
        //    {
        //        if (Views.Global.listofstatusAndPoint[i].SKUName == ViewState["SelectedskuName"] && Views.Global.listofstatusAndPoint[i].NewItemQuantity == Convert.ToInt16(ViewState["ItemQuantity"]))
        //        {
        //            Views.Global.listofstatusAndPoint.RemoveAt(i);
        //        }
        //    }

        //    _lsstatusandpoints.IsMannually = 0;
        //    Views.Global.listofstatusAndPoint.Add(_lsstatusandpoints);

        //    #region SaveSKUReason
        //    Guid SkuReasonID = Guid.NewGuid();
        //    if (txtotherreasons.Text != "")
        //    {
        //        SkuReasonID = Obj.Rcall.UpsertReasons(txtotherreasons.Text);
        //    }
        //    else
        //    {
        //        SkuReasonID = new Guid(ddlotherreasons.SelectedValue);
        //    }
        //    SkuReasonIDSequence lsskusequenceReasons = new SkuReasonIDSequence();
        //    lsskusequenceReasons.ReasonID = SkuReasonID;
        //    lsskusequenceReasons.SKU_sequence = Convert.ToInt16(Convert.ToInt16(ViewState["ItemQuantity"]));
        //    lsskusequenceReasons.SKUName = ViewState["SelectedskuName"].ToString();
        //    Views.Global._lsReasonSKU.Add(lsskusequenceReasons);
        //    #endregion

        //    btnsubmit.Enabled = false;
        //    brdDefecttransite.Enabled = false;
        //    brdManufacturer.Enabled = false;
        //    brdstatus.Enabled = false;
        //    brdInstalled.Enabled = false;
        //    brdItemNew.Enabled = false;

        //    brdItemNew.Items.FindByText("Yes").Selected = false;
        //    brdItemNew.Items.FindByText("No").Selected = false;

        //    brdDefecttransite.Items.FindByText("Yes").Selected = false;
        //    brdDefecttransite.Items.FindByText("No").Selected = false;

        //    brdManufacturer.Items.FindByText("Yes").Selected = false;
        //    brdManufacturer.Items.FindByText("No").Selected = false;

        //    brdstatus.Items.FindByText("Yes").Selected = false;
        //    brdstatus.Items.FindByText("No").Selected = false;

        //    brdInstalled.Items.FindByText("Yes").Selected = false;
        //    brdInstalled.Items.FindByText("No").Selected = false;
        //    lblMassege.Text = "Submit information";
        //}

        protected void btnsubmit_Click(object sender, EventArgs e)
        {


            System.Threading.Thread.Sleep(3000);

            lblMassege.Text = "Process is completed";
            //Views.Global.DT1 = ViewState["dt"] as DataTable;

            //for (int i = Views.Global.DT1.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataRow d = Views.Global.DT1.Rows[i];
            //    if (d["SKU"].ToString() == ViewState["SelectedskuName"].ToString() && d["ItemQuantity"].ToString() == ViewState["ItemQuantity"].ToString())
            //        d.Delete();
            //}
           
            if (Views.Global.flagForDtReturnReason == 0)
            {
                // Creating Columns for DtReturnReason table
                DtReturnReason.Columns.Add("SKU", typeof(string));
                DtReturnReason.Columns.Add("Reason", typeof(string));
                DtReturnReason.Columns.Add("Reason_Value", typeof(string));
                DtReturnReason.Columns.Add("Points", typeof(int));
                DtReturnReason.Columns.Add("ItemQuantity", typeof(string));
                // DtReturnReason.Columns.Add("ReturnedSKUID", typeof(Guid));
                // DtReturnReason.Columns.Add("ReturnDetailID", typeof(Guid));
                Session["dtsr"] = DtReturnReason;
               
            }


            Views.Global.flagForDtReturnReason = 1;





            #region DtOperaion
            DataRow dr = ((DataTable)Session["dtsr"]).NewRow();
            dr["SKU"] = ViewState["SelectedskuName"];
            dr["ItemQuantity"] = ViewState["ItemQuantity"];

            //  string retun = ViewState["ReturnDetailID"].ToString();

            //if (ViewState["ReturnDetailID"] == "")
            //{
            //    dr["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}
            //dr[""]
            if (brdItemNew.Items.FindByText("Yes").Selected == true)
            {
                dr["Reason"] = lblitemNew.Text;
                dr["Reason_Value"] = "Yes";
                dr["Points"] = 100;
                ((DataTable)Session["dtsr"]).Rows.Add(dr);
            }
            else if (brdItemNew.Items.FindByText("No").Selected == true)
            {
                dr["Reason"] = lblitemNew.Text;
                dr["Reason_Value"] = "No";
                dr["Points"] = 0;
                ((DataTable)Session["dtsr"]).Rows.Add(dr);
            }

            DataRow dr1 = ((DataTable)Session["dtsr"]).NewRow();
            dr1["SKU"] = ViewState["SelectedskuName"];
            dr1["ItemQuantity"] = ViewState["ItemQuantity"];
            //if (ViewState["ReturnDetailID"] == "")
            //{
            //    dr1["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr1["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}
            if (brdInstalled.Items.FindByText("Yes").Selected == true)
            {
                dr1["Reason"] = lblInstalled.Text;
                dr1["Reason_Value"] = "Yes";
                dr1["Points"] = 0;
                ((DataTable)Session["dtsr"]).Rows.Add(dr1);
            }
            else if (brdInstalled.Items.FindByText("No").Selected == true)
            {
                dr1["Reason"] = lblInstalled.Text;
                dr1["Reason_Value"] = "No";
                dr1["Points"] = 100;
                ((DataTable)Session["dtsr"]).Rows.Add(dr1);
            }

            DataRow dr2 = ((DataTable)Session["dtsr"]).NewRow();
            dr2["SKU"] = ViewState["SelectedskuName"];
            dr2["ItemQuantity"] = ViewState["ItemQuantity"];
            //if (ViewState["ReturnDetailID"] == "")
            //{
            //    dr2["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr2["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}
            if (brdstatus.Items.FindByText("Yes").Selected == true)
            {
                dr2["Reason"] = lblstatus.Text;
                dr2["Reason_Value"] = "Yes";
                dr2["Points"] = 0;
                ((DataTable)Session["dtsr"]).Rows.Add(dr2);
            }
            else if (brdstatus.Items.FindByText("No").Selected == true)
            {
                dr2["Reason"] = lblstatus.Text;
                dr2["Reason_Value"] = "No";
                dr2["Points"] = 100;
                ((DataTable)Session["dtsr"]).Rows.Add(dr2);
            }

            DataRow dr3 = ((DataTable)Session["dtsr"]).NewRow();
            dr3["SKU"] = ViewState["SelectedskuName"];
            dr3["ItemQuantity"] = ViewState["ItemQuantity"];
            //if (ViewState["ReturnDetailID"] == "")
            //{
            //    dr3["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr3["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}
            if (brdManufacturer.Items.FindByText("Yes").Selected == true)
            {
                dr3["Reason"] = lblManifacturerDefective.Text;
                dr3["Reason_Value"] = "Yes";
                dr3["Points"] = 100;
                ((DataTable)Session["dtsr"]).Rows.Add(dr3);
            }
            else if (brdManufacturer.Items.FindByText("No").Selected == true)
            {
                dr3["Reason"] = lblManifacturerDefective.Text;
                dr3["Reason_Value"] = "No";
                dr3["Points"] = 0;
                ((DataTable)Session["dtsr"]).Rows.Add(dr3);
            }

            DataRow dr4 = ((DataTable)Session["dtsr"]).NewRow();
            dr4["SKU"] = ViewState["SelectedskuName"];
            dr4["ItemQuantity"] = ViewState["ItemQuantity"];
            //if (ViewState["ReturnDetailID"] == "")
            //{
            //    dr4["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr4["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}
            if (brdDefecttransite.Items.FindByText("Yes").Selected == true)
            {
                dr4["Reason"] = lblDefectintransite.Text;
                dr4["Reason_Value"] = "Yes";
                dr4["Points"] = 100;
                ((DataTable)Session["dtsr"]).Rows.Add(dr4);
            }
            else if (brdDefecttransite.Items.FindByText("No").Selected == true)
            {
                dr4["Reason"] = lblDefectintransite.Text;
                dr4["Reason_Value"] = "No";
                dr4["Points"] = 0;
                ((DataTable)Session["dtsr"]).Rows.Add(dr4);
            }
            #endregion

            StatusAndPoints _lsstatusandpoints = new StatusAndPoints();
            _lsstatusandpoints.SKUName = ViewState["SelectedskuName"].ToString();
            _lsstatusandpoints.Status = ViewState["Sku_status"].ToString();
            _lsstatusandpoints.Points = 100;//Views.clGlobal.TotalPoints;
            _lsstatusandpoints.NewItemQuantity = Convert.ToInt16(ViewState["ItemQuantity"]);
            _lsstatusandpoints.skusequence = Convert.ToInt16(ViewState["SkuQuantitySequence"]);

            //for (int i = Views.Global.listofstatusAndPoint.Count - 1; i >= 0; i--)
            //{
            //    if (Views.Global.listofstatusAndPoint[i].SKUName == ViewState["SelectedskuName"] && Views.Global.listofstatusAndPoint[i].NewItemQuantity == Convert.ToInt16(ViewState["ItemQuantity"]))
            //    {
            //        Views.Global.listofstatusAndPoint.RemoveAt(i);
            //    }
            //}

            _lsstatusandpoints.IsMannually = 0;

            List<StatusAndPoints> mylist = new List<StatusAndPoints>();

            mylist = (List<StatusAndPoints>)Session["listofstatusAndPoint"];

            ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Add(_lsstatusandpoints);

            #region SaveSKUReason



            Guid SkuReasonID = Guid.NewGuid();
            if (txtotherreasons.Text != "")
            {
                SkuReasonID = Obj.Rcall.UpsertReasons(txtotherreasons.Text);
            }
            else
            {
                SkuReasonID = new Guid(ddlotherreasons.SelectedValue);
            }
            SkuReasonIDSequence lsskusequenceReasons = new SkuReasonIDSequence();
            lsskusequenceReasons.ReasonID = SkuReasonID;
            lsskusequenceReasons.SKU_sequence = Convert.ToInt16(Convert.ToInt16(ViewState["ItemQuantity"]));
            lsskusequenceReasons.SKUName = ViewState["SelectedskuName"].ToString();



            ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"]).Add(lsskusequenceReasons);




            #endregion

            btnsubmit.Enabled = false;
            brdDefecttransite.Enabled = false;
            brdManufacturer.Enabled = false;
            brdstatus.Enabled = false;
            brdInstalled.Enabled = false;
            brdItemNew.Enabled = false;

            brdItemNew.Items.FindByText("Yes").Selected = false;
            brdItemNew.Items.FindByText("No").Selected = false;

            brdDefecttransite.Items.FindByText("Yes").Selected = false;
            brdDefecttransite.Items.FindByText("No").Selected = false;

            brdManufacturer.Items.FindByText("Yes").Selected = false;
            brdManufacturer.Items.FindByText("No").Selected = false;

            brdstatus.Items.FindByText("Yes").Selected = false;
            brdstatus.Items.FindByText("No").Selected = false;

            brdInstalled.Items.FindByText("Yes").Selected = false;
            brdInstalled.Items.FindByText("No").Selected = false;
            lblMassege.Text = "Submit information";
            mpePopupForSubmitYes.Show();
        }
        #endregion

        #region Email Button Click Event
       
        #endregion

        #region
        #endregion

        private String _TextBox(String TextBoxID, GridView GridViewName)
        {
            String _return = "";
            try
            {
                TextBox txt = (TextBox)GridViewName.Rows[0].FindControl(TextBoxID);
                _return = txt.Text;
            }
            catch (Exception)
            { }
            return _return;
        }

        //txtSKU_textChanged for textsuggest SKU number and ProductName.
        protected void txtSKU_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
                TextBox t = (TextBox)currentRow.FindControl("txtsku");
                string rt = t.Text;
                TextBox txt = (TextBox)currentRow.FindControl("txtproductame");
                txt.Text = productcategory(rt, 0);
            }
        }

        //txtReason Click open popup Window to check RMAReasons.
        protected void txtreasons_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;

                TextBox sku = (TextBox)currentRow.FindControl("txtsku");
                Obj.RowID = currentRow.RowIndex;

                string url = "frmPopupForEditRMAReasons.aspx?Category=" + productcategory(sku.Text, 1) + "";

                string s = "window.open('" + url + "', 'popup_window', 'width=500,height=300,left=300,top=300,resizable=yes');";
                ScriptManager.RegisterStartupScript(this, Page.GetType(), "Script", s, true);
            }
        }

       
        public string productcategory(string sku, int flag)
        {
            string _returnString = "";
            List<string> lsTrackingTbl = Obj.call._skulist(sku);
            try
            {
                if (flag == 0)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _returnString = TrackItm.ToString().Split(new char[] { '#' })[1];
                    }
                }
                else if (flag == 1)
                {
                    foreach (var TrackItm in lsTrackingTbl)
                    {
                        _returnString = TrackItm.ToString().Split(new char[] { '#' })[2];
                    }
                }

            }
            catch (Exception)
            { }
            return _returnString;
        }

        protected void ddlotherreasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlotherreasons.SelectedIndex == 0)
            {
                txtotherreasons.Text = "";
            }
            else
            {              
            }
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            //#region Uploading single Image
            //string updir = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString();
            //GridViewRow gvRow = (sender as Button).NamingContainer as GridViewRow;
            //FileUpload fupload = gvRow.FindControl("FileUpload1") as FileUpload;

            //String fileNeme = fupload.FileName.ToString();
            //fileNeme = RemoveSpecialCharacters(Convert.ToString(DateTime.Now)) + fileNeme;

            //fupload.SaveAs(@"C:\Images\" + fileNeme);

            ////method to upload file to the FTP server.
            //ExtensionMethods.Upload(@"\\192.168.1.172\Macintosh HD\ftp_share\RGAImages", "mediaserver", "kraus2013", "C:\\Images\\" + fileNeme.ToString(), fupload.FileBytes);
            ////delete file from the local.
            //File.Delete(@"C:\Images\" + fileNeme.ToString());

            //Label lbl = gvRow.FindControl("lblImagesName") as Label;
            //lbl.Text = lbl.Text + "\n" + fileNeme.ToString();
            //#endregion        

            #region Uploading Multiple Images
            string updir = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString();
            GridViewRow gvRow = (sender as Button).NamingContainer as GridViewRow;
            FileUpload fupload = gvRow.FindControl("FileUpload1") as FileUpload;
            bool hasfile = fupload.HasFile;
            //int c=fupload.FileName.Count();
            //Label Image = (gvRow.FindControl("lblNoImages") as Label);



            bool folderExists = Directory.Exists(@"C:\Images1\");
            if (folderExists)
            {
                foreach (string directory in Directory.GetDirectories(@"C:\Images1\"))
                {
                    string filepath = directory;
                    foreach (string file in Directory.GetFiles(filepath))
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(directory);
                }

                foreach (string file in Directory.GetFiles(@"C:\Images1\"))
                {
                    File.Delete(file);
                }

            }
            else
            {
                Directory.CreateDirectory(@"C:\Images1\");
            }
            bool folderExists1 = Directory.Exists(@"C:\Images\");

            if (folderExists1)
            {

            }
            else
            {
                Directory.CreateDirectory(@"C:\Images\");
            }

            HttpFileCollection fileCollection = Request.Files;

            int count = 0;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                string fileName = Path.GetFileName(uploadfile.FileName);
                fileName = "img" + RemoveSpecialCharacters(Convert.ToString(DateTime.Now) + fileName);
                if (uploadfile.ContentLength > 0)
                {
                    count++;
                    uploadfile.SaveAs(@"C:\Images1\" + fileName);
                    #region  Resizing of Images1
                    String filepath = @"C:\Images1\" + fileName;
                    ResizeImage(300, filepath, @"C:\Images\" + fileName);
                    #endregion

                    byte[] bytes = File.ReadAllBytes(@"C:\Images\" + fileName);
                    // ExtensionMethods.Upload(@"\\192.168.1.172\Macintosh HD\ftp_share\RGAImages", "mediaserver", "kraus2013", "C:\\Images\\" + fileName.ToString(), bytes);
                    ExtensionMethods.Upload(@"ftp://fileshare.kraususa.com", "rgauser", "rgaICG2014", "C:\\Images\\" + fileName.ToString(), bytes);
                    File.Delete(@"C:\Images\" + fileName.ToString());
                    File.Delete(@"C:\Images1\" + fileName.ToString());
                    Label lbl = gvRow.FindControl("lblImagesName") as Label;
                    lbl.Text = lbl.Text + "\n" + fileName.ToString();
                    mpePopupForImageYes.Show();
                }
            }
            Directory.Delete(@"C:\Images1\");
            #endregion

            string ImageNo = (gvRow.FindControl("txtImageCount") as LinkButton).Text;
            int img = Convert.ToInt16(ImageNo.Split(new char[] { ' ' })[0]);

            int noOfImages;
            noOfImages = img + count;

            string displayImageCount = noOfImages.ToString() + " " + "Image(s)";
            // gvRow.Cells[8].Text = displayImageCount.ToString();


            foreach (GridViewRow row in gvReturnDetails.Rows)
            {
                if (gvRow.RowIndex == row.RowIndex)
                {
                    (row.FindControl("txtImageCount") as LinkButton).Text = displayImageCount.ToString();
                }
                else
                {
                    //string GuidReturnDetail = (row.FindControl("lblguid") as Label).Text;

                    // List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(GuidReturnDetail));
                    //string PresentCount = (row.FindControl("txtImageCount") as LinkButton).Text;

                    //string ImageCount = Convert.ToString(lsImages2.Count);

                    //(row.FindControl("txtImageCount") as LinkButton).Text = PresentCount;
                }
            }

            #region Showing Image Count
            //Label Image = (gvRow.FindControl("lblNoImages") as Label);
            //string ImageNo = gvRow.Cells[8].Text;
            //int img= Convert.ToInt16(ImageNo.Split(new char[] { ' ' })[0]);

            //int noOfImages;
            //noOfImages = img + fileCollection.Count;
            //(gvRow.FindControl("lblNoImages") as Label).Text = noOfImages.ToString();
            #endregion

            //#region Uploading single Image
            //string updir = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString();
            //GridViewRow gvRow = (sender as Button).NamingContainer as GridViewRow;
            //FileUpload fupload = gvRow.FindControl("FileUpload1") as FileUpload;

            //String fileNeme1 = fupload.FileName.ToString();

            //string fileNeme = RemoveSpecialCharacters(fileNeme1);
            //fileNeme = RemoveSpecialCharacters(Convert.ToString(DateTime.Now)) + fileNeme;


            //#region  Resizing of Images1
            //bool folderExists = Directory.Exists(@"D:\Images\");
            //if (!folderExists)
            //    Directory.CreateDirectory(@"D:\Images\");
            //fupload.SaveAs(@"D:\Images\" + fileNeme);
            //String filepath = @"D:\Images\" + fileNeme;
            //// ResizeImage(100, filepath, @"C:\Images\" + fileNeme);
            //ResizeImage(300, filepath, @"C:\Images\" + fileNeme);
            //#endregion

            ////fupload.SaveAs(@"C:\Images\" + fileNeme);
            //byte[] bytes = File.ReadAllBytes(@"C:\Images\" + fileNeme);
            ////method to upload file to the FTP server.
            //ExtensionMethods.Upload(@"\\192.168.1.172\Macintosh HD\ftp_share\RGAImages", "mediaserver", "kraus2013", "C:\\Images\\" + fileNeme.ToString(), bytes);
            ////delete file from the local.
            //File.Delete(@"C:\Images\" + fileNeme.ToString());
            //File.Delete(@"D:\Images\" + fileNeme.ToString());

            //Directory.Delete(@"D:\Images\");

            //Label lbl = gvRow.FindControl("lblImagesName") as Label;
            //lbl.Text = lbl.Text + "\n" + fileNeme.ToString();
            //#endregion        

        }

        #region Resizing of Images2
        //function to resize image
        public static void ResizeImage(int size, string filePath, string saveFilePath)
        {
            //variables for image dimension/scale
            double newHeight = 0;
            double newWidth = 0;
            double scale = 0;

            //create new image object
            Bitmap curImage = new Bitmap(filePath);

            //Determine image scaling
            if (curImage.Height > curImage.Width)
            {
                scale = Convert.ToSingle(size) / curImage.Height;
            }
            else
            {
                scale = Convert.ToSingle(size) / curImage.Width;
            }
            if (scale < 0 || scale > 1) { scale = 1; }

            //New image dimension
            newHeight = Math.Floor(Convert.ToSingle(curImage.Height) * scale);
            newWidth = Math.Floor(Convert.ToSingle(curImage.Width) * scale);

            //Create new object image
            Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
            Graphics imgDest = Graphics.FromImage(newImage);
            imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            imgDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            imgDest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters param = new EncoderParameters(1);
            param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            //Draw the object image
            imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);

            //Save image file
            newImage.Save(saveFilePath, info[1], param);

            //Dispose the image objects
            curImage.Dispose();
            newImage.Dispose();
            imgDest.Dispose();
        }
        #endregion

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        protected void FileUpload1_Load(object sender, EventArgs e)
        {
            GridViewRow gvRow = (sender as FileUpload).NamingContainer as GridViewRow;
            Button btnupload = gvRow.FindControl("btnUpdate") as Button;

            //string ImageNo = (gvRow.FindControl("lblNoImages") as Label).Text;

            //int NoImgages = Convert.ToInt16(ImageNo.Split(new char[] { ' ' })[0]);

            //(gvRow.FindControl("lblNoImages") as Label).Text = NoImgages + 1 + " " + "Image(s)";

            btnupload.Enabled = true;
        }

     

        protected void brdItemNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brdItemNew.Items.FindByText("Yes").Selected == true)
            {
                ViewState["Sku_status"] = "Refund";

                brdDefecttransite.Enabled = false;
                brdManufacturer.Enabled = false;
                brdstatus.Enabled = false;
                brdInstalled.Enabled = false;

                brdDefecttransite.Items.FindByText("Yes").Selected = false;
                brdDefecttransite.Items.FindByText("No").Selected = false;

                brdManufacturer.Items.FindByText("Yes").Selected = false;
                brdManufacturer.Items.FindByText("No").Selected = false;

                brdstatus.Items.FindByText("Yes").Selected = false;
                brdstatus.Items.FindByText("No").Selected = false;

                brdInstalled.Items.FindByText("Yes").Selected = false;
                brdInstalled.Items.FindByText("No").Selected = false;



            }
            else if (brdItemNew.Items.FindByText("No").Selected == true)
            {
                ViewState["Sku_status"] = "Deny";

                brdDefecttransite.Enabled = true;
                brdManufacturer.Enabled = true;
                brdstatus.Enabled = true;
                brdInstalled.Enabled = true;
            }
        }


        protected void lkbtnPath_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmHomePage.aspx");
        }

        protected void brdDefecttransite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brdDefecttransite.Items.FindByText("Yes").Selected == true)
            {

            }
            else if (brdDefecttransite.Items.FindByText("No").Selected == true)
            {

            }
        }

        protected void brdManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brdManufacturer.Items.FindByText("Yes").Selected == true)
            {

            }
            else if (brdManufacturer.Items.FindByText("No").Selected == true)
            {
                //ViewState["Sku_status"] = "Deny";
            }
        }

        protected void brdstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brdstatus.Items.FindByText("Yes").Selected == true)
            {

            }
            else if (brdstatus.Items.FindByText("No").Selected == true)
            {

            }
        }

        protected void brdInstalled_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (brdInstalled.Items.FindByText("Yes").Selected == true)
            {
                ViewState["Sku_status"] = "Deny";

                brdDefecttransite.Enabled = false;
                brdManufacturer.Enabled = false;
                brdstatus.Enabled = false;


                brdDefecttransite.Items.FindByText("Yes").Selected = false;
                brdDefecttransite.Items.FindByText("No").Selected = false;

                brdManufacturer.Items.FindByText("Yes").Selected = false;
                brdManufacturer.Items.FindByText("No").Selected = false;

                brdstatus.Items.FindByText("Yes").Selected = false;
                brdstatus.Items.FindByText("No").Selected = false;



            }
            else if (brdInstalled.Items.FindByText("No").Selected == true)
            {
                brdDefecttransite.Enabled = true;
                brdManufacturer.Enabled = true;
                brdstatus.Enabled = true;

            }
        }

        //protected void btnOk_Click(object sender, EventArgs e)
        //{

        //    // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

        //    Response.Redirect(@"~\Forms\Web Forms\frmRetunDetail.aspx");
        //}

        protected void txtcomment_TextChanged(object sender, EventArgs e)
        {
            if (txtcomment.Text == "")
            {
                btnComment.Visible = false;
            }
            else
            {
                btnComment.Visible = true;
            }


        }

        protected void txtImageCount_Click(object sender, EventArgs e)
        {
            ///Show Image Popup
            this.Controls.Add(new LiteralControl("<div id='myP' style=' border-radius: 11px 0 0 11px;  border: 1px solid; position : absolute; color:#179090; left : 50px; right : 50px; top :49px;width:auto !important; max-width:1240px;height:430px;overflow: auto;'>"));
            this.Controls.Add(new LiteralControl("<b><input type='submit' align='right' onclick='demoDisplay()' value='Close' ><table id='tblmg' height='100%' width='100%' bgcolor='#00FF00'><tr><td bgcolor='#8DC6FF'>"));

            //<input type='image' src='../Themes/Images/close.jpg'  align='right' width='48px' height='48px' onclick='demoDisplay()' style='background-color: #FF0000' alt='Close' fontsize='30'>
            //for (int i = 0; i < 4; i++)
            //{
            //    string path = "sample.jpg";
            //    this.Controls.Add(new LiteralControl(" <img src='../../images/" + path + "' alt='Deeeepak' height='400' width='400'>"));
            //}


            GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;

            string ReturndetailID = (gvRow.FindControl("lblguid") as Label).Text;

            //for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            //{
            //    int flg = 1;

            //    string ReturnROWID = Views.Global.ReteunGlobal.RGAROWID;

            //    string GuidReturnDetail = (gvReturnDetails.Rows[i].FindControl("lblguid") as Label).Text;
            ///////////   lblImagesFor.Text = "Sorry! Images for GRA Detail Number : " + ReturnROWID + " not found!";
            List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(ReturndetailID));

            if (lsImages2.Count > 0)
            {

                List<String> lsImages = new List<string>();
                String ImgServerString = System.Configuration.ConfigurationManager.AppSettings["ImageServerPath"].ToString();
                foreach (var Imaitem in lsImages2)
                {
                    //lsImages.Add("~/images/"+Imaitem.Split(new char[] { '\\' }).Last().ToString());
                    lsImages.Add(ImgServerString.Replace("#{ImageName}#", Imaitem.Split(new char[] { '\\' }).Last().ToString()));
                }
                //foreach (var Imaitem in lsImages2)
                //{
                //    //lsImages.Add("~/images/"+Imaitem.Split(new char[] { '\\' }).Last().ToString());
                //    lsImages.Add(ImgServerString.Replace("#{ImageName}#", Imaitem.Split(new char[] { '\\' }).Last().ToString()));
                //}
                /////192.168.1.172/Macintosh HD/ftp_share/RGAImages/
                if (lsImages2.Count > 0)
                {
                    ////////// lblImagesFor.Text = "Images for GRA Detail Number : " + ReturnROWID;
                    for (int j = 0; j < lsImages2.Count; j++)
                    {
                        // flg = 2;
                        string path = lsImages[j].ToString();
                        this.Controls.Add(new LiteralControl(" <img src='" + path + "' height='400' width='400'>"));
                    }
                }
                else
                {

                }

            }

            else
            {
                this.Controls.Add(new LiteralControl("<b>Image not found"));
            }
            this.Controls.Add(new LiteralControl("</td></tr></table>"));
            this.Controls.Add(new LiteralControl("</div>"));

            //}
        }

        protected void txtponumber_TextChanged(object sender, EventArgs e)
        {
            //List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomer(txtponumber.Text);

            //if (lsCustomeronfo.Count > 0)
            //{
            //    txtponumber.Text = lsCustomeronfo[0].PONumber;
            //    // txtcustomeraddress.Text = lsCustomeronfo[0].Address1;
            //    // txtcountry.Text = lsCustomeronfo[0].Country;
            //    // txtcity.Text = lsCustomeronfo[0].City;
            //    // txtstate.Text = lsCustomeronfo[0].State;
            //    // txtzipcode.Text = lsCustomeronfo[0].ZipCode;
            //    txtvendorName.Text = lsCustomeronfo[0].VendorName;
            //    txtvendornumber.Text = lsCustomeronfo[0].VendorNumber;
            //    txtRMAnumber.Text = lsCustomeronfo[0].OrderNumber;
            //    txtcustomerName.Text = lsCustomeronfo[0].CustomerName1;
            //    //txtrganumber.Text=lsCustomeronfo[0]
            //    txtshipmentnumber.Text = lsCustomeronfo[0].ShipmentNumber;
            //   // TextBox1.Text = lsCustomeronfo[0].CallTag;
            //    txtordernumber.Text = lsCustomeronfo[0].OrderNumber;
            //    DateTime dt = lsCustomeronfo[0].OrderDate;
            //    txtorderdate.Text = dt.ToString("MM/dd/yyyy hh:mm tt");

            //}
        }

        protected void gvReturnDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnOkForSaveYes_Click(object sender, EventArgs e)
        {       
            Response.Redirect("~/Forms/Web Forms/frmHomePage.aspx");          
        }


    }
}