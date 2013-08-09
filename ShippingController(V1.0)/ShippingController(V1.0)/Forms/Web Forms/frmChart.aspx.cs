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
            ShipmentCountGraph();
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

            #region Graph

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
                      Title = new XAxisTitle { Text = "Shipment Numbers" },
                      Min = 10
                      
                  })
                  .SetLegend(new Legend
                  {
                      VerticalAlign = VerticalAligns.Bottom
                  })

                  .SetYAxis(new YAxis
                  {
                      Title = new YAxisTitle { Text = "Time in (Min.Sec)" }
                  })
                  .SetSeries(new Series
                  {
                      Data = new Data(Times),
                      Name = "Time Taken"
                  });
            
            #endregion
            
            ltrChart.Text = chart.ToHtmlString();
        }


        public void ShipmentCountGraph()
        {
            List<cstUserShipmentCount> _lsShipmetCount = cGlobal.Rcall.GetUserTotalPakedPerDay().OrderByDescending(x => x.Datepacked).ToList();
                      
            List<String> lsDistinctNames = _lsShipmetCount.Select(x => x.UserName).Distinct().ToList();
            List<DateTime> lsDistinctDates = _lsShipmetCount.Select(x => x.Datepacked).Distinct().ToList();

            String[] strCatagories = new string[lsDistinctDates.Count];
            Series[] Seri = new Series[lsDistinctNames.Count()];
         
            int si = 0;
            object[] lobj = new object[lsDistinctDates.Count];
           
            foreach (String Namei in lsDistinctNames)
            {
               
                List<Object> lso = new List<object>();

                foreach (DateTime Dt in lsDistinctDates)
                {
                   
                    cstUserShipmentCount Shipc = new cstUserShipmentCount();
                    Shipc = _lsShipmetCount.SingleOrDefault(i => i.UserName == Namei && i.Datepacked == Dt);
                    if (Shipc == null)
                    {

                        lso.Add(0);
                    }
                    else
                    {
                        lso.Add(Shipc.ShipmentCount);
                    }

                }
                lso.CopyTo(lobj);
               
                Series Seris = new Series { Name = Namei, Data = new Data(lobj.ToArray()) };
                Seri[si] = Seris;
                si++;
            }

            for (int i = 0; i < lsDistinctDates.Count; i++)
            {
                strCatagories[i] = lsDistinctDates[i].ToString("MMM dd, yyyy");

            }

            DotNet.Highcharts.Highcharts Chart = new DotNet.Highcharts.Highcharts("Chart")
            .InitChart(new Chart
            {
                Type = ChartTypes.Line
            })
                .SetTitle(new Title
                {
                    Text = "Shipment Packing Count"
                })
                .SetXAxis(new XAxis
                {
                    Categories = (strCatagories),
                    Title = new XAxisTitle { Text = "Packing Dates" }
                })
                  .SetYAxis(new YAxis
                  {
                      Title = new YAxisTitle { Text = "Shipments packed" }
                  })
                  .SetSeries(Seri);


            ltrTodayspacking.Text = Chart.ToHtmlString();
        }
    }
}