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
                  });
            #endregion

            ltrChart.Text = chart.ToHtmlString();
        }


        public void ShipmentCountGraph()
        {
            List<cstUserShipmentCount> _lsShipmetCount = cGlobal.Rcall.GetUserTotalPakedPerDay();
                      
            List<String> lsDistinctNames = _lsShipmetCount.Select(x => x.UserName).Distinct().ToList();
            List<DateTime> lsDistinctDates = _lsShipmetCount.Select(x => x.Datepacked).Distinct().ToList();

            String[] strCatagories = new string[lsDistinctDates.Count];
            Series[] Seri = new Series[lsDistinctNames.Count()];
         
            int si = 0;


            foreach (String Namei in lsDistinctNames)
            {
                object[] lobj = new object[lsDistinctDates.Count];
                List<int> lso = new List<int>();

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

                for (int i = 0; i < lsDistinctDates.Count; i++)
                {
                    lobj[i] = lso.ToArray();
                }
                Series Seris = new Series { Name = Namei, Data = new Data(lobj.ToArray()) };
                Seri[si] = Seris;
                si++;
            }

            for (int i = 0; i < lsDistinctDates.Count; i++)
            {
                strCatagories[i] = lsDistinctDates[i].ToString("MMM dd, yyyy");

            }


            Series[] s = new Series[3];
            s[0] = new Series { Name = "Shipping", Data = new Data(new object[] { 1,2,3,4,5,4 }), Color = System.Drawing.Color.FromArgb(193, 230, 26) };
            s[1] = new Series { Name = "Packing", Data = new Data(new object[] { 1,3,1,0,5,3 }), Color = System.Drawing.Color.FromArgb(222, 230, 26) };
            s[2] = new Series { Name = "Picking ", Data = new Data(new object[] { 1,2,6,3,1,1 }), Color = System.Drawing.Color.FromArgb(233, 190, 35) };


            DotNet.Highcharts.Highcharts Chart = new DotNet.Highcharts.Highcharts("Chart")
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
                    Categories = (strCatagories),
                    Title = new XAxisTitle { Text = "Shipment Numbers" }
                })
                  .SetYAxis(new YAxis
                  {
                      Title = new YAxisTitle { Text = "Time in (Min.Sec)" }
                  })
                  .SetSeries(Seri);


            ltrTodayspacking.Text = Chart.ToHtmlString();
        }
    }
}