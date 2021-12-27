using System;

using Xamarin.Forms;

namespace BaseTest
{
	public class CustomerDetail : ContentPage
	{
		// Define a public event for transferring data.
		public EventHandler<Customer> CustomerSave;

		public CustomerDetail (Customer customer)
		{
			Title = "明細";

			var ecName = new EntryCell 
			{
				Label = "名稱：",
				Text = customer.Name,
				Placeholder = "輸入客戶名稱",
				Keyboard = Keyboard.Default,
			};

			var ecTelphone = new EntryCell 
			{
				Label = "電話：",
				Text = customer.Telphone,
				Placeholder = "輸入客戶聯絡電話",
				Keyboard = Keyboard.Telephone,
			};

			var ecAddress = new EntryCell
			{
				Label = "地址：",
				Text = customer.Address,
				Placeholder = "輸入客戶聯絡地址",
				Keyboard = Keyboard.Default,
			};

			ToolbarItems.Add(new ToolbarItem {
				Text = "SAVE",
				Icon = "save.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(() => {
					EventHandler<Customer> handler = CustomerSave;

					customer.Name = ecName.Text;
					customer.Telphone = ecTelphone.Text;
					customer.Address = ecAddress.Text;

					if (handler != null)
						handler(this, customer);

					Navigation.PopAsync();
				})
			});
			ToolbarItems.Add(new ToolbarItem {
				Text = "DELETE",
				Icon = "delete.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(async() => {

					var action = await DisplayAlert ("訊息", "確定要刪除？", "Yes", "No");

					if (action)
					{
						EventHandler<Customer> handler = CustomerSave;

						customer.Telphone = "DELETE";

						if (handler != null)
							handler(this, customer);

						await Navigation.PopAsync();
					};
				})
			});

			TableView tableView = new TableView
			{
				Intent = TableIntent.Form,
				Root = new TableRoot("CustomerDetail")
				{
					new TableSection("")
					{
						ecName,
						ecTelphone,
						ecAddress
					}
				}
			};

			// Build the page.
			this.Content = new StackLayout
			{
				Children = 
				{
					tableView
				}
			};
		}
	}
}


