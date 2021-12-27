using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class AboutPage : ContentPage
	{
		public AboutPage ()
		{
			Title = "關於";

			var titleimg = new Image
			{
				HorizontalOptions = LayoutOptions.Start,
				Source = FileImageSource.FromFile("chititle.png"),
			};

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot("About")
				{
					new TableSection("正航資訊股份有限公司")
					{
						new ViewCell
						{
							View = new StackLayout
							{
								Padding = new Thickness(10,0,10,0),
								Children = 
								{
									new Label
									{
										FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
										Text = "秉持產品標準化、套裝化、服務化之理念，兩岸三地企業電腦化領導供應商。"
									}
								}
							}
						}
					},
					new TableSection("台北總公司")
					{
						new TextCell
						{
							Text = "電話：02-7720-9699",
							Detail = "地址：台北市內湖區瑞光路316巷56號5樓",
						}
					},
					new TableSection("台中分公司")
					{
						new TextCell
						{
							Text = "電話：04-2358-9967",
							Detail = "地址：台中市台灣大道4段925號18樓之2",
						}
					},
					new TableSection("高雄分公司")
					{
						new TextCell
						{
							Text = "電話：07-971-2345",
							Detail = "地址：高雄市前鎮區復興四路12號6樓-16",
						}
					}
				}
			};

			// Build the page.
			this.Content = new StackLayout
			{
				Children = 
				{
					titleimg,
					tableView
				}
			};
		}
	}
}


