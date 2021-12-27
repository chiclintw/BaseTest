using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;

namespace BaseTest
{
	public partial class DevExpressGrid : ContentPage
	{
		int count;

		public DevExpressGrid ()
		{
			Title = "Grid展示";

			InitializeComponent ();

			TestOrdersRepository model = new TestOrdersRepository ();
			BindingContext = model;
		}

		void OnCalculateCustomSummary(object sender, CustomSummaryEventArgs e){
			if (e.FieldName.ToString () == "Shipped")
			if (e.IsTotalSummary){
				if (e.SummaryProcess == CustomSummaryProcess.Start) {
					count = 0;
				}
				if (e.SummaryProcess == CustomSummaryProcess.Calculate) {
					if (!(bool)e.FieldValue)
						count++;
					e.TotalValue = count;
				}
			}
		}
	}
}

