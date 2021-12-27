using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class HomePage : MasterDetailPage
	{
		public HomePage ()
		{
			var menuPage = new HomeMenuPage ();
			menuPage.OnMenuSelect = (menuDetailPage) => {
				Detail = new NavigationPage(menuDetailPage);
				IsPresented = false;
			};

			Master = menuPage;

			Detail = new NavigationPage(new TabPage ());

			MasterBehavior = MasterBehavior.Split;
		}
	}
}

