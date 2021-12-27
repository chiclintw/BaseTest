using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DevExpress.Mobile.DataGrid;

namespace BaseTest
{
	public class DevExpressGrid2 : ContentPage
	{
		int count;

		public DevExpressGrid2 ()
		{
			TestOrdersRepository model = new TestOrdersRepository ();

			GridControl gridControl = new GridControl () {
				NewItemRowVisibility = false,
				SortMode = GridSortMode.Multiple,
				AutoFilterPanelVisibility = true,
				Columns = {
					new TextColumn { FieldName="Product.Name", Caption = "Product", Width = 170, SortOrder = DevExpress.Data.ColumnSortOrder.Descending, SortIndex = 0},
					new NumberColumn { FieldName="Product.UnitPrice", Caption = "Price", DisplayFormat="C0"},
					new NumberColumn { FieldName="Quantity", SortOrder = DevExpress.Data.ColumnSortOrder.Ascending, SortIndex = 1},
					new NumberColumn { FieldName="Total", UnboundType=DevExpress.Data.UnboundColumnType.Integer, UnboundExpression="[Quantity] * [Product.UnitPrice]", DisplayFormat="C0", IsReadOnly=true},
					new DateColumn { FieldName="Date", DisplayFormat="d", IsGrouped = true, GroupInterval = DevExpress.Data.ColumnGroupInterval.Date},
					new SwitchColumn { FieldName="Shipped", AllowSort = DevExpress.Utils.DefaultBoolean.False, AllowAutoFilter=false},
				},
				GroupSummaries = {
					new GridColumnSummary { FieldName="Total", Type=DevExpress.Mobile.DataGrid.SummaryType.Max},
				},
				TotalSummaries = {
					new GridColumnSummary {FieldName="Total", Type=DevExpress.Mobile.DataGrid.SummaryType.Sum, DisplayFormat= "Total: {0:C0}"},
					new GridColumnSummary {FieldName="Shipped", Type=DevExpress.Mobile.DataGrid.SummaryType.Custom, DisplayFormat= "Not Shipped: {0}"},
				},
			};
			gridControl.CalculateCustomSummary += OnCalculateCustomSummary;
			gridControl.BindingContext = model;
			gridControl.ItemsSource = model.Orders;

			Content = new StackLayout { 
				Children = {
					gridControl
				}
			};
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


