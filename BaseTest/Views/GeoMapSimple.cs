using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Geolocator.Plugin;

namespace BaseTest
{
	public class GeoMapSimple : ContentPage
	{
		Map map;
		Geocoder geoCoder;

		public GeoMapSimple ()
		{
			Title = "Map展示";
			geoCoder = new Geocoder ();

			map = new Map { 
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// CHI座標資料
			var chiPosition = new Position (25.076188,121.5710304);
			map.MoveToRegion (MapSpan.FromCenterAndRadius (chiPosition, Distance.FromKilometers (1)));
			var chiAddress = new Pin {
				Type = PinType.Place,
				Position = chiPosition,
				Address = "台北市內湖區瑞光路316巷56號5樓",
				Label = "正航資訊"
			};
			map.Pins.Add(chiAddress);

			var entAddress = new Entry { Placeholder = "輸入地址", HorizontalOptions = LayoutOptions.FillAndExpand };
			var btnLocate = new Button { Text = "定位", HorizontalOptions = LayoutOptions.End };
			var btnCurrent = new Button { Text = "目前", HorizontalOptions = LayoutOptions.End };

			btnLocate.Clicked += async (sender, e) => 
			{
				try
				{
					btnLocate.IsEnabled = false;
					if(entAddress.Text!="")
					{
						var locPosition = new Position (25.076188,121.5710304);
						// 依據地址取得經緯度
						var locAddress = await geoCoder.GetPositionsForAddressAsync (entAddress.Text);
						foreach (var p in locAddress) {
							locPosition = new Position (p.Latitude,p.Longitude);
						}

						var pinAddress = new Pin {
							Type = PinType.Place,
							Position = locPosition,
							Address = entAddress.Text,
							Label = "定位"
						};
						map.Pins.Add(pinAddress);

						map.MoveToRegion (MapSpan.FromCenterAndRadius (locPosition, Distance.FromKilometers (1)));
					}
				}
				catch(Exception ex)
				{
				}
				finally
				{
					btnLocate.IsEnabled = true;
				}			
			};

			btnCurrent.Clicked += async (sender, e) => 
			{
				try
				{
					btnCurrent.IsEnabled = false;
					var sAddress = "";
					// 取得目前位置經緯度
					var pos = await CrossGeolocator.Current.GetPositionAsync(1000);
					var curPosition = new Position (pos.Latitude,pos.Longitude);

					var curAddress = await geoCoder.GetAddressesForPositionAsync (curPosition);
					foreach (var p in curAddress) {
						sAddress = p;
					}
					sAddress = sAddress.Replace("\n","");

					var pinAddress = new Pin {
						Type = PinType.Place,
						Position = curPosition,
						Address = sAddress,
						Label = "目前"
					};
					map.Pins.Add(pinAddress);

					map.MoveToRegion (MapSpan.FromCenterAndRadius (curPosition, Distance.FromKilometers (1)));
					// map.MoveToRegion (new MapSpan (curPosition, 100, 100) );
				}
				catch(Exception ex)
				{
				}
				finally
				{
					btnCurrent.IsEnabled = true;
				}			
			};

			var stkAddress = new StackLayout { Spacing = 10, Padding = 10, 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal, 
				Children = {entAddress, btnLocate, btnCurrent}
			};

			Content = new StackLayout { 
				Children = {
					stkAddress,
					map
				}
			};
		}
	}
}


