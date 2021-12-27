using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class MenuItemCell : ViewCell
	{
		public MenuItemCell ()
		{
			var icon = new Image {
				HorizontalOptions = LayoutOptions.Start,
			};
			icon.SetBinding (Image.SourceProperty, "MenuIcon");

			var title = new Label
			{
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				VerticalTextAlignment = TextAlignment.Center
			};
			title.SetBinding(Label.TextProperty, "MenuTitle");

			var layout = new StackLayout
			{
				Padding = new Thickness(10, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				Children = 
				{ 
					icon,
					title 
				}
			};

			View = layout;
		}

		protected override void OnBindingContextChanged()
		{
			// Fixme : this is happening because the View.Parent is getting 
			// set after the Cell gets the binding context set on it. Then it is inheriting
			// the parents binding context.
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged();
		}
	}
}


