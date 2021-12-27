using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Geolocator.Plugin;

namespace BaseTest
{
	public class GeoMap : ContentPage
	{
		Map map;
		Geocoder geoCoder;

		public GeoMap ()
		{
			Title = "Map展示";
			geoCoder = new Geocoder ();

			map = new Map { 
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			map.MoveToRegion (new MapSpan (new Position (0,0), 360, 360) );

			// add the slider
			var slider = new Slider (1, 18, 1);
			slider.ValueChanged += (sender, e) => {
				var zoomLevel = e.NewValue; // between 1 and 18
				var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
				Debug.WriteLine(zoomLevel + " -> " + latlongdegrees);
				if (map.VisibleRegion != null)
					map.MoveToRegion(new MapSpan (map.VisibleRegion.Center, latlongdegrees, latlongdegrees)); 
			};

			var entAddress = new Entry { Placeholder = "輸入地址", HorizontalOptions = LayoutOptions.FillAndExpand };
			var btnAddress = new Button { Text = "定位", HorizontalOptions = LayoutOptions.End };

			btnAddress.Clicked += async (sender, e) => {
				var  posAddress = new Position (0,0);
				var txtAddress = entAddress.Text;
				var locAddress = await geoCoder.GetPositionsForAddressAsync (entAddress.Text);
				foreach (var p in locAddress) {
					posAddress = new Position (p.Latitude,p.Longitude);
				}
				var pinAddress = new Pin {
					Type = PinType.Place,
					Position = posAddress,
					Address = entAddress.Text,
					Label = "定位"
				};
				map.Pins.Add(pinAddress);

				map.MoveToRegion (new MapSpan (posAddress, 360, 360) );
				slider.Value = 1;

			};

			var stkAddress = new StackLayout { Spacing = 10, Padding = 10, 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal, 
				Children = {entAddress, btnAddress}
			};


			// create map style buttons
			var street = new Button { Text = "Street" };
			var hybrid = new Button { Text = "Hybrid" };
			var satellite = new Button { Text = "Satellite" };
			var currentPos = new Button { Text = "Current" };
			street.Clicked += HandleClicked;
			hybrid.Clicked += HandleClicked;
			satellite.Clicked += HandleClicked;

			currentPos.Clicked += async (sender, e) => 
			{
				try
				{
					currentPos.IsEnabled = false;
					var strAddress = "";
					var test = await CrossGeolocator.Current.GetPositionAsync(1000);
					var position = new Position (test.Latitude,test.Longitude);

					var curAddress = await geoCoder.GetAddressesForPositionAsync (position);
					foreach (var p in curAddress) {
						strAddress = p;
					}

					strAddress = strAddress.Replace("\n","");

					var pin = new Pin {
						Type = PinType.Place,
						Position = position,
						Address = strAddress,
						Label = "我的位置"
					};
					map.Pins.Add(pin);

					map.MoveToRegion (new MapSpan (position, 360, 360) );
					slider.Value = 1;
				}
				catch(Exception ex)
				{
				}
				finally
				{
					currentPos.IsEnabled = true;
				}
			};

			var segments = new StackLayout { Spacing = 30,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Orientation = StackOrientation.Horizontal, 
				Children = {street, hybrid, satellite,currentPos}
			};

			// put the page together
			var stack = new StackLayout { Spacing = 0 };
			stack.Children.Add(stkAddress);
			stack.Children.Add(map);
			stack.Children.Add (slider);
			stack.Children.Add (segments);
			Content = stack;

			// for debugging output only
			map.PropertyChanged += (sender, e) => {
				Debug.WriteLine(e.PropertyName + " just changed!");
				if (e.PropertyName == "VisibleRegion" && map.VisibleRegion != null)
					CalculateBoundingCoordinates (map.VisibleRegion);
			};
		}

		void HandleClicked (object sender, EventArgs e)
		{
			var b = sender as Button;
			switch (b.Text) {
			case "Street":
				map.MapType = MapType.Street;
				break;
			case "Hybrid":
				map.MapType = MapType.Hybrid;
				break;
			case "Satellite":
				map.MapType = MapType.Satellite;
				break;
			}
		}

		/// <summary>
		/// In response to this forum question http://forums.xamarin.com/discussion/22493/maps-visibleregion-bounds
		/// Useful if you need to send the bounds to a web service or otherwise calculate what
		/// pins might need to be drawn inside the currently visible viewport.
		/// </summary>
		static void CalculateBoundingCoordinates (MapSpan region)
		{
			// WARNING: I haven't tested the correctness of this exhaustively!
			var center = region.Center;
			var halfheightDegrees = region.LatitudeDegrees / 2;
			var halfwidthDegrees = region.LongitudeDegrees / 2;

			var left = center.Longitude - halfwidthDegrees;
			var right = center.Longitude + halfwidthDegrees;
			var top = center.Latitude + halfheightDegrees;
			var bottom = center.Latitude - halfheightDegrees;

			// Adjust for Internation Date Line (+/- 180 degrees longitude)
			if (left < -180) left = 180 + (180 + left);
			if (right > 180) right = (right - 180) - 180;
			// I don't wrap around north or south; I don't think the map control allows this anyway

			Debug.WriteLine ("Bounding box:");
			Debug.WriteLine ("                    " + top);
			Debug.WriteLine ("  " + left + "                " + right);
			Debug.WriteLine ("                    " + bottom);
		}
	}
}


