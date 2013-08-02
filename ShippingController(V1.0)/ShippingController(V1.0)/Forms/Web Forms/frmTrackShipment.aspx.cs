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
namespace ShippingController_V1._0_.Forms.Web_Forms
{
    public partial class frmTrackShipment : System.Web.UI.Page
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
         for (int i = 0; i <= lsShipInfo.Count-1; i++)
			{
			 Sarray[i] = lsShipInfo[i].ShippingNumber.ToString();
             String s = lsShipInfo[i].TimeSpend.ToString();
             String[]t = s.Split(new char[]{':','H','S','M'});
            String timess = t[2].Trim() + "." + t[4].Trim();
            Decimal D = Convert.ToDecimal(timess);
            Times[i] =Math.Round( D,2);
             }
        

          DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
              .SetXAxis(new XAxis
                {
                    Categories = Sarray
                    //new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new Series
                {
                    Data = new Data(Times)
                    //new Data( Times)
                    //new object[] { "0.5", "5.5", "6.5", "7.1", "1.2", "2.1", "10.1", "11.0", "12.11", "0.1", "1.0", "5.1" }
                });

          ltrChart.Text = chart.ToHtmlString();
      }

        
    }
}