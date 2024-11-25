using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TradingPlatform.BusinessLayer;

namespace smaFlatline
{
	public class smaFlatline : Indicator
    {

        [InputParameter("Flip Circle")]
        public bool flipCircle = false;

        [InputParameter("Circle Color")]
        public Color circleColor = Color.Purple;

        [InputParameter("Circle Color")]
        public Color topColor = Color.CadetBlue;

        [InputParameter("Circle Color")]
        public Color bottomColor = Color.Orange;



        public SMAFlatline()
            : base()
        {
            Name = "SMA Flatline";
            Description = "SMA 10/20 Flatline Signal";

            // Defines line on demand with particular parameters.
            this.AddLineSeries("Signal Line", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("Signal Line", Color.FromArgb(0), 1, LineStyle.Points);

            // By default indicator will be applied on main window of the chart
            SeparateWindow = false;
        }

        protected override void OnInit()
        {
            // Any initialization code goes here
        }

        /// <summary>
        /// Calculation entry point. This function is called when a price data updates. 
        /// Will be runing under the HistoricalBar mode during history loading. 
        /// Under NewTick during realtime. 
        /// Under NewBar if start of the new bar is required.
        /// </summary>
        /// <param name="args">Provides data of updating reason and incoming price.</param>
        protected override void OnUpdate(UpdateArgs args)
        {

            SetValue(GetPrice(PriceType.Low) - 6, 1);

            if (flipCircle == true)
            {
                SetValue(GetPrice(PriceType.High) + 12, 0);
                SetValue(GetPrice(PriceType.Low) - 12, 1);
            }

            double sum10 = 0.0;
            double sum20 = 0.0;

            double sum10_1 = 0.0;
            double sum20_1 = 0.0;

            for (int i = 0; i < 10; i++)
            {
                sum10 += this.GetPrice(PriceType.Close, i);
            }
            for (int i = 0; i < 20; i++)
            {
                sum20 += this.GetPrice(PriceType.Close, i);
            }

            for (int i = 1; i < 11; i++)
            {
                sum10_1 += this.GetPrice(PriceType.Close, i);
            }
            for (int i = 1; i < 21; i++)
            {
                sum20_1 += this.GetPrice(PriceType.Close, i);
            }

            double sma10 = sum10 / 10;
            double sma20 = sum20 / 20;

            double sma10_1 = sum10_1 / 10;
            double sma20_1 = sum20_1 / 20;


            if (flipCircle == true)
            {
                if (sma10 == sma20)
                {
                    if (sma10_1 > sma20_1)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(topColor, upperIcon: IndicatorLineMarkerIconType.FillCircle));
                    }
                    else if (sma20_1 > sma10_1)
                    {
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(bottomColor, bottomIcon: IndicatorLineMarkerIconType.FillCircle));
                    }
                }
            }
            else if (flipCircle == false)
            {
                if (sma10 == sma20)
                {
                    this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(circleColor, bottomIcon: IndicatorLineMarkerIconType.FillCircle));
                }
            }
        }
    }
}
