using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class PizzaItemCell : ViewCell
	{
		public PizzaItemCell ()
		{
			var icon = new Image();
			icon.SetBinding (Image.SourceProperty, "Icon");

			var name = new Label
			{
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				VerticalTextAlignment = TextAlignment.Center
			};
			name.SetBinding(Label.TextProperty, "Name");

			var price = new Label
			{
				FontSize = Device.GetNamedSize (NamedSize.Micro, typeof(Label)),
				VerticalTextAlignment = TextAlignment.Center
			};
			price.SetBinding(Label.TextProperty, "Price");

			var order = new Image {
				Source = FileImageSource.FromFile ("check.png")
			};
			order.SetBinding (Image.IsVisibleProperty, "Order");

			var layout2 = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Children = 
				{ 
					name,
					price 
				}
			};

			var layout3 = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				Children = 
				{ 
					order
				}
			};

			var layout = new StackLayout
			{
				Padding = new Thickness(10, 0, 10, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = 
				{ 
					icon,
					layout2,
					layout3
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

