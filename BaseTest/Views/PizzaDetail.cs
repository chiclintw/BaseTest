using System;

using Xamarin.Forms;

namespace BaseTest
{
	public partial class PizzaDetail : ContentPage
	{
		// Define a public event for transferring data.
		public EventHandler<Pizza> PizzaOrdered;

		public PizzaDetail (Pizza pizza)
		{
			Title = pizza.Name;

			ToolbarItems.Add(new ToolbarItem {
				Text = "CHECK",
				Icon = "check.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => {
					EventHandler<Pizza> handler = PizzaOrdered;

					pizza.Order = !pizza.Order;

					if (handler != null)
						handler(this, pizza);

					Navigation.PopAsync();
				})
			});

			var pizzaIcon = new Image
			{
				HorizontalOptions = LayoutOptions.Start,
				Source = FileImageSource.FromFile(pizza.Icon),
			};

			var labelPrice = new Label 
			{ 
				Text = " 價錢 ", 
				HorizontalTextAlignment = TextAlignment.Center, 
				VerticalTextAlignment = TextAlignment.Center, 
				BackgroundColor = Color.FromHex("ffa000"),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)) 
			};

			var pizzaPrice = new Label 
			{ 
				Text = pizza.Price, 
				VerticalTextAlignment = TextAlignment.Center, 
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)) 
			};

			var labelNote = new Label 
			{ 
				Text = " 餡料 ", 
				HorizontalTextAlignment = TextAlignment.Center, 
				VerticalTextAlignment = TextAlignment.Center, 
				BackgroundColor = Color.FromHex("ffa000"),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)) 
			};

			var pizzaNote = new Label 
			{ 
				Text = pizza.Note, 
				VerticalTextAlignment = TextAlignment.Center, 
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)) 
			};

			var labelQuantity = new Label 
			{ 
				Text = " 數量 ", 
				HorizontalTextAlignment = TextAlignment.Center, 
				VerticalTextAlignment = TextAlignment.Center, 
				BackgroundColor = Color.FromHex("ffa000"),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)) 
			};

			var pizzaQuantity = new Entry 
			{ 
				Text = "1", 
				Keyboard = Keyboard.Numeric,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)) 
			};

			var labelPS = new Label 
			{ 
				Text = " 備註 ", 
				HorizontalTextAlignment = TextAlignment.Center, 
				VerticalTextAlignment = TextAlignment.Center, 
				BackgroundColor = Color.FromHex("ffa000"),
				FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)) 
			};

			var pizzaPS = new Entry 
			{ 
				Text = "", 
				Keyboard = Keyboard.Text,
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)) 
			};

			// Create a grid to hold the Labels & Entry controls.
			Grid pizzaGrid = new Grid
			{
				Padding = 5,
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				},
				Children =
				{
					{ labelPrice, 0, 0 },
					{ labelNote, 0, 1 },
					{ labelQuantity, 0, 2 },
					{ labelPS, 0, 3 },
					{ pizzaPrice, 1, 0 },
					{ pizzaNote, 1, 1 },
					{ pizzaQuantity, 1, 2 },
					{ pizzaPS, 1, 3 }
				}
			};

			Content = new ScrollView
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
				Content = new StackLayout
				{
					Children =
					{
						pizzaIcon,
						pizzaGrid
					}
				}
			};
		}

		protected override void OnDisappearing()
		{
			//back button logic here
			DisplayAlert("Information", "OnDisappearing", "OK");
		}
	}
}


