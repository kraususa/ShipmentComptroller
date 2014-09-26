using PackingClassLibrary.CustomEntity.SMEntitys.RGA;
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
using PackingClassLibrary.Commands.SMcommands.RGA;
using Ionic.Zip;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmReturnEdit : System.Web.UI.Page
    {
        List<Return> _lsreturn = new List<Return>();
        cmdReturn _retn = new cmdReturn();
       //Create Object of modelRertunUpdate.
        modelReaturnUpdate _Update = new modelReaturnUpdate();

        Models.modelReturn _newRMA = new Models.modelReturn();

        DataTable dt = new DataTable();

      //  DataTable DT1 = new DataTable(); 
       /// <summary>
       /// /fmglbgbl;
       /// </summary>

        DataTable DtReturnReason = new DataTable();

      //  List<SkuReasonIDSequence> _lsReasonSKU = new List<SkuReasonIDSequence>();

        List<ReturnedSKUPoints> listofstatus = new List<ReturnedSKUPoints>();

      //  Return retuen = new Return();

        Boolean NonPo = true;

       // List<StatusAndPoints> listofstatusAndPoint = new List<StatusAndPoints>();

       // List<SKUReason> lsSKUReasons = new List<SKUReason>();

        string rga;

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



        //on Page Load Event Display all information on the Form for Update.
        protected void Page_Load(object sender, EventArgs e)
        {
           // ShowComments();
            if (!IsPostBack)
            {
                Session["_lsSlipPrintSKUNumber"] = new List<string>();
                rga = Request.QueryString["RGAROWID"].ToString();
                Session["RGAROIDE"] = rga;
                DtReturnReason.Columns.Add("SKU", typeof(string));
                DtReturnReason.Columns.Add("Reason", typeof(string));
                DtReturnReason.Columns.Add("Reason_Value", typeof(string));
                DtReturnReason.Columns.Add("Points", typeof(int));
                DtReturnReason.Columns.Add("ItemQuantity", typeof(string));

                DtReturnReason.Columns.Add("ReturnedSKUID", typeof(Guid));
                DtReturnReason.Columns.Add("ReturnDetailID", typeof(Guid));

                display(Request.QueryString["RGAROWID"].ToString());
                FillReturnDetails(Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString()));
                List<StatusAndPoints> listofstatusAndPoint = new List<StatusAndPoints>();
                Session["listofstatusAndPoint"] = listofstatusAndPoint;

                List<SkuReasonIDSequence> lsskureason=new List<SkuReasonIDSequence>();
                Session["_lsReasonSKU"] = lsskureason;
               
       
                fillReturnDetailAndStatus();
                Session["DT1"] = DtReturnReason;
                GetLatestUser();


              
                fillReturnedstatusandpoit();


                Obj.ReasonsIDs.PropertyChanged +=ReasonsIDs_PropertyChanged;
                Obj._ReasonList = new List<Views.ReasonList>();
                fillddlotherReasons();
               // GetMainReason(Request.QueryString["RGAROWID"].ToString());
                fillforprint();
                SavedShowComments();
            }
           // ShowComments();
        }


        public void fillforprint()
        {

            foreach (var item in (List<ReturnDetail>)Session["ReturnDetails"])
            {
                if (item.SKU_Qty_Seq == 1)
                {
                    ((List<String>)Session["_lsSlipPrintSKUNumber"]).Add(item.SKUNumber);
                }
            }
        
        }




        public void GetLatestUser()
        {
            //UserMaster user = new UserMaster();
            try
            {
                Guid userId = (Guid)((Return)Session["ReteunGlobal"]).CreatedBy;
                Obj.Rcall.GetUserInfobyUserID(userId);
                lblUserName.Text = Obj.Rcall.GetUserInfobyUserID(userId).UserFullName;
                lblLastTime.Text = ((Return)Session["ReteunGlobal"]).UpdatedDate.ToString("MM/dd/yyyy hh:mm tt");
            }
            catch (Exception)
            {
               
            }

           
        }


        public void fillReturnedstatusandpoit()
        {
           // retuen = Obj.Rcall.ReturnByRGAROWID(RGA)[0];
            listofstatus = Obj.Rcall.ReturnedSKUansPoints(((Return)Session["ReteunGlobal"]).ReturnID);

            for (int i = 0; i < listofstatus.Count; i++)
            {
                DataRow dr0 = DtReturnReason.NewRow();
                dr0["SKU"] = listofstatus[i].SKU;
                dr0["Reason"] = listofstatus[i].Reason;
                dr0["Reason_Value"] = listofstatus[i].Reason_Value;
                dr0["Points"] = listofstatus[i].Points;
                dr0["ItemQuantity"] = listofstatus[i].SkuSequence;
                dr0["ReturnedSKUID"] = listofstatus[i].ID;
                dr0["ReturnDetailID"] = listofstatus[i].ReturnDetailID;
                DtReturnReason.Rows.Add(dr0);
            }



        }

        public void fillReturnDetailAndStatus()
        {
            List<ReturnDetail> retuen = Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"]);
            Session["lsSKUReasons"] = Obj.Rcall.SKUReasonsByReturnDetails(retuen);



            //foreach (var item in ((List<SKUReason>)Session["lsSKUReasons"]))
            //{

            //    SkuReasonIDSequence lsskusequenceReasons = new SkuReasonIDSequence();
            //    lsskusequenceReasons.ReasonID = item.ReasonID;
            //    lsskusequenceReasons.SKU_sequence = item.SKU_sequence;
            //    lsskusequenceReasons.SKUName = item.SKUName;
            //    ((List<SkuReasonIDSequence>)Session["_lsReasonSKU"]).Add(lsskusequenceReasons);
            //}

            for (int i = 0; i < Obj._lsReturnDetails.Count; i++)
            {
                StatusAndPoints _lsstatusandpoints = new StatusAndPoints();
                if (Obj._lsReturnDetails[i].SKU_Status != "")
                {
                    _lsstatusandpoints.SKUName = Obj._lsReturnDetails[i].SKUNumber;
                    _lsstatusandpoints.Status = Obj._lsReturnDetails[i].SKU_Status;
                    _lsstatusandpoints.Points = Obj._lsReturnDetails[i].SKU_Reason_Total_Points;
                    _lsstatusandpoints.IsMannually = Obj._lsReturnDetails[i].IsManuallyAdded;
                    _lsstatusandpoints.IsScanned = Obj._lsReturnDetails[i].IsSkuScanned;
                    _lsstatusandpoints.NewItemQuantity = Obj._lsReturnDetails[i].SKU_Sequence;
                    _lsstatusandpoints.skusequence = Obj._lsReturnDetails[i].SKU_Qty_Seq;
                    ((List<StatusAndPoints>) Session["listofstatusAndPoint"]).Add(_lsstatusandpoints);
                }

            }
           // ViewState["ReturnStatus"] = listofstatusAndPoint;
        }


        public void fillddlotherReasons()
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
        }

        //public void GetMainReason(String RGA)
        //{
        //    Return retuen = Obj.Rcall.ReturnByRGAROWID(RGA)[0];
        //    string[] ReturnReasons = retuen.ReturnReason.Split(new char[] { '.' });
        //    int flag = 0;

        //    for (int i = 0; i < ReturnReasons.Count(); i++)
        //    {
        //        flag = 0;

        //        if (ReturnReasons[i].Trim() == chkduplicate.Text.Split(new char[] { '.' })[0])
        //        {
        //            chkduplicate.Checked = true;
        //            flag = 1;
        //        }
        //        if (ReturnReasons[i].Trim() == chkitemdamaged.Text.Split(new char[] { '.' })[0])
        //        {
        //            chkitemdamaged.Checked = true;
        //            flag = 1;
        //        }
        //        if (ReturnReasons[i].Trim() == chkitemdifferent.Text.Split(new char[] { '.' })[0])
        //        {
        //            chkitemdifferent.Checked = true;
        //            flag = 1;
        //        }
        //        if (ReturnReasons[i].Trim() == chkitemordered.Text.Split(new char[] { '.' })[0])
        //        {
        //            chkitemordered.Checked = true;
        //            flag = 1;
        //        }
        //        if (ReturnReasons[i].Trim() == chknotsatisfied.Text.Split(new char[] { '.' })[0])
        //        {
        //            chknotsatisfied.Checked = true;
        //            flag = 1;
        //        }
        //        if (ReturnReasons[i].Trim() == chkwrongitem.Text.Split(new char[] { '.' })[0])
        //        {
        //            chkwrongitem.Checked = true;
        //            flag = 1;
        //        }

        //        if (flag == 0)
        //        {
        //            txtotherreasons.Text = ReturnReasons[i].Trim();
        //            ddlotherreasons.Text = ReturnReasons[i].Trim();
        //        }

        //    }

           
        //}

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


        protected void lkbtnPath_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmHomePage.aspx");
        }

        //Return Boolean value by passing String RGA.
        //display all values Of Return information on all textboxes for Update. 
        public Boolean display(String RGA)
        {
            Boolean _flag = false;
            try
            {
               Session["ReteunGlobal"] = Obj.Rcall.ReturnByRGAROWID(RGA)[0];



               txtcustomerName.Text = ((Return)Session["ReteunGlobal"]).CustomerName1;
               txtponumber.Text = ((Return)Session["ReteunGlobal"]).PONumber;
               txtCustomerAddress.Text = ((Return)Session["ReteunGlobal"]).Address1;
               txtCustomerCity.Text = ((Return)Session["ReteunGlobal"]).City;
               txtCustomerState.Text = ((Return)Session["ReteunGlobal"]).State;
               txtCustomerZip.Text = ((Return)Session["ReteunGlobal"]).ZipCode;

              //  txtponumber.Text =Convert.ToString(retuen.UpdatedBy);

               txtvendorName.Text = ((Return)Session["ReteunGlobal"]).VendoeName;
               txtRMAnumber.Text = ((Return)Session["ReteunGlobal"]).RMANumber;
               txtshipmentnumber.Text = ((Return)Session["ReteunGlobal"]).ShipmentNumber;
               txtvendornumber.Text = ((Return)Session["ReteunGlobal"]).VendorNumber;
               txtrganumber.Text = ((Return)Session["ReteunGlobal"]).RGAROWID;
               txtreturndate.Text = Convert.ToString(((Return)Session["ReteunGlobal"]).ReturnDate.ToShortDateString());
              // txtorderdate.Text = Convert.ToString(((Return)Session["ReteunGlobal"]).OrderDate.ToShortDateString());
              // txtordernumber.Text = ((Return)Session["ReteunGlobal"]).OrderNumber;
               ddlstatus.SelectedIndex = Convert.ToInt16(((Return)Session["ReteunGlobal"]).RMAStatus);
               ddldecision.SelectedIndex = Convert.ToInt16(((Return)Session["ReteunGlobal"]).Decision);
               txtCalltag.Text = ((Return)Session["ReteunGlobal"]).CallTag;
               if (((Return)Session["ReteunGlobal"]).ProgressFlag == 1)
                {
                    chkflag.Checked = true;
                }



                _flag = true;
            }
            catch (Exception)
            {
            }
            return _flag;
        }

        /// <summary>
        /// Fill RetunDetails in GridView.
        /// </summary>
        /// <param name="lsReturnDetails">
        /// Pass List of ReturnDetails to the Methods.
        /// </param>
        public void FillReturnDetails(List<ReturnDetail> lsReturnDetails)
        {
            try
            {
                string ImageNam;
                string NoofImages;


                Obj._lsReturnDetails = lsReturnDetails;

                Session["ReturnDetails"] = lsReturnDetails;

                var ReaturnDetails = from Rs in lsReturnDetails
                                     select new
                                     {
                                         Rs.RGADROWID,
                                         Rs.SKUNumber,
                                         Rs.SKU_Qty_Seq,
                                         Rs.SKU_Status,
                                         Rs.SKU_Sequence,
                                         Rs.ProductID,
                                         Rs.SalesPrice,
                                         Rs.LineType,
                                         Rs.ShipmentLines,
                                         Rs.ReturnLines,
                                         Rs.ReturnDetailID,
                                         ReasonIDs = _Update.ReasonsIdByHasg(Rs.ReturnDetailID),
                                         ImageName="",
                                         NoofImages = "",
                                         //string imagename=""

                                     };


                gvReturnDetails.DataSource = ReaturnDetails.OrderBy(x => x.SKU_Sequence).ToList();
                gvReturnDetails.DataBind();

                GetCount();
                //foreach (GridViewRow row in gvReturnDetails.Rows)
                //{

                //    string GuidReturnDetail = (row.FindControl("lblguid") as Label).Text;

                //    List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(GuidReturnDetail));

                //    string ImageCount = Convert.ToString(lsImages2.Count);

                //    row.Cells[8].Text = ImageCount + " " + "Image(s)";
                //}
               

               

                //gvReturnDetails.Columns[9].Visible = false;
                //gvReturnDetails.Columns[10].Visible = false;
                //gvReturnDetails.Columns[11].Visible = false;
                //gvReturnDetails.Columns[12].Visible = false;
            }
            catch (Exception)
            { }
        }

        Guid returnid;
        //Update All Information of Return and Return Details.

        public void GetCount()
        {
            foreach (GridViewRow row in gvReturnDetails.Rows)
            {

                string GuidReturnDetail = (row.FindControl("lblguid") as Label).Text;

                List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(GuidReturnDetail));

                string ImageCount = Convert.ToString(lsImages2.Count);

               // row.Cells[8].Text = ImageCount + " " + "Image(s)";
                (row.FindControl("txtImageCount") as LinkButton).Text = ImageCount + " " + "Image(s)";

            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            //object of return.


            int InProgress = 0;

            if (chkflag.Checked == true)
            {
                InProgress = 1;
            }


          //  Return ret = Obj.Rcall.ReturnByRGAROWID(rga)[0];

            DateTime ScannedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time"); 
            DateTime ExpirationDate =  TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").AddDays(60);

            #region ReturnDetail
            
          


            //list of ReturnDetails by using RGAROWID.
          //  Views.Global.lsReturnDetail = Obj.Rcall.ReturnDetailByRGAROWID(Request.QueryString["RGAROWID"].ToString());

            //Set the Return Information in Return Table.
           // Guid returnid = _Update.SetReturnTbl(ret, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), Convert.ToDateTime(txtreturndate.Text),"");

          //  returnid = _Update.SetReturnByRGANumber(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);


            if (((Return)Session["ReteunGlobal"]).RMANumber == "N/A")
            {
                if (((Return)Session["ReteunGlobal"]).OrderNumber == "N/A")
                {
                    returnid = _Update.SetReturnByRGANumber(((Return)Session["ReteunGlobal"]), Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
                }
                else
                {
                    returnid = _Update.SetReturnByPonumberTbl(((Return)Session["ReteunGlobal"]), Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
                }
            }
            else
            {

                returnid = _Update.SetReturnTbl(((Return)Session["ReteunGlobal"]), Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);
            }
            //set Gridview information in ReturnDetail Table.
            for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            {
                int flag = 0;

              //  Guid ReturnDetailsID = Views.Global.lsReturnDetail[i].ReturnDetailID;

                //string Dquantity = (gvReturnDetails.Rows[i].FindControl("txtdeliveredquantity") as TextBox).Text;

                string Rquantity = (gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq") as TextBox).Text;

                String SKUNumber = (gvReturnDetails.Rows[i].FindControl("txtSKU") as TextBox).Text;

                string ProductID = (gvReturnDetails.Rows[i].FindControl("txtProductID") as TextBox).Text;

                string SKUSequence = (gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence") as TextBox).Text;

                string SalesPrice = (gvReturnDetails.Rows[i].FindControl("txtSalesPrice") as TextBox).Text;

                string Linetype = (gvReturnDetails.Rows[i].FindControl("txtLineType") as TextBox).Text;

                string ShipmentLine = (gvReturnDetails.Rows[i].FindControl("txtShipmentLines") as TextBox).Text;

                string ReturnLine = (gvReturnDetails.Rows[i].FindControl("txtReturnLines") as TextBox).Text;

                string GuidReturnDetail = (gvReturnDetails.Rows[i].FindControl("lblguid") as Label).Text;

                string imglist = ((Label)gvReturnDetails.Rows[i].FindControl("lblImagesName")).Text;

                string SKUNewName = "";
                Boolean checkflag = false;





                if (((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Count > 0)
                {
                    for (int j = ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Count - 1; j >= 0; j--)
                    {
                        if (((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].SKUName == SKUNumber && ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].NewItemQuantity == Convert.ToInt16(SKUSequence))
                        {
                            SKUNewName = SKUNumber;
                           ViewState["SKU_Staus"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].Status;
                            ViewState["TotalPoints"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].Points;
                            ViewState["IsScanned"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].IsScanned;
                            ViewState["IsManually"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].IsMannually;
                            ViewState["NewItemQty"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].NewItemQuantity;
                            ViewState["_SKU_Qty_Seq"] = ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[j].skusequence;

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
                if (GuidReturnDetail !="")
                {
                    ReturnDetailsID = _Update.SetReturnDetailTbl(Guid.Parse(GuidReturnDetail), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], (string)ViewState["SKU_Staus"], (int)ViewState["TotalPoints"], (int)ViewState["IsScanned"], (int)ViewState["IsManually"], Convert.ToInt16(SKUSequence), (int)ViewState["_SKU_Qty_Seq"], ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));

                }
                else
                {
                    ReturnDetailsID = _Update.SetReturnDetailNewInsertTbl(Guid.NewGuid(), returnid, SKUNumber, "", Convert.ToInt32(Rquantity), (Guid)Session["UserID"], (string)ViewState["SKU_Staus"], (int)ViewState["TotalPoints"], (int)ViewState["IsScanned"], (int)ViewState["IsManually"], Convert.ToInt16(SKUSequence), (int)ViewState["_SKU_Qty_Seq"], ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));
                }

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


                if (((DataTable)Session["DT1"]).Rows.Count > 0)
                {
                    for (int k = ((DataTable)Session["DT1"]).Rows.Count - 1; k >= 0; k--)
                    {
                        DataRow d = ((DataTable)Session["DT1"]).Rows[k];
                        if (d["SKU"].ToString() == SKUNumber && d["ItemQuantity"].ToString() == SKUSequence)
                        {
                            string RetirID = d["ReturnDetailID"].ToString();

                            if (Guid.Parse(d["ReturnDetailID"].ToString()) == ReturnDetailsID && d["ReturnedSKUID"].ToString() != null && d["ReturnedSKUID"].ToString() != "")
                            {
                               // Guid skureturn = Guid.Parse(d["ReturnedSKUID"].ToString());

                                Guid ReturnedSKUPoints = _Update.SetReturnedSKUPoints(Guid.Parse(d["ReturnedSKUID"].ToString()) , ReturnDetailsID, returnid, ((DataTable)Session["DT1"]).Rows[k][0].ToString(), ((DataTable)Session["DT1"]).Rows[k][1].ToString(), ((DataTable)Session["DT1"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][4].ToString()));
                                d.Delete();
                            }
                            else
                            {
                                _Update.SetReturnedSKUPoints(Guid.NewGuid(), ReturnDetailsID, returnid, ((DataTable)Session["DT1"]).Rows[k][0].ToString(), ((DataTable)Session["DT1"]).Rows[k][1].ToString(), ((DataTable)Session["DT1"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][4].ToString()));
                                d.Delete();
                            }
                           
                        }
                    }
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









                // _Update.SetReturnDetailTbl(lsretundetail[i], Convert.ToInt16(Dquantity), Convert.ToInt16(Rquantity), SKUNumber,ProductName);

            }

            //Clear the Reasons list from Global Object.
            Obj._ReasonList = new List<Views.ReasonList>();

          //  Response.Redirect("~/Forms/Web Forms/frmRetunDetail.aspx");
            lblUser.Text = "Please Select Any One Option";
            ModalPopupExtender1.Show();
        }



        /// <summary>
        /// String of Return Reason.
        /// </summary>
        /// <returns>
        /// Return string Of Reasons.
        /// </returns>
        //private String ReturnReasons()
        //{
        //    String _ReturnReason = "";

        //    if (chkitemdamaged.Checked == true) _ReturnReason = _ReturnReason + chkitemdamaged.Text;

        //    if (chkitemdifferent.Checked == true) _ReturnReason = _ReturnReason + chkitemdifferent.Text;

        //    if (chkduplicate.Checked == true) _ReturnReason = _ReturnReason + chkduplicate.Text;

        //    if (chkitemordered.Checked == true) _ReturnReason = _ReturnReason + chkitemordered.Text;

        //    if (chknotsatisfied.Checked == true) _ReturnReason = _ReturnReason + chknotsatisfied.Text;

        //    if (chkwrongitem.Checked == true) _ReturnReason = _ReturnReason + chkwrongitem.Text;

        //    _ReturnReason += txtotherreasons.Text;

        //    return _ReturnReason;

        //}


        /// <summary>
        /// Text Of Textbox
        /// </summary>
        /// <param name="TextBoxID">
        /// String textbox ID
        /// </param>
        /// <param name="GridViewName">
        /// Gridview Object textbox belongs to
        /// </param>
        /// <returns>
        /// String Text Of TextBox 
        /// </returns>
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

        /// <summary>
        /// This Method is for get Product Category.
        /// </summary>
        /// <param name="sku">
        /// String SKU pass as peremeter for Split.
        /// </param>
        /// <param name="flag">
        /// int flag is for array index.
        /// </param>
        /// <returns>
        /// Return Category.
        /// </returns>
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
               // txtotherreasons.Text = ddlotherreasons.SelectedItem.Text;
            }
        }

        //protected void btnUpdate_Click1(object sender, EventArgs e)
        //{
        //    string updir = System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString();
        //    GridViewRow gvRow = (sender as Button).NamingContainer as GridViewRow;
        //    FileUpload fileUpload = gvRow.FindControl("FileUpload1") as FileUpload;

        //    fileUpload.SaveAs(@"C:\Images\" + fileUpload.FileName);
        //    //method to upload file to the FTP server.
        //   ExtensionMethods.Upload(@"ftp://fileshare.kraususa.com", "rgauser", "rgaICG2014", "C:\\Images\\" + fileUpload.FileName, fileUpload.FileBytes);
        //    //delete file from the local.
        //    File.Delete(@"C:\Images\" + fileUpload.FileName);

        //    Label lbl = gvRow.FindControl("lblImagesName") as Label;
        //    lbl.Text = lbl.Text + "\n" + fileUpload.FileName;
        //}

      
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

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            txtNewItem.Visible = true;
            BtnAddNewItem.Visible = true;
        }
        int max, shipmax, returnmax;
        protected void BtnAddNewItem_Click(object sender, EventArgs e)
        {
            //String RowString = "";

            //try
            //{
            //    RowString = (gvReturnDetails.Rows[Convert.ToInt32(gvReturnDetails.Rows.Count - 1)].Cells[0].FindControl("txtSKU") as TextBox).Text.Trim();
            //}
            //catch (Exception)
            //{
            //    RowString = "New ROW";
            //}
            //if (RowString != "")
            //{

            if (txtNewItem.Text != "")
            {
                dt.Columns.Add("RGADROWID");
                dt.Columns.Add("SKUNumber");
                dt.Columns.Add("SKU_Qty_Seq");
                dt.Columns.Add("SKU_Status");
                dt.Columns.Add("ProductID");
                dt.Columns.Add("SKU_Sequence");
                dt.Columns.Add("SalesPrice");

                dt.Columns.Add("NoofImages");
                dt.Columns.Add("ImageName");
                dt.Columns.Add("LineType");
                dt.Columns.Add("ShipmentLines");
                dt.Columns.Add("ReturnLines");
                dt.Columns.Add("ReturnDetailID");


                for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
                {
                    try
                    {
                        DataRow dr1 = dt.NewRow();

                        TextBox RowID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtRGANumberID");
                        TextBox SKUNumber = (TextBox)gvReturnDetails.Rows[i].FindControl("txtsku");
                        TextBox SKU_Qty_Seq = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq");
                        TextBox SKU_Status = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Status");
                        TextBox ProductID = (TextBox)gvReturnDetails.Rows[i].FindControl("txtProductID");

                        TextBox SKU_Sequence = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence");

                        // LinkButton reasons = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtreasons");
                        TextBox SalesPrice = (TextBox)gvReturnDetails.Rows[i].FindControl("txtSalesPrice");
                        TextBox LineType = (TextBox)gvReturnDetails.Rows[i].FindControl("txtLineType");
                        TextBox ShipmentLines = (TextBox)gvReturnDetails.Rows[i].FindControl("txtShipmentLines");
                        TextBox ReturnLines = (TextBox)gvReturnDetails.Rows[i].FindControl("txtReturnLines");
                        Label lblimages = (Label)gvReturnDetails.Rows[i].FindControl("lblImagesName");

                        LinkButton NoOfImages = (LinkButton)gvReturnDetails.Rows[i].FindControl("txtImageCount");

                        Label lblReturnDetailID = (Label)gvReturnDetails.Rows[i].FindControl("lblguid");

                        dr1[0] = RowID.Text;
                        dr1[1] = SKUNumber.Text;
                        dr1[2] = SKU_Qty_Seq.Text;
                        dr1[3] = SKU_Status.Text;

                        dr1[4] = ProductID.Text;

                        dr1[5] = SKU_Sequence.Text;
                        dr1[6] = SalesPrice.Text;

                        dr1[7] = NoOfImages.Text;

                        dr1[8] = lblimages.Text;
                        dr1[9] = LineType.Text;
                        dr1[10] = ShipmentLines.Text;
                        dr1[11] = ReturnLines.Text;
                        dr1[12] = lblReturnDetailID.Text;




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
                dr[0] = "";
                dr[1] = txtNewItem.Text;
                dr[2] = "0";
                dr[3] = "";
                dr[4] = "0";
                dr[5] = max + 1000;
                dr[6] = "0";

                dr[7] = "0 Image(s)";

                dr[8] = "";
                dr[9] = "1";
                dr[10] = shipmax + 1000;
                dr[11] = returnmax + 1000;
                dr[12] = "";


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
           // ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('SKU Added');</script>");
            else
            {
                lblMassege.Text = "Please Enter SKU Name";
                mpePopupForAddNo.Show();
            }

          //  }
        }

        protected void gvReturnDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Outlook.Application mApp = new Microsoft.Office.Interop.Outlook.Application();
            //Microsoft.Office.Interop.Outlook.MailItem mEmail = null;
            //mEmail = (Microsoft.Office.Interop.Outlook.MailItem)mApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            //mEmail.To = "";
            //mEmail.Subject = "";
            //mEmail.Display();
            System.Threading.Thread.Sleep(3000);

            lblMassege.Text = "Process is completed";

          //  Session["DT1"] = ViewState["dt"] as DataTable;

          //  List<StatusAndPoints> listofstatusAndPoint1 = new List<StatusAndPoints>();

           // listofstatusAndPoint1 = ViewState["ReturnStatus"] as List<StatusAndPoints>;

            for (int i = ((DataTable)Session["DT1"]).Rows.Count - 1; i >= 0; i--)
            {
                DataRow d = ((DataTable)Session["DT1"]).Rows[i];
                if (d["SKU"].ToString() == ViewState["SelectedskuName"].ToString() && d["ItemQuantity"].ToString() == ViewState["ItemQuantity"])
                    d.Delete();
            }

            #region DtOperaion
            DataRow dr = ((DataTable)Session["DT1"]).NewRow();
            dr["SKU"] = ViewState["SelectedskuName"];
            dr["ItemQuantity"] = ViewState["ItemQuantity"];

            string retun = ViewState["ReturnDetailID"].ToString();

            if (ViewState["ReturnDetailID"] == "")
            {
                dr["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            }
            else
            {
                dr["ReturnDetailID"] = ViewState["ReturnDetailID"];
            }
                //dr[""]
                if ( brdItemNew.Items.FindByText("Yes").Selected == true)
                {
                    dr["Reason"] = lblitemNew.Text;
                    dr["Reason_Value"] = "Yes";
                    dr["Points"] = 100;
                    ((DataTable)Session["DT1"]).Rows.Add(dr);
                }
                else if ( brdItemNew.Items.FindByText("No").Selected == true)
                {
                    dr["Reason"] = lblitemNew.Text;
                    dr["Reason_Value"] = "No";
                    dr["Points"] = 0;
                    ((DataTable)Session["DT1"]).Rows.Add(dr);
                }

                DataRow dr1 = ((DataTable)Session["DT1"]).NewRow();
                dr1["SKU"] = ViewState["SelectedskuName"];
                dr1["ItemQuantity"] = ViewState["ItemQuantity"];
                if (ViewState["ReturnDetailID"] == "")
                {
                    dr1["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    dr1["ReturnDetailID"] = ViewState["ReturnDetailID"];
                }
                if (brdInstalled.Items.FindByText("Yes").Selected == true)
                {
                    dr1["Reason"] = lblInstalled.Text;
                    dr1["Reason_Value"] = "Yes";
                    dr1["Points"] = 0;
                    ((DataTable)Session["DT1"]).Rows.Add(dr1);
                }
                else if (brdInstalled.Items.FindByText("No").Selected == true)
                {
                    dr1["Reason"] = lblInstalled.Text;
                    dr1["Reason_Value"] = "No";
                    dr1["Points"] = 100;
                    ((DataTable)Session["DT1"]).Rows.Add(dr1);
                }

                DataRow dr2 = ((DataTable)Session["DT1"]).NewRow();
                dr2["SKU"] = ViewState["SelectedskuName"];
                dr2["ItemQuantity"] = ViewState["ItemQuantity"];
                if (ViewState["ReturnDetailID"] == "")
                {
                    dr2["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    dr2["ReturnDetailID"] = ViewState["ReturnDetailID"];
                }
                if (brdstatus.Items.FindByText("Yes").Selected == true)
                {
                    dr2["Reason"] = lblstatus.Text;
                    dr2["Reason_Value"] = "Yes";
                    dr2["Points"] = 0;
                    ((DataTable)Session["DT1"]).Rows.Add(dr2);
                }
                else if (brdstatus.Items.FindByText("No").Selected == true)
                {
                    dr2["Reason"] = lblstatus.Text;
                    dr2["Reason_Value"] = "No";
                    dr2["Points"] = 100;
                    ((DataTable)Session["DT1"]).Rows.Add(dr2);
                }

                DataRow dr3 = ((DataTable)Session["DT1"]).NewRow();
                dr3["SKU"] = ViewState["SelectedskuName"];
                dr3["ItemQuantity"] = ViewState["ItemQuantity"];
                if (ViewState["ReturnDetailID"] == "")
                {
                    dr3["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    dr3["ReturnDetailID"] = ViewState["ReturnDetailID"];
                }
                if (brdManufacturer.Items.FindByText("Yes").Selected == true)
                {
                    dr3["Reason"] = lblManifacturerDefective.Text;
                    dr3["Reason_Value"] = "Yes";
                    dr3["Points"] = 100;
                    ((DataTable)Session["DT1"]).Rows.Add(dr3);
                }
                else if (brdManufacturer.Items.FindByText("No").Selected == true)
                {
                    dr3["Reason"] = lblManifacturerDefective.Text;
                    dr3["Reason_Value"] = "No";
                    dr3["Points"] = 0;
                    ((DataTable)Session["DT1"]).Rows.Add(dr3);
                }

                DataRow dr4 = ((DataTable)Session["DT1"]).NewRow();
                dr4["SKU"] = ViewState["SelectedskuName"];
                dr4["ItemQuantity"] = ViewState["ItemQuantity"];
                if (ViewState["ReturnDetailID"] == "")
                {
                    dr4["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
                }
                else
                {
                    dr4["ReturnDetailID"] = ViewState["ReturnDetailID"];
                }
                if ( brdDefecttransite.Items.FindByText("Yes").Selected == true)
                {
                    dr4["Reason"] = lblDefectintransite.Text;
                    dr4["Reason_Value"] = "Yes";
                    dr4["Points"] = 100;
                    ((DataTable)Session["DT1"]).Rows.Add(dr4);
                }
                else if ( brdDefecttransite.Items.FindByText("No").Selected == true)
                {
                    dr4["Reason"] = lblDefectintransite.Text;
                    dr4["Reason_Value"] = "No";
                    dr4["Points"] = 0;
                    ((DataTable)Session["DT1"]).Rows.Add(dr4);
                }
                #endregion

                StatusAndPoints _lsstatusandpoints = new StatusAndPoints();
                _lsstatusandpoints.SKUName = ViewState["SelectedskuName"].ToString();

                //if (!NonPo)
                //{
                //    Views.clGlobal.SKU_Staus = "Deny";
                //    NonPo = true;
                //}

                _lsstatusandpoints.Status = ViewState["Sku_status"].ToString();
                _lsstatusandpoints.Points = 100;//Views.clGlobal.TotalPoints;
                _lsstatusandpoints.NewItemQuantity = Convert.ToInt16(ViewState["ItemQuantity"]);
                _lsstatusandpoints.skusequence = Convert.ToInt16(ViewState["SkuQuantitySequence"]);

                //for (int i = 0; i < lsskuIsScanned.Count; i++)
                //{
                //    if (lsskuIsScanned[i].SKUName == SelectedskuName)
                //    {
                //        _lsstatusandpoints.IsScanned = lsskuIsScanned[i].IsScanned;
                //        break;
                //    }
                //}


                for (int i = ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).Count - 1; i >= 0; i--)
                {
                    if (((List<StatusAndPoints>)Session["listofstatusAndPoint"])[i].SKUName == ViewState["SelectedskuName"] && ((List<StatusAndPoints>)Session["listofstatusAndPoint"])[i].NewItemQuantity == Convert.ToInt16(ViewState["ItemQuantity"]))
                    {
                        ((List<StatusAndPoints>)Session["listofstatusAndPoint"]).RemoveAt(i);
                    }
                }


                _lsstatusandpoints.IsMannually = 0;

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

                //if (cmbSkuReasons.SelectedIndex == 0 && txtskuReasons.Text == "")
                //{
                //    MessageBox.Show("Please Select or Enter Reason");
                //    btnAdd.IsEnabled = true;
                //    CanvasConditions.IsEnabled = true;
                //}
                //else
                //{
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


              ///  ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('Submit information');</script>");
                lblMassege.Text = "Submit information";
                mpePopupForSubmitYes.Show();
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
                DT = Session["DT1"] as DataTable;

                for (int j = 0; j < gvReturnDetails.Rows.Count; j++)
                {
                    RadioButton rb = (gvReturnDetails.Rows[j].FindControl("RadioButton1")) as RadioButton;
                    if (rb.Checked == true)
                    {
                        // Id = gvReturnDetails.Rows[i].Cells[1].Text;

                        #region Deepak
                      //  Session["_lsSlipPrintSKUNumber"] = new List<String>();
                        String SKUNumberforprint = (gvReturnDetails.Rows[j].FindControl("txtSKU") as TextBox).Text;
                        ((List<String>)Session["_lsSlipPrintSKUNumber"]).Add(SKUNumberforprint);
                        #endregion

                        String LinetType = (gvReturnDetails.Rows[j].FindControl("txtLineType") as TextBox).Text;

                        if (LinetType != "6" || LinetType == "" || LinetType==null)
                        {
                            String SKUNumber = (gvReturnDetails.Rows[j].FindControl("txtSKU") as TextBox).Text;

                            ViewState["SelectedskuName"] = SKUNumber;

                            String SKUSequence = (gvReturnDetails.Rows[j].FindControl("txtSKU_Sequence") as TextBox).Text;

                            ViewState["ItemQuantity"] = SKUSequence;

                            (gvReturnDetails.Rows[j].FindControl("txtSKU_Qty_Seq") as TextBox).Text = "1";

                            String SkuQuantitySequence = (gvReturnDetails.Rows[j].FindControl("txtSKU_Qty_Seq") as TextBox).Text;

                            //String ReturnDetailID = (gvReturnDetails.Rows[j].FindControl("txtSKU_Sequence") as TextBox).Text;

                            string GuidReturnDetail = (gvReturnDetails.Rows[j].FindControl("lblguid") as Label).Text;

                            ViewState["SkuQuantitySequence"] = SkuQuantitySequence;

                            ViewState["ReturnDetailID"] = GuidReturnDetail;

                            //row.Cells[5].Text = _mReturn.ConvertToDecision(Value1);

                            String SKUStatus = (gvReturnDetails.Rows[j].FindControl("txtSKU_Status") as TextBox).Text;

                            ViewState["Sku_status"] = SKUStatus;

                            if (SKUStatus != "")
                            {
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {

                                    // string kU = DT.Rows[i][1].ToString();

                                    if (SKUNumber == DT.Rows[i][0].ToString() && SKUSequence == DT.Rows[i][4].ToString())
                                    {
                                        // msg = dt.Rows[i][1].ToString() + " : " + dt.Rows[i][2].ToString() + "\n" + msg;

                                        string data1 = DT.Rows[i][1].ToString();
                                        string data2 = DT.Rows[i][2].ToString();

                                        if (DT.Rows[i][1].ToString() == "Item is New" && DT.Rows[i][2].ToString() == "Yes")
                                        {
                                            brdItemNew.Items.FindByText("Yes").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Item is New" && DT.Rows[i][2].ToString() == "No"))
                                        {
                                            brdItemNew.Items.FindByText("No").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Installed" && DT.Rows[i][2].ToString() == "Yes"))
                                        {
                                            brdInstalled.Items.FindByText("Yes").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Installed" && DT.Rows[i][2].ToString() == "No"))
                                        {
                                            brdInstalled.Items.FindByText("No").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Chip/Bended/Scratch/Broken" && DT.Rows[i][2].ToString() == "Yes"))
                                        {
                                            brdstatus.Items.FindByText("Yes").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Chip/Bended/Scratch/Broken" && DT.Rows[i][2].ToString() == "No"))
                                        {
                                            brdstatus.Items.FindByText("No").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Manufacturer Defective" && DT.Rows[i][2].ToString() == "Yes"))
                                        {
                                            brdManufacturer.Items.FindByText("Yes").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Manufacturer Defective" && DT.Rows[i][2].ToString() == "No"))
                                        {
                                            brdManufacturer.Items.FindByText("No").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Defect in Transite" && DT.Rows[i][2].ToString() == "Yes"))
                                        {
                                            brdDefecttransite.Items.FindByText("Yes").Selected = true;
                                        }
                                        else if ((DT.Rows[i][1].ToString() == "Defect in Transite" && DT.Rows[i][2].ToString() == "No"))
                                        {
                                            brdDefecttransite.Items.FindByText("No").Selected = true;
                                        }
                                    }
                                }


                                for (int k = 0; k < ((List<SKUReason>)Session["lsSKUReasons"]).Count; k++)
                                {
                                    if (((List<SKUReason>)Session["lsSKUReasons"])[k].ReturnDetailID == Guid.Parse(GuidReturnDetail))
                                    {
                                        System.Guid ReturnID = ((List<SKUReason>)Session["lsSKUReasons"])[k].ReturnDetailID;

                                        string reas = Obj.Rcall.GetReasonstringbyReturnID(ReturnID);

                                        ddlotherreasons.SelectedItem.Text = reas;

                                        //cmbSkuReasons.Text = reas;
                                    }
                                }

                            }
                            else
                            {
                              
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
                        else
                        {
                            mpeForLineType.Show();
                           // ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('Can not add comment/parent sku for combination item');</script>");
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

        protected void btnComment_Click(object sender, EventArgs e)
        {
            //RMAComment rmaComment = new RMAComment();

            //rmaComment.RMACommentID = Guid.NewGuid();
            //rmaComment.ReturnID = Views.Global.ReteunGlobal.ReturnID;
            //rmaComment.UserID =(Guid)Session["UserID"];
            //rmaComment.Comment = txtcomment.Text;
            //rmaComment.CommentDate = DateTime.UtcNow;

            //Obj.Rcall.InsertRMACommnt(rmaComment);

            //txtcomment.Text = "";


            //ShowComments();

            //Deepak 19-08-2014

            //fnforComment();
            //ShowComments();


            fnforComment();
           // List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);
            DataTable dtRepeater = new DataTable();
            dtRepeater.Columns.Add("UserName");
            dtRepeater.Columns.Add("Time");
            dtRepeater.Columns.Add("Content");
            List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(((Return)Session["ReteunGlobal"]).ReturnID);

            foreach (var item in lsComment.OrderByDescending(y => y.CommentDate))
            {

                DataRow rd = dtRepeater.NewRow();
                string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName;

                rd["UserName"] = Usernm;
                rd["Time"] = item.CommentDate.ToString("MM/dd/yyyy hh:mm tt");
                rd["Content"] = item.Comment;
               // rd["Content"] ="adrtml";
                dtRepeater.Rows.Add(rd);

            }
            //if(txtcomment.Text!="" || txtcomment.Text!=null)
            //{
            //    DataRow rd = dtRepeater.NewRow();
            //    string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)Session["UserID"]).UserFullName;

            //    rd["UserName"] = Usernm;
            //    rd["Time"] = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").ToString("MM/dd/yyyy hh:mm tt");
            //    rd["Content"] = txtcomment.Text;               
            //    dtRepeater.Rows.Add(rd);
            //}
            Repeater1.DataSource = dtRepeater;
            Repeater1.DataBind();

            txtcomment.Text = "";
            //  ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('Comment Added');</script>");
            lblMassege.Text = "Comment Added";
            mpePopupForCommentYes.Show();
            ////ENDD
        }
        public void fnforComment()
        {
            RMAComment rmaComment = new RMAComment();

            rmaComment.RMACommentID = Guid.NewGuid();
            rmaComment.ReturnID = ((Return)Session["ReteunGlobal"]).ReturnID;
            rmaComment.UserID = (Guid)Session["UserID"];
            rmaComment.Comment = txtcomment.Text;
            rmaComment.CommentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");


            Obj.Rcall.InsertRMACommnt(rmaComment);

            txtcomment.Text = "";




        }

        public void SavedShowComments()
        {
            DataTable dtRepeater = new DataTable();
            dtRepeater.Columns.Add("UserName");
            dtRepeater.Columns.Add("Time");
            dtRepeater.Columns.Add("Content");

            List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(((Return)Session["ReteunGlobal"]).ReturnID);

            foreach (var item in lsComment.OrderByDescending(y => y.CommentDate))
            {

                DataRow rd = dtRepeater.NewRow();
                string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName;

                rd["UserName"] = Usernm;
                rd["Time"] = item.CommentDate.ToString("MM/dd/yyyy hh:mm tt");
                rd["Content"] = item.Comment;
                // rd["Content"] ="adrtml";
                dtRepeater.Rows.Add(rd);

            }
            Repeater1.DataSource = dtRepeater;
            Repeater1.DataBind();
            //if(txtcomment.Text!="" || txtcomment.Text!=null)
            //{
            //    DataRow rd = dtRepeater.NewRow();
            //    string Usernm = Obj.Rcall.GetUserInfobyUserID((Guid)Session["UserID"]).UserFullName;

            //    rd["UserName"] = Usernm;
            //    rd["Time"] = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").ToString("MM/dd/yyyy hh:mm tt");
            //    rd["Content"] = txtcomment.Text;               
            //    dtRepeater.Rows.Add(rd);
            //}
           

            //string comment = "";
            //List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(((Return)Session["ReteunGlobal"]).ReturnID);

            //foreach (var item in lsComment)
            //{
            //    comment = comment + item.Comment;
            //}
            //lblcomments.Text = comment;
        }




        public void ShowComments()
        {
            //string comment = "";
            //List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);

            //foreach (var item in lsComment)
            //{
            //    comment = comment + item.Comment;
            //}
            //lblcomments.Text = comment;

            ///Deeepak 19-08-2014
            ///

            this.Controls.Add(new LiteralControl("<div style=' border-radius: 11px 0 0 11px;  border: 1px solid; position : absolute; color:#179090; left :1193px; right : 50px; top :218px;width:502px;height:259px;overflow: auto;'>"));
         
             

            
            
            //Guid userId = (Guid)Views.Global.ReteunGlobal.UpdatedBy;
            //Obj.Rcall.GetUserInfobyUserID(userId);
            // string comment = "";
            List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(((Return)Session["ReteunGlobal"]).ReturnID);

            foreach (var item in lsComment.OrderByDescending(y => y.CommentDate))
            {
                // comment = comment + item.Comment;

                this.Controls.Add(new LiteralControl("<table width='100%' >"));
                this.Controls.Add(new LiteralControl("<tr><td bgcolor='#8DC6FF'>"));
                this.Controls.Add(new LiteralControl("<h8> " + Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName + " || " + item.CommentDate.ToString("MM/dd/yyyy hh:mm tt") + "</h8> "));
                this.Controls.Add(new LiteralControl("</td></tr><tr><td bgcolor='#FFFFFF'shape='rect'><b>" + item.Comment + "</td></tr>"));
                // this.Controls.Add(new LiteralControl("<h3>RMA REQUEST FORM <h3>"));
                // this.Controls.Add(new LiteralControl("<h8> ----------</h8> "));
                this.Controls.Add(new LiteralControl(" </table>"));
            }
            // lblcomments.Text = comment;
            this.Controls.Add(new LiteralControl("</div>"));
            ///ENDD
        }

     
        protected void btnEmail_Click(object sender, EventArgs e)
        {
            //Microsoft.Office.Interop.Outlook.Application mApp = new Microsoft.Office.Interop.Outlook.Application();
            //Microsoft.Office.Interop.Outlook.MailItem mEmail = null;
            //mEmail = (Microsoft.Office.Interop.Outlook.MailItem)mApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            //mEmail.To = "";
            //mEmail.Subject = "";
            //mEmail.Body = "";
            ////mEmail.Attachments.Add(
            //mEmail.Display();


            //Deepak 19-08-14
                      
            //Microsoft.Office.Interop.Outlook.Application mApp = new Microsoft.Office.Interop.Outlook.Application();
            //Microsoft.Office.Interop.Outlook.MailItem mEmail = null;
            //mEmail = (Microsoft.Office.Interop.Outlook.MailItem)mApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            //mEmail.To = "";
            //mEmail.Subject = "";
           
            //string dd = "Dear \t " + txtcustomerName.Text + ",<p> <p><p><p><p><table width='100%' border='1' bgcolor='#6699FF' ><th>SKU</th><th>Qty</th><th>Status</th>";
            //string ReturDetailNo = "", SKU = "", Qty = "", Status = "", SKUSeq = "", SalePrice = "", LT = "", SL = "", RL = "";
            //for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            //{
            //    ReturDetailNo = (gvReturnDetails.Rows[i].FindControl("txtRGANumberID") as TextBox).Text;
            //    SKU = (gvReturnDetails.Rows[i].FindControl("txtSKU") as TextBox).Text;
            //    Qty = (gvReturnDetails.Rows[i].FindControl("txtSKU_Qty_Seq") as TextBox).Text;
            //    Status = (gvReturnDetails.Rows[i].FindControl("txtSKU_Status") as TextBox).Text;
            //    SKUSeq = (gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence") as TextBox).Text;
            //    LT = (gvReturnDetails.Rows[i].FindControl("txtLineType") as TextBox).Text;
            //    SL = (gvReturnDetails.Rows[i].FindControl("txtShipmentLines") as TextBox).Text;
            //    RL = (gvReturnDetails.Rows[i].FindControl("txtReturnLines") as TextBox).Text;
            //    SalePrice = (gvReturnDetails.Rows[i].FindControl("txtSalesPrice") as TextBox).Text;

            //    //mEmail.HTMLBody = "Dear \t " + txtcustomerName.Text + ",<p> <p><p><p><p><table width='100%' border='1' bgcolor='#6699FF' ><th>SKU</th><th>Qty</th><th>Status</th><tr bgcolor='#8DC6FF'><td align='center'>" + SKU + "</td><td align='center'>" + Qty + "</td><td align='center'>" + Status + "</td></tr></table>";

            //    dd += "<tr bgcolor='#8DC6FF'><td align='center'>" + SKU + "</td><td align='center'>" + Qty + "</td><td align='center'>" + Status + "</td></tr>";
            //}
            //dd += "</table>";
            //mEmail.HTMLBody = dd;

            //for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
            //{
            //   // string ReturnROWID = Views.Global.ReteunGlobal.RGAROWID;
            //    //    Session["RGAROIDE"]
            //    string ReturnROWID = Session["RGAROIDE"].ToString();
            //    string GuidReturnDetail = (gvReturnDetails.Rows[i].FindControl("lblguid") as Label).Text;
            //    ///////////   lblImagesFor.Text = "Sorry! Images for GRA Detail Number : " + ReturnROWID + " not found!";
            //    List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(GuidReturnDetail));
            //    List<String> lsImages = new List<string>();
            //    String ImgServerString = System.Configuration.ConfigurationManager.AppSettings["ImageServerPath"].ToString();

            //    //foreach (var Imaitem in lsImages2)
            //    //{
            //    //    //lsImages.Add("~/images/"+Imaitem.Split(new char[] { '\\' }).Last().ToString());
            //    //    lsImages.Add(ImgServerString.Replace("#{ImageName}#", Imaitem.Split(new char[] { '\\' }).Last().ToString()));
            //    //}
            //    if (lsImages2.Count > 0)
            //    {
            //        ////////// lblImagesFor.Text = "Images for GRA Detail Number : " + ReturnROWID;
            //        for (int j = 0; j < lsImages2.Count(); j++)
            //        {
            //            mEmail.Attachments.Add(lsImages2[j]);
            //        }
            //    }

            //}




            //mEmail.Display();


            //end
       }

        //public void OnConfirm(object sender, EventArgs e)
        //{
        //    string confirmValue = Request.Form["confirm_value"];
        //    if (confirmValue == "Yes")
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
        //    }
        //    else
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        //    }
        //}

        protected void btnOk_Click(object sender, EventArgs e)
        {

           // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

            Response.Redirect(@"~\Forms\Web Forms\DemoGrid.aspx");
        }

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
            //this.Controls.Add(new LiteralControl("</td></tr><tr><td bgcolor='#8DC6FF'><button type='button' onclick='alert('Hello world!')'>Download Me!</button></td></tr>"));


            //this.Controls.Add(new LiteralControl("</table>"));



            //this.Controls.Add(new LiteralControl("</div>"));

            //}
        }

        protected void lkbtnPath1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DemoGrid.aspx");
        }

        protected void btnOkForSaveYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmHomePage.aspx");
        }

        protected void Button14_Click(object sender, EventArgs e)
        {

        }

        protected void btnReprint_Click(object sender, EventArgs e)
        {
            try
            {
                List<cSlipInfo> lspr = new List<cSlipInfo>();
                if (((List<String>)Session["_lsSlipPrintSKUNumber"]).Count > 0)
                {
                    foreach (var n in ((List<String>)Session["_lsSlipPrintSKUNumber"]))
                    {
                        if (((Return)Session["ReteunGlobal"]).RMANumber == "N/A")
                        {

                            string encd = Obj.Rcall.EncodeCode(n);
                            Guid userId = (Guid)Session["UserID"];
                            string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                            //_retn.GetReturnTblByReturnID(returnid)
                            var rr = ((Return)Session["ReteunGlobal"]).RGAROWID;

                            string nrr = rr.ToString();

                            lspr.Add(_Update.GetSlipInfo(n, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm));

                        }
                        else
                        {
                            string encd = Obj.Rcall.EncodeCode(n);
                            Guid userId = (Guid)Session["UserID"];
                            string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                            //_retn.GetReturnTblByReturnID(returnid)
                            var rr = ((Return)Session["ReteunGlobal"]).RMANumber;

                            string nrr = rr.ToString();

                            lspr.Add(_Update.GetSlipInfo(n, encd, "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm));
                        }
                    }
                    Session["lsSlipInfo"] = lspr;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmSlipPrint.aspx','_newtab');", true);

                    //foreach (var n in ((List<String>)Session["_lsSlipPrintSKUNumber"]))
                    //{


                    //    Guid userId = (Guid)Session["UserID"];
                    //    string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                    //    //_retn.GetReturnTblByReturnID(returnid)
                    //    var rr = Request.QueryString["RGAROWID"].ToString();
                    //    string nrr = rr.ToString();
                    //    Session["lsSlipInfo"] = _Update.GetSlipInfo(_lsreturn, n, Obj.Rcall.EncodeCode(n), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);
                    //    // Views.Global.lsSlipInfo = _Update.GetSlipInfo(_lsreturn, Global.arr[i], Obj.Rcall.EncodeCode(Global.arr[i]), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmSlipPrint.aspx','_newtab');", true);






                    //    // literal.Text += "a ID='linkcontact' runat='server' href='" + "www.website./pagename.aspx?ID=" + id + "'>contact</a>";

                    //}
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "alert(Plaese Select One SKu);", true);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void Button17_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["RGAROWIDPrint"] = new string[] { };


            var myList = new List<string>();
            string[] arr2 = { };
            int i = 0;
            // String RowId = (((GridViewRow)((LinkButton)sender).Parent.Parent).Cells[0].FindControl("lbtnRGANumberID") as LinkButton).Text;
            rga = Request.QueryString["RGAROWID"].ToString();
            myList.Add(rga);

           arr2=myList.ToArray();
           Session["RGAROWIDPrint"] = arr2;

         //   ShippingController_V1._0_.Views.Global.arr = myList.ToArray();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmRMAFormPrint2.aspx','_newtab');", true);
        }

        protected void Button16_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDownload_Click1(object sender, EventArgs e)
        {
            DataTable dtForImages = new DataTable();
            dtForImages.Columns.Add("Images");
            List<String> lsImagePath = new List<string>();
            GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;

            string ReturndetailID = (gvRow.FindControl("lblguid") as Label).Text;
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
               
                if (lsImages2.Count > 0)
                {
                    ////////// lblImagesFor.Text = "Images for GRA Detail Number : " + ReturnROWID;
                    for (int j = 0; j < lsImages2.Count; j++)
                    {
                        // flg = 2;
                        string path = lsImages[j].ToString();
                       // this.Controls.Add(new LiteralControl(" <img src='" + path + "' height='400' width='400'>"));
                        lsImagePath.Add(path);


                        DataRow drForImages = dtForImages.NewRow();

                        drForImages["Images"] = path;
                        dtForImages.Rows.Add(drForImages);
                    }
                }
                else
                {

                }
               // Session["ImagePath"] = lsImagePath;
                Session["ImagePath"] = dtForImages;
            }

            else
            {
                //this.Controls.Add(new LiteralControl("<b>Image not found"));
            }
            //this.Controls.Add(new LiteralControl("</td></tr></table>"));
            //this.Controls.Add(new LiteralControl("</div>"));
            Response.Redirect("~/Forms/Web Forms/DownLoadImages.aspx");
        }


        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;

            string ReturndetailID = (gvRow.FindControl("lblguid") as Label).Text;

            if (ReturndetailID != "")
            {
                List<string> lsImages2 = Obj.Rcall.ReturnImagesByReturnDetailsID(Guid.Parse(ReturndetailID));

                if (lsImages2.Count > 0)
                {

                    List<String> lsImages = new List<string>();
                    String ImgServerString = System.Configuration.ConfigurationManager.AppSettings["ImageServerPath"].ToString();
                    foreach (var Imaitem in lsImages2)
                    {
                        //lsImages.Add("~/images/"+Imaitem.Split(new char[] { '\\' }).Last().ToString());
                       // lsImages.Add(ImgServerString.Replace("#{ImageName}#", Imaitem.Split(new char[] { '\\' }).Last().ToString()));
                        lsImages.Add(Imaitem);
                    }

                    if (lsImages2.Count > 0)
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                            zip.AddDirectoryByName("Files2");
                            ////////// lblImagesFor.Text = "Images for GRA Detail Number : " + ReturnROWID;
                            for (int j = 0; j < lsImages2.Count; j++)
                            {


                                string path = lsImages2[j].ToString();


                                zip.AddFile(path, "Files2");
                            }
                            // flg = 2;

                            //this.Controls.Add(new LiteralControl(" <img src='" + path + "' height='400' width='400'>"));

                            // string filePath = path;
                            ///  Response.ContentType = ContentType;

                            //////  System.Web.UI.WebControls.Image img=new System.Web.UI.WebControls.Image();
                            /// img=ExtensionMethods.Upload(@"ftp://fileshare.kraususa.com", "rgauser", "rgaICG2014", "C:\\Images\\" +path,);



                            ///  Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));


                            ////  Response.WriteFile(filePath);
                            ///  Response.End();



                            Response.Clear();
                            Response.BufferOutput = false;
                            string zipName = String.Format("images{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                            Response.ContentType = "application/zip";
                            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);

                            //  zip.Save(@"D:\\kk\myFile.zip");

                            var ms = new MemoryStream();
                            zip.Save(ms);
                            ms.Position = 0;
                            ms.CopyTo(Response.OutputStream);

                            //  zipName.CopyTo(Response.OutputStream);
                            // Response.WriteFile(zipName);

                            Response.End();
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
            }

        }
        //protected void btnPrevious_Click(object sender, EventArgs e)
        //{
        //    ShowComments();
        //}

        //protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
        //    {
        //        RadioButton RowID = (RadioButton)gvReturnDetails.Rows[i].FindControl("rdbselect");
        //        RowID.Checked = false;
        //    }
        //    RadioButton RowID1 = (RadioButton)gvReturnDetails.Rows[gvReturnDetails.SelectedIndex].FindControl("rdbselect");
        //    RowID1.Checked = true;
        //}

    }
}