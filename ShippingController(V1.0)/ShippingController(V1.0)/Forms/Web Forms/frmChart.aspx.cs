using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using PackingClassLibrary.CustomEntity.ReportEntitys;
using ShippingController_V1._0_.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetGraph();
        }
        public void SetGraph()
        {
            List<cstPackingTime> lsShipInfo = cGlobal.call.GetPackingTimeQuantity();
            String[] Sarray = new string[lsShipInfo.Count];
            object[] Times = new object[lsShipInfo.Count];
            for (int i = 0; i <= lsShipInfo.Count - 1; i++)
            {
                Sarray[i] = lsShipInfo[i].ShippingNumber.ToString();
                String s = lsShipInfo[i].TimeSpend.ToString();
                String[] t = s.Split(new char[] { ':', 'H', 'S', 'M' });
                String timess = t[2].Trim() + "." + t[4].Trim();
                Decimal D = Convert.ToDecimal(timess);
                Times[i] = Math.Round(D, 2);
            }


            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            .InitChart(new Chart
            {
                Type = ChartTypes.Line
            })
                .SetTitle(new Title
                {
                    Text = "Shipment packing Time"
                })
                .SetXAxis(new XAxis
                  {
                      Categories = Sarray,
                      Title = new XAxisTitle { Text = "Shipment Numbers" }
                  })
                  .SetYAxis(new YAxis
                  {
                      Title = new YAxisTitle { Text = "Time in (Min.Sec)" }
                  })
                  .SetSeries(new Series
                  {
                      Data = new Data(Times),
                      Name = "Time Taken"
                  })

                  ;

            ltrChart.Text = chart.ToHtmlString();
        }

    }
}