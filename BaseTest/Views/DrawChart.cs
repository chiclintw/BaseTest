using System;

using Xamarin.Forms;
using OxyPlot.Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using OxyPlot.Axes;

namespace BaseTest
{
	public class DrawChart : ContentPage
	{
		public DrawChart ()
		{
			Title = "統計圖表";

			var opv1 = new PlotView {
				WidthRequest = 300, HeightRequest = 300,
			};
			opv1.Model = DemoChart1();

			var opv2 = new PlotView {
				WidthRequest = 300, HeightRequest = 300,
			};
			opv2.Model = DemoChart2();

			var opv3 = new PlotView {
				WidthRequest = 300, HeightRequest = 300,
			};
			opv3.Model = DemoChart3();


			Content = new ScrollView
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
				Content = new StackLayout
				{
					Children =
					{
						opv1,
						opv2,
						opv3
					}
				}
			};
		}

		public PlotModel DemoChart1()
		{
			var Points = new List<DataPoint>
			{
				new DataPoint(0, 4),
				new DataPoint(10, 13),
				new DataPoint(20, 15),
				new DataPoint(30, 16),
				new DataPoint(40, 12),
				new DataPoint(50, 12)
			};

			var model = new PlotModel {Title = "折線圖"};
			model.PlotType = PlotType.XY;

			var s = new LineSeries ();
			s.ItemsSource = Points;
			model.Series.Add (s);

			return model;
		}

		public PlotModel DemoChart2()
		{
			var model = new PlotModel
			{
				Title = "長條圖",
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.BottomCenter,
				LegendOrientation = LegendOrientation.Horizontal,
				LegendBorderThickness = 0
			};

			var s1 = new BarSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			s1.Items.Add(new BarItem { Value = 25 });
			s1.Items.Add(new BarItem { Value = 137 });
			s1.Items.Add(new BarItem { Value = 18 });
			s1.Items.Add(new BarItem { Value = 40 });

			var s2 = new BarSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
			s2.Items.Add(new BarItem { Value = 12 });
			s2.Items.Add(new BarItem { Value = 14 });
			s2.Items.Add(new BarItem { Value = 120 });
			s2.Items.Add(new BarItem { Value = 26 });

			var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
			categoryAxis.Labels.Add("Category A");
			categoryAxis.Labels.Add("Category B");
			categoryAxis.Labels.Add("Category C");
			categoryAxis.Labels.Add("Category D");
			var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
			model.Series.Add(s1);
			model.Series.Add(s2);
			model.Axes.Add(categoryAxis);
			model.Axes.Add(valueAxis);

			return model;
		}

		public PlotModel DemoChart3()
		{
			var model = new PlotModel { Title = "圓餅圖" };

			dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

			seriesP1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
			seriesP1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
			seriesP1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
			seriesP1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
			seriesP1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

			model.Series.Add(seriesP1);

			return model;
		}

	}
}

