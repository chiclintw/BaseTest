using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace BaseTest
{
	public class MenuItem
	{
		static MenuItem ()
		{
			Data = new ObservableCollection<MenuItem>()
			{
				new MenuItem { MenuTitle = "首頁", MenuIcon = "item.png", MenuPage = () => new TabPage() },
				new MenuItem { MenuTitle = "訂購展示", MenuIcon = "item.png", MenuPage = () => new PizzaPage() },
				new MenuItem { MenuTitle = "條碼展示", MenuIcon = "item.png", MenuPage = () => new CustomerPage() },
				new MenuItem { MenuTitle = "照片展示", MenuIcon = "item.png", MenuPage = () => new TakePhoto() },
				new MenuItem { MenuTitle = "Grid展示", MenuIcon = "item.png", MenuPage = () => new DevExpressGrid2() },
				new MenuItem { MenuTitle = "Map展示", MenuIcon = "item.png", MenuPage = () => new GeoMapSimple() },
				new MenuItem { MenuTitle = "Chart展示", MenuIcon = "item.png", MenuPage = () => new DrawChart() },
				new MenuItem { MenuTitle = "關於", MenuIcon = "item.png", MenuPage = () => new AboutPage() },
			};
		}
		public string MenuTitle { get; set; }
		public FileImageSource MenuIcon { get; set; }
		public Func<Page> MenuPage { get; set; }
		public static IList<MenuItem> Data { set; get; }
	}
}

