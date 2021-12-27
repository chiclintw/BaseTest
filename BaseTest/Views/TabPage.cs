using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class TabPage : TabbedPage
	{
		public TabPage ()
		{
			Title = "功能展示";
			this.Children.Add (new PizzaPage (){ Icon = "tab1.png" });
			this.Children.Add (new CustomerPage (){ Icon = "tab3.png" });
			this.Children.Add (new AboutPage (){ Icon = "tab2.png" });
		}
	}
}


