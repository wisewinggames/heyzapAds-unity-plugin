//https://documentation.onesignal.com/docs/unity-sdk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneSignalPush;

namespace WiseWingGames
{
	

	public class PushNotificationManager : MonoBehaviour
	{
		public static PushNotificationManager instance { get; private set; }

		public string oneSignalAppID;

		void Awake ()
		{
			if (instance != null)
				Destroy (this);
			else
				instance = this;
		}

		void Start ()
		{
			// Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
			// OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

			OneSignal.StartInit (oneSignalAppID)
				.HandleNotificationOpened (HandleNotificationOpened)
				.EndInit ();

			OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

			// Call syncHashedEmail anywhere in your app if you have the user's email.
			// This improves the effectiveness of OneSignal's "best-time" notification scheduling feature.
			// OneSignal.syncHashedEmail(userEmail);
		}

		//		 Gets called when the player opens the notification.
		private static void HandleNotificationOpened (OSNotificationOpenedResult result)
		{
		}

		public void PromptForPermmision(){
			OneSignal.PromptForPushNotificationsWithUserResponse(OneSignal_promptForPushNotificationsReponse);

		}

		private void OneSignal_promptForPushNotificationsReponse(bool accepted) {
			Debug.Log("OneSignal_promptForPushNotificationsReponse: " + accepted);
		}
	}
}