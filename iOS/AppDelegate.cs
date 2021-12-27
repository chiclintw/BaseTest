using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using WindowsAzure.Messaging;

namespace BaseTest.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		private SBNotificationHub Hub { get; set; }

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Change UINavigationBar Style
//			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(255, 152, 0);
//			UINavigationBar.Appearance.TintColor = UIColor.White;
//			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() { TextColor = UIColor.White });

			// 啟動OxyPlot
			OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

			// 啟動Acr.BarCodes
			global::Acr.BarCodes.BarCodes.Init();

			// Xamarin.Forms.Maps
			global::Xamarin.FormsMaps.Init ();

			// registers for push Notification
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
					   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
					   new NSSet());

				UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else {
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		public override void OnActivated(UIApplication uiApplication)
		{
			AzureKey.badgeCount = 0;
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = AzureKey.badgeCount;
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			// Modify device token
			string _deviceToken = deviceToken.Description;
			_deviceToken = _deviceToken.Trim('<', '>').Replace(" ", "");

			AzureKey.deviceToken = _deviceToken;

			Hub = new SBNotificationHub(AzureKey.ConnectionString, AzureKey.NotificationHubPath);

			Hub.UnregisterAllAsync(deviceToken, (error) =>
			{
				if (error != null)
				{
					Console.WriteLine("Error calling Unregister: {0}", error.ToString());
					return;
				}

				// NSSet tags = null; // create tags if you want

				NSSet tags = new NSSet( _deviceToken );

				Hub.RegisterNativeAsync(deviceToken, tags, (errorCallback) =>
				{
					if (errorCallback != null)
						Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
				});
			});
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			ProcessNotification(userInfo, false);
		}

		void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			// Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
			if (null != options && options.ContainsKey(new NSString("aps")))
			{
				//Get the aps dictionary
				NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

				string alert = string.Empty;

				//Extract the alert text
				if (aps.ContainsKey(new NSString("alert")))
					alert = (aps[new NSString("alert")] as NSString).ToString();

				//If this came from the ReceivedRemoteNotification while the app was running,
				// we of course need to manually process things like the sound, badge, and alert.
				if (!fromFinishedLaunching)
				{
					//Manually show an alert
					if (!string.IsNullOrEmpty(alert))
					{
						UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
						avAlert.Show();
					}
				}
			}
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			AzureKey.badgeCount = AzureKey.badgeCount + 1;
			UIApplication.SharedApplication.ApplicationIconBadgeNumber = AzureKey.badgeCount;
		}
	}
}

