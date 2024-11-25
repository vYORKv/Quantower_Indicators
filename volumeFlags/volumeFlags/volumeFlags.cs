using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TradingPlatform.BusinessLayer;

namespace VolumeFlags
{
	public class VolumeFlags : Indicator
    {

        [InputParameter("Weak Flags")]
        public bool weakFlags = true;

        [InputParameter("Medium Flags")]
        public bool mediumFlags = true;

        [InputParameter("Strong Flags")]
        public bool strongFlags = true;

        [InputParameter("Weak Multiplicative")]
        public double weakMultiplicative = 2.0;

        [InputParameter("Medium Multiplicative")]
        public double mediumMultiplicative = 3.0;

        [InputParameter("Strong Multiplicative")]
        public double strongMultiplicative = 4.0;

        [InputParameter("Half Range")]
        public bool halfRange = true;

        [InputParameter("Buy Volume Flag Weak")]
        public Color buyFlagColorWeak = Color.Blue;

        [InputParameter("Sell Volume Flag Weak")]
        public Color sellFlagColorWeak = Color.Red;

        [InputParameter("Buy Volume Flag Medium")]
        public Color buyFlagColorMedium = Color.Green;

        [InputParameter("Sell Volume Arrow Medium")]
        public Color sellFlagColorMedium = Color.Orange;

        [InputParameter("Buy Volume Flag Strong")]
        public Color buyFlagColorStrong = Color.Purple;

        [InputParameter("Sell Volume Arrow Strong")]
        public Color sellFlagColorStrong = Color.Brown;



        public VolumeFlags()
            : base()
        {
            Name = "Volume Flags";
            Description = "My indicator's annotation";

            // Defines line on demand with particular parameters.
            this.AddLineSeries("High Line Weak", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("Low Line Weak", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("High Line Medium", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("Low Line Medium", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("High Line Strong", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("Low Line Strong", Color.FromArgb(0), 1, LineStyle.Points);


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
            SetValue(GetPrice(PriceType.High) + 20, 0); //20 NQ_10 ES
            SetValue(GetPrice(PriceType.Low) - 20, 1);  //20 NQ_10 ES
            SetValue(GetPrice(PriceType.High) + 25, 2); //25 NQ_15 ES
            SetValue(GetPrice(PriceType.Low) - 25, 3);  //25 NQ_15 ES
            SetValue(GetPrice(PriceType.High) + 30, 4); //30 NQ_20 ES
            SetValue(GetPrice(PriceType.Low) - 30, 5);  //30 NQ_20 ES

            double volume = GetPrice(PriceType.Volume);
            double open = GetPrice(PriceType.Open);
            double close = GetPrice(PriceType.Close);

            double volume_1 = Volume(1);
            double volume_2 = Volume(2);
            double volume_3 = Volume(3);
            double volume_4 = Volume(4);
            double volume_5 = Volume(5);
            double volume_6 = Volume(6);
            double volume_7 = Volume(7);
            double volume_8 = Volume(8);
            double volume_9 = Volume(9);
            double volume_10 = Volume(10);

            double volume_sum_full = volume_1 + volume_2 + volume_3 + volume_4 + volume_5 + volume_6 + volume_7 + volume_8 + volume_9 + volume_10;
            double volume_sum_half = volume_1 + volume_2 + volume_3 + volume_4 + volume_5;

            double volume_avg_full = volume_sum_full / 10;
            double volume_avg_half = volume_sum_half / 5;

            if (halfRange == false)
            {
                if (volume > volume_avg_full * weakMultiplicative && weakFlags == true)
                {
                    if (close > open)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(buyFlagColorWeak, bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                    else if (close < open)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(sellFlagColorWeak, upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                }
            }
            else if (halfRange == true)
            {
                if (volume > volume_avg_half * weakMultiplicative && weakFlags == true)
                {
                    if (close > open)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(buyFlagColorWeak, bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                    else if (close < open)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(sellFlagColorWeak, upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                }
                if (volume > volume_avg_half * mediumMultiplicative && mediumFlags == true)
                {
                    if (close > open)
                    {
                        this.LinesSeries[2].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[3].SetMarker(0, new IndicatorLineMarker(buyFlagColorMedium, bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                    else if (close < open)
                    {
                        this.LinesSeries[2].SetMarker(0, new IndicatorLineMarker(sellFlagColorMedium, upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[3].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                }
                if (volume > volume_avg_half * strongMultiplicative && strongFlags == true)
                {
                    if (close > open)
                    {
                        this.LinesSeries[4].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[5].SetMarker(0, new IndicatorLineMarker(buyFlagColorStrong, bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                    else if (close < open)
                    {
                        this.LinesSeries[4].SetMarker(0, new IndicatorLineMarker(sellFlagColorStrong, upperIcon: IndicatorLineMarkerIconType.Flag));
                        this.LinesSeries[5].SetMarker(0, new IndicatorLineMarker(Color.FromArgb(0), bottomIcon: IndicatorLineMarkerIconType.Flag));
                    }
                }
            }


        }
    }
}
