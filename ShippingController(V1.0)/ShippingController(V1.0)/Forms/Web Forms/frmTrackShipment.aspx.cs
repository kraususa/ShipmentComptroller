using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts;
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmTrackShipment : System.Web.UI.Page
    {
      protected void Page_Load(object sender, EventArgs e)
        {
           
          
        }

      #region TImeSHipment Graph
      public void SetGraph(List<cstShipmentNumStatus> _lsGrapgPar)
      {
          Series[] sr = null;
          String name = "";

          foreach (cstShipmentNumStatus item in _lsGrapgPar)
          {
              if (item.ShippingCompletedInt == 40)
              {
                  sr = new Series[4];
                  sr[0] = new Series { Name = "Packing", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                  sr[1] = new Series { Name = "Picking ", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                  sr[2] = new Series { Name = "Allocated", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                  sr[3] = new Series { Name = "New", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
              }
              else if (item.ShippingCompletedInt == 50)
              {
                  sr = new Series[5];
                  sr[0] = new Series { Name = "Shipping", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                  sr[1] = new Series { Name = "Packing", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                  sr[2] = new Series { Name = "Picking ", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                  sr[3] = new Series { Name = "Allocated", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                  sr[4] = new Series { Name = "New", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
              }
              else if (item.ShippingCompletedInt == 60)
              {
                  sr = new Series[6];
                  sr[0] = new Series { Name = "Shipped", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(170, 230, 26) };
                  sr[1] = new Series { Name = "Shipping", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
                  sr[2] = new Series { Name = "Packing", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
                  sr[3] = new Series { Name = "Picking ", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };
                  sr[4] = new Series { Name = "Allocated", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 128, 35) };
                  sr[5] = new Series { Name = "New", Data = new Data(new object[] { 10 }), Color = System.Drawing.Color.FromArgb(233, 81, 35) };
              }

              
              Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart
                {
                    Type = ChartTypes.Bar,
                    BackgroundColor = new BackColorOrGradient(System.Drawing.Color.Transparent)
                })
                .SetTitle(new Title
                {
                    Text = "Shipment Tracking",
                    Style = "fontSize: '30px',fontFamily: 'Verdana', fontBold: 'true', color: 'White' "
                })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { item.ShippingNum },
                    Labels = new XAxisLabels { Style = "fontSize: '25px', fontFamily: 'Verdana', fontBold: 'true', color: 'White'" }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = "States of Shipment", Style = "fontSize: '15px', fontFamily: 'Verdana', color: 'White'" },
                    Labels = new YAxisLabels { Enabled = false },
                    GridLineWidth = 0
                })
                .SetTooltip(new Tooltip { Formatter = "function() { return this.series.name }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        Stacking = Stackings.Normal,
                        Shadow = true,
                        DataLabels = new PlotOptionsBarDataLabels
                        {
                            Enabled = true,
                            Formatter = "function() { return this.series.name; }",
                            Color = System.Drawing.Color.Black,
                            Style = "fontSize: '25px', fontFamily: 'Verdana', fontBold: 'true', color: 'Black'"
                        },
                        Events = new PlotOptionsBarEvents { Click = "function(event) {  location.href ='/Forms/Web Forms/rpt'+ this.name +'.aspx'; }" },
                        PointWidth = 70,
                        
                    }
                })
                .SetSeries(sr);

              ltrChart.Text = chart.ToHtmlString();
          }
      }
      #endregion

      protected void txtShippingNumber_TextChanged(object sender, EventArgs e)
      {
          //GridView1.DataSource = cGlobal.Rcall.GetShippingStatus(txtShippingNumber.Text);
          //GridView1.DataBind();
          List<cstShipmentNumStatus> _lsGrapgPar = cGlobal.Rcall.GetShippingStatus(txtShippingNumber.Text);
          //show Graph

          SetGraph(_lsGrapgPar);
      }

      public void Test()
      {
          Response.Redirect("");
      }
    }
}