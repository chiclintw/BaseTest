using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BaseTest
{
	public class HomeMenuPage : ContentPage
	{
		public Action<Page> OnMenuSelect {
			get;
			set;
		}

		public HomeMenuPage ()
		{
			Title = "Menu";
			Icon = "menu.png";

			Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 0, 0);

			var dataTemplate = new DataTemplate(typeof(MenuItemCell));

			ListView listView = new ListView () {
				ItemsSource = MenuItem.Data,
				ItemTemplate = dataTemplate
			};

			listView.ItemSelected += (sender, e) =>
			{
				if (OnMenuSelect != null)
				{
					var menuItem = (MenuItem)e.SelectedItem;
					var menuPage = menuItem.MenuPage();
					OnMenuSelect(menuPage);
				}
			};
				
			Content = listView; 
		}
	}
}


