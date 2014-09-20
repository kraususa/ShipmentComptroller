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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmRMAEnter : System.Web.UI.Page
    {
        #region Declaration
        cmdReturn _retn = new cmdReturn();
       //Object of ModelReturn.
        Models.modelReturn _newRMA = new Models.modelReturn();
        modelReaturnUpdate _Update = new modelReaturnUpdate();
        //call smController Object. 
        smController call = new smController();

        DataTable DtReturnReason = new DataTable();

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
        Guid returnid;
        Boolean NonPo = true;

        //Count Reasons.
        int count;

        //Textbox Object of txtSkuID.
        TextBox txtSKUID;

        //Thread
        public static Thread CopyThread;
        int flagForDtReturnReason;
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

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                List<cSlipInfo> _lsslipinfo = new List<cSlipInfo>();
                // List Of return Reasons.
                List<Reason> lsReturn = _newRMA.GetReasons();

                // Adding Flag for Creating Columns for DtReturnReason table
                Views.Global.flagForDtReturnReason = flagForDtReturnReason;

                List<RMAComment> rmaComment = new List<RMAComment>();

                Session["rmacomment"] = rmaComment;

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
               txtreturndate.Text = DateTime.UtcNow.Date.ToString("MMM dd, yyyy");

               Obj._popupValue.PropertyChanged += _popupValue_PropertyChanged;
               Obj._popupValue.ReasnValue = "";
               txtSKUID = new TextBox();
            }
            ShowComments();
        }

        /// <summary>
        /// Fill Grid method Fills GridView.
        /// using Datatable.
        /// </summary>
        public void fillGrid()
        {

            //dt.Columns.Add("SKU");
            //dt.Columns.Add("ProductName");
            //dt.Columns.Add("Quantity");
            //dt.Columns.Add("Category");
            //dt.Columns.Add("Reasons");
            //dt.Columns.Add("SKUID");
            //dt.Columns.Add("ImageName");

            //DataRow dr = dt.NewRow();

            //dr[0] = "";
            //dr[1] = "";
            //dr[2] = "1";
            //dr[3] = "";
            //dr[4] = "Reasons";
            //dr[5] = "";
            //dr[6] = "";

            //dt.Rows.Add(dr);

            //gvReturnDetails.DataSource = dt;
            //gvReturnDetails.DataBind();


            dt.Columns.Add("SKUNumber");
            dt.Columns.Add("SKU_Qty_Seq");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("SKU_Sequence");
            dt.Columns.Add("SalesPrice");
            dt.Columns.Add("NoofImages");
            dt.Columns.Add("ImageName");
            dt.Columns.Add("LineType");
            dt.Columns.Add("ShipmentLines");
            dt.Columns.Add("ReturnLines");

            DataRow dr = dt.NewRow();

            dr[0] = txtNewItem.Text;
            dr[1] = "0";
            // dr[3] = "";
            dr[2] = "0";
            dr[3] = 1000;
            dr[4] = "0";
            dr[5] = "0 Image(s)";
            dr[6] = "";
            dr[7] = "1";
            dr[8] = 1000;
            dr[9] = 1000;
            // dr[12] = "";          


            dt.Rows.Add(dr);

            //gvReturnDetails.DataSource = dt;
            //gvReturnDetails.DataBind();




        
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
        #region ReturnReasons
        private String ReturnReasons()
        {
            String _ReturnReason = "";

            //((DataTable)Session["DT1"]) = ViewState["dt"] as DataTable;

            //for (int i = ((DataTable)Session["DT1"]).Rows.Count - 1; i >= 0; i--)
            //{
            //    DataRow d = ((DataTable)Session["DT1"]).Rows[i];
            //    if (d["SKU"].ToString() == ViewState["SelectedskuName"].ToString() && d["ItemQuantity"].ToString() == ViewState["ItemQuantity"].ToString())
            //        d.Delete();
            //}

           
            //if (chkitemdamaged.Checked == true) _ReturnReason = _ReturnReason + chkitemdamaged.Text;

            //if (chkitemdifferent.Checked == true) _ReturnReason = _ReturnReason + chkitemdifferent.Text;

            //if (chkduplicate.Checked == true) _ReturnReason = _ReturnReason + chkduplicate.Text;

            //if (chkitemordered.Checked == true) _ReturnReason = _ReturnReason + chkitemordered.Text;

            //if (chknotsatisfied.Checked == true) _ReturnReason = _ReturnReason + chknotsatisfied.Text;

            //if (chkwrongitem.Checked == true) _ReturnReason = _ReturnReason + chkwrongitem.Text;

            //_ReturnReason += txtotherreasons.Text;

            return _ReturnReason;

        }
#endregion

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

        #region Add Comment Button click event
        protected void btnComment_Click(object sender, EventArgs e)
        {
            fnforComment();
            ShowComments();
            lblMassege.Text = "Comment Added";
            mpePopupForCommentYes.Show();
        }
        #endregion

        #region Function For Comment
        public void fnforComment()
        {
            List<RMAComment> lscoment = new List<RMAComment>();

            lscoment = (List<RMAComment>)Session["rmacomment"];

            RMAComment lscomment = new RMAComment();
            lscomment.RMACommentID = Guid.NewGuid();
            lscomment.ReturnID = Views.Global.ReteunGlobal.ReturnID;
            lscomment.UserID = (Guid)Session["UserID"];
            lscomment.Comment = txtcomment.Text;
            lscomment.CommentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");

            // Views.Global.rmaComment.Add(lscomment);


            lscoment.Add(lscomment);

            Session["rmacomment"] = lscoment;

            //  }


            //rmaComment.RMACommentID = Guid.NewGuid();
            //rmaComment.ReturnID = Views.Global.ReteunGlobal.ReturnID;
            //rmaComment.UserID = (Guid)Session["UserID"];
            //rmaComment.Comment = txtcomment.Text;
            //rmaComment.CommentDate = DateTime.UtcNow;
            //Obj.Rcall.InsertRMACommnt(rmaComment);
            txtcomment.Text = "";
        }
        #endregion

        #region Showing Comments
        public void ShowComments()
        {
            this.Controls.Add(new LiteralControl("<div style=' border-radius: 11px 0 0 11px;  border: 1px solid; position : absolute; color:#179090; left :  1190px; right : 50px; top :137px;width:360px;height:220px;overflow: auto;'>"));
            List<RMAComment> lsComment = Obj.Rcall.GetRMACommentByReturnID(Views.Global.ReteunGlobal.ReturnID);
            foreach (var item in Views.Global.rmaComment.OrderByDescending(y => y.CommentDate))
            {
                this.Controls.Add(new LiteralControl("<table width='100%' >"));
                this.Controls.Add(new LiteralControl("<tr><td bgcolor='#8DC6FF'>"));
                this.Controls.Add(new LiteralControl("<h8> " + Obj.Rcall.GetUserInfobyUserID((Guid)item.UserID).UserFullName + " || " + item.CommentDate.ToString("MM/dd/yyyy hh:mm tt") + "</h8> "));
                this.Controls.Add(new LiteralControl("</td></tr><tr><td bgcolor='#FFFFFF'shape='rect'><b>" + item.Comment + "</td></tr>"));
                this.Controls.Add(new LiteralControl(" </table>"));
            }
            this.Controls.Add(new LiteralControl("</div>"));
        }
        #endregion

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
            if (!folderExists)
                Directory.CreateDirectory(@"C:\Images1\");
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

        protected void btnOk_Click(object sender, EventArgs e)
        {

            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

           // Response.Redirect(@"~\Forms\Web Forms\frmRMAPopup.aspx");
        }



        #region Button Click Event
        /// <summary>
        /// all Information save in all tables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnsave_Click(object sender, EventArgs e)
        //{
        //    Byte Status = Convert.ToByte(ddlstatus.SelectedValue);
        //    Byte Decision = Convert.ToByte(ddldecision.SelectedValue);

        //    //List of Return Information.
        //    List<Return> _lsreturn = new List<Return>();
        //    Return ret = new Return();
        //    ret.RMANumber = "";
        //    ret.VendoeName = txtvendername.Text;
        //    ret.VendorNumber = txtvendernumber.Text;
        //    ret.ReturnDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtrequestdate.Text), "Eastern Standard Time");
        //    ret.PONumber = txtponumber.Text;
        //    ret.CustomerName1 = txtcustomername.Text;
        //    ret.Address1 = txtcustomeraddress.Text;
        //    ret.City = txtcity.Text;
        //    ret.Country = txtcountry.Text;
        //    ret.ZipCode = txtzipcode.Text;
        //    ret.State = txtstate.Text;
        //    ret.ScannedDate = DateTime.UtcNow;
        //    ret.ExpirationDate = DateTime.UtcNow.AddDays(60);

        //    _lsreturn.Add(ret);
                
        //    //user Name Is Stored in sessoin.
        //    lsUserInfo = call.GetSelcetedUserMaster(Session["UName"].ToString());

        //    //Save Return information of New RMA.
          //  Guid ReturnID = _newRMA.SetReturnTbl(_lsreturn, ReturnReasons(), Status, Decision, lsUserInfo[0].UserID);


        //    // Display New RMA Number Means RGA number.
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('RMA number for this return is :" + _newRMA.GetNewROWID(ReturnID) + "');", true);

        //    //Save return Details from The Gridview. 
        //    for (int i = 0; i < gvReturnDetails.Rows.Count; i++)
        //    {
        //        string  sku = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtsku")).Text;
        //        string  productname = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtproductname")).Text;
        //        string  quantity = ((TextBox)gvReturnDetails.Rows[i].FindControl("txtquantity")).Text;
        //        string category = productcategory(sku, 1);

        //        if (sku != "" && productname != "")
        //        {
        //            ReturnDetailsID = _newRMA.SetReturnDetailTbl(ReturnID, sku, productname, 0, 0, Convert.ToInt32(quantity), category, lsUserInfo[0].UserID);
        //        }

        //        if (Obj._ReasonList.Count !=0)
        //        {
        //            string SkuReasons = Obj._ReasonList.SingleOrDefault(j => j.ID == i).ReasonString;
        //            if (SkuReasons != "" && SkuReasons != null)
        //            {
        //                foreach (Guid Ritem in (SkuReasons.GetGuid()))
        //                {
        //                    _newRMA.SetSkuReasons(Ritem, ReturnDetailsID);
        //                }
        //            }
        //        }

        //        string imglist = ((Label)gvReturnDetails.Rows[i].FindControl("lblImagesName")).Text;

        //        foreach (var item in imglist.Split(new char[] { '\n' }))
        //        {
        //            if(item!=null && item!="")
        //            {

        //             String NameImage =System.Configuration.ConfigurationManager.AppSettings["PhysicalPath"].ToString() +"\\" + item.ToString() ;
                       
        //             Guid ImageID = _newRMA.SetReturnedImages(Guid.NewGuid(), ReturnDetailsID, NameImage, lsUserInfo[0].UserID);
        //            }
        //        }
        //    }
        //    clear();
        //}

        protected void btnsave_Click(object sender, EventArgs e)
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
           // Views.Global.lsReturnDetailBySRNumber = Obj.Rcall.ReturnDetailBySRNumber(Request.QueryString["RMANumber"].ToString());

            //Set the Return Information in Return Table.
            // Guid returnid = _Update.SetReturnTbl(ret, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), Convert.ToDateTime(txtreturndate.Text),"");

            //  returnid = _Update.SetReturnByRGANumber(Views.Global.ReteunGlobal, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text);


            //if (Views.Global.ReteunGlobal.RMANumber == "N/A")
            //{
            //    if (Views.Global.ReteunGlobal.OrderNumber == "N/A")
            //    {
            //Views.Global.RMAInfoGlobal
           // List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomerByRMANumber(Request.QueryString["RMANumber"].ToString());


           // ret.RMANumber = txtRMANumber.Text;
           // ret.VendoeName = txtVendorName.Text;
            //ret.VendorNumber = txtVendorNumber.Text;
           // ret.ScannedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time");
           // ret.ExpirationDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Eastern Standard Time").AddDays(60);
           // eastern = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(txtRMAReqDate.SelectedDate.Value, "Eastern Standard Time");
            //ret.ReturnDate = eastern;
           // ret.PONumber = txtPoNumber.Text;
           // ret.CustomerName1 = txtName.Text;
           // ret.Address1 = txtAddress.Text;
           // ret.City = txtCustCity.Text;
           // ret.Country = txtCountry.Text;
            //ret.ZipCode = txtZipCode.Text;
            //ret.State = txtState.Text;
           // ret.CallTag = txtcalltag.Text;
            //ret.RGAROWID = txtRMANumber.Text;

            //List of Return Information.
            List<Return> _lsreturn = new List<Return>();
            Return ret = new Return();
            //ret.RMANumber = "";
            //ret.VendoeName = txtvendername.Text;
            //ret.VendorNumber = txtvendernumber.Text;
            //ret.ReturnDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtreturndate.Text), "Eastern Standard Time");
            //ret.PONumber = txtponumber.Text;
            //ret.CustomerName1 = txtcustomername.Text;
            //ret.Address1 = txtcustomeraddress.Text;
            //ret.City = txtcity.Text;
            //ret.Country = txtcountry.Text;
            //ret.ZipCode = txtzipcode.Text;
            //ret.State = txtstate.Text;
            //ret.ScannedDate = DateTime.UtcNow;
            //ret.ExpirationDate = DateTime.UtcNow.AddDays(60);
            //ret.CallTag = txtCalltag.Text;
           // ret.RGAROWID = "";

            ret.RMANumber = txtRMAnumber.Text;
            ret.RMAStatus = Convert.ToByte(ddlstatus.SelectedItem.Value);
            ret.ShipmentNumber = txtshipmentnumber.Text;
            ret.Decision = Convert.ToByte(ddldecision.SelectedItem.Value);
            ret.PONumber = txtponumber.Text;
            ret.ReturnDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtreturndate.Text), "Eastern Standard Time");
            ret.OrderDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(Convert.ToDateTime(txtorderdate.Text), "Eastern Standard Time");
            ret.CustomerName1 = txtcustomerName.Text;
            ret.VendoeName = txtvendorName.Text;
            ret.OrderNumber = txtordernumber.Text;
            ret.VendorNumber = txtvendornumber.Text;
            ret.CallTag = txtCalltag.Text;

            _lsreturn.Add(ret);




            //DateTime DeliveryDate = lsCustomeronfo[0].DeliveryDate;
            //DateTime CurrentDate = DateTime.UtcNow;
            //TimeSpan Diff = CurrentDate.Subtract(DeliveryDate);
            int Days = 0;
            Views.Global.ShipDate_ScanDate_Diff = Days;

            string wrongRMA = "0";
            string Warranty = "1";

            returnid = _Update.SetReturnTblForNewRMA(_lsreturn, Convert.ToByte(ddlstatus.SelectedValue.ToString()), Convert.ToByte(ddldecision.SelectedValue.ToString()), (Guid)Session["UserID"], ScannedDate, ExpirationDate, InProgress, txtCalltag.Text, wrongRMA, Warranty, 60, Views.Global.ShipDate_ScanDate_Diff);

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

                string ProductID = (gvReturnDetails.Rows[i].FindControl("txtProductID") as TextBox).Text;

                string SKUSequence = (gvReturnDetails.Rows[i].FindControl("txtSKU_Sequence") as TextBox).Text;

                string ProductName = SKUSequence;

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


                if (statusAndPoint.Count > 0)
                {
                    for (int j = statusAndPoint.Count - 1; j >= 0; j--)
                    {
                        if (statusAndPoint[j].SKUName == SKUNumber && statusAndPoint[j].NewItemQuantity == Convert.ToInt16(SKUSequence))
                        {
                            SKUNewName = SKUNumber;
                            ViewState["SKU_Staus"] = statusAndPoint[j].Status;
                            ViewState["TotalPoints"] = statusAndPoint[j].Points;
                            ViewState["IsScanned"] = statusAndPoint[j].IsScanned;
                            ViewState["IsManually"] = statusAndPoint[j].IsMannually;
                            ViewState["NewItemQty"] = statusAndPoint[j].NewItemQuantity;
                            ViewState["_SKU_Qty_Seq"] = statusAndPoint[j].skusequence;

                            statusAndPoint.RemoveAt(j);
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
                Session["listofstatusAndPoint"] = statusAndPoint;

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
                ReturnDetailsID = _Update.SetReturnDetailNewInsertTbl(Guid.NewGuid(), returnid, SKUNumber, ProductName, Convert.ToInt32(Rquantity), (Guid)Session["UserID"], (String)ViewState["SKU_Staus"], (int)ViewState["TotalPoints"], (int)ViewState["IsScanned"], (int)ViewState["IsManually"], Convert.ToInt16(SKUSequence), (int)ViewState["_SKU_Qty_Seq"], ProductID, Convert.ToDecimal(SalesPrice), Convert.ToInt16(Linetype), Convert.ToInt16(ShipmentLine), Convert.ToInt16(ReturnLine));
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


                if (((DataTable)Session["DT1"]).Rows.Count > 0)
                {
                    for (int k = ((DataTable)Session["DT1"]).Rows.Count - 1; k >= 0; k--)
                    {
                        DataRow d = ((DataTable)Session["DT1"]).Rows[k];
                        if (d["SKU"].ToString() == SKUNumber && d["ItemQuantity"].ToString() == SKUSequence)
                        {
                            //string RetirID = d["ReturnDetailID"].ToString();

                            //if (Guid.Parse(d["ReturnDetailID"].ToString()) == ReturnDetailsID && d["ReturnedSKUID"].ToString() != null && d["ReturnedSKUID"].ToString() != "")
                            //{
                            //    // Guid skureturn = Guid.Parse(d["ReturnedSKUID"].ToString());

                            //    Guid ReturnedSKUPoints = _Update.SetReturnedSKUPoints(Guid.Parse(d["ReturnedSKUID"].ToString()), ReturnDetailsID, returnid, ((DataTable)Session["DT1"]).Rows[k][0].ToString(), ((DataTable)Session["DT1"]).Rows[k][1].ToString(), ((DataTable)Session["DT1"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][4].ToString()));
                            //    d.Delete();
                            //}
                            //else
                            //{
                            _Update.SetReturnedSKUPoints(Guid.NewGuid(), ReturnDetailsID, returnid, ((DataTable)Session["DT1"]).Rows[k][0].ToString(), ((DataTable)Session["DT1"]).Rows[k][1].ToString(), ((DataTable)Session["DT1"]).Rows[k][2].ToString(), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][3].ToString()), Convert.ToInt16(((DataTable)Session["DT1"]).Rows[k][4].ToString()));
                            d.Delete();
                            //}

                        }
                    }
                }


                #endregion


                #region SaveComments


                if (((List<RMAComment>)Session["rmacomment"]).Count > 0)
                {

                    foreach (var item in ((List<RMAComment>)Session["rmacomment"]))
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
                //foreach (var n in Views.Global._lsSlipPrintSKUNumber)
                //{
                //    if (n == SKUNumber)
                //    {

                //        Guid userId = (Guid)Session["UserID"];
                //        string nm = Obj.Rcall.GetUserInfobyUserID(userId).UserName;
                //        //_retn.GetReturnTblByReturnID(returnid)
                //        var rr = _retn.GetReturnTblByReturnID(returnid).RGAROWID;
                //        string nrr = rr.ToString();
                //        Views.Global.lsSlipInfo = _Update.GetSlipInfo(_lsreturn, SKUNumber, Obj.Rcall.EncodeCode(n), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);
                //        //  Views.Global.lsSlipInfo = _Update.GetSlipInfo(_lsreturn, Global.arr[i], Obj.Rcall.EncodeCode(Global.arr[i]), "", nrr, ddlstatus.SelectedIndex.ToString(), "Refund", nm);

                //     //   Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('http://192.168.1.16:12/Forms/Web%20Forms/frmSlipPrint.aspx','_newtab');", true);






                //        // literal.Text += "a ID='linkcontact' runat='server' href='" + "www.website./pagename.aspx?ID=" + id + "'>contact</a>";
                //    }
                //}
                #endregion







                // _Update.SetReturnDetailTbl(lsretundetail[i], Convert.ToInt16(Dquantity), Convert.ToInt16(Rquantity), SKUNumber,ProductName);

            }

            //Clear the Reasons list from Global Object.
            Obj._ReasonList = new List<Views.ReasonList>();

            //  Response.Redirect("~/Forms/Web Forms/frmRetunDetail.aspx");
          //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('frmSlipPrint.aspx','_newtab');", true);

            lblMassege.Text = "Success";
            mpePopupForSaveYes.Show();
            //  ModalPopupExtender1.Show();
            clear();
            //lblUser.Text = "Please Select Any One Option";
           // ModalPopupExtender1.Show();
        }
        #endregion

        #region
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
           // ReturnReasons();
            System.Threading.Thread.Sleep(3000);

            lblMassege.Text = "Process is completed";


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
                Session["DT1"] = DtReturnReason;

            }


            Views.Global.flagForDtReturnReason = 1;





            #region DtOperaion
            // DataRow dr = ((DataTable)Session["DT1"]).NewRow();

            //DataRow dr0 = DtReturnReason.NewRow();
            //dr0["SKU"] = ViewState["SelectedskuName"];
            //dr0["ItemQuantity"] = ViewState["ItemQuantity"];
            DataRow dr = ((DataTable)Session["DT1"]).NewRow();
            dr["SKU"] = ViewState["SelectedskuName"];
            dr["ItemQuantity"] = ViewState["ItemQuantity"];

            // string retun = ViewState["ReturnDetailID"].ToString();

            //if (ViewState["ReturnDetailID"].ToString() == "" || ViewState["ReturnDetailID"].ToString() == null)
            //{
            //    dr["ReturnDetailID"] = "00000000-0000-0000-0000-000000000000";
            //}
            //else
            //{
            //    dr["ReturnDetailID"] = ViewState["ReturnDetailID"];
            //}

            if (brdItemNew.Items.FindByText("Yes").Selected == true)
            {
                dr["Reason"] = lblitemNew.Text;
                dr["Reason_Value"] = "Yes";
                dr["Points"] = 100;
                ((DataTable)Session["DT1"]).Rows.Add(dr);
            }
            else if (brdItemNew.Items.FindByText("No").Selected == true)
            {
                dr["Reason"] = lblitemNew.Text;
                dr["Reason_Value"] = "No";
                dr["Points"] = 0;
                ((DataTable)Session["DT1"]).Rows.Add(dr);
            }

            DataRow dr1 = ((DataTable)Session["DT1"]).NewRow();
            dr1["SKU"] = ViewState["SelectedskuName"];
            dr1["ItemQuantity"] = ViewState["ItemQuantity"];
            //if (ViewState["ReturnDetailID"].ToString() == "" || ViewState["ReturnDetailID"].ToString() == null)
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
            //if (ViewState["ReturnDetailID"].ToString() == "" || ViewState["ReturnDetailID"].ToString() == null)
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
            //if (ViewState["ReturnDetailID"].ToString() == "" || ViewState["ReturnDetailID"].ToString() == null)
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
            //if (ViewState["ReturnDetailID"].ToString() == "" || ViewState["ReturnDetailID"].ToString() == null)
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
                ((DataTable)Session["DT1"]).Rows.Add(dr4);
            }
            else if (brdDefecttransite.Items.FindByText("No").Selected == true)
            {
                dr4["Reason"] = lblDefectintransite.Text;
                dr4["Reason_Value"] = "No";
                dr4["Points"] = 0;
                ((DataTable)Session["DT1"]).Rows.Add(dr4);
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
            Views.Global.listofstatusAndPoint.Add(_lsstatusandpoints);

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
            brdItemNew.Enabled = false;
            brdInstalled.Enabled = false;
            brdstatus.Enabled = false;
            brdManufacturer.Enabled = false;
            brdDefecttransite.Enabled = false;
           

            brdItemNew.Items.FindByText("Yes").Selected = false;
            brdItemNew.Items.FindByText("No").Selected = false;

            brdInstalled.Items.FindByText("Yes").Selected = false;
            brdInstalled.Items.FindByText("No").Selected = false;

            brdstatus.Items.FindByText("Yes").Selected = false;
            brdstatus.Items.FindByText("No").Selected = false;

            brdManufacturer.Items.FindByText("Yes").Selected = false;
            brdManufacturer.Items.FindByText("No").Selected = false;

            brdDefecttransite.Items.FindByText("Yes").Selected = false;
            brdDefecttransite.Items.FindByText("No").Selected = false;

          
            lblMassege.Text = "Submit information";
            mpePopupForSubmitYes.Show();
        }
        #endregion


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

        protected void brdReasonstatus_SelectedIndexChanged(object sender, EventArgs e)
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



        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < gvReturnDetails.Rows.Count; j++)
                {
                    RadioButton rb = (gvReturnDetails.Rows[j].FindControl("RadioButton1")) as RadioButton;
                    if (rb.Checked == true)
                    {
                        //UpdatePanel1.Visible = true;


                        #region Deepak
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
                            
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "<script language='javascript'>alert('Can not add comment/parent sku for combination item');</script>");
                            lblMassege.Text = "Can not add comment/parent sku for combination item";
                            //  string display = "This is Line Type 6";
                            // ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + display + "');", true);

                            rb.Checked = false;

                            btnsubmit.Enabled = false;
                            brdItemNew.Enabled = false;
                            brdInstalled.Enabled = false;
                            brdstatus.Enabled = false;
                            brdManufacturer.Enabled = false;
                            brdDefecttransite.Enabled = false;


                            brdItemNew.Items.FindByText("Yes").Selected = false;
                            brdItemNew.Items.FindByText("No").Selected = false;

                            brdInstalled.Items.FindByText("Yes").Selected = false;
                            brdInstalled.Items.FindByText("No").Selected = false;

                            brdstatus.Items.FindByText("Yes").Selected = false;
                            brdstatus.Items.FindByText("No").Selected = false;

                            brdManufacturer.Items.FindByText("Yes").Selected = false;
                            brdManufacturer.Items.FindByText("No").Selected = false;

                            brdDefecttransite.Items.FindByText("Yes").Selected = false;
                            brdDefecttransite.Items.FindByText("No").Selected = false;
                        }

                    }
                }
            }
            catch (Exception)
            {
            }

            //  GetCount();

        }


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


        #region Add New Product Button Click Event
        protected void btnaddnew_Click1(object sender, EventArgs e)
        {
            txtNewItem.Visible = true;
            BtnAddNewItem.Visible = true;
        }
        #endregion


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
            //txtcity.Text = "";
            //txtcountry.Text = "";
            //txtcustomeraddress.Text = "";
            //txtcustomername.Text="";
            //txtotherreasons.Text = "";
            //txtponumber.Text = "";
            //txtrmanumber.Text = "";
            //txtstate.Text = "";
            //txtvendernumber.Text = "";
            //txtvendername.Text="";
            //txtzipcode.Text = "";
            txtCalltag.Text = "";
            txtcomment.Text = "";
            txtcustomerName.Text = "";
            txtNewItem.Text = "";
            txtorderdate.Text = "";
            txtordernumber.Text = "";
            txtotherreasons.Text = "";
            txtponumber.Text = "";
            txtreturndate.Text = "";
            txtrganumber.Text = "";
            txtRMAnumber.Text = "";
            txtshipmentnumber.Text = "";
            txtvendorName.Text = "";
            txtvendornumber.Text = "";

            ddldecision.SelectedIndex = 0;
            ddlotherreasons.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            //chkduplicate.Checked = false;
            //chkitemdamaged.Checked = false;
            //chkitemdifferent.Checked = false;
            //chkitemordered.Checked = false;
            //chknotsatisfied.Checked = false;
            //chkwrongitem.Checked = false;

            //dt.Columns.Add("SKU");
            //dt.Columns.Add("ProductName");
            //dt.Columns.Add("Quantity");
            //dt.Columns.Add("Category");
            //dt.Columns.Add("Reasons");
            //dt.Columns.Add("SKUID");
            //dt.Columns.Add("ImageName");

            dt.Columns.Add("SKUNumber");
            dt.Columns.Add("SKU_Qty_Seq");
            dt.Columns.Add("ProductID");
            dt.Columns.Add("SKU_Sequence");
            dt.Columns.Add("SalesPrice");
            dt.Columns.Add("NoofImages");
            dt.Columns.Add("ImageName");
            dt.Columns.Add("LineType");
            dt.Columns.Add("ShipmentLines");
            dt.Columns.Add("ReturnLines");

            DataRow dr = dt.NewRow();

            dr[0] = txtNewItem.Text;
            dr[1] = "0";
            // dr[3] = "";
            dr[2] = "0";
            dr[3] = "";
            dr[4] = "0";
            dr[5] = "0 Image(s)";
            dr[6] = "";
            dr[7] = "";
            dr[8] = "";
            dr[9] = "";
            // dr[12] = "";          
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
            //if (txtponumber.Text.Trim() != "")
            //{
            //    List<RMAInfo> lsCustomeronfo = _newRMA.GetCustomer(txtponumber.Text);

            //    if (lsCustomeronfo.Count > 0)
            //    {
            //        txtponumber.Text = lsCustomeronfo[0].PONumber;
            //        txtcustomeraddress.Text = lsCustomeronfo[0].Address1;
            //        txtcountry.Text = lsCustomeronfo[0].Country;
            //        txtcity.Text = lsCustomeronfo[0].City;
            //        txtstate.Text = lsCustomeronfo[0].State;
            //        txtzipcode.Text = lsCustomeronfo[0].ZipCode;
            //        txtcustomername.Text = lsCustomeronfo[0].CustomerName1;
            //    }
            //}
        }

        protected void txtvendername_TextChanged(object sender, EventArgs e)
        {
            if (txtvendorName.Text.Trim() != "")
            {
                string lsvendername = _newRMA.GetVenderNumberByVenderName(txtvendorName.Text);

                txtvendornumber.Text = lsvendername;
            }
        }

        protected void txtvendernumber_TextChanged(object sender, EventArgs e)
        {
            //if (txtvendernumber.Text.Trim() != "")
            //{
            //    string lsvendernumber = _newRMA.GetVenderNameByVenderNumber(txtvendernumber.Text);

            //    txtvendername.Text = lsvendernumber;
            //}
        }

        protected void lkbtnPath_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmHomePage.aspx");
        }

        protected void lkbtnPath1_Click(object sender, EventArgs e)
        {

        }

        protected void btnOkForSaveYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Forms/Web Forms/frmDemoGrid.aspx");
        }


    }
}