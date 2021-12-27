using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.BarCodes;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

using Gcm.Client;

namespace BaseTest.Droid
{
	[Activity(Label = "MainActivity", Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		static MainActivity instance;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			DevExpress.Mobile.Forms.Init();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			instance = this;
			//CurrentPlatform.Init();

			// 啟動OxyPlot
			OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

			// 啟動Acr.BarCodes
			BarCodes.Init(() => (Activity)Forms.Context);

			// Xamarin.Forms.Maps
			global::Xamarin.FormsMaps.Init(this, bundle);

			LoadApplication(new App());

			// Push notifications code
			try
			{
				// Check to ensure everything's setup right
				GcmClient.CheckDevice(this);
				GcmClient.CheckManifest(this);

				// Register for push notifications
				System.Diagnostics.Debug.WriteLine("Registering...");
				GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
			}
			catch (Java.Net.MalformedURLException)
			{
				CreateAndShowDialog(new Exception("There was an error creating the Mobile Service. Verify the URL"), "Error");
			}
			catch (Exception e)
			{
				CreateAndShowDialog(e, "Error");
			}
		}

		public static MainActivity CurrentActivity
		{
			get { return instance; }
		}

		void CreateAndShowDialog(Exception exception, String title)
		{
			CreateAndShowDialog(exception.Message, title);
		}

		void CreateAndShowDialog(string message, string title)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);

			builder.SetMessage(message);
			builder.SetTitle(title);
			builder.Create().Show();
		}
	}
}

