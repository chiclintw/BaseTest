using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

namespace BaseTest
{
	public class CustomerPage : ContentPage
	{
		ListView listView;
		IList<Customer> xcustomer = Customer.Data;

		public CustomerPage ()
		{
			Title = "客戶";

			Padding = new Thickness (0, 10);

			SearchBar searchbar = new SearchBar () {
				Placeholder = "Search",
			};

			searchbar.TextChanged += (sender, e) => {
				SearchFilter(searchbar.Text);
			};

			searchbar.SearchButtonPressed += (sender, e) => {
				SearchFilter(searchbar.Text);
			};

			ToolbarItems.Add(new ToolbarItem {
				Text = "Add",
				Icon = "plus.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(async () =>  {
					var customerItem  = new Customer {Name="", Telphone="", Address=""};
					CustomerDetail detailPage = new CustomerDetail(customerItem);
					await Navigation.PushAsync(detailPage);

					// Set event handler for obtaining information.
					detailPage.CustomerSave += OnCustomerSave;
				})
			});

			ToolbarItems.Add(new ToolbarItem {
				Text = "Barcode",
				Icon = "barcode.png",
				Order = ToolbarItemOrder.Primary,
				Command = new Command(async () =>  {
					var scanResult = await Acr.BarCodes.BarCodes.Instance.Read();  
					if (!scanResult.Success)   
					{  
						await this.DisplayAlert("Alert ! ", "Sorry ! \n Failed to read the Barcode !", "OK");  
					}   
					else   
					{  
						await this.DisplayAlert("Scan Successful !", String.Format("Barcode Format : {0} \n Barcode Value : {1}", scanResult.Format, scanResult.Code), "OK");  
						searchbar.Text = scanResult.Code;
					}
				})
			});

			var itemTemplate = new DataTemplate (typeof(TextCell));
			itemTemplate.SetBinding(TextCell.TextProperty, "Name");

			listView = new ListView () 
			{
				ItemsSource = xcustomer,
				ItemTemplate = itemTemplate
			};

			listView.ItemSelected += async (sender, e) =>
			{
				if(listView.SelectedItem == null) return;

				var customerItem = (Customer)e.SelectedItem;

				// De-select the item.
				listView.SelectedItem = null;

				CustomerDetail detailPage = new CustomerDetail(customerItem);
				await Navigation.PushAsync(detailPage);

				// Set event handler for obtaining information.
				detailPage.CustomerSave += OnCustomerSave;
			};

			var stack = new StackLayout () {
				Children = {
					searchbar,
					listView
				}
			};

			Content = stack;
		}

		public void SearchFilter (string filter)
		{
			listView.BeginRefresh();

			if (string.IsNullOrWhiteSpace(filter))
				listView.ItemsSource = xcustomer;
			else
				listView.ItemsSource = xcustomer.Where(i => i.Name.Contains(filter));

			listView.EndRefresh();			
		}

		void OnCustomerSave(object sender, Customer customer)
		{
			int index = xcustomer.IndexOf(customer);
			if (index != -1) {
				if (customer.Telphone == "DELETE")
					xcustomer.RemoveAt (index);
				else
					xcustomer [index] = customer;
			} else {
				xcustomer.Add (customer);
			}
		}
	}
}

