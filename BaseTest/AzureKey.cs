using System;

namespace BaseTest
{
	public static class AzureKey
	{
		// Replace strings with your mobile services url and key.
		public const string ConnectionString = "Endpoint=sb://chiappns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=9IU5QG6rLYh4lCxIivagsA2HtfQ8PxmONTSTRdJnt0k=";
		public const string NotificationHubPath = "basetest";
		public const string SenderID = "45025433258"; // Google API Project Number

		public static string deviceToken;
		public static int badgeCount = 0;
	}
}

