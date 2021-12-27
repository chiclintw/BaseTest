using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class LoginPage : ContentPage
	{
		public LoginPage()
		{
			var titleimg = new Image
			{
				HorizontalOptions = LayoutOptions.Start,
				Source = FileImageSource.FromFile("title.png"),
			};

			var idImage = new Image { Source = FileImageSource.FromFile ("user.png") };
			//var idLabel = new Label { Text = "帳號", VerticalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)) };
			var idEntry = new Entry { Placeholder = "輸入連線帳號" };

			var passImage = new Image { Source = FileImageSource.FromFile ("pass.png") };
			//var passLabel = new Label { Text = "密碼", VerticalTextAlignment = TextAlignment.Center, FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)) };
			var passEntry = new Entry { Placeholder = "輸入連線密碼", IsPassword = true };

			var loginButton = new Button { Image = "login.png" };
			loginButton.Clicked += (sender, e) =>
			{
				Navigation.PushModalAsync(new HomePage());
			};

			// Create a grid to hold the Labels & Entry controls.
			Grid inputGrid = new Grid
			{
				Padding = 20,
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				},
				Children =
				{
					{ idImage, 0, 0 },
					{ passImage, 0, 1 },
					{ idEntry, 1, 0 },
					{ passEntry, 1, 1 }
				}
			};

			// Accomodate iPhone status bar.
			//this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			Content = new ScrollView
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
				Content = new StackLayout
				{
					Children =
					{
						titleimg,
						inputGrid,
						loginButton
					}
				}
			};
		}
	}
}


