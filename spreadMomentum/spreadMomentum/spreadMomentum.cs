using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TradingPlatform.BusinessLayer;

namespace spreadMomentum
{
	public class spreadMomentum : Indicator
    {

        [InputParameter("Weak Signal")]
        public bool weak = true;

        [InputParameter("Medium Signal")]
        public bool medium = true;

        [InputParameter("Strong Signal")]
        public bool strong = true;

        [InputParameter("Multiplicative Weak")]
        public double multiplicativeWeak = 2.0; // Was 4.0

        [InputParameter("Multiplicative Medium")]
        public double multiplicativeMedium = 6.0;

        [InputParameter("Multiplicative Strong")]
        public double multiplicativeStrong = 4.0;

        [InputParameter("Lookback Period Weak (2-6)")]
        public int periodWeak = 3;

        [InputParameter("Lookback Period Medium (2-6)")]
        public int periodMedium = 6;

        [InputParameter("Buy Arrow Color (Weak)")]
        public Color buyArrowColorWeak = Color.Cyan;

        [InputParameter("Sell Arrow Color (Weak")]
        public Color sellArrowColorWeak = Color.Orange;

        [InputParameter("Buy Arrow Color (Medium)")]
        public Color buyArrowColorMedium = Color.Violet;

        [InputParameter("Sell Arrow Color (Medium)")]
        public Color sellArrowColorMedium = Color.Brown;

        [InputParameter("Buy Arrow Color (Strong)")]
        public Color buyArrowColorStrong = Color.Green;

        [InputParameter("Sell Arrow Color (Strong)")]
        public Color sellArrowColorStrong = Color.Red;

        [InputParameter("Inversion Arrow Color")]
        public Color inversionArrowColor = Color.Black;


        public spreadMomentum()
            : base()
        {
            this.Name = "Spread Momentum";
            this.Description = "SMA Spread Momentum Signals";

            // Defines line on demand with particular parameters.
            this.AddLineSeries("HighLine Weak", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("LowLine Weak", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("HighLine Medium", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("LowLine Medium", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("HighLine Strong", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("LowLine Strong", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("HighLine Delta", Color.FromArgb(0), 1, LineStyle.Points);
            this.AddLineSeries("LowLine Delta", Color.FromArgb(0), 1, LineStyle.Points);


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
        /// 
        protected override void OnUpdate(UpdateArgs args)
        {
            double open = GetPrice(PriceType.Open);
            double close = GetPrice(PriceType.Close);
            double low = GetPrice(PriceType.Low);
            double high = GetPrice(PriceType.High);

            SetValue(GetPrice(PriceType.High), 0);
            SetValue(GetPrice(PriceType.Low), 1);
            SetValue(GetPrice(PriceType.High) + 6, 2);
            SetValue(GetPrice(PriceType.Low) - 6, 3);
            SetValue(GetPrice(PriceType.High) + 12, 4);
            SetValue(GetPrice(PriceType.Low) - 12, 5);
            SetValue(GetPrice(PriceType.High) + 28, 6);
            SetValue(GetPrice(PriceType.Low) - 28, 7);

            double sum10_0 = 0.0; // Sum of last 10 prices
            double sum20_0 = 0.0; // Sum of last 20 prices
            for (int i = 0; i < 10; i++)
            {
                // Adding bar's price to the sum
                sum10_0 += this.GetPrice(PriceType.Close, i);
            }
            for (int i = 0; i < 20; i++)
            {
                // Adding bar's price to the sum
                sum20_0 += this.GetPrice(PriceType.Close, i);
            }

            double close1 = this.Close(1);
            double close2 = this.Close(2);
            double close3 = this.Close(3);
            double close4 = this.Close(4);
            double close5 = this.Close(5);
            double close6 = this.Close(6);
            double close7 = this.Close(7);
            double close8 = this.Close(8);
            double close9 = this.Close(9);
            double close10 = this.Close(10);
            double close11 = this.Close(11);
            double close12 = this.Close(12);
            double close13 = this.Close(13);
            double close14 = this.Close(14);
            double close15 = this.Close(15);
            double close16 = this.Close(16);
            double close17 = this.Close(17);
            double close18 = this.Close(18);
            double close19 = this.Close(19);
            double close20 = this.Close(20);
            double close21 = this.Close(21);
            double close22 = this.Close(22);
            double close23 = this.Close(23);
            double close24 = this.Close(24);
            double close25 = this.Close(25);
            double close26 = this.Close(26);
            double close27 = this.Close(27);
            double close28 = this.Close(28);
            double close29 = this.Close(29);
            double close30 = this.Close(30);

            double sum10_1 = close1 + close2 + close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10;
            double sum10_2 = close2 + close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11;
            double sum10_3 = close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12;
            double sum10_4 = close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13;
            double sum10_5 = close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14;
            double sum10_6 = close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15;
            double sum10_7 = close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16;
            double sum10_8 = close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17;
            double sum10_9 = close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18;
            double sum10_10 = close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19;
            double sum20_1 = close1 + close2 + close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20;
            double sum20_2 = close2 + close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21;
            double sum20_3 = close3 + close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22;
            double sum20_4 = close4 + close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23;
            double sum20_5 = close5 + close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24;
            double sum20_6 = close6 + close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24 + close25;
            double sum20_7 = close7 + close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24 + close25 + close26;
            double sum20_8 = close8 + close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24 + close25 + close26 + close27;
            double sum20_9 = close9 + close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24 + close25 + close26 + close27 + close28;
            double sum20_10 = close10 + close11 + close12 + close13 + close14 + close15 + close16 + close17 + close18 + close19 + close20 + close21 + close22 + close23 + close24 + close25 + close26 + close27 + close28 + close29;

            double sma10_1 = sum10_1 / 10;
            double sma10_2 = sum10_2 / 10;
            double sma10_3 = sum10_3 / 10;
            double sma10_4 = sum10_4 / 10;
            double sma10_5 = sum10_5 / 10;
            double sma10_6 = sum10_6 / 10;
            double sma10_7 = sum10_7 / 10;
            double sma10_8 = sum10_8 / 10;
            double sma10_9 = sum10_9 / 10;
            double sma10_10 = sum10_10 / 10;
            double sma20_1 = sum20_1 / 20;
            double sma20_2 = sum20_2 / 20;
            double sma20_3 = sum20_3 / 20;
            double sma20_4 = sum20_4 / 20;
            double sma20_5 = sum20_5 / 20;
            double sma20_6 = sum20_6 / 20;
            double sma20_7 = sum20_7 / 20;
            double sma20_8 = sum20_8 / 20;
            double sma20_9 = sum20_9 / 20;
            double sma20_10 = sum20_10 / 20;

            double diff_1 = 0.0;
            double diff_2 = 0.0;
            double diff_3 = 0.0;
            double diff_4 = 0.0;
            double diff_5 = 0.0;
            double diff_6 = 0.0;
            double diff_7 = 0.0;
            double diff_8 = 0.0;
            double diff_9 = 0.0;
            double diff_10 = 0.0;


            diff_1 = Math.Abs(sma10_1 - sma20_1);
            diff_2 = Math.Abs(sma10_2 - sma20_2);
            diff_3 = Math.Abs(sma10_3 - sma20_3);
            diff_4 = Math.Abs(sma10_4 - sma20_4);
            diff_5 = Math.Abs(sma10_5 - sma20_5);
            diff_6 = Math.Abs(sma10_6 - sma20_6);
            diff_7 = Math.Abs(sma10_7 - sma20_7);
            diff_8 = Math.Abs(sma10_8 - sma20_8);
            diff_9 = Math.Abs(sma10_9 - sma20_9);
            diff_10 = Math.Abs(sma10_10 - sma20_10);

            double offset_diff_2 = diff_2 + diff_3 + diff_4 + diff_5 + diff_6;
            double offset_avg_2 = offset_diff_2 / 5;

            double offset_diff_3 = diff_3 + diff_4 + diff_5 + diff_6 + diff_7;
            double offset_avg_3 = offset_diff_3 / 5;

            double offset_diff_4 = diff_4 + diff_5 + diff_6 + diff_7 + diff_8;
            double offset_avg_4 = offset_diff_4 / 5;

            double offset_diff_5 = diff_5 + diff_6 + diff_7 + diff_8 + diff_9;
            double offset_avg_5 = offset_diff_5 / 5;

            double offset_diff_6 = diff_6 + diff_7 + diff_8 + diff_9 + diff_10;
            double offset_avg_6 = offset_diff_6 / 5;

            double diff_total_10 = diff_1 + diff_2 + diff_3 + diff_4 + diff_5 + diff_6 + diff_7 + diff_8 + diff_9 + diff_10;
            double diff_avg_10 = diff_total_10 / 10;


            double[] lookbackPeriods = [offset_avg_2, offset_avg_2, offset_avg_2, offset_avg_3, offset_avg_4, offset_avg_5, offset_avg_6, offset_avg_6, offset_avg_6, offset_avg_6, offset_avg_6];

            double sma10 = sum10_0 / 10;
            double sma20 = sum20_0 / 20;
            double diff_0 = 0.0;

            diff_0 = Math.Abs(sma10 - sma20);

            if (strong == true)
            {
                if (diff_0 > diff_avg_10 * multiplicativeStrong)
                {
                    if (sma10 > sma20)
                    {
                        this.LinesSeries[5].SetMarker(0, new IndicatorLineMarker(buyArrowColorStrong, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        if (low < sma10 | close < open)
                        {
                            this.LinesSeries[5].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        }
                    }
                    else if (sma20 > sma10)
                    {
                        this.LinesSeries[4].SetMarker(0, new IndicatorLineMarker(sellArrowColorStrong, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        if (high > sma10 | close > open)
                        {
                            this.LinesSeries[4].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        }
                    }
                }
            }
            if (medium == true)
            {
                if (diff_0 > lookbackPeriods[periodMedium] * multiplicativeMedium)
                {
                    if (sma10 > sma20)
                    {
                        this.LinesSeries[3].SetMarker(0, new IndicatorLineMarker(buyArrowColorMedium, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        if (low < sma10 | close < open)
                        {
                            this.LinesSeries[3].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        }
                    }
                    else if (sma20 > sma10)
                    {
                        this.LinesSeries[2].SetMarker(0, new IndicatorLineMarker(sellArrowColorMedium, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        if (high > sma10 | close > open)
                        {
                            this.LinesSeries[2].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        }
                    }
                }

            }
            if (weak == true)
            {
                if (diff_0 > lookbackPeriods[periodWeak] * multiplicativeWeak)
                {
                    if (sma10 > sma20)
                    {
                        this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(buyArrowColorWeak, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        if (low < sma10 | close < open)
                        {
                            this.LinesSeries[1].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, bottomIcon: IndicatorLineMarkerIconType.UpArrow));
                        }
                    }
                    else if (sma20 > sma10)
                    {
                        this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(sellArrowColorWeak, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        if (high > sma10 | close > open)
                        {
                            this.LinesSeries[0].SetMarker(0, new IndicatorLineMarker(inversionArrowColor, upperIcon: IndicatorLineMarkerIconType.DownArrow));
                        }

                    }
                }

            }
        }
    }
}
