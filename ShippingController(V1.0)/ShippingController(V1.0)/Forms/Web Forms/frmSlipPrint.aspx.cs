
using ShippingController_V1._0_.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Media;
using System.IO;
using ShippingController_V1._0_.Models;
using KeepAutomation.Barcode.Bean;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmSlipPrint : System.Web.UI.Page
    {
        BarCode barcode = new BarCode();
        //public UPCA upc = null;
        //BarcodeLib.Barcode b = new BarcodeLib.Barcode();
      //  List<cSlipInfo> _lsInfoSlip = new List<cSlipInfo>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //int i = 0;
            //foreach (var n in Global._lsSlipPrintSKUNumber)
            //{


            //C:\Users\Shiva3\Documents\GitHub\ShipmentComptroller\ShippingController(V1.0)\ShippingController(V1.0)\Themes\Images
            int k = 0;

            string imgurlPrd = "C://Users/Shiva3/Documents/GitHub/ShipmentComptroller/ShippingController(V1.0)/ShippingController(V1.0)/Themes/Images/barcodeProduct"+k+".png";

            string imgurl = "C://Users/Shiva3/Documents/GitHub/ShipmentComptroller/ShippingController(V1.0)/ShippingController(V1.0)/Themes/Images/barcode" + k + ".png";
            FileInfo TheFile = new FileInfo(imgurl);
            if (TheFile.Exists)
            {
                File.Delete(imgurl);   // It not works if file is used in another process
            }
            FileInfo TheFile2 = new FileInfo(imgurlPrd);
            if (TheFile2.Exists)
            {
                File.Delete(imgurlPrd);   // It not works if file is used in another process
            }

                for (int i = 0; i < Global.lsSlipInfo.Count; i++)
                {
                    k++;
                // _lsInfoSlip = Global.lsSlipInfo;

                    imgurlPrd = "C://Users/Shiva3/Documents/GitHub/ShipmentComptroller/ShippingController(V1.0)/ShippingController(V1.0)/Themes/Images/barcodeProduct" + k + ".png";

                    imgurl = "C://Users/Shiva3/Documents/GitHub/ShipmentComptroller/ShippingController(V1.0)/ShippingController(V1.0)/Themes/Images/barcode" + k + ".png";


                string SRnumber = Global.lsSlipInfo[i].SRNumber;
                string SKUName = Global.lsSlipInfo[i].ProductName;

                //string SKUName = Global.lsSlipInfo[i].EANCode;
                string productname = Global.lsSlipInfo[i].EANCode;
                DateTime ReceivedDate = Global.lsSlipInfo[i].ReceivedDate;
                DateTime Expiration = Global.lsSlipInfo[0].Expiration;
                string UserName = Global.lsSlipInfo[0].ReceivedBY;
                string RMAStatusReal = "N/A";
                String RMAStatus = Global.lsSlipInfo[0].RMAStatus;

                if (RMAStatus == "0")
                {
                    RMAStatusReal = "Incomplete";
                }
                else if (RMAStatus == "1")
                {
                    RMAStatusReal = "Complete"; //"Rejected";
                }
                else if (RMAStatus == "2")
                {
                    RMAStatusReal = "Wrong RMA";//"Rejected";
                }

                string ItemStatus = Global.lsSlipInfo[0].ItemStatus;

                string Reason = "N/A";

                if (Global.lsSlipInfo[0].Reason != "")
                    Reason = Global.lsSlipInfo[0].Reason;

                ////var sBoxNumber = b.Encode(BarcodeLib.TYPE.CODE128, SRnumber, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 1500, 550);
                // var sproductname = b.Encode(BarcodeLib.TYPE.UPCA, productname, System.Drawing.Color.Black, System.Drawing.Color.Transparent, 2000, 500);

                string txtTextToAdd = Global.lsSlipInfo[i].EANCode;

                if (Global.lsSlipInfo[0].EANCode == "" || Global.lsSlipInfo[0].EANCode == "N/A")
                {
                    txtTextToAdd = "000000000000";
                    SKUName = "*[UPC Code Not Found] " + SKUName;
                }





                BarcodeUpca ua = new BarcodeUpca();

                if (txtTextToAdd != "")
                {
                    //this.txtTextToAdd.Text = this.txtTextToAdd.Text.Substring(0, 11) + upca.GetCheckSum(this.txtTextToAdd.Text).ToString();
                    //System.Drawing.Image img;
                    //img = upca.CreateBarCode(this.txtTextToAdd.Text, 3);

                    //this.image.Left = System.Convert.ToInt32((this.image.Width / 2) - (img.Width / 2));
                    ////Deepak 

                    txtTextToAdd = txtTextToAdd.Substring(0, 11) + ua.GetCheckSum(txtTextToAdd).ToString();
                    System.Drawing.Image img;
                    img = ua.CreateBarCode(txtTextToAdd, 3);
                    string tempPath = Path.GetTempFileName();

                    // string imgurlPrd ="D://barcodeProduct.png";
                    //  string imgurl ="D://barcode.png";
                  
                    //string imgurlPrd = "~/barcodeProduct.png";
                    ////(@"ftp://fileshare.kraususa.com
                    ///  string imgurl = @"ftp://fileshare.kraususa.com/barcode.png";
                    ///  

                    try
                    {
                       

                        img.Save(imgurlPrd);

                        ///  ExtensionMethods.Upload(@"ftp://fileshare.kraususa.com", "rgauser", "rgaICG2014", "D:\\barcodeProduct.png");


                        barcode.Symbology = KeepAutomation.Barcode.Symbology.UPCE;

                        // barcode.CodeToEncode = txtTextToAdd.Text;
                        barcode.ChecksumEnabled = true;
                        barcode.X = 1;
                        barcode.Y = 50;
                        barcode.BarCodeWidth = 100;
                        barcode.BarCodeHeight = 70;
                        barcode.Orientation = KeepAutomation.Barcode.Orientation.Degree0;
                        barcode.BarcodeUnit = KeepAutomation.Barcode.BarcodeUnit.Pixel;
                        barcode.DPI = 72;
                        barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;


                        ///   Image1.ImageUrl = "~/Themes/Images/barcode.png";
                   ///     Image1.ImageUrl = "~/Themes/Images/barcodeProduct.png";
                        barcode.CodeToEncode = SRnumber;
                        barcode.Symbology = KeepAutomation.Barcode.Symbology.Code128A;
                        barcode.generateBarcodeToImageFile(imgurl);

                 ///       imgbarcode.ImageUrl = "~/Themes/Images/barcode.png";
                        // File.Delete(imgurl);  
                      ///  txtTextToAdd.Visible = false;
                      ///  
                        //////For print Dynamically
                        this.Controls.Add(new LiteralControl("<form id='form1' runat='server'>"));
                        this.Controls.Add(new LiteralControl("<html><body>"));
                        this.Controls.Add(new LiteralControl("<table width='100%' style='border: medium solid;'><tr><td  style='width: 10px;'>"));
                        this.Controls.Add(new LiteralControl("<div id ='printdiv'><table><tr><td>Received  :</td><td>" + ReceivedDate.ToString("MMM dd, yyyy") + "</td> </tr><tr><td>Expiration :</td><td>" + Expiration.ToString("MMM dd, yyyy") + "</td></tr><tr><td>Recived By:</td><td>" + UserName + "</td></tr><tr><td>Reasons  :</td><td>" + Reason + "</td></tr><tr><td>RMA Status:</td><td>" + RMAStatusReal + "</td></tr><tr><td>Item Status:</td><td>" + ItemStatus + "</td></tr><tr><td></td><td></td></tr></table></div>"));
                        this.Controls.Add(new LiteralControl("</td><td>"));
                        this.Controls.Add(new LiteralControl("<div id ='barcoderga'><table><tr><td><img src='../../Themes/Images/barcode" + k + ".png' alt='No Barcode' ></td></tr><tr><td>" + SRnumber + "</td></tr></table></div>"));
                        this.Controls.Add(new LiteralControl("</td></tr><tr><td>"));
                        this.Controls.Add(new LiteralControl("<div id ='barcodeSKU'><table><tr><td><img src='../../Themes/Images/barcodeProduct" + k + ".png' alt='No Barcode'></td></tr><tr><td>" + SKUName + "</td></tr></table></div>"));
                        this.Controls.Add(new LiteralControl("</td></tr></table>"));
                               
                     
                        // this.Controls.Add(new LiteralControl(" printWindow.document.write('</body></html>'); printWindow.document.close(); setTimeout(function () { printWindow.print(); }, 500); return false; }</script>"));
                        this.Controls.Add(new LiteralControl("</body></html>"));

                        this.Controls.Add(new LiteralControl("<P CLASS='pagebreakhere'>"));

                        this.Controls.Add(new LiteralControl("</form  > "));
                        /////end


                        //txtExpiration.Text = Expiration.ToString("MMM dd, yyyy");
                //txtReceivedDate.Text = ReceivedDate.ToString("MMM dd, yyyy");
                //txtReceived.Text = UserName;
                //txtReason.Text = Reason;
                //txtSRNumber.Text = SRnumber;
                //txtproductName.Text = SKUName;
                //txtRMAStatus.Text = RMAStatusReal;
                //txtItemStatus.Text = ItemStatus;



                    }
                    catch (Exception)
                    {
                    }



                    ///End

                    //   var newimag = Imaging.CreateBitmapSourceFromHBitmap(imges.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    //  var newimag = System.Windows.intrptr.Imaging.CreateBitmapSourceFromHBitmap(imges.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                    ///  Image1.ImageUrl = newimag;

                    //this.pctBarCode.Image = img;
                    //this.txtTextToAdd.SelectAll();
                }
                else
                {
                   //// this.imgbarcode.ImageUrl = null;
                }

                ///// var bitmapBox = new System.Drawing.Bitmap(sBoxNumber);
                //   var pbitmapBox = new System.Drawing.Bitmap(sproductname);

                ///////  var bBoxSource = Imaging.CreateBitmapSourceFromHBitmap(bitmapBox.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                // var pproduct = Imaging.CreateBitmapSourceFromHBitmap(pbitmapBox.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                ///   bitmapBox.Dispose();

                //   imgbarcode.ImageUrl = bBoxSource;
                //  image.Source = pproduct;

                //txtExpiration.Text = Expiration.ToString("MMM dd, yyyy");
                //txtReceivedDate.Text = ReceivedDate.ToString("MMM dd, yyyy");
                //txtReceived.Text = UserName;
                //txtReason.Text = Reason;
                //txtSRNumber.Text = SRnumber;
                //txtproductName.Text = SKUName;
                //txtRMAStatus.Text = RMAStatusReal;
                //txtItemStatus.Text = ItemStatus;

            //}
              
                
                    //this.Controls.Add(new LiteralControl("<script type = 'text/javascript'> function PrintPanel() {var div = document.getElementById('<%=prntdiv.ClientID %>'); var printWindow = window.open('', '', 'height=400,width=800'); printWindow.document.write('<html><head><title>DIV Contents</title>') printWindow.document.write('</head><body >') printWindow.document.write("));


                //}

            }
        }
    }
}