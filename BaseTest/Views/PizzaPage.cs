using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace BaseTest
{
	public class PizzaPage : ContentPage
	{
		IList<Pizza> xpizza = Pizza.Data;
		ListView listView;

		public PizzaPage ()
		{
			Title = "比薩";

			Padding = new Thickness (0, 10);

			var imageTemplate = new DataTemplate (typeof(PizzaItemCell));

			listView = new ListView () 
			{
				ItemsSource = xpizza,
				ItemTemplate = imageTemplate,
				IsPullToRefreshEnabled = true
			};

			listView.ItemSelected += async (sender, e) =>
			{
				if(listView.SelectedItem == null) return;

				var pizzaItem = (Pizza)e.SelectedItem;

				// De-select the item.
				listView.SelectedItem = null;

				PizzaDetail detailPage = new PizzaDetail(pizzaItem);
				await Navigation.PushAsync(detailPage);

				// Set event handler for obtaining information.
				detailPage.PizzaOrdered += OnIPizzaDetailOrdered;
			};

			listView.RefreshCommand = new Command (async () => ExecuteCommand ());

			Content = listView;
		}

		protected async Task ExecuteCommand()
		{
			await Task.Delay(100);
			listView.IsRefreshing = false;
		}

		void OnIPizzaDetailOrdered(object sender, Pizza pizza)
		{
			int index = xpizza.IndexOf(pizza);
			if (index != -1)
			{
				xpizza[index] = pizza;
			}
		}
	}
}