<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="DemoGrid.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.DemoGrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--  <%@ Page Language="C#" %>--%>

    <%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
    <%@ Import Namespace="ShippingController_V1._0_.Models" %>
    <%@ Import Namespace="PackingClassLibrary.CustomEntity.SMEntitys.RGA" %>
    <%@ Import Namespace="ShippingController_V1._0_.Views" %>


    <script runat="server">
    
        Boolean flag = true;
             
        
              
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

    
    
    
        protected void Button3_Click(object sender, DirectEventArgs e)
        {
            //string RowIndex = e.ExtraParams["rowIndex"].ToString();
            var mylst = new List<string>();
            string[] ass = { };
            StringBuilder result = new StringBuilder();

            string command = e.ExtraParams["command"].ToString();
            //string RowID = e.ExtraParams["rowIndex"].ToString();
            string RowID = e.ExtraParams["RowID"].ToString();
            mylst.Add(RowID);


            Session["RGAROWIDPrint"] = mylst.ToArray();

             // string script = "window.open('http://localhost:44038/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";
            string script = "window.open('http://192.168.1.16:14/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";

            this.Button3.AddScript(script);
        }
    </script>




    <%-- <script runat="server">
   protected void Button3_Click(object sender, DirectEventArgs e)
   {
       string script = "window.open('http://localhost:44038/Forms/Web%20Forms/frmLogin.aspx', 'myNewWindow')";
       //string script = "window.open('http://192.168.1.16:12/Forms/Web%20Forms/DemoGrid.aspx', 'myNewWindow')";  

       this.Button3.AddScript(script);
   }
</script>--%>



    <script>
        var template = '<Div style="background-color:{0};color:{0};">{1}</Div>';

        var ProgressFlag = function (valu) {
            if (valu > 0) {
                return Ext.String.format(template, (valu > 0) ? "red" : "green", 'F');
            }
            else {
                // return Ext.String.format(template, (valu > 0) ? "green" : "red", 'F');
            }
        };
        //var template = '<span style="background:{0};">{1}</span>';

        //var ProgressFlag = function (valu) {
        //    if (valu > 0) {
        //        return Ext.String.format(template, (valu > 0) ? "red" : "green", 'Flag');
        //        // return 'different-color-class';
        //        // valu.backgroundColor("blue");
        //        meta.style = "background-color:green;";
        //    }
        //    else {
        //        return Ext.String.format(template, (valu > 0) ? "green" : "red", '');
        //    }
        //};

        //var pctChange = function (value) {
        //    return Ext.String.format(template, (value > 0) ? "green" : "red", value + "%");
        //};



    </script>


    <script>
        Ext.net.FilterHeader.behaviour.addBehaviour("string", {
            name: "any",

            is: function (value) {
                return Ext.net.StringUtils.startsWith(value, "any ");
            },

            getValue: function (value) {
                var values = Ext.net.FilterHeader.behaviour.getStrValue(value).substring(4).split(" "),
                tmp = [];

                Ext.each(values, function (v) {
                    v = v.trim();
                    if (!Ext.isEmpty(v)) {
                        tmp.push(v);
                    }
                });

                values = tmp;

                return { value: values, valid: values.length > 0 };
            },

            match: function (recordValue, matchValue) {
                for (var i = 0; i < matchValue.length; i++) {
                    if (recordValue === matchValue[i]) {
                        return true;
                    }
                }

                return false;
            },

            isValid: function (value) {
                return this.getValue(value, field).valid;
            },

            serialize: function (value) {
                return {
                    type: "string",
                    op: "any",
                    value: value
                };
            }
        });



    </script>

    <script type="text/javascript">
        function ButtonRed() {
            document.body.style.backgroundColor = "blue"

        }

    </script>

    <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["flag"] = true;

            if (!X.IsAjaxRequest)
            {
                // Obj.Rcall.ReturnAll();
                this.BindData();
            }
        }

        protected void MyData_Refresh(object sender, StoreReadDataEventArgs e)
        {
            this.BindData();
        }

        private void BindData()
        {
            //Store store = this.GridPanel1.GetStore();

            //store.DataSource = this.Data;
            //store.DataBind();

          //  ShippingController_V1._0_.Views.Global.lsReturn1 = Obj.Rcall.ReturnAll().OrderByDescending(i => i.UpdatedDate).ToList();

            Session["lsReturn1"] = Obj.Rcall.ReturnAll().OrderByDescending(i => i.UpdatedDate).ToList();

            List<Return> lsReturn2 = new List<Return>();

            //if (flag == true)
            //{
            //    TodaysData();
            //}
            //else
            //{
                if (Session["transaction"] == "pending")
                {
                    PendingDecision();
                }
                else if (Session["transaction"] == "viewall")
                {
                    CheckAll();
                }
                else
                {
                    TodaysData();
                }

          //  }
            
            
            //if (Session["transaction"] == "todays")
            //{
            //    var updatedBy = from up in Obj.Rcall.Todaysall()
            //                    select new
            //                    {
            //                        up.RGAROWID,
            //                        up.RMANumber,
            //                        up.PONumber,
            //                        up.OrderNumber,
            //                        up.ShipmentNumber,
            //                        up.ReturnDate,
            //                        up.CustomerName1,
            //                        up.VendoeName,

            //                        up.UpdatedDate,

            //                        up.ProgressFlag,
            //                        up.RMAStatus,
            //                        up.Decision,

            //                        UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
            //                    };





            //    Store store = this.GridPanel1.GetStore();
            //    this.Store1.DataSource = updatedBy.ToList(); //Obj.Rcall.DataforToday(ShippingController_V1._0_.Views.Global.lsReturn1).ToList();
            //    this.Store1.DataBind();
            //}
            //else if (Session["transaction"] == "pending")
            //{
            //    PendingDecision();
            //}
            //else if (Session["transaction"] == "viewall")
            //{
            //    CheckAll();
            //}
            //else
            //{
            //    TodaysData();
            //}
            

      

            int count = Obj.Rcall.Todaysall().Count;

            int pending = Obj.Rcall.PendingDecision().Count;

            int ViewAll = ((List<Return>)Session["lsReturn1"]).Count;

            btntodays.Text = count + " " + "Today's Received Transaction.";

            btnPending.Text = pending + " " + "Pending Decision Transaction.";

            btnViewAll.Text = "View All" + "(" + ViewAll + ")";



            //Store store1 = this.ComboBox12.GetStore();
            //this.Store2.DataSource = lsReturn1;
            //this.Store2.DataBind();


            //Store storePOnumber = this.ComboBoxforPOnumber.GetStore();
            //this.Store3.DataSource = lsReturn1;
            //this.Store3.DataBind();




            //List<Return> arr = Obj.Rcall.ReturnAll(); //.Cast<object>().ToArray();
            //foreach (Return row in arr)
            //{
            //    ReturnForFrid lsReturn = new ReturnForFrid();
            //    //List<ReturnForFrid> lsreturb = new List<ReturnForFrid>();
            //    string lastUpdatedBy = row.UpdatedBy.ToString();

            //    if (lastUpdatedBy == "")
            //    {
            //        lsReturn.UpdatedBy = "";
            //        //row.Cells[14].Text = "";
            //    }
            //    else
            //    {
            //        Guid UserID = Guid.Parse(lastUpdatedBy);
            //        lsReturn.UpdatedBy = Obj.Rcall.GetUserInfobyUserID(UserID).UserFullName;
            //    }
            //}




            //GridPanel1

        }
        public void Button1_Click2(object sender, DirectEventArgs e)
        {
            var mylst = new List<string>();
            string[] ass = { };
            StringBuilder result = new StringBuilder();

            /////////  result.Append("<b>Selected Rows (ids)</b></br /><ul>");
            RowSelectionModel sm = this.GridPanel1.GetSelectionModel() as RowSelectionModel;

            foreach (SelectedRow row in sm.SelectedRows)
            {
                /////result.Append("<li>" + row.RecordID + "</li>");
                mylst.Add(row.RecordID);
            }

            ////// result.Append("</ul>");  ~/Forms/Web Forms/
            // this.Label2.Html = result.ToString();


            Session["RGAROWIDPrint"] = mylst.ToArray();


            //string script = "window.open('http://localhost:44038/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";
            string script = "window.open('http://192.168.1.16:14/Forms/Web%20Forms/frmRMAFormPrint2.aspx', 'myNewWindow')";

            this.Button3.AddScript(script);


            //Response.Redirect("frmRMAFormPrint2.aspx");

            //string script = "window.open('http://localhost:44038/Forms/Web%20Forms/frmLogin.aspx', 'myNewWindow')";
            ////string script = "window.open('http://192.168.1.16:12/Forms/Web%20Forms/DemoGrid.aspx', 'myNewWindow')";

            //this.Button1.AddScript(script);





            //  callPrintPage

            // ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('frmRMAFormPrint2.aspx','Graph','height=400,width=500');", true);


            ///http://localhost:44038/Forms/Web%20Forms/frmLogin.aspx

            //string pgrl = @"~/Forms/Web%20Forms/frmRMAFormPrint2.aspx";
            //string blank = "_blank";


            //Response.End();

            //  string script = "window.open('http://Forms/Web Forms/frmRMAFormPrint2.aspx',target='_blank');";


            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenPage", script, true);



            /// Button1_Click2.OnClientClick = String.Format("window.open({0});return false;", LocationSkidPackReportPage);


            //result.Append("window.open('");
            //result.Append(""+pgrl+"");
            //result.Append("');'");
            //result.Append("<'/script'>");
            //ClientScript.RegisterStartupScript(this.GetType(), "script", result.ToString());




        }
        protected void LoadCombo2(object sender, DirectEventArgs e)
        {
            //this.ComboBox1.AddItem("List1", "L1");
            //this.ComboBox1.AddItem("List2", "L2");
            //var RMA = from returnALL in Obj.Rcall.ReturnAll()
            //          where returnALL.RMANumber == this.ComboBox12.SelectedItem.Text
            //          select returnALL;

            //BindByCombobox(Obj.Rcall.DataforSearch(Obj.Rcall.ReturnAll(), this.ComboBox12.SelectedItem.Text));

        }

        protected void LoadComboForPOnumber(object sender, DirectEventArgs e)
        {
            //this.ComboBox1.AddItem("List1", "L1");
            //this.ComboBox1.AddItem("List2", "L2");
            //var RMA = from returnALL in Obj.Rcall.ReturnAll()
            //          where returnALL.RMANumber == this.ComboBox12.SelectedItem.Text
            //          select returnALL;

            //BindByCombobox(Obj.Rcall.DataforSearchforPOnumber(Obj.Rcall.ReturnAll(), this.ComboBoxforPOnumber.SelectedItem.Text));

        }

        public void BindByCombobox(List<Return> lsReturn)
        {
            List<Return> lsReturn1 = new List<Return>();
            lsReturn1 = lsReturn;

            this.Store1.DataSource = lsReturn1;
            this.Store1.DataBind();



        }


        private object[] Data
        {
            get
            {
                // Obj._lsreturn = lsReturn;
                // List<Return> arr = Obj.Rcall.ReturnAll(); //.Cast<object>().ToArray();


                object[] array = Obj.Rcall.lsReturnForGrid().Cast<object>().ToArray();

                return array;

            }
        }
    </script>

    <!DOCTYPE html>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <html>


    <head>
        <title></title>
        <script type="text/javascript">
            function se() {
                window.open('frmRMAFormPrint2.aspx');
            }
        </script>

        <script runat="server">

            [DirectMethod]
            public void ShowEditPage(string RowId)
            {
                // this.Label1.Text = DateTime.Now.ToLongTimeString();
                Response.Redirect("~/Forms/Web Forms/frmReturnEdit.aspx?RGAROWID=" + RowId);

            }
            [DirectMethod]
            public void PrintFunction(string RowId)
            {
                var myList = new List<string>();
                string[] arr = { };
                int i = 0;
                //   String RowId = (((GridViewRow)((LinkButton)sender).Parent.Parent).Cells[0].FindControl("lbtnRGANumberID") as LinkButton).Text;

                myList.Add(RowId);


                Session["RGAROWIDPrint"] = myList.ToArray();

                Response.Redirect("~/Forms/Web Forms/frmRMAFormPrint2.aspx");





            }

            [DirectMethod]
            public void applyFilterForSRNUMber(string RMA)
            {

                string val = RMA;

            }

            [DirectMethod]
            public void TodaysData()
            {
                Store store = this.GridPanel1.GetStore();

                var updatedBy = from up in Obj.Rcall.Todaysall()
                                select new
                                {
                                    up.RGAROWID,
                                    up.RMANumber,
                                    up.PONumber,
                                    up.OrderNumber,
                                    up.ShipmentNumber,
                                    up.ReturnDate,
                                    up.CustomerName1,
                                    up.VendoeName,

                                    up.UpdatedDate,

                                    up.ProgressFlag,
                                    up.RMAStatus,
                                    up.Decision,

                                    UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
                                };




                this.Store1.DataSource = updatedBy.ToList();//Obj.Rcall.DataforToday(ShippingController_V1._0_.Views.Global.lsReturn1).ToList();
                this.Store1.DataBind();

                lbltransaction.Text = "This Grid Shows Todays Received Transactions.";

                Session["transaction"] = "todays";
                
                //btntodays

            }

            [DirectMethod]
            public void PendingDecision()
            {
                //int count = Obj.Rcall.DataForPendingDecision(ShippingController_V1._0_.Views.Global.lsReturn1).Count;

                Store store = this.GridPanel1.GetStore();


                var updatedBy = from up in Obj.Rcall.PendingDecision()
                                select new
                                {
                                    up.RGAROWID,
                                    up.RMANumber,
                                    up.PONumber,
                                    up.OrderNumber,
                                    up.ShipmentNumber,
                                    up.ReturnDate,
                                    up.CustomerName1,
                                    up.VendoeName,

                                    up.UpdatedDate,

                                    up.ProgressFlag,
                                    up.RMAStatus,
                                    up.Decision,

                                    UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
                                };






                this.Store1.DataSource = updatedBy.ToList();//Obj.Rcall.DataForPendingDecision(ShippingController_V1._0_.Views.Global.lsReturn1).ToList();
                this.Store1.DataBind();

                lbltransaction.Text = "This Grid Shows Pending Decision Transactions";

                flag = false;

                Session["transaction"] = "pending";

            }
            


            [DirectMethod]
            public void SearchByDates()
            {
                DateTime from = DateField1.SelectedDate;
                DateTime to = DateField2.SelectedDate;

                Store store = this.GridPanel1.GetStore();

                List<Return> lsreturnDateBetween = new List<Return>();
                lsreturnDateBetween = Obj.Rcall.DataforBetweenDates((List<Return>)Session["lsReturn1"], from, to);

                var updatedBy = from up in lsreturnDateBetween
                                select new
                                {
                                    up.RGAROWID,
                                    up.RMANumber,
                                    up.PONumber,
                                    up.OrderNumber,
                                    up.ShipmentNumber,
                                    up.ReturnDate,
                                    up.CustomerName1,
                                    up.VendoeName,

                                    up.UpdatedDate,

                                    up.ProgressFlag,
                                    up.RMAStatus,
                                    up.Decision,

                                    UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
                                };






                this.Store1.DataSource = updatedBy.ToList();//Obj.Rcall.DataforBetweenDates(ShippingController_V1._0_.Views.Global.lsReturn1, from, to).ToList();
                this.Store1.DataBind();
            }

            [DirectMethod]
            public void CheckAll()
            {
                //if (chkAll.Checked == true)
                //{
                // List<Return> lsReturn1 = new List<Return>();
                // lsReturn1 = Obj.Rcall.ReturnAll().OrderByDescending(i => i.UpdatedDate).ToList();

                Store store = this.GridPanel1.GetStore();

                var updatedBy = from up in (List<Return>)Session["lsReturn1"]
                                select new
                                {
                                    up.RGAROWID,
                                    up.RMANumber,
                                    up.PONumber,
                                    up.OrderNumber,
                                    up.ShipmentNumber,
                                    up.ReturnDate,
                                    up.CustomerName1,
                                    up.VendoeName,

                                    up.UpdatedDate,

                                    up.ProgressFlag,
                                    up.RMAStatus,
                                    up.Decision,

                                    UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
                                };


                this.Store1.DataSource = updatedBy.ToList();//(List<Return>)Session["lsReturn1"];//.ToList();//(List<Return>)Session["lsReturn1"];//ShippingController_V1._0_.Views.Global.lsReturn1.ToList();
                //ShippingController_V1._0_.Views.Global.lsReturn1.ToList();
                this.Store1.DataBind();

                lbltransaction.Text = "This Grid Shows All Transactions";
                flag = false;
                Session["transaction"] = "viewall";

            }
            //else
            //{
            //    Store store = this.GridPanel1.GetStore();

            //var updatedBy = from up in Obj.Rcall.DataforToday(ShippingController_V1._0_.Views.Global.lsReturn1)
            //                select new
            //                {
            //                    up.RGAROWID,
            //                    up.RMANumber,
            //                    up.PONumber,
            //                    up.OrderNumber,
            //                    up.ShipmentNumber,
            //                    up.ReturnDate,
            //                    up.CustomerName1,
            //                    up.VendoeName,

            //                    up.ProgressFlag,
            //                    up.RMAStatus,
            //                    up.Decision,

            //                    UpdatedBy = up.UpdatedBy == null ? "" : Obj.Rcall.GetUserInfobyUserID((Guid)up.UpdatedBy).UserFullName,
            //                };




            //    this.Store1.DataSource = Obj.Rcall.DataforToday(ShippingController_V1._0_.Views.Global.lsReturn1).ToList();
            //    this.Store1.DataBind();

            //}
            //}

            [DirectMethod(Namespace = "CompanyX")]
            public void DoSomething()
            {
                System.Threading.Thread.Sleep(6000);
                X.Mask.Hide();
            }

            [DirectMethod(Namespace = "CompanyX")]
            public void DoSomethingfortoday()
            {
                System.Threading.Thread.Sleep(3000);
                X.Mask.Hide();
            }                    

        </script>



    </head>
    <body>
        <form id="Form1">
        <div>
                
                  
                    <asp:LinkButton ID="lkbtnPath1" runat="server"  Text="Return Details" CssClass="link" BackColor="white" BorderColor="blue"></asp:LinkButton>
            <div><a href=""><img src="../../Themes/Images/ad.jpg" /></a></div>
                
            </div>
            <table id="tblStatus" runat="server" hidden="hidden">
                
                <tr>
                    <td>
                        <div style="width: 300px; height: 100px; border: 1px solid #000; background-color: white">
                            <br />
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Label ID="lblStatusBoard" runat="server" Text="Status Board :-" Font-Size="Large"></asp:Label>
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                           <br />
                            <%--  <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit" OnClick="btnEdit_Click" />--%>
                            <br />
                            <%-- <asp:Label ID="lblTodaysTransaction" runat="server" Text="Todays Transaction"></asp:Label> --%>   
                         &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                          <asp:LinkButton ID="lkbtnTodaysTransaction" runat="server" Text='(0)Todays Transactions' />
                            <%-- <asp:LinkButton ID="lkbtnTodaysTransaction" runat="server" Text='<%#Eval("NoofImages") %>' OnClick="lkbtnTodaysTransaction_Click" />--%>
                            <br />
                            <%-- <asp:Label ID="lblPendingTransaction" runat="server" Text="Pending Transaction"></asp:Label>  --%>
                         &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                         <asp:LinkButton ID="lkbtnPendingTransaction" runat="server" Text='(0)Pending Decision Transactions' />
                            <%-- <asp:LinkButton ID="lkbtnPendingTransaction" runat="server" Text='<%#Eval("NoofImages") %>' OnClick="lkbtnPendingTransaction_Click" />--%>
                            <br />
                        </div>
                    </td>
                </tr>
            </table>


            <ext:ResourceManager ID="ResourceManager1" runat="server" />

            <%--<ext:ResourceManager ID="ResourceManager2" runat="server" ContentUpdated="False" LicenseKey="" Locale="en-US" />--%>

            <items>
<ext:FieldSet ID="FieldSet1"
runat="server"
Title="Status Board"
Layout="AnchorLayout"
DefaultAnchor="40%">
<Items>
<ext:FieldContainer ID="FieldContainer1"
runat="server"
FieldLabel="Name"
Layout="HBoxLayout"
CombineErrors="true">
<FieldDefaults HideLabel="true" />
<Items>

<ext:Button runat="server" Text="View All" ID="btnViewAll" Cls="lnkbtn" Icon="ApplicationViewList" >
<Listeners>
<Click Handler="Ext.net.Mask.show({ msg : 'Loading Please wait...' }); CompanyX.DoSomething();     App.direct.CheckAll();" />
</Listeners>
</ext:Button>
    <ext:MenuSeparator />
<ext:Button runat="server" Text="Todays Transaction." ID="btntodays" Cls="lnkbtn">
<Listeners>
<Click Handler="Ext.net.Mask.show({ msg : 'Loading Please wait...' }); CompanyX.DoSomethingfortoday();      App.direct.TodaysData();" />
</Listeners>
</ext:Button>


   

<ext:MenuSeparator />

<ext:Button runat="server" Text="Pending Decision Transaction." ID="btnPending" Cls="lnkbtn">
<Listeners>
<Click Handler="Ext.net.Mask.show({ msg : 'Loading Please wait...' }); CompanyX.DoSomething();      App.direct.PendingDecision();" />
</Listeners>
</ext:Button>


     

</Items>
</ext:FieldContainer>
</Items>
</ext:FieldSet>



                <ext:FieldSet ID="FieldSet2"
runat="server"
Title="Grid Shows"
Layout="AnchorLayout"
DefaultAnchor="40%">
                    <Items>
                        <ext:Label runat="server" ID="lbltransaction" Text="This Grid Shows Todays Received Transactions" ></ext:Label>
                        </Items>


                </ext:FieldSet>



</items>




            <ext:GridPanel
                ID="GridPanel1"
                runat="server"
                Title="Return Details Information (RMA)"
                Width="1365"
                UI="Danger">
                <Store>
                    <ext:Store ID="Store1" runat="server" OnReadData="Page_Load" PageSize="20">
                        <Model>
                            <ext:Model ID="Model1" runat="server" IDProperty="RGAROWID">
                                <Fields>

                                    <ext:ModelField Name="ProgressFlag" Type="String" />
                                    <ext:ModelField Name="RGAROWID" Type="String" />
                                    <ext:ModelField Name="RMANumber" Type="String" />
                                    <ext:ModelField Name="PONumber" Type="String" />
                                    <ext:ModelField Name="RMAStatus" Type="String" />

                                    <ext:ModelField Name="Decision" Type="String" />
                                    <ext:ModelField Name="VendoeName" Type="String" />
                                    <ext:ModelField Name="CustomerName1" Type="String" />
                                    <ext:ModelField Name="ShipmentNumber" Type="String" />

                                    <ext:ModelField Name="OrderNumber" Type="String" />
                                    <ext:ModelField Name="ReturnDate" Type="Date" />
                                    <ext:ModelField Name="UpdatedBy" Type="String" />
                                    <ext:ModelField Name="UpdatedDate" Type="Date" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel1" runat="server" UI="Primary">
                    <Columns>





                        <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                        <ext:CommandColumn ID="CommandColumn1" Width="52" runat="server" Filterable="false">
                            <Commands>

                                <ext:GridCommand Icon="NoteEdit" CommandName="Edit" Text="Edit" />

                            </Commands>
                            <Listeners>
                                <%--      <Command Handler="Ext.Msg.alert(command, record.data.RGAROWID);" />--%>
                                <Command Handler="App.direct.ShowEditPage(record.data.RGAROWID)" />

                            </Listeners>

                        </ext:CommandColumn>
                        <ext:CommandColumn ID="CommandForPrint" Width="52" runat="server">
                            <Commands>
                                <ext:GridCommand Icon="Printer" CommandName="Print" Text="Print" />
                            </Commands>
                            <DirectEvents>
                                <Command OnEvent="Button3_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="command" Value="command" Mode="Raw"></ext:Parameter>
                                        <ext:Parameter Name="RowID" Value="record.data.RGAROWID" Mode="Raw"></ext:Parameter>
                                        <%-- <ext:Parameter Name="rowIndex" Value="rowIndex" Mode="Raw"></ext:Parameter>--%>
                                    </ExtraParams>
                                </Command>

                            </DirectEvents>
                            <%-- <DirectEvents>
                                   <Command OnEvent="Button3_Click"></Command>
                            </DirectEvents>--%>
                            <%-- <Listeners>
                                      <Command Handler="Ext.Msg.alert(command, record.data.RGAROWID);" />
                                <Command Handler="App.direct.PrintFunction(record.data.RGAROWID)" />

                            </Listeners>--%>
                        </ext:CommandColumn>


                        <ext:Column ID="Column1" runat="server" Text="Flag" DataIndex="ProgressFlag" Width="75" Filterable="false">
                            <Renderer Fn="ProgressFlag" />

                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="RGAROWID" Width="82" DataIndex="RGAROWID" />
                        <ext:Column ID="Column3" runat="server" Text="RMANumber" Width="85" DataIndex="RMANumber" />
                        <ext:Column ID="Column4" runat="server" Text="PONumber" Width="85" DataIndex="PONumber" />

                        <ext:Column ID="Column5" runat="server" Text="RMAStatus" DataIndex="RMAStatus" Width="82" Filterable="false">
                            <Renderer Handler="if (value === '0') 
                             { 
                             return 'Incomplete'; 
                             } 
                             else if (value === '1')
                              {
                              return 'Complete';
                              } 
                             else if (value === '2') 
                             {
                             return 'Wrong RMA';
                              }
                              else if (value === '3') 
                             {
                             return 'To Process';
                              }
                             else 
                             {
                              return '';
                             }" />
                        </ext:Column>

                        <ext:Column ID="Column6" runat="server" Text="Decision" Width="85" DataIndex="Decision" Filterable="false">
                            <Renderer Handler="if (value === '0') 
                             { 
                             return 'Pending'; 
                             } 
                             else if (value === '1')
                              {
                              return 'Deny';
                              } 
                             else if (value === '2') 
                             {
                             return 'Full Refund';
                              }
                              else if (value === '3') 
                             {
                             return 'Partial Refund';
                              }
                             else 
                             {
                              return '';
                             }" />
                        </ext:Column>

                        <ext:Column ID="Column7" runat="server" Text="Vendor Name" Width="120" DataIndex="VendoeName" Filterable="false" />
                        <ext:Column ID="Column8" runat="server" Text="Customer Name" Width="110" DataIndex="CustomerName1" Filterable="false" />

                        <ext:Column ID="Column9" runat="server" Text="Shipment Number" DataIndex="ShipmentNumber" Width="94" />
                        <ext:Column ID="Column10" runat="server" Text="Order Number" Width="85" DataIndex="OrderNumber" />
                        <ext:Column ID="Column11" runat="server" Text="Return Date" Width="110" DataIndex="ReturnDate" Filterable="false" />
                        <ext:Column ID="Column12" runat="server" Text="Updated By" Width="110" DataIndex="UpdatedBy" Filterable="false" />

                        <ext:Column ID="Column13" runat="server" Text="Updated Date" Width="120" DataIndex="UpdatedDate" Filterable="false" />



                        <%--<ext:DateColumn ID="DateColumn1" runat="server" Text="Last Updated" Width="85" DataIndex="lastChange" Format="H:mm:ss" />--%>
                    </Columns>
                </ColumnModel>
                <Plugins>
                    <ext:FilterHeader ID="FilterHeader1" runat="server" />
                </Plugins>
                <SelectionModel>
                    <%-- <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi" />--%>

                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi">
                        <%-- <Listeners>
                            <Select Fn="AddUser"></Select>
                        </Listeners>--%>
                    </ext:CheckboxSelectionModel>






                </SelectionModel>
                <View>
                    <ext:GridView ID="GridView1" runat="server" StripeRows="true" UI="Success" />

                </View>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                        <Items>
                            <%--<ext:Label ID="Label1" runat="server" Text="Page size:" />--%>
                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                            <%--  <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                <Items>
                                    <ext:ListItem Text="1" />
                                    <ext:ListItem Text="2" />
                                    <ext:ListItem Text="10" />
                                    <ext:ListItem Text="20" />
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="10" />
                                </SelectedItems>
                                <Listeners>
                                    <Select Handler="#{GridPanel1}.store.pageSize = parseInt(this.getValue(), 10); #{GridPanel1}.store.reload();" />
                                </Listeners>
                            </ext:ComboBox>--%>
                        </Items>
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>

                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:Button ID="Button3" runat="server" Text="Print" Icon="Printer" OnDirectClick="Button3_Click" Width="1px">
                                <%-- <DirectEvents>
<Click OnEvent="Button1_Click2"></Click>
</DirectEvents>--%>
                            </ext:Button>


                            <%--<%-- <ext:Label Text="RMA Number" ID="lblRMANumber" runat="server"></ext:Label>
                            <ext:ComboBox ID="ComboBox12"
                                runat="server"
                                TriggerAction="All"
                                Mode="Remote"
                                DisplayField="RMANumber"
                                ValueField="RMANumber">
                                <Store>
                                    <ext:Store ID="Store2" runat="server">
                                        <Model>
                                            <ext:Model ID="Model2" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="RMANumber" Type="String" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                      </ext:Store>
                                </Store>
                                <DirectEvents>
                                    <Select OnEvent="LoadCombo2" />
                                </DirectEvents>
                                <Listeners>
                                    <KeyPress Handler="App.direct.applyFilterForSRNUMber(this);" />
                                    <%-- <KeyDown Handler="window.alert(this);" Buffer="250"/>--%>
                            <%--             </Listeners>
                            </ext:ComboBox>--%>

                            <%--<ext:Label Text="PO Number" ID="lblPOnumber" runat="server"></ext:Label>--%>

                            <%--<ext:ComboBox ID="ComboBoxforPOnumber"
                                runat="server"
                                TriggerAction="All"
                                Mode="Remote"
                                DisplayField="PONumber"
                                ValueField="PONumber">
                                <Store>
                                    <ext:Store ID="Store3" runat="server">
                                        <Model>
                                            <ext:Model ID="Model3" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="PONumber" Type="String" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <DirectEvents>
                                    <Select OnEvent="LoadComboForPOnumber" />
                                </DirectEvents>
                                <Listeners>
                                    <Select Handler="applyFilter(this);" Buffer="250" />
                                    <%-- <Select Handler="window.alert('Message');" Buffer="250"/>--%>
                            <%--  </Listeners>
                            </ext:ComboBox>--%>

                            <ext:Button ID="Button1" runat="server" Text="Print Selected" Icon="Printer">
                                <DirectEvents>
                                    <Click OnEvent="Button1_Click2"></Click>
                                </DirectEvents>
                            </ext:Button>

                            <ext:ToolbarSeparator />


                          <%--  <ext:Button ID="Button2" runat="server" Text="Print current grid page" Icon="Printer" Handler="this.up('grid').print({currentPageOnly : true});" />--%>

                            <ext:ToolbarSeparator />

                            <%--<ext:Checkbox runat="server" ID="chkAll" BoxLabel="View All" Handler="Ext.net.Mask.show({ msg : 'Loading Please wait...' }); CompanyX.DoSomething();  App.direct.CheckAll()">
                            </ext:Checkbox>--%>
                            <ext:ToolbarSeparator />
                            <ext:Label runat="server" Text="From Date :"></ext:Label>
                            <ext:DateField ID="DateField1" runat="server">
                                <Listeners>
                                    <Select Handler="#{DisplayField1}.setValue(Ext.util.Format.time(this.getValue()));" />
                                </Listeners>
                            </ext:DateField>
                            <ext:ToolbarSeparator />

                            <ext:Label ID="Label2" runat="server" Text="To Date :"></ext:Label>

                            <ext:DateField ID="DateField2" runat="server">
                                <Listeners>
                                    <Select Handler="#{DisplayField1}.setValue(Ext.util.Format.time(this.getValue()));" />
                                </Listeners>
                            </ext:DateField>


                            <ext:Button ID="btnSearch" runat="server" Text="Search All">
                                <Listeners>
                                    <%--<Click Handler="window.alert('Hi')"/>--%>
                                    <Click Handler="Ext.net.Mask.show({ msg : 'Loading Please wait...' }); CompanyX.DoSomething();    App.direct.SearchByDates();" />
                                </Listeners>

                            </ext:Button>


                        </Items>
                    </ext:Toolbar>
                </TopBar>

            </ext:GridPanel>
            <%-- <ext:Label runat="server" ID="lblshow" ></ext:Label>--%>
        </form>
    </body>
    </html>

    </table>

</asp:Content>
